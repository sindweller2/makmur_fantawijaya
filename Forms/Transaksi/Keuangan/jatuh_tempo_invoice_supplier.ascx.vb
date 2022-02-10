Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_jatuh_tempo_invoice_supplier
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub checkdata()
        Dim vtgl As String = Me.tb_tanggal.Text.Substring(3, 2) & "/" & Me.tb_tanggal.Text.Substring(0, 2) & "/" & Me.tb_tanggal.Text.Substring(6, 4)

        sqlcom = "select *"
        sqlcom = sqlcom + " from entry_dokumen_impor"
        sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
        sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
        sqlcom = sqlcom + " where entry_dokumen_impor.tgl_jatuh_tempo_invoice_supplier <= '" & vtgl & "'"
        sqlcom = sqlcom + " and entry_dokumen_impor.tgl_bayar_invoice is null"
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

            sqlcom = "select entry_dokumen_impor.seq, entry_dokumen_impor.invoice_no, rtrim(convert(char, entry_dokumen_impor.tgl_invoice, 103)) as tgl_invoice,"
            sqlcom = sqlcom + " rtrim(convert(char, entry_dokumen_impor.tgl_terima, 103)) as tgl_terima,"
            sqlcom = sqlcom + " rtrim(convert(char, entry_dokumen_impor.tgl_jatuh_tempo_invoice_supplier, 103)) as tgl_jatuh_tempo_invoice_supplier,"
            sqlcom = sqlcom + " isnull(nilai_invoice,0) as nilai_invoice, daftar_supplier.name as nama_supplier, purchase_order.id_currency as mata_uang,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when entry_dokumen_impor.tgl_bayar_invoice is null then 'Belum bayar'"
            sqlcom = sqlcom + " when entry_dokumen_impor.tgl_bayar_invoice is not null then 'Sudah bayar'"
            sqlcom = sqlcom + " end as status_bayar,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when entry_dokumen_impor.is_submit_bayar = 'B' then 'Belum disubmit'"
            sqlcom = sqlcom + " when entry_dokumen_impor.is_submit_bayar = 'S' then 'Sudah disubmit'"
            sqlcom = sqlcom + " end as status_submit"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " where entry_dokumen_impor.tgl_jatuh_tempo_invoice_supplier <= '" & vtgl & "'"
            sqlcom = sqlcom + " and entry_dokumen_impor.tgl_bayar_invoice is null"

            If Me.dd_pilihan.SelectedValue = "0" Or Me.dd_pilihan.SelectedValue = "1" Then
                If String.IsNullOrEmpty(Me.tb_search.Text) Then
                    sqlcom = sqlcom
                Else
                    If Me.dd_pilihan.SelectedValue = "0" Then
                        sqlcom = sqlcom + " and upper(entry_dokumen_impor.invoice_no) like upper('%" & Me.tb_search.Text & "%')"
                    Else
                        sqlcom = sqlcom + " and upper(daftar_supplier.name) like upper('%" & Me.tb_search.Text & "%')"
                    End If
                End If
            End If

            sqlcom = sqlcom + " order by entry_dokumen_impor.seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "entry_dokumen_impor")
                Me.dg_data.DataSource = ds.Tables("entry_dokumen_impor").DefaultView

                If ds.Tables("entry_dokumen_impor").Rows.Count > 0 Then
                    If ds.Tables("entry_dokumen_impor").Rows.Count > 10 Then
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

