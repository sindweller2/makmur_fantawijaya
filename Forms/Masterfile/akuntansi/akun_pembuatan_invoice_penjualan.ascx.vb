Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akuntansi_akun_pembuatan_invoice_penjualan
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearpenjualan()
        Me.tb_id_akun_penjualan.Text = "0"
        Me.lbl_akun_penjualan.Text = "------"
        Me.link_popup_akun_penjualan.Visible = True
    End Sub

    Sub bindakunpenjualan()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_penjualan.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_penjualan.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearpenjualan_ekspor()
        Me.tb_id_akun_penjualan_ekspor.Text = "0"
        Me.lbl_akun_penjualan_ekspor.Text = "------"
        Me.link_popup_akun_penjualan_ekspor.Visible = True
    End Sub

    Sub bindakunpenjualan_ekspor()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_penjualan_ekspor.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_penjualan_ekspor.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub


    Sub clearhutang_ppn()
        Me.tb_id_akun_hutang_ppn.Text = "0"
        Me.lbl_akun_hutang_ppn.Text = "------"
        Me.link_popup_akun_hutang_ppn.Visible = True
    End Sub

    Sub bindakunhutang_ppn()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_hutang_ppn.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_hutang_ppn.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loaddata()
        Try
            sqlcom = "select akun_penjualan, akun_penjualan_ekspor, akun_hutang_ppn"
            sqlcom = sqlcom + " from akun_invoice_penjualan"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_id_akun_penjualan.Text = reader.Item("akun_penjualan").ToString
                Me.tb_id_akun_penjualan_ekspor.Text = reader.Item("akun_penjualan_ekspor").ToString
                Me.tb_id_akun_hutang_ppn.Text = reader.Item("akun_hutang_ppn").ToString

                Me.bindakunpenjualan()
                Me.bindakunpenjualan_ekspor()
                Me.bindakunhutang_ppn()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearpenjualan()
            Me.clearpenjualan_ekspor()
            Me.clearhutang_ppn()
            Me.loaddata()
            Me.tb_id_akun_penjualan.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_penjualan_ekspor.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_hutang_ppn.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_penjualan.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_penjualan_ekspor.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_hutang_ppn.Attributes.Add("style", "display: none;")
            Me.link_popup_akun_penjualan.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_penjualan.ClientID & "', '" & Me.link_refresh_akun_penjualan.UniqueID & "')")
            Me.link_popup_akun_penjualan_ekspor.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_penjualan_ekspor.ClientID & "', '" & Me.link_refresh_akun_penjualan_ekspor.UniqueID & "')")
            Me.link_popup_akun_hutang_ppn.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_hutang_ppn.ClientID & "', '" & Me.link_refresh_akun_hutang_ppn.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub


    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_akun_penjualan.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun penjualan terlebih dahulu"
            ElseIf Me.tb_id_akun_penjualan_ekspor.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun penjualan ekspor terlebih dahulu"
            ElseIf Me.tb_id_akun_hutang_ppn.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun hutang ppn terlebih dahulu"
            Else
                sqlcom = "select * from akun_invoice_penjualan"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If Not reader.HasRows Then
                    sqlcom = "insert into akun_invoice_penjualan(akun_penjualan, akun_penjualan_ekspor, akun_hutang_ppn)"
                    sqlcom = sqlcom + " values('" & Me.tb_id_akun_penjualan.Text & "','" & me.tb_id_akun_penjualan_ekspor.text & "','" & Me.tb_id_akun_hutang_ppn.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update akun_invoice_penjualan"
                    sqlcom = sqlcom + " set akun_penjualan = '" & Me.tb_id_akun_penjualan.Text & "',"
                    sqlcom = sqlcom + " akun_penjualan_ekspor = '" & Me.tb_id_akun_penjualan_ekspor.Text & "',"
                    sqlcom = sqlcom + " akun_hutang_ppn = '" & Me.tb_id_akun_hutang_ppn.Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub link_refresh_akun_penjualan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_penjualan.Click
        Me.bindakunpenjualan()
    End Sub

    Protected Sub link_refresh_akun_penjualan_ekspor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_penjualan_ekspor.Click
        Me.bindakunpenjualan_ekspor()
    End Sub

    Protected Sub link_refresh_akun_hutang_ppn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_hutang_ppn.Click
        Me.bindakunhutang_ppn()
    End Sub
End Class
