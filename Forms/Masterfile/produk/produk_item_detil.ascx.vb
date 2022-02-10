Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_produk_produk_item_detil
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

    Public Property id_product() As Integer
        Get
            Dim o As Object = ViewState("id_product")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_product") = value
        End Set
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
            Me.lbl_category.Text = reader.Item("name").ToString
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
            Me.lbl_sub_category.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindsatuan()
        sqlcom = "select id, name from measurement_unit order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_satuan.DataSource = reader
        Me.dd_satuan.DataTextField = "name"
        Me.dd_satuan.DataValueField = "id"
        Me.dd_satuan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindmerek()
        sqlcom = "select id, name from product_brand order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_merek.DataSource = reader
        Me.dd_merek.DataTextField = "name"
        Me.dd_merek.DataValueField = "id"
        Me.dd_merek.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindsatuan_packaging()
        sqlcom = "select id, name from measurement_unit order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_satuan_packaging.DataSource = reader
        Me.dd_satuan_packaging.DataTextField = "name"
        Me.dd_satuan_packaging.DataValueField = "id"
        Me.dd_satuan_packaging.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.tb_name.Text = ""
        Me.tb_min_qty.Text = ""
        Me.tb_packaging_qty.Text = ""
    End Sub

    Sub loaddata()
        Try
            If Me.vid_product <> 0 Then
                Me.id_product = Me.vid_product
            End If

            sqlcom = "select nama_beli, min_qty, id_measurement, id_brand, id_measurement_conversion, qty_conversion,"
            sqlcom = sqlcom + " is_packaging, status"
            sqlcom = sqlcom + " from product_item"
            sqlcom = sqlcom + " where id = " & Me.id_product
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_name.Text = reader.Item("nama_beli").ToString
                Me.tb_min_qty.Text = reader.Item("min_qty").ToString
                Me.dd_satuan.SelectedValue = reader.Item("id_measurement").ToString
                Me.dd_merek.SelectedValue = reader.Item("id_brand").ToString
                Me.dd_satuan_packaging.SelectedValue = reader.Item("id_measurement_conversion").ToString
                Me.tb_packaging_qty.Text = reader.Item("qty_conversion").ToString
                Me.dd_status.SelectedValue = reader.Item("status").ToString
                Me.dd_is_packaging.SelectedValue = reader.Item("is_packaging").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.bindkategori()
            Me.bindsubkategori()
            Me.bindsatuan()
            Me.bindmerek()
            Me.bindsatuan_packaging()
            Me.loaddata()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/product_item.aspx?vid_category=" & Me.vid_category & "&vid_sub_category=" & Me.vid_sub_category)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama produk terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_min_qty.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi qty minimum terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_packaging_qty.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi qty packaging terlebih dahulu"
            Else
                If Me.id_product = 0 Then
                    Dim vmax As Integer = 0
                    sqlcom = "select isnull(max(id),0) + 1 as vmax from product_item"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = reader.Item("vmax").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into product_item(id, nama_beli, id_measurement, min_qty, id_measurement_conversion, qty_conversion, "
                    sqlcom = sqlcom + " id_category, id_sub_category, id_brand, is_packaging, status)"
                    sqlcom = sqlcom + " values(" & vmax & ",'" & Me.tb_name.Text & "'," & Me.dd_satuan.SelectedValue & "," & Me.tb_min_qty.Text
                    sqlcom = sqlcom + "," & Me.dd_satuan_packaging.SelectedValue & "," & Me.tb_packaging_qty.Text & "," & Me.vid_category
                    sqlcom = sqlcom + "," & Me.vid_sub_category & "," & Me.dd_merek.SelectedValue & ",'" & Me.dd_is_packaging.SelectedValue & "','" & Me.dd_status.SelectedValue & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.id_product = vmax
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update product_item"
                    sqlcom = sqlcom + " set nama_beli = '" & Me.tb_name.Text & "',"
                    sqlcom = sqlcom + " id_measurement = " & Me.dd_satuan.SelectedValue & ","
                    sqlcom = sqlcom + " min_qty = " & Decimal.ToDouble(Me.tb_min_qty.Text) & ","
                    sqlcom = sqlcom + " id_measurement_conversion = " & Me.dd_satuan_packaging.SelectedValue & ","
                    sqlcom = sqlcom + " qty_conversion = " & Decimal.ToDouble(Me.tb_packaging_qty.Text) & ","
                    sqlcom = sqlcom + " id_brand = " & Me.dd_merek.SelectedValue & ","
                    sqlcom = sqlcom + " is_packaging = '" & Me.dd_is_packaging.SelectedValue & "',"
                    sqlcom = sqlcom + " status = '" & Me.dd_status.SelectedValue & "'"
                    sqlcom = sqlcom + " where id = " & Me.id_product
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
