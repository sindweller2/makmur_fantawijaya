Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_supplier_daftar_supplier_kontak_person
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Private ReadOnly Property vid_supplier() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_supplier")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Sub bindsupplier()
        sqlcom = "select name from daftar_supplier where id = " & Me.vid_supplier
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_name.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.tb_kontak_person.Text = ""
        Me.tb_email.Text = ""
    End Sub

    Sub loadgrid()
        Try
            Me.clearform()
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, contact_person, email"
            sqlcom = sqlcom + " from daftar_supplier_contact_person"
            sqlcom = sqlcom + " where id_supplier = " & Me.vid_supplier
            sqlcom = sqlcom + " order by contact_person"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "daftar_supplier_contact_person")
                Me.dg_data.DataSource = ds.Tables("daftar_supplier_contact_person").DefaultView

                If ds.Tables("daftar_supplier_contact_person").Rows.Count > 0 Then
                    If ds.Tables("daftar_supplier_contact_person").Rows.Count > 8 Then
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
            Me.bindsupplier()
            Me.clearform()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/detil_supplier.aspx?vid_supplier=" & Me.vid_supplier)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_kontak_person.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama kontak person terlebih dahulu"
            Else
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vmax from daftar_supplier_contact_person"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into daftar_supplier_contact_person(id_supplier, seq, contact_person, email)"
                sqlcom = sqlcom + " values(" & Me.vid_supplier & "," & vmax & ",'" & Me.tb_kontak_person.Text & "','" & Me.tb_email.Text & "')"
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
                    sqlcom = "delete daftar_supplier_contact_person"
                    sqlcom = sqlcom + " where id_supplier = " & Me.vid_supplier
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
                    sqlcom = "update daftar_supplier_contact_person"
                    sqlcom = sqlcom + " set contact_person = '" & CType(Me.dg_data.Items(x).FindControl("tb_contact_person"), TextBox).Text & "',"
                    sqlcom = sqlcom + " email = '" & CType(Me.dg_data.Items(x).FindControl("tb_email"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where id_supplier = " & Me.vid_supplier
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
