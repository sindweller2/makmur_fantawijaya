
Partial Class Forms_Report_lap_daftar_coa
    Inherits System.Web.UI.UserControl

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Dim reportPath As String = Server.MapPath("reports\coa_list.rpt")
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
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/coa_list.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/coa_list.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub
End Class
