Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akun_biaya_non_lc
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearbiaya_bank()
        Me.tb_id_akun_biaya_bank.Text = "0"
        Me.lbl_akun_biaya_bank.Text = "------"
        Me.link_popup_akun_biaya_bank.Visible = True
    End Sub

    Sub bindakunbiaya_bank()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_biaya_bank.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_biaya_bank.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub


    Sub loaddata()
        Try
            sqlcom = "select akun_biaya_bank"
            sqlcom = sqlcom + " from akun_biaya_non_lc"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_id_akun_biaya_bank.Text = reader.Item("akun_biaya_bank").ToString
                Me.bindakunbiaya_bank()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearbiaya_bank()
            Me.loaddata()
            Me.tb_id_akun_biaya_bank.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_biaya_bank.Attributes.Add("style", "display: none;")
            Me.link_popup_akun_biaya_bank.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_biaya_bank.ClientID & "', '" & Me.link_refresh_akun_biaya_bank.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_akun_biaya_bank.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun biaya bank non L/C terlebih dahulu"
            Else
                sqlcom = "select * from akun_biaya_non_lc"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If Not reader.HasRows Then
                    sqlcom = "insert into akun_biaya_non_lc(akun_biaya_bank)"
                    sqlcom = sqlcom + " values('" & Me.tb_id_akun_biaya_bank.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update akun_biaya_non_lc"
                    sqlcom = sqlcom + " set akun_biaya_bank = '" & Me.tb_id_akun_biaya_bank.Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub link_refresh_akun_biaya_bank_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_biaya_bank.Click
        Me.bindakunbiaya_bank()
    End Sub


End Class

