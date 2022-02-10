Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Akuntansi_general_ledger_bagian_total
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

    Sub bindtransaksi()
        Try
            sqlcom = "select code_transaksi, nama_transaksi"
            sqlcom = sqlcom + " from gl_transaksi"
            sqlcom = sqlcom + " where bagian = '" & Me.dd_bagian.selectedValue & "'"
            sqlcom = sqlcom + " order by nama_transaksi"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_transaksi.DataSource = reader
            Me.dd_transaksi.DataTextField = "nama_transaksi"
            Me.dd_transaksi.DataValueField = "code_transaksi"
            Me.dd_transaksi.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            If Me.dd_transaksi.SelectedValue = "INVJUAL" Then
                sqlcom = "select coa_parent.inaname as coa_name, coa_parent_lawan.inaname as coa_lawan_name, "
                sqlcom = sqlcom + " sum(nilai_debet) as total_nilai_debet, sum(nilai_kredit) as total_nilai_kredit"
                sqlcom = sqlcom + " from akun_general_ledger"
                sqlcom = sqlcom + " inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
                sqlcom = sqlcom + " inner join coa_list coa_lawan on coa_lawan.accountno = akun_general_ledger.coa_code_lawan"
                sqlcom = sqlcom + " inner join coa_list coa_parent on coa_parent.accountno = coa_list.parentacc"
                sqlcom = sqlcom + " inner join coa_list coa_parent_lawan on coa_parent_lawan.accountno = coa_lawan.parentacc"
                sqlcom = sqlcom + " where kode_transaksi = 'INVJUAL'"
                sqlcom = sqlcom + " and id_transaction_period = " & Me.dd_bulan.SelectedValue
                sqlcom = sqlcom + " group by coa_parent.inaname, coa_parent_lawan.inaname"
            End If

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "akun_general_ledger")
                Me.dg_data.DataSource = ds.Tables("akun_general_ledger").DefaultView

                If ds.Tables("akun_general_ledger").Rows.Count > 0 Then
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_print.Enabled = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_print.Enabled = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()

        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tahun.Text = Now.Year
            Me.bindperiode_transaksi()
            Me.bindtransaksi()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
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
    '        oRD.SetParameterValue("id_periode", Me.dd_bulan.SelectedValue)

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
    '        vscript = vscript + "window.open('" & file_pdf & "', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
    '        vscript = vscript + "</script>"
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
    '        connection.koneksi.CloseKoneksi()
    '    Catch ex As Exception
    '        Me.lbl_msg.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub dd_bagian_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bagian.SelectedIndexChanged
        Me.bindtransaksi()
        Me.loadgrid()
    End Sub


    Protected Sub dd_transaksi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_transaksi.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_tahun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_tahun.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.loadgrid()
    End Sub
End Class
