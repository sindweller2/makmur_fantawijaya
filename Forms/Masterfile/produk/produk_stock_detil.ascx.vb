Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_produk_produk_stock_detil
    Inherits System.Web.UI.UserControl

    Private ReadOnly Property vid_category() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_category")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vid_sub_category() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_sub_category")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vid_product() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_product")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindkategori()
        sqlcom = "select name from product_category"
        sqlcom = sqlcom + " where id = " & Me.vid_category
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_category_name.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindsubkategori()
        sqlcom = "select name from product_sub_category"
        sqlcom = sqlcom + " where id_sub_category = " & Me.vid_sub_category
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_subcategory_name.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindproduk()
        sqlcom = "select product_item.id, product_item.nama_beli, product_item.qty_conversion, satuan_produk.name as satuan_produk,"
        sqlcom = sqlcom + " satuan_packaging_produk.name as satuan_packaging_produk,"
        sqlcom = sqlcom + " case"
        sqlcom = sqlcom + " when product_item.status = '1' then 'Aktif'"
        sqlcom = sqlcom + " when product_item.status = '0' then 'Tidak aktif'"
        sqlcom = sqlcom + " end as status"
        sqlcom = sqlcom + " from product_item"
        sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
        sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
        sqlcom = sqlcom + " where product_item.id = " & Me.vid_product
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_name.Text = reader.Item("nama_beli").ToString
            Me.lbl_measurement.Text = reader.Item("satuan_produk").ToString
            Me.lbl_measurement_conversion.Text = reader.Item("satuan_packaging_produk").ToString
            Me.lbl_conversion_qty.Text = reader.Item("qty_conversion").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try

            Me.tb_lot_no.Text = ""
            Me.tb_qty.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select lot_no, qty"
            sqlcom = sqlcom + " from product_stock"
            sqlcom = sqlcom + " where id_product = " & Me.vid_product
            sqlcom = sqlcom + " order by lot_no"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "product_stock")
                Me.dg_data.DataSource = ds.Tables("product_stock").DefaultView

                If ds.Tables("product_stock").Rows.Count > 0 Then
                    If ds.Tables("product_stock").Rows.Count > 8 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 8
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_delete.Visible = True

                    Dim vtotal As Decimal = 0
                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        CType(Me.dg_data.Items(x).FindControl("lbl_conversion_qty_text"), Label).Text = FormatNumber(Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) * Decimal.ToDouble(Me.lbl_conversion_qty.Text), 2)
                        vtotal = vtotal + Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text)
                    Next
                    Me.lbl_total_stock.Text = FormatNumber(vtotal, 2)
                    Me.lbl_total_conversion_stock.Text = FormatNumber(vtotal * Decimal.ToDouble(Me.lbl_conversion_qty.Text), 2)
                Else
                    Me.dg_data.Visible = False
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
            Me.bindkategori()
            Me.bindsubkategori()
            Me.bindproduk()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Try
            If String.IsNullOrEmpty(Me.tb_lot_no.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor lot terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_qty.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi qty terlebih dahulu"
            Else
                sqlcom = "insert into product_stock(id_product, lot_no, qty)"
                sqlcom = sqlcom + " values(" & Me.vid_product & ",'" & Me.tb_lot_no.Text & "'," & Decimal.ToDouble(Me.tb_qty.Text) & ")"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/product_stock.aspx?vid_category=" & Me.vid_category & "&vid_sub_category=" & Me.vid_sub_category)
    End Sub
End Class
