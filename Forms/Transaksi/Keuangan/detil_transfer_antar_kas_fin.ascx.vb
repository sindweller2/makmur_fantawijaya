Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_transfer_antar_kas_fin
    Inherits System.Web.UI.UserControl
    'Daniel
    Public tradingClass As New tradingClass()

    Sub GL()

        Try
            Dim nilai_awal As Decimal = System.Convert.ToDecimal(Me.tb_nilai_awal.Text) * System.Convert.ToDecimal(Me.tb_kurs_awal.Text)
            Dim nilai_akhir As Decimal = System.Convert.ToDecimal(Me.tb_nilai_akhir.Text) * System.Convert.ToDecimal(Me.tb_kurs_akhir.Text)
            Dim id As String = Me.tradingClass.IDTransaksiMax


            Dim keterangan As String = "Transfer antar kas/bank no. " & Me.lbl_no_transaksi.Text & " (" & Me.tb_keterangan.Text & ")"
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), id, Me.tradingClass.JurnalType("5"), keterangan, Me.id_transaction_period)

            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), vakun_kas_tujuan, vakun_kas_asal, nilai_awal, 0, keterangan, Me.id_transaction_period, Me.lbl_mata_uang_asal.Text, System.Convert.ToDecimal(Me.tb_kurs_awal.Text), System.Convert.ToDecimal(Me.tb_nilai_awal.Text), 0, Me.tb_no_voucher.Text.Trim())
            'Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.seq_max, id, Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), "82.01", vakun_kas_tujuan, 0, 0, keterangan, Me.id_transaction_period, Me.lbl_mata_uang_asal.Text, d_kurs_awal, System.Convert.ToDecimal(Me.tb_nilai_awal.Text), 0, Me.tb_no_voucher.Text.Trim())
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), vakun_kas_asal, vakun_kas_tujuan, 0, nilai_akhir, keterangan, Me.id_transaction_period, Me.lbl_mata_uang_tujuan.Text, System.Convert.ToDecimal(Me.tb_kurs_akhir.Text), 0, System.Convert.ToDecimal(Me.tb_nilai_akhir.Text), Me.tb_no_voucher.Text.Trim())
            'Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.seq_max, id, Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), "82.01", vakun_kas_asal, 0, 0, keterangan, Me.id_transaction_period, Me.lbl_mata_uang_tujuan.Text, d_kurs_akhir, 0, System.Convert.ToDecimal(Me.tb_nilai_akhir.Text), Me.tb_no_voucher.Text.Trim())

            'insert history kas
            ' kas asal
            Me.max_seq_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq, no_voucher)"
            sqlcom = sqlcom + " values(" & Me.id_transaction_period & "," & Me.dd_kas_bank_asal.SelectedValue & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text) & "','"
            sqlcom = sqlcom + keterangan
            sqlcom = sqlcom + "',0," & nilai_awal & "," & Me.vmax_history_kas & ",'" & Me.tb_no_voucher.Text.Trim() & "')"
            connection.koneksi.InsertRecord(sqlcom)

            ' kas tujuan
            Me.max_seq_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq, no_voucher)"
            sqlcom = sqlcom + " values(" & Me.id_transaction_period & "," & Me.dd_kas_bank_tujuan.SelectedValue & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text) & "','"
            sqlcom = sqlcom + keterangan
            sqlcom = sqlcom + "'," & nilai_akhir & ",0," & Me.vmax_history_kas & ",'" & Me.tb_no_voucher.Text.Trim() & "')"
            connection.koneksi.InsertRecord(sqlcom)

            'update kas
            'kas asal
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - " & nilai_awal
            sqlcom = sqlcom + " where id = " & Me.dd_kas_bank_asal.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)

            'kas tujuan
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) + " & nilai_akhir
            sqlcom = sqlcom + " where id = " & Me.dd_kas_bank_tujuan.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)

            Me.loaddata()
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

    Private ReadOnly Property vid() As Integer
        Get
            Dim o As Object = Request.QueryString("vid")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vpaging() As Integer
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property id_transaksi() As Integer
        Get
            Dim o As Object = ViewState("id_transaksi")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_transaksi") = value
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

    Public Property id_transaction_period() As Integer
        Get
            Dim o As Object = ViewState("id_transaction_period")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_transaction_period") = value
        End Set
    End Property

    Public Property id_user() As Integer
        Get
            Dim o As Object = ViewState("id_user")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_user") = value
        End Set
    End Property

    Public Property vakun_kas_asal() As String
        Get
            Dim o As Object = ViewState("vakun_kas_asal")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_kas_asal") = value
        End Set
    End Property

    Public Property vakun_kas_tujuan() As String
        Get
            Dim o As Object = ViewState("vakun_kas_tujuan")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_kas_tujuan") = value
        End Set
    End Property

    Private ReadOnly Property voption() As Integer
        Get
            Dim o As Object = Request.QueryString("voption")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vsearch() As String
        Get
            Dim o As Object = Request.QueryString("vsearch")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiodepembayaran()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where tahun = " & Me.vtahun
        sqlcom = sqlcom + " and bulan = " & Me.vbulan
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.id_transaction_period = reader.Item("id").ToString
            Me.lbl_periode_transaksi.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindcashaccount_asal()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from bank_account"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_kas_bank_asal.DataSource = reader
        Me.dd_kas_bank_asal.DataTextField = "name"
        Me.dd_kas_bank_asal.DataValueField = "id"
        Me.dd_kas_bank_asal.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindcashaccount_tujuan()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from bank_account"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_kas_bank_tujuan.DataSource = reader
        Me.dd_kas_bank_tujuan.DataTextField = "name"
        Me.dd_kas_bank_tujuan.DataValueField = "id"
        Me.dd_kas_bank_tujuan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindmatauangasal()
        Try
            sqlcom = "select id_currency from bank_account where id = " & Me.dd_kas_bank_asal.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_mata_uang_asal.Text = reader.Item("id_currency").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindmatauangtujuan()
        Try
            sqlcom = "select id_currency from bank_account where id = " & Me.dd_kas_bank_tujuan.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_mata_uang_tujuan.Text = reader.Item("id_currency").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loaddata()

        If Me.vid <> 0 Then
            Me.id_transaksi = Me.vid
        End If

        sqlcom = "select seq, convert(char, tanggal, 103) as tanggal, id_cash_bank_asal, id_cash_bank_tujuan,"
        sqlcom = sqlcom + " isnull(nilai,0) as nilai, isnull(nilai_akhir,0) as nilai_akhir, isnull(kurs,0) as kurs, isnull(kurs_nilai_akhir,0) as kurs_nilai_akhir,"
        sqlcom = sqlcom + " isnull(nilai_biaya_provisi,0) as nilai_biaya_provisi, keterangan, dibuat_oleh, is_submit,"
        sqlcom = sqlcom + " convert(char, tanggal_transfer, 103) as tgl_transfer, id_transaction_period,"
        sqlcom = sqlcom + " convert(char, tanggal_masuk, 103) as tanggal_masuk, no_voucher"
        sqlcom = sqlcom + " from transfer_antar_kas"
        sqlcom = sqlcom + " where seq = " & Me.id_transaksi
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_no_transaksi.Text = reader.Item("seq").ToString
            Me.tb_tgl_transaksi.Text = reader.Item("tanggal").ToString
            Me.tb_tgl_transfer.Text = reader.Item("tgl_transfer").ToString
            Me.tb_tgl_masuk.Text = reader.Item("tanggal_masuk").ToString
            Me.dd_kas_bank_asal.SelectedValue = reader.Item("id_cash_bank_asal").ToString
            Me.dd_kas_bank_tujuan.SelectedValue = reader.Item("id_cash_bank_tujuan").ToString
            Me.tb_nilai_awal.Text = FormatNumber(reader.Item("nilai").ToString, 2)
            Me.tb_nilai_akhir.Text = FormatNumber(reader.Item("nilai_akhir").ToString, 2)
            Me.tb_kurs_awal.Text = FormatNumber(reader.Item("kurs").ToString, 2)
            Me.tb_kurs_akhir.Text = FormatNumber(reader.Item("kurs_nilai_akhir").ToString, 2)
            Me.tb_nilai_biaya_provisi.Text = FormatNumber(reader.Item("nilai_biaya_provisi").ToString, 2)
            Me.tb_keterangan.Text = reader.Item("keterangan").ToString
            Me.tb_no_voucher.Text = reader.Item("no_voucher").ToString

            If reader.Item("is_submit").ToString = "S" Then
                Me.btn_save.Enabled = False
                Me.btn_submit.Enabled = False
                Me.btn_unsubmit.Enabled = True
                Me.dd_kas_bank_asal.Enabled = False
                Me.dd_kas_bank_tujuan.Enabled = False
                Me.tb_tgl_transaksi.Enabled = False
                Me.tb_keterangan.Enabled = False
                Me.tb_nilai_awal.Enabled = False
                Me.tb_nilai_akhir.Enabled = False
                Me.lbl_status_submit.Text = "Data sudah disubmit"
            ElseIf reader.Item("is_submit").ToString = "B" Then
                Me.btn_save.Enabled = True
                Me.btn_submit.Enabled = True
                Me.btn_unsubmit.Enabled = False
                Me.dd_kas_bank_asal.Enabled = True
                Me.dd_kas_bank_tujuan.Enabled = True
                Me.tb_tgl_transaksi.Enabled = True
                Me.tb_keterangan.Enabled = True
                Me.tb_nilai_awal.Enabled = True
                Me.tb_nilai_akhir.Enabled = True
                Me.lbl_status_submit.Text = "Data belum disubmit"
            End If

            Me.bindmatauangasal()
            Me.bindmatauangtujuan()
        Else
            Me.btn_submit.Enabled = False
            Me.btn_unsubmit.Enabled = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.id_user = HttpContext.Current.Session("UserID")
            Me.bindperiodepembayaran()
            Me.bindcashaccount_asal()
            Me.bindcashaccount_tujuan()
            Me.bindmatauangasal()
            Me.bindmatauangtujuan()
            Me.tb_tgl_transaksi.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.loaddata()
            tradingClass.ViewButtonUnsubmit(Me.btn_unsubmit)
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/daftar_transfer_antar_kas_fin.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&voption=" & Me.voption & "&vsearch=" & Me.vsearch & "&vpaging=" & Me.vpaging)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tgl_transaksi.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal transaksi terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_transfer.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal transfer terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_masuk.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal masuk terlebih dahulu"
            ElseIf Me.dd_kas_bank_asal.SelectedValue = Me.dd_kas_bank_tujuan.SelectedValue Then
                Me.lbl_msg.Text = "Kas/Bank asal tidak boleh sama dengan Kas/Bank tujuan"
                'ElseIf Me.lbl_mata_uang_asal.Text <> Me.lbl_mata_uang_tujuan.Text Then
                'Me.lbl_msg.Text = "Mata uang Kas/Bank asal harus sama dengan mata uang Kas/Bank tujuan"
            ElseIf String.IsNullOrEmpty(Me.tb_nilai_awal.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nilai kas/bank awal terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_nilai_akhir.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nilai kas/bank akhir terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_kurs_awal.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi kurs kas/bank awal terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_kurs_akhir.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi kurs  kas/bank akhir terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_nilai_biaya_provisi.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nilai biaya provisi terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_keterangan.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi keterangan terlebih dahulu"
            Else
                Dim vtgl As String = Me.tb_tgl_transaksi.Text.Substring(3, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(0, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(6, 4)
                Dim vtgl_transfer As String = Me.tb_tgl_transfer.Text.Substring(3, 2) & "/" & Me.tb_tgl_transfer.Text.Substring(0, 2) & "/" & Me.tb_tgl_transfer.Text.Substring(6, 4)
                Dim vtgl_masuk As String = Me.tb_tgl_masuk.Text.Substring(3, 2) & "/" & Me.tb_tgl_masuk.Text.Substring(0, 2) & "/" & Me.tb_tgl_masuk.Text.Substring(6, 4)

                If Me.id_transaksi = 0 Then
                    Dim vmax As Integer = 0
                    sqlcom = "select isnull(max(convert(int, right(seq, 5))),0) + 1 as vid"
                    sqlcom = sqlcom + " from transfer_antar_kas"
                    sqlcom = sqlcom + " where convert(int, substring(convert(char, seq), 3,2)) = " & Me.vbulan
                    sqlcom = sqlcom + " and convert(int, left(seq, 2)) = " & Right(Me.vtahun, 2)
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = Right(Me.vtahun, 2) & Me.vbulan.ToString.PadLeft(2, "0") & reader.Item("vid").ToString.PadLeft(5, "0")
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into transfer_antar_kas(seq, tanggal, tanggal_transfer, id_cash_bank_asal, id_cash_bank_tujuan,"
                    sqlcom = sqlcom + " nilai, keterangan, dibuat_oleh, is_submit, id_transaction_period, nilai_akhir, kurs, kurs_nilai_akhir,"
                    sqlcom = sqlcom + " tanggal_masuk, nilai_biaya_provisi, no_voucher)"
                    sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "','" & vtgl_transfer & "'," & Me.dd_kas_bank_asal.SelectedValue & "," & Me.dd_kas_bank_tujuan.SelectedValue
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_nilai_awal.Text) & ",'" & Me.tb_keterangan.Text & "','1','B'," & Me.id_transaction_period
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_nilai_awal.Text) & "," & Decimal.ToDouble(Me.tb_kurs_awal.Text) & "," & Decimal.ToDouble(Me.tb_kurs_akhir.Text)
                    sqlcom = sqlcom + ",'" & vtgl_masuk & "'," & Decimal.ToDouble(tb_nilai_biaya_provisi.Text) & ",'" & Me.tb_no_voucher.Text.Trim() & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.id_transaksi = vmax
                    tradingClass.Alert("Data sudah disimpan", Me.Page)
                Else
                    sqlcom = "update transfer_antar_kas"
                    sqlcom = sqlcom + " set id_cash_bank_asal = " & Me.dd_kas_bank_asal.SelectedValue & ","
                    sqlcom = sqlcom + " tanggal = '" & vtgl & "',"
                    sqlcom = sqlcom + " tanggal_transfer = '" & vtgl_transfer & "',"
                    sqlcom = sqlcom + " id_cash_bank_tujuan = " & Me.dd_kas_bank_tujuan.SelectedValue & ","
                    sqlcom = sqlcom + " nilai = " & Decimal.ToDouble(Me.tb_nilai_awal.Text) & ","
                    sqlcom = sqlcom + " keterangan = '" & Me.tb_keterangan.Text & "',"
                    sqlcom = sqlcom + " nilai_akhir = " & Decimal.ToDouble(Me.tb_nilai_akhir.Text) & ","
                    sqlcom = sqlcom + " kurs = " & Decimal.ToDouble(Me.tb_kurs_awal.Text) & ","
                    sqlcom = sqlcom + " kurs_nilai_akhir = " & Decimal.ToDouble(Me.tb_kurs_akhir.Text) & ","
                    sqlcom = sqlcom + " tanggal_masuk = '" & vtgl_masuk & "',"
                    sqlcom = sqlcom + " nilai_biaya_provisi = " & Decimal.ToDouble(Me.tb_nilai_biaya_provisi.Text) & ","
                    sqlcom = sqlcom + " no_voucher = '" & Me.tb_no_voucher.Text.Trim() & "'"
                    sqlcom = sqlcom + " where seq = " & Me.id_transaksi
                    connection.koneksi.UpdateRecord(sqlcom)
                    tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
                Me.loaddata()

            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub seq_max()
        sqlcom = "select isnull(max(seq),0) + 1 as vmax from akun_general_ledger"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.vmax = reader.Item("vmax").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub max_seq_history_kas()
        sqlcom = "select isnull(max(seq),0) + 1 as vmax from history_kas"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.vmax_history_kas = reader.Item("vmax").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub jurnal()

        Dim vtgl As String = Me.tb_tgl_transfer.Text.Substring(3, 2) & "/" & Me.tb_tgl_transfer.Text.Substring(0, 2) & "/" & Me.tb_tgl_transfer.Text.Substring(6, 4)
        Dim vtgl_masuk As String = Me.tb_tgl_masuk.Text.Substring(3, 2) & "/" & Me.tb_tgl_masuk.Text.Substring(0, 2) & "/" & Me.tb_tgl_masuk.Text.Substring(6, 4)

        Dim vnilai_masuk As Decimal = 0
        If Decimal.ToDouble(Me.tb_nilai_biaya_provisi.Text) = 0 Then
            vnilai_masuk = Decimal.ToDouble(Me.tb_nilai_akhir.Text)
        Else
            vnilai_masuk = Decimal.ToDouble(Me.tb_nilai_akhir.Text) - Decimal.ToDouble(Me.tb_nilai_biaya_provisi.Text)
        End If

        'kas asal
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl & "','TRNFKAS','" & vakun_kas_asal & "',"
        sqlcom = sqlcom + "'" & vakun_kas_tujuan & "',0," & Decimal.ToDouble(Me.tb_nilai_awal.Text) & ", 'Transfer antar kas/bank no. " & Me.lbl_no_transaksi.Text & " (" & Me.tb_keterangan.Text & ")'"
        sqlcom = sqlcom + "," & Me.id_transaction_period & ",'" & Me.lbl_mata_uang_asal.Text & "'," & Decimal.ToDouble(Me.tb_kurs_awal.Text)

        If Me.lbl_mata_uang_tujuan.Text = "IDR" Then
            sqlcom = sqlcom + ", 0, 0)"
        Else
            sqlcom = sqlcom + ", 0, " & Decimal.ToDouble(Me.tb_nilai_awal.Text) & ")"
        End If

        connection.koneksi.InsertRecord(sqlcom)


        ' kas tujuan
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl_masuk & "','TRNFKAS','" & vakun_kas_tujuan & "',"
        sqlcom = sqlcom + "'" & vakun_kas_asal & "'," & Decimal.ToDouble(vnilai_masuk) & ",0, 'Transfer antar kas/bank no. " & Me.lbl_no_transaksi.Text & " (" & Me.tb_keterangan.Text & ")'"
        sqlcom = sqlcom + "," & Me.id_transaction_period & ",'" & Me.lbl_mata_uang_tujuan.Text & "'," & Decimal.ToDouble(Me.tb_kurs_awal.Text)

        If Me.lbl_mata_uang_tujuan.Text = "IDR" Then
            sqlcom = sqlcom + ", 0, 0)"
        Else
            sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_nilai_awal.Text) & ", 0)"
        End If
        connection.koneksi.InsertRecord(sqlcom)


        If Decimal.ToDouble(Me.tb_nilai_biaya_provisi.Text) <> 0 Then

            ' biaya provisi
            Dim vakun_biaya_provisi = "63.12"
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl_masuk & "','TRNFKAS','" & vakun_biaya_provisi & "',"
            sqlcom = sqlcom + "'" & vakun_kas_asal & "'," & Decimal.ToDouble(Me.tb_nilai_biaya_provisi.Text) & ",0, 'Transfer antar kas/bank no. " & Me.lbl_no_transaksi.Text & " (" & Me.tb_keterangan.Text & ")'"
            sqlcom = sqlcom + "," & Me.id_transaction_period & ",'" & Me.lbl_mata_uang_tujuan.Text & "'," & Decimal.ToDouble(Me.tb_kurs_awal.Text)

            If Me.lbl_mata_uang_tujuan.Text = "IDR" Then
                sqlcom = sqlcom + ", 0, 0)"
            Else
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_nilai_awal.Text) & ", 0)"
            End If
            connection.koneksi.InsertRecord(sqlcom)

        End If
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            Dim nilai_awal As Decimal = System.Convert.ToDecimal(Me.tb_nilai_awal.Text) * System.Convert.ToDecimal(Me.tb_kurs_awal.Text)
            Dim nilai_akhir As Decimal = System.Convert.ToDecimal(Me.tb_nilai_akhir.Text) * System.Convert.ToDecimal(Me.tb_kurs_akhir.Text)

            If nilai_awal = nilai_akhir Then
                'akun dan saldo akhir kas asal
                Dim vsaldo_akhir As Decimal = 0

                sqlcom = "select account_code, isnull(saldo_akhir,0) as saldo_akhir"
                sqlcom = sqlcom + " from bank_account"
                sqlcom = sqlcom + " where id = " & Me.dd_kas_bank_asal.SelectedValue
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.vakun_kas_asal = reader.Item("account_code").ToString.Trim
                    vsaldo_akhir = reader.Item("saldo_akhir").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                If String.IsNullOrEmpty(Me.vakun_kas_asal) Then
                    Me.lbl_msg.Text = "Kode akun pada Kas/Bank asal tidak ada"
                    Exit Sub
                End If

                'If Decimal.ToDouble(vsaldo_akhir) < Decimal.ToDouble(Me.tb_nilai.Text) Then
                'Me.lbl_msg.Text = "Saldo akhir Kas/Bank asal tersebut tidak mencukupi"
                'Exit Sub
                'End If

                'akun kas tujuan
                sqlcom = "select account_code"
                sqlcom = sqlcom + " from bank_account"
                sqlcom = sqlcom + " where id = " & Me.dd_kas_bank_tujuan.SelectedValue
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.vakun_kas_tujuan = reader.Item("account_code").ToString.Trim
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                If String.IsNullOrEmpty(Me.vakun_kas_tujuan) Then
                    Me.lbl_msg.Text = "Kode akun pada Kas/Bank tujuan tidak ada"
                    Exit Sub
                End If

                sqlcom = "update transfer_antar_kas"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where seq = " & Me.id_transaksi
                connection.koneksi.UpdateRecord(sqlcom)

                '<--adien 24 des 2013
                'Me.jurnal()
                Me.GL()
                'adien-->
                Me.loaddata()

            Else
                tradingClass.Alert("Nilai kas bank awal dan bank akhir tidak sama", Me.Page)
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub dd_kas_bank_asal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_kas_bank_asal.SelectedIndexChanged
        Me.bindmatauangasal()
    End Sub

    Protected Sub dd_kas_bank_tujuan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_kas_bank_tujuan.SelectedIndexChanged
        Me.bindmatauangtujuan()
    End Sub

    Protected Sub btn_unsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_unsubmit.Click
        Try
            Dim nilai_awal As Decimal = System.Convert.ToDecimal(Me.tb_nilai_awal.Text) * System.Convert.ToDecimal(Me.tb_kurs_awal.Text)
            Dim nilai_akhir As Decimal = System.Convert.ToDecimal(Me.tb_nilai_akhir.Text) * System.Convert.ToDecimal(Me.tb_kurs_akhir.Text)

            sqlcom = Nothing

            sqlcom = "update transfer_antar_kas"
            sqlcom = sqlcom + " set is_submit = 'B'"
            sqlcom = sqlcom + " where seq = " & Me.id_transaksi
            connection.koneksi.UpdateRecord(sqlcom)

            Dim keterangan As String = "Transfer antar kas/bank no. " & Me.lbl_no_transaksi.Text & " (" & Me.tb_keterangan.Text & ")"

            Me.tradingClass.DeleteAkunJurnal(keterangan, Me.id_transaction_period)
            Me.tradingClass.DeleteAkunGeneralLedger(keterangan, Me.id_transaction_period)

            sqlcom = Nothing
            sqlcom = " delete from history_kas "
            sqlcom = sqlcom + " where id_transaction_period = '" & Me.id_transaction_period & "' "
            sqlcom = sqlcom + " and id_cash_bank = '" & Me.dd_kas_bank_asal.SelectedValue & "' "
            sqlcom = sqlcom + " and tanggal = '" & Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text) & "' "
            sqlcom = sqlcom + " and keterangan = '" & keterangan & "' "
            sqlcom = sqlcom + " and nilai_debet = '0' "
            sqlcom = sqlcom + " and nilai_kredit = '" & nilai_awal & "' "
            sqlcom = sqlcom + " and no_voucher = '" & Me.tb_no_voucher.Text.Trim() & "' "
            connection.koneksi.DeleteRecord(sqlcom)

            sqlcom = Nothing
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) + " & nilai_awal
            sqlcom = sqlcom + " where id = " & Me.dd_kas_bank_asal.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)

            sqlcom = Nothing
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - " & nilai_akhir
            sqlcom = sqlcom + " where id = " & Me.dd_kas_bank_tujuan.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)

            Me.loaddata()
            tradingClass.Alert("Data sudah diunsubmit!", Me.Page)
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class
