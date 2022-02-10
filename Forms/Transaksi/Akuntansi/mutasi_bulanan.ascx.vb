Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Akuntansi_mutasi_bulanan
    Inherits System.Web.UI.UserControl

    Public tradingClass As New tradingClass()
    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode(ByVal DropDownList As DropDownList, ByVal Year As String)
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where tahun = " & Year
        sqlcom = sqlcom + " order by bulan"
        reader = connection.koneksi.SelectRecord(sqlcom)
        DropDownList.DataSource = reader
        DropDownList.DataTextField = "name"
        DropDownList.DataValueField = "id"
        DropDownList.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub delete_data()
        Try
            sqlcom = "delete inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan_to.SelectedValue)
            connection.koneksi.DeleteRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message + "delete"
        End Try
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

    Sub insert_data()
        Try
            sqlcom = "insert into inventory_stock_barang(id_produk, qty_stock, harga_stock, amount_stock, id_transaction_period)"
            sqlcom = sqlcom + " select id_produk, qty_akhir, harga_akhir, amount_akhir," & Val(Me.dd_bulan_to.SelectedValue)
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan_from.SelectedValue)
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message + "insert_new"
        End Try
    End Sub

    Sub insert_missing_stock()
        Try
            sqlcom = "insert into inventory_stock_barang (id_produk,id_transaction_period)"
            sqlcom = sqlcom + " select product_item.id, '" & Val(Me.dd_bulan_to.SelectedValue) & "'"
            sqlcom = sqlcom + " from product_item, product_stock"
            sqlcom = sqlcom + " where product_item.id = product_stock.id_product"
            sqlcom = sqlcom + " and product_stock.id_transaction_period = '" & Val(Me.dd_bulan_to.SelectedValue) & "'"
            sqlcom = sqlcom + " and product_item.id not in"
            sqlcom = sqlcom + " (select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = '" & Val(Me.dd_bulan_to.SelectedValue) & "')"
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message + "insert_new"
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tahun_from.Text = Year(Now.Date)
            Me.bindperiode(Me.dd_bulan_from, Me.tb_tahun_from.Text)
            Me.tb_tahun_to.Text = Year(Now.Date)
            Me.bindperiode(Me.dd_bulan_to, Me.tb_tahun_to.Text)
        End If
    End Sub

    Protected Sub btn_view_from_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view_from.Click
        Me.bindperiode(Me.dd_bulan_from, Me.tb_tahun_from.Text)
    End Sub

    Protected Sub btn_view_to_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view_to.Click
        Me.bindperiode(Me.dd_bulan_to, Me.tb_tahun_to.Text)
    End Sub

    Sub calculateStock(ByVal id_period As String, ByVal year As String)
        Me.delete_data()
        Me.clear_adjustment(id_period)
        Me.update_purchase(year, id_period)
        Me.update_purchase_local(year, id_period)
        Me.update_sales(year, id_period)
        Me.update_retur_sales(year, id_period)
        Me.update_adjustment(id_period)
        Me.update_saldo_akhir(id_period)
        Me.update_amount_akhir(id_period)
        Me.insert_data()
        Me.insert_missing_stock()
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Me.calculateStock(Me.dd_bulan_from.SelectedValue, Me.tb_tahun_from.Text)
        Me.tradingClass.Alert("Mutasi selesai!", Me.Page)
    End Sub
End Class
