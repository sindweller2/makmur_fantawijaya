Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Penjualan_kurs_harian
    Inherits System.Web.UI.UserControl
    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindkurs_harian()
        sqlcom = "select kurs_harian from kurs_harian where convert(char, tanggal, 103) = '" & Me.lbl_hari_ini.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.tb_kurs_harian.Text = FormatNumber(reader.Item("kurs_harian").ToString, 2)
        Else
            Me.tb_kurs_harian.Text = FormatNumber(0, 2)
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.lbl_hari_ini.Text = Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year
            Me.bindkurs_harian()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            Dim vtgl As String = Me.lbl_hari_ini.Text.Substring(3, 2) & "/" & Me.lbl_hari_ini.Text.Substring(0, 2) & "/" & Me.lbl_hari_ini.Text.Substring(6, 4)

            sqlcom = "select kurs_harian from kurs_harian where convert(char, tanggal, 103) = '" & Me.lbl_hari_ini.Text & "'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If Not reader.HasRows Then
                sqlcom = "insert into kurs_harian(kurs_harian, tanggal) values (" & Decimal.ToDouble(Me.tb_kurs_harian.Text) & ",'" & vtgl & "')"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
            Else
                sqlcom = "update kurs_harian"
                sqlcom = sqlcom + " set kurs_harian = " & Decimal.ToDouble(Me.tb_kurs_harian.Text)
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah diupdate"
            End If
            Me.bindkurs_harian()
        Catch ex As Exception
            Me.lbl_hari_ini.Text = ex.Message
        End Try
    End Sub
End Class
