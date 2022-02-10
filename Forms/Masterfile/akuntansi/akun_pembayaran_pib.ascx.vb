Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akun_pembayaran_pib
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearbea_masuk()
        Me.tb_id_akun_bea_masuk.Text = "0"
        Me.lbl_akun_bea_masuk.Text = "------"
        Me.link_popup_akun_bea_masuk.Visible = True
    End Sub

    Sub bindakunbea_masuk()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_bea_masuk.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_bea_masuk.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearppn_masukan()
        Me.tb_id_akun_ppn_masukan.Text = "0"
        Me.lbl_akun_ppn_masukan.Text = "------"
        Me.link_popup_akun_ppn_masukan.Visible = True
    End Sub

    Sub bindakunppn_masukan()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_ppn_masukan.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_ppn_masukan.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearpph22()
        Me.tb_id_akun_pph22.Text = "0"
        Me.lbl_akun_pph22.Text = "------"
        Me.link_popup_akun_pph22.Visible = True
    End Sub

    Sub bindakunpph22()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_pph22.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_pph22.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearbiayalain2()
        Me.tb_id_akun_biaya_lain.Text = "0"
        Me.lbl_akun_biaya_lain.Text = "------"
        Me.link_popup_akun_biaya_lain.Visible = True
    End Sub

    Sub bindakunbiaya_lain2()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_biaya_lain.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_biaya_lain.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loaddata()
        Try
            sqlcom = "select akun_bea_masuk, akun_ppn_impor, akun_pph22, akun_biaya_lain2"
            sqlcom = sqlcom + " from  akun_pembayaran_pib"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_id_akun_bea_masuk.Text = reader.Item("akun_bea_masuk").ToString
                Me.tb_id_akun_ppn_masukan.Text = reader.Item("akun_ppn_impor").ToString
                Me.tb_id_akun_pph22.Text = reader.Item("akun_pph22").ToString
                Me.tb_id_akun_biaya_lain.Text = reader.Item("akun_biaya_lain2").ToString
                Me.bindakunbea_masuk()
                Me.bindakunppn_masukan()
                Me.bindakunpph22()
                Me.bindakunbiaya_lain2()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearbea_masuk()
            Me.clearppn_masukan()
            Me.clearpph22()
            Me.clearbiayalain2()
            Me.loaddata()
            Me.tb_id_akun_bea_masuk.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_ppn_masukan.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_pph22.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_biaya_lain.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_bea_masuk.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_ppn_masukan.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_pph22.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_biaya_lain.Attributes.Add("style", "display: none;")
            Me.link_popup_akun_bea_masuk.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_bea_masuk.ClientID & "', '" & Me.link_refresh_akun_bea_masuk.UniqueID & "')")
            Me.link_popup_akun_ppn_masukan.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_ppn_masukan.ClientID & "', '" & Me.link_refresh_akun_ppn_masukan.UniqueID & "')")
            Me.link_popup_akun_pph22.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_pph22.ClientID & "', '" & Me.link_refresh_akun_pph22.UniqueID & "')")
            Me.link_popup_akun_biaya_lain.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_biaya_lain.ClientID & "', '" & Me.link_refresh_akun_biaya_lain.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub


    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_akun_bea_masuk.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun bea masuk terlebih dahulu"
            ElseIf Me.tb_id_akun_ppn_masukan.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun PPN masukan terlebih dahulu"
            ElseIf Me.tb_id_akun_pph22.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun PPh22 terlebih dahulu"
            ElseIf Me.tb_id_akun_biaya_lain.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun Biaya lain2 terlebih dahulu"
            Else
                sqlcom = "select * from akun_pembayaran_pib"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If Not reader.HasRows Then
                    sqlcom = "insert into akun_pembayaran_pib(akun_bea_masuk, akun_ppn_impor, akun_pph22, akun_biaya_lain2)"
                    sqlcom = sqlcom + " values('" & Me.tb_id_akun_bea_masuk.Text & "','" & Me.tb_id_akun_ppn_masukan.Text & "',"
                    sqlcom = sqlcom + "'" & Me.tb_id_akun_pph22.Text & "','" & Me.tb_id_akun_biaya_lain.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update akun_pembayaran_pib"
                    sqlcom = sqlcom + " set akun_bea_masuk = '" & Me.tb_id_akun_bea_masuk.Text & "',"
                    sqlcom = sqlcom + " akun_ppn_impor = '" & Me.tb_id_akun_ppn_masukan.Text & "',"
                    sqlcom = sqlcom + " akun_pph22 = '" & Me.tb_id_akun_pph22.Text & "',"
                    sqlcom = sqlcom + " akun_biaya_lain2 = '" & Me.tb_id_akun_biaya_lain.Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub link_refresh_akun_bea_masuk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_bea_masuk.Click
        Me.bindakunbea_masuk()
    End Sub

    Protected Sub link_refresh_akun_ppn_masukan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_ppn_masukan.Click
        Me.bindakunppn_masukan()
    End Sub

    Protected Sub link_refresh_akun_pph22_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_pph22.Click
        Me.bindakunpph22()
    End Sub

    Protected Sub link_refresh_akun_biaya_lain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_biaya_lain.Click
        Me.bindakunbiaya_lain2()
    End Sub
End Class
