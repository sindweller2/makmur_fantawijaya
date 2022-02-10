Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Gudang_produk_stock_list_temp
    Inherits System.Web.UI.UserControl

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

    Sub insert_produk()
        Try
            sqlcom = "insert into product_stock(id_product, id_transaction_period)"
            sqlcom = sqlcom + " select id, " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " from product_item"
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_stock_awal()
        Try
            sqlcom = "update product_stock"
            sqlcom = sqlcom + " set qty_stock = (select isnull(x.total_saldo,0) from product_stock x"
            sqlcom = sqlcom + " where x.id_product = product_stock.id_product and x.id_transaction_period = " & Me.dd_bulan.SelectedValue - 1 & ")"
            sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_stock_masuk()
        Try
            sqlcom = "select id_product, sum(isnull(qty,0)) as stock_masuk"
            sqlcom = sqlcom + " from barang_masuk"
            sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " group by id_product"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update product_stock"
                sqlcom = sqlcom + " set total_masuk = " & reader.Item("stock_masuk").ToString
                sqlcom = sqlcom + " where id_product = " & reader.Item("id_product").ToString
                sqlcom = sqlcom + " and id_transaction_period = " & Me.dd_bulan.SelectedValue
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_stock_keluar()
        Try
            sqlcom = "select id_product, sum(qty) as qty"
            sqlcom = sqlcom + " from sales_order_detail"
            sqlcom = sqlcom + " where sales_order_detail.no_sales_order in (select no from sales_order"
            sqlcom = sqlcom + " where sales_order.id_transaction_period = " & Me.dd_bulan.SelectedValue & ")"
            sqlcom = sqlcom + " group by id_product"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "update product_stock"
                sqlcom = sqlcom + " set total_keluar = " & reader.Item("qty").ToString
                sqlcom = sqlcom + " where id_product = " & reader.Item("id_product").ToString
                sqlcom = sqlcom + " and id_transaction_period = " & Me.dd_bulan.SelectedValue
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception

        End Try
    End Sub

    Sub update_stock_akhir()
        Try
            sqlcom = "update product_stock"
            sqlcom = sqlcom + " set total_saldo = isnull(qty_stock,0) + isnull(total_masuk,0) - isnull(total_keluar,0)"
            sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub checkdata()
        sqlcom = "select *"
        sqlcom = sqlcom + " from product_item"
        sqlcom = sqlcom + " inner join product_stock on product_stock.id_product = product_item.id"
        sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
        sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
        sqlcom = sqlcom + " where product_stock.id_transaction_period = " & Me.dd_bulan.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.update_stock_masuk()
            Me.update_stock_keluar()
            Me.update_stock_akhir()
            Me.tbl_search.Visible = True
        Else
            Me.insert_produk()
            Me.update_stock_awal()
            Me.update_stock_masuk()
            Me.update_stock_keluar()
            Me.update_stock_akhir()
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select product_item.id, product_item.nama_beli, isnull(product_stock.qty_stock,0) as qty_stock,"
            sqlcom = sqlcom + " isnull(product_stock.qty_stock,0) / isnull(product_item.qty_conversion,0) as total_stock_conversion,"
            sqlcom = sqlcom + " isnull(product_stock.total_masuk,0) as total_masuk,"
            sqlcom = sqlcom + " isnull(product_stock.total_masuk,0) / isnull(product_item.qty_conversion,0) as total_masuk_conversion,"
            sqlcom = sqlcom + " isnull(product_stock.total_keluar,0) as total_keluar,"
            sqlcom = sqlcom + " isnull(product_stock.total_keluar,0) / isnull(product_item.qty_conversion,0) as total_keluar_conversion,"
            sqlcom = sqlcom + " isnull(product_stock.total_saldo,0) as total_saldo,"
            sqlcom = sqlcom + " isnull(product_stock.total_saldo,0) / isnull(product_item.qty_conversion,0) as total_saldo_conversion,"
            sqlcom = sqlcom + " satuan_produk.name as satuan_produk, satuan_packaging_produk.name as satuan_packaging,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when product_item.is_packaging = 'P' then satuan_produk.name"
            sqlcom = sqlcom + " when product_item.is_packaging = 'Q' then satuan_packaging_produk.name"
            sqlcom = sqlcom + " end as satuan_produk,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when product_item.status = '1' then 'Aktif'"
            sqlcom = sqlcom + " when product_item.status = '0' then 'Tidak aktif'"
            sqlcom = sqlcom + " end as status,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + satuan_packaging_produk.name + '/' + satuan_produk.name as packaging,"
            sqlcom = sqlcom + " isnull(product_item.qty_conversion,0) as qty_conversion"
            sqlcom = sqlcom + " from product_stock"
            sqlcom = sqlcom + " inner join product_item on product_item.id = product_stock.id_product"
            sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where product_stock.id_transaction_period = " & Me.dd_bulan.SelectedValue

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(product_item.nama_beli) like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " order by nama_beli"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "product_item")
                Me.dg_data.DataSource = ds.Tables("product_item").DefaultView

                If ds.Tables("product_item").Rows.Count > 0 Then
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                Else
                    Me.dg_data.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Me.tb_tahun.Text = Year(Now.Date)
            Me.tb_tahun.Text = 2011
            Me.bindperiode_transaksi()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.loadgrid()
    End Sub
End Class
