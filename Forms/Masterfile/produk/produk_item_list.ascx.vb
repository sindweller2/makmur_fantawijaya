Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_produk_produk_item_list
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

    Public Property sub_category() As Integer
        Get
            Dim o As Object = ViewState("sub_category")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("sub_category") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindkategori()
        sqlcom = "select id, name from product_category"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_category.DataSource = reader
        Me.dd_category.DataTextField = "name"
        Me.dd_category.DataValueField = "id"
        Me.dd_category.DataBind()


        If Me.vid_category <> 0 Then
            Me.dd_category.SelectedValue = Me.vid_category
        End If

        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindsubkategori()
        sqlcom = "select id_sub_category, name from product_sub_category"
        sqlcom = sqlcom + " where id_category = " & Me.dd_category.SelectedValue
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_sub_category.DataSource = reader
        Me.dd_sub_category.DataTextField = "name"
        Me.dd_sub_category.DataValueField = "id_sub_category"
        Me.dd_sub_category.DataBind()

        If Me.sub_category <> 0 Then
            If Me.vid_sub_category <> 0 Then
                Me.dd_sub_category.SelectedValue = Me.vid_sub_category
            End If
        End If        
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub checkdata()
        sqlcom = "select *"
        sqlcom = sqlcom + " from product_item"
        sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
        sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
        sqlcom = sqlcom + " where id_category = " & Me.dd_category.SelectedValue
        sqlcom = sqlcom + " and id_sub_category = " & Me.dd_sub_category.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.tbl_search.Visible = True
        Else
            Me.tbl_search.Visible = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select product_item.id, product_item.nama_beli, "
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + satuan_packaging_produk.name + '/' + satuan_produk.name as packaging,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when is_packaging = 'P' then 'Per satuan packaging'"
            sqlcom = sqlcom + " when is_packaging = 'Q' then 'Per satuan qty packaging'"
            sqlcom = sqlcom + " end as satuan_stock,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when product_item.status = '1' then 'Aktif'"
            sqlcom = sqlcom + " when product_item.status = '0' then 'Tidak aktif'"
            sqlcom = sqlcom + " end as status"
            sqlcom = sqlcom + " from product_item"
            sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where id_category = " & Me.dd_category.SelectedValue
            sqlcom = sqlcom + " and id_sub_category = " & Me.dd_sub_category.SelectedValue

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
                    If ds.Tables("product_item").Rows.Count > 8 Then
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

            Page.Form.DefaultButton = Me.FindControl("btn_search").UniqueId

            If Me.vid_sub_category <> 0 Then
                Me.sub_category = Me.vid_sub_category
            Else
                Me.sub_category = 0
            End If

            Me.bindkategori()
            Me.bindsubkategori()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub dd_category_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_category.SelectedIndexChanged
        Me.sub_category = 0
        Me.bindsubkategori()
        Me.loadgrid()
    End Sub

    Protected Sub dd_sub_category_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_sub_category.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Response.Redirect("~/detil_produk.aspx?vid_category=" & Me.dd_category.SelectedValue & "&vid_sub_category=" & Me.dd_sub_category.SelectedValue & "&vid_product=" & CType(e.Item.FindControl("lbl_id"), Label).Text)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete product_item"
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    connection.koneksi.SelectRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah dihapus"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.lbl_msg.Text = "Data masih digunakan di form lain"
            Else
                Me.lbl_msg.Text = ex.Message
            End If
        End Try
    End Sub

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        Response.Redirect("~/detil_produk.aspx?vid_category=" & Me.dd_category.SelectedValue & "&vid_sub_category=" & Me.dd_sub_category.SelectedValue)
    End Sub
End Class
