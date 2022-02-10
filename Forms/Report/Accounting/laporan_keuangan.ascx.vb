Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_Accounting_laporan_keuangan
    Inherits System.Web.UI.UserControl

    Dim tradingClass As tradingClass = New tradingClass()

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bind_periode_transaksi()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
        sqlcom = sqlcom + " order by bulan"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_1_bulan.DataSource = reader
        Me.dd_1_bulan.DataTextField = "name"
        Me.dd_1_bulan.DataValueField = "id"
        Me.dd_1_bulan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clear_adjustment(ByVal month As String)
        Try
            sqlcom = Nothing

            sqlcom = "select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update inventory_stock_barang"
                sqlcom = sqlcom + " set qty_adjustment = 0,"
                sqlcom = sqlcom + " amount_adjustment = 0"
                sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
                sqlcom = sqlcom + " and id_produk = " & reader.Item("id_produk").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_purchase(ByVal year As String, ByVal month As String)
        Try
            sqlcom = Nothing

            sqlcom = "select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update inventory_stock_barang"
                sqlcom = sqlcom + " set qty_purchase = ("
                sqlcom = sqlcom + " select "
                sqlcom = sqlcom + " isnull(sum(isnull(hitungan_hpp.qty_stock,0)),0)"
                sqlcom = sqlcom + " from hitungan_hpp"
                sqlcom = sqlcom + " inner join product_item on product_item.id = hitungan_hpp.id_produk"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq = hitungan_hpp.seq"
                sqlcom = sqlcom + " and hitungan_hpp.rupiah_text = 'x'"
                sqlcom = sqlcom + " where month(entry_dokumen_impor.tanggal_bayar_pib) = (select bulan from transaction_period where id = " & Val(month) & ")"
                sqlcom = sqlcom + " and year(entry_dokumen_impor.tanggal_bayar_pib) = " & year
                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " group by product_item.id"
                sqlcom = sqlcom + "),"
                sqlcom = sqlcom + " amount_purchase = ("
                sqlcom = sqlcom + " select isnull(sum(isnull(hitungan_hpp.sub_total,0)),0)"
                sqlcom = sqlcom + " from hitungan_hpp"
                sqlcom = sqlcom + " inner join product_item on product_item.id = hitungan_hpp.id_produk"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq = hitungan_hpp.seq"
                sqlcom = sqlcom + " and hitungan_hpp.rupiah_text = 'x'"
                sqlcom = sqlcom + " where month(entry_dokumen_impor.tanggal_bayar_pib) = (select bulan from transaction_period where id = " & Val(month) & ")"
                sqlcom = sqlcom + " and year(entry_dokumen_impor.tanggal_bayar_pib) = " & year

                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + ")"
                sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
                sqlcom = sqlcom + " and id_produk = " & reader.Item("id_produk").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            'connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_purchase_local(ByVal year As String, ByVal month As String)
        Try
            sqlcom = Nothing

            sqlcom = "select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update inventory_stock_barang"
                sqlcom = sqlcom + " set qty_purchase = isnull(qty_purchase,0) + (select isnull(sum(isnull(purchase_order_local_detil.qty,0)),0) from purchase_order_local_detil inner join purchase_order_local on purchase_order_local_detil.po_no = purchase_order_local.no inner join product_item on purchase_order_local_detil.id_product = product_item.id"
                sqlcom = sqlcom + " where month(purchase_order_local.tanggal) = (select bulan from transaction_period where id = " & Val(month) & ")"
                sqlcom = sqlcom + " and year(purchase_order_local.tanggal) = " & year
                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + "),"
                sqlcom = sqlcom + " amount_purchase = isnull(amount_purchase,0) + (select isnull(sum(isnull(purchase_order_local_detil.qty,0) * isnull(purchase_order_local_detil.unit_price,0)),0) from purchase_order_local_detil inner join purchase_order_local on purchase_order_local_detil.po_no = purchase_order_local.no inner join product_item on purchase_order_local_detil.id_product = product_item.id"
                sqlcom = sqlcom + " where month(purchase_order_local.tanggal) = (select bulan from transaction_period where id = " & Val(month) & ")"
                sqlcom = sqlcom + " and year(purchase_order_local.tanggal) = " & year
                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + ")"
                sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
                sqlcom = sqlcom + " and id_produk = " & reader.Item("id_produk").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            'connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_sales(ByVal year As String, ByVal month As String)
        Try
            sqlcom = Nothing

            sqlcom = "select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update inventory_stock_barang"
                sqlcom = sqlcom + " set qty_sales = isnull(("
                sqlcom = sqlcom + " select "
                sqlcom = sqlcom + " isnull(sum(isnull(qty,0)),0)"
                sqlcom = sqlcom + " from sales_order_detail "
                sqlcom = sqlcom + " inner join sales_order on sales_order.no = sales_order_detail.no_sales_order"
                sqlcom = sqlcom + " where sales_order_detail.id_product = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " and sales_order.id_transaction_period = " & Val(month)
                sqlcom = sqlcom + " group by sales_order_detail.id_product"
                sqlcom = sqlcom + "),0),"
                sqlcom = sqlcom + " amount_sales = "
                sqlcom = sqlcom + " round(isnull((select sum(round((sales_order_detail.harga_jual - (sales_order_detail.harga_jual *  "
                sqlcom = sqlcom + " sales_order_detail.discount /100)) * sales_order_detail.qty /1.1 * sales_order.rate,0)) "
                sqlcom = sqlcom + " from sales_order   "
                sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no   "
                sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer   "
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(month)
                sqlcom = sqlcom + " and daftar_customer.is_kawasan_berikat = 'T'  "
                sqlcom = sqlcom + " and sales_order.id_currency = 'IDR'  "
                sqlcom = sqlcom + " and sales_order_detail.id_product =  " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " and sales_order.ppn = 0  "
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product),0)  "
                sqlcom = sqlcom + " +  "
                sqlcom = sqlcom + " isnull((select sum(round((sales_order_detail.harga_jual - (sales_order_detail.harga_jual *  "
                sqlcom = sqlcom + " sales_order_detail.discount /100)) * sales_order_detail.qty * sales_order.rate, 0))  "
                sqlcom = sqlcom + " from sales_order   "
                sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no   "
                sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer   "
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(month)
                sqlcom = sqlcom + " and daftar_customer.is_kawasan_berikat = 'Y'  "
                sqlcom = sqlcom + " and sales_order.id_currency = 'IDR'  "
                sqlcom = sqlcom + " and sales_order_detail.id_product =  " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " and sales_order.ppn = 0   "
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product),0)  "
                sqlcom = sqlcom + " + "
                sqlcom = sqlcom + " isnull((select sum(round((sales_order_detail.harga_jual - (sales_order_detail.harga_jual *  "
                sqlcom = sqlcom + " sales_order_detail.discount /100)) * sales_order_detail.qty /1.1 * sales_order.rate, 3))   "
                sqlcom = sqlcom + " from sales_order   "
                sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no   "
                sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer   "
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(month)
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product   "
                sqlcom = sqlcom + " and daftar_customer.is_kawasan_berikat = 'T'  "
                sqlcom = sqlcom + " and sales_order.id_currency = 'USD'  "
                sqlcom = sqlcom + " and sales_order_detail.id_product =  " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " and sales_order.ppn = 0   "
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product),0)   "
                sqlcom = sqlcom + " + "
                sqlcom = sqlcom + " isnull((select sum(round((sales_order_detail.harga_jual - (sales_order_detail.harga_jual *  "
                sqlcom = sqlcom + " sales_order_detail.discount /100)) * sales_order_detail.qty * sales_order.rate, 3))   "
                sqlcom = sqlcom + " from sales_order   "
                sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no   "
                sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer   "
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(month)
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product   "
                sqlcom = sqlcom + " and daftar_customer.is_kawasan_berikat = 'Y'  "
                sqlcom = sqlcom + " and sales_order.id_currency = 'USD'  "
                sqlcom = sqlcom + " and sales_order_detail.id_product = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " and sales_order.ppn = 0   "
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product),0)  "
                sqlcom = sqlcom + " + "
                sqlcom = sqlcom + " isnull((select sum(round((sales_order_detail.harga_jual - (sales_order_detail.harga_jual *  "
                sqlcom = sqlcom + " sales_order_detail.discount /100)) * sales_order_detail.qty * sales_order.rate, 0))   "
                sqlcom = sqlcom + " from sales_order   "
                sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no   "
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(month)
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product   "
                sqlcom = sqlcom + " and sales_order.id_currency = 'IDR'  "
                sqlcom = sqlcom + " and sales_order_detail.id_product =  " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " and sales_order.ppn = 10   "
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product),0)  "
                sqlcom = sqlcom + " + "
                sqlcom = sqlcom + " isnull((select sum(round((sales_order_detail.harga_jual - (sales_order_detail.harga_jual *  "
                sqlcom = sqlcom + " sales_order_detail.discount /100)) * sales_order_detail.qty * sales_order.rate,0)) "
                sqlcom = sqlcom + " from sales_order   "
                sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no   "
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(month)
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product   "
                sqlcom = sqlcom + " and sales_order.id_currency = 'USD'  "
                sqlcom = sqlcom + " and sales_order_detail.id_product =  " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " and sales_order.ppn = 10   "
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product),0),0) "
                sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
                sqlcom = sqlcom + " and id_produk = " & reader.Item("id_produk").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            'connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_retur_sales(ByVal year As String, ByVal month As String)
        Try
            sqlcom = Nothing

            sqlcom = "select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update inventory_stock_barang"
                sqlcom = sqlcom + " set qty_adjustment = (select isnull(sum(isnull(retur_sales_order_detil.qty,0)),0) from retur_sales_order_detil inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so inner join product_item on retur_sales_order_detil.id_produk = product_item.id"
                sqlcom = sqlcom + " where month(retur_sales_order.tanggal) = (select bulan from transaction_period where id = " & month & ")"
                sqlcom = sqlcom + " and year(retur_sales_order.tanggal) = " & year
                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + "),"
                sqlcom = sqlcom + " amount_adjustment = (select isnull(sum(isnull(retur_sales_order_detil.qty,0) * isnull(sales_order_detail.harga_jual,0)),0) from retur_sales_order_detil inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so inner join product_item on retur_sales_order_detil.id_produk = product_item.id inner join sales_order_detail on sales_order_detail.no_sales_order = retur_sales_order.no_sales_order"
                sqlcom = sqlcom + " where month(retur_sales_order.tanggal) = (select bulan from transaction_period where id = " & month & ")"
                sqlcom = sqlcom + " and year(retur_sales_order.tanggal) = " & year
                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + ")"
                sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
                sqlcom = sqlcom + " and id_produk = " & reader.Item("id_produk").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            'connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_adjustment(ByVal month As String)
        Try
            sqlcom = Nothing

            sqlcom = "update inventory_stock_barang set inventory_stock_barang.qty_adjustment = isnull(inventory_stock_barang.qty_adjustment, 0) + isnull(-mutasi_stock.qty, 0) from inventory_stock_barang inner join mutasi_stock on inventory_stock_barang.id_produk = mutasi_stock.id_product_from and inventory_stock_barang.id_transaction_period = mutasi_stock.id_transaction_period where inventory_stock_barang.id_transaction_period = " & Val(month)
            connection.koneksi.UpdateRecord(sqlcom)

            sqlcom = "update inventory_stock_barang set inventory_stock_barang.qty_adjustment = isnull(inventory_stock_barang.qty_adjustment, 0) + isnull(mutasi_stock.qty, 0) from inventory_stock_barang inner join mutasi_stock on inventory_stock_barang.id_produk = mutasi_stock.id_product_to and inventory_stock_barang.id_transaction_period = mutasi_stock.id_transaction_period where inventory_stock_barang.id_transaction_period = " & Val(month)
            connection.koneksi.UpdateRecord(sqlcom)

            sqlcom = "update inventory_stock_barang set amount_adjustment = isnull(qty_adjustment, 0) * isnull(harga_stock, 0) where id_transaction_period = " & Val(month)
            connection.koneksi.UpdateRecord(sqlcom)

            sqlcom = "update isb2 set isb2.amount_adjustment = isnull(isb2.qty_adjustment, 0) * isnull(isb1.harga_stock, 0) from inventory_stock_barang isb1 inner join mutasi_stock ms on isb1.id_produk = ms.id_product_from inner join inventory_stock_barang isb2 on isb2.id_produk = ms.id_product_to where isb1.id_transaction_period = " & Val(month) & " and ms.id_transaction_period = " & Val(month) & " and isb2.id_transaction_period = " & Val(month)
            connection.koneksi.UpdateRecord(sqlcom)

        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_saldo_akhir(ByVal month As String)
        Try
            sqlcom = Nothing

            sqlcom = "update inventory_stock_barang"
            sqlcom = sqlcom + " set qty_akhir = isnull(qty_stock,0) + isnull(qty_purchase,0) + isnull(qty_adjustment,0) - isnull(qty_sales,0),"
            sqlcom = sqlcom + " harga_akhir = "
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when isnull(qty_purchase,0) = 0 then isnull(harga_stock,0)"
            sqlcom = sqlcom + " when isnull(qty_purchase,0) <> 0 then (isnull(amount_stock,0) + isnull(amount_purchase,0)) / nullif((isnull(qty_stock,0) + isnull(qty_purchase,0)),0)"
            sqlcom = sqlcom + " end"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_amount_akhir(ByVal month As String)
        Try
            sqlcom = Nothing

            sqlcom = "update inventory_stock_barang"
            sqlcom = sqlcom + " set amount_akhir = isnull(qty_akhir,0) * isnull(harga_akhir,0)"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(month)
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub calculateStock(ByVal id_period As String, ByVal year As String)
        Me.clear_adjustment(id_period)
        Me.update_purchase(year, id_period)
        Me.update_purchase_local(year, id_period)
        Me.update_sales(year, id_period)
        Me.update_retur_sales(year, id_period)
        Me.update_adjustment(id_period)
        Me.update_saldo_akhir(id_period)
        Me.update_amount_akhir(id_period)
    End Sub

    Sub calculateStockThread(ByVal Parameters As Object)
        calculateStock(Parameters(0), Parameters(1))
    End Sub

    Sub calculateProfitLoss(ByVal id_period As String)
        Dim temp As String = Nothing
        Me.tradingClass.DeleteProfitLoss(id_period)

        Me.tradingClass.InsertProfitLoss(id_period)

        Me.tradingClass.UpdateProfitLoss(id_period, "penjualan_lokal", Me.tradingClass.CreditDebitParentNoBalance(id_period, "51"))
        Me.tradingClass.UpdateProfitLoss(id_period, "biaya_penjualan_lokal", Me.tradingClass.DebitCreditParentNoBalance(id_period, "61"))

        temp = Val(Me.tradingClass.ProfitLossValue(id_period, "penjualan_lokal")) - Val(Me.tradingClass.ProfitLossValue(id_period, "biaya_penjualan_lokal"))
        Me.tradingClass.UpdateProfitLoss(id_period, "penjualan_bersih", temp)
        temp = Nothing

        Me.tradingClass.UpdateProfitLoss(id_period, "pembelian", Me.tradingClass.DebitCreditParentNoBalance(id_period, "4"))
        'Me.tradingClass.UpdateProfitLoss(id_period, "hpp", Me.tradingClass.PurchasingInventory(id_period))

        temp = Val(Me.tradingClass.ProfitLossValue(id_period, "penjualan_bersih")) - Val(Me.tradingClass.ProfitLossValue(id_period, "pembelian"))
        Me.tradingClass.UpdateProfitLoss(id_period, "laba_kotor", temp)
        temp = Nothing

        Me.tradingClass.UpdateProfitLoss(id_period, "biaya_bunga", Me.tradingClass.DebitCreditParentNoBalance(id_period, "64"))
        Me.tradingClass.UpdateProfitLoss(id_period, "biaya_expedisi", Me.tradingClass.DebitCreditParentNoBalance(id_period, "43.04"))
        Me.tradingClass.UpdateProfitLoss(id_period, "biaya_umum_dan_administrasi", Me.tradingClass.DebitCreditParentNoBalance(id_period, "63"))

        temp = Val(Me.tradingClass.ProfitLossValue(id_period, "biaya_bunga")) + Val(Me.tradingClass.ProfitLossValue(id_period, "biaya_expedisi")) + Val(Me.tradingClass.ProfitLossValue(id_period, "biaya_umum_dan_administrasi"))
        Me.tradingClass.UpdateProfitLoss(id_period, "total_biaya", temp)
        temp = Nothing

        temp = Val(Me.tradingClass.ProfitLossValue(id_period, "laba_kotor")) - Val(Me.tradingClass.ProfitLossValue(id_period, "total_biaya"))
        Me.tradingClass.UpdateProfitLoss(id_period, "laba_kotor_dikurangi_biaya", temp)
        temp = Nothing

        Me.tradingClass.UpdateProfitLoss(id_period, "biaya_dan_pendapatan_lain", Me.tradingClass.CreditDebitParentNoBalance(id_period, "82"))
        Me.tradingClass.UpdateProfitLoss(id_period, "pendapatan_lain_lain", Me.tradingClass.CreditDebitParentNoBalance(id_period, "71"))

        temp = Val(Me.tradingClass.ProfitLossValue(id_period, "laba_kotor_dikurangi_biaya")) + Val(Me.tradingClass.ProfitLossValue(id_period, "biaya_dan_pendapatan_lain")) + Val(Me.tradingClass.ProfitLossValue(id_period, "pendapatan_lain_lain"))
        Me.tradingClass.UpdateProfitLoss(id_period, "laba_bersih_sebelum_pajak", temp)
        temp = Nothing

    End Sub

    Sub calculateBalanceSheet(ByVal id_period As String)
        Dim temp As String = Nothing

        Me.tradingClass.DeleteBalanceSheet(id_period)

        Me.tradingClass.InsertBalanceSheet(id_period)

        Me.tradingClass.UpdateBalanceSheet(id_period, "kas", Me.tradingClass.DebitCreditParentBalance(id_period, "11.01"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "bank", Me.tradingClass.DebitCreditParentBalance(id_period, "11.02"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "piutang_dagang", Me.tradingClass.DebitCreditParentBalance(id_period, "11.03.02"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "piutang_karyawan", Me.tradingClass.DebitCreditParentBalance(id_period, "11.03.04"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "piutang_giro_mundur", Me.tradingClass.DebitCreditParentBalance(id_period, "11.03.01"))
        'Me.tradingClass.UpdateBalanceSheet(id_period, "piutang_lain_lain", Me.tradingClass.DebitCreditParentBalance(id_period, "11.03.05"))

        'Me.tradingClass.UpdateBalanceSheet(id_period, "persediaan_barang", Me.tradingClass.StockInventory(id_period))

        'Me.tradingClass.UpdateBalanceSheet(id_period, "persediaan_barang_dalam_perjalanan", Me.tradingClass.DebitCreditNoParentBalance(id_period, "11.05"))
        'Me.tradingClass.UpdateBalanceSheet(id_period, "uang_muka_dibayar", Me.tradingClass.DebitCreditNoParentBalance(id_period, "11.08"))

        Me.tradingClass.UpdateBalanceSheet(id_period, "biaya_dibayar_dimuka", Me.tradingClass.DebitCreditParentBalance(id_period, "11.06"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "pajak_dibayar_dimuka", Me.tradingClass.DebitCreditParentBalance(id_period, "11.07"))

        temp = Val(Me.tradingClass.BalanceSheetValue(id_period, "kas")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "bank")) + +Val(Me.tradingClass.BalanceSheetValue(id_period, "piutang_dagang")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "piutang_karyawan")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "piutang_giro_mundur")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "biaya_dibayar_dimuka")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "pajak_dibayar_dimuka"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "total_aktiva", temp)
        temp = Nothing

        Me.tradingClass.UpdateBalanceSheet(id_period, "tanah_gudang", Me.tradingClass.DebitCreditNoParentBalance(id_period, "12.01.01"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "bangunan_gudang", Me.tradingClass.DebitCreditNoParentBalance(id_period, "12.01.02"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "tanah_kantor", Me.tradingClass.DebitCreditNoParentBalance(id_period, "12.01.04"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "bangunan_kantor", Me.tradingClass.DebitCreditNoParentBalance(id_period, "12.01.05"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "inventaris_kantor_gudang", Me.tradingClass.DebitCreditNoParentBalance(id_period, "12.01.09"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "kendaraan", Me.tradingClass.DebitCreditNoParentBalance(id_period, "12.01.07"))

        temp = Val(Me.tradingClass.BalanceSheetValue(id_period, "tanah_gudang")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "bangunan_gudang")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "tanah_kantor")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "bangunan_kantor")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "inventaris_kantor_gudang")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "kendaraan"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "total_aktiva_tetap_sblm_penyusutan", temp)
        temp = Nothing

        temp = Val(Me.tradingClass.DebitCreditNoParentBalance(id_period, "12.01.08")) + Val(Me.tradingClass.DebitCreditNoParentBalance(id_period, "12.01.10"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "penyusutan", temp)
        temp = Nothing

        temp = Val(Me.tradingClass.BalanceSheetValue(id_period, "total_aktiva_tetap_sblm_penyusutan")) - Val(Me.tradingClass.BalanceSheetValue(id_period, "penyusutan"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "total_aktiva_tetap", temp)
        temp = Nothing

        temp = Val(Me.tradingClass.BalanceSheetValue(id_period, "total_aktiva")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "total_aktiva_tetap"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "grand_total_aktiva", temp)
        temp = Nothing

        Me.tradingClass.UpdateBalanceSheet(id_period, "hutang_bank", Me.tradingClass.CreditDebitParentBalance(id_period, "21.01"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "hutang_pajak", Me.tradingClass.CreditDebitParentBalance(id_period, "21.04"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "hutang_dagang_luar_negeri", Me.tradingClass.CreditDebitParentBalance(id_period, "21.02.02"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "hutang_tr", Me.tradingClass.CreditDebitParentBalance(id_period, "21.02.01"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "hutang_lain_lain", Me.tradingClass.CreditDebitParentBalance(id_period, "21.03"))

        temp = Val(Me.tradingClass.BalanceSheetValue(id_period, "hutang_bank")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "hutang_pajak")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "hutang_dagang_luar_negeri")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "hutang_tr")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "hutang_lain_lain"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "total_hutang_lancar", temp)
        temp = Nothing

        Me.tradingClass.UpdateBalanceSheet(id_period, "modal_disetor", Me.tradingClass.CreditDebitNoParentBalance(id_period, "31"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "laba_bersih_tahun_berjalan", Me.tradingClass.ProfitLossValue(id_period, "laba_bersih_sebelum_pajak"))

        temp = Val(Me.tradingClass.BalanceSheetValue(id_period, "modal_disetor")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "laba_bersih_tahun_berjalan"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "total_modal", temp)
        temp = Nothing

        temp = Val(Me.tradingClass.BalanceSheetValue(id_period, "total_hutang_lancar")) + Val(Me.tradingClass.BalanceSheetValue(id_period, "total_modal"))
        Me.tradingClass.UpdateBalanceSheet(id_period, "grand_total_pasiva", temp)
        temp = Nothing

    End Sub

    Sub neraca()
        Try
            Me.lbl_msg.Text = Nothing

            Dim x As Integer
            Dim y As Integer

            'Dim thread = New System.Threading.Thread(New Threading.ThreadStart(AddressOf t_all_1_bulan))
            'thread.IsBackground = True

            Dim reportPath As String = Nothing
            Dim file_pdf As String = Nothing

            reportPath = Server.MapPath("reports\lap_neraca.rpt")
            file_pdf = "Pdf_files/lap_neraca.pdf"

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

            If Me.RadioButton1bulan.Checked = True Then

                Dim thread = New Threading.Thread(AddressOf calculateStockThread)
                Dim Parameters = New Object() {Me.dd_1_bulan.SelectedValue, Me.tb_tahun.Text}
                thread.IsBackground = True
                'thread.start(Parameters)

                Me.calculateProfitLoss(Me.dd_1_bulan.SelectedValue)
                Me.calculateBalanceSheet(Me.dd_1_bulan.SelectedValue)

                oRD.SetParameterValue("id_periode1", Me.dd_1_bulan.SelectedValue)
                oRD.SetParameterValue("id_periode2", Me.dd_1_bulan.SelectedValue)

            ElseIf Me.RadioButton3bulan.Checked = True Then

                If Me.dd_3_bulan.SelectedIndex = 0 Then
                    x = 1
                    y = 3
                ElseIf Me.dd_3_bulan.SelectedIndex = 1 Then
                    x = 4
                    y = 6
                ElseIf Me.dd_3_bulan.SelectedIndex = 2 Then
                    x = 7
                    y = 9
                ElseIf Me.dd_3_bulan.SelectedIndex = 3 Then
                    x = 10
                    y = 12
                End If

                If Me.tradingClass.IDPeriodChecking(Me.tb_tahun.Text.Trim, x, y) = True Then

                    For index As Integer = x To y
                        Dim thread = New Threading.Thread(AddressOf calculateStockThread)
                        Dim Parameters = New Object() {Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, index), Me.tb_tahun.Text}
                        thread.IsBackground = True
                    Next

                    oRD.SetParameterValue("id_periode1", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, x))
                    oRD.SetParameterValue("id_periode2", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, y))
                Else
                    Me.tradingClass.Alert("Periode transaksi belum ada!", Me.Page)
                    Exit Sub
                End If

            ElseIf Me.RadioButton6bulan.Checked = True Then

                If Me.dd_6_bulan.SelectedIndex = 0 Then
                    x = 1
                    y = 6
                ElseIf Me.dd_6_bulan.SelectedIndex = 1 Then
                    x = 7
                    y = 12
                End If

                If Me.tradingClass.IDPeriodChecking(Me.tb_tahun.Text.Trim, x, y) = True Then

                    For index As Integer = x To y
                        Dim thread = New Threading.Thread(AddressOf calculateStockThread)
                        Dim Parameters = New Object() {Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, index), Me.tb_tahun.Text}
                        thread.IsBackground = True
                    Next

                    oRD.SetParameterValue("id_periode1", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, x))
                    oRD.SetParameterValue("id_periode2", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, y))
                Else
                    Me.tradingClass.Alert("Periode transaksi belum ada!", Me.Page)
                    Exit Sub
                End If

            ElseIf Me.RadioButton12bulan.Checked = True Then

                x = 1
                y = 12

                If Me.tradingClass.IDPeriodChecking(Me.tb_tahun.Text.Trim, x, y) = True Then

                    For index As Integer = x To y
                        Dim thread = New Threading.Thread(AddressOf calculateStockThread)
                        Dim Parameters = New Object() {Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, index), Me.tb_tahun.Text}
                        thread.IsBackground = True
                    Next

                    oRD.SetParameterValue("id_periode1", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, x))
                    oRD.SetParameterValue("id_periode2", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, y))
                Else
                    Me.tradingClass.Alert("Periode transaksi belum ada!", Me.Page)
                    Exit Sub
                End If

            End If

            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(file_pdf))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('" & file_pdf & "', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
            '    End If

        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub rugi_laba()
        Try
            Me.lbl_msg.Text = Nothing

            Dim x As Integer
            Dim y As Integer

            'Dim thread = New System.Threading.Thread(New Threading.ThreadStart(AddressOf t_all_1_bulan()))
            'thread.IsBackground = True

            Dim reportPath As String = Nothing
            Dim file_pdf As String = Nothing

            reportPath = (Server.MapPath("reports\lap_rugi_laba.rpt"))
            file_pdf = "Pdf_files/lap_rugi_laba.pdf"

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

            If Me.RadioButton1bulan.Checked = True Then

                Dim thread = New Threading.Thread(AddressOf calculateStockThread)
                Dim Parameters = New Object() {Me.dd_1_bulan.SelectedValue, Me.tb_tahun.Text}
                thread.IsBackground = True
                'thread.start(Parameters)

                Me.calculateProfitLoss(Me.dd_1_bulan.SelectedValue)

                oRD.SetParameterValue("id_periode1", Me.dd_1_bulan.SelectedValue)
                oRD.SetParameterValue("id_periode2", Me.dd_1_bulan.SelectedValue)

            ElseIf Me.RadioButton3bulan.Checked = True Then

                If Me.dd_3_bulan.SelectedIndex = 0 Then
                    x = 1
                    y = 3
                ElseIf Me.dd_3_bulan.SelectedIndex = 1 Then
                    x = 4
                    y = 6
                ElseIf Me.dd_3_bulan.SelectedIndex = 2 Then
                    x = 7
                    y = 9
                ElseIf Me.dd_3_bulan.SelectedIndex = 3 Then
                    x = 10
                    y = 12
                End If

                If Me.tradingClass.IDPeriodChecking(Me.tb_tahun.Text.Trim, x, y) = True Then

                    For index As Integer = x To y
                        Dim thread = New Threading.Thread(AddressOf calculateStockThread)
                        Dim Parameters = New Object() {Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, index), Me.tb_tahun.Text}
                        thread.IsBackground = True

                        Me.calculateProfitLoss(Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, index))
                    Next

                    oRD.SetParameterValue("id_periode1", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, x))
                    oRD.SetParameterValue("id_periode2", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, y))
                Else
                    Me.tradingClass.Alert("Periode transaksi belum ada!", Me.Page)
                    Exit Sub
                End If

            ElseIf Me.RadioButton6bulan.Checked = True Then

                If Me.dd_6_bulan.SelectedIndex = 0 Then
                    x = 1
                    y = 6
                ElseIf Me.dd_6_bulan.SelectedIndex = 1 Then
                    x = 7
                    y = 12
                End If

                If Me.tradingClass.IDPeriodChecking(Me.tb_tahun.Text.Trim, x, y) = True Then

                    For index As Integer = x To y
                        Dim thread = New Threading.Thread(AddressOf calculateStockThread)
                        Dim Parameters = New Object() {Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, index), Me.tb_tahun.Text}
                        thread.IsBackground = True

                        Me.calculateProfitLoss(Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, index))
                    Next

                    oRD.SetParameterValue("id_periode1", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, x))
                    oRD.SetParameterValue("id_periode2", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, y))
                Else
                    Me.tradingClass.Alert("Periode transaksi belum ada!", Me.Page)
                    Exit Sub
                End If

            ElseIf Me.RadioButton12bulan.Checked = True Then

                x = 1
                y = 12

                If Me.tradingClass.IDPeriodChecking(Me.tb_tahun.Text.Trim, x, y) = True Then

                    For index As Integer = x To y
                        Dim thread = New Threading.Thread(AddressOf calculateStockThread)
                        Dim Parameters = New Object() {Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, index), Me.tb_tahun.Text}
                        thread.IsBackground = True

                        Me.calculateProfitLoss(Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, index))
                    Next

                    oRD.SetParameterValue("id_periode1", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, x))
                    oRD.SetParameterValue("id_periode2", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, y))
                Else
                    Me.tradingClass.Alert("Periode transaksi belum ada!", Me.Page)
                    Exit Sub
                End If

            End If

            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
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
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub perincian_biaya_umum()
        Try
            Me.lbl_msg.Text = Nothing

            Dim x As Integer
            Dim y As Integer

            Dim reportPath As String = Nothing
            Dim file_pdf As String = Nothing

            reportPath = (Server.MapPath("reports\lap_perincian_biaya_umum.rpt"))
            file_pdf = "Pdf_files/lap_perincian_biaya_umum.pdf"

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

            If Me.RadioButton1bulan.Checked = True Then

                'oRD.SetParameterValue("id_periode", Me.dd_1_bulan.SelectedValue)
                oRD.SetParameterValue("id_periode1", Me.dd_1_bulan.SelectedValue)
                oRD.SetParameterValue("id_periode2", Me.dd_1_bulan.SelectedValue)

            ElseIf Me.RadioButton3bulan.Checked = True Then

                If Me.dd_3_bulan.SelectedIndex = 0 Then
                    x = 1
                    y = 3
                ElseIf Me.dd_3_bulan.SelectedIndex = 1 Then
                    x = 4
                    y = 6
                ElseIf Me.dd_3_bulan.SelectedIndex = 2 Then
                    x = 7
                    y = 9
                ElseIf Me.dd_3_bulan.SelectedIndex = 3 Then
                    x = 10
                    y = 12
                End If

                If Me.tradingClass.IDPeriodChecking(Me.tb_tahun.Text.Trim, x, y) = True Then
                    oRD.SetParameterValue("id_periode1", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, x))
                    oRD.SetParameterValue("id_periode2", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, y))
                Else
                    Me.tradingClass.Alert("Periode transaksi belum ada!", Me.Page)
                    Exit Sub
                End If

            ElseIf Me.RadioButton6bulan.Checked = True Then

                If Me.dd_6_bulan.SelectedIndex = 0 Then
                    x = 1
                    y = 6
                ElseIf Me.dd_6_bulan.SelectedIndex = 1 Then
                    x = 7
                    y = 12
                End If

                If Me.tradingClass.IDPeriodChecking(Me.tb_tahun.Text.Trim, x, y) = True Then
                    oRD.SetParameterValue("id_periode1", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, x))
                    oRD.SetParameterValue("id_periode2", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, y))
                Else
                    Me.tradingClass.Alert("Periode transaksi belum ada!", Me.Page)
                    Exit Sub
                End If

            ElseIf Me.RadioButton12bulan.Checked = True Then

                x = 1
                y = 12

                If Me.tradingClass.IDPeriodChecking(Me.tb_tahun.Text.Trim, x, y) = True Then
                    oRD.SetParameterValue("id_periode1", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, x))
                    oRD.SetParameterValue("id_periode2", Me.tradingClass.IDPeriodConverter(Me.tb_tahun.Text.Trim, y))
                Else
                    Me.tradingClass.Alert("Periode transaksi belum ada!", Me.Page)
                    Exit Sub
                End If

            End If

            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
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
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tahun.Text = Year(Now.Date)
            Me.bind_periode_transaksi()
        End If
    End Sub

    Protected Sub btn_refresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        Me.bind_periode_transaksi()
    End Sub

    Protected Sub RadioButtonNeraca_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonNeraca.CheckedChanged
        If Me.RadioButtonNeraca.Checked = True Then
            Me.RadioButton1bulan.Enabled = True
            Me.RadioButton1bulan.Checked = False
            Me.RadioButton3bulan.Enabled = False
            Me.RadioButton3bulan.Checked = False
            Me.RadioButton6bulan.Enabled = False
            Me.RadioButton6bulan.Checked = False
            Me.RadioButton12bulan.Enabled = False
            Me.RadioButton12bulan.Checked = False
        End If
    End Sub

    Protected Sub RadioButtonRugiLaba_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonRugiLaba.CheckedChanged
        If Me.RadioButtonRugiLaba.Checked = True Then
            Me.RadioButton1bulan.Enabled = True
            Me.RadioButton1bulan.Checked = False
            Me.RadioButton3bulan.Enabled = True
            Me.RadioButton3bulan.Checked = False
            Me.RadioButton6bulan.Enabled = True
            Me.RadioButton6bulan.Checked = False
            Me.RadioButton12bulan.Enabled = True
            Me.RadioButton12bulan.Checked = False
        End If
    End Sub

    Protected Sub RadioButtonPerincianBiayaUmum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonPerincianBiayaUmum.CheckedChanged
        If Me.RadioButtonPerincianBiayaUmum.Checked = True Then
            Me.RadioButton1bulan.Enabled = True
            Me.RadioButton1bulan.Checked = False
            Me.RadioButton3bulan.Enabled = True
            Me.RadioButton3bulan.Checked = False
            Me.RadioButton6bulan.Enabled = True
            Me.RadioButton6bulan.Checked = False
            Me.RadioButton12bulan.Enabled = True
            Me.RadioButton12bulan.Checked = False
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click

        If Me.RadioButtonNeraca.Checked = True And Me.RadioButtonRugiLaba.Checked = False And Me.RadioButtonPerincianBiayaUmum.Checked = False Then
            Me.neraca()
        ElseIf Me.RadioButtonNeraca.Checked = False And Me.RadioButtonRugiLaba.Checked = True And Me.RadioButtonPerincianBiayaUmum.Checked = False Then
            Me.rugi_laba()
        ElseIf Me.RadioButtonNeraca.Checked = False And Me.RadioButtonRugiLaba.Checked = False And Me.RadioButtonPerincianBiayaUmum.Checked = True Then
            Me.perincian_biaya_umum()
        End If

    End Sub
End Class