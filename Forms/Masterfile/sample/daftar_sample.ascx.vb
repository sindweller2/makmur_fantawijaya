Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_sample_daftar_sample
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindkategori()
        Try
            sqlcom = "select id, name from product_category"
            sqlcom = sqlcom + " order by name"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_category.DataSource = reader
            Me.dd_category.DataTextField = "name"
            Me.dd_category.DataValueField = "id"
            Me.dd_category.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bindsubkategori()
        Try
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
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bindsatuan()
        Try
            sqlcom = "select id, name from measurement_unit"
            sqlcom = sqlcom + " order by name"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_satuan.DataSource = reader
            Me.dd_satuan.DataTextField = "name"
            Me.dd_satuan.DataValueField = "id"
            Me.dd_satuan.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception

        End Try
    End Sub

    Sub checkdata()
        Try
            sqlcom = "select *"
            sqlcom = sqlcom + " from sample"
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
        Catch ex As Exception

        End Try
    End Sub

    Sub loadgrid()
        Try

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select sample.id, sample.name, sample.id_satuan, sample.status, "
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when sample.status = '1' then 'Aktif'"
            sqlcom = sqlcom + " when sample.status = '0' then 'Tidak aktif'"
            sqlcom = sqlcom + " end as status"
            sqlcom = sqlcom + " from sample"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = sample.id_satuan"
            sqlcom = sqlcom + " where id_category = " & Me.dd_category.SelectedValue
            sqlcom = sqlcom + " and id_sub_category = " & Me.dd_sub_category.SelectedValue

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(sample.name) like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " order by sample.name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "sample")
                Me.dg_data.DataSource = ds.Tables("sample").DefaultView

                If ds.Tables("sample").Rows.Count > 0 Then
                    If ds.Tables("sample").Rows.Count > 8 Then
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
                    Me.btn_delete.Visible = True

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name from measurement_unit"
                        sqlcom = sqlcom + " order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_satuan"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_satuan"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_satuan"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_satuan"), DropDownList).DataBind()
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        CType(Me.dg_data.Items(x).FindControl("dd_satuan"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_id_satuan"), Label).Text
                        CType(Me.dg_data.Items(x).FindControl("dd_status"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_status"), Label).Text
                    Next
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
            Me.bindsubkategori()
            Me.bindsatuan()
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

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_nama.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama sample terlebih dahulu"
            Else
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(id),0) + 1 as vmax from sample"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into sample(id, name, id_satuan, id_category, id_sub_category, status)"
                sqlcom = sqlcom + " values(" & vmax & ",'" & Me.tb_nama.Text & "'," & Me.dd_satuan.SelectedValue & "," & Me.dd_category.SelectedValue
                sqlcom = sqlcom + "," & Me.dd_sub_category.SelectedValue & ",'A')"
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
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete sample"
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
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
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update sample"
                    sqlcom = sqlcom + " set name = '" & CType(Me.dg_data.Items(x).FindControl("tb_name"), TextBox).Text & "',"
                    sqlcom = sqlcom + " id_satuan = " & CType(Me.dg_data.Items(x).FindControl("dd_satuan"), DropDownList).SelectedValue & ","
                    sqlcom = sqlcom + " status = '" & CType(Me.dg_data.Items(x).FindControl("dd_status"), DropDownList).SelectedValue & "'"
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
