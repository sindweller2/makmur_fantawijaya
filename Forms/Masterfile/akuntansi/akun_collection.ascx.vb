Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akuntansi_akun_collection
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearpiutang_giro_mundur()
        Me.tb_id_akun_piutang_giro_mundur.Text = "0"
        Me.lbl_akun_piutang_giro_mundur.Text = "------"
        Me.link_popup_akun_piutang_giro_mundur.Visible = True
    End Sub

    Sub bindakunpiutang_giro_mundur()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_piutang_giro_mundur.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_piutang_giro_mundur.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearpiutang_dagang()
        Me.tb_id_akun_piutang_dagang.Text = "0"
        Me.lbl_akun_piutang_dagang.Text = "------"
        Me.link_popup_akun_piutang_dagang.Visible = True
    End Sub

    Sub bindakunpiutang_dagang()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_piutang_dagang.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_piutang_dagang.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loaddata()
        Try
            sqlcom = "select akun_piutang_giro_mundur, akun_piutang_dagang"
            sqlcom = sqlcom + " from akun_collection"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_id_akun_piutang_giro_mundur.Text = reader.Item("akun_piutang_giro_mundur").ToString
                Me.tb_id_akun_piutang_dagang.Text = reader.Item("akun_piutang_dagang").ToString

                Me.bindakunpiutang_giro_mundur()
                Me.bindakunpiutang_dagang()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearpiutang_giro_mundur()
            Me.clearpiutang_dagang()
            Me.loaddata()
            Me.tb_id_akun_piutang_giro_mundur.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_piutang_dagang.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_piutang_giro_mundur.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_piutang_dagang.Attributes.Add("style", "display: none;")
            Me.link_popup_akun_piutang_giro_mundur.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_piutang_giro_mundur.ClientID & "', '" & Me.link_refresh_akun_piutang_giro_mundur.UniqueID & "')")
            Me.link_popup_akun_piutang_dagang.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_piutang_dagang.ClientID & "', '" & Me.link_refresh_akun_piutang_dagang.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub


    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_akun_piutang_giro_mundur.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun piutang giro mundur terlebih dahulu"
            ElseIf Me.tb_id_akun_piutang_dagang.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun piutang dagang terlebih dahulu"
            Else
                sqlcom = "select * from akun_collection"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If Not reader.HasRows Then
                    sqlcom = "insert into akun_collection(akun_piutang_giro_mundur, akun_piutang_dagang)"
                    sqlcom = sqlcom + " values('" & Me.tb_id_akun_piutang_giro_mundur.Text & "','" & Me.tb_id_akun_piutang_dagang.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update akun_collection"
                    sqlcom = sqlcom + " set akun_piutang_giro_mundur = '" & Me.tb_id_akun_piutang_giro_mundur.Text & "',"
                    sqlcom = sqlcom + " akun_piutang_dagang = '" & Me.tb_id_akun_piutang_dagang.Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub link_refresh_akun_piutang_giro_mundur_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_piutang_giro_mundur.Click
        Me.bindakunpiutang_giro_mundur()
    End Sub

    Protected Sub link_refresh_akun_piutang_dagang_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_piutang_dagang.Click
        Me.bindakunpiutang_dagang()
    End Sub
End Class
