Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_hpp
    Inherits System.Web.UI.UserControl

    Public Property vis_lc() As String
        Get
            Dim o As Object = ViewState("vis_lc")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vis_lc") = value
        End Set
    End Property

    Public Property vpo_no() As Integer
        Get
            Dim o As Object = ViewState("vpo_no")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vpo_no") = value
        End Set
    End Property

    Public Property vseq() As Integer
        Get
            Dim o As Object = ViewState("vseq")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vseq") = value
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

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, bl_no, purchase_order.id_currency as mata_uang, no_po, is_lc,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when purchase_order.is_lc = 1 then 'L/C. ' + purchase_order.no_lc"
            sqlcom = sqlcom + " when purchase_order.is_lc = 0 then 'Inv. ' + invoice_no"
            sqlcom = sqlcom + " end as invoice_no,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when purchase_order.is_lc = 1 then convert(char, purchase_order.tanggal_lc, 103)"
            sqlcom = sqlcom + " when purchase_order.is_lc = 0 then convert(char, tgl_invoice, 103)"
            sqlcom = sqlcom + " end as tgl_invoice,"
            sqlcom = sqlcom + " isnull(nilai_invoice,0) as nilai_invoice, packing_list_no"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
            sqlcom = sqlcom + " where month(tanggal_bayar_pib) = (select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue & ")"
            sqlcom = sqlcom + " and year(tanggal_bayar_pib) = " & Me.tb_tahun.Text
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
            Me.tb_tahun.Text = Year(Now.Date)
            Me.bindperiode_transaksi()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkPIB" Then
            Me.vpo_no = CType(e.Item.FindControl("lbl_po_no"), Label).Text
            Me.vseq = CType(e.Item.FindControl("lbl_seq"), Label).Text

            If CType(e.Item.FindControl("lbl_is_lc"), Label).Text = "True" Then
                Me.vis_lc = "Y"
            Else
                Me.vis_lc = "N"
            End If
            Me.print()
        End If
    End Sub

    Sub hitung_hpp()
        Try
            sqlcom = "delete hitungan_hpp"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and seq = " & Me.vseq
            connection.koneksi.DeleteRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 1 as no, '' as tanggal, '1. PEMBELIAN DEVISA' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total, " & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 2 as no, convert(char, tanggal_bayar, 103) as tanggal, '' as judul, purchase_order.id_currency as mata_uang, "
            sqlcom = sqlcom + " pembayaran_lc.jumlah_nilai as harga_beli, 'x Rp. ' as rupiah_text, "
            sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " pembayaran_lc.jumlah_nilai * pembayaran_lc.kurs as total_rupiah, 'Rp. ' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 2 as no, '' as tanggal, '' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, 'Rp. ' as rupiah_text_sub_total, sum(pembayaran_lc.jumlah_nilai * pembayaran_lc.kurs) as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " group by po_no"
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 3 as no, '' as tanggal, '2. BIAYA L/C' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)


            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 4 as no, convert(char, tgl_bayar_biaya_lc, 103) as tanggal, 'KOMISI BANK' as judul, purchase_order.id_currency as mata_uang, "
            sqlcom = sqlcom + " purchase_order.biaya_komisi_bank as harga_beli, 'x Rp. ' as rupiah_text, "
            sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " purchase_order.biaya_komisi_bank * pembayaran_lc.kurs as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 5 as no, convert(char, tgl_bayar_biaya_lc, 103) as tanggal, 'ONGKOS KAWAT' as judul, purchase_order.id_currency as mata_uang, "
            sqlcom = sqlcom + " purchase_order.biaya_ongkos_kawat as harga_beli, 'x Rp. ' as rupiah_text, "
            sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " purchase_order.biaya_ongkos_kawat * pembayaran_lc.kurs as total_rupiah, '' as rupiah_text_sub_total,0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 6 as no, convert(char, tgl_bayar_biaya_lc, 103) as tanggal, 'PORTO MATERAI' as judul, purchase_order.id_currency as mata_uang, "
            sqlcom = sqlcom + " purchase_order.biaya_porto_materai as harga_beli, 'x Rp. ' as rupiah_text, "
            sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " purchase_order.biaya_porto_materai * pembayaran_lc.kurs as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 7 as no, convert(char, tgl_bayar_biaya_lc, 103) as tanggal, 'BIAYA COURIER' as judul, purchase_order.id_currency as mata_uang, "
            sqlcom = sqlcom + " purchase_order.biaya_courier as harga_beli, 'x Rp. ' as rupiah_text, "
            sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " purchase_order.biaya_courier * pembayaran_lc.kurs as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 8 as no, convert(char, tgl_bayar_biaya_lc, 103) as tanggal, 'BIAYA L/C AMENDMENT' as judul, purchase_order.id_currency as mata_uang, "
            sqlcom = sqlcom + " purchase_order.biaya_lc_amendment as harga_beli, 'x Rp. ' as rupiah_text, "
            sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " purchase_order.biaya_lc_amendment * pembayaran_lc.kurs as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 9 as no, '' as tanggal, '' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, 'Rp. ' as rupiah_text_sub_total, "
            sqlcom = sqlcom + " sum((purchase_order.biaya_komisi_bank + purchase_order.biaya_ongkos_kawat + purchase_order.biaya_porto_materai + purchase_order.biaya_courier + purchase_order.biaya_lc_amendment) * pembayaran_lc.kurs) as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
            sqlcom = sqlcom + " group by po_no"
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 10 as no, '' as tanggal, '3. PAJAK IMPORT' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 11 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'BEA MASUK' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.bea_masuk as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 12 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'PPN IMPORT' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.ppn_import as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 13 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'PPH PS.22' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.pph_ps22 as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 9 as no, '' as tanggal, '' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, 'Rp. ' as rupiah_text_sub_total, "
            sqlcom = sqlcom + " sum(isnull(entry_dokumen_impor.bea_masuk,0)) as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 12 as no, '' as tanggal, '4. BIAYA LAIN-LAIN' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 13 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'BIAYA ADM. PIB' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.biaya_adm_pib as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 14 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'PNBP' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.biaya_pnbp as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 15 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'BIAYA DOKUMEN' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.biaya_dokumen as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 16 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'SHIPPING GUARANTEE' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.shipping_guarantee as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 17 as no, '' as tanggal, '' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, 'Rp. ' as rupiah_text_sub_total, "
            sqlcom = sqlcom + " sum(entry_dokumen_impor.biaya_adm_pib + entry_dokumen_impor.biaya_pnbp + entry_dokumen_impor.biaya_dokumen + entry_dokumen_impor.shipping_guarantee) as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 18 as no, '' as tanggal, '5. BIAYA EKSPEDISI' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 19 as no, convert(char, received_import_expedition_invoice.tanggal, 103) as tanggal, "
            sqlcom = sqlcom + " received_import_expedition_invoice_detail.description as judul, "
            sqlcom = sqlcom + " '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " isnull(jumlah,0) as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from received_import_expedition_invoice"
            sqlcom = sqlcom + " inner join received_import_expedition_invoice_detail on received_import_expedition_invoice_detail.id_invoice = received_import_expedition_invoice.id "
            sqlcom = sqlcom + " inner join penugasan_ekspedisi_impor_detil on penugasan_ekspedisi_impor_detil.seq_penugasan = received_import_expedition_invoice.seq_penugasan_ekspedisi"
            sqlcom = sqlcom + " where penugasan_ekspedisi_impor_detil.seq_entry_dokumen_impor = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 20 as no, '' as tanggal, '' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, 'Rp.' as rupiah_text_sub_total, isnull(sum(isnull(jumlah,0)),0) as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from received_import_expedition_invoice"
            sqlcom = sqlcom + " inner join received_import_expedition_invoice_detail on received_import_expedition_invoice_detail.id_invoice = received_import_expedition_invoice.id "
            sqlcom = sqlcom + " inner join penugasan_ekspedisi_impor_detil on penugasan_ekspedisi_impor_detil.seq_penugasan = received_import_expedition_invoice.seq_penugasan_ekspedisi"
            sqlcom = sqlcom + " where penugasan_ekspedisi_impor_detil.seq_entry_dokumen_impor = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 21 as no, '' as tanggal, '' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'TOTAL' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, 'Rp.' as rupiah_text_sub_total, "
            sqlcom = sqlcom + " (select sum(pembayaran_lc.jumlah_nilai * pembayaran_lc.kurs) "
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " group by po_no) +"
            sqlcom = sqlcom + " (select sum((purchase_order.biaya_komisi_bank + purchase_order.biaya_ongkos_kawat + purchase_order.biaya_porto_materai + purchase_order.biaya_courier + purchase_order.biaya_lc_amendment) * pembayaran_lc.kurs)"
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
            sqlcom = sqlcom + " group by po_no) +"
            sqlcom = sqlcom + " (select sum(entry_dokumen_impor.bea_masuk)"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq & ")+"
            sqlcom = sqlcom + " (select sum(entry_dokumen_impor.biaya_adm_pib + entry_dokumen_impor.biaya_pnbp + entry_dokumen_impor.biaya_dokumen + entry_dokumen_impor.shipping_guarantee)"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq & ")+"
            sqlcom = sqlcom + " (select isnull(sum(isnull(jumlah,0)),0)"
            sqlcom = sqlcom + " from received_import_expedition_invoice"
            sqlcom = sqlcom + " inner join received_import_expedition_invoice_detail on received_import_expedition_invoice_detail.id_invoice = received_import_expedition_invoice.id "
            sqlcom = sqlcom + " inner join penugasan_ekspedisi_impor_detil on penugasan_ekspedisi_impor_detil.seq_penugasan = received_import_expedition_invoice.seq_penugasan_ekspedisi"
            sqlcom = sqlcom + " where penugasan_ekspedisi_impor_detil.seq_entry_dokumen_impor = " & Me.vseq & ")"
            sqlcom = sqlcom + " as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 22 as no, '-------' as tanggal, '' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            Dim vtotal As Decimal = 0

            sqlcom = "select sub_total from hitungan_hpp"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and seq = " & Me.vseq
            sqlcom = sqlcom + " and rupiah_text_total = 'TOTAL'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                vtotal = Decimal.ToDouble(reader.Item("sub_total").ToString)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            Dim vtotal_product As Decimal = 0
            sqlcom = "select isnull(sum(isnull(qty,0) * (isnull(unit_price,0) - (isnull(unit_price,0) * isnull(discount,0) /100))),0) as total_product"
            sqlcom = sqlcom + " from entry_dokumen_impor_produk x"
            sqlcom = sqlcom + " where seq_entry = " & Me.vseq
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                vtotal_product = Decimal.ToDouble(reader.Item("total_product").ToString)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            Dim vcount As Integer = 0
            sqlcom = "select count(*) as vcount from entry_dokumen_impor_produk"
            sqlcom = sqlcom + " where seq_entry = " & Me.vseq
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                vcount = reader.Item("vcount").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If vcount > 1 Then
                'product < max
                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 23 as no, '' as tanggal, product_item.nama_beli as judul,"
                sqlcom = sqlcom + " '' as mata_uang, isnull(qty,0) as harga_beli, 'x' as rupiah_text,"
                sqlcom = sqlcom + " round(" & Math.Round(vtotal, 2) / Math.Round(vtotal_product)
                sqlcom = sqlcom + " *"
                sqlcom = sqlcom + " isnull(unit_price,0) - (isnull(unit_price,0) * isnull(discount,0) /100), 2, 1)"
                sqlcom = sqlcom + " as kurs,"
                sqlcom = sqlcom + " '' as rupiah_text_total,"
                sqlcom = sqlcom + " 0 as total,"
                sqlcom = sqlcom + " 'Rp. ' as rupiah_text_sub_total, "
                sqlcom = sqlcom + " round(" & Math.Round(vtotal, 2) / Math.Round(vtotal_product)
                sqlcom = sqlcom + " *"
                sqlcom = sqlcom + " isnull(unit_price,0) - (isnull(unit_price,0) * isnull(discount,0) /100), 2, 1)"
                sqlcom = sqlcom + " * "
                sqlcom = sqlcom + " isnull(qty,0)"
                sqlcom = sqlcom + " as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from entry_dokumen_impor_produk"
                sqlcom = sqlcom + " inner join product_item on product_item.id = entry_dokumen_impor_produk.id_product"
                sqlcom = sqlcom + " where seq_entry = " & Me.vseq
                sqlcom = sqlcom + " and seq < (select max(x.seq) from entry_dokumen_impor_produk x where x.seq_entry = " & Me.vseq & ")"
                connection.koneksi.InsertRecord(sqlcom)

                Dim vtotal_product_hasil As Decimal = 0

                sqlcom = "select sum(round(" & Math.Round(vtotal, 2) / Math.Round(vtotal_product)
                sqlcom = sqlcom + " *"
                sqlcom = sqlcom + " isnull(unit_price,0) - (isnull(unit_price,0) * isnull(discount,0) /100), 2, 1)"
                sqlcom = sqlcom + " * "
                sqlcom = sqlcom + " isnull(qty,0))"
                sqlcom = sqlcom + " as sub_total"
                sqlcom = sqlcom + " from entry_dokumen_impor_produk"
                sqlcom = sqlcom + " inner join product_item on product_item.id = entry_dokumen_impor_produk.id_product"
                sqlcom = sqlcom + " where seq_entry = " & Me.vseq
                sqlcom = sqlcom + " and seq < (select max(x.seq) from entry_dokumen_impor_produk x where x.seq_entry = " & Me.vseq & ")"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vtotal_product_hasil = Decimal.ToDouble(reader.Item("sub_total").ToString)
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vqty As Decimal = 0
                sqlcom = "select isnull(qty,0) as qty"
                sqlcom = sqlcom + " from entry_dokumen_impor_produk"
                sqlcom = sqlcom + " where seq_entry = " & Me.vseq
                sqlcom = sqlcom + " and seq = (select max(x.seq) from entry_dokumen_impor_produk x where x.seq_entry = " & Me.vseq & ")"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vqty = Decimal.ToDouble(reader.Item("qty").ToString)
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                'product(terakhir)
                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 23 as no, '' as tanggal, product_item.nama_beli as judul,"
                sqlcom = sqlcom + " '' as mata_uang, round(qty,2,1) as harga_beli, 'x' as rupiah_text,"
                sqlcom = sqlcom + " round("
                sqlcom = sqlcom + " " & (Math.Round(vtotal, 2) - Math.Round(vtotal_product_hasil, 2)) & " / qty , 2, 1) as kurs,"
                sqlcom = sqlcom + " '' as rupiah_text_total,"
                sqlcom = sqlcom + " 0 as total,"
                sqlcom = sqlcom + " 'Rp. ' as rupiah_text_sub_total," & Math.Round(vtotal, 2) - Math.Round(vtotal_product_hasil, 2) & " as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from entry_dokumen_impor_produk"
                sqlcom = sqlcom + " inner join product_item on product_item.id = entry_dokumen_impor_produk.id_product"
                sqlcom = sqlcom + " where seq_entry = " & Me.vseq
                sqlcom = sqlcom + " and seq = (select max(x.seq) from entry_dokumen_impor_produk x where x.seq_entry = " & Me.vseq & ")"
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 22 as no, '' as tanggal, '' as judul, '' as mata_uang, "
                sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
                sqlcom = sqlcom + " 0 as kurs, 'TOTAL' as rupiah_text_total, "
                sqlcom = sqlcom + " 0 as total_rupiah, '' as rupiah_text_sub_total," & Math.Round(vtotal, 2) & " as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                connection.koneksi.InsertRecord(sqlcom)
            End If
            

            Me.lbl_msg.Text = "Perhitungan sudah selesai"
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub


    Sub print()
        Try

            Me.hitung_hpp()

            Dim reportPath As String = Nothing
            If Me.vis_lc = "Y" Then
                reportPath = Server.MapPath("reports\hpp_lc.rpt")
            ElseIf Me.vis_lc = "N" Then
                reportPath = Server.MapPath("reports\hpp_invoice.rpt")
            End If
            Me.CrystalReportSource1.Report.FileName = reportPath
            Me.CrystalReportSource1.ReportDocument.Close()
            Me.CrystalReportSource1.ReportDocument.Refresh()
            Dim oExO As CrystalDecisions.Shared.ExportOptions
            Dim oExDo As New CrystalDecisions.Shared.DiskFileDestinationOptions()
            Dim con As New System.Data.SqlClient.SqlConnectionStringBuilder

            con.ConnectionString = ConfigurationManager.ConnectionStrings("trading").ToString
            Dim consinfo As New CrystalDecisions.Shared.ConnectionInfo
            consinfo.ServerName = con.DataSource
            consinfo.UserID = con.UserID
            consinfo.DatabaseName = con.InitialCatalog
            consinfo.Password = con.Password
            consinfo.Type = CrystalDecisions.Shared.ConnectionInfoType.SQL
            Dim oRD As CrystalDecisions.CrystalReports.Engine.ReportDocument = Me.CrystalReportSource1.ReportDocument
            Dim myTables As CrystalDecisions.CrystalReports.Engine.Tables = oRD.Database.Tables
            For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                Dim myTableLogonInfo As CrystalDecisions.Shared.TableLogOnInfo = myTable.LogOnInfo
                myTableLogonInfo.ConnectionInfo = consinfo
                myTable.ApplyLogOnInfo(myTableLogonInfo)
            Next
            oRD.Load(reportPath)
            oRD.SetParameterValue("po_no", Me.vpo_no)
            oRD.SetParameterValue("seq", Me.vseq)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/hpp.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/hpp.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
