Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akun_pembayaran_lc
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearuang_muka_lc()
        Me.tb_id_akun_uang_muka_lc.Text = "0"
        Me.lbl_akun_uang_muka_lc.Text = "------"
        Me.link_popup_akun_uang_muka_lc.Visible = True
    End Sub

    Sub bindakunuang_muka_lc()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_uang_muka_lc.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_uang_muka_lc.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearpembelian_impor()
        Me.tb_id_akun_pembelian_impor.Text = "0"
        Me.lbl_akun_pembelian_impor.Text = "------"
        Me.link_popup_akun_pembelian_impor.Visible = True
    End Sub

    Sub bindakunpembelian_impor()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_pembelian_impor.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_pembelian_impor.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub


    Sub loaddata()
        Try
            sqlcom = "select akun_uang_muka_lc, akun_pembelian_impor"
            sqlcom = sqlcom + " from akun_pembayaran_lc"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_id_akun_uang_muka_lc.Text = reader.Item("akun_uang_muka_lc").ToString
                Me.tb_id_akun_pembelian_impor.Text = reader.Item("akun_pembelian_impor").ToString

                Me.bindakunuang_muka_lc()
                Me.bindakunpembelian_impor()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearuang_muka_lc()
            Me.clearpembelian_impor()
            Me.loaddata()
            Me.tb_id_akun_uang_muka_lc.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_pembelian_impor.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_uang_muka_lc.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_pembelian_impor.Attributes.Add("style", "display: none;")
            Me.link_popup_akun_uang_muka_lc.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_uang_muka_lc.ClientID & "', '" & Me.link_refresh_akun_uang_muka_lc.UniqueID & "')")
            Me.link_popup_akun_pembelian_impor.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_pembelian_impor.ClientID & "', '" & Me.link_refresh_akun_pembelian_impor.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub


    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_akun_uang_muka_lc.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun uang muka L/C terlebih dahulu"
            ElseIf Me.tb_id_akun_pembelian_impor.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama pembelian impor terlebih dahulu"
            Else
                sqlcom = "select * from akun_pembayaran_lc"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If Not reader.HasRows Then
                    sqlcom = "insert into akun_pembayaran_lc(akun_uang_muka_lc, akun_pembelian_impor)"
                    sqlcom = sqlcom + " values('" & Me.tb_id_akun_uang_muka_lc.Text & "','" & Me.tb_id_akun_pembelian_impor.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update akun_pembayaran_lc"
                    sqlcom = sqlcom + " set akun_uang_muka_lc = '" & Me.tb_id_akun_uang_muka_lc.Text & "',"
                    sqlcom = sqlcom + " akun_pembelian_impor = '" & Me.tb_id_akun_pembelian_impor.Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub link_refresh_akun_uang_muka_lc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_uang_muka_lc.Click
        Me.bindakunuang_muka_lc()
    End Sub


    Protected Sub link_refresh_akun_pembelian_impor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_pembelian_impor.Click
        Me.bindakunpembelian_impor()
    End Sub

End Class
