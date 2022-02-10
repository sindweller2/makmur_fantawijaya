Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akun_penerimaan_invoice_ekspedisi_impor
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearbiaya_impor()
        Me.tb_id_akun_biaya_impor.Text = "0"
        Me.lbl_akun_biaya_impor.Text = "------"
        Me.link_popup_akun_biaya_impor.Visible = True
    End Sub

    Sub bindakunbiaya_impor()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun_biaya_impor.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun_biaya_impor.Text = reader.Item("nama_akun").ToString
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

    Sub loaddata()
        Try
            sqlcom = "select akun_biaya_impor, akun_ppn_masukan"
            sqlcom = sqlcom + " from akun_penerimaan_inv_eks_imp"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_id_akun_biaya_impor.Text = reader.Item("akun_biaya_impor").ToString
                Me.tb_id_akun_ppn_masukan.Text = reader.Item("akun_ppn_masukan").ToString 
                Me.bindakunbiaya_impor()
                Me.bindakunppn_masukan()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearbiaya_impor()
            Me.clearppn_masukan()
            Me.loaddata()
            Me.tb_id_akun_biaya_impor.Attributes.Add("style", "display: none;")
            Me.tb_id_akun_ppn_masukan.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_biaya_impor.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun_ppn_masukan.Attributes.Add("style", "display: none;")
            Me.link_popup_akun_biaya_impor.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_biaya_impor.ClientID & "', '" & Me.link_refresh_akun_biaya_impor.UniqueID & "')")
            Me.link_popup_akun_ppn_masukan.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun_ppn_masukan.ClientID & "', '" & Me.link_refresh_akun_ppn_masukan.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub


    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_akun_biaya_impor.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun biaya impor terlebih dahulu"
            ElseIf Me.tb_id_akun_ppn_masukan.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun PPN masukan terlebih dahulu"
            Else
                sqlcom = "select * from akun_penerimaan_inv_eks_imp"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If Not reader.HasRows Then
                    sqlcom = "insert into akun_penerimaan_inv_eks_imp(akun_biaya_impor, akun_ppn_masukan)"
                    sqlcom = sqlcom + " values('" & Me.tb_id_akun_biaya_impor.Text & "','" & Me.tb_id_akun_ppn_masukan.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update akun_penerimaan_inv_eks_imp"
                    sqlcom = sqlcom + " set akun_biaya_impor = '" & Me.tb_id_akun_biaya_impor.Text & "',"
                    sqlcom = sqlcom + " akun_ppn_masukan = '" & Me.tb_id_akun_ppn_masukan.Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub link_refresh_akun_biaya_impor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_biaya_impor.Click
        Me.bindakunbiaya_impor()
    End Sub

    Protected Sub link_refresh_akun_ppn_masukan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun_ppn_masukan.Click
        Me.bindakunppn_masukan()
    End Sub

End Class
