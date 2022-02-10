Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_pembayaran_biaya_lc
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()

    Sub GL()

        Try
            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim d_kurs As Decimal = Me.tb_kurs.Text  'System.Convert.ToDecimal(tradingClass.KursBulanan(Me.vid_periode_transaksi))

            Dim keterangan As String = "Pembayaran biaya L/C no. " & Me.vno_lc & " (Total Biaya, L/C no. " & Me.lbl_no_lc.Text & ")"
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_bayar.Text), id, Me.tradingClass.JurnalType("4"), keterangan, Me.vid_periode_transaksi)

            tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_bayar.Text), Me.DropDownListAccount.SelectedValue, Me.vakun_bank_account, System.Convert.ToDecimal(Me.lbl_total_biaya.Text) * d_kurs, 0, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, d_kurs, IIf(Me.lbl_mata_uang.Text = "IDR", 0, System.Convert.ToDecimal(Me.lbl_total_biaya.Text)), 0, String.Empty)
            tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_bayar.Text), Me.vakun_bank_account, Me.DropDownListAccount.SelectedValue, 0, System.Convert.ToDecimal(Me.lbl_total_biaya.Text) * d_kurs, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, d_kurs, 0, IIf(Me.lbl_mata_uang.Text = "IDR", 0, System.Convert.ToDecimal(Me.lbl_total_biaya.Text)), String.Empty)

            Dim id_periode As Integer = 0
            Dim vtgl As String = Me.tb_tgl_bayar.Text.Substring(3, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(0, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(6, 4)

            sqlcom = "select id from transaction_period"
            sqlcom = sqlcom + " where tgl_awal <= '" & vtgl & "' and tgl_akhir >= '" & vtgl & "'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                id_periode = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            'history kas
            Me.seq_max_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
            sqlcom = sqlcom + " values (" & id_periode & "," & Me.dd_bank.SelectedValue & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_bayar.Text) & "','"
            sqlcom = sqlcom + keterangan & "',0," & System.Convert.ToDecimal(Me.lbl_total_biaya.Text) * d_kurs
            sqlcom = sqlcom + "," & Me.vmax_history_kas & ")"
            connection.koneksi.InsertRecord(sqlcom)

            'mengurangi saldo kas/bank
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - isnull(" & System.Convert.ToDecimal(Me.lbl_total_biaya.Text) * d_kurs & ",0)"
            sqlcom = sqlcom + " where id = " & Me.dd_bank.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)

            tradingClass.Alert("Data sudah disubmit!", Me.Page)
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
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

    Private ReadOnly Property vno_lc() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_lc")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
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

    Public Property vakun_komisi_bank() As String
        Get
            Dim o As Object = ViewState("vakun_komisi_bank")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_komisi_bank") = value
        End Set
    End Property

    Public Property vakun_ongkos_kawat() As String
        Get
            Dim o As Object = ViewState("vakun_ongkos_kawat")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_ongkos_kawat") = value
        End Set
    End Property

    Public Property vakun_porto_materai() As String
        Get
            Dim o As Object = ViewState("vakun_porto_materai")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_porto_materai") = value
        End Set
    End Property

    Public Property vakun_biaya_courier() As String
        Get
            Dim o As Object = ViewState("vakun_biaya_courier")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_biaya_courier") = value
        End Set
    End Property

    Public Property vakun_biaya_lc_amendment() As String
        Get
            Dim o As Object = ViewState("vakun_biaya_lc_amendment")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_biaya_lc_amendment") = value
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

    Sub clearform()
        Me.tb_tgl_bayar.Text = ""
        Me.tb_no_giro.Text = ""
        Me.tb_tgl_giro.Text = ""
        Me.tb_jatuh_tempo.Text = ""
    End Sub

    Sub loaddata()
        Try
            sqlcom = "select purchase_order.po_no_text, convert(char, purchase_order.tanggal, 103) as tanggal, purchase_order.id_currency,"
            sqlcom = sqlcom + " lc.no_lc, convert(char, lc.tanggal_lc, 103) as tanggal_lc, convert(char, lc.tgl_berlaku_lc, 103) as tgl_berlaku_lc,"
            sqlcom = sqlcom + " lc.id_lc_type, convert(char, lc.due_date_lc, 103) as due_date_lc, lc.id_negara_koresponden, lc.id_dikapalkan_dari,"
            sqlcom = sqlcom + " lc.id_pelabuhan_tujuan, lc.id_negara_asal, daftar_supplier.name as nama_supplier,"
            sqlcom = sqlcom + " isnull(lc.nilai_lc,0) as total_pembelian, transaction_period.name as nama_periode,transaction_period.id as id_periode,"
            sqlcom = sqlcom + " isnull(lc.biaya_komisi_bank,0) as biaya_komisi_bank, isnull(lc.biaya_ongkos_kawat,0) as biaya_ongkos_kawat,"
            sqlcom = sqlcom + " isnull(lc.biaya_porto_materai,0) as biaya_porto_materai, isnull(lc.biaya_courier,0) as biaya_courier,"
            sqlcom = sqlcom + " isnull(lc.biaya_lc_amendment,0) as biaya_lc_amendment,"
            sqlcom = sqlcom + " isnull(lc.biaya_komisi_bank,0) + isnull(lc.biaya_ongkos_kawat,0) + "
            sqlcom = sqlcom + " isnull(lc.biaya_porto_materai,0) + isnull(lc.biaya_courier,0) + "
            sqlcom = sqlcom + " isnull(lc.biaya_lc_amendment,0) as total_biaya,"
            sqlcom = sqlcom + " convert(char, lc.tgl_bayar_biaya_lc, 103) as tgl_bayar_biaya_lc, lc.id_bank_biaya_lc, lc.id_jenis_pembayaran_biaya_lc,"
            sqlcom = sqlcom + " isnull(lc.kurs_biaya_lc,0) as kurs_biaya_lc, lc.no_giro, convert(char, lc.tgl_giro, 103) as tgl_giro, convert(char, lc.tgl_jatuh_tempo, 103) as tgl_jatuh_tempo,"
            sqlcom = sqlcom + " lc.is_submit_bayar"
            sqlcom = sqlcom + " from lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = lc.no_po"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " inner join transaction_period on transaction_period.id = lc.id_transaction_period"
            sqlcom = sqlcom + " where seq = " & Me.vno_lc
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vid_periode_transaksi = reader.Item("id_periode").ToString
                Me.lbl_periode.Text = reader.Item("nama_periode").ToString
                Me.lbl_no_pembelian.Text = reader.Item("po_no_text").ToString
                Me.lbl_tgl_pembelian.Text = reader.Item("tanggal").ToString
                Me.lbl_mata_uang.Text = reader.Item("id_currency").ToString
                Me.lbl_total_nilai_pembelian.Text = FormatNumber(reader.Item("total_pembelian").ToString, 2)
                Me.lbl_nama_supplier.Text = reader.Item("nama_supplier").ToString
                Me.lbl_no_lc.Text = reader.Item("no_lc").ToString
                Me.lbl_tgl_lc.Text = reader.Item("tanggal_lc").ToString
                Me.lbl_komisi_bank.Text = FormatNumber(reader.Item("biaya_komisi_bank").ToString, 2)
                Me.lbl_ongkos_kawat.Text = FormatNumber(reader.Item("biaya_ongkos_kawat").ToString, 2)
                Me.lbl_porto_materai.Text = FormatNumber(reader.Item("biaya_porto_materai").ToString, 2)
                Me.lbl_biaya_courier.Text = FormatNumber(reader.Item("biaya_courier").ToString, 2)
                Me.lbl_biaya_lc_amendment.Text = FormatNumber(reader.Item("biaya_lc_amendment").ToString, 2)
                Me.lbl_total_biaya.Text = FormatNumber(reader.Item("total_biaya").ToString, 2)
                Me.tb_tgl_bayar.Text = reader.Item("tgl_bayar_biaya_lc").ToString
                Me.dd_jenis_pembayaran.SelectedValue = reader.Item("id_jenis_pembayaran_biaya_lc").ToString
                Me.dd_bank.SelectedValue = reader.Item("id_bank_biaya_lc").ToString
                Me.tb_kurs.Text = FormatNumber(reader.Item("kurs_biaya_lc").ToString, 2)
                Me.tb_no_giro.Text = reader.Item("no_giro").ToString
                Me.tb_tgl_giro.Text = reader.Item("tgl_giro").ToString
                Me.tb_jatuh_tempo.Text = reader.Item("tgl_jatuh_tempo").ToString

                If reader.Item("is_submit_bayar").ToString = "S" Then
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                Else
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                End If
            Else
                Me.btn_submit.Enabled = False
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.bindjenispembayaran()
            Me.bindbank()
            Me.loaddata()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/pembayaran_biaya_lc.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tgl_bayar.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_kurs.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi kurs bulanan terlebih dahulu"
            Else
                If Me.dd_jenis_pembayaran.SelectedValue = 2 Or Me.dd_jenis_pembayaran.SelectedValue = 3 Then
                    If String.IsNullOrEmpty(Me.tb_no_giro.Text) Or String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Or String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                        Me.lbl_msg.Text = "Silahkan mengisi No. Giro/Cek, Tgl. Giro/Cek, Tgl. jatuh tempo Giro/Cek"
                        Exit Sub
                    End If
                End If

                Dim vtgl_bayar As String = Me.tb_tgl_bayar.Text.Substring(3, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(0, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(6, 4)

                Dim vtgl_giro As String = ""
                Dim vtgl_jatuh_tempo As String = ""

                If Not String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                    vtgl_giro = Me.tb_tgl_giro.Text.Substring(3, 2) & "/" & Me.tb_tgl_giro.Text.Substring(0, 2) & "/" & Me.tb_tgl_giro.Text.Substring(6, 4)
                End If

                If Not String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                    vtgl_jatuh_tempo = Me.tb_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(6, 4)
                End If

                sqlcom = "update lc"
                sqlcom = sqlcom + " set tgl_bayar_biaya_lc = '" & vtgl_bayar & "',"
                sqlcom = sqlcom + " id_jenis_pembayaran_biaya_lc = " & Me.dd_jenis_pembayaran.SelectedValue & ","
                sqlcom = sqlcom + " id_bank_biaya_lc = " & Me.dd_bank.SelectedValue & ","
                sqlcom = sqlcom + " kurs_biaya_lc = " & Decimal.ToDouble(Me.tb_kurs.Text) & ","
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

                sqlcom = sqlcom + " where seq = " & Me.vno_lc
                connection.koneksi.UpdateRecord(sqlcom)
                tradingClass.Alert("Data sudah disimpan", Me.Page)
                Me.loaddata()
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    'Protected Sub btn_kurs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs.Click
    '    Try
    '        If String.IsNullOrEmpty(Me.tb_tgl_bayar.Text) Then
    '            Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran terlebih dahulu"
    '        Else
    '            Dim vtgl As Date = Me.tb_tgl_bayar.Text.Substring(3, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(0, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(6, 4)

    '            sqlcom = "select isnull(kurs_bulanan,0) as kurs_bulanan"
    '            sqlcom = sqlcom + " from transaction_period"
    '            sqlcom = sqlcom + " where tgl_awal <= '" & vtgl & "' and tgl_akhir >= '" & vtgl & "'"
    '            reader = connection.koneksi.SelectRecord(sqlcom)
    '            reader.Read()
    '            If reader.HasRows Then
    '                Me.tb_kurs.Text = FormatNumber(reader.Item("kurs_bulanan").ToString, 2)
    '            End If
    '            reader.Close()
    '            connection.koneksi.CloseKoneksi()
    '        End If
    '    Catch ex As Exception
    '        tradingClass.Alert(ex.Message, Me.Page)
    '    End Try
    'End Sub

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
            Dim id_periode As Integer = 0
            Dim vtgl As String = Me.tb_tgl_bayar.Text.Substring(3, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(0, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(6, 4)

            sqlcom = "select id from transaction_period"
            sqlcom = sqlcom + " where tgl_awal <= '" & vtgl & "' and tgl_akhir >= '" & vtgl & "'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                id_periode = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            'debet
            ' akun komisi bank -> akun kas/bank

            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRBIAYALC','" & Me.vakun_komisi_bank & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(Me.lbl_komisi_bank.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ",0, 'Pembayaran biaya L/C no. " & Me.vno_lc & " (Komisi Bank, L/C no. " & Me.lbl_no_lc.Text & ")'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & "," & Decimal.ToDouble(Me.lbl_komisi_bank.Text) & ",0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun kas/bank -> akun komisi bank
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRBIAYALC','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & Me.vakun_komisi_bank & "', 0," & Decimal.ToDouble(Me.lbl_komisi_bank.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ", 'Pembayaran biaya L/C no. " & Me.vno_lc & " (Komisi Bank, L/C no. " & Me.lbl_no_lc.Text & ")'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ", 0," & Decimal.ToDouble(Me.lbl_komisi_bank.Text) & ")"
            connection.koneksi.InsertRecord(sqlcom)

            '=============================
            'debet
            ' akun ongkos kawat -> akun kas/bank

            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRBIAYALC','" & Me.vakun_ongkos_kawat & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(Me.lbl_ongkos_kawat.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ",0, 'Pembayaran biaya L/C no. " & Me.vno_lc & " (Ongkos kawat, L/C no. " & Me.lbl_no_lc.Text & ")'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & "," & Decimal.ToDouble(Me.lbl_ongkos_kawat.Text) & ",0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun kas/bank -> akun ongkos kawat
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRBIAYALC','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & Me.vakun_ongkos_kawat & "', 0," & Decimal.ToDouble(Me.lbl_ongkos_kawat.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ", 'Pembayaran biaya L/C no. " & Me.vno_lc & " (Ongkos kawat, L/C no. " & Me.lbl_no_lc.Text & ")'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ", 0," & Decimal.ToDouble(Me.lbl_porto_materai.Text) & ")"
            connection.koneksi.InsertRecord(sqlcom)

            '=============================
            'debet
            ' akun porto materai -> akun kas/bank

            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRBIAYALC','" & Me.vakun_porto_materai & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(Me.lbl_porto_materai.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ",0, 'Pembayaran biaya L/C no. " & Me.vno_lc & " (Porto materai, L/C no. " & Me.lbl_no_lc.Text & ")'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & "," & Decimal.ToDouble(Me.lbl_porto_materai.Text) & ",0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun kas/bank -> akun ongkos kawat
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRBIAYALC','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & Me.vakun_porto_materai & "', 0," & Decimal.ToDouble(Me.lbl_porto_materai.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ", 'Pembayaran biaya L/C no. " & Me.vno_lc & " (Porto materai, L/C no. " & Me.lbl_no_lc.Text & ")'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ", 0," & Decimal.ToDouble(Me.lbl_porto_materai.Text) & ")"
            connection.koneksi.InsertRecord(sqlcom)

            '=============================
            'debet
            ' akun biaya courier -> akun kas/bank

            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRBIAYALC','" & Me.vakun_biaya_courier & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(Me.lbl_biaya_courier.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ",0, 'Pembayaran biaya L/C no. " & Me.vno_lc & " (Biaya courier, L/C no. " & Me.lbl_no_lc.Text & ")'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & "," & Decimal.ToDouble(Me.lbl_biaya_courier.Text) & ",0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun kas/bank -> akun ongkos kawat
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRBIAYALC','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & Me.vakun_biaya_courier & "', 0," & Decimal.ToDouble(Me.lbl_biaya_courier.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ", 'Pembayaran biaya L/C no. " & Me.vno_lc & " (Biaya courier, L/C no. " & Me.lbl_no_lc.Text & ")'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ", 0," & Decimal.ToDouble(Me.lbl_biaya_courier.Text) & ")"
            connection.koneksi.InsertRecord(sqlcom)

            '=============================
            'debet
            ' akun biaya L/C amendment -> akun kas/bank

            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRBIAYALC','" & Me.vakun_biaya_lc_amendment & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(Me.lbl_biaya_lc_amendment.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ",0, 'Pembayaran biaya L/C no. " & Me.vno_lc & " (Biaya L/C Amendment, L/C no. " & Me.lbl_no_lc.Text & ")'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & "," & Decimal.ToDouble(Me.lbl_biaya_lc_amendment.Text) & ",0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun kas/bank -> akun ongkos kawat
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRBIAYALC','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & Me.vakun_biaya_lc_amendment & "', 0," & Decimal.ToDouble(Me.lbl_biaya_lc_amendment.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ", 'Pembayaran biaya L/C no. " & Me.vno_lc & " (Biaya L/C Amendment, L/C no. " & Me.lbl_no_lc.Text & ")'"
            sqlcom = sqlcom + "," & id_periode & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ", 0," & Decimal.ToDouble(Me.lbl_biaya_lc_amendment.Text) & ")"
            connection.koneksi.InsertRecord(sqlcom)

            
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            sqlcom = "select convert(char, tgl_bayar_biaya_lc, 103) as tgl_bayar_biaya_lc"
            sqlcom = sqlcom + " from lc"
            sqlcom = sqlcom + " where seq = " & Me.vno_lc
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If String.IsNullOrEmpty(reader.Item("tgl_bayar_biaya_lc").ToString) Then
                    Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran terlebih dahulu"
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    Exit Sub
                End If
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            ' cek akun biaya lc
            sqlcom = " select akun_komisi_bank, akun_ongkos_kawat, akun_porto_materai, akun_biaya_courier, akun_biaya_lc_amendment"
            sqlcom = sqlcom + " from akun_biaya_lc"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_komisi_bank = reader.Item("akun_komisi_bank").ToString
                Me.vakun_ongkos_kawat = reader.Item("akun_ongkos_kawat").ToString
                Me.vakun_porto_materai = reader.Item("akun_porto_materai").ToString
                Me.vakun_biaya_courier = reader.Item("akun_biaya_courier").ToString
                Me.vakun_biaya_lc_amendment = reader.Item("akun_biaya_lc_amendment").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_komisi_bank) Then
                Me.lbl_msg.Text = "Akun komisi bank tidak ada pada Akun biaya L/C"
                Exit Sub
            End If

            If String.IsNullOrEmpty(Me.vakun_ongkos_kawat) Then
                Me.lbl_msg.Text = "Akun ongkos kawat tidak ada pada Akun biaya L/C"
                Exit Sub
            End If

            If String.IsNullOrEmpty(Me.vakun_porto_materai) Then
                Me.lbl_msg.Text = "Akun porto materai tidak ada pada Akun biaya L/C"
                Exit Sub
            End If

            If String.IsNullOrEmpty(Me.vakun_biaya_courier) Then
                Me.lbl_msg.Text = "Akun biaya courier tidak ada pada Akun biaya L/C"
                Exit Sub
            End If

            If String.IsNullOrEmpty(Me.vakun_biaya_lc_amendment) Then
                Me.lbl_msg.Text = "Akun biaya L/C Amendment tidak ada pada Akun biaya L/C"
                Exit Sub
            End If

            'cek akun bank account
            Dim saldo_akhir As Decimal = 0
            sqlcom = "select account_code, isnull(saldo_akhir,0) as saldo_akhir from bank_account where id = " & Me.dd_bank.SelectedValue
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

            'If Decimal.ToDouble(saldo_akhir) < Decimal.ToDouble(Me.lbl_total_biaya.Text) Then
            'Me.lbl_msg.Text = "Saldo akhir pada Kas/Bank tersebut tidak mencukupi"
            'Exit Sub
            'End If

            sqlcom = "update lc set is_submit_bayar = 'S'"
            sqlcom = sqlcom + " where seq = " & Me.vno_lc
            connection.koneksi.UpdateRecord(sqlcom)

            'Daniel
            'Me.jurnal()
            Me.GL()
            'Daniel

            Me.loaddata()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_kurs_idr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_idr.Click
        Me.tb_kurs.Text = "1.00"
    End Sub

    Protected Sub btn_kurs_usd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_usd.Click
        Me.tb_kurs.Text = tradingClass.KursBulanan("[kurs_bulanan]", Me.vid_periode_transaksi)
    End Sub
End Class

