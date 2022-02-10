Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akun_biaya_lc
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearkomisi_bank()
        Me.tb_id_akun_komisi_bank.Text = "0"
        Me.lbl_akun_komisi_bank.Text = "------"
        Me.link_popup_akun_komisi_bank.Visible = True
    End Sub

    Sub bindakunkomisi_bank()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_komisi_bank.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_komisi_bank.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearongkos_kawat()
        Me.tb_id_akun_ongkos_kawat.Text = "0"
        Me.lbl_akun_ongkos_kawat.Text = "------"
        Me.link_popup_akun_ongkos_kawat.Visible = True
    End Sub

    Sub bindakunongkos_kawat()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_ongkos_kawat.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_ongkos_kawat.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearporto_materai()
        Me.tb_id_akun_porto_materai.Text = "0"
        Me.lbl_akun_porto_materai.Text = "------"
        Me.link_popup_akun_porto_materai.Visible = True
    End Sub

    Sub bindakunporto_materai()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_porto_materai.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_porto_materai.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearbiaya_courier()
        Me.tb_id_akun_biaya_courier.Text = "0"
        Me.lbl_akun_biaya_courier.Text = "------"
        Me.link_popup_akun_biaya_courier.Visible = True
    End Sub

    Sub bindakunbiaya_courier()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_biaya_courier.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_biaya_courier.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearbiaya_lc_amendment()
        Me.tb_id_akun_biaya_lc_amendment.Text = "0"
        Me.lbl_akun_biaya_lc_amendment.Text = "------"
        Me.link_popup_akun_biaya_lc_amendment.Visible = True
    End Sub

    Sub bindakunbiaya_lc_amendment()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_biaya_lc_amendment.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_biaya_lc_amendment.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loaddata()
        Try
            sqlcom = "select akun_komisi_bank, akun_ongkos_kawat, akun_porto_materai, akun_biaya_courier, akun_biaya_lc_amendment"
            sqlcom = sqlcom + " from akun_biaya_lc"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_id_akun_komisi_bank.Text = reader.Item("akun_komisi_bank").ToString
                Me.tb_id_akun_ongkos_kawat.Text = reader.Item("akun_ongkos_kawat").ToString
                Me.tb_id_akun_porto_materai.Text = reader.Item("akun_porto_materai").ToString
                Me.tb_id_akun_biaya_courier.Text = reader.Item("akun_biaya_courier").ToString
                Me.tb_id_akun_biaya_lc_amendment.Text = reader.Item("akun_biaya_lc_amendment").ToString
                Me.bindakunkomisi_bank()
                Me.bindakunongkos_kawat()
                Me.bindakunporto_materai()
                Me.bindakunbiaya_courier()
                Me.bindakunbiaya_lc_amendment()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearkomisi_bank()
            Me.clearongkos_kawat()
            Me.clearporto_materai()
            Me.clearbiaya_courier()
            Me.clearbiaya_lc_amendment()
            Me.loaddata()
            Me.tb_id_akun_komisi_bank.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_ongkos_kawat.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_porto_materai.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_biaya_courier.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_biaya_lc_amendment.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_komisi_bank.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_ongkos_kawat.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_porto_materai.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_biaya_courier.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_biaya_lc_amendment.Attributes.Add("style", "display: none;")
            Me.link_popup_akun_komisi_bank.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_komisi_bank.ClientID & "', '" & Me.link_refresh_akun_komisi_bank.UniqueID & "')")
            Me.link_popup_akun_ongkos_kawat.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_ongkos_kawat.ClientID & "', '" & Me.link_refresh_akun_ongkos_kawat.UniqueID & "')")
            Me.link_popup_akun_porto_materai.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_porto_materai.ClientID & "', '" & Me.link_refresh_akun_porto_materai.UniqueID & "')")
            Me.link_popup_akun_biaya_courier.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_biaya_courier.ClientID & "', '" & Me.link_refresh_akun_biaya_courier.UniqueID & "')")
            Me.link_popup_akun_biaya_lc_amendment.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_biaya_lc_amendment.ClientID & "', '" & Me.link_refresh_akun_biaya_lc_amendment.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_akun_komisi_bank.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun komisi bank terlebih dahulu"
            ElseIf Me.tb_id_akun_ongkos_kawat.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun ongkos kawat terlebih dahulu"
            ElseIf Me.tb_id_akun_porto_materai.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun porto materai terlebih dahulu"
            ElseIf Me.tb_id_akun_biaya_courier.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun biaya courier terlebih dahulu"
            ElseIf Me.tb_id_akun_biaya_lc_amendment.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun biaya L/C amendment terlebih dahulu"
            Else
                sqlcom = "select * from akun_biaya_lc"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If Not reader.HasRows Then
                    sqlcom = "insert into akun_biaya_lc(akun_komisi_bank, akun_ongkos_kawat, akun_porto_materai, akun_biaya_courier, akun_biaya_lc_amendment)"
                    sqlcom = sqlcom + " values('" & Me.tb_id_akun_komisi_bank.Text & "','" & Me.tb_id_akun_ongkos_kawat.Text & "',"
                    sqlcom = sqlcom + "'" & Me.tb_id_akun_porto_materai.Text & "','" & Me.tb_id_akun_biaya_courier.Text & "',"
                    sqlcom = sqlcom + "'" & Me.tb_id_akun_biaya_lc_amendment.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update akun_biaya_lc"
                    sqlcom = sqlcom + " set akun_komisi_bank = '" & Me.tb_id_akun_komisi_bank.Text & "',"
                    sqlcom = sqlcom + " akun_ongkos_kawat = '" & Me.tb_id_akun_ongkos_kawat.Text & "',"
                    sqlcom = sqlcom + " akun_porto_materai = '" & Me.tb_id_akun_porto_materai.Text & "',"
                    sqlcom = sqlcom + " akun_biaya_courier = '" & Me.tb_id_akun_biaya_courier.Text & "',"
                    sqlcom = sqlcom + " akun_biaya_lc_amendment = '" & Me.tb_id_akun_biaya_lc_amendment.Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub link_refresh_akun_komisi_bank_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_komisi_bank.Click
        Me.bindakunkomisi_bank()
    End Sub

    Protected Sub link_refresh_akun_ongkos_kawat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_ongkos_kawat.Click
        Me.bindakunongkos_kawat()
    End Sub

    Protected Sub link_refresh_akun_porto_materai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_porto_materai.Click
        Me.bindakunporto_materai()
    End Sub

    Protected Sub link_refresh_akun_biaya_courier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_biaya_courier.Click
        Me.bindakunbiaya_courier()
    End Sub

    Protected Sub link_refresh_akun_biaya_lc_amendment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_biaya_lc_amendment.Click
        Me.bindakunbiaya_lc_amendment()
    End Sub
End Class

