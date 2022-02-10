Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_Collection_lap_piutang_customer
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub hitung_piutang()
        Try

            sqlcom = "delete from temp_piutang_customer"
            connection.koneksi.DeleteRecord(sqlcom)

            Dim vtgl_jatuh_tempo As String = Me.tb_tgl_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(6, 4)

            sqlcom = "insert into temp_piutang_customer(id_sales, id_customer, no_so_text, nama_customer,"
            sqlcom = sqlcom + " nama_sales, tgl_invoice, tgl_jatuh_tempo, nilai_idr, nilai_usd, nilai_ppn_usd)"

            sqlcom = sqlcom + " select sales_order.id_sales, sales_order.id_customer, sales_order.so_no_text, daftar_customer.name, user_list.nama_pegawai, sales_order.tanggal,"
            sqlcom = sqlcom + " sales_order.tgl_jatuh_tempo,"

            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then "
            sqlcom = sqlcom + "        case"
            sqlcom = sqlcom + "           when sales_order.ppn = 0 then          "
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no)"
            sqlcom = sqlcom + "           when sales_order.ppn = 10 then"
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no) +"
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no) * 0.1"
            sqlcom = sqlcom + "        end"
            sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then 0"
            sqlcom = sqlcom + " end "

            sqlcom = sqlcom + " - "

            sqlcom = sqlcom + " (select isnull(sum(isnull(nilai_pembayaran,0) +  isnull(potongan,0) - isnull(kelebihan,0)),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no"
            sqlcom = sqlcom + " and id_currency = 'IDR')"

            sqlcom = sqlcom + " - isnull((select isnull( case     when x.ppn = 0 then  isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)     when x.ppn = 10 then  (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +   ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1))  end, 0) as nilai_retur  "
            sqlcom = sqlcom + " from retur_sales_order_detil  inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so  inner join sales_order x on x.no = retur_sales_order.no_sales_order  inner join sales_order_detail on sales_order_detail.no_sales_order = x.no  and sales_order_detail.id_product = retur_sales_order_detil.id_produk "
            sqlcom = sqlcom + " where(retur_sales_order.no_sales_order = sales_order.no) "
            sqlcom = sqlcom + " group by x.ppn, x.rate),0) "

            sqlcom = sqlcom + " as total_nilai_idr, "

            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then 0 "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then  "
            sqlcom = sqlcom + "               (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                where no_sales_order = sales_order.no) "
            sqlcom = sqlcom + " end"

            sqlcom = sqlcom + " - "

            sqlcom = sqlcom + " (select isnull(sum((isnull(nilai_pembayaran,0) + isnull(potongan,0) - isnull(kelebihan,0))),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no"
            sqlcom = sqlcom + " and id_currency = 'USD') "

            sqlcom = sqlcom + " as total_nilai_usd,"

            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then 0 "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then  "
            sqlcom = sqlcom + "        case"
            sqlcom = sqlcom + "          when sales_order.ppn = 0 then 0"
            sqlcom = sqlcom + "          when sales_order.ppn = 10 then"
            sqlcom = sqlcom + "               round(isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                where no_sales_order = sales_order.no) * 0.1 * isnull(sales_order.rate,0),0),0)"
           sqlcom = sqlcom + "       end"
            sqlcom = sqlcom + " end as total_nilai_ppn_usd"

            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
            sqlcom = sqlcom + " where sales_order.tgl_jatuh_tempo <= '" & vtgl_jatuh_tempo & "'"
            sqlcom = sqlcom + " and sales_order.tanggal >= '01/01/2012'"
            sqlcom = sqlcom + " and sales_order.status_invoice = 'B'"
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tgl_jatuh_tempo.Text = Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Me.hitung_piutang()
        Me.print()
    End Sub

    Sub print()
        Try
            Dim reportPath As String = Server.MapPath("reports\lap_piutang_customer_keseluruhan.rpt")
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
            Dim vtgl_jatuh_tempo As String = Me.tb_tgl_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(6, 4)
            oRD.SetParameterValue("tgl_jatuh_tempo", vtgl_jatuh_tempo)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_piutang_customer_keseluruhan.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/lap_piutang_customer_keseluruhan.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

End Class
