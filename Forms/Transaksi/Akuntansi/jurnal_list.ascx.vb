Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Akuntansi_jurnal_list
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()
    'Daniel

    Private ReadOnly Property vtahun() As Integer
        Get
            Dim o As Object = Request.QueryString("vtahun")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vbulan() As Integer
        Get
            Dim o As Object = Request.QueryString("vbulan")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vjenis_jurnal() As String
        Get
            Dim o As Object = Request.QueryString("vjenis_jurnal")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Public Property bulan() As Integer
        Get
            Dim o As Object = ViewState("bulan")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("bulan") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    'Daniel
    'Sub bindperiode_transaksi()
    '    sqlcom = "select id, name"
    '    sqlcom = sqlcom + " from transaction_period"
    '    sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
    '    sqlcom = sqlcom + " order by bulan"
    '    reader = connection.koneksi.SelectRecord(sqlcom)
    '    Me.dd_bulan.DataSource = reader
    '    Me.dd_bulan.DataTextField = "name"
    '    Me.dd_bulan.DataValueField = "id"
    '    Me.dd_bulan.DataBind()
    '    reader.Close()
    '    connection.koneksi.CloseKoneksi()

    '    sqlcom = "select id from transaction_period where bulan = " & Me.bulan & " and tahun=" & Me.tb_tahun.Text
    '    reader = connection.koneksi.SelectRecord(sqlcom)
    '    reader.Read()
    '    If reader.HasRows Then
    '        Me.dd_bulan.SelectedValue = reader.Item("id").ToString
    '    End If
    '    reader.Close()
    '    connection.koneksi.CloseKoneksi()

    'End Sub


    'Sub bindbulan()
    '    sqlcom = "select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue
    '    reader = connection.koneksi.SelectRecord(sqlcom)
    '    reader.Read()
    '    If reader.HasRows Then
    '        Me.bulan = reader.Item("bulan").ToString
    '    End If
    '    reader.Close()
    '    connection.koneksi.CloseKoneksi()
    'End Sub

    'Sub checkdata()
    '    sqlcom = "select *"
    '    sqlcom = sqlcom + " from jurnal_umum"
    '    sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
    '    sqlcom = sqlcom + " where tanggal between '" + Me.tradingClass.DateValidated(Me.tb_tgl_awal.Text) + "' and '" + Me.tradingClass.DateValidated(Me.tb_tgl_akhir.Text) + "'"
    '    reader = connection.koneksi.SelectRecord(sqlcom)
    '    reader.Read()
    '    If reader.HasRows Then
    '        Me.tbl_search.Visible = True
    '    Else
    '        Me.tbl_search.Visible = False
    '    End If
    '    reader.Close()
    '    connection.koneksi.CloseKoneksi()
    'End Sub
    'Daniel

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

    Sub loadgrid()
        Try
            'Daniel
            'Me.checkdata()
            Me.lbl_msg.Text = String.Empty
            'Daniel
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)
            'Daniel
            'sqlcom = "select id, id_text, rtrim(convert(char, tanggal, 103)) as tanggal, id_currency,"
            'sqlcom = sqlcom + " is_submit,"
            'sqlcom = sqlcom + " case"
            'sqlcom = sqlcom + " when is_submit = 'S' then 'Sudah'"
            'sqlcom = sqlcom + " when is_submit = 'B' then 'Belum'"
            'sqlcom = sqlcom + " end as status_submit,"
            'sqlcom = sqlcom + " isnull((select sum(isnull(debet,0)) from jurnal_item where id_jurnal = jurnal_umum.id),0) as total"
            'sqlcom = sqlcom + " from jurnal_umum"
            sqlcom = "SELECT [seq],[id_jurnal],convert(char, [tgl_jurnal], 103) as tgl_jurnal,[nama_jurnal],[keterangan],[id_transaction_period] FROM [akun_jurnal]"
            sqlcom = sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)


            'sqlcom = sqlcom + " where [id_transaction_period] = " & Me.dd_bulan.SelectedValue
            'sqlcom = sqlcom + " and jenis_jurnal = '" & Me.dd_jenis_jurnal.SelectedValue & "'"

            'If Me.dd_pilihan.SelectedValue = "0" Then
            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and keterangan like '%" & Me.tb_search.Text & "%'"
            End If
            'Else
            '    sqlcom = sqlcom + " and is_submit = '" & Me.dd_submit.SelectedValue & "'"
            'End If

            sqlcom = sqlcom + " order by 1,2,3"
            'Daniel

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                'Daniel
                da.Fill(ds, "akun_jurnal")
                Me.dg_data.DataSource = ds.Tables("akun_jurnal").DefaultView

                If ds.Tables("akun_jurnal").Rows.Count > 0 Then
                    If ds.Tables("akun_jurnal").Rows.Count > 10 Then
                        'Daniel
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
                    Me.btn_delete.Visible = True
                    'Daniel
                    Me.lbl_search.Visible = True
                    Me.tb_search.Visible = True
                    Me.btn_search.Visible = True
                    Me.btn_new.Visible = True

                    'For x As Integer = 0 To Me.dg_data.Items.Count - 1
                    '    If CType(Me.dg_data.Items(x).FindControl("lbl_is_submit"), Label).Text = "B" Then
                    '        CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                    '    Else
                    '        CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                    '    End If
                    'Next
                    'Daniel
                Else
                    Me.dg_data.Visible = False
                    Me.btn_delete.Visible = False
                    'Daniel
                    Me.lbl_search.Visible = False
                    Me.tb_search.Visible = False
                    Me.btn_search.Visible = False
                    Me.btn_new.Visible = False
                    'Daniel
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Daniel
            'If Me.vtahun = 0 Then
            '    Me.tb_tahun.Text = Now.Year
            'Else
            '    Me.tb_tahun.Text = Me.vtahun
            'End If
            'Daniel

            If Me.vbulan = 0 Then
                Me.bulan = Now.Month
            Else
                Me.bulan = Me.vbulan
            End If

            'Daniel
            'If String.IsNullOrEmpty(Me.vjenis_jurnal) Then
            '    Me.dd_jenis_jurnal.SelectedValue = "U"
            'Else
            '    Me.dd_jenis_jurnal.SelectedValue = Me.vjenis_jurnal
            'End If

            'Me.dd_pilihan.SelectedValue = "0"
            'Me.pilihan()
            'Me.bindperiode_transaksi()
            'Me.bindbulan()
            Me.tb_tahun.Text = Year(Now.Date)
            Me.bindperiode_transaksi()

            'Daniel
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        'Daniel
        'Me.bindperiode_transaksi()
        'Daniel
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        'Daniel
        'Response.Redirect("~/detil_jurnal.aspx?vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&vseq=" & String.Empty)
        Response.Redirect("~/detil_jurnal.aspx?vseq=" & String.Empty)
        'Daniel
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    'Daniel
                    sqlcom = "delete akun_jurnal"
                    sqlcom = sqlcom + " where id_jurnal = " & CType(Me.dg_data.Items(x).FindControl("lbl_no_jurnal"), LinkButton).Text
                    'Daniel
                    connection.koneksi.DeleteRecord(sqlcom)

                    'Daniel
                    sqlcom = "delete akun_general_ledger"
                    sqlcom = sqlcom + " where id_transaksi = " & CType(Me.dg_data.Items(x).FindControl("lbl_no_jurnal"), LinkButton).Text
                    connection.koneksi.DeleteRecord(sqlcom)


                    'Me.lbl_msg.Text = "Data sudah dihapus"
                    'Daniel
                End If
            Next

            tradingClass.Alert("Data sudah dihapus", Me.Page)

            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                tradingClass.Alert("Data masih digunakan di form lain", Me.Page)
            Else
                tradingClass.Alert(ex.Message, Me.Page)
            End If
        End Try
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            'Daniel
            'Response.Redirect("~/detil_jurnal.aspx?vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&vid=" & CType(e.Item.FindControl("lbl_no_jurnal"), LinkButton).Text)
            Response.Redirect("~/detil_jurnal.aspx?vid=" & CType(e.Item.FindControl("lbl_no_jurnal"), LinkButton).Text)
            'Daniel
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    'Daniel
    'Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
    '    Me.bindbulan()
    '    Me.loadgrid()
    'End Sub
    'Daniel

    'Daniel
    'Protected Sub dd_jenis_jurnal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_jenis_jurnal.SelectedIndexChanged
    '    Me.loadgrid()
    'End Sub
    'Daniel

    'Sub pilihan()
    '    If Me.dd_pilihan.SelectedValue = "0" Then
    '        Me.tb_search.Visible = True
    '        Me.btn_search.Visible = True
    '        Me.dd_submit.Visible = False
    '    Else
    '        Me.tb_search.Visible = False
    '        Me.btn_search.Visible = False
    '        Me.dd_submit.Visible = True
    '    End If
    'End Sub

    'Protected Sub dd_pilihan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_pilihan.SelectedIndexChanged
    '    Me.pilihan()
    '    Me.loadgrid()
    'End Sub

    'Protected Sub dd_submit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_submit.SelectedIndexChanged
    '    Me.loadgrid()
    'End Sub
    'Daniel

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    Protected Sub btn_refresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        Me.bindperiode_transaksi()
    End Sub
End Class
