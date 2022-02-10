Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_history_kas
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

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

    Sub bindkasbank()
        sqlcom = "select id, name from bank_account order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_kas_bank.DataSource = reader
        Me.dd_kas_bank.DataTextField = "name"
        Me.dd_kas_bank.DataValueField = "id"
        Me.dd_kas_bank.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, rtrim(convert(char, tanggal, 103)) as tanggal, keterangan, nama_customer,"
            sqlcom = sqlcom + " isnull(nilai_debet,0) as nilai_debet, isnull(nilai_kredit,0) as nilai_kredit"
            'Daniel
            sqlcom = sqlcom + " , seq , no_voucher"
            'Daniel
            sqlcom = sqlcom + " from history_kas"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            sqlcom = sqlcom + " and id_cash_bank = " & Me.dd_kas_bank.SelectedValue

            if Not String.IsnullOrEmpty(Me.tb_search.text) then
                sqlcom = sqlcom + " and (keterangan like '%" & Me.tb_search.Text & "%' or no_voucher like '%" & Me.tb_search.Text & "%')"
            end if

            sqlcom = sqlcom + " order by rtrim(convert(char, tanggal, 103))"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "history_kas")
                Me.dg_data.DataSource = ds.Tables("history_kas").DefaultView

                If ds.Tables("history_kas").Rows.Count > 0 Then
                    If ds.Tables("history_kas").Rows.Count > 100 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 100
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_update.Visible = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bulansekarang()
        Try
            sqlcom = "select id from transaction_period where bulan = " & Month(Now.Date) & " and tahun = " & Year(Now.Date)
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.dd_bulan.SelectedValue = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tahun.Text = Now.Year
            Me.bindperiode_transaksi()
            Me.bulansekarang()
            Me.bindkasbank()
            Me.loadgrid()

            'Dim thread = New Threading.Thread(AddressOf loadgrid)
            'thread.IsBackground = True
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub dd_kas_bank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_kas_bank.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Dim reportPath As String = Nothing
            Dim file_pdf As String = Nothing

            sqlcom = "select id_bank from bank_account where id = " & Me.dd_kas_bank.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If String.IsNullOrEmpty(reader.Item("id_bank").ToString) Then
                    reportPath = Server.MapPath("reports\buku_kas.rpt")
                    file_pdf = "Pdf_files/buku_kas.pdf"
                Else
                    reportPath = Server.MapPath("reports\buku_bank.rpt")
                    file_pdf = "Pdf_files/buku_bank.pdf"
                End If
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            Me.CrystalReportSource1.Report.FileName = reportPath
            Me.CrystalReportSource1.ReportDocument.Close()
            Me.CrystalReportSource1.ReportDocument.Refresh()
            Dim oExO As CrystalDecisions.Shared.ExportOptions
            Dim oExDo As New CrystalDecisions.Shared.DiskFileDestinationOptions()
            Dim con As New System.Data.SqlClient.SqlConnectionStringBuilder

            con.ConnectionString = ConfigurationManager.ConnectionStrings("trading").ToString
            Dim consinfo As New CrystalDecisions.Shared.ConnectionInfo
            consinfo.ServerName = con.DataSource
            consinfo.UserID = con.UserID
            consinfo.DatabaseName = con.InitialCatalog
            consinfo.Password = con.Password
            consinfo.Type = CrystalDecisions.Shared.ConnectionInfoType.SQL
            Dim oRD As CrystalDecisions.CrystalReports.Engine.ReportDocument = Me.CrystalReportSource1.ReportDocument
            Dim myTables As CrystalDecisions.CrystalReports.Engine.Tables = oRD.Database.Tables
            For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                Dim myTableLogonInfo As CrystalDecisions.Shared.TableLogOnInfo = myTable.LogOnInfo
                myTableLogonInfo.ConnectionInfo = consinfo
                myTable.ApplyLogOnInfo(myTableLogonInfo)
            Next
            oRD.Load(reportPath)
            oRD.SetParameterValue("id_periode", Me.dd_bulan.SelectedValue)
            oRD.SetParameterValue("id_kas", Me.dd_kas_bank.SelectedValue)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(file_pdf))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('" & file_pdf & "', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        me.loadgrid()
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update history_kas"
                    sqlcom = sqlcom + " set no_voucher = '" & CType(Me.dg_data.Items(x).FindControl("TextboxNoVoucher"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)

                    sqlcom = Nothing

                    sqlcom = "update akun_general_ledger"
                    sqlcom = sqlcom + " set no_voucher = '" & CType(Me.dg_data.Items(x).FindControl("TextboxNoVoucher"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where keterangan = '" & CType(Me.dg_data.Items(x).FindControl("lbl_keterangan"), Label).Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)

                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
