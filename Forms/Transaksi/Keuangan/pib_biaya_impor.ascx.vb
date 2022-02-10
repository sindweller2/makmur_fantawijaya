Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_pib_biaya_impor
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

    Private ReadOnly Property vno_po() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_po")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub loaddata()
        Try
            sqlcom = "select po_no_text, convert(char, tanggal, 103) as tanggal, id_currency,"
            sqlcom = sqlcom + " no_lc, convert(char, tanggal_lc, 103) as tanggal_lc, convert(char, tgl_berlaku_lc, 103) as tgl_berlaku_lc,"
            sqlcom = sqlcom + " id_lc_type, convert(char, due_date_lc, 103) as due_date_lc, id_negara_koresponden, id_dikapalkan_dari,"
            sqlcom = sqlcom + " id_pelabuhan_tujuan, id_negara_asal, daftar_supplier.name as nama_supplier,"
            sqlcom = sqlcom + " isnull((select sum(isnull(purchase_order_detil.qty,0) * "
            sqlcom = sqlcom + " (isnull(purchase_order_detil.unit_price,0) - "
            sqlcom = sqlcom + " (isnull(purchase_order_detil.unit_price,0) * (isnull(purchase_order_detil.discount,0)/100))))"
            sqlcom = sqlcom + " from purchase_order_detil"
            sqlcom = sqlcom + " where po_no = purchase_order.no group by po_no),0) as total_pembelian, transaction_period.name as nama_periode"
            sqlcom = sqlcom + " from purchase_order"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " inner join transaction_period on transaction_period.id = purchase_order.id_transaction_period"
            sqlcom = sqlcom + " where no = " & Me.vno_po
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_periode.Text = reader.Item("nama_periode").ToString
                Me.lbl_no_pembelian.Text = reader.Item("po_no_text").ToString
                Me.lbl_tgl_pembelian.Text = reader.Item("tanggal").ToString
                Me.lbl_mata_uang.Text = reader.Item("id_currency").ToString
                Me.lbl_total_nilai_pembelian.Text = FormatNumber(reader.Item("total_pembelian").ToString, 2)
                Me.lbl_nama_supplier.Text = reader.Item("nama_supplier").ToString
                Me.lbl_no_lc.Text = reader.Item("no_lc").ToString
                Me.lbl_tgl_lc.Text = reader.Item("tanggal_lc").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, bl_no, invoice_no, convert(char, tgl_invoice, 103) as tgl_invoice,"
            sqlcom = sqlcom + " isnull(nilai_invoice,0) as nilai_invoice, packing_list_no"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where no_po = " & Me.vno_po
            sqlcom = sqlcom + " order by seq"

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
            Me.loaddata()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/pembayaran_biaya_pib.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkPIB" Then
            Response.Redirect("~/detil_pembayaran_pib.aspx?vno_po=" & Me.vno_po & "&vseq=" & CType(e.Item.FindControl("lbl_seq"), Label).Text)
        End If
    End Sub
End Class
