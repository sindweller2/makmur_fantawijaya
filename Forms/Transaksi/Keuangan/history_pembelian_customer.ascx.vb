Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_history_pembelian_customer
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearcustomer()
            Me.tb_tahun.Text = Year(Now.Date)
            Me.tb_id_customer.Attributes.Add("style", "display: none;")
            Me.link_refresh_customer.Attributes.Add("style", "display: none;")
            Me.link_popup_customer.Attributes.Add("onclick", "popup_customer('" & Me.tb_id_customer.ClientID & "','" & Me.link_refresh_customer.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Sub print()
        Try
            Dim reportPath As String = Server.MapPath("reports\history_pembelian_customer.rpt")
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

            oRD.SetParameterValue("tahun", Me.tb_tahun.Text)
            oRD.SetParameterValue("id_customer", Me.tb_id_customer.Text)            
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/history_pembelian_customer.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/history_pembelian_customer.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        If String.IsNullOrEmpty(Me.tb_tahun.Text) Then
            Me.lbl_msg.Text = "Silahkan mengisi tahun terlebih dahulu"
        ElseIf Me.tb_id_customer.Text = 0 Then
            Me.lbl_msg.Text = "Silahkan mengisi nama customer terlebih dahulu"
        Else
            Me.print()
        End If
    End Sub

    Protected Sub link_refresh_customer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_customer.Click
        Me.bindcustomer()
    End Sub
End Class
