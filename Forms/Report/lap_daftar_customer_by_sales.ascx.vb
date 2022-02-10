Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_lap_daftar_customer_by_sales
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearsales()
        Me.code.Text = 0
        Me.lbl_nama_sales.Text = "-----"
        Me.link_popup_sales.Visible = True
    End Sub

    Sub bindsales()
        sqlcom = "select code,nama_pegawai,case when status = 1 then 'Aktif' when status = 0 then 'Tidak aktif' end as status from user_list where code = " & Me.code.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_sales.Text = reader.Item("nama_pegawai").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub popup()
        Me.clearsales()
        If Me.dd_pilihan.SelectedValue = "A" Then
            Me.link_popup_sales.Enabled = "False"
            Me.link_popup_sales.Attributes.Clear()
        Else
            Me.link_popup_sales.Enabled = "True"
            Me.link_popup_sales.Attributes.Add("onclick", "popup_sales('" & Me.code.ClientID & "','" & Me.link_refresh_sales.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Dim reportPath As String
            
            If Me.dd_pilihan.SelectedValue = "A" Then
                reportPath = Server.MapPath("reports\lap_daftar_customer_by_all_sales.rpt")
            Else
                reportPath = Server.MapPath("reports\lap_daftar_customer_by_sales.rpt")
            End If

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

            If Me.dd_pilihan.SelectedValue = "C" Then
                oRD.SetParameterValue("code", Me.code.Text)
            End If

            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile

            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf

            If Me.dd_pilihan.SelectedValue = "A" Then
                oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_daftar_customer_by_all_sales.pdf"))
                vscript = vscript + "window.open('Pdf_files/lap_daftar_customer_by_all_sales.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            Else
                oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_daftar_customer_by_sales.pdf"))
                vscript = vscript + "window.open('Pdf_files/lap_daftar_customer_by_sales.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            End If


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
            Me.dd_pilihan.SelectedValue = "A"
            Me.code.Attributes.Add("style", "display: none;")
            Me.link_refresh_sales.Attributes.Add("style", "display: none;")
            Me.popup()
        End If
    End Sub

    Protected Sub dd_pilihan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_pilihan.SelectedIndexChanged
        Me.popup()
    End Sub

    Protected Sub link_refresh_sales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_sales.Click
        Me.bindsales()
    End Sub
End Class
