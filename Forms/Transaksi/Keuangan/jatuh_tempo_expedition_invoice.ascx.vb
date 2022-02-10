Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_jatuh_tempo_expedition_invoice
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub checkdata()
        Dim vtgl As String = Me.tb_tanggal.Text.Substring(3, 2) & "/" & Me.tb_tanggal.Text.Substring(0, 2) & "/" & Me.tb_tanggal.Text.Substring(6, 4)

        sqlcom = "select *"
        sqlcom = sqlcom + " from received_import_expedition_invoice"
        sqlcom = sqlcom + " inner join daftar_expedition on daftar_expedition.id = received_import_expedition_invoice.id_expedition"
        sqlcom = sqlcom + " inner join penugasan_ekspedisi_impor on penugasan_ekspedisi_impor.seq = received_import_expedition_invoice.seq_penugasan_ekspedisi"
        sqlcom = sqlcom + " where received_import_expedition_invoice.tgl_jatuh_tempo <= '" & vtgl & "'"
        sqlcom = sqlcom + " and is_submit = 'S' and is_bayar = 'B'"
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

            Dim vtgl As String = Me.tb_tanggal.Text.Substring(3, 2) & "/" & Me.tb_tanggal.Text.Substring(0, 2) & "/" & Me.tb_tanggal.Text.Substring(6, 4)

            sqlcom = "select received_import_expedition_invoice.id, convert(char, received_import_expedition_invoice.tanggal, 103) as tanggal,"
            sqlcom = sqlcom + " received_import_expedition_invoice.id_expedition, received_import_expedition_invoice.invoice_no, "
            sqlcom = sqlcom + " convert(char, received_import_expedition_invoice.invoice_date, 103) as invoice_date,"
            sqlcom = sqlcom + " convert(char, received_import_expedition_invoice.tgl_jatuh_tempo, 103) as tgl_jatuh_tempo,"
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
            sqlcom = sqlcom + " penugasan_ekspedisi_impor.no_aju,"
            sqlcom = sqlcom + " isnull((select sum(isnull(jumlah,0)) from received_import_expedition_invoice_detail"
            sqlcom = sqlcom + " where id_invoice = received_import_expedition_invoice.id),0) as jumlah_nilai "
            sqlcom = sqlcom + " from received_import_expedition_invoice"
            sqlcom = sqlcom + " inner join daftar_expedition on daftar_expedition.id = received_import_expedition_invoice.id_expedition"
            sqlcom = sqlcom + " inner join penugasan_ekspedisi_impor on penugasan_ekspedisi_impor.seq = received_import_expedition_invoice.seq_penugasan_ekspedisi"
            sqlcom = sqlcom + " where received_import_expedition_invoice.tgl_jatuh_tempo <= '" & vtgl & "'"
            sqlcom = sqlcom + " and is_submit = 'S' and is_bayar = 'B'"

            If Me.dd_pilihan.SelectedValue = "0" Or Me.dd_pilihan.SelectedValue = "1" Then
                If String.IsNullOrEmpty(Me.tb_search.Text) Then
                    sqlcom = sqlcom
                Else
                    If Me.dd_pilihan.SelectedValue = "0" Then
                        sqlcom = sqlcom + " and upper(daftar_expedition.name) like upper('%" & Me.tb_search.Text & "%')"
                    Else
                        sqlcom = sqlcom + " and upper(received_import_expedition_invoice.invoice_no) like upper('%" & Me.tb_search.Text & "%')"
                    End If
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
            Me.tb_tanggal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.dd_pilihan.SelectedValue = "0"
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub dd_pilihan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_pilihan.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

End Class

