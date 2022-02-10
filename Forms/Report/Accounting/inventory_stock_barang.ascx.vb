Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_Accounting_inventory_stock_barang
    Inherits System.Web.UI.UserControl

    Public tradingClass As tradingClass = New tradingClass()

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode_transaksi()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
        sqlcom = sqlcom + " order by bulan"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_bulan.DataSource = reader
        Me.dd_bulan.DataTextField = "name"
        Me.dd_bulan.DataValueField = "id"
        Me.dd_bulan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tahun.Text = Year(Now.Date)
            Me.bindperiode_transaksi()
        End If
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Sub delete_data()
        Try
            sqlcom = Nothing

            sqlcom = "delete inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            connection.koneksi.DeleteRecord(sqlcom)
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub insert_data()
        Try
            sqlcom = Nothing

            sqlcom = "insert into inventory_stock_barang(id_produk, qty_stock, harga_stock, amount_stock, id_transaction_period, is_submit)"
            sqlcom = sqlcom + " select id_produk, qty_akhir, harga_akhir, amount_akhir," & Val(Me.dd_bulan.SelectedValue) & ",'B'"
            sqlcom = sqlcom + " from inventory_stock_barang"
            'Daniel
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            'Daniel
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clear_adjustment()
        Try
            sqlcom = Nothing

            sqlcom = "select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update inventory_stock_barang"
                sqlcom = sqlcom + " set qty_adjustment = 0,"
                sqlcom = sqlcom + " amount_adjustment = 0"
                sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                sqlcom = sqlcom + " and id_produk = " & reader.Item("id_produk").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub update_purchase()
        Try
            sqlcom = Nothing

            sqlcom = "select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update inventory_stock_barang"
                sqlcom = sqlcom + " set qty_purchase = ("
                sqlcom = sqlcom + " select "
                'sqlcom = sqlcom + " case"
                'sqlcom = sqlcom + "     when (product_item.id = 98 or product_item.id = 149) then "
                'sqlcom = sqlcom + "           isnull(sum(isnull(hitungan_hpp.qty_stock,0)),0) / 146 * 200"
                'sqlcom = sqlcom + "     when (product_item.id <> 98 or product_item.id <> 149) then "
                sqlcom = sqlcom + "           isnull(sum(isnull(hitungan_hpp.qty_stock,0)),0)"
                'sqlcom = sqlcom + " end"
                sqlcom = sqlcom + " from hitungan_hpp"
                sqlcom = sqlcom + " inner join product_item on product_item.id = hitungan_hpp.id_produk"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq = hitungan_hpp.seq"
                sqlcom = sqlcom + " and hitungan_hpp.rupiah_text = 'x'"

                'sqlcom = sqlcom + " and entry_dokumen_impor.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)

                sqlcom = sqlcom + " where month(entry_dokumen_impor.tanggal_bayar_pib) = (select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue & ")"
                sqlcom = sqlcom + " and year(entry_dokumen_impor.tanggal_bayar_pib) = " & Me.tb_tahun.Text

                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " group by product_item.id"
                sqlcom = sqlcom + "),"
                sqlcom = sqlcom + " amount_purchase = ("
                sqlcom = sqlcom + " select isnull(sum(isnull(hitungan_hpp.sub_total,0)),0)"
                sqlcom = sqlcom + " from hitungan_hpp"
                sqlcom = sqlcom + " inner join product_item on product_item.id = hitungan_hpp.id_produk"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq = hitungan_hpp.seq"
                sqlcom = sqlcom + " and hitungan_hpp.rupiah_text = 'x'"

                'sqlcom = sqlcom + " and entry_dokumen_impor.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)

                sqlcom = sqlcom + " where month(entry_dokumen_impor.tanggal_bayar_pib) = (select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue & ")"
                sqlcom = sqlcom + " and year(entry_dokumen_impor.tanggal_bayar_pib) = " & Me.tb_tahun.Text

                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + ")"
                sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                sqlcom = sqlcom + " and id_produk = " & reader.Item("id_produk").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub update_purchase_local()
        Try
            sqlcom = Nothing

            sqlcom = "select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update inventory_stock_barang"
                sqlcom = sqlcom + " set qty_purchase = isnull(qty_purchase,0) + (select isnull(sum(isnull(purchase_order_local_detil.qty,0)),0) from purchase_order_local_detil inner join purchase_order_local on purchase_order_local_detil.po_no = purchase_order_local.no inner join product_item on purchase_order_local_detil.id_product = product_item.id"
                sqlcom = sqlcom + " where month(purchase_order_local.tanggal) = (select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue & ")"
                sqlcom = sqlcom + " and year(purchase_order_local.tanggal) = " & Me.tb_tahun.Text
                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + "),"
                sqlcom = sqlcom + " amount_purchase = isnull(amount_purchase,0) + (select isnull(sum(isnull(purchase_order_local_detil.qty,0) * isnull(purchase_order_local_detil.unit_price,0)),0) from purchase_order_local_detil inner join purchase_order_local on purchase_order_local_detil.po_no = purchase_order_local.no inner join product_item on purchase_order_local_detil.id_product = product_item.id"
                sqlcom = sqlcom + " where month(purchase_order_local.tanggal) = (select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue & ")"
                sqlcom = sqlcom + " and year(purchase_order_local.tanggal) = " & Me.tb_tahun.Text
                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + ")"
                sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                sqlcom = sqlcom + " and id_produk = " & reader.Item("id_produk").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub update_sales()
        Try
            sqlcom = Nothing

            sqlcom = "select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update inventory_stock_barang"
                sqlcom = sqlcom + " set qty_sales = isnull(("
                sqlcom = sqlcom + " select "
                'sqlcom = sqlcom + " case"
                'sqlcom = sqlcom + "     when (sales_order_detail.id_product = 98 or sales_order_detail.id_product = 149) then "
                'sqlcom = sqlcom + "           isnull(sum(isnull(qty,0)),0) / 146 * 200"
                'sqlcom = sqlcom + "     when (sales_order_detail.id_product <> 98 or sales_order_detail.id_product <> 149) then "
                sqlcom = sqlcom + "           isnull(sum(isnull(qty,0)),0)"
                'sqlcom = sqlcom + " end"
                sqlcom = sqlcom + " from sales_order_detail "
                sqlcom = sqlcom + " inner join sales_order on sales_order.no = sales_order_detail.no_sales_order"
                sqlcom = sqlcom + " where sales_order_detail.id_product = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " and sales_order.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                sqlcom = sqlcom + " group by sales_order_detail.id_product"
                sqlcom = sqlcom + "),0),"
                sqlcom = sqlcom + " amount_sales = "
                sqlcom = sqlcom + " round(isnull((select sum(round((sales_order_detail.harga_jual - (sales_order_detail.harga_jual *  "
                sqlcom = sqlcom + " sales_order_detail.discount /100)) * sales_order_detail.qty /1.1 * sales_order.rate,0)) "
                sqlcom = sqlcom + " from sales_order   "
                sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no   "
                sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer   "
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(Me.dd_bulan.SelectedValue)
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
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(Me.dd_bulan.SelectedValue)
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
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(Me.dd_bulan.SelectedValue)
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
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(Me.dd_bulan.SelectedValue)
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
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(Me.dd_bulan.SelectedValue)
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
                sqlcom = sqlcom + " where id_transaction_period =  " & Val(Me.dd_bulan.SelectedValue)
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product   "
                sqlcom = sqlcom + " and sales_order.id_currency = 'USD'  "
                sqlcom = sqlcom + " and sales_order_detail.id_product =  " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + " and sales_order.ppn = 10   "
                sqlcom = sqlcom + " and sales_order_detail.id_product = sales_order_detail.id_product),0),0) "
                sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                sqlcom = sqlcom + " and id_produk = " & reader.Item("id_produk").ToString
                'me.lbl_msg.text = sqlcom
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub update_retur_sales()
        Try
            sqlcom = Nothing

            'sqlcom = "update inventory_stock_barang"
            'sqlcom = sqlcom + " set qty_sales = isnull(qty_sales,0) - 0,"
            'sqlcom = sqlcom + " amount_sales = isnull(amount_sales,0) - 0"
            'sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            'connection.koneksi.UpdateRecord(sqlcom)

            sqlcom = "select id_produk"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update inventory_stock_barang"
                sqlcom = sqlcom + " set qty_adjustment = (select isnull(sum(isnull(retur_sales_order_detil.qty,0)),0) from retur_sales_order_detil inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so inner join product_item on retur_sales_order_detil.id_produk = product_item.id"
                sqlcom = sqlcom + " where month(retur_sales_order.tanggal) = (select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue & ")"
                sqlcom = sqlcom + " and year(retur_sales_order.tanggal) = " & Me.tb_tahun.Text
                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + "),"
                sqlcom = sqlcom + " amount_adjustment = (select isnull(sum(isnull(retur_sales_order_detil.qty,0) * isnull(sales_order_detail.harga_jual,0)),0) from retur_sales_order_detil inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so inner join product_item on retur_sales_order_detil.id_produk = product_item.id inner join sales_order_detail on sales_order_detail.no_sales_order = retur_sales_order.no_sales_order"
                sqlcom = sqlcom + " where month(retur_sales_order.tanggal) = (select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue & ")"
                sqlcom = sqlcom + " and year(retur_sales_order.tanggal) = " & Me.tb_tahun.Text
                sqlcom = sqlcom + " and product_item.id = " & reader.Item("id_produk").ToString
                sqlcom = sqlcom + ")"
                sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                sqlcom = sqlcom + " and id_produk = " & reader.Item("id_produk").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub update_adjustment()
        Try

            sqlcom = Nothing

            'Daniel - 25052016
            'sqlcom = "update inventory_stock_barang"
            'sqlcom = sqlcom + " set qty_adjustment = 0,"
            'sqlcom = sqlcom + " amount_adjustment = 0"
            'sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            'connection.koneksi.UpdateRecord(sqlcom)

            'asal mutasi
            sqlcom = "update inventory_stock_barang set inventory_stock_barang.qty_adjustment = isnull(inventory_stock_barang.qty_adjustment, 0) + isnull(-mutasi_stock.qty, 0) from inventory_stock_barang inner join mutasi_stock on inventory_stock_barang.id_produk = mutasi_stock.id_product_from and inventory_stock_barang.id_transaction_period = mutasi_stock.id_transaction_period where inventory_stock_barang.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            connection.koneksi.UpdateRecord(sqlcom)

            'tujuan mutasi
            sqlcom = "update inventory_stock_barang set inventory_stock_barang.qty_adjustment = isnull(inventory_stock_barang.qty_adjustment, 0) + isnull(mutasi_stock.qty, 0) from inventory_stock_barang inner join mutasi_stock on inventory_stock_barang.id_produk = mutasi_stock.id_product_to and inventory_stock_barang.id_transaction_period = mutasi_stock.id_transaction_period where inventory_stock_barang.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            connection.koneksi.UpdateRecord(sqlcom)

            'harga adjustment
            sqlcom = "update inventory_stock_barang set amount_adjustment = isnull(qty_adjustment, 0) * isnull(harga_stock, 0) where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            connection.koneksi.UpdateRecord(sqlcom)

            'Daniel - 25052016

            'Daniel - 23/02/2017=====================================================================
            sqlcom = "update isb2 set isb2.amount_adjustment = isnull(isb2.qty_adjustment, 0) * isnull(isb1.harga_stock, 0) from inventory_stock_barang isb1 inner join mutasi_stock ms on isb1.id_produk = ms.id_product_from inner join inventory_stock_barang isb2 on isb2.id_produk = ms.id_product_to where isb1.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue) & " and ms.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue) & " and isb2.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            connection.koneksi.UpdateRecord(sqlcom)
            '=====================================================================Daniel - 23/02/2017

        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub update_saldo_akhir()
        Try
            sqlcom = Nothing

            sqlcom = "update inventory_stock_barang"

            'Daniel - 25052016
            'sqlcom = sqlcom + " set qty_akhir = isnull(qty_stock,0) + isnull(qty_purchase,0) - isnull(qty_sales,0) + isnull(qty_adjustment,0),"
            sqlcom = sqlcom + " set qty_akhir = isnull(qty_stock,0) + isnull(qty_purchase,0) + isnull(qty_adjustment,0) - isnull(qty_sales,0),"
            'Daniel - 25052016

            sqlcom = sqlcom + " harga_akhir = "
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when isnull(qty_purchase,0) = 0 then isnull(harga_stock,0)"
            sqlcom = sqlcom + " when isnull(qty_purchase,0) <> 0 then (isnull(amount_stock,0) + isnull(amount_purchase,0)) / nullif((isnull(qty_stock,0) + isnull(qty_purchase,0)),0)"
            sqlcom = sqlcom + " end"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub update_amount_akhir()
        Try
            sqlcom = Nothing

            sqlcom = "update inventory_stock_barang"
            sqlcom = sqlcom + " set amount_akhir = isnull(qty_akhir,0) * isnull(harga_akhir,0)"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub print()
        Try
            If Me.RadioButtonPDF.Checked = True And Me.RadioButtonExcel.Checked = False Then

                Dim reportPath As String = Server.MapPath("reports\lap_inventory_stock_barang.rpt")
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

                oRD.SetParameterValue("id_periode", Me.dd_bulan.SelectedValue)

                oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
                oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
                oExO = oRD.ExportOptions
                oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_inventory_stock_barang.pdf"))
                Dim vscript As String = ""
                vscript = "<script>" & vbCrLf
                vscript = vscript + "window.open('Pdf_files/lap_inventory_stock_barang.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
                vscript = vscript + "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
                connection.koneksi.CloseKoneksi()


            ElseIf Me.RadioButtonPDF.Checked = False And Me.RadioButtonExcel.Checked = True Then

                Dim DataTable As DataTable
                sqlcom = Nothing

                sqlcom = "select case when product_item.nama_beli + ' (' + product_item.nama_jual + ')' is not null then product_item.nama_beli + CHAR(13) + ' ( ' + product_item.nama_jual + ' )' else product_item.nama_beli end as 'Nama Barang',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.qty_stock,0) as 'Stok Awal Quantity',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.harga_stock,0) as 'Stok Awal Harga',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.amount_stock,0) as 'Stok Awal Total Harga',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.qty_purchase,0) as 'Pembelian Quantity',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.amount_purchase,0) as 'Pembelian Total Harga',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.qty_sales,0) as 'Sales Quantity',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.amount_sales,0) as 'Sales Total Harga',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.qty_adjustment,0) as 'Adjustment Quantity',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.amount_adjustment,0) as 'Adjustment Total Harga',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.qty_akhir,0) as 'Stok Akhir Quantity',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.harga_akhir,0) as 'Stok Akhir Harga',"
                sqlcom = sqlcom + " isnull(inventory_stock_barang.amount_akhir,0) as 'Stok Akhir Total Harga'"
                sqlcom = sqlcom + " from inventory_stock_barang"
                sqlcom = sqlcom + " inner join product_item on product_item.id = inventory_stock_barang.id_produk"
                sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                sqlcom = sqlcom + " order by 1"

                DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                tradingClass.WriteExcel("~/Excel_files/lap_inventory_stock_barang.xls", DataTable, 1)
                tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_inventory_stock_barang.xls", "800", "600", "no", "yes")

            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub calculateStock()
        Me.lbl_msg.Text = Nothing

        Me.clear_adjustment()
        Me.update_purchase()
        Me.update_purchase_local()
        Me.update_sales()
        Me.update_retur_sales()
        Me.update_adjustment()
        Me.update_saldo_akhir()
        Me.update_amount_akhir()
        'Me.lbl_msg.Text = "Proses perhitungan Invetory Stock Barang sudah selesai"
        'tradingClass.Alert("Proses perhitungan Invetory Stock Barang sudah selesai", Me.Page)
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            'Daniel
            'If Me.dd_bulan.SelectedValue > 5 Then
            '    sqlcom = "select * from inventory_stock_barang"
            '   sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue) - 1
            '   reader = connection.koneksi.SelectRecord(sqlcom)
            '   reader.Read()
            '   If Not reader.HasRows Then
            '      Me.lbl_msg.Text = "Periode bulan sebelumnya belum diproses"
            '      reader.Close()
            '      connection.koneksi.CloseKoneksi()
            '      exit sub
            '   else
            '      reader.Close()
            '      connection.koneksi.CloseKoneksi()
            'Me.delete_data()
            'Daniel
            'Me.insert_data()
            'Daniel
            'End If
            'reader.Close()
            'connection.koneksi.CloseKoneksi()
            'end if
            'Daniel


            'Dim thread = New System.Threading.Thread(New Threading.ThreadStart(AddressOf t_all))
            'thread.IsBackground = True
            Me.calculateStock()
            Me.print()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    'Protected Sub btn_process_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_process.Click
    '    Me.calculateStock()
    '    tradingClass.Alert("Proses perhitungan Invetory Stock Barang sudah selesai", Me.Page)
    'End Sub
End Class
