Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_transaksi_pengeluaran_uang_lain2
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

    Private ReadOnly Property vpaging() As Integer
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vpilihan() As String
        Get
            Dim o As Object = Request.QueryString("vpilihan")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return "0"
        End Get
    End Property


    Private ReadOnly Property vsearch() As String
        Get
            Dim o As Object = Request.QueryString("vsearch")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Private ReadOnly Property vsearch_tanggal() As String
        Get
            Dim o As Object = Request.QueryString("vsearch_tanggal")
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
        sqlcom = sqlcom + " from pengeluaran_uang_lain"
        sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
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

            sqlcom = "select id, convert(char, tgl_jatuh_tempo, 103) as tgl_jatuh_tempo, keterangan, is_submit,no_voucher,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when is_submit = 'S' then 'Sudah disubmit'"
            sqlcom = sqlcom + " when is_submit = 'B' then 'Belum disubmit'"
            sqlcom = sqlcom + " end as status_submit"
            sqlcom = sqlcom + " from pengeluaran_uang_lain"
            sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue


            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                If Me.dd_pilihan.SelectedValue = "0" Then
                    sqlcom = sqlcom + " and id like '%" & Me.tb_search.Text.Trim() & "%'"
                ElseIf Me.dd_pilihan.SelectedValue = "1" Then
                    sqlcom = sqlcom + " and convert(char, tgl_jatuh_tempo, 103) = '" & Me.tb_search_tanggal.Text & "'"
                ElseIf Me.dd_pilihan.SelectedValue = "2" Then
                    sqlcom = sqlcom + " and no_voucher like '%" & Me.tb_search.Text.Trim() & "%'"
                End If
            End If

            sqlcom = sqlcom + " order by id"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "pengeluaran_uang_lain")
                Me.dg_data.DataSource = ds.Tables("pengeluaran_uang_lain").DefaultView

                If ds.Tables("pengeluaran_uang_lain").Rows.Count > 0 Then
                    If ds.Tables("pengeluaran_uang_lain").Rows.Count > 10 Then
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

            Me.dd_pilihan.SelectedValue = Me.vpilihan

            If String.IsNullOrEmpty(Me.vsearch) Then
                Me.tb_search.Text = Nothing
            Else
                Me.tb_search.Text = Me.vsearch
            End If


            If String.IsNullOrEmpty(Me.vsearch_tanggal) Then
                Me.tb_search_tanggal.Text = Nothing
            Else
                Me.tb_search_tanggal.Text = Me.vsearch_tanggal
            End If

            Me.pilihan()
            Me.bindperiode_transaksi()
            Me.bindbulan()


            If Me.vpaging <> 0 Then
                Me.dg_data.CurrentPageIndex = Me.vpaging
            End If

            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.bindbulan()
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        Response.Redirect("~/detil_transaksi_pengeluaran_uang_lain.aspx?vtahun=" & Me.tb_tahun.Text & "&vbulan=" & Me.bulan & "&vpaging=" & Me.dg_data.CurrentPageIndex)
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = Nothing
                    sqlcom = " delete puld from pengeluaran_uang_lain_detil puld inner join pengeluaran_uang_lain pul "
                    sqlcom = sqlcom + " on puld.id_pengeluaran_lain = pul.id "
                    sqlcom = sqlcom + " where puld.id_pengeluaran_lain = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), LinkButton).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                End If
            Next

            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = Nothing
                    sqlcom = "delete pengeluaran_uang_lain"
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), LinkButton).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                End If
            Next

            Me.lbl_msg.Text = "Data sudah dihapus"
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.lbl_msg.Text = "Data masih digunakan di form lain"
            Else
                Me.lbl_msg.Text = ex.Message
            End If
        End Try
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            If Me.dd_pilihan.SelectedValue = "0" Then
                Response.Redirect("~/detil_transaksi_pengeluaran_uang_lain.aspx?vid=" & CType(e.Item.FindControl("lbl_id"), LinkButton).Text & "&vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&vpaging=" & Me.dg_data.CurrentPageIndex & "&vpilihan=" & Me.dd_pilihan.SelectedValue & "&vsearch=" & Me.tb_search.Text)
            Else
                Response.Redirect("~/detil_transaksi_pengeluaran_uang_lain.aspx?vid=" & CType(e.Item.FindControl("lbl_id"), LinkButton).Text & "&vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&vpaging=" & Me.dg_data.CurrentPageIndex & "&vpilihan=" & Me.dd_pilihan.SelectedValue & "&vsearch_tanggal=" & Me.tb_search_tanggal.Text)
            End If
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.bindbulan()
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    Protected Sub dd_pilihan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_pilihan.SelectedIndexChanged
        Me.pilihan()
        Me.tb_search.Text = Nothing
        Me.tb_search_tanggal.Text = Nothing
        Me.loadgrid()
    End Sub

    Sub pilihan()
        If Me.dd_pilihan.SelectedValue <> "1" Then
            Me.tb_search.Visible = True
            Me.tb_search_tanggal.Visible = False
            Me.btn_search.Visible = True
        Else
            Me.tb_search.Visible = False
            Me.tb_search_tanggal.Visible = True
            Me.btn_search.Visible = True
        End If
    End Sub
End Class
