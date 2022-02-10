Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Akuntansi_general_ledger
    Inherits System.Web.UI.UserControl
    Public tradingClass As tradingClass = New tradingClass()
    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    'Daniel
    'Sub bindperiode_transaksi()
    '    sqlcom = "select id, name"
    '    sqlcom = sqlcom + " from transaction_period"
    '    sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
    '    'Daniel
    '    'sqlcom = sqlcom + " and bulan = " & DateTime.Now.Month
    '    'Daniel
    '    sqlcom = sqlcom + " order by bulan"
    '    reader = connection.koneksi.SelectRecord(sqlcom)
    '    Me.dd_bulan.DataSource = reader
    '    Me.dd_bulan.DataTextField = "name"
    '    Me.dd_bulan.DataValueField = "id"
    '    Me.dd_bulan.DataBind()
    '    reader.Close()
    '    connection.koneksi.CloseKoneksi()
    '    'Daniel
    '    Me.dd_bulan.SelectedValue = tradingClass.IDTransaksiPeriod()
    '    'Daniel
    'End Sub
    'Daniel

    Sub clearakun()
        Me.tb_id_akun.Text = "0"
        Me.lbl_akun.Text = "------"
        Me.link_popup_akun.Visible = True
    End Sub

    Sub bindakun()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun.Text = reader.Item("nama_akun").ToString
            reader.Close()
            connection.koneksi.CloseKoneksi()
            Me.loadgrid()
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    'Sub bindakun()
    '    sqlcom = "select AccountNo, InaName from coa_list"
    '    sqlcom = sqlcom + " where IsControl = 'N'"
    '    sqlcom = sqlcom + " order by InaName"
    '    reader = connection.koneksi.SelectRecord(sqlcom)
    '    Me.dd_akun.DataSource = reader
    '    Me.dd_akun.DataTextField = "InaName"
    '    Me.dd_akun.DataValueField = "AccountNo"
    '    Me.dd_akun.DataBind()
    '    Me.dd_akun.Items.Add(New ListItem("---Semua akun---", "0"))
    '    reader.Close()
    '    connection.koneksi.CloseKoneksi()
    'End Sub

    Sub bindperiode_transaksi()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
        sqlcom = sqlcom + " order by bulan"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_bulan.DataSource = reader
        Me.dd_bulan.DataTextField = "name"
        Me.dd_bulan.DataValueField = "id"
        Me.dd_bulan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindtotal()
        Try
            sqlcom = Nothing
            sqlcom = "select isnull(sum(isnull(nilai_debet,0)),0) as nilai_debet, isnull(sum(isnull(nilai_kredit,0)),0) as nilai_kredit,"
            sqlcom = sqlcom + " isnull(sum(isnull(nilai_debet,0)) - sum(isnull(nilai_kredit,0)),0) as saldo"
            sqlcom = sqlcom + " from akun_general_ledger"
            sqlcom = sqlcom + " inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
            'Daniel

            If Me.rd_semua.Checked = True Then
                sqlcom = sqlcom + " where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            Else
                sqlcom = sqlcom + " where id_transaksi in (select id_transaksi from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue) & " and (coa_code = '" + Me.tb_id_akun.Text.Trim.Trim + "' or coa_code_lawan = '" + Me.tb_id_akun.Text.Trim.Trim + "'))"
            End If


            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and (coa_code like '%" & Me.tb_search.Text.Trim & "%' or coa_code_lawan like '%" & Me.tb_search.Text.Trim & "%' or coa_list.inaname like '%" & Me.tb_search.Text & "%' or keterangan like '%" & Me.tb_search.Text.Trim & "%' or no_voucher like '%" & Me.tb_search.Text.Trim & "%')"
            End If

            sqlcom = sqlcom + " order by 2,1"


            'sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)

            'If Me.rd_semua.Checked = True Then
            '    sqlcom = sqlcom
            'Else
            'sqlcom = sqlcom + " and coa_code = '" & Me.tb_id_akun.Text.Trim.Trim & "'"
            'sqlcom = sqlcom + " and id_transaksi in (select id_transaksi from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.tgl_transaksi >= '" + Me.tradingClass.DateValidated(Me.tb_tgl_awal.Text) + "' and akun_general_ledger.tgl_transaksi <= '" + Me.tradingClass.DateValidated(Me.tb_tgl_akhir.Text) + "' and coa_code = '" + Me.tb_id_akun.Text.Trim.Trim + "')"
            'End If
            'Daniel

            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_total_debet.Text = FormatNumber(reader.Item("nilai_debet").ToString, 2)
                Me.lbl_total_kredit.Text = FormatNumber(reader.Item("nilai_kredit").ToString, 2)
                Me.lbl_saldo.Text = FormatNumber(reader.Item("saldo").ToString, 2)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub checkdata()
        Try

            sqlcom = Nothing
            sqlcom = "select seq, id_transaksi, convert(char, tgl_transaksi, 103) as tgl_transaksi, kode_transaksi, coa_code, isnull(nilai_debet,0) as nilai_debet, isnull(nilai_kredit,0) as nilai_kredit, keterangan, id_currency, coa_list.inaname as nama_akun, isnull(kurs,0) as kurs,no_voucher from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code "
            sqlcom = sqlcom + " where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)

            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tbl_search.Visible = True
            Else
                Me.tbl_search.Visible = False
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try
            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, id_transaksi, convert(char, tgl_transaksi, 103) as tgl_transaksi,"
            sqlcom = sqlcom + " kode_transaksi, coa_code, coa_code_lawan, isnull(nilai_debet,0) as nilai_debet, isnull(nilai_kredit,0) as nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_currency, coa_list.inaname as nama_akun, isnull(kurs,0) as kurs, no_voucher"
            sqlcom = sqlcom + " from akun_general_ledger"
            sqlcom = sqlcom + " inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"


            If Me.rd_semua.Checked = True Then
                sqlcom = sqlcom + " where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            Else
                sqlcom = sqlcom + " where id_transaksi in (select id_transaksi from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue) & " and (coa_code = '" + Me.tb_id_akun.Text.Trim.Trim + "' or coa_code_lawan = '" + Me.tb_id_akun.Text.Trim.Trim + "'))"
            End If


            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and (coa_code like '%" & Me.tb_search.Text.Trim & "%' or coa_code_lawan like '%" & Me.tb_search.Text.Trim & "%' or coa_list.inaname like '%" & Me.tb_search.Text & "%' or keterangan like '%" & Me.tb_search.Text.Trim & "%' or no_voucher like '%" & Me.tb_search.Text.Trim & "%')"
            End If

            sqlcom = sqlcom + " order by 2,1"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "akun_general_ledger")

                Me.dg_data.DataSource = ds.Tables("akun_general_ledger").DefaultView

                If ds.Tables("akun_general_ledger").Rows.Count > 0 Then
                    If ds.Tables("akun_general_ledger").Rows.Count > 200 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 200
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True

                    Me.btn_print.Enabled = True

                    'Daniel 7/8/2017=================================================================
                    Me.btn_update.Visible = True
                    Me.btn_delete.Visible = True
                    '=================================================================Daniel 7/8/2017

                Else

                    Me.dg_data.Visible = False
                    Me.btn_print.Enabled = False

                    'Daniel 7/8/2017=================================================================
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
                    '=================================================================Daniel 7/8/2017

                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()

            Me.bindtotal()

        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearakun()

            Me.tb_tahun.Text = Year(Now.Date)
            Me.bindperiode_transaksi()

            Me.btn_update.Visible = False
            Me.btn_delete.Visible = False
            'Daniel
            Me.rd_semua.Checked = True
            Me.tb_id_akun.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun.Attributes.Add("style", "display: none;")
            Me.popup_akun()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    'Daniel
    'Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
    '    Me.loadgrid()
    'End Sub
    'Daniel

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        'Daniel
        'Me.bindperiode_transaksi()
        'Daniel
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    'Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
    '    Try
    '        Dim reportPath As String = Nothing
    '        Dim file_pdf As String = Nothing
    '        If Me.rd_semua.Checked = True Then
    '            reportPath = Server.MapPath("reports\general_ledger_all.rpt")
    '            file_pdf = "Pdf_files/general_ledger_all.pdf"
    '        Else
    '            reportPath = Server.MapPath("reports\general_ledger_per_akun.rpt")
    '            file_pdf = "Pdf_files/general_ledger_per_akun.pdf"
    '        End If

    '        Me.CrystalReportSource1.Report.FileName = reportPath
    '        Me.CrystalReportSource1.ReportDocument.Close()
    '        Me.CrystalReportSource1.ReportDocument.Refresh()
    '        Dim oExO As CrystalDecisions.Shared.ExportOptions
    '        Dim oExDo As New CrystalDecisions.Shared.DiskFileDestinationOptions()
    '        Dim con As New System.Data.SqlClient.SqlConnectionStringBuilder

    '        con.ConnectionString = ConfigurationManager.ConnectionStrings("trading").ToString
    '        Dim consinfo As New CrystalDecisions.Shared.ConnectionInfo
    '        consinfo.ServerName = con.DataSource
    '        consinfo.UserID = con.UserID
    '        consinfo.DatabaseName = con.InitialCatalog
    '        consinfo.Password = con.Password
    '        consinfo.Type = CrystalDecisions.Shared.ConnectionInfoType.SQL
    '        Dim oRD As CrystalDecisions.CrystalReports.Engine.ReportDocument = Me.CrystalReportSource1.ReportDocument
    '        Dim myTables As CrystalDecisions.CrystalReports.Engine.Tables = oRD.Database.Tables
    '        For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
    '            Dim myTableLogonInfo As CrystalDecisions.Shared.TableLogOnInfo = myTable.LogOnInfo
    '            myTableLogonInfo.ConnectionInfo = consinfo
    '            myTable.ApplyLogOnInfo(myTableLogonInfo)
    '        Next
    '        oRD.Load(reportPath)
    '        'Daniel
    '        'oRD.SetParameterValue("id_periode", Me.dd_bulan.SelectedValue)

    '        If Me.tb_tgl_awal.Text <> Nothing And Me.tb_tgl_awal.Text <> Nothing Then
    '            oRD.SetParameterValue("tgl_awal", Me.tradingClass.DateValidated(Me.tb_tgl_awal.Text))
    '            oRD.SetParameterValue("tgl_akhir", Me.tradingClass.DateValidated(Me.tb_tgl_akhir.Text))
    '        Else
    '            oRD.SetParameterValue("tgl_awal", String.Empty)
    '            oRD.SetParameterValue("tgl_akhir", String.Empty)
    '        End If

    '        'Daniel

    '        If Me.rd_akun.Checked = True Then
    '            oRD.SetParameterValue("kode_akun", Me.tb_id_akun.Text)
    '        End If

    '        oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
    '        oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
    '        oExO = oRD.ExportOptions
    '        oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
    '        oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(file_pdf))
    '        Dim vscript As String = ""
    '        vscript = "<script>" & vbCrLf
    '        vscript = vscript + "window.open('" & file_pdf & "', '_blank', 'width=800,height=600,toolbar=no,menubar=no,location=no,titlebar=no, scroolbar=yes');" & vbCrLf
    '        vscript = vscript + "</script>"
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
    '        connection.koneksi.CloseKoneksi()
    '    Catch ex As Exception
    '        tradingClass.Alert(ex.Message, Me.Page)
    '    End Try
    'End Sub

    Protected Sub link_refresh_akun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun.Click
        Me.bindakun()
    End Sub

    Sub popup_akun()
        If Me.rd_akun.Checked = True Then
            Me.rd_semua.Checked = False
            Me.link_popup_akun.Enabled = True
            Me.link_popup_akun.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun.ClientID & "', '" & Me.link_refresh_akun.UniqueID & "')")
        Else
            Me.rd_semua.Checked = True
            Me.link_popup_akun.Enabled = False
            Me.link_popup_akun.Attributes.Clear()
        End If
        Me.loadgrid()
    End Sub

    Protected Sub rd_akun_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rd_akun.CheckedChanged
        Me.popup_akun()
    End Sub

    Protected Sub rd_semua_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rd_semua.CheckedChanged
        Me.popup_akun()
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update akun_general_ledger"
                    sqlcom = sqlcom + " set keterangan = '" & CType(Me.dg_data.Items(x).FindControl("tb_keterangan"), TextBox).Text & "'"
                    'Daniel
                    sqlcom = sqlcom + " , nilai_debet = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("lbl_nilai_debet"), TextBox).Text)
                    sqlcom = sqlcom + " , nilai_kredit = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("lbl_nilai_kredit"), TextBox).Text)
                    sqlcom = sqlcom + " , kurs = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("lbl_kurs"), TextBox).Text)
                    sqlcom = sqlcom + " , no_voucher = '" & CType(Me.dg_data.Items(x).FindControl("tb_no_voucher"), TextBox).Text & "'"
                    'Daniel
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)

                    tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    'Daniel
    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete akun_general_ledger"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)

                    tradingClass.Alert("Data sudah dihapus", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
    'Daniel

    Protected Sub buttonSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSearch.Click
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    Protected Sub btn_refresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        Me.bindperiode_transaksi()
    End Sub
End Class
