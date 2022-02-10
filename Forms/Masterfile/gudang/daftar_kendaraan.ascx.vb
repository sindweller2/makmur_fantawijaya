Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_gudang_daftar_kendaraan
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindjenis_kendaraan()
        sqlcom = "select id, name from jenis_vehicle order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_jenis_kendaraan.DataSource = reader
        Me.dd_jenis_kendaraan.DataTextField = "name"
        Me.dd_jenis_kendaraan.DataValueField = "id"
        Me.dd_jenis_kendaraan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try
            Me.tb_no_polisi.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id, no_polisi, id_jenis_vehicle"
            sqlcom = sqlcom + " from vehicle_list"
            sqlcom = sqlcom + " order by no_polisi"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "vehicle_list")
                Me.dg_data.DataSource = ds.Tables("vehicle_list").DefaultView

                If ds.Tables("vehicle_list").Rows.Count > 0 Then
                    If ds.Tables("vehicle_list").Rows.Count > 10 Then
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

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name from jenis_vehicle order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_kendaraan"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_kendaraan"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_kendaraan"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_kendaraan"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_kendaraan"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_jenis_kendaraan"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
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
            Me.bindjenis_kendaraan()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_no_polisi.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor polisi terlebih dahulu"
            Else
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(id),0) + 1 as vmax from vehicle_list"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into vehicle_list(id, no_polisi, id_jenis_vehicle)"
                sqlcom = sqlcom + " values(" & vmax & ",'" & Me.tb_no_polisi.Text & "'," & Me.dd_jenis_kendaraan.SelectedValue & ")"
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
                    sqlcom = "delete vehicle_list where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
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
                    sqlcom = "update vehicle_list"
                    sqlcom = sqlcom + " set no_polisi = '" & CType(Me.dg_data.Items(x).FindControl("tb_no_polisi"), TextBox).Text & "',"
                    sqlcom = sqlcom + " id_jenis_vehicle = " & CType(Me.dg_data.Items(x).FindControl("dd_jenis_kendaraan"), DropDownList).SelectedValue
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

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class
