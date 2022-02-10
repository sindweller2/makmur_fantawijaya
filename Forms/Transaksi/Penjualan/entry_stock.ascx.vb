Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Sales_entry_stock
    Inherits System.Web.UI.UserControl

    Public Property vpilih_semua() As Integer
        Get
            Dim o As Object = ViewState("vpilih_semua")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vpilih_semua") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode_transaksi()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
        'sqlcom = sqlcom + " and bulan = 1"
        sqlcom = sqlcom + " order by bulan"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_bulan.DataSource = reader
        Me.dd_bulan.DataTextField = "name"
        Me.dd_bulan.DataValueField = "id"
        Me.dd_bulan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub
   

    Sub checkdata()
        Try
            sqlcom = "select *"
            sqlcom = sqlcom + " from stock_barang_gudang"
            sqlcom = sqlcom + " inner join product_item on product_item.id = stock_barang_gudang.id_product"
            sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where stock_barang_gudang.id_transaction_period = " & Me.dd_bulan.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tbl_search.visible = True
            Else
                Me.tbl_search.visible = False
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try
            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select stock_barang_gudang.id_transaction_period, stock_barang_gudang.id_product,"
            sqlcom = sqlcom + " isnull(stock_barang_gudang.qty_stock,0) as qty_stock, product_item.nama_beli,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + satuan_packaging_produk.name + '/' + satuan_produk.name as packaging"
            sqlcom = sqlcom + " from stock_barang_gudang"
            sqlcom = sqlcom + " inner join product_item on product_item.id = stock_barang_gudang.id_product"
            sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where stock_barang_gudang.id_transaction_period = " & Me.dd_bulan.SelectedValue

            if not string.isnullorempty(me.tb_search.text) then
               sqlcom = sqlcom + " and upper(product_item.nama_beli) like upper('%" & me.tb_search.text & "%')"
            end if

            sqlcom = sqlcom + " order by product_item.nama_beli"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "stock_barang_gudang")
                Me.dg_data.DataSource = ds.Tables("stock_barang_gudang").DefaultView

                If ds.Tables("stock_barang_gudang").Rows.Count > 0 Then
                    If ds.Tables("stock_barang_gudang").Rows.Count > 10 Then
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
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
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
            Me.vpilih_semua = 0
            Me.tb_tahun.Text = Now.Year
            Me.bindperiode_transaksi()
            Me.loadgrid()
        End If
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

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update stock_barang_gudang"
                    sqlcom = sqlcom + " set qty_stock = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text)
                    sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
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

    Protected Sub btn_select_all_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_select_all.Click
        If Me.vpilih_semua = 0 Then
            Me.vpilih_semua = 1
            Me.btn_select_all.Text = "Batal semua"
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True
            Next
        Else
            Me.vpilih_semua = 0
            Me.btn_select_all.Text = "Pilih semua"
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = False
            Next
        End If

    End Sub

End Class
