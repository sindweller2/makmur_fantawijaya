Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Collection_permintaan_petty_cash
    Inherits System.Web.UI.UserControl

    Public Property vbulan() As Integer
        Get
            Dim o As Object = ViewState("vbulan")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vbulan") = value
        End Set
    End Property

    Public Property id_user() As Integer
        Get
            Dim o As Object = ViewState("id_user")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_user") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode_transaksi()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
        sqlcom = sqlcom + " order by bulan"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_bulan.DataSource = reader
        Me.dd_bulan.DataTextField = "name"
        Me.dd_bulan.DataValueField = "id"
        Me.dd_bulan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindmata_uang()
        sqlcom = "select id from currency order by id"
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

            Me.tb_nilai.Text = ""
            Me.tb_keterangan.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id, convert(char, tanggal, 103) as tanggal, id_currency, is_cash, isnull(nilai,0) as nilai, keterangan, diminta_oleh,"
            sqlcom = sqlcom + " isnull(jumlah_dikeluarkan,0) as jumlah_dikeluarkan, status, is_submit"
            sqlcom = sqlcom + " from permintaan_petty_cash"
            sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " order by id"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "permintaan_petty_cash")
                Me.dg_data.DataSource = ds.Tables("permintaan_petty_cash").DefaultView

                If ds.Tables("permintaan_petty_cash").Rows.Count > 0 Then
                    If ds.Tables("permintaan_petty_cash").Rows.Count > 10 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 10
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_update.Visible = True
                    Me.btn_delete.Visible = True

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1

                        CType(Me.dg_data.Items(x).FindControl("dd_is_cash"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_is_cash"), Label).Text

                        sqlcom = "select id from currency order by id"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataTextField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_mata_uang"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If CType(Me.dg_data.Items(x).FindControl("lbl_submit"), Label).Text = "S" Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                        End If
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
            Me.id_user = HttpContext.Current.Session("UserID")
            Me.tb_tahun.Text = Now.Year
            Me.tb_tanggal.Text = Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year
            Me.bindperiode_transaksi()
            Me.dd_bulan.SelectedValue = Month(Now.Date)
            Me.bindmata_uang()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tanggal.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_nilai.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah nilai terlebih dahulu"
            ElseIf Not String.IsNullOrEmpty(Me.tb_nilai.Text) And Decimal.ToDouble(Me.tb_nilai.Text) <= 0 Then
                Me.lbl_msg.Text = "Jumlah nilai tidak boleh lebih kecil atau sama dengan Nol"
            Else
                sqlcom = "select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.vbulan = reader.Item("bulan").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vmax As String = ""
                sqlcom = "select isnull(max(convert(int, right(id, 5))),0) + 1 as vid"
                sqlcom = sqlcom + " from permintaan_petty_cash"
                sqlcom = sqlcom + " where convert(int, substring(convert(char, id), 3,2)) = " & Me.vbulan
                sqlcom = sqlcom + " and convert(int, left(id, 2)) = " & Right(Me.tb_tahun.Text, 2)
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = Right(Me.tb_tahun.Text, 2) & Me.vbulan.ToString.PadLeft(2, "0") & reader.Item("vid").ToString.PadLeft(5, "0")
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vtgl As String = Me.tb_tanggal.Text.Substring(3, 2) & "/" & Me.tb_tanggal.Text.Substring(0, 2) & "/" & Me.tb_tanggal.Text.Substring(6, 4)

                sqlcom = "insert into permintaan_petty_cash(id, tanggal, id_currency, is_cash, nilai, keterangan, id_transaction_period,"
                sqlcom = sqlcom + " diminta_oleh, status, is_submit, is_submit_keuangan)"
                sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "','" & Me.dd_mata_uang.SelectedValue & "','" & Me.dd_is_cash.SelectedValue & "'"
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_nilai.Text) & ",'" & Me.tb_keterangan.Text & "'," & Me.dd_bulan.SelectedValue
                sqlcom = sqlcom + "," & Me.id_user & ",'P', 'B', 'B')"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete permintaan_petty_cash"
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
                    Dim vtgl As String = CType(Me.dg_data.Items(x).FindControl("tb_tanggal"), TextBox).Text
                    vtgl = vtgl.Substring(3, 2) & "/" & vtgl.Substring(0, 2) & "/" & vtgl.Substring(6, 4)

                    sqlcom = "update permintaan_petty_cash"
                    sqlcom = sqlcom + " set tanggal = '" & vtgl & "',"
                    sqlcom = sqlcom + " id_currency = '" & CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue & "',"
                    sqlcom = sqlcom + " is_cash = '" & CType(Me.dg_data.Items(x).FindControl("dd_is_cash"), DropDownList).SelectedValue & "',"
                    sqlcom = sqlcom + " nilai = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_nilai"), TextBox).Text) & ","
                    sqlcom = sqlcom + " keterangan = '" & CType(Me.dg_data.Items(x).FindControl("tb_keterangan"), TextBox).Text & "'"
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

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Try
                sqlcom = "update permintaan_petty_cash"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where id = " & CType(e.Item.FindControl("lbl_id"), Label).Text
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disubmit dan tidak dapat diubah kembali"
                Me.loadgrid()
            Catch ex As Exception
                Me.lbl_msg.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class
