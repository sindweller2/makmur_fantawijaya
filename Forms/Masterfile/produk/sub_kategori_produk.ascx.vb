Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_produk_sub_kategori_produk
    Inherits System.Web.UI.UserControl

    Private ReadOnly Property vid_category() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_category")
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
            Me.lbl_kategori.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try
            Me.tb_name.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id_sub_category as id, name"
            sqlcom = sqlcom + " from product_sub_category"
            sqlcom = sqlcom + " where id_category = " & Me.vid_category
            sqlcom = sqlcom + " order by name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "product_sub_category")
                Me.dg_data.DataSource = ds.Tables("product_sub_category").DefaultView

                If ds.Tables("product_sub_category").Rows.Count > 0 Then
                    If ds.Tables("product_sub_category").Rows.Count > 10 Then
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
            Me.bindkategori()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/product_category.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama sub kategori produk terlebih dahulu"
            Else
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(id_sub_category),0) + 1 as vmax from product_sub_category"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into product_sub_category(id_sub_category, name, id_category)"
                sqlcom = sqlcom + " values(" & vmax & ",'" & Me.tb_name.Text & "'," & Me.vid_category & ")"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"

                Me.loadgrid()

            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked Then
                    sqlcom = "delete product_sub_category where id_sub_category = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
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

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked Then
                    sqlcom = "update product_sub_category"
                    sqlcom = sqlcom + " set name = '" & CType(Me.dg_data.Items(x).FindControl("tb_name"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where id_sub_category = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
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
End Class
