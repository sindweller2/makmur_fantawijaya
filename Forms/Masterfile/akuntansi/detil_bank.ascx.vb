Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_keuangan_detil_bank
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Private ReadOnly Property vid_bank() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_bank")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property id_bank() As Integer
        Get
            Dim o As Object = ViewState("id_bank")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_bank") = value
        End Set
    End Property

    Sub clearform()
        Me.id_bank = 0
        Me.tb_name.Text = ""
        Me.tb_alamat.Text = ""
        Me.tb_telepon.Text = ""
        Me.tb_fax.Text = ""
        Me.tb_kontak_person.Text = ""
    End Sub

    Sub loaddata()
        Try
            If Me.vid_bank <> 0 Then
                Me.id_bank = Me.vid_bank
            End If

            sqlcom = "select name, address, phone, fax, contact_person,"
            sqlcom = sqlcom + "(select accountno + ' - ' + inaname from coa_list where accountno = bank_list.akun_hutang_tr_idr) as akun_hutang_tr_idr,"
            sqlcom = sqlcom + "(select accountno + ' - ' + inaname from coa_list where accountno = bank_list.akun_hutang_tr_usd) as akun_hutang_tr_usd"
            sqlcom = sqlcom + " from bank_list"
            sqlcom = sqlcom + " where id = " & Me.id_bank
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_name.Text = reader.Item("name").ToString
                Me.tb_alamat.Text = reader.Item("address").ToString
                Me.tb_telepon.Text = reader.Item("phone").ToString
                Me.tb_fax.Text = reader.Item("fax").ToString
                Me.tb_kontak_person.Text = reader.Item("contact_person").ToString
                Me.lbl_akun_hutang_tr_idr.Text = reader.Item("akun_hutang_tr_idr").ToString
                Me.lbl_akun_hutang_tr_usd.Text = reader.Item("akun_hutang_tr_usd").ToString
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
            Me.loaddata()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/bank_list.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama customer terlebih dahulu"
            Else

                If Me.id_bank = 0 Then
                    Dim vmax As Integer = 0
                    sqlcom = "select isnull(max(id),0) + 1 as vmax from bank_list"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = reader.Item("vmax").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into bank_list(id, name, address, phone, fax, contact_person)"
                    sqlcom = sqlcom + " values(" & vmax & ",'" & Me.tb_name.Text & "','" & Me.tb_alamat.Text & "','" & Me.tb_telepon.Text & "','" & Me.tb_fax.Text & "','" & Me.tb_kontak_person.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.id_bank = vmax
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update bank_list"
                    sqlcom = sqlcom + " set name = '" & Me.tb_name.Text & "',"
                    sqlcom = sqlcom + " address = '" & Me.tb_alamat.Text & "',"
                    sqlcom = sqlcom + " phone = '" & Me.tb_telepon.Text & "',"
                    sqlcom = sqlcom + " fax = '" & Me.tb_fax.Text & "',"
                    sqlcom = sqlcom + " contact_person = '" & Me.tb_kontak_person.Text & "'"
                    sqlcom = sqlcom + " where id = " & Me.id_bank
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
