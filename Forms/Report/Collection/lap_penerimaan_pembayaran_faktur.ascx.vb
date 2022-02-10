Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_lap_penerimaan_pembayaran_faktur
    Inherits System.Web.UI.UserControl

    Public tradingClass As tradingClass = New tradingClass()

    Dim sqlcom As String

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try

            If Me.RadioButtonPDF.Checked = True And Me.RadioButtonExcel.Checked = False Then

                Dim reportPath As String = Server.MapPath("reports\penerimaan_pembayaran_penjualan.rpt")
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
                oRD.SetParameterValue("tanggal", Me.tb_tgl_awal.Text)
                oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
                oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal
                oExO = oRD.ExportOptions
                oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/penerimaan_pembayaran_penjualan.pdf"))
                Dim vscript As String = ""
                vscript = "<script>" & vbCrLf
                vscript = vscript + "window.open('Pdf_files/penerimaan_pembayaran_penjualan.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
                vscript = vscript + "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
                connection.koneksi.CloseKoneksi()

            ElseIf Me.RadioButtonPDF.Checked = False And Me.RadioButtonExcel.Checked = True Then

                Dim DataTable As DataTable
                sqlcom = Nothing

                sqlcom = "SELECT sales_order.so_no_text as 'No. Penjualan', "
                sqlcom = sqlcom & "daftar_customer.name as 'Nama Customer', "
                sqlcom = sqlcom & "jenis_pembayaran.name as 'Jenis Pembayaran', "
                sqlcom = sqlcom & "case    when (select id_bank from bank_account where id = pembayaran_invoice_penjualan.id_bank_account) is NULL then          bank_account.name    when (select id_bank from bank_account where id = pembayaran_invoice_penjualan.id_bank_account) is not NULL then          (select name from bank_list where id = (select id_bank from bank_account where id = pembayaran_invoice_penjualan.id_bank_account)) end as 'Kas / Bank', "
                sqlcom = sqlcom & "no_cek_giro as 'No. Cek/Giro', "
                sqlcom = sqlcom & "rtrim(convert(char, tgl_cek_giro, 103)) as 'Tanggal Cek/Giro', "
                sqlcom = sqlcom & "rtrim(convert(char, tgl_jatuh_tempo_cek_giro, 103)) as 'Tgl. Jatuh Tempo', "
                sqlcom = sqlcom & "pembayaran_invoice_penjualan.id_currency as 'Mata Uang', "
                sqlcom = sqlcom & "nilai_pembayaran as 'Nilai Pembayaran', "
                sqlcom = sqlcom & "potongan as 'Nilai Potongan', "
                sqlcom = sqlcom & "kelebihan as 'Nilai Kelebihan', "
                sqlcom = sqlcom & "pembayaran_invoice_penjualan.keterangan as 'Keterangan' "
                sqlcom = sqlcom & "FROM pembayaran_invoice_penjualan inner join sales_order on sales_order.no = pembayaran_invoice_penjualan.no_so inner join jenis_pembayaran on jenis_pembayaran.id = pembayaran_invoice_penjualan.id_jenis_pembayaran inner join bank_account on bank_account.id = pembayaran_invoice_penjualan.id_bank_account inner join daftar_customer on daftar_customer.id = sales_order.id_customer where  rtrim(convert(char, pembayaran_invoice_penjualan.tanggal, 103)) = '" & Me.tb_tgl_awal.Text.Trim & "' order by pembayaran_invoice_penjualan.seq"

                DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                tradingClass.WriteExcel("~/Excel_files/penerimaan_pembayaran_penjualan.xls", DataTable, 1)
                tradingClass.OpenBrowser(Me.Page, "Excel_files/penerimaan_pembayaran_penjualan.xls", "800", "600", "no", "yes")

            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tgl_awal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
        End If
    End Sub
End Class
