Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akuntansi_akun_pembayaran_piutang
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

    Sub loaddata()
        Try
            sqlcom = "select akun_piutang_giro_mundur"
            sqlcom = sqlcom + " from akun_finance_piutang"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_id_akun_piutang_giro_mundur.Text = reader.Item("akun_piutang_giro_mundur").ToString

                Me.bindakunpiutang_giro_mundur()
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
            Me.loaddata()
            Me.tb_id_akun_piutang_giro_mundur.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_piutang_giro_mundur.Attributes.Add("style", "display: none;")
            Me.link_popup_akun_piutang_giro_mundur.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_piutang_giro_mundur.ClientID & "', '" & Me.link_refresh_akun_piutang_giro_mundur.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub


    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_akun_piutang_giro_mundur.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun piutang giro mundur terlebih dahulu"
            Else
                sqlcom = "select * from akun_finance_piutang"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If Not reader.HasRows Then
                    sqlcom = "insert into akun_finance_piutang(akun_piutang_giro_mundur)"
                    sqlcom = sqlcom + " values('" & Me.tb_id_akun_piutang_giro_mundur.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update akun_finance_piutang"
                    sqlcom = sqlcom + " set akun_piutang_giro_mundur = '" & Me.tb_id_akun_piutang_giro_mundur.Text & "'"
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

End Class
