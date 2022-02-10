Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_accounting_periode_transaksi
    Inherits System.Web.UI.UserControl

    Private Property vawal() As String
        Get
            Dim o As Object = ViewState("vawal")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vawal") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindbulan()
        Try
            sqlcom = "select id, name from bulan order by id"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_bulan.DataSource = reader
            Me.dd_bulan.DataTextField = "name"
            Me.dd_bulan.DataValueField = "id"
            Me.dd_bulan.DataBind()

            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
    Private Sub BindMataUang()
        Try
            sqlcom = "select * from(select ''id,''name from currency " & _
                                "union " & _
                                "SELECT id, name FROM currency )a"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.ddlMataUang.DataSource = reader
            Me.ddlMataUang.DataTextField = "id"
            Me.ddlMataUang.DataValueField = "id"
            Me.ddlMataUang.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message

        End Try
    End Sub

    Sub loadgrid()
        Try
            Me.tb_name.Text = ""
            Me.tb_tgl_awal.Text = ""
            Me.tb_tgl_akhir.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id, bulan, tahun, name, convert(char, tgl_awal, 103) as tgl_awal,"
            sqlcom = sqlcom + " convert(char, tgl_akhir, 103) as tgl_akhir,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when is_closing = 'A' then 'Sudah'"
            sqlcom = sqlcom + " when is_closing = 'B' then 'Belum'"
            sqlcom = sqlcom + " end as closing,"
            sqlcom = sqlcom + " is_awal"
            sqlcom = sqlcom + " from transaction_period"
            sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
            sqlcom = sqlcom + " order by bulan"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "transaction_period")
                Me.dg_data.DataSource = ds.Tables("transaction_period").DefaultView

                If ds.Tables("transaction_period").Rows.Count > 0 Then
                    If ds.Tables("transaction_period").Rows.Count > 12 Then
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

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name from bulan order by id"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_bulan"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_bulan"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_bulan"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_bulan"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_bulan"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_bulan"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        'sqlcom = "select id from currency order by id"
                        'reader = connection.koneksi.SelectRecord(sqlcom)
                        'CType(Me.dg_data.Items(x).FindControl("ddlMataUang"), DropDownList).DataSource = reader
                        'CType(Me.dg_data.Items(x).FindControl("ddlMataUang"), DropDownList).DataTextField = "id"
                        'CType(Me.dg_data.Items(x).FindControl("ddlMataUang"), DropDownList).DataValueField = "id"
                        'CType(Me.dg_data.Items(x).FindControl("ddlMataUang"), DropDownList).DataBind()
                        'CType(Me.dg_data.Items(x).FindControl("ddlMataUang"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lblMataUang"), Label).Text
                        'reader.Close()
                        'connection.koneksi.CloseKoneksi()


                        CType(Me.dg_data.Items(x).FindControl("dd_is_awal"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_is_awal"), Label).Text
                    Next


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
            Me.bindbulan()

            Me.loadgrid()

            Me.BindMataUang()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.loadgrid()
    End Sub

    Sub checkawal()
        Try
            sqlcom = "select * from transaction_period where is_awal = 'Y'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vawal = "Y"
            Else
                Me.vawal = "T"
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tahun.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tahun terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama periode terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_awal.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal awal periode terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_akhir.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal akhir periode terlebih dahulu"
                'ElseIf String.IsNullOrEmpty(Me.ddlMataUang.Text) Then
                '    Me.lbl_msg.Text = "Silahkan mengisi mata uang terlebih dahulu"
            Else
                If Me.dd_is_awal.SelectedValue = "Y" Then
                    Me.checkawal()
                    If Me.vawal = "Y" Then
                        Me.lbl_msg.Text = "Sudah ada periode transaksi yang merupakan periode awal"
                        Exit Sub
                    End If
                End If

                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(id),0) + 1 as vmax from transaction_period"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
                Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

                sqlcom = "insert into transaction_period(id, bulan, tahun, name, tgl_awal, tgl_akhir, is_closing, is_awal)"
                sqlcom = sqlcom + " values (" & vmax & "," & Me.dd_bulan.SelectedValue & "," & Me.tb_tahun.Text & ",'" & Me.tb_name.Text & "',"
                sqlcom = sqlcom + "'" & vtgl_awal & "','" & vtgl_akhir & "','B','" & Me.dd_is_awal.SelectedValue & "')"
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
                    sqlcom = "delete transaction_period"
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
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

                    If CType(Me.dg_data.Items(x).FindControl("dd_is_awal"), DropDownList).SelectedValue = "Y" Then
                        Me.checkawal()
                        If Me.vawal = "Y" Then
                            Me.lbl_msg.Text = "Sudah ada periode transaksi yang merupakan periode awal"
                            Exit Sub
                        End If
                    End If

                    Dim vtgl_awal As String = CType(Me.dg_data.Items(x).FindControl("tb_tgl_awal"), TextBox).Text
                    Dim vtgl_akhir As String = CType(Me.dg_data.Items(x).FindControl("tb_tgl_akhir"), TextBox).Text

                    vtgl_awal = vtgl_awal.Substring(3, 2) & "/" & vtgl_awal.Substring(0, 2) & "/" & vtgl_awal.Substring(6, 4)
                    vtgl_akhir = vtgl_akhir.Substring(3, 2) & "/" & vtgl_akhir.Substring(0, 2) & "/" & vtgl_akhir.Substring(6, 4)

                    sqlcom = "update transaction_period"
                    sqlcom = sqlcom + " set bulan = " & CType(Me.dg_data.Items(x).FindControl("dd_bulan"), DropDownList).SelectedValue & ","
                    sqlcom = sqlcom + " name = '" & CType(Me.dg_data.Items(x).FindControl("tb_name"), TextBox).Text & "',"
                    sqlcom = sqlcom + " tgl_awal = '" & vtgl_awal & "',"
                    sqlcom = sqlcom + " tgl_akhir = '" & vtgl_akhir & "',"
                    sqlcom = sqlcom + " is_awal = '" & CType(Me.dg_data.Items(x).FindControl("dd_is_awal"), DropDownList).SelectedValue & "'"
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
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
