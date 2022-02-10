Imports System.Configuration
Imports System.Data


Partial Class Forms_Report_Accounting_laporan_penerimaan_piutang
    Inherits System.Web.UI.UserControl

    Public tradingClass As tradingClass = New tradingClass()

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tgl.Text = "01" & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
        End If
    End Sub

    'Sub hitung_piutang()
    '    Try

    '        sqlcom = "delete temp_penerimaan_piutang_customer"
    '        connection.koneksi.DeleteRecord(sqlcom)

    '        Dim vtgl_jatuh_tempo As String = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)

    '        sqlcom = "insert into temp_penerimaan_piutang_customer(id_sales, id_customer, no_so_text, nama_customer,"
    '        sqlcom = sqlcom + "nama_sales, tgl_invoice, tgl_jatuh_tempo, nilai_idr, nilai_usd, nilai_ppn_usd, kurs)"
    '        sqlcom = sqlcom + " select sales_order.id_sales, sales_order.id_customer, sales_order.so_no_text, daftar_customer.name, user_list.nama_pegawai, sales_order.tanggal,"
    '        sqlcom = sqlcom + " sales_order.tgl_jatuh_tempo,"
    '        sqlcom = sqlcom + " case "
    '        sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then "
    '        sqlcom = sqlcom + "        case"
    '        sqlcom = sqlcom + "           when sales_order.ppn = 0 then          "
    '        sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
    '        sqlcom = sqlcom + "                 where no_sales_order = sales_order.no)"
    '        sqlcom = sqlcom + "           when sales_order.ppn = 10 then"
    '        sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
    '        sqlcom = sqlcom + "                 where no_sales_order = sales_order.no) +"
    '        sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
    '        sqlcom = sqlcom + "                 where no_sales_order = sales_order.no) * 0.1"
    '        sqlcom = sqlcom + "        end"
    '        sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then 0"
    '        sqlcom = sqlcom + " end as total_nilai_idr, "
    '        sqlcom = sqlcom + " case "
    '        sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then 0 "
    '        sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then  "
    '        sqlcom = sqlcom + "               (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
    '        sqlcom = sqlcom + "                where no_sales_order = sales_order.no) "
    '        sqlcom = sqlcom + " end as total_nilai_usd,"
    '        sqlcom = sqlcom + " case "
    '        sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then 0 "
    '        sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then  "
    '        sqlcom = sqlcom + "        case"
    '        sqlcom = sqlcom + "          when sales_order.ppn = 0 then 0"
    '        sqlcom = sqlcom + "          when sales_order.ppn = 10 then"
    '        sqlcom = sqlcom + "               round(isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
    '        sqlcom = sqlcom + "                where no_sales_order = sales_order.no) / 1.1 * 0.1 * isnull(sales_order.rate,0),0),0)"
    '        sqlcom = sqlcom + "       end"
    '        sqlcom = sqlcom + " end as total_nilai_pajak_usd, sales_order.rate"
    '        sqlcom = sqlcom + " from sales_order"
    '        sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
    '        sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
    '        sqlcom = sqlcom + " where sales_order.tgl_jatuh_tempo <= '" & vtgl_jatuh_tempo & "'"
    '        sqlcom = sqlcom + " and sales_order.tanggal >= '01/01/2012'"
    '        sqlcom = sqlcom + " and sales_order.status_invoice = 'B'"
    '        connection.koneksi.InsertRecord(sqlcom)
    '    Catch ex As Exception
    '        Me.lbl_msg.Text = ex.Message
    '    End Try
    'End Sub

    Sub hapus_piutang()
        sqlcom = Nothing
        sqlcom = "delete from temp_penerimaan_piutang_customer"
        connection.koneksi.DeleteRecord(sqlcom)
    End Sub

    Sub hitung_piutang_outstanding()
        Try

            sqlcom = Nothing

            Dim vtgl_awal As String = Me.tb_tgl.Text.Substring(3, 2) & "/" & Me.tb_tgl.Text.Substring(0, 2) & "/" & Me.tb_tgl.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl.Text.Substring(3, 2) & "/" & Me.tb_tgl.Text.Substring(0, 2) & "/" & Me.tb_tgl.Text.Substring(6, 4)

            sqlcom = "insert into temp_penerimaan_piutang_customer(no_so_text, id_sales, id_customer, nama_customer,"
            sqlcom = sqlcom + " nama_sales, tgl_invoice, tgl_jatuh_tempo, nilai_idr, nilai_usd, nilai_saldo_awal, nilai_invoice, nilai_bayar, nilai_retur)"

            sqlcom = sqlcom + " select distinct(sales_order.so_no_text), sales_order.id_sales, sales_order.id_customer, daftar_customer.name, user_list.nama_pegawai, sales_order.tanggal, sales_order.tgl_jatuh_tempo,"

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
            sqlcom = sqlcom + " end"

            sqlcom = sqlcom + " - "

            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when sales_order.id_currency = 'IDR' then "
            sqlcom = sqlcom + " (select isnull(sum(isnull(nilai_pembayaran,0) +  isnull(potongan,0) - isnull(kelebihan,0)),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no"
            sqlcom = sqlcom + " and id_currency = 'IDR' and pembayaran_invoice_penjualan.tanggal < '" & vtgl_awal & "')"
            sqlcom = sqlcom + " when sales_order.id_currency = 'USD' then "
            sqlcom = sqlcom + " (select isnull(sum(isnull(nilai_pembayaran,0) +  isnull(potongan,0) - isnull(kelebihan,0)),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no"
            sqlcom = sqlcom + " and id_currency = 'USD' and pembayaran_invoice_penjualan.tanggal < '" & vtgl_awal & "')"
            sqlcom = sqlcom + " end"

            sqlcom = sqlcom + " - "

            sqlcom = sqlcom + " isnull((select isnull( case     when x.ppn = 0 then  isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)     when x.ppn = 10 then  (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +   ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1))  end, 0) as nilai_retur"
            sqlcom = sqlcom + " from retur_sales_order_detil  inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so  inner join sales_order x on x.no = retur_sales_order.no_sales_order  inner join sales_order_detail on sales_order_detail.no_sales_order = x.no  and sales_order_detail.id_product = retur_sales_order_detil.id_produk"
            sqlcom = sqlcom + " where(retur_sales_order.no_sales_order = sales_order.no)"
            sqlcom = sqlcom + " group by x.ppn, x.rate),0)"

            sqlcom = sqlcom + " as total_nilai_idr,"

            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then 0"
            sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then"
            sqlcom = sqlcom + "               (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail"
            sqlcom = sqlcom + "                where no_sales_order = sales_order.no)"
            sqlcom = sqlcom + " end"

            sqlcom = sqlcom + " - "

            sqlcom = sqlcom + " (select isnull(sum((isnull(nilai_pembayaran,0) + isnull(potongan,0) - isnull(kelebihan,0))),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no"
            sqlcom = sqlcom + " and id_currency = 'USD' and pembayaran_invoice_penjualan.tanggal < '" & vtgl_awal & "')"

            sqlcom = sqlcom + " as total_nilai_usd,"

            sqlcom = sqlcom + " case when sales_order.tanggal < '" & vtgl_awal & "' then"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then"
            sqlcom = sqlcom + "        case"
            sqlcom = sqlcom + "           when sales_order.ppn = 0 then"
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail"
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no)"
            sqlcom = sqlcom + "           when sales_order.ppn = 10 then"
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail"
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no) +"
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail"
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no) * 0.1"
            sqlcom = sqlcom + "        end"
            sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then 0"
            sqlcom = sqlcom + " end else 0 end as nilai_saldo_awal,"

            sqlcom = sqlcom + " case when sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "' then"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then"
            sqlcom = sqlcom + "        case"
            sqlcom = sqlcom + "           when sales_order.ppn = 0 then          "
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail"
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no)"
            sqlcom = sqlcom + "           when sales_order.ppn = 10 then"
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail"
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no) +"
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail"
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no) * 0.1"
            sqlcom = sqlcom + "        end"
            sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then 0"
            sqlcom = sqlcom + " end else 0 end as nilai_invoice,"

            sqlcom = sqlcom + " (select isnull(sum(isnull(pembayaran_invoice_penjualan.nilai_pembayaran,0) +  isnull(pembayaran_invoice_penjualan.potongan,0) - isnull(pembayaran_invoice_penjualan.kelebihan,0)),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where pembayaran_invoice_penjualan.no_so = sales_order.no"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.id_currency = 'IDR' and pembayaran_invoice_penjualan.tanggal < '" & vtgl_awal & "') as nilai_bayar,"

            sqlcom = sqlcom + " isnull((select isnull( case     when x.ppn = 0 then  isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)     when x.ppn = 10 then  (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +   ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1))  end, 0) as nilai_retur"
            sqlcom = sqlcom + " from retur_sales_order_detil  inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so  inner join sales_order x on x.no = retur_sales_order.no_sales_order  inner join sales_order_detail on sales_order_detail.no_sales_order = x.no  and sales_order_detail.id_product = retur_sales_order_detil.id_produk"
            sqlcom = sqlcom + " where(retur_sales_order.no_sales_order = sales_order.no)"
            sqlcom = sqlcom + " group by x.ppn, x.rate),0) as nilai_retur"

            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
            sqlcom = sqlcom + " where (sales_order.tanggal >= '2012-01-01'"
            sqlcom = sqlcom + " and sales_order.tanggal < '" & vtgl_awal & "')"
            sqlcom = sqlcom + " and sales_order.status_invoice = 'B'"
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub hitung_penjualan()
        Try

            sqlcom = Nothing

            Dim vtgl_awal As String = Me.tb_tgl.Text.Substring(3, 2) & "/" & Me.tb_tgl.Text.Substring(0, 2) & "/" & Me.tb_tgl.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl.Text.Substring(3, 2) & "/" & Me.tb_tgl.Text.Substring(0, 2) & "/" & Me.tb_tgl.Text.Substring(6, 4)

            sqlcom = "insert into temp_penerimaan_piutang_customer(no_so_text, id_sales, id_customer, nama_customer, "
            sqlcom = sqlcom + " nama_sales, tgl_invoice, tgl_jatuh_tempo, nilai_idr, nilai_usd, nilai_saldo_awal, nilai_invoice, nilai_bayar, nilai_retur)"

            sqlcom = sqlcom + " select distinct(sales_order.so_no_text), sales_order.id_sales, sales_order.id_customer, daftar_customer.name, user_list.nama_pegawai, sales_order.tanggal, sales_order.tgl_jatuh_tempo,"

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
            sqlcom = sqlcom + " and id_currency = 'IDR' and pembayaran_invoice_penjualan.tanggal >= '" & vtgl_awal & "' and  pembayaran_invoice_penjualan.tanggal <= '" & vtgl_akhir & "')"

            sqlcom = sqlcom + " - "

            sqlcom = sqlcom + " isnull((select isnull( case     when x.ppn = 0 then  isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)     when x.ppn = 10 then  (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +   ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1))  end, 0) as nilai_retur  "
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
            sqlcom = sqlcom + " and id_currency = 'USD' and pembayaran_invoice_penjualan.tanggal >= '" & vtgl_awal & "' and  pembayaran_invoice_penjualan.tanggal <= '" & vtgl_akhir & "') "

            sqlcom = sqlcom + " as total_nilai_usd,"

            sqlcom = sqlcom + " case when sales_order.tanggal < '" & vtgl_awal & "' then "
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
            sqlcom = sqlcom + " end else 0 end as nilai_saldo_awal,"

            sqlcom = sqlcom + " case when sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "' then "
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
            sqlcom = sqlcom + " end else 0 end as nilai_invoice,"

            sqlcom = sqlcom + " (select isnull(sum(isnull(pembayaran_invoice_penjualan.nilai_pembayaran,0) +  isnull(pembayaran_invoice_penjualan.potongan,0) - isnull(pembayaran_invoice_penjualan.kelebihan,0)),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where pembayaran_invoice_penjualan.no_so = sales_order.no"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.id_currency = 'IDR' and pembayaran_invoice_penjualan.tanggal >= '" & vtgl_awal & "' and  pembayaran_invoice_penjualan.tanggal <= '" & vtgl_akhir & "') as nilai_bayar,"

            sqlcom = sqlcom + " isnull((select isnull( case     when x.ppn = 0 then  isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)     when x.ppn = 10 then  (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +   ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1))  end, 0) as nilai_retur  "
            sqlcom = sqlcom + " from retur_sales_order_detil  inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so  inner join sales_order x on x.no = retur_sales_order.no_sales_order  inner join sales_order_detail on sales_order_detail.no_sales_order = x.no  and sales_order_detail.id_product = retur_sales_order_detil.id_produk "
            sqlcom = sqlcom + " where(retur_sales_order.no_sales_order = sales_order.no) "
            sqlcom = sqlcom + " group by x.ppn, x.rate),0) as nilai_retur"

            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
            sqlcom = sqlcom + " where sales_order.tanggal >= '" & vtgl_awal & "'"
            sqlcom = sqlcom + " and sales_order.tanggal <= '" & vtgl_akhir & "'"
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub hitung_pembayaran()
        Try

            sqlcom = Nothing

            Dim vtgl_awal As String = Me.tb_tgl.Text.Substring(3, 2) & "/" & Me.tb_tgl.Text.Substring(0, 2) & "/" & Me.tb_tgl.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl.Text.Substring(3, 2) & "/" & Me.tb_tgl.Text.Substring(0, 2) & "/" & Me.tb_tgl.Text.Substring(6, 4)

            sqlcom = "insert into temp_penerimaan_piutang_customer(no_so_text, id_sales, id_customer, nama_customer, id_jenis_pembayaran, id_bank_account, no_cek_giro, tgl_cek_giro, tgl_jatuh_tempo_cek_giro,"
            sqlcom = sqlcom + " nama_sales, tgl_invoice, tgl_jatuh_tempo, nilai_idr, nilai_usd, nilai_saldo_awal, nilai_invoice, nilai_bayar, nilai_retur)"

            sqlcom = sqlcom + " select distinct(sales_order.so_no_text), sales_order.id_sales, sales_order.id_customer, daftar_customer.name, pembayaran_invoice_penjualan.id_jenis_pembayaran, pembayaran_invoice_penjualan.id_bank_account, pembayaran_invoice_penjualan.no_cek_giro, pembayaran_invoice_penjualan.tgl_cek_giro, pembayaran_invoice_penjualan.tgl_jatuh_tempo_cek_giro, user_list.nama_pegawai, sales_order.tanggal, sales_order.tgl_jatuh_tempo,"

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
            sqlcom = sqlcom + " and id_currency = 'IDR' and pembayaran_invoice_penjualan.tanggal >= '" & vtgl_awal & "' and  pembayaran_invoice_penjualan.tanggal <= '" & vtgl_akhir & "')"

            sqlcom = sqlcom + " - "

            sqlcom = sqlcom + " isnull((select isnull( case     when x.ppn = 0 then  isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)     when x.ppn = 10 then  (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +   ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1))  end, 0) as nilai_retur  "
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
            sqlcom = sqlcom + " and id_currency = 'USD' and pembayaran_invoice_penjualan.tanggal >= '" & vtgl_awal & "' and  pembayaran_invoice_penjualan.tanggal <= '" & vtgl_akhir & "') "

            sqlcom = sqlcom + " as total_nilai_usd,"

            sqlcom = sqlcom + " case when sales_order.tanggal < '" & vtgl_awal & "' then "
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
            sqlcom = sqlcom + " end else 0 end as nilai_saldo_awal,"

            sqlcom = sqlcom + " case when sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "' then "
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
            sqlcom = sqlcom + " end else 0 end as nilai_invoice,"

            sqlcom = sqlcom + " (select isnull(sum(isnull(pembayaran_invoice_penjualan.nilai_pembayaran,0) +  isnull(pembayaran_invoice_penjualan.potongan,0) - isnull(pembayaran_invoice_penjualan.kelebihan,0)),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where pembayaran_invoice_penjualan.no_so = sales_order.no"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.id_currency = 'IDR' and pembayaran_invoice_penjualan.tanggal >= '" & vtgl_awal & "' and  pembayaran_invoice_penjualan.tanggal <= '" & vtgl_akhir & "') as nilai_bayar,"

            sqlcom = sqlcom + " isnull((select isnull( case     when x.ppn = 0 then  isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)     when x.ppn = 10 then  (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +   ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1))  end, 0) as nilai_retur  "
            sqlcom = sqlcom + " from retur_sales_order_detil  inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so  inner join sales_order x on x.no = retur_sales_order.no_sales_order  inner join sales_order_detail on sales_order_detail.no_sales_order = x.no  and sales_order_detail.id_product = retur_sales_order_detil.id_produk "
            sqlcom = sqlcom + " where(retur_sales_order.no_sales_order = sales_order.no) "
            sqlcom = sqlcom + " group by x.ppn, x.rate),0) as nilai_retur"

            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
            sqlcom = sqlcom + " inner join pembayaran_invoice_penjualan on pembayaran_invoice_penjualan.no_so = sales_order.no"
            sqlcom = sqlcom + " where pembayaran_invoice_penjualan.tanggal >= '" & vtgl_awal & "'"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.tanggal <= '" & vtgl_akhir & "'"
            'sqlcom = sqlcom + " and sales_order.tanggal < '" & vtgl_awal & "'"
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub hitung_piutang_selesai()
        Try

            sqlcom = Nothing

            Dim vtgl_awal As String = Me.tb_tgl.Text.Substring(3, 2) & "/" & Me.tb_tgl.Text.Substring(0, 2) & "/" & Me.tb_tgl.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl.Text.Substring(3, 2) & "/" & Me.tb_tgl.Text.Substring(0, 2) & "/" & Me.tb_tgl.Text.Substring(6, 4)

            sqlcom = "insert into temp_penerimaan_piutang_customer(no_so_text, id_sales, id_customer, nama_customer, id_jenis_pembayaran, id_bank_account, no_cek_giro, tgl_cek_giro, tgl_jatuh_tempo_cek_giro,"
            sqlcom = sqlcom + " nama_sales, tgl_invoice, tgl_jatuh_tempo, nilai_idr, nilai_usd, nilai_saldo_awal, nilai_invoice, nilai_bayar, nilai_retur)"

            sqlcom = sqlcom + " select distinct(sales_order.so_no_text), sales_order.id_sales, sales_order.id_customer, daftar_customer.name, pembayaran_invoice_penjualan.id_jenis_pembayaran, pembayaran_invoice_penjualan.id_bank_account, pembayaran_invoice_penjualan.no_cek_giro, pembayaran_invoice_penjualan.tgl_cek_giro, pembayaran_invoice_penjualan.tgl_jatuh_tempo_cek_giro, user_list.nama_pegawai, sales_order.tanggal, sales_order.tgl_jatuh_tempo,"

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
            sqlcom = sqlcom + " and id_currency = 'IDR' and pembayaran_invoice_penjualan.tanggal >= '" & vtgl_awal & "' and  pembayaran_invoice_penjualan.tanggal <= '" & vtgl_akhir & "')"

            sqlcom = sqlcom + " - "

            sqlcom = sqlcom + " isnull((select isnull( case     when x.ppn = 0 then  isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)     when x.ppn = 10 then  (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +   ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1))  end, 0) as nilai_retur  "
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
            sqlcom = sqlcom + " and id_currency = 'USD' and pembayaran_invoice_penjualan.tanggal >= '" & vtgl_awal & "' and  pembayaran_invoice_penjualan.tanggal <= '" & vtgl_akhir & "') "

            sqlcom = sqlcom + " as total_nilai_usd,"

            sqlcom = sqlcom + " case when sales_order.tanggal < '" & vtgl_awal & "' then "
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
            sqlcom = sqlcom + " end else 0 end as nilai_saldo_awal,"

            sqlcom = sqlcom + " case when sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "' then "
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
            sqlcom = sqlcom + " end else 0 end as nilai_invoice,"

            sqlcom = sqlcom + " (select isnull(sum(isnull(pembayaran_invoice_penjualan.nilai_pembayaran,0) +  isnull(pembayaran_invoice_penjualan.potongan,0) - isnull(pembayaran_invoice_penjualan.kelebihan,0)),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where pembayaran_invoice_penjualan.no_so = sales_order.no"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.id_currency = 'IDR' and pembayaran_invoice_penjualan.tanggal >= '" & vtgl_awal & "' and  pembayaran_invoice_penjualan.tanggal <= '" & vtgl_akhir & "') as nilai_bayar,"

            sqlcom = sqlcom + " isnull((select isnull( case     when x.ppn = 0 then  isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)     when x.ppn = 10 then  (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +   ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) -  (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1))  end, 0) as nilai_retur  "
            sqlcom = sqlcom + " from retur_sales_order_detil  inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so  inner join sales_order x on x.no = retur_sales_order.no_sales_order  inner join sales_order_detail on sales_order_detail.no_sales_order = x.no  and sales_order_detail.id_product = retur_sales_order_detil.id_produk "
            sqlcom = sqlcom + " where(retur_sales_order.no_sales_order = sales_order.no) "
            sqlcom = sqlcom + " group by x.ppn, x.rate),0) as nilai_retur"

            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
            sqlcom = sqlcom + " inner join pembayaran_invoice_penjualan on pembayaran_invoice_penjualan.no_so = sales_order.no"
            sqlcom = sqlcom + " where sales_order.tanggal < '" & vtgl_awal & "'"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.tanggal > '" & vtgl_akhir & "'"
            sqlcom = sqlcom + " and sales_order.status_invoice = 'S'"
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub hapus_duplikat_data()
        Try
            sqlcom = Nothing
            sqlcom = sqlcom + " WITH CTE([no_so_text],[nama_customer],[nama_sales],[tgl_invoice],[tgl_jatuh_tempo],[nilai_idr],[nilai_usd],[id_customer],[id_sales],[kurs],[nilai_saldo_awal],[nilai_invoice],[nilai_bayar],[nilai_retur], id_jenis_pembayaran, id_bank_account, no_cek_giro, tgl_cek_giro, tgl_jatuh_tempo_cek_giro, duplicatecount)"
            sqlcom = sqlcom + " AS (SELECT [no_so_text],[nama_customer],[nama_sales],[tgl_invoice],[tgl_jatuh_tempo],[nilai_idr],[nilai_usd],[id_customer],[id_sales],[kurs],[nilai_saldo_awal],[nilai_invoice],[nilai_bayar],[nilai_retur], id_jenis_pembayaran, id_bank_account, no_cek_giro, tgl_cek_giro, tgl_jatuh_tempo_cek_giro, ROW_NUMBER() OVER(PARTITION BY [no_so_text],[nama_customer],[nama_sales],[tgl_invoice],[tgl_jatuh_tempo],[nilai_idr],[nilai_usd],[id_customer],[id_sales],[kurs],[nilai_saldo_awal],[nilai_invoice],[nilai_bayar],[nilai_retur], id_jenis_pembayaran, id_bank_account, no_cek_giro, tgl_cek_giro, tgl_jatuh_tempo_cek_giro ORDER BY [no_so_text]) AS DuplicateCount FROM temp_penerimaan_piutang_customer)"
            sqlcom = sqlcom + " DELETE FROM CTE WHERE DuplicateCount > 1"
            connection.koneksi.DeleteRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub hapus_data_kosong()
        Try
            sqlcom = Nothing
            sqlcom = sqlcom + " DELETE"
            sqlcom = sqlcom + " FROM temp_penerimaan_piutang_customer"
            sqlcom = sqlcom + " WHERE nilai_bayar = 0"
            connection.koneksi.DeleteRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Me.hapus_piutang()
            'Me.hitung_piutang_outstanding()
            Me.hitung_penjualan()
            Me.hitung_pembayaran()
            'Me.hitung_piutang_selesai()
            Me.hapus_duplikat_data()
            Me.hapus_data_kosong()
            Me.print()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub print()
        Try
            If Me.RadioButtonPDF.Checked = True And Me.RadioButtonExcel.Checked = False Then
                Dim reportPath As String = Nothing
                Dim file_pdf As String = Nothing

                reportPath = Server.MapPath("reports\lap_penerimaan_piutang_customer.rpt")
                file_pdf = "Pdf_files/lap_penerimaan_piutang_customer.pdf"

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

                Dim vtgl As Date = Me.tb_tgl.Text.Substring(3, 2) & "/" & Me.tb_tgl.Text.Substring(0, 2) & "/" & Me.tb_tgl.Text.Substring(6, 4)

                oRD.SetParameterValue("tgl", vtgl)

                oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
                oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
                oExO = oRD.ExportOptions
                oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(file_pdf))
                Dim vscript As String = ""
                vscript = "<script>" & vbCrLf
                vscript = vscript + "window.open('" & file_pdf & "', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
                vscript = vscript + "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
                connection.koneksi.CloseKoneksi()


            ElseIf Me.RadioButtonPDF.Checked = False And Me.RadioButtonExcel.Checked = True Then

                Dim DataTable As DataTable
                sqlcom = Nothing

                sqlcom = "select temp_penerimaan_piutang_customer.nama_customer as 'Pinjaman dari'"
                sqlcom += " ,temp_penerimaan_piutang_customer.no_so_text as 'No. Faktur'"
                sqlcom += " ,convert(char, temp_penerimaan_piutang_customer.tgl_invoice, 103) as 'Tanggal Faktur'"
                sqlcom += " ,bank_account.name as 'Bank / Rekening'"
                sqlcom += " ,temp_penerimaan_piutang_customer.no_cek_giro as 'No. Cheque / BG'"
                sqlcom += " ,convert(char, temp_penerimaan_piutang_customer.tgl_jatuh_tempo_cek_giro, 103) as 'JT'"
                sqlcom += " ,isnull(temp_penerimaan_piutang_customer.nilai_bayar,0) as 'Jumlah'"
                sqlcom += " from temp_penerimaan_piutang_customer"
                sqlcom += " left join jenis_pembayaran on temp_penerimaan_piutang_customer.id_jenis_pembayaran = jenis_pembayaran.id"
                sqlcom += " left join bank_account on temp_penerimaan_piutang_customer.id_bank_account = bank_account.id"
                sqlcom += " order by 1"

                DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                tradingClass.WriteExcel("~/Excel_files/lap_penerimaan_piutang_customer.xls", DataTable, 1)
                tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_penerimaan_piutang_customer.xls", "800", "600", "no", "yes")

                'If Me.RadioButtonDetail.Checked = True And Me.RadioButtonSummary.Checked = False Then
                '    Dim DataTable As DataTable
                '    sqlcom = Nothing

                '    sqlcom = "select distinct(no_so_text) as 'No. Invoice', nama_customer as 'Nama Customer', convert(char, tgl_invoice, 103) as 'Tgl. Invoice', convert(char, tgl_jatuh_tempo, 103) as 'Tgl. jatuh tempo',  isnull(nilai_saldo_awal,0) as 'Saldo Awal', isnull(nilai_invoice,0) as 'Penjualan', isnull(nilai_bayar,0) as 'Pembayaran', isnull(nilai_retur,0) as 'Retur', isnull(nilai_idr,0) as 'Nilai IDR', isnull(nilai_usd,0) as 'Nilai USD'"
                '    sqlcom += " from temp_penerimaan_piutang_customer"
                '    sqlcom += " order by nama_customer, no_so_text"

                '    DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                '    tradingClass.WriteExcel("~/Excel_files/lap_piutang_customer_by_payment_date.xls", DataTable, 1)
                '    tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_piutang_customer_by_payment_date.xls", "800", "600", "no", "yes")
                'ElseIf Me.RadioButtonDetail.Checked = False And Me.RadioButtonSummary.Checked = True Then
                '    Dim DataTable As DataTable
                '    sqlcom = Nothing

                '    sqlcom = "select nama_customer,sum(isnull(nilai_saldo_awal,0)) as 'Saldo Awal',sum(isnull(nilai_invoice,0)) as 'Penjualan',sum(isnull(nilai_bayar,0)) as 'Pembayaran',sum(isnull(nilai_retur,0)) as 'Retur',sum(isnull(nilai_idr,0)) as 'Nilai IDR',sum(isnull(nilai_usd,0)) as 'Nilai USD'"
                '    sqlcom += " from temp_penerimaan_piutang_customer"
                '    sqlcom += " group by nama_customer"
                '    sqlcom += " order by nama_customer"

                '    DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                '    tradingClass.WriteExcel("~/Excel_files/lap_piutang_customer_by_payment_date_summary.xls", DataTable, 1)
                '    tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_piutang_customer_by_payment_date_summary.xls", "800", "600", "no", "yes")
                'End If

            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub
End Class
