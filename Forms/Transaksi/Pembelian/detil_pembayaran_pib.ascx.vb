Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_pembayaran_lc
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

    Private ReadOnly Property vno_dokumen() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_dokumen")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode()
        sqlcom = "select name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_periode.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindbank()
        sqlcom = "select id, name from bank_account order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_bank.DataSource = reader
        Me.dd_bank.DataTextField = "name"
        Me.dd_bank.DataValueField = "id"
        Me.dd_bank.DataBind()
        Me.dd_bank.Items.Add(New ListItem("---Kas/Bank---", 0))
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.tb_bea_masuk.Text = ""
        Me.tb_ppn_impor.Text = ""
        Me.tb_pph_ps22.Text = ""
        Me.tb_biaya_adm_pib.Text = ""
        Me.tb_pnbp.Text = ""
        Me.tb_biaya_dokumen.Text = ""
        Me.tb_shipping_guarantee.Text = ""
    End Sub

    Sub loaddata()
        Try
            'data pembelian
            sqlcom = "select purchase_order.po_no_text, convert(char, purchase_order.tanggal, 103) as tanggal, purchase_order.id_currency,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when entry_dokumen_impor.is_lc = 'True' then (select no_lc from lc where seq = entry_dokumen_impor.seq_lc)"
            sqlcom = sqlcom + " when entry_dokumen_impor.is_lc = 'False' then '---'"
            sqlcom = sqlcom + " end as no_lc,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when entry_dokumen_impor.is_lc = 'True' then (select convert(char, tanggal_lc, 103) from lc where seq = entry_dokumen_impor.seq_lc)"
            sqlcom = sqlcom + " when entry_dokumen_impor.is_lc = 'False' then '---'"
            sqlcom = sqlcom + " end as tanggal_lc,"
            sqlcom = sqlcom + " daftar_supplier.name as nama_supplier,"
            sqlcom = sqlcom + " isnull((select sum(isnull(purchase_order_detil.qty,0) * "
            sqlcom = sqlcom + " (isnull(purchase_order_detil.unit_price,0) - "
            sqlcom = sqlcom + " (isnull(purchase_order_detil.unit_price,0) * (isnull(purchase_order_detil.discount,0)/100))))"
            sqlcom = sqlcom + " from purchase_order_detil"
            sqlcom = sqlcom + " where po_no = purchase_order.no group by po_no),0) as total_pembelian,"
            sqlcom = sqlcom + " transaction_period.name as nama_periode,"
            'data pembayara pib
            sqlcom = sqlcom + " bl_no, invoice_no, convert(char, tgl_invoice, 103) as tgl_invoice, isnull(nilai_invoice,0) as nilai_invoice, packing_list_no,"
            sqlcom = sqlcom + " convert(char, tanggal_bayar_pib, 103) as tgl_pembayaran, isnull(bea_masuk,0) as bea_masuk, isnull(ppn_import,0) as ppn_import, isnull(pph_ps22,0) as pph_ps22,"
            sqlcom = sqlcom + " isnull(biaya_adm_pib,0) as biaya_adm_pib, isnull(biaya_pnbp,0) as biaya_pnbp, isnull(biaya_dokumen,0) as biaya_dokumen,"
            sqlcom = sqlcom + " isnull(shipping_guarantee,0) as shipping_guarantee, id_bank"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " inner join transaction_period on transaction_period.id = purchase_order.id_transaction_period"
            sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vno_dokumen
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                'data pembelian
                Me.lbl_periode.Text = reader.Item("nama_periode").ToString
                Me.lbl_no_pembelian.Text = reader.Item("po_no_text").ToString
                Me.lbl_tgl_pembelian.Text = reader.Item("tanggal").ToString
                Me.lbl_mata_uang.Text = reader.Item("id_currency").ToString
                Me.lbl_total_nilai_pembelian.Text = FormatNumber(reader.Item("total_pembelian").ToString, 2)
                Me.lbl_nama_supplier.Text = reader.Item("nama_supplier").ToString
                Me.lbl_no_lc.Text = reader.Item("no_lc").ToString
                Me.lbl_tgl_lc.Text = reader.Item("tanggal_lc").ToString

                'data pembayaran_pib
                Me.lbl_no_dokumen.Text = Me.vno_dokumen
                Me.lbl_no_bl.Text = reader.Item("bl_no").ToString
                Me.lbl_no_packing_list.Text = reader.Item("packing_list_no").ToString
                Me.lbl_no_invoice.Text = reader.Item("invoice_no").ToString
                Me.lbl_tgl_invoice.Text = reader.Item("tgl_invoice").ToString
                Me.lbl_nilai_invoice.Text = FormatNumber(reader.Item("nilai_invoice").ToString, 2)
                Me.lbl_tgl_pembayaran.Text = reader.Item("tgl_pembayaran").ToString
                Me.tb_bea_masuk.Text = FormatNumber(reader.Item("bea_masuk").ToString)
                Me.tb_ppn_impor.Text = FormatNumber(reader.Item("ppn_import").ToString)
                Me.tb_pph_ps22.Text = FormatNumber(reader.Item("pph_ps22").ToString)
                Me.tb_biaya_adm_pib.Text = FormatNumber(reader.Item("biaya_adm_pib").ToString)
                Me.tb_pnbp.Text = FormatNumber(reader.Item("biaya_pnbp").ToString)
                Me.tb_biaya_dokumen.Text = FormatNumber(reader.Item("biaya_dokumen").ToString)
                Me.tb_shipping_guarantee.Text = FormatNumber(reader.Item("shipping_guarantee").ToString)

                If String.IsNullOrEmpty(reader.Item("id_bank").ToString) Then
                    Me.dd_bank.SelectedValue = 0
                    Me.btn_save.Enabled = True
                Else
                    Me.dd_bank.SelectedValue = reader.Item("id_bank").ToString
                    Me.btn_save.Enabled = False
                End If

            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.bindperiode()
            Me.bindbank()
            Me.loaddata()
            Me.tbl_produk.Visible = False
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/pembayaran_biaya_pib_purchasing.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_bea_masuk.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi Bea Masuk terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_ppn_impor.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi PPN Impor terlebih dahulu"
            Else
                sqlcom = "update entry_dokumen_impor"
                sqlcom = sqlcom + " set bea_masuk = " & Decimal.ToDouble(Me.tb_bea_masuk.Text) & ","
                sqlcom = sqlcom + " ppn_import = " & Decimal.ToDouble(Me.tb_ppn_impor.Text) & ","
                sqlcom = sqlcom + " pph_ps22 = " & IIf(String.IsNullOrEmpty(Me.tb_pph_ps22.Text), 0, Decimal.ToDouble(Me.tb_pph_ps22.Text)) & ","
                sqlcom = sqlcom + " biaya_adm_pib = " & IIf(String.IsNullOrEmpty(Me.tb_biaya_adm_pib.Text), 0, Decimal.ToDouble(Me.tb_biaya_adm_pib.Text)) & ","
                sqlcom = sqlcom + " biaya_pnbp = " & IIf(String.IsNullOrEmpty(Me.tb_pnbp.Text), 0, Decimal.ToDouble(Me.tb_pnbp.Text)) & ","
                sqlcom = sqlcom + " biaya_dokumen = " & IIf(String.IsNullOrEmpty(Me.tb_biaya_dokumen.Text), 0, Decimal.ToDouble(Me.tb_biaya_dokumen.Text)) & ","
                sqlcom = sqlcom + " shipping_guarantee = " & IIf(String.IsNullOrEmpty(Me.tb_shipping_guarantee.Text), 0, Decimal.ToDouble(Me.tb_shipping_guarantee.Text))
                sqlcom = sqlcom + " where seq = " & Me.vno_dokumen
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_hitung_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_hitung.Click
        Try
            Dim vppn As Decimal = 0
            Dim vbbm As Decimal = 0

            sqlcom = "select sum(isnull(qty,0) * (isnull(unit_price,0)  - isnull(unit_price,0) * isnull(discount,0) /100) * isnull(product_item.ppn_tax,0)/100) as ppn_tax, "
            sqlcom = sqlcom + " sum(isnull(qty,0) * (isnull(unit_price,0)  - isnull(unit_price,0) * isnull(discount,0) /100) * isnull(product_item.bbm_tax,0)/100) as bbm_tax"
            sqlcom = sqlcom + " from entry_dokumen_impor_produk"
            sqlcom = sqlcom + " inner join product_item on product_item.id = entry_dokumen_impor_produk.id_product"
            sqlcom = sqlcom + " where seq_entry = " & Me.vno_dokumen
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_ppn_impor.Text = FormatNumber(reader.Item("ppn_tax").ToString)
                Me.tb_bea_masuk.Text = FormatNumber(reader.Item("bbm_tax").ToString)
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

            sqlcom = "select id_product, nama_product, isnull(qty,0) as qty, isnull(unit_price,0) as unit_price,"
            sqlcom = sqlcom + " isnull(discount,0) as discount,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + packaging.name + '/' + measurement_unit.name as packaging,"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + " when product_item.is_packaging = 'P' then measurement_unit.name"
            sqlcom = sqlcom + " when product_item.is_packaging = 'Q' then packaging.name"
            sqlcom = sqlcom + " end as satuan_produk, "
            sqlcom = sqlcom + " isnull(isnull(entry_dokumen_impor_produk.qty,0) * (isnull(entry_dokumen_impor_produk.unit_price,0) - "
            sqlcom = sqlcom + " isnull(entry_dokumen_impor_produk.unit_price, 0) * isnull(entry_dokumen_impor_produk.discount,0) /100),0) as sub_total"
            sqlcom = sqlcom + " from entry_dokumen_impor_produk"
            sqlcom = sqlcom + " inner join product_item on product_item.id = entry_dokumen_impor_produk.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where entry_dokumen_impor_produk.seq_entry = " & Me.vno_dokumen
            sqlcom = sqlcom + " order by entry_dokumen_impor_produk.nama_product"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "entry_dokumen_impor_produk")
                Me.dg_data.DataSource = ds.Tables("entry_dokumen_impor_produk").DefaultView

                If ds.Tables("entry_dokumen_impor_produk").Rows.Count > 0 Then
                    If ds.Tables("entry_dokumen_impor_produk").Rows.Count > 10 Then
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

    Protected Sub btn_produk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_produk.Click
        Me.loadgrid()
        Me.tbl_produk.Visible = True
    End Sub
End Class
