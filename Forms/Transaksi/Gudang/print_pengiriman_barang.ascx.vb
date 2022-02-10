Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Penjualan_print_pengiriman_barang
    Inherits System.Web.UI.UserControl

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub


    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Dim reportPath As String

            

            reportPath = Server.MapPath("reports\do_print.rpt")

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
            dim vno_awal as integer = int(me.tb_nomor_awal.text)
            dim vno_akhir as integer = int(me.tb_nomor_akhir.text)            
            oRD.SetParameterValue("tahun", Me.tb_tahun.Text)
            oRD.SetParameterValue("so_no_awal", vno_awal)
            oRD.SetParameterValue("so_no_akhir", vno_akhir)

            'oRD.SetParameterValue("do_no", vno_awal)

            'Dim i As Integer = 0
            'Dim PrintDoc As New Drawing.Printing.PrintDocument()
            'Dim PkSize As New System.Drawing.Printing.PaperSize("", 1, 1)

            'For i = 0 To PrintDoc.PrinterSettings.PaperSizes.Count - 1
                'If PrintDoc.PrinterSettings.PaperSizes.Item(i).PaperName = "do" Then
                    'PkSize = PrintDoc.PrinterSettings.PaperSizes.Item(i)
                'End If
            'Next

            'oRD.PrintOptions.PaperSize = CType(PkSize.Kind, CrystalDecisions.Shared.PaperSize)

            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            'oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/do_print.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/do_print.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tahun.Text = Now.Year
        End If
    End Sub
    
End Class
