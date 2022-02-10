Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akuntansi_nomor_urut_faktur_pajak
    Inherits System.Web.UI.UserControl

    
    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub loadgrid()
        Try
            Me.tb_no_awal.Text = ""
            Me.tb_no_akhir.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, no_awal, no_akhir, no_berjalan"
            sqlcom = sqlcom + " from no_akhir_pajak"
            sqlcom = sqlcom + " order by seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "no_akhir_pajak")
                Me.dg_data.DataSource = ds.Tables("no_akhir_pajak").DefaultView

                If ds.Tables("no_akhir_pajak").Rows.Count > 0 Then
                    If ds.Tables("no_akhir_pajak").Rows.Count > 12 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 12
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
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

     Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_no_awal.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi No. awal terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_no_akhir.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi No. akhir terlebih dahulu"
            Else                
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vmax from no_akhir_pajak"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into no_akhir_pajak(seq, no_awal, no_akhir, no_berjalan)"
                sqlcom = sqlcom + " values (" & vmax & "," & Me.tb_no_awal.Text & "," & Me.tb_no_akhir.Text & "," & int(me.tb_no_awal.text) - 1 & ")"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete no_akhir_pajak"
                    sqlcom = sqlcom + " where seq= " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
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
                    sqlcom = "update no_akhir_pajak"
                    sqlcom = sqlcom + " set no_awal = " & int(CType(Me.dg_data.Items(x).FindControl("tb_no_awal"), TextBox).Text) & ","
                    sqlcom = sqlcom + " no_akhir = " & int(CType(Me.dg_data.Items(x).FindControl("tb_no_akhir"), TextBox).Text) & ","
                    sqlcom = sqlcom + " no_berjalan = " & int(CType(Me.dg_data.Items(x).FindControl("tb_no_berjalan"), TextBox).Text)
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
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
