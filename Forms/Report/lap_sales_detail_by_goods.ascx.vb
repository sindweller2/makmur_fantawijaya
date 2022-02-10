Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_lap_sales_detail_by_goods
    Inherits System.Web.UI.UserControl

    Public tradingClass As tradingClass = New tradingClass()

    Dim sqlcom As String

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try

            If Me.RadioButtonPDF.Checked = True And Me.RadioButtonExcel.Checked = False Then


                Dim reportPath As String = Server.MapPath("reports\lap_sales_detail_by_goods.rpt")
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

                oRD.SetParameterValue("tgl_awal", vtgl_awal)
                oRD.SetParameterValue("tgl_akhir", vtgl_akhir)
                oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
                oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
                oExO = oRD.ExportOptions
                oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_sales_detail_by_goods.pdf"))
                Dim vscript As String = ""
                vscript = "<script>" & vbCrLf
                vscript = vscript + "window.open('Pdf_files/lap_sales_detail_by_goods.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
                vscript = vscript + "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
                connection.koneksi.CloseKoneksi()

            ElseIf Me.RadioButtonPDF.Checked = False And Me.RadioButtonExcel.Checked = True Then

                Dim DataTable As DataTable
                sqlcom = Nothing

                sqlcom = "select product_item.qty_conversion as 'Conversion Unit', "
                sqlcom = sqlcom & "sales_order_detail.nama_product as 'Material Name', "
                sqlcom = sqlcom & "sales_order.so_no_text as 'S.O No.', "
                sqlcom = sqlcom & "rtrim(convert(char, sales_order.tanggal, 103)) as 'Date', "
                sqlcom = sqlcom & "daftar_customer.name as 'Customer Name', "
                sqlcom = sqlcom & "sales_order_detail.qty as 'Quantity', "
                sqlcom = sqlcom & "case when product_item.is_packaging = 'Q' then measurement_unit.name when product_item.is_packaging = 'P' then packaging.name end as 'Unit', "
                sqlcom = sqlcom & "case    when sales_order.ppn = 0 then (sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100) / 1.1  * sales_order.rate    when sales_order.ppn = 10 then (sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order.rate end as 'Price', "
                sqlcom = sqlcom & "case    when sales_order.ppn = 0 then       case           when daftar_customer.is_kawasan_berikat = 'Y' then               case                  when sales_order.id_currency = 'IDR' then                     round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order_detail.qty,0) * sales_order.rate                  when sales_order.id_currency = 'USD' then                     round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order_detail.qty,3) * sales_order.rate               end           when daftar_customer.is_kawasan_berikat = 'T' then               case                  when sales_order.id_currency = 'IDR' then                       round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100) / 1.1  * sales_order_detail.qty,0) * sales_order.rate                  when sales_order.id_currency = 'USD' then                       round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100) / 1.1  * sales_order_detail.qty,3) * sales_order.rate               end       end    when sales_order.ppn = 10 then         case             when sales_order.id_currency = 'IDR' then                 round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order_detail.qty,0) * sales_order.rate             when sales_order.id_currency = 'USD' then 	            round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order_detail.qty,3) * sales_order.rate         end end as 'Total' "
                sqlcom = sqlcom & "from sales_order inner join daftar_customer on daftar_customer.id = sales_order.id_customer inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no inner join product_item on product_item.id = sales_order_detail.id_product inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion inner join measurement_unit packaging on packaging.id = product_item.id_measurement "
                sqlcom = sqlcom & "where (sales_order.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and sales_order.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') "
                sqlcom = sqlcom & "order by sales_order_detail.nama_product, sales_order.tanggal"

                DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                tradingClass.WriteExcel("~/Excel_files/lap_sales_detail_by_goods.xls", DataTable, 1)
                tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_sales_detail_by_goods.xls", "800", "600", "no", "yes")

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
            Me.tb_tgl_akhir.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
        End If
    End Sub
End Class
