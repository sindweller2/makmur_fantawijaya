Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_pembayaran_expedition_invoice
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
        sqlcom = sqlcom + " from received_import_expedition_invoice"
        sqlcom = sqlcom + " inner join daftar_expedition on daftar_expedition.id = received_import_expedition_invoice.id_expedition"
        sqlcom = sqlcom + " inner join penugasan_ekspedisi_impor on penugasan_ekspedisi_impor.seq = received_import_expedition_invoice.seq_penugasan_ekspedisi"
        sqlcom = sqlcom + " where received_import_expedition_invoice.id_transaction_period = " & Me.dd_bulan.SelectedValue
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

            sqlcom = "select received_import_expedition_invoice.id, convert(char, received_import_expedition_invoice.tanggal, 103) as tanggal,"
            sqlcom = sqlcom + " received_import_expedition_invoice.id_expedition, received_import_expedition_invoice.invoice_no, "
            sqlcom = sqlcom + " convert(char, received_import_expedition_invoice.invoice_date, 103) as invoice_date,"
            sqlcom = sqlcom + " received_import_expedition_invoice.is_bayar, "
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when received_import_expedition_invoice.is_bayar = 'B' then 'Belum dibayar'"
            sqlcom = sqlcom + " when received_import_expedition_invoice.is_bayar = 'S' then 'Sudah dibayar'"
            sqlcom = sqlcom + " end as status_bayar,"
            sqlcom = sqlcom + " convert(char, received_import_expedition_invoice.tanggal_bayar, 103) as tanggal_bayar,"
            sqlcom = sqlcom + " seq_penugasan_ekspedisi, received_import_expedition_invoice.is_submit_bayar,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when received_import_expedition_invoice.is_submit_bayar = 'B' then 'Belum disubmit'"
            sqlcom = sqlcom + " when received_import_expedition_invoice.is_submit_bayar = 'S' then 'Sudah disubmit'"
            sqlcom = sqlcom + " end as status_submit,"
            sqlcom = sqlcom + " daftar_expedition.name as nama_ekspedisi,"
            sqlcom = sqlcom + " penugasan_ekspedisi_impor.no_aju"
            sqlcom = sqlcom + " from received_import_expedition_invoice"
            sqlcom = sqlcom + " inner join daftar_expedition on daftar_expedition.id = received_import_expedition_invoice.id_expedition"
            sqlcom = sqlcom + " inner join penugasan_ekspedisi_impor on penugasan_ekspedisi_impor.seq = received_import_expedition_invoice.seq_penugasan_ekspedisi"
            sqlcom = sqlcom + " where received_import_expedition_invoice.id_transaction_period = " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " and is_submit = 'S'"

            If Me.dd_pilihan.SelectedValue = "0" Or Me.dd_pilihan.SelectedValue = "1" Then
                If String.IsNullOrEmpty(Me.tb_search.Text) Then
                    sqlcom = sqlcom
                Else
                    If Me.dd_pilihan.SelectedValue = "0" Then
                        sqlcom = sqlcom + " and upper(penugasan_ekspedisi_impor.no_aju) like upper('%" & Me.tb_search.Text & "%')"
                    ElseIf Me.dd_pilihan.SelectedValue = "1" Then
                        sqlcom = sqlcom + " and upper(received_import_expedition_invoice.invoice_no) like upper('%" & Me.tb_search.Text & "%')"
                    ElseIf Me.dd_pilihan.SelectedValue = "2" Then
                        sqlcom = sqlcom + " and upper(daftar_expedition.name) like upper('%" & Me.tb_search.Text & "%')"
                    End If
                End If
            ElseIf Me.dd_pilihan.SelectedValue = "2" Then
                If Me.dd_submit.SelectedValue = "B" Then
                    sqlcom = sqlcom + " and received_import_expedition_invoice.is_submit_bayar = 'B'"
                Else
                    sqlcom = sqlcom + " and received_import_expedition_invoice.is_submit_bayar = 'S'"
                End If
            Else
                If Me.dd_bayar.SelectedValue = "B" Then
                    sqlcom = sqlcom + " and received_import_expedition_invoice.is_bayar = 'B'"
                Else
                    sqlcom = sqlcom + " and received_import_expedition_invoice.is_bayar = 'S'"
                End If
            End If

            sqlcom = sqlcom + " order by received_import_expedition_invoice.id"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "received_import_expedition_invoice")
                Me.dg_data.DataSource = ds.Tables("received_import_expedition_invoice").DefaultView

                If ds.Tables("received_import_expedition_invoice").Rows.Count > 0 Then
                    If ds.Tables("received_import_expedition_invoice").Rows.Count > 10 Then
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
                Else
                    Me.dg_data.Visible = False
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

            Me.bindperiode_transaksi()
            Me.dd_pilihan.SelectedValue = "0"
            Me.pilihan()
            Me.bindbulan()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Response.Redirect("~/detil_pembayaran_expedition_invoice.aspx?vid_received=" & CType(e.Item.FindControl("lbl_no_penerimaan"), LinkButton).Text & "&vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.bindbulan()
        Me.loadgrid()
    End Sub

    Sub pilihan()
        If Me.dd_pilihan.SelectedValue = "0" Or Me.dd_pilihan.SelectedValue = "1" Or Me.dd_pilihan.SelectedValue = "2" Then
            Me.tb_search.Visible = True
            Me.btn_search.Visible = True
            Me.dd_submit.Visible = False
            Me.dd_bayar.Visible = False
        ElseIf Me.dd_pilihan.SelectedValue = "3" Then
            Me.tb_search.Visible = False
            Me.btn_search.Visible = False
            Me.dd_submit.Visible = True
            Me.dd_bayar.Visible = False
        Else
            Me.tb_search.Visible = False
            Me.btn_search.Visible = False
            Me.dd_submit.Visible = False
            Me.dd_bayar.Visible = True
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

    Protected Sub dd_bayar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bayar.SelectedIndexChanged
        Me.loadgrid()
    End Sub
End Class
