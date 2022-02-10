Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Collection_transfer_antar_kas
    Inherits System.Web.UI.UserControl

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

    Private ReadOnly Property voption() As Integer
        Get
            Dim o As Object = Request.QueryString("voption")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vsearch() As String
        Get
            Dim o As Object = Request.QueryString("vsearch")
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

        sqlcom = "select id from transaction_period where bulan = " & Me.bulan & " and tahun=" & Me.tb_tahun.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.dd_bulan.SelectedValue = reader.Item("id").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

    End Sub

    Sub bindbulan()
        sqlcom = "select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.bulan = reader.Item("bulan").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub checkdata()
        sqlcom = "select *"
        sqlcom = sqlcom + " from transfer_antar_kas"
        sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
        'sqlcom = sqlcom + " and dibuat_oleh = " & Me.id_user
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.tbl_search.Visible = True
        Else
            Me.tbl_search.Visible = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, convert(char, tanggal_transfer, 103) as tanggal_transfer, isnull(nilai,0) as nilai, keterangan, is_submit,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when is_submit = 'S' then 'Sudah disubmit'"
            sqlcom = sqlcom + " when is_submit = 'B' then 'Belum disubmit'"
            sqlcom = sqlcom + " end as status_submit,"
            sqlcom = sqlcom + " bank_account.id_currency as mata_uang"
            sqlcom = sqlcom + " from transfer_antar_kas"
            sqlcom = sqlcom + " inner join bank_account on bank_account.id = transfer_antar_kas.id_cash_bank_asal"
            sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
            'sqlcom = sqlcom + " and dibuat_oleh = " & Me.id_user

            If Me.dd_pilihan.SelectedValue = "0" Then
                If String.IsNullOrEmpty(Me.tb_search.Text) Then
                    sqlcom = sqlcom
                Else
                    sqlcom = sqlcom + " and seq like upper('%" & Me.tb_search.Text & "%')"
                End If
            ElseIf Me.dd_pilihan.SelectedValue = "2" Then
                If String.IsNullOrEmpty(Me.tb_search.Text) Then
                    sqlcom = sqlcom
                Else
                    sqlcom = sqlcom + " and upper(keterangan) like upper('%" & Me.tb_search.Text & "%')"
                End If            
            Else
                If Me.dd_submit.SelectedValue = "B" Then
                    sqlcom = sqlcom + " and is_submit = 'B'"
                Else
                    sqlcom = sqlcom + " and is_submit = 'S'"
                End If
            End If

            sqlcom = sqlcom + " order by seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "transfer_antar_kas")
                Me.dg_data.DataSource = ds.Tables("transfer_antar_kas").DefaultView

                If ds.Tables("transfer_antar_kas").Rows.Count > 0 Then
                    If ds.Tables("transfer_antar_kas").Rows.Count > 10 Then
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

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        If CType(Me.dg_data.Items(x).FindControl("lbl_is_submit"), Label).Text = "B" Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                        End If
                    Next
                Else
                    Me.dg_data.Visible = False
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

            If Me.vtahun = 0 Then
                Me.tb_tahun.Text = Now.Year
            Else
                Me.tb_tahun.Text = Me.vtahun
            End If

            If Me.vbulan = 0 Then
                Me.bulan = Now.Month
            Else
                Me.bulan = Me.vbulan
            End If

            Me.bindperiode_transaksi()

            Me.dd_pilihan.SelectedValue = Me.voption

            If Me.voption = 0 or me.voption = 2 Then
                Me.tb_search.Text = Me.vsearch
            Else
                Me.dd_submit.SelectedValue = Me.vsearch
            End If


            Me.pilihan()
            Me.bindbulan()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.bindbulan()
        Me.loadgrid()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.bindbulan()
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            If Me.dd_pilihan.SelectedValue = 0 or Me.dd_pilihan.SelectedValue = 2 Then
                Response.Redirect("~/transfer_antar_kas_detil.aspx?vid=" & CType(e.Item.FindControl("lbl_seq"), LinkButton).Text & "&vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&voption=" & Me.dd_pilihan.SelectedValue & "&vsearch=" & Me.tb_search.Text)
            ElseIf Me.dd_pilihan.SelectedValue = 1 Then
                Response.Redirect("~/transfer_antar_kas_detil.aspx?vid=" & CType(e.Item.FindControl("lbl_seq"), LinkButton).Text & "&vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&voption=" & Me.dd_pilihan.SelectedValue & "&vsearch=" & Me.dd_submit.SelectedValue)
            End If
        End If
    End Sub

    Sub pilihan()
        If Me.dd_pilihan.SelectedValue = "0" or Me.dd_pilihan.SelectedValue = "2"  Then
            Me.tb_search.Visible = True
            Me.btn_search.Visible = True
            Me.dd_submit.Visible = False
        Else
            Me.tb_search.Visible = False
            Me.btn_search.Visible = False
            Me.dd_submit.Visible = True
        End If
    End Sub

    Protected Sub dd_pilihan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_pilihan.SelectedIndexChanged
        Me.pilihan()
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub dd_submit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_submit.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        Response.Redirect("~/transfer_antar_kas_detil.aspx?vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text)
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete transfer_antar_kas"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), LinkButton).Text
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

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class
