Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_Purchasing_detil_ekspedisi_import
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Private ReadOnly Property vid_ekspedisi() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_ekspedisi")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property id_ekspedisi() As Integer
        Get
            Dim o As Object = ViewState("id_ekspedisi")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_ekspedisi") = value
        End Set
    End Property

    Sub clearform()
        Me.id_ekspedisi = 0
        Me.tb_name.Text = ""
        Me.tb_alamat.Text = ""
        Me.tb_telp.Text = ""
        Me.tb_fax.Text = ""
        Me.tb_kontak_person.Text = ""
        Me.tb_lama_pembayaran.Text = ""        
    End Sub

    Sub loaddata()
        Try
            If Me.vid_ekspedisi <> 0 Then
                Me.id_ekspedisi = Me.vid_ekspedisi
            End If

            sqlcom = "select name, alamat, tipe, telp, fax, kontak_person, lama_pembayaran, status,"
            sqlcom = sqlcom + "(select inaname from coa_list where accountno = daftar_expedition.akun_hutang_lain2) as akun_hutang_lain2"
            sqlcom = sqlcom + " from daftar_expedition"
            sqlcom = sqlcom + " where id = " & Me.id_ekspedisi
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_name.Text = reader.Item("name").ToString                
                Me.tb_alamat.Text = reader.Item("alamat").ToString
                Me.dd_jenis_layanan.SelectedValue = reader.Item("tipe").ToString
                Me.tb_telp.Text = reader.Item("telp").ToString
                Me.tb_fax.Text = reader.Item("fax").ToString
                Me.tb_kontak_person.Text = reader.Item("kontak_person").ToString
                Me.tb_lama_pembayaran.Text = reader.Item("lama_pembayaran").ToString
                Me.dd_status.SelectedValue = reader.Item("status").ToString
                Me.lbl_akun_hutang_lain_lain.Text = reader.Item("akun_hutang_lain2").ToString
                Me.btn_biaya_jasa.Enabled = True
            Else
                Me.btn_biaya_jasa.Enabled = False
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
        Response.Redirect("~/expedition.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama ekspedisi terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_alamat.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi alamat terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_lama_pembayaran.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi lama pembayaran terlebih dahulu"
            Else
                If Me.id_ekspedisi = 0 Then
                    Dim vmax As Integer = 0
                    sqlcom = "select isnull(max(id),0) + 1 as vmax from daftar_expedition"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = reader.Item("vmax").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into daftar_expedition(id, name, alamat, tipe, telp, fax, kontak_person, lama_pembayaran, status)"
                    sqlcom = sqlcom + " values(" & vmax & ",'" & Me.tb_name.Text & "','" & Me.tb_alamat.Text & "','" & Me.dd_jenis_layanan.SelectedValue & "'"
                    sqlcom = sqlcom + ",'" & Me.tb_telp.Text & "','" & Me.tb_fax.Text & "','" & Me.tb_kontak_person.Text & "'"
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_lama_pembayaran.Text) & ",'" & Me.dd_status.SelectedValue & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.id_ekspedisi = vmax
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update daftar_expedition"
                    sqlcom = sqlcom + " set name = '" & Me.tb_name.Text & "',"
                    sqlcom = sqlcom + " alamat = '" & Me.tb_alamat.Text & "',"
                    sqlcom = sqlcom + " tipe = '" & Me.dd_jenis_layanan.SelectedValue & "',"
                    sqlcom = sqlcom + " telp = '" & Me.tb_telp.Text & "',"
                    sqlcom = sqlcom + " fax = '" & Me.tb_fax.Text & "',"
                    sqlcom = sqlcom + " kontak_person = '" & Me.tb_kontak_person.Text & "',"
                    sqlcom = sqlcom + " lama_pembayaran = " & Decimal.ToDouble(Me.tb_lama_pembayaran.Text) & ","
                    sqlcom = sqlcom + " status = '" & Me.dd_status.SelectedValue & "'"
                    sqlcom = sqlcom + " where id = " & Me.id_ekspedisi
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

End Class
