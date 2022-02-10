Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_pembayaran_lc
    Inherits System.Web.UI.UserControl
    'Daniel
    Public tradingClass As New tradingClass()


    Sub GL()

        Try
            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim akun_bea_masuk As String = Nothing
            Dim akun_ppn_impor As String = Nothing
            Dim akun_pph22 As String = Nothing
            Dim akun_biaya_lain2 As String = Nothing

            Dim vtgl As String = Me.tb_tgl_pembayaran.Text.Substring(3, 2) & "/" & Me.tb_tgl_pembayaran.Text.Substring(0, 2) & "/" & Me.tb_tgl_pembayaran.Text.Substring(6, 4)
            Dim id_periode As Integer = 0
            sqlcom = "select id from transaction_period"
            sqlcom = sqlcom + " where tgl_awal <= '" & vtgl & "' and tgl_akhir >= '" & vtgl & "'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                id_periode = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            sqlcom = "select akun_bea_masuk, akun_ppn_impor, akun_pph22, akun_biaya_lain2"
            sqlcom = sqlcom + " from  akun_pembayaran_pib"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                akun_bea_masuk = reader.Item("akun_bea_masuk").ToString
                akun_ppn_impor = reader.Item("akun_ppn_impor").ToString
                akun_pph22 = reader.Item("akun_pph22").ToString
                akun_biaya_lain2 = reader.Item("akun_biaya_lain2").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            Dim keterangan As String = "Pembayaran PIB dokumen no. " & Me.vno_dokumen

            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text), id, Me.tradingClass.JurnalType("4"), keterangan, Me.vid_periode_transaksi)

            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text), akun_bea_masuk, Me.vakun_bank_account, Me.bea_masuk, 0, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, "1", 0, 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text), Me.vakun_bank_account, akun_bea_masuk, 0, Me.bea_masuk, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, "1", 0, 0, String.Empty)

            'history kas
            Me.seq_max_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
            sqlcom = sqlcom + " values (" & id_periode & "," & Me.dd_bank.SelectedValue & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text) & "','"
            sqlcom = sqlcom + keterangan & "', 0"
            sqlcom = sqlcom + ", " & Me.bea_masuk & "," & Me.vmax_history_kas & ")"
            connection.koneksi.InsertRecord(sqlcom)

            'mengurangi saldo kas/bank
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - isnull(" & Me.bea_masuk & ",0)"
            sqlcom = sqlcom + " where id = " & Me.dd_bank.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)

            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text), akun_ppn_impor, Me.vakun_bank_account, Me.ppn_impor, 0, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, "1", 0, 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text), Me.vakun_bank_account, akun_ppn_impor, 0, Me.ppn_impor, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, "1", 0, 0, String.Empty)

            'history kas
            Me.seq_max_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
            sqlcom = sqlcom + " values (" & id_periode & "," & Me.dd_bank.SelectedValue & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text) & "','"
            sqlcom = sqlcom + keterangan & "', 0"
            sqlcom = sqlcom + ", " & Me.ppn_impor & "," & Me.vmax_history_kas & ")"
            connection.koneksi.InsertRecord(sqlcom)

            'mengurangi saldo kas/bank
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - isnull(" & Me.ppn_impor & ",0)"
            sqlcom = sqlcom + " where id = " & Me.dd_bank.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)

            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text), akun_pph22, Me.vakun_bank_account, Me.pph22, 0, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, "1", 0, 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text), Me.vakun_bank_account, akun_pph22, 0, Me.pph22, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, "1", 0, 0, String.Empty)

            'history kas
            Me.seq_max_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
            sqlcom = sqlcom + " values (" & id_periode & "," & Me.dd_bank.SelectedValue & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text) & "','"
            sqlcom = sqlcom + keterangan & "', 0"
            sqlcom = sqlcom + ", " & Me.pph22 & "," & Me.vmax_history_kas & ")"
            connection.koneksi.InsertRecord(sqlcom)

            'mengurangi saldo kas/bank
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - isnull(" & Me.pph22 & ",0)"
            sqlcom = sqlcom + " where id = " & Me.dd_bank.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)

            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text), akun_biaya_lain2, Me.vakun_bank_account, Me.biaya_lain2, 0, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, "1", 0, 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text), Me.vakun_bank_account, akun_biaya_lain2, 0, Me.biaya_lain2, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, "1", 0, 0, String.Empty)

            'history kas
            Me.seq_max_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
            sqlcom = sqlcom + " values (" & id_periode & "," & Me.dd_bank.SelectedValue & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_pembayaran.Text) & "','"
            sqlcom = sqlcom + keterangan & "', 0"
            sqlcom = sqlcom + ", " & Me.biaya_lain2 & "," & Me.vmax_history_kas & ")"
            connection.koneksi.InsertRecord(sqlcom)

            'mengurangi saldo kas/bank
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - isnull(" & Me.biaya_lain2 & ",0)"
            sqlcom = sqlcom + " where id = " & Me.dd_bank.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)

            tradingClass.Alert("Data sudah disubmit!", Me.Page)
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Public Property bea_masuk() As Decimal
        Get
            Dim o As Object = ViewState("bea_masuk")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("bea_masuk") = value
        End Set
    End Property

    Public Property ppn_impor() As Decimal
        Get
            Dim o As Object = ViewState("ppn_impor")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("ppn_impor") = value
        End Set
    End Property

    Public Property pph22() As Decimal
        Get
            Dim o As Object = ViewState("pph22")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("pph22") = value
        End Set
    End Property

    Public Property biaya_lain2() As Decimal
        Get
            Dim o As Object = ViewState("biaya_lain2")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("biaya_lain2") = value
        End Set
    End Property

    Public Property vid_periode_transaksi() As Integer
        Get
            Dim o As Object = ViewState("vid_periode_transaksi")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_periode_transaksi") = value
        End Set
    End Property

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

    Private ReadOnly Property vno_po() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_po")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vno_dokumen() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_dokumen")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private Property is_view_produk() As String
        Get
            Dim o As Object = ViewState("is_view_produk")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("is_view_produk") = value
        End Set
    End Property

    Public Property vakun_bea_masuk() As String
        Get
            Dim o As Object = ViewState("vakun_bea_masuk")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_bea_masuk") = value
        End Set
    End Property

    Public Property vakun_ppn_impor() As String
        Get
            Dim o As Object = ViewState("vakun_ppn_impor")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_ppn_impor") = value
        End Set
    End Property

    Public Property vakun_biaya_lain2() As String
        Get
            Dim o As Object = ViewState("vakun_biaya_lain2")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_biaya_lain2") = value
        End Set
    End Property

    Public Property vakun_pph22() As String
        Get
            Dim o As Object = ViewState("vakun_pph22")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_pph22") = value
        End Set
    End Property

    Public Property vakun_bank_account() As String
        Get
            Dim o As Object = ViewState("vakun_bank_account")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_bank_account") = value
        End Set
    End Property

    Public Property vmax() As Integer
        Get
            Dim o As Object = ViewState("vmax")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vmax") = value
        End Set
    End Property

    Public Property vmax_history_kas() As Integer
        Get
            Dim o As Object = ViewState("vmax_history_kas")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vmax_history_kas") = value
        End Set
    End Property


    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode()
        Try
            sqlcom = "select id, name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_periode.Text = reader.Item("name").ToString
                Me.vid_periode_transaksi = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindjenispembayaran()
        Try
            sqlcom = "select id, name from jenis_pembayaran order by name"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_jenis_pembayaran.DataSource = reader
            Me.dd_jenis_pembayaran.DataTextField = "name"
            Me.dd_jenis_pembayaran.DataValueField = "id"
            Me.dd_jenis_pembayaran.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindbank()
        Try
            sqlcom = "select id, name from bank_account order by name"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_bank.DataSource = reader
            Me.dd_bank.DataTextField = "name"
            Me.dd_bank.DataValueField = "id"
            Me.dd_bank.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clearform()
        Me.tb_tgl_pembayaran.Text = ""
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
            sqlcom = sqlcom + " entry_dokumen_impor.bl_no, entry_dokumen_impor.invoice_no, convert(char, entry_dokumen_impor.tgl_invoice, 103) as tgl_invoice,"
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.nilai_invoice,0) as nilai_invoice, entry_dokumen_impor.packing_list_no,"
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.bea_masuk,0) as bea_masuk, isnull(entry_dokumen_impor.ppn_import,0) as ppn_import,"
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.pph_ps22,0) as pph_ps22, isnull(entry_dokumen_impor.biaya_adm_pib,0) as biaya_adm_pib,"
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.biaya_pnbp,0) as biaya_pnbp, isnull(entry_dokumen_impor.biaya_dokumen,0) as biaya_dokumen,"
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.shipping_guarantee,0) as shipping_guarantee,"
            sqlcom = sqlcom + " convert(char, entry_dokumen_impor.tanggal_bayar_pib, 103) as tgl_pembayaran, entry_dokumen_impor.id_bank,"
            sqlcom = sqlcom + " entry_dokumen_impor.id_jenis_pembayaran_pib, entry_dokumen_impor.no_giro,"
            sqlcom = sqlcom + " convert(char, entry_dokumen_impor.tgl_giro, 103) as tgl_giro, "
            sqlcom = sqlcom + " convert(char, entry_dokumen_impor.tgl_jatuh_tempo, 103) as tgl_jatuh_tempo, is_submit_pib"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " inner join transaction_period on transaction_period.id = purchase_order.id_transaction_period"
            sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vno_dokumen
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

                'data pembayaran_pib
                Me.lbl_no_dokumen.Text = Me.vno_dokumen
                Me.lbl_no_bl.Text = reader.Item("bl_no").ToString
                Me.lbl_no_packing_list.Text = reader.Item("packing_list_no").ToString
                Me.lbl_no_invoice.Text = reader.Item("invoice_no").ToString
                Me.lbl_tgl_invoice.Text = reader.Item("tgl_invoice").ToString
                Me.lbl_nilai_invoice.Text = FormatNumber(reader.Item("nilai_invoice").ToString, 2)
                Me.lbl_bea_masuk.Text = FormatNumber(reader.Item("bea_masuk").ToString)
                Me.bea_masuk = reader.Item("bea_masuk")
                Me.lbl_ppn_impor.Text = FormatNumber(reader.Item("ppn_import").ToString)
                Me.ppn_impor = reader.Item("ppn_import")
                Me.lbl_pph_ps22.Text = FormatNumber(reader.Item("pph_ps22").ToString)
                Me.pph22 = reader.Item("pph_ps22")
                Me.lbl_biaya_adm_pib.Text = FormatNumber(reader.Item("biaya_adm_pib").ToString)
                Me.lbl_pnbp.Text = FormatNumber(reader.Item("biaya_pnbp").ToString)
                Me.lbl_biaya_dokumen.Text = FormatNumber(reader.Item("biaya_dokumen").ToString)
                Me.lbl_shipping_guarantee.Text = FormatNumber(reader.Item("shipping_guarantee").ToString)
                Me.biaya_lain2 = Val(reader.Item("biaya_adm_pib")) + Val(reader.Item("biaya_pnbp")) + Val(reader.Item("biaya_dokumen")) + Val(reader.Item("shipping_guarantee"))
                Me.tb_tgl_pembayaran.Text = reader.Item("tgl_pembayaran").ToString
                Me.dd_jenis_pembayaran.SelectedValue = reader.Item("id_jenis_pembayaran_pib").ToString
                Me.dd_bank.SelectedValue = reader.Item("id_bank").ToString
                Me.tb_no_giro.Text = reader.Item("no_giro").ToString
                Me.tb_tgl_giro.Text = reader.Item("tgl_giro").ToString
                Me.tb_jatuh_tempo.Text = reader.Item("tgl_jatuh_tempo").ToString

                If reader.Item("is_submit_pib").ToString = "B" Then
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                Else
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                End If
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.bindperiode()
            Me.bindjenispembayaran()
            Me.bindbank()
            Me.loaddata()
            Me.is_view_produk = "Y"
            Me.view_produk()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/pembayaran_biaya_pib.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tgl_pembayaran.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran terlebih dahulu"
            Else
                Dim vtgl_bayar As String = Me.tb_tgl_pembayaran.Text.Substring(3, 2) & "/" & Me.tb_tgl_pembayaran.Text.Substring(0, 2) & "/" & Me.tb_tgl_pembayaran.Text.Substring(6, 4)

                Dim vtgl_giro As String = ""
                Dim vtgl_jatuh_tempo As String = ""

                If Not String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                    vtgl_giro = Me.tb_tgl_giro.Text.Substring(3, 2) & "/" & Me.tb_tgl_giro.Text.Substring(0, 2) & "/" & Me.tb_tgl_giro.Text.Substring(6, 4)
                End If

                If Not String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                    vtgl_jatuh_tempo = Me.tb_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(6, 4)
                End If

                sqlcom = "update entry_dokumen_impor"
                sqlcom = sqlcom + " set tanggal_bayar_pib = '" & vtgl_bayar & "',"
                sqlcom = sqlcom + " id_jenis_pembayaran_pib = " & Me.dd_jenis_pembayaran.SelectedValue & ","
                sqlcom = sqlcom + " id_bank = " & Me.dd_bank.SelectedValue & ","
                sqlcom = sqlcom + " no_giro = '" & Me.tb_no_giro.Text & "',"

                If String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                    sqlcom = sqlcom + " tgl_giro = NULL,"
                Else
                    sqlcom = sqlcom + " tgl_giro = '" & vtgl_giro & "',"
                End If

                If String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                    sqlcom = sqlcom + " tgl_jatuh_tempo = NULL"
                Else
                    sqlcom = sqlcom + " tgl_jatuh_tempo = '" & vtgl_jatuh_tempo & "'"
                End If

                sqlcom = sqlcom + " where seq = " & Me.vno_dokumen
                connection.koneksi.UpdateRecord(sqlcom)
                tradingClass.Alert("Data sudah disimpan", Me.Page)
                Me.loaddata()
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
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
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub view_produk()
        If Me.is_view_produk = "N" Then
            Me.btn_produk.Text = "View Produk Item"
            Me.tbl_produk.Visible = False
            Me.is_view_produk = "Y"
        Else
            Me.btn_produk.Text = "Hide Produk Item"
            Me.tbl_produk.Visible = True
            Me.is_view_produk = "N"
        End If
        Me.loadgrid()
    End Sub

    Protected Sub btn_produk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_produk.Click
        Me.view_produk()
    End Sub

    Sub seq_max()
        Dim readermax As SqlClient.SqlDataReader
        sqlcom = "select isnull(max(seq),0) + 1 as vmax from akun_general_ledger"
        readermax = connection.koneksi.SelectRecord(sqlcom)
        readermax.Read()
        If readermax.HasRows Then
            Me.vmax = readermax.Item("vmax").ToString
        End If
        readermax.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub seq_max_history_kas()
        Dim readermax As SqlClient.SqlDataReader
        sqlcom = "select isnull(max(seq),0) + 1 as vmax from history_kas"
        readermax = connection.koneksi.SelectRecord(sqlcom)
        readermax.Read()
        If readermax.HasRows Then
            Me.vmax_history_kas = readermax.Item("vmax").ToString
        End If
        readermax.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub jurnal()
        Try

            Dim vtgl As String = Me.tb_tgl_pembayaran.Text.Substring(3, 2) & "/" & Me.tb_tgl_pembayaran.Text.Substring(0, 2) & "/" & Me.tb_tgl_pembayaran.Text.Substring(6, 4)

            Dim id_periode As Integer = 0
            sqlcom = "select id from transaction_period"
            sqlcom = sqlcom + " where tgl_awal <= '" & vtgl & "' and tgl_akhir >= '" & vtgl & "'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                id_periode = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            'debet bea masuk -> kas_bank
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_dokumen & "','" & vtgl & "','BYRLPIB','" & Me.vakun_bea_masuk & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(Me.lbl_bea_masuk.Text) & ",0, 'Pembayaran PIB dokumen no. " & Me.vno_dokumen & " (Bea masuk)'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "', 1, 0 ,0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun kas/bank -> bea masuk
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_dokumen & "','" & vtgl & "','BYRLPIB','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bea_masuk & "', 0," & Decimal.ToDouble(Me.lbl_bea_masuk.Text) & ",'Pembayaran PIB dokumen no. " & Me.vno_dokumen & " (Bea masuk)'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "', 1, 0 ,0)"
            connection.koneksi.InsertRecord(sqlcom)

            '================
            'debet ppn impor -> kas_bank
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_dokumen & "','" & vtgl & "','BYRLPIB','" & Me.vakun_ppn_impor & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(Me.lbl_ppn_impor.Text) & ",0, 'Pembayaran PIB dokumen no. " & Me.vno_dokumen & " (PPN Impor)'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "', 1, 0 ,0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun kas/bank -> bea masuk
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_dokumen & "','" & vtgl & "','BYRLPIB','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & Me.vakun_ppn_impor & "', 0," & Decimal.ToDouble(Me.lbl_ppn_impor.Text) & ",'Pembayaran PIB dokumen no. " & Me.vno_dokumen & " (PPN Impor)'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "', 1, 0 ,0)"
            connection.koneksi.InsertRecord(sqlcom)

            '================
            'debet pph22 -> kas_bank
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_dokumen & "','" & vtgl & "','BYRLPIB','" & Me.vakun_pph22 & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(Me.lbl_pph_ps22.Text) & ",0, 'Pembayaran PIB dokumen no. " & Me.vno_dokumen & " (PPh 22)'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "', 1, 0 ,0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun kas/bank -> bea masuk
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_dokumen & "','" & vtgl & "','BYRLPIB','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & Me.vakun_pph22 & "', 0," & Decimal.ToDouble(Me.lbl_pph_ps22.Text) & ",'Pembayaran PIB dokumen no. " & Me.vno_dokumen & " (PPh 22)'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "', 1, 0 ,0)"
            connection.koneksi.InsertRecord(sqlcom)


            '================
            'debet biaya lain2 -> kas_bank
            Dim total_biaya_lain2 As Decimal = Decimal.ToDouble(Me.lbl_biaya_adm_pib.Text) + Decimal.ToDouble(Me.lbl_pnbp.Text) + Decimal.ToDouble(Me.lbl_biaya_dokumen.Text)
            total_biaya_lain2 = total_biaya_lain2 + Decimal.ToDouble(Me.lbl_shipping_guarantee.Text)

            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_dokumen & "','" & vtgl & "','BYRLPIB','" & Me.vakun_biaya_lain2 & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(total_biaya_lain2) & ",0, 'Pembayaran PIB dokumen no. " & Me.vno_dokumen & " (Biaya lain-lain)'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "', 1, 0 ,0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun kas/bank -> bea masuk
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_dokumen & "','" & vtgl & "','BYRLPIB','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & Me.vakun_biaya_lain2 & "', 0," & Decimal.ToDouble(total_biaya_lain2) & ",'Pembayaran PIB dokumen no. " & Me.vno_dokumen & " (Biaya lain-lain)'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "', 1, 0 ,0)"
            connection.koneksi.InsertRecord(sqlcom)

            Dim total_biaya As Decimal = Decimal.ToDouble(Me.lbl_bea_masuk.Text) + Decimal.ToDouble(Me.lbl_ppn_impor.Text) + Decimal.ToDouble(Me.lbl_pph_ps22.Text)
            total_biaya = total_biaya + total_biaya_lain2

          

        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            'cek tanggal
            sqlcom = "select convert(char, tanggal_bayar_pib, 103) as tanggal_bayar_pib"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vno_dokumen
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If String.IsNullOrEmpty(reader.Item("tanggal_bayar_pib").ToString) Then
                    Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran terlebih dahulu"
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    Exit Sub
                End If
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            'cek akun pembayaran pib
            sqlcom = "select akun_bea_masuk, akun_ppn_impor, akun_pph22, akun_biaya_lain2"
            sqlcom = sqlcom + " from akun_pembayaran_pib"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_bea_masuk = reader.Item("akun_bea_masuk").ToString.Trim
                Me.vakun_ppn_impor = reader.Item("akun_ppn_impor").ToString.Trim
                Me.vakun_pph22 = reader.Item("akun_pph22").ToString.Trim
                Me.vakun_biaya_lain2 = reader.Item("akun_biaya_lain2").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_bea_masuk) Then
                Me.lbl_msg.Text = "Akun bea masuk tidak ada pada akun pembayaran pib"
                Exit Sub
            End If

            If String.IsNullOrEmpty(Me.vakun_ppn_impor) Then
                Me.lbl_msg.Text = "Akun PPN masukan tidak ada pada akun pembayaran pib"
                Exit Sub
            End If

            If String.IsNullOrEmpty(Me.vakun_pph22) Then
                Me.lbl_msg.Text = "Akun PPH 22 tidak ada pada akun pembayaran pib"
                Exit Sub
            End If

            If String.IsNullOrEmpty(Me.vakun_biaya_lain2) Then
                Me.lbl_msg.Text = "Akun biaya lain-lain tidak ada pada akun pembayaran pib"
                Exit Sub
            End If

            'total biaya
            Dim total_biaya As Decimal = Decimal.ToDouble(Me.lbl_bea_masuk.Text) + Decimal.ToDouble(Me.lbl_ppn_impor.Text) + Decimal.ToDouble(Me.lbl_pph_ps22.Text)
            total_biaya = total_biaya + Decimal.ToDouble(Me.lbl_biaya_adm_pib.Text) + Decimal.ToDouble(Me.lbl_pnbp.Text) + Decimal.ToDouble(Me.lbl_biaya_dokumen.Text)
            total_biaya = total_biaya + Decimal.ToDouble(Me.lbl_shipping_guarantee.Text)

            'cek akun dan saldo kas bank account
            Dim saldo_akhir As Decimal = 0
            sqlcom = "select account_code, isnull(saldo_akhir,0) as saldo_akhir"
            sqlcom = sqlcom + " from bank_account"
            sqlcom = sqlcom + " where id = " & Me.dd_bank.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_bank_account = reader.Item("account_code").ToString
                saldo_akhir = reader.Item("saldo_akhir").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_bank_account) Then
                Me.lbl_msg.Text = "Kode akun pada Kas/Bank tersebut tidak ada"
                Exit Sub
            End If

            'If Decimal.ToDouble(saldo_akhir) < total_biaya Then
            'Me.lbl_msg.Text = "Saldo Kas/Bank tersebut tidak mencukupi"
            'Exit Sub
            'End If

            sqlcom = "update entry_dokumen_impor"
            sqlcom = sqlcom + " set is_submit_pib = 'S'"
            sqlcom = sqlcom + " where seq = " & Me.vno_dokumen
            connection.koneksi.UpdateRecord(sqlcom)
            '<--adien 27 Des 2013
            'Me.jurnal()
            Me.GL()

            'adien-->
            Me.loaddata()

        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class
