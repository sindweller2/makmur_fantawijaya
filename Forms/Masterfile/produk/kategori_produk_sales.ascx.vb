Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_produk_kategori_produk_sales
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

    Sub bindsales()
        sqlcom = "select code, nama_pegawai"
        sqlcom = sqlcom + " from user_list"
        sqlcom = sqlcom + " where (code_group = 5 or code = 34)"
        sqlcom = sqlcom + " and code not in (select code_pegawai from product_category_sales where id_category = " & Me.vid_category & ")"
        sqlcom = sqlcom + " order by nama_pegawai"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_sales.DataSource = reader
        Me.dd_sales.DataTextField = "nama_pegawai"
        Me.dd_sales.DataValueField = "code"
        Me.dd_sales.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try
            
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select product_category_sales.id_category, product_category_sales.code_pegawai, user_list.nama_pegawai as nama_sales"
            sqlcom = sqlcom + " from product_category_sales"
            sqlcom = sqlcom + " inner join user_list on user_list.code = product_category_sales.code_pegawai"
            sqlcom = sqlcom + " where product_category_sales.id_category = " & Me.vid_category
            sqlcom = sqlcom + " order by user_list.nama_pegawai"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "product_category_sales")
                Me.dg_data.DataSource = ds.Tables("product_category_sales").DefaultView

                If ds.Tables("product_category_sales").Rows.Count > 0 Then
                    If ds.Tables("product_category_sales").Rows.Count > 10 Then
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
                    Me.btn_delete.Visible = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_delete.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
            Me.bindsales()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.bindkategori()
            Me.bindsales()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/product_category.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.dd_sales.SelectedIndex = -1 Then
                Me.lbl_msg.Text = "Silahkan memilih nama sales terlebih dahulu"
            Else
                sqlcom = "insert into product_category_sales(id_category, code_pegawai)"
                sqlcom = sqlcom + " values(" & Me.vid_category & "," & Me.dd_sales.SelectedValue & ")"
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
                    sqlcom = "delete product_category_sales"
                    sqlcom = sqlcom + " where id_category = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_category"), Label).Text
                    sqlcom = sqlcom + " and code_pegawai = " & CType(Me.dg_data.Items(x).FindControl("lbl_code_pegawai"), Label).Text
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

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class
