Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_lap_omset_penjualan_perbulan
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    sub print()
        Try
            Dim reportPath As String = Server.MapPath("reports\lap_omset_penjualan_per_bulan.rpt")
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
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_omset_penjualan_per_bulan.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/lap_omset_penjualan_per_bulan.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    end sub


    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
         Me.grand_total()
         Me.grand_ppn()
         Me.print()
    End Sub    

    sub grand_total()
        Try
            sqlcom = "delete temp_grand_total_sos"
            connection.koneksi.DeleteRecord(sqlcom)


            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            sqlcom = "insert into temp_grand_total_sos (grand_total)"
            sqlcom = sqlcom + " select "
            sqlcom = sqlcom + " round( "
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + "   when sales_order.ppn = 0 then "
            sqlcom = sqlcom + "     case "
            sqlcom = sqlcom + "       when daftar_customer.is_kawasan_berikat = 'Y' then "
            sqlcom = sqlcom + "            isnull((select sum((y.harga_jual - y.harga_jual * y.discount /100) * x.rate * y.qty) "
            sqlcom = sqlcom + "            from sales_order x  "
            sqlcom = sqlcom + "            inner join sales_order_detail y on y.no_sales_order = x.no "
            sqlcom = sqlcom + "            where x.no = sales_order.no  "
            sqlcom = sqlcom + "            and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "') "
            sqlcom = sqlcom + "            group by x.no),0) "
            sqlcom = sqlcom + "       when daftar_customer.is_kawasan_berikat = 'T' then "
            sqlcom = sqlcom + "            isnull((select sum((y.harga_jual - y.harga_jual * y.discount /100) / 1.1  * x.rate * y.qty)     "
            sqlcom = sqlcom + "            from sales_order x  "
            sqlcom = sqlcom + "            inner join sales_order_detail y on y.no_sales_order = x.no "
            sqlcom = sqlcom + "            where x.no = sales_order.no  "
            sqlcom = sqlcom + "            and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "') "
            sqlcom = sqlcom + "            group by x.no),0) "
            sqlcom = sqlcom + "       end "     
            sqlcom = sqlcom + "  when sales_order.ppn = 10 then " 
            sqlcom = sqlcom + "     isnull((select sum((y.harga_jual - y.harga_jual * y.discount /100)  * x.rate * y.qty) "
            sqlcom = sqlcom + "     from sales_order x  "
            sqlcom = sqlcom + "     inner join sales_order_detail y on y.no_sales_order = x.no "
            sqlcom = sqlcom + "     where x.no = sales_order.no  "
            sqlcom = sqlcom + "     and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "') "
            sqlcom = sqlcom + "     group by x.no),0) "
            sqlcom = sqlcom + " end ,0) as grand_total "
            sqlcom = sqlcom + " from sales_order "
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer "
            sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no "
            sqlcom = sqlcom + " inner join product_item on product_item.id = sales_order_detail.id_product "
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion "
            sqlcom = sqlcom + " where (sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "') "
            sqlcom = sqlcom + " group by so_no_text, sales_order.no, sales_order.ppn, daftar_customer.is_kawasan_berikat, sales_order.tanggal "
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    end sub

    sub grand_ppn()
        Try
            sqlcom = "delete temp_grand_ppn_sos"
            connection.koneksi.DeleteRecord(sqlcom)

            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            sqlcom = "insert into temp_grand_ppn_sos(grand_ppn) "
            sqlcom = sqlcom + " select "  
            sqlcom = sqlcom + " round( "
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when sales_order.ppn = 0 then " 
            sqlcom = sqlcom + "       case "
            sqlcom = sqlcom + "          when daftar_customer.is_kawasan_berikat = 'Y' then 0 "
            sqlcom = sqlcom + "          when daftar_customer.is_kawasan_berikat = 'T' then "
            sqlcom = sqlcom + "               (select sum((y.harga_jual - y.harga_jual * y.discount /100) /1.1 * 0.1 * y.qty  * x.rate) "
            sqlcom = sqlcom + "                from sales_order x  "
            sqlcom = sqlcom + "                inner join sales_order_detail y on y.no_sales_order = sales_order.no "
            sqlcom = sqlcom + "                where x.no = sales_order.no  "
            sqlcom = sqlcom + "                and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "') "
            sqlcom = sqlcom + "                group by x.no) "
            sqlcom = sqlcom + "       end "
            sqlcom = sqlcom + "    when sales_order.ppn = 10 then  "
            sqlcom = sqlcom + "       (select sum(((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  * x.rate) * 0.1) "
            sqlcom = sqlcom + "        from sales_order x  "
            sqlcom = sqlcom + "        inner join sales_order_detail y on y.no_sales_order = sales_order.no "
            sqlcom = sqlcom + "        where x.no = sales_order.no  "
            sqlcom = sqlcom + "        and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "') "
            sqlcom = sqlcom + "        group by x.no) "
            sqlcom = sqlcom + " end ,0) as total_ppn "
            sqlcom = sqlcom + " from sales_order "
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer "
            sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no "
            sqlcom = sqlcom + " inner join product_item on product_item.id = sales_order_detail.id_product "
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion "
            sqlcom = sqlcom + " where (sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "') "
            sqlcom = sqlcom + " group by so_no_text, sales_order.no, sales_order.ppn, daftar_customer.is_kawasan_berikat, sales_order.tanggal "
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    end sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tgl_awal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
            Me.tb_tgl_akhir.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub
End Class
