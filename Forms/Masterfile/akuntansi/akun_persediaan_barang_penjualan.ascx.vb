Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akuntansi_akun_persediaan_barang_penjualan
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearhp_penjualan()
        Me.tb_id_akun_hppenjualan.Text = "0"
        Me.lbl_akun_hppenjualan.Text = "------"
        Me.link_popup_akun_hppenjualan.Visible = True
    End Sub

    Sub bindakunhp_penjualan()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_hppenjualan.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_hppenjualan.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearpersediaanbarang()
        Me.tb_id_akun_persediaan_barang.Text = "0"
        Me.lbl_akun_persediaan_barang.Text = "------"
        Me.link_popup_akun_persediaan_barang.Visible = True
    End Sub

    Sub bindakunpersediaanbarang()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_persediaan_barang.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_persediaan_barang.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loaddata()
        Try
            sqlcom = "select akun_hpp_penjualan, akun_persediaan_barang"
            sqlcom = sqlcom + " from akun_persediaan_penjualan"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_id_akun_hppenjualan.Text = reader.Item("akun_hpp_penjualan").ToString
                Me.tb_id_akun_persediaan_barang.Text = reader.Item("akun_persediaan_barang").ToString

                Me.bindakunhp_penjualan()
                Me.bindakunpersediaanbarang()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearhp_penjualan()
            Me.clearpersediaanbarang()
            Me.loaddata()
            Me.tb_id_akun_hppenjualan.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_persediaan_barang.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_hppenjualan.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_persediaan_barang.Attributes.Add("style", "display: none;")
            Me.link_popup_akun_hppenjualan.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_hppenjualan.ClientID & "', '" & Me.link_refresh_akun_hppenjualan.UniqueID & "')")
            Me.link_popup_akun_persediaan_barang.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_persediaan_barang.ClientID & "', '" & Me.link_refresh_akun_persediaan_barang.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub


    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_akun_hppenjualan.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun harga pokok penjualan terlebih dahulu"
            ElseIf Me.tb_id_akun_persediaan_barang.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun persediaan barang terlebih dahulu"
            Else
                sqlcom = "select * from akun_persediaan_penjualan"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If Not reader.HasRows Then
                    sqlcom = "insert into akun_persediaan_penjualan(akun_hpp_penjualan, akun_persediaan_barang)"
                    sqlcom = sqlcom + " values('" & Me.tb_id_akun_hppenjualan.Text & "','" & Me.tb_id_akun_persediaan_barang.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update akun_persediaan_penjualan"
                    sqlcom = sqlcom + " set akun_hpp_penjualan = '" & Me.tb_id_akun_hppenjualan.Text & "',"
                    sqlcom = sqlcom + " akun_persediaan_barang = '" & Me.tb_id_akun_persediaan_barang.Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub link_refresh_akun_hppenjualan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_hppenjualan.Click
        Me.bindakunhp_penjualan()
    End Sub

    Protected Sub link_refresh_akun_persediaan_barang_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_persediaan_barang.Click
        Me.bindakunpersediaanbarang()
    End Sub

End Class
