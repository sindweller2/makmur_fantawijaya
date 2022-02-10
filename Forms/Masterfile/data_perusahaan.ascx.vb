Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_data_perusahaan
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub loaddata()
        Try
            sqlcom = "select nama, alamat, telp, fax, npwp, api, no_asuransi"
            sqlcom = sqlcom + " from data_perusahaan"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_name.Text = reader.Item("nama").ToString
                Me.tb_alamat.Text = reader.Item("alamat").ToString
                Me.tb_telp.Text = reader.Item("telp").ToString
                Me.tb_fax.Text = reader.Item("fax").ToString
                Me.tb_npwp.Text = reader.Item("npwp").ToString
                Me.tb_no_api.Text = reader.Item("api").ToString
                Me.tb_asuransi.Text = reader.Item("no_asuransi").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.loaddata()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama perusahaan terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_alamat.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi alamat terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_telp.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor telepon terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_fax.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor fax terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_npwp.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor NPWP terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_no_api.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor API terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_asuransi.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor asuransi import terlebih dahulu"
            Else
                sqlcom = "update data_perusahaan"
                sqlcom = sqlcom + " set nama = '" & Me.tb_name.Text & "',"
                sqlcom = sqlcom + " alamat = '" & Me.tb_alamat.Text & "',"
                sqlcom = sqlcom + " telp = '" & Me.tb_telp.Text & "',"
                sqlcom = sqlcom + " fax = '" & Me.tb_fax.Text & "',"
                sqlcom = sqlcom + " npwp = '" & Me.tb_npwp.Text & "',"
                sqlcom = sqlcom + " api = '" & Me.tb_no_api.Text & "',"
                sqlcom = sqlcom + " no_asuransi = '" & Me.tb_asuransi.Text & "'"
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
