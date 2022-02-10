Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_customer_alamat_customer
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Private ReadOnly Property vid_customer() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_customer")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Sub bindcustomer()
        sqlcom = "select name from daftar_customer where id = " & Me.vid_customer
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_customer.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.tb_alamat.Text = ""
        Me.tb_no_telp.Text = ""
        Me.tb_no_telp.Text = ""
        Me.tb_fax.Text = ""
        Me.tb_email.Text = ""
        Me.tb_kontak_person.Text = ""
    End Sub

    Sub loadgrid()
        Try
            Me.clearform()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, alamat, tlp, fax, email, contact_person"
            sqlcom = sqlcom + " from daftar_customer_alamat"
            sqlcom = sqlcom + " where id_customer = " & Me.vid_customer
            sqlcom = sqlcom + " order by seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "daftar_customer_alamat")
                Me.dg_data.DataSource = ds.Tables("daftar_customer_alamat").DefaultView

                If ds.Tables("daftar_customer_alamat").Rows.Count > 0 Then
                    If ds.Tables("daftar_customer_alamat").Rows.Count > 8 Then
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
            Me.bindcustomer()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/detil_customer.aspx?vid_customer=" & Me.vid_customer)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_alamat.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi alamat terlebih dahulu"
            Else
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vmax from daftar_customer_alamat"
                sqlcom = sqlcom + " where id_customer = " & Me.vid_customer
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into daftar_customer_alamat(id_customer, seq, alamat, tlp, fax, email, contact_person)"
                sqlcom = sqlcom + " values(" & Me.vid_customer & "," & vmax & ",'" & Me.tb_alamat.Text & "','" & Me.tb_no_telp.Text & "',"
                sqlcom = sqlcom + "'" & Me.tb_fax.Text & "','" & Me.tb_email.Text & "','" & Me.tb_kontak_person.Text & "')"
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
                    sqlcom = "delete daftar_customer_alamat"
                    sqlcom = sqlcom + " where id_customer = " & Me.vid_customer
                    sqlcom = sqlcom + " and seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
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
                    sqlcom = "update daftar_customer_alamat"
                    sqlcom = sqlcom + " set alamat = '" & CType(Me.dg_data.Items(x).FindControl("tb_alamat"), TextBox).Text & "',"
                    sqlcom = sqlcom + " tlp = '" & CType(Me.dg_data.Items(x).FindControl("tb_telepon"), TextBox).Text & "',"
                    sqlcom = sqlcom + " fax = '" & CType(Me.dg_data.Items(x).FindControl("tb_fax"), TextBox).Text & "',"
                    sqlcom = sqlcom + " email = '" & CType(Me.dg_data.Items(x).FindControl("tb_email"), TextBox).Text & "',"
                    sqlcom = sqlcom + " contact_person = '" & CType(Me.dg_data.Items(x).FindControl("tb_contact_person"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where id_customer = " & Me.vid_customer
                    sqlcom = sqlcom + " and seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
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
