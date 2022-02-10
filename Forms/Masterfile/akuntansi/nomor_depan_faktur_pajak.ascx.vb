Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akuntansi_nomor_depan_faktur_pajak
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub loadgrid()
        Try
            Me.tb_tahun.Text = ""
            Me.tb_nomor_depan.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select tahun, no_depan, is_kawasan_berikat,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when is_kawasan_berikat = 'Y' then 'Ya'"
            sqlcom = sqlcom + " when is_kawasan_berikat = 'T' then 'Tidak'"
            sqlcom = sqlcom + " end as kawasan_berikat"
            sqlcom = sqlcom + " from nomor_depan_faktur_pajak"
            sqlcom = sqlcom + " order by tahun"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "nomor_depan_faktur_pajak")
                Me.dg_data.DataSource = ds.Tables("nomor_depan_faktur_pajak").DefaultView

                If ds.Tables("nomor_depan_faktur_pajak").Rows.Count > 0 Then
                    If ds.Tables("nomor_depan_faktur_pajak").Rows.Count > 8 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 8
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_update.Visible = True
                    Me.btn_delete.Visible = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tahun.Text = Now.Year
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tahun.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tahun terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_nomor_depan.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor depan faktur pajak terlebih dahulu"
            Else
                sqlcom = "select * from nomor_depan_faktur_pajak"
                sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
                sqlcom = sqlcom + " and is_kawasan_berikat = '" & Me.dd_is_kawasan_berikat.SelectedValue & "'"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.lbl_msg.Text = "Tahun tersebut sudah ada"
                Else
                    sqlcom = "insert into nomor_depan_faktur_pajak(tahun, no_depan, is_kawasan_berikat)"
                    sqlcom = sqlcom + " values (" & Me.tb_tahun.Text & ",'" & Me.tb_nomor_depan.Text & "','" & Me.dd_is_kawasan_berikat.SelectedValue & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                    Me.loadgrid()
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete nomor_depan_faktur_pajak"
                    sqlcom = sqlcom + " where tahun = " & CType(Me.dg_data.Items(x).FindControl("lbl_tahun"), Label).Text
                    sqlcom = sqlcom + " and is_kawasan_berikat = '" & CType(Me.dg_data.Items(x).FindControl("lbl_is_kawasan_berikat"), Label).Text & "'"
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah dihapus"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.lbl_msg.Text = "Data masih digunakan di form lain"
            Else
                Me.lbl_msg.Text = ex.Message
            End If
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update nomor_depan_faktur_pajak"
                    sqlcom = sqlcom + " set no_depan = '" & CType(Me.dg_data.Items(x).FindControl("tb_nomor_depan"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where tahun = " & CType(Me.dg_data.Items(x).FindControl("lbl_tahun"), Label).Text
                    sqlcom = sqlcom + " and is_kawasan_berikat = '" & CType(Me.dg_data.Items(x).FindControl("lbl_is_kawasan_berikat"), Label).Text & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class

