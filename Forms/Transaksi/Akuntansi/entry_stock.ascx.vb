Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Akuntansi_entry_stock
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

    Sub clearproduk()
        Me.tb_id_produk.Text = 0
        Me.lbl_nama_produk.Text = "------"
        Me.link_popup_produk.Visible = True
        Me.tb_qty.Text = ""
        Me.tb_harga.Text = ""
    End Sub

    Sub bindproduk()
        Try
            sqlcom = "select product_item.nama_beli,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + satuan_packaging_produk.name + '/' + satuan_produk.name as packaging"
            sqlcom = sqlcom + " from product_item"
            sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where product_item.id = " & Me.tb_id_produk.Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_nama_produk.Text = reader.Item("nama_beli").ToString
                Me.lbl_nama_packaging.Text = reader.Item("packaging").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try

            Me.clearproduk()
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select inventory_stock_barang.id_transaction_period, inventory_stock_barang.id_produk,"
            sqlcom = sqlcom + " isnull(inventory_stock_barang.qty_stock,0) as qty_stock, isnull(inventory_stock_barang.harga_stock,0) as harga_stock,"
            sqlcom = sqlcom + " isnull(inventory_stock_barang.amount_stock,0) as amount_stock,"
            sqlcom = sqlcom + " product_item.nama_beli,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + satuan_packaging_produk.name + '/' + satuan_produk.name as packaging"
            sqlcom = sqlcom + " from inventory_stock_barang"
            sqlcom = sqlcom + " inner join product_item on product_item.id = inventory_stock_barang.id_produk"
            sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where inventory_stock_barang.id_transaction_period = " & Me.dd_bulan.SelectedValue

            if not string.isnullorempty(me.tb_search.text) then
               sqlcom = sqlcom + " and upper(product_item.nama_beli) like upper('%" & me.tb_search.text & "%')"
            end if

            sqlcom = sqlcom + " order by product_item.nama_beli"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "inventory_stock_barang")
                Me.dg_data.DataSource = ds.Tables("inventory_stock_barang").DefaultView

                If ds.Tables("inventory_stock_barang").Rows.Count > 0 Then
                    If ds.Tables("inventory_stock_barang").Rows.Count > 10 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 10
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_update.Visible = True
                    Me.btn_delete.Visible = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
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
            Me.clearproduk()
            Me.tb_tahun.Text = Now.Year
            Me.bindperiode_transaksi()
            Me.loadgrid()
            Me.tb_id_produk.Attributes.Add("style", "display: none;")
            Me.link_refresh_produk.Attributes.Add("style", "display: none;")
            Me.link_popup_produk.Attributes.Add("onclick", "popup_produk_item('" & Me.tb_id_produk.ClientID & "','" & Me.link_refresh_produk.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    Protected Sub link_refresh_produk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_produk.Click
        Me.bindproduk()
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_produk.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama produk terlebih dahulu"
            Else
                sqlcom = "insert into inventory_stock_barang(id_transaction_period, id_produk, qty_stock, harga_stock, amount_stock)"
                sqlcom = sqlcom + " values(" & Me.dd_bulan.SelectedValue & "," & Me.tb_id_produk.Text
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_qty.Text) & "," & Decimal.ToDouble(Me.tb_harga.Text)
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_qty.Text) * Decimal.ToDouble(Me.tb_harga.Text) & ")"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data tersebut sudah disimpan"
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete inventory_stock_barang"
                    sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
                    sqlcom = sqlcom + " and id_produk = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.lbl_msg.Text = "Data tersebut sudah dihapus"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.lbl_msg.Text = "Data tersebut masih digunakan di form lain"
            Else
                Me.lbl_msg.Text = ex.Message
            End If
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update inventory_stock_barang"
                    sqlcom = sqlcom + " set qty_stock = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) & ","
                    sqlcom = sqlcom + " harga_stock = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_harga"), TextBox).Text) & ","
                    'Daniel
                    sqlcom = sqlcom + " amount_stock = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) * Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_harga"), TextBox).Text)
                    'Daniel
                    sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
                    sqlcom = sqlcom + " and id_produk = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data tersebut sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub


    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        me.loadgrid()
    End Sub

    'Daniel - 26052016
    Protected Sub btn_mutasi_bulanan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_mutasi_bulanan.Click
        Response.Redirect("~/mutasi_bulanan.aspx")
    End Sub
    'Daniel - 26052016

End Class
