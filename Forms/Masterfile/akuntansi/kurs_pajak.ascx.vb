Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akuntansi_kurs_pajak
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindmatauang()
        sqlcom = "select id"
        sqlcom = sqlcom + " from currency"
        sqlcom = sqlcom + " where id <> 'IDR'"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_mata_uang.DataSource = reader
        Me.dd_mata_uang.DataTextField = "id"
        Me.dd_mata_uang.DataValueField = "id"
        Me.dd_mata_uang.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try
            Me.tb_tgl_awal.Text = ""
            Me.tb_tgl_akhir.Text = ""
            Me.tb_kurs_pajak.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select convert(char, tgl_awal, 103) as tgl_awal, convert(char, tgl_akhir, 103) as tgl_akhir, id_currency, isnull(rate,0) as rate"
            sqlcom = sqlcom + " from rate_pajak"
            sqlcom = sqlcom + " where id_currency = '" & Me.dd_mata_uang.SelectedValue & "'"
            sqlcom = sqlcom + " and tahun = " & Me.tb_tahun.Text
            'sqlcom = sqlcom + " order by tgl_awal"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "rate_pajak")
                Me.dg_data.DataSource = ds.Tables("rate_pajak").DefaultView

                If ds.Tables("rate_pajak").Rows.Count > 0 Then
                    If ds.Tables("rate_pajak").Rows.Count > 8 Then
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
            Me.bindmatauang()
            Me.tb_tahun.Text = Year(Now.Date)
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
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_awal.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal awal terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_akhir.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal akhir terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_kurs_pajak.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi kurs pajak terlebih dahulu"
            Else
                sqlcom = "select * from rate_pajak"
                sqlcom = sqlcom + " where convert(char, tgl_awal, 103) = '" & Me.tb_tgl_awal.Text & "'"
                sqlcom = sqlcom + " and convert(char, tgl_akhir, 103) = '" & Me.tb_tgl_akhir.Text & "'"
                sqlcom = sqlcom + " and id_currency = '" & Me.dd_mata_uang.SelectedValue & "'"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.lbl_msg.Text = "Kurs pajak untuk mata uang tersebut sudah ada"
                Else
                    Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
                    Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

                    sqlcom = "insert into rate_pajak(id_currency, rate, tgl_awal, tgl_akhir, tahun)"
                    sqlcom = sqlcom + " values ('" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs_pajak.Text) & ","
                    sqlcom = sqlcom + "'" & vtgl_awal & "','" & vtgl_akhir & "'," & Me.tb_tahun.Text & ")"
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
                    sqlcom = "delete rate_pajak"
                    sqlcom = sqlcom + " where convert(char, tgl_awal, 103) = '" & CType(Me.dg_data.Items(x).FindControl("lbl_tgl_awal"), Label).Text & "'"
                    sqlcom = sqlcom + " and convert(char, tgl_akhir, 103) = '" & CType(Me.dg_data.Items(x).FindControl("lbl_tgl_akhir"), Label).Text & "'"
                    sqlcom = sqlcom + " and id_currency = '" & Me.dd_mata_uang.SelectedValue & "'"
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
                    sqlcom = "update rate_pajak"
                    sqlcom = sqlcom + " set rate = " & CType(Me.dg_data.Items(x).FindControl("tb_kurs_pajak"), TextBox).Text
                    sqlcom = sqlcom + " where convert(char, tgl_awal, 103) = '" & CType(Me.dg_data.Items(x).FindControl("lbl_tgl_awal"), Label).Text & "'"
                    sqlcom = sqlcom + " and convert(char, tgl_akhir, 103) = '" & CType(Me.dg_data.Items(x).FindControl("lbl_tgl_akhir"), Label).Text & "'"
                    sqlcom = sqlcom + " and id_currency = '" & Me.dd_mata_uang.SelectedValue & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dd_mata_uang_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_mata_uang.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.loadgrid()
    End Sub
End Class
