Imports System.Configuration
Imports System.Data

Partial Class Forms_Popup_popup_produk_item
    Inherits System.Web.UI.UserControl

    Public Event CloseClicked(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ProdukClicked(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Public Property id_produk() As Integer
        Get
            Dim o As Object = ViewState("id_produk")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_produk") = value
        End Set
    End Property

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select product_item.id, product_item.nama_beli, product_category.name as kategori_produk, measurement_unit.name as satuan_produk,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + satuan_packaging_produk.name + '/' + measurement_unit.name as packaging"
            sqlcom = sqlcom + " from product_item"
            sqlcom = sqlcom + " inner join product_category on product_category.id = product_item.id_category"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where status = 'True'"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(product_item.nama_beli) like upper('%" & Me.tb_search.Text & "%')"
            End If
            sqlcom = sqlcom + " order by product_item.nama_beli"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "product_item")
                Me.dg_data.DataSource = ds.Tables("product_item").DefaultView

                If ds.Tables("product_item").Rows.Count > 0 Then
                    If ds.Tables("product_item").Rows.Count > 10 Then
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
                Else
                    Me.dg_data.Visible = False
                End If
            End Using
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub tb_search_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_search.TextChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        RaiseEvent CloseClicked(sender, e)
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Me.id_produk = CType(e.Item.FindControl("lbl_id"), Label).Text
            RaiseEvent ProdukClicked(source, e)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class