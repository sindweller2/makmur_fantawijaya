Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_produk_harga_produk
    Inherits System.Web.UI.UserControl

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

            sqlcom = "select product_item.id, product_item.nama_beli, satuan_produk.name as satuan_produk,"
            sqlcom = sqlcom + " isnull(product_price.harga_beli,0) as harga_beli, product_price.id_currency,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when product_item.status = '1' then 'Aktif'"
            sqlcom = sqlcom + " when product_item.status = '0' then 'Tidak aktif'"
            sqlcom = sqlcom + " end as status"
            sqlcom = sqlcom + " from product_item"
            sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join product_price on product_price.id_product = product_item.id"
            sqlcom = sqlcom + " where id_category = " & Me.dd_category.SelectedValue
            sqlcom = sqlcom + " and id_sub_category = " & Me.dd_sub_category.SelectedValue

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(product_item.nama_beli) like upper('%" & Me.tb_search.Text & "%')"
            End If

            If Me.rad_all.Checked = True Then
                sqlcom = sqlcom
            ElseIf Me.rad_full.Checked = True Then
                sqlcom = sqlcom + " and product_price.harga_beli <> 0"
            ElseIf Me.rad_null.Checked = True Then
                sqlcom = sqlcom + " and product_price.harga_beli = 0"
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
            Me.bindkategori()
            Me.bindsubkategori()
            Me.rad_all.Checked = True
            Me.loadgrid()
        End If
    End Sub

    Protected Sub dd_category_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_category.SelectedIndexChanged
        Me.bindsubkategori()
        Me.loadgrid()
    End Sub

    Protected Sub dd_sub_category_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_sub_category.SelectedIndexChanged
        Me.loadgrid()
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

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update product_price"
                    sqlcom = sqlcom + " set harga_beli = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_harga_beli"), TextBox).Text)
                    sqlcom = sqlcom + " where id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    sqlcom = sqlcom + " and id_currency = '" & CType(Me.dg_data.Items(x).FindControl("lbl_mata_uang"), Label).Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_sinkron_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_sinkron.Click
        Try
            sqlcom = "select id from currency order by id"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                sqlcom = "insert into product_price(id_product, id_currency, harga_beli, harga_jual)"
                sqlcom = sqlcom + " select id, '" & reader.Item("id").ToString & "',0,0"
                sqlcom = sqlcom + " from product_item"
                sqlcom = sqlcom + " where id not in (select id_product from product_price where id_currency = '" & reader.Item("id").ToString & "')"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Item produk sudah selesai diupload"
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
            Me.loadgrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rad_full_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_full.CheckedChanged
        Me.loadgrid()
    End Sub

    Protected Sub rad_all_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_all.CheckedChanged
        Me.loadgrid()
    End Sub

    Protected Sub rad_null_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_null.CheckedChanged
        Me.loadgrid()
    End Sub
End Class
