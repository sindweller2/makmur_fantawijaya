Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_hpp
    Inherits System.Web.UI.UserControl

    Public tradingClass As tradingClass = New tradingClass()

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

    'Daniel
    Public Property kurs_bulanan() As String
        Get
            Dim o As Object = ViewState("kurs_bulanan")
            If Not o Is Nothing Then Return CStr(o) Else Return 0
        End Get
        Set(ByVal value As String)
            ViewState("kurs_bulanan") = value
        End Set
    End Property
    'Daniel

    Public Property kurs_akhir_bulan() As Decimal
        Get
            Dim o As Object = ViewState("kurs_akhir_bulan")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("kurs_akhir_bulan") = value
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

            sqlcom = "select entry_dokumen_impor.seq, entry_dokumen_impor.bl_no, purchase_order.id_currency as mata_uang,"
            sqlcom = sqlcom + " entry_dokumen_impor.no_po, entry_dokumen_impor.is_lc, entry_dokumen_impor.dibuat_oleh, entry_dokumen_impor.disetujui_oleh,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when purchase_order.is_lc = 1 then 'L/C. ' + (select no_lc from lc where seq = entry_dokumen_impor.seq_lc)"
            sqlcom = sqlcom + " when purchase_order.is_lc = 0 then 'Inv. ' + entry_dokumen_impor.invoice_no"
            sqlcom = sqlcom + " end as invoice_no,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when purchase_order.is_lc = 1 then (select convert(char, lc.tanggal_lc, 103) from lc where seq = entry_dokumen_impor.seq_lc)"
            sqlcom = sqlcom + " when purchase_order.is_lc = 0 then convert(char, entry_dokumen_impor.tgl_invoice, 103)"
            sqlcom = sqlcom + " end as tgl_invoice,"
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.nilai_invoice,0) as nilai_invoice, packing_list_no"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
            sqlcom = sqlcom + " where month(entry_dokumen_impor.tanggal_bayar_pib) = (select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue & ")"
            sqlcom = sqlcom + " and year(entry_dokumen_impor.tanggal_bayar_pib) = " & Me.tb_tahun.Text

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and (entry_dokumen_impor.bl_no like '%" & Me.tb_search.Text.Trim & "%' or invoice_no like '%" & Me.tb_search.Text.Trim & "%' or packing_list_no like '%" & Me.tb_search.Text & "%')"
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
                    Me.btn_update.visible = True

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select code, nama_pegawai from user_list where code <> 1 order by nama_pegawai"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_dibuat_oleh"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_dibuat_oleh"), DropDownList).DataTextField = "nama_pegawai"
                        CType(Me.dg_data.Items(x).FindControl("dd_dibuat_oleh"), DropDownList).DataValueField = "code"
                        CType(Me.dg_data.Items(x).FindControl("dd_dibuat_oleh"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_dibuat_oleh"), DropDownList).Items.Add(New ListItem("--- Nama pegawai ---", 0))
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_dibuat_oleh"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("dd_dibuat_oleh"), DropDownList).SelectedValue = 0
                        Else
                            CType(Me.dg_data.Items(x).FindControl("dd_dibuat_oleh"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_dibuat_oleh"), Label).Text
                        End If


                        sqlcom = "select code, nama_pegawai from user_list where code <> 1  order by nama_pegawai"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_disetujui_oleh"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_disetujui_oleh"), DropDownList).DataTextField = "nama_pegawai"
                        CType(Me.dg_data.Items(x).FindControl("dd_disetujui_oleh"), DropDownList).DataValueField = "code"
                        CType(Me.dg_data.Items(x).FindControl("dd_disetujui_oleh"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_disetujui_oleh"), DropDownList).Items.Add(New ListItem("--- Nama pegawai ---", 0))
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_disetujui_oleh"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("dd_disetujui_oleh"), DropDownList).SelectedValue = 0
                        Else
                            CType(Me.dg_data.Items(x).FindControl("dd_disetujui_oleh"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_disetujui_oleh"), Label).Text
                        End If
                    Next
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.visible = False
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
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkHPP" Then


            If CType(e.Item.FindControl("dd_dibuat_oleh"), DropDownList).SelectedValue <> 0 And CType(e.Item.FindControl("dd_disetujui_oleh"), DropDownList).SelectedValue <> 0 Then
                Me.vpo_no = CType(e.Item.FindControl("lbl_po_no"), Label).Text
                Me.vseq = CType(e.Item.FindControl("lbl_seq"), Label).Text
                'Daniel
                Me.kurs_bulanan = CType(e.Item.FindControl("tb_mata_uang"), Label).Text

                If Me.kurs_bulanan = "EURO" Then
                    Me.kurs_bulanan = "EUR"
                ElseIf Me.kurs_bulanan = "USD" Then
                    Me.kurs_bulanan = "kurs_bulanan"
                End If
                'Daniel
                If CType(e.Item.FindControl("lbl_is_lc"), Label).Text = "True" Then
                    Me.vis_lc = "Y"
                Else
                    Me.vis_lc = "N"
                End If
                Me.print()
            Else
                tradingClass.Alert("Silakan dilengkapi data nya", Me.Page)
            End If

        End If
    End Sub

    Sub insert_hpp()
        Try
            'cek data
            sqlcom = "select *"
            sqlcom = sqlcom + " from harga_pokok_pembelian_produk"
            sqlcom = sqlcom + " where seq_dokumen_impor = " & Me.vseq
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                sqlcom = "delete harga_pokok_pembelian_produk"
                sqlcom = sqlcom + " where seq_dokumen_impor = " & Me.vseq
                connection.koneksi.DeleteRecord(sqlcom)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            sqlcom = "select product_item.id as product_id, isnull(hitungan_hpp.kurs,0) as kurs"
            sqlcom = sqlcom + " from entry_dokumen_impor_produk"
            sqlcom = sqlcom + " inner join product_item on product_item.id = entry_dokumen_impor_produk.id_product"
            sqlcom = sqlcom + " inner join hitungan_hpp on hitungan_hpp.seq = entry_dokumen_impor_produk.seq_entry and hitungan_hpp.judul = product_item.nama_beli"
            sqlcom = sqlcom + " where seq_entry = " & Me.vseq
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                Dim readermax As SqlClient.SqlDataReader
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(isnull(seq,0)),0) + 1 as vmax"
                sqlcom = sqlcom + " from harga_pokok_pembelian_produk"
                sqlcom = sqlcom + " where id_produk = " & reader.Item("product_id").ToString
                readermax = connection.koneksi.SelectRecord(sqlcom)
                readermax.Read()
                If readermax.HasRows Then
                    vmax = readermax.Item("vmax").ToString
                End If
                readermax.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into harga_pokok_pembelian_produk(id_transaction_period, id_produk, seq, hpp, seq_dokumen_impor)"
                sqlcom = sqlcom + " values(" & Me.dd_bulan.SelectedValue & "," & reader.Item("product_id").ToString & "," & vmax
                sqlcom = sqlcom + "," & Decimal.ToDouble(reader.Item("kurs").ToString) & "," & Me.vseq & ")"
                connection.koneksi.InsertRecord(sqlcom)
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub hitung_hpp()
        Try
            sqlcom = "delete hitungan_hpp"
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and seq = " & Me.vseq
            connection.koneksi.DeleteRecord(sqlcom)

            'Daniel
            sqlcom = "select isnull(" & Me.kurs_bulanan & ",0) as kurs_bulanan"
            'Daniel
            sqlcom = sqlcom + " from transaction_period"
            sqlcom = sqlcom + " where id = " & Me.dd_bulan.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.kurs_akhir_bulan = reader.Item("kurs_bulanan").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 1 as no, '' as tanggal, '1. PEMBELIAN DEVISA' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total, " & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            If Me.vis_lc = "Y" Then
                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 2 as no, convert(char, tanggal_bayar, 103) as tanggal, '' as judul, purchase_order.id_currency as mata_uang, "
                sqlcom = sqlcom + " pembayaran_lc.jumlah_nilai as harga_beli, 'x Rp. ' as rupiah_text, "
                sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " pembayaran_lc.jumlah_nilai * pembayaran_lc.kurs as total_rupiah, 'Rp. ' as rupiah_text_sub_total, 0 as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from pembayaran_lc"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq_lc = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 3 as no, '' as tanggal, '' as judul, '' as mata_uang, "
                sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
                sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " 0 as total_rupiah, 'Rp. ' as rupiah_text_sub_total, sum(pembayaran_lc.jumlah_nilai * pembayaran_lc.kurs) as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from pembayaran_lc"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq_lc = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                sqlcom = sqlcom + " group by po_no"
                connection.koneksi.InsertRecord(sqlcom)

            Else

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 2 as no, convert(char, entry_dokumen_impor.tgl_terima, 103) as tanggal, '' as judul, purchase_order.id_currency as mata_uang, "
                sqlcom = sqlcom + " entry_dokumen_impor.nilai_invoice as harga_beli, 'x Rp. ' as rupiah_text "
                sqlcom = sqlcom + "," & Me.kurs_akhir_bulan & " as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " entry_dokumen_impor.nilai_invoice * " & Me.kurs_akhir_bulan & " as total_rupiah, 'Rp. ' as rupiah_text_sub_total, 0 as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from entry_dokumen_impor"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                connection.koneksi.InsertRecord(sqlcom)

                '--Modify by : Adien / 11-12-2013
                'sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                'sqlcom = sqlcom + " sub_total, po_no, seq)"
                'sqlcom = sqlcom + " select 2 as no, convert(char, entry_dokumen_impor.tgl_terima, 103) as tanggal, '' as judul, purchase_order.id_currency as mata_uang, "
                'sqlcom = sqlcom + " entry_dokumen_impor.nilai_invoice as harga_beli, 'x Rp. ' as rupiah_text "
                'sqlcom = sqlcom + ",entry_dokumen_impor.kurs, 'Rp. ' as rupiah_text_total, "
                'sqlcom = sqlcom + " entry_dokumen_impor.nilai_invoice * " & Me.kurs_akhir_bulan & " as total_rupiah, 'Rp. ' as rupiah_text_sub_total, 0 as sub_total"
                'sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                'sqlcom = sqlcom + " from entry_dokumen_impor"
                'sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
                'sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                'connection.koneksi.InsertRecord(sqlcom)

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 3 as no, '' as tanggal, '' as judul, '' as mata_uang, "
                sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
                sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " 0 as total_rupiah, 'Rp. ' as rupiah_text_sub_total, sum(entry_dokumen_impor.nilai_invoice * " & Me.kurs_akhir_bulan & ") as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from entry_dokumen_impor"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                connection.koneksi.InsertRecord(sqlcom)

            End If

            If Me.vis_lc = "Y" Then
                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 4 as no, '' as tanggal, '2. BIAYA L/C' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
                sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 5 as no, convert(char, lc.tgl_bayar_biaya_lc, 103) as tanggal, 'KOMISI BANK' as judul, purchase_order.id_currency as mata_uang, "
                sqlcom = sqlcom + " isnull(lc.biaya_komisi_bank,0) as harga_beli, 'x Rp. ' as rupiah_text, "
                sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " lc.biaya_komisi_bank * pembayaran_lc.kurs as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from pembayaran_lc"
                sqlcom = sqlcom + " inner join lc on lc.seq = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq_lc = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 6 as no, convert(char, lc.tgl_bayar_biaya_lc, 103) as tanggal, 'ONGKOS KAWAT' as judul, purchase_order.id_currency as mata_uang, "
                sqlcom = sqlcom + " isnull(lc.biaya_ongkos_kawat,0) as harga_beli, 'x Rp. ' as rupiah_text, "
                sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " lc.biaya_ongkos_kawat * pembayaran_lc.kurs as total_rupiah, '' as rupiah_text_sub_total,0 as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from pembayaran_lc"
                sqlcom = sqlcom + " inner join lc on lc.seq = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq_lc = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 7 as no, convert(char, lc.tgl_bayar_biaya_lc, 103) as tanggal, 'PORTO MATERAI' as judul, purchase_order.id_currency as mata_uang, "
                sqlcom = sqlcom + " isnull(lc.biaya_porto_materai,0) as harga_beli, 'x Rp. ' as rupiah_text, "
                sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " lc.biaya_porto_materai * pembayaran_lc.kurs as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from pembayaran_lc"
                sqlcom = sqlcom + " inner join lc on lc.seq = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq_lc = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 8 as no, convert(char, lc.tgl_bayar_biaya_lc, 103) as tanggal, 'BIAYA COURIER' as judul, purchase_order.id_currency as mata_uang, "
                sqlcom = sqlcom + " isnull(lc.biaya_courier,0) as harga_beli, 'x Rp. ' as rupiah_text, "
                sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "

                'daniel 17/02/2017====================================================================

                'sqlcom = sqlcom + " (lc.biaya_courier * pembayaran_lc.kurs) as total_rupiah, '' as rupiah_text_sub_total,"
                sqlcom = sqlcom + " round((lc.biaya_courier * pembayaran_lc.kurs),-4) as total_rupiah, '' as rupiah_text_sub_total,"
                sqlcom = sqlcom + " 0 as sub_total"

                '====================================================================daniel 17/02/2017

                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from pembayaran_lc"
                sqlcom = sqlcom + " inner join lc on lc.seq = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq_lc = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
                connection.koneksi.InsertRecord(sqlcom)



                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 9 as no, convert(char, lc.tgl_bayar_biaya_lc, 103) as tanggal, 'BIAYA L/C AMENDMENT' as judul, purchase_order.id_currency as mata_uang, "
                sqlcom = sqlcom + " isnull(lc.biaya_lc_amendment,0) as harga_beli, 'x Rp. ' as rupiah_text, "
                sqlcom = sqlcom + " pembayaran_lc.kurs as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " lc.biaya_lc_amendment * pembayaran_lc.kurs as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from pembayaran_lc"
                sqlcom = sqlcom + " inner join lc on lc.seq = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq_lc = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 10 as no, '' as tanggal, '' as judul, '' as mata_uang, "
                sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
                sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " 0 as total_rupiah, 'Rp. ' as rupiah_text_sub_total, "

                'daniel 22/02/2017====================================================================

                'sqlcom = sqlcom + " sum((isnull(lc.biaya_komisi_bank,0) + isnull(lc.biaya_ongkos_kawat,0) + isnull(lc.biaya_porto_materai,0) + isnull(lc.biaya_courier,0) + isnull(lc.biaya_lc_amendment,0)) * isnull(pembayaran_lc.kurs,0)) as sub_total"
                sqlcom = sqlcom + " sum((isnull(lc.biaya_komisi_bank,0) + isnull(lc.biaya_ongkos_kawat,0) + isnull(lc.biaya_porto_materai,0) + isnull(lc.biaya_lc_amendment,0)) * isnull(pembayaran_lc.kurs,0)) + sum(round((isnull(lc.biaya_courier,0) * isnull(pembayaran_lc.kurs,0)),-4)) as sub_total"

                '====================================================================daniel 22/02/2017


                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from pembayaran_lc"
                sqlcom = sqlcom + " inner join lc on lc.seq = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq_lc = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
                sqlcom = sqlcom + " group by po_no"
                connection.koneksi.InsertRecord(sqlcom)
            Else
                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 4 as no, '' as tanggal, '2. BIAYA BANK' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
                sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 5 as no, convert(char, entry_dokumen_impor.tgl_byr_biayabank_nonlc, 103) as tanggal, 'BIAYA BANK' as judul, 'IDR', "
                sqlcom = sqlcom + " isnull(entry_dokumen_impor.biaya_bank_non_lc,0) as harga_beli, 'x Rp.' as rupiah_text "
                sqlcom = sqlcom + "," & Me.kurs_akhir_bulan & " as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " isnull(entry_dokumen_impor.biaya_bank_non_lc,0) * " & Me.kurs_akhir_bulan & " as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from entry_dokumen_impor"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq)"
                sqlcom = sqlcom + " select 10 as no, '' as tanggal, '' as judul, '' as mata_uang, "
                sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
                sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
                sqlcom = sqlcom + " 0 as total_rupiah, 'Rp. ' as rupiah_text_sub_total, "
                sqlcom = sqlcom + " isnull(entry_dokumen_impor.biaya_bank_non_lc,0) * " & Me.kurs_akhir_bulan & " as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
                sqlcom = sqlcom + " from entry_dokumen_impor"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                connection.koneksi.InsertRecord(sqlcom)
            End If

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 11 as no, '' as tanggal, '3. PAJAK IMPORT' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 12 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'BEA MASUK' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.bea_masuk as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 13 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'PPN IMPORT' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.ppn_import as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 14 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'PPH PS.22' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.pph_ps22 as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 15 as no, '' as tanggal, '' as judul, '' as mata_uang, "
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
            sqlcom = sqlcom + " select 16 as no, '' as tanggal, '4. BIAYA LAIN-LAIN' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 17 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'BIAYA ADM. PIB' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.biaya_adm_pib as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 18 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'PNBP' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.biaya_pnbp as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 19 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'BIAYA DOKUMEN' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.biaya_dokumen as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 20 as no, convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tanggal, 'SHIPPING GUARANTEE' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " entry_dokumen_impor.shipping_guarantee as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 21 as no, '' as tanggal, '' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, 'Rp. ' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, 'Rp. ' as rupiah_text_sub_total, "
            sqlcom = sqlcom + " sum(isnull(entry_dokumen_impor.biaya_adm_pib,0) + "
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.biaya_pnbp,0) + "
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.biaya_dokumen,0) + "
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.shipping_guarantee,0)) as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 22 as no, '' as tanggal, '5. BIAYA EKSPEDISI' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, 0 as total, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 23 as no, convert(char, received_import_expedition_invoice.tanggal, 103) as tanggal, "
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
            sqlcom = sqlcom + " and received_import_expedition_invoice_detail.item_hpp = 'Y'"
            sqlcom = sqlcom + " and received_import_expedition_invoice_detail.item_ppn = 'T'"
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 24 as no, '' as tanggal, '' as judul, '' as mata_uang, 0 as harga_beli, '' as rupiah_text,"
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, 'Rp.' as rupiah_text_sub_total, isnull(sum(isnull(jumlah,0)),0) as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            sqlcom = sqlcom + " from received_import_expedition_invoice"
            sqlcom = sqlcom + " inner join received_import_expedition_invoice_detail on received_import_expedition_invoice_detail.id_invoice = received_import_expedition_invoice.id "
            sqlcom = sqlcom + " inner join penugasan_ekspedisi_impor_detil on penugasan_ekspedisi_impor_detil.seq_penugasan = received_import_expedition_invoice.seq_penugasan_ekspedisi"
            sqlcom = sqlcom + " where penugasan_ekspedisi_impor_detil.seq_entry_dokumen_impor = " & Me.vseq
            sqlcom = sqlcom + " and received_import_expedition_invoice_detail.item_hpp = 'Y'"
            sqlcom = sqlcom + " and received_import_expedition_invoice_detail.item_ppn = 'T'"
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 25 as no, '' as tanggal, 'TOTAL' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, 'Rp.' as rupiah_text_sub_total, "

            If Me.vis_lc = "Y" Then

                sqlcom = sqlcom + " (select sum(pembayaran_lc.jumlah_nilai * pembayaran_lc.kurs) "
                sqlcom = sqlcom + " from pembayaran_lc"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq_lc = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                sqlcom = sqlcom + " group by po_no) +"

                'daniel 22/02/2017====================================================================

                'sqlcom = sqlcom + " (select sum((isnull(lc.biaya_komisi_bank,0) + isnull(lc.biaya_ongkos_kawat,0) + isnull(lc.biaya_porto_materai,0) + isnull(lc.biaya_courier,0) + isnull(lc.biaya_lc_amendment,0)) * isnull(pembayaran_lc.kurs,0))"
                sqlcom = sqlcom + " (select sum((isnull(lc.biaya_komisi_bank,0) + isnull(lc.biaya_ongkos_kawat,0) + isnull(lc.biaya_porto_materai,0) + isnull(lc.biaya_lc_amendment,0)) * isnull(pembayaran_lc.kurs,0)) + sum(round((isnull(lc.biaya_courier,0) * isnull(pembayaran_lc.kurs,0)),-4))"

                '====================================================================daniel 22/02/2017

                sqlcom = sqlcom + " from pembayaran_lc"
                sqlcom = sqlcom + " inner join lc on lc.seq = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq_lc = pembayaran_lc.seq_lc"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = pembayaran_lc.po_no"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
                sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = 1"
                sqlcom = sqlcom + " group by po_no) +"

            Else

                sqlcom = sqlcom + " (select isnull(sum(isnull(entry_dokumen_impor.nilai_invoice,0)),0) * " & Me.kurs_akhir_bulan
                sqlcom = sqlcom + " from entry_dokumen_impor"
                sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
                sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq & ") + "

                sqlcom = sqlcom + " (select isnull(biaya_bank_non_lc,0) * " & Me.kurs_akhir_bulan
                sqlcom = sqlcom + " from entry_dokumen_impor"
                sqlcom = sqlcom + " where seq = " & Me.vseq & ") +"
            End If

            sqlcom = sqlcom + " (select sum(isnull(entry_dokumen_impor.biaya_adm_pib,0) + "
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.biaya_pnbp,0) + "
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.biaya_dokumen,0) + "
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.shipping_guarantee,0))"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq & ")+"

            sqlcom = sqlcom + " (select sum(isnull(entry_dokumen_impor.bea_masuk,0))"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vseq & ")+"

            sqlcom = sqlcom + " (select isnull(sum(isnull(jumlah,0)),0)"
            sqlcom = sqlcom + " from received_import_expedition_invoice"
            sqlcom = sqlcom + " inner join received_import_expedition_invoice_detail on received_import_expedition_invoice_detail.id_invoice = received_import_expedition_invoice.id "
            sqlcom = sqlcom + " inner join penugasan_ekspedisi_impor_detil on penugasan_ekspedisi_impor_detil.seq_penugasan = received_import_expedition_invoice.seq_penugasan_ekspedisi"
            sqlcom = sqlcom + " where penugasan_ekspedisi_impor_detil.seq_entry_dokumen_impor = " & Me.vseq
            sqlcom = sqlcom + " and received_import_expedition_invoice_detail.item_hpp = 'Y'"
            sqlcom = sqlcom + " and received_import_expedition_invoice_detail.item_ppn = 'T')"
            sqlcom = sqlcom + " as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 27 as no, '' as tanggal, '' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 28 as no, '' as tanggal, 'CATATAN LAIN-LAIN' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)


            Dim vtotal As Decimal = 0
            'Daniel
            'sqlcom = "select isnull(sub_total,0) as sub_total from hitungan_hpp"
            sqlcom = "select sub_total from hitungan_hpp"
            'Daniel
            sqlcom = sqlcom + " where po_no = " & Me.vpo_no
            sqlcom = sqlcom + " and seq = " & Me.vseq
            sqlcom = sqlcom + " and judul = 'TOTAL'"
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

            Dim vtotal_product_hasil As Decimal = 0

            If Int(vcount) > 1 Then
                'product < max
                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq, qty_stock, id_produk)"
                sqlcom = sqlcom + " select 29 as no, '' as tanggal, product_item.nama_beli as judul,"
                sqlcom = sqlcom + " '' as mata_uang, isnull(qty,0) as harga_beli, 'x' as rupiah_text,"
                sqlcom = sqlcom + " round(" & vtotal / vtotal_product
                sqlcom = sqlcom + " *"
                sqlcom = sqlcom + " isnull(unit_price,0) - (isnull(unit_price,0) * isnull(discount,0) /100), 2)"
                sqlcom = sqlcom + " as kurs,"
                sqlcom = sqlcom + " '' as rupiah_text_total,"
                sqlcom = sqlcom + " 0 as total,"
                sqlcom = sqlcom + " 'Rp. ' as rupiah_text_sub_total, "
                sqlcom = sqlcom + " round(" & vtotal / vtotal_product
                sqlcom = sqlcom + " *"
                sqlcom = sqlcom + " isnull(unit_price,0) - (isnull(unit_price,0) * isnull(discount,0) /100), 2, 1)"
                sqlcom = sqlcom + " * "
                sqlcom = sqlcom + " isnull(qty,0)"
                sqlcom = sqlcom + " as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq & ", entry_dokumen_impor_produk.qty_stock, entry_dokumen_impor_produk.id_product"
                sqlcom = sqlcom + " from entry_dokumen_impor_produk"
                sqlcom = sqlcom + " inner join product_item on product_item.id = entry_dokumen_impor_produk.id_product"
                sqlcom = sqlcom + " where seq_entry = " & Me.vseq
                sqlcom = sqlcom + " and seq < (select max(x.seq) from entry_dokumen_impor_produk x where x.seq_entry = " & Me.vseq & ")"
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = "select sum(round(" & vtotal / vtotal_product
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
                sqlcom = sqlcom + " sub_total, po_no, seq, qty_stock, id_produk)"
                sqlcom = sqlcom + " select 30 as no, '' as tanggal, product_item.nama_beli as judul,"
                sqlcom = sqlcom + " '' as mata_uang, round(qty,2,1) as harga_beli, 'x' as rupiah_text,"
                'sqlcom = sqlcom + " round("
                'sqlcom = sqlcom + " " & vtotal - vtotal_product_hasil & " / qty , 2, 1) as kurs,"

                sqlcom = sqlcom + " round(" & vtotal / vtotal_product
                sqlcom = sqlcom + " *"
                sqlcom = sqlcom + " isnull(unit_price,0) - (isnull(unit_price,0) * isnull(discount,0) /100), 2)"
                sqlcom = sqlcom + " as kurs,"

                sqlcom = sqlcom + " '' as rupiah_text_total,"
                sqlcom = sqlcom + " 0 as total,"
                sqlcom = sqlcom + " 'Rp. ' as rupiah_text_sub_total," & vtotal - vtotal_product_hasil & " as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq & ", entry_dokumen_impor_produk.qty_stock, entry_dokumen_impor_produk.id_product"
                sqlcom = sqlcom + " from entry_dokumen_impor_produk"
                sqlcom = sqlcom + " inner join product_item on product_item.id = entry_dokumen_impor_produk.id_product"
                sqlcom = sqlcom + " where seq_entry = " & Me.vseq
                sqlcom = sqlcom + " and seq = (select max(x.seq) from entry_dokumen_impor_produk x where x.seq_entry = " & Me.vseq & ")"
                connection.koneksi.InsertRecord(sqlcom)
            Else
                sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
                sqlcom = sqlcom + " sub_total, po_no, seq, qty_stock, id_produk)"
                sqlcom = sqlcom + " select 31 as no, '' as tanggal, product_item.nama_beli as judul,"
                sqlcom = sqlcom + " '' as mata_uang, round(qty,2,1) as harga_beli, 'x' as rupiah_text,"
                'sqlcom = sqlcom + " round("
                'sqlcom = sqlcom + " " & vtotal - vtotal_product_hasil & " / qty as kurs,"

                sqlcom = sqlcom + " round(" & vtotal / vtotal_product
                sqlcom = sqlcom + " *"
                sqlcom = sqlcom + " isnull(unit_price,0) - (isnull(unit_price,0) * isnull(discount,0) /100), 2)"
                sqlcom = sqlcom + " as kurs,"

                sqlcom = sqlcom + " '' as rupiah_text_total,"
                sqlcom = sqlcom + " 0 as total,"
                sqlcom = sqlcom + " 'Rp. ' as rupiah_text_sub_total," & vtotal - vtotal_product_hasil & " as sub_total"
                sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq & ", entry_dokumen_impor_produk.qty_stock, entry_dokumen_impor_produk.id_product"
                sqlcom = sqlcom + " from entry_dokumen_impor_produk"
                sqlcom = sqlcom + " inner join product_item on product_item.id = entry_dokumen_impor_produk.id_product"
                sqlcom = sqlcom + " where seq_entry = " & Me.vseq
                sqlcom = sqlcom + " and seq = (select max(x.seq) from entry_dokumen_impor_produk x where x.seq_entry = " & Me.vseq & ")"
                connection.koneksi.InsertRecord(sqlcom)

                'sqlcom = sqlcom + " " & (Math.Round(vtotal, 2) - Math.Round(vtotal_product_hasil, 2)) & " / qty , 2, 1) as kurs,"

            End If

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 998 as no, '' as tanggal, 'TOTAL' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, '' as rupiah_text_sub_total," & Math.Round(vtotal, 2) & " as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 999 as no, '' as tanggal, '' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = " insert into hitungan_hpp(no, tanggal, judul, mata_uang, harga_beli, rupiah_text, kurs, rupiah_text_total, total, rupiah_text_sub_total,"
            sqlcom = sqlcom + " sub_total, po_no, seq)"
            sqlcom = sqlcom + " select 1000 as no, '' as tanggal, 'SISA LEBIH/KURANG' as judul, '' as mata_uang, "
            sqlcom = sqlcom + " 0 as harga_beli, '' as rupiah_text, "
            sqlcom = sqlcom + " 0 as kurs, '' as rupiah_text_total, "
            sqlcom = sqlcom + " 0 as total_rupiah, '' as rupiah_text_sub_total, 0 as sub_total"
            sqlcom = sqlcom + "," & Me.vpo_no & "," & Me.vseq
            connection.koneksi.InsertRecord(sqlcom)

            If vtotal_product <> 0 Then
                Me.insert_hpp()
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
                reportPath = Server.MapPath("reports\hpp_lc.rpt")
                'reportPath = Server.MapPath("reports\hpp_invoice.rpt")
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
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal
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

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update entry_dokumen_impor"
                    sqlcom = sqlcom + " set "

                    If CType(Me.dg_data.Items(x).FindControl("dd_dibuat_oleh"), DropDownList).SelectedValue = 0 Then
                        sqlcom = sqlcom + " dibuat_oleh = NULL,"
                    Else
                        sqlcom = sqlcom + " dibuat_oleh = " & CType(Me.dg_data.Items(x).FindControl("dd_dibuat_oleh"), DropDownList).SelectedValue & ","
                    End If

                    If CType(Me.dg_data.Items(x).FindControl("dd_disetujui_oleh"), DropDownList).SelectedValue = 0 Then
                        sqlcom = sqlcom + " disetujui_oleh = NULL,"
                    Else
                        sqlcom = sqlcom + " disetujui_oleh = " & CType(Me.dg_data.Items(x).FindControl("dd_disetujui_oleh"), DropDownList).SelectedValue
                    End If

                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub buttonSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles buttonSearch.Click
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub
End Class