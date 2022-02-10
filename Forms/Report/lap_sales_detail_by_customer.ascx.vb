Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_lap_sales_detail_by_customer
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearcustomer()
        Me.tb_id_customer.Text = 0
        Me.lbl_nama_customer.Text = "-----"
        Me.link_popup_customer.Visible = True
    End Sub

    Sub bindcustomer()
        sqlcom = "select name, is_polos, code_sales from daftar_customer where id = " & Me.tb_id_customer.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_customer.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Dim reportPath As String
 
            if Me.dd_pilihan.SelectedValue = "A" then
               reportPath = Server.MapPath("reports\lap_sales_detail_by_customer.rpt")
            else
               reportPath = Server.MapPath("reports\lap_sales_detail_by_customer_customername.rpt")
            end if

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
            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            if Me.dd_pilihan.SelectedValue = "C" then
               oRD.SetParameterValue("id_customer", Me.tb_id_customer.Text)
            end if
            oRD.SetParameterValue("tgl_awal", vtgl_awal)
            oRD.SetParameterValue("tgl_akhir", vtgl_akhir)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_sales_detail_by_customer.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/lap_sales_detail_by_customer.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tgl_awal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
            Me.tb_tgl_akhir.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
            Me.dd_pilihan.SelectedValue = "A"
	    Me.tb_id_customer.Attributes.Add("style", "display: none;")
            Me.link_refresh_customer.Attributes.Add("style", "display: none;")
            Me.popup()
        End If
    End Sub

    Protected Sub link_refresh_customer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_customer.Click
        Me.bindcustomer()
    End Sub

    Sub popup()
        Me.clearcustomer()
        if Me.dd_pilihan.SelectedValue="A" then
           Me.link_popup_customer.Enabled = "False"
           Me.link_popup_customer.Attributes.clear()
        else
           Me.link_popup_customer.Enabled = "True"
           Me.link_popup_customer.Attributes.Add("onclick", "popup_customer('" & Me.tb_id_customer.ClientID & "','" & Me.link_refresh_customer.UniqueID & "')")
        end if
    End Sub

    Protected Sub dd_pilihan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_pilihan.SelectedIndexChanged
        Me.popup()
    End Sub
End Class
