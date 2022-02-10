Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_detil_dokumen_impor
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()

    Sub GL()
        Try
            Dim idr As Decimal = System.Convert.ToDecimal(Me.lbl_total_nilai.Text) * System.Convert.ToDecimal(Me.tb_kurs.Text)
            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim keterangan As String = "Entry Dokumen Impor no. " & Me.lbl_no_po.Text
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_terima.Text), id, Me.tradingClass.JurnalType("2"), keterangan, Me.vid_periode_transaksi)

            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_terima.Text), Me.vakun_pembelian_impor, Me.vakun_hutang_dagang, idr, 0, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, Me.tb_kurs.Text, System.Convert.ToDecimal(Me.lbl_total_nilai.Text), 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_terima.Text), Me.vakun_hutang_dagang, Me.vakun_pembelian_impor, 0, idr, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, Me.tb_kurs.Text, 0, System.Convert.ToDecimal(Me.lbl_total_nilai.Text), String.Empty)

            Me.tradingClass.Alert("Data sudah disubmit!", Me.Page)
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
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

    Private ReadOnly Property vpaging() As string
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return Cstr(o) Else Return Nothing
        End Get
    End Property

    Private ReadOnly Property vno_dokumen() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_dokumen")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vpilihan() As string
        Get
            Dim o As Object = Request.QueryString("vpilihan")
            If String.IsNullOrEmpty(o) = False Then Return Cstr(o) Else Return Nothing
        End Get
    End Property

    Private ReadOnly Property vsearch() As string
        Get
            Dim o As Object = Request.QueryString("vsearch")
            If String.IsNullOrEmpty(o) = False Then Return Cstr(o) Else Return Nothing
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

    Public Property no_dokumen() As Integer
        Get
            Dim o As Object = ViewState("no_dokumen")
            If Not o Is Nothing Then Return CInt(o) Else Return Nothing
        End Get
        Set(ByVal value As Integer)
            ViewState("no_dokumen") = value
        End Set
    End Property

    Public Property vis_lc() As Boolean
        Get
            Dim o As Object = ViewState("vis_lc")
            If Not o Is Nothing Then Return CBool(o) Else Return False
        End Get
        Set(ByVal value As Boolean)
            ViewState("vis_lc") = value
        End Set
    End Property

    Public Property vid_supplier() As Integer
        Get
            Dim o As Object = ViewState("vid_supplier")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_supplier") = value
        End Set
    End Property

    Public Property vakun_hutang_dagang() As String
        Get
            Dim o As Object = ViewState("vakun_hutang_dagang")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_hutang_dagang") = value
        End Set
    End Property

    Public Property vakun_pembelian_impor() As String
        Get
            Dim o As Object = ViewState("vakun_pembelian_impor")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_pembelian_impor") = value
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

    Sub clearform()
        Me.no_dokumen = 0
        Me.tb_tgl_terima.Text = ""
        Me.tb_no_bl.Text = ""
        Me.tb_no_invoice.Text = ""
        Me.tb_tgl_invoice.Text = ""
        Me.tb_tgl_jatuh_tempo.Text = ""
        Me.tb_nilai_invoice.Text = ""
        Me.tb_no_packing_list.Text = ""
    End Sub

    Sub bindperiode_transaksi()
        sqlcom = "select id, name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_periode.Text = reader.Item("name").ToString
            Me.vid_periode_transaksi = reader.Item("id").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearpo()
        Me.tb_no_po.Text = 0
        Me.lbl_no_po.Text = "-----"
        Me.link_popup_no_po.Visible = True
    End Sub

    Sub bindpo()
        Dim readerpo As SqlClient.SqlDataReader
        sqlcom = "select purchase_order.no, purchase_order.po_no_text, convert(char, purchase_order.tanggal, 103) as tgl_po, purchase_order.is_lc,"
        sqlcom = sqlcom + " purchase_order.id_supplier, purchase_order.id_currency,"
        sqlcom = sqlcom + " case"
        sqlcom = sqlcom + " when purchase_order.is_lc = 'True' then 'Ya'"
        sqlcom = sqlcom + " when purchase_order.is_lc = 'False' then 'Bukan'"
        sqlcom = sqlcom + " end as is_lc_text,"
        sqlcom = sqlcom + " daftar_supplier.name as nama_supplier"
        sqlcom = sqlcom + " from purchase_order"
        sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
        sqlcom = sqlcom + " where purchase_order.no = " & Me.tb_no_po.Text
        readerpo = connection.koneksi.SelectRecord(sqlcom)
        readerpo.Read()
        If readerpo.HasRows Then
            Me.vid_supplier = readerpo.Item("id_supplier").ToString
            Me.lbl_no_po.Text = readerpo.Item("po_no_text").ToString
            Me.lbl_tgl_pembelian.Text = readerpo.Item("tgl_po").ToString
            Me.lbl_is_lc.Text = readerpo.Item("is_lc_text").ToString
            Me.lbl_nama_supplier.Text = readerpo.Item("nama_supplier").ToString
            Me.vis_lc = readerpo.Item("is_lc").ToString
            Me.lbl_mata_uang.Text = readerpo.Item("id_currency").ToString

            If readerpo.Item("is_lc").ToString = "True" Then
                Me.popup_lc()
            Else
                Me.tb_no_lc.Text = 0
                Me.clear_popup_lc()
            End If

        End If
        readerpo.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearlc()
        Me.tb_no_lc.Text = 0
        Me.lbl_nomor_lc.Text = "-----"
        Me.link_popup_no_lc.Visible = True
    End Sub

    Sub bindlc()
        Dim readerlc As SqlClient.SqlDataReader
        sqlcom = "select no_lc"
        sqlcom = sqlcom + " from lc"
        sqlcom = sqlcom + " where seq = " & Me.tb_no_lc.Text
        readerlc = connection.koneksi.SelectRecord(sqlcom)
        readerlc.Read()
        If readerlc.HasRows Then
            Me.lbl_nomor_lc.Text = readerlc.Item("no_lc").ToString
        End If
        readerlc.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loaddata()
        Try

            If Me.vno_dokumen <> 0 Then
                Me.no_dokumen = Me.vno_dokumen
            End If

            sqlcom = "select seq, no_po, convert(char, tgl_terima, 103) as tgl_terima, bl_no, invoice_no, convert(char, tgl_invoice, 103) as tgl_invoice,"
            sqlcom = sqlcom + " convert(char, tgl_jatuh_tempo_invoice_supplier, 103) as tgl_jatuh_tempo_invoice_supplier,"
            sqlcom = sqlcom + " isnull(nilai_invoice,0) as nilai_invoice, isnull(kurs,0) as kurs, packing_list_no, seq_lc, is_submit, "
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when is_lc = 'True' then 'Ya'"
            sqlcom = sqlcom + " when is_lc = 'False' then 'Bukan'"
            sqlcom = sqlcom + " end as is_lc,"
            sqlcom = sqlcom + " (select no_lc from lc where seq = entry_dokumen_impor.seq_lc) as no_lc,"
            sqlcom = sqlcom + " (select name from transaction_period where id = entry_dokumen_impor.id_transaction_period) as bulan"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq = " & Me.vno_dokumen
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_penerimaan.Text = reader.Item("seq").ToString
                Me.tb_no_po.Text = reader.Item("no_po").ToString
                Me.tb_tgl_terima.Text = reader.Item("tgl_terima").ToString
                Me.tb_no_bl.Text = reader.Item("bl_no").ToString
                Me.tb_no_invoice.Text = reader.Item("invoice_no").ToString
                Me.tb_tgl_invoice.Text = reader.Item("tgl_invoice").ToString
                Me.tb_tgl_jatuh_tempo.Text = reader.Item("tgl_jatuh_tempo_invoice_supplier").ToString
                Me.tb_nilai_invoice.Text = FormatNumber(reader.Item("nilai_invoice").ToString, 2)
                Me.tb_kurs.Text = FormatNumber(reader.Item("kurs").ToString, 2)
                Me.tb_no_packing_list.Text = reader.Item("packing_list_no").ToString

                If Not String.IsNullOrEmpty(reader.Item("seq_lc").ToString) Then
                    Me.tb_no_lc.Text = reader.Item("seq_lc").ToString
                    Me.popup_lc()
                Else
                    Me.tb_no_lc.Text = 0
                    Me.clear_popup_lc()
                End If

                If reader.Item("is_submit").ToString = "B" Then
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                    Me.btn_unsubmit.Enabled = False
                Else
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                    Me.btn_unsubmit.Enabled = True
                End If

                Me.bindpo()
                Me.bindlc()
            Else
                Me.btn_submit.Enabled = False
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindkursbulanan()
        Try
            sqlcom = "select isnull(kurs_bulanan,0) as kurs_bulanan"
            sqlcom = sqlcom + " from transaction_period"
            sqlcom = sqlcom + " where id = " & Me.vid_periode_transaksi
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_kurs.Text = FormatNumber(reader.Item("kurs_bulanan").ToString, 2)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            tradingClass.ViewButtonUnsubmit(Me.btn_unsubmit)
            Me.clearpo()
            Me.clearlc()
            Me.clearform()
            Me.tb_tgl_terima.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.bindperiode_transaksi()
            Me.bindkursbulanan()
            Me.loaddata()
            Me.cek_produk()
            Me.tb_no_po.Attributes.Add("style", "display: none;")
            Me.tb_no_lc.Attributes.Add("style", "display: none;")
            Me.link_refresh_no_po.Attributes.Add("style", "display: none;")
            Me.link_refresh_no_lc.Attributes.Add("style", "display: none;")
            Me.link_popup_no_po.Attributes.Add("onclick", "popup_po('" & Me.tb_no_po.ClientID & "','" & Me.link_refresh_no_po.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/entri_dokumen_impor.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&vpilihan=" & Me.vpilihan & "&vsearch=" & Me.vsearch & "&vpaging=" & Me.vpaging)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tgl_terima.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terima terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_no_bl.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi no. B/L / AWB terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_no_invoice.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi no. invoice terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_invoice.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tgl. invoice terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_jatuh_tempo.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tgl. jatuh tempo invoice terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_nilai_invoice.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nilai invoice terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_kurs.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nilai kurs terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_no_packing_list.Text) Then                
                Me.lbl_msg.Text = "Silahkan mengisi no. packing list terlebih dahulu"
            ElseIf Me.vis_lc = "True" And Me.tb_no_lc.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi no. L/C terlebih dahulu"
            Else

                Dim vtgl As String = Me.tb_tgl_invoice.Text.Substring(3, 2) & "/" & Me.tb_tgl_invoice.Text.Substring(0, 2) & "/" & Me.tb_tgl_invoice.Text.Substring(6, 4)
                Dim vtgl_terima As String = Me.tb_tgl_terima.Text.Substring(3, 2) & "/" & Me.tb_tgl_terima.Text.Substring(0, 2) & "/" & Me.tb_tgl_terima.Text.Substring(6, 4)
                Dim vtgl_jatuh_tempo As String = Me.tb_tgl_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(6, 4)


                If Me.no_dokumen = 0 Then
                    Dim vmax As String = ""
                    sqlcom = "select isnull(max(convert(int, right(seq, 5))),0) + 1 as vseq"
                    sqlcom = sqlcom + " from entry_dokumen_impor"
                    sqlcom = sqlcom + " where convert(int, substring(convert(char, seq), 3,2)) = " & vbulan
                    sqlcom = sqlcom + " and convert(int, left(seq, 2)) = " & Right(Me.vtahun, 2)
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = Right(Me.vtahun, 2) & vbulan.ToString.PadLeft(2, "0") & reader.Item("vseq").ToString.PadLeft(5, "0")
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into entry_dokumen_impor(no_po, seq, id_transaction_period, tgl_terima, packing_list_no, bl_no, invoice_no, tgl_invoice,"
                    sqlcom = sqlcom + " tgl_jatuh_tempo_invoice_supplier, nilai_invoice, kurs, is_lc, seq_lc, is_submit, is_submit_pib)"
                    sqlcom = sqlcom + " values(" & Me.tb_no_po.Text & "," & vmax & "," & Me.vid_periode_transaksi & ",'" & vtgl_terima & "','" & Me.tb_no_packing_list.Text & "','" & Me.tb_no_bl.Text & "',"
                    sqlcom = sqlcom + "'" & Me.tb_no_invoice.Text & "','" & vtgl & "','" & vtgl_jatuh_tempo & "'," & Decimal.ToDouble(Me.tb_nilai_invoice.Text)
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_kurs.Text) & ",'" & Me.vis_lc & "'"

                    If Me.tb_no_lc.Text <> 0 Then
                        sqlcom = sqlcom + "," & Me.tb_no_lc.Text & ",'B', 'B')"
                    Else
                        sqlcom = sqlcom + ", NULL,'B','B')"
                    End If

                    Me.no_dokumen = vmax
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah disimpan", Me.Page)
                Else
                    sqlcom = "update entry_dokumen_impor"
                    sqlcom = sqlcom + " set no_po = " & Me.tb_no_po.Text & ","
                    sqlcom = sqlcom + " is_lc = '" & Me.vis_lc & "',"

                    If Me.tb_no_lc.Text <> 0 Then
                        sqlcom = sqlcom + " seq_lc = " & Me.tb_no_lc.Text & ","
                    Else
                        sqlcom = sqlcom + " seq_lc = NULL,"
                    End If

                    sqlcom = sqlcom + " packing_list_no = '" & Me.tb_no_packing_list.Text & "',"
                    sqlcom = sqlcom + " bl_no = '" & Me.tb_no_bl.Text & "',"
                    sqlcom = sqlcom + " invoice_no = '" & Me.tb_no_invoice.Text & "',"
                    sqlcom = sqlcom + " tgl_invoice = '" & vtgl & "',"
                    sqlcom = sqlcom + " tgl_terima = '" & vtgl_terima & "',"
                    sqlcom = sqlcom + " tgl_jatuh_tempo_invoice_supplier = '" & vtgl_jatuh_tempo & "',"
                    sqlcom = sqlcom + " nilai_invoice = " & Decimal.ToDouble(Me.tb_nilai_invoice.Text) & ","
                    sqlcom = sqlcom + " kurs = " & Decimal.ToDouble(Me.tb_kurs.Text)
                    sqlcom = sqlcom + " where seq = " & Me.vno_dokumen
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clear_popup_lc()
        Me.link_popup_no_lc.Enabled = False
        Me.link_popup_no_lc.Attributes.Clear()
    End Sub

    Sub popup_lc()
        Me.link_popup_no_lc.Enabled = True
        Me.link_popup_no_lc.Attributes.Add("onclick", "popup_lc('" & Me.tb_no_lc.ClientID & "','" & Me.link_refresh_no_lc.UniqueID & "')")
    End Sub

    Protected Sub link_refresh_no_lc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_no_lc.Click
        Me.bindlc()
    End Sub

    Protected Sub link_refresh_no_po_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_no_po.Click
        Me.bindpo()
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

    Sub jurnal()
        Try
            Dim vtgl As String = Me.tb_tgl_terima.Text.Substring(3, 2) & "/" & Me.tb_tgl_terima.Text.Substring(0, 2) & "/" & Me.tb_tgl_terima.Text.Substring(6, 4)

            'debet
            ' akun_pembelian_impor -> akun hutang dagang

            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penerimaan.Text & "','" & vtgl & "','TRMINVSUPIMP','" & Me.vakun_pembelian_impor & "',"
            sqlcom = sqlcom + "'" & Me.vakun_hutang_dagang & "'," & Decimal.ToDouble(Me.tb_nilai_invoice.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ",0, 'Penerimaan invoice supplier impor TT no. " & Me.lbl_no_penerimaan.Text & "'"
            sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & "," & Decimal.ToDouble(Me.tb_nilai_invoice.Text) & ",0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun hutang dagang -> akun_pembelian_impor

            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penerimaan.Text & "','" & vtgl & "','TRMINVSUPIMP','" & Me.vakun_hutang_dagang & "',"
            sqlcom = sqlcom + "'" & Me.vakun_pembelian_impor & "',0," & Decimal.ToDouble(Me.tb_nilai_invoice.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ",'Penerimaan invoice supplier impor TT no. " & Me.lbl_no_penerimaan.Text & "'"
            sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ",0," & Decimal.ToDouble(Me.tb_nilai_invoice.Text) & ")"
            connection.koneksi.InsertRecord(sqlcom)

        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            'If Me.lbl_is_lc.Text = "Bukan" Then
            ' cek akun hutang dagang supplier
            sqlcom = "select akun_hutang_dagang from daftar_supplier where id = " & Me.vid_supplier
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_hutang_dagang = reader.Item("akun_hutang_dagang").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_hutang_dagang) Then
                Me.lbl_msg.Text = "Akun hutang dagang pada data supplier tersebut tidak ada"
                Exit Sub
            End If

            'cek akun pembelian
            sqlcom = "select akun_pembelian_impor from akun_penerimaan_invoice_supplier_tt"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_pembelian_impor = reader.Item("akun_pembelian_impor").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_pembelian_impor) Then
                Me.lbl_msg.Text = "Akun hutang pembelian impor pada Akun penerimaan invoice supplier TT tidak ada"
                Exit Sub
            End If


            'Else
            'cek no.lc
            sqlcom = "select seq_lc from entry_dokumen_impor where seq = " & Me.no_dokumen
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            'Daniel
            'If reader.HasRows Then
            '    If String.IsNullOrEmpty(reader.Item("seq_lc").ToString) Then
            '        Me.lbl_msg.Text = "Silahkan mengisi no. lc terlebih dahulu"
            '        reader.Close()
            '        connection.koneksi.CloseKoneksi()
            '        Exit Sub
            '    End If
            'End If
            'Daniel
            reader.Close()
            connection.koneksi.CloseKoneksi()
            'End If

            sqlcom = "update entry_dokumen_impor"
            sqlcom = sqlcom + " set is_submit = 'S'"
            sqlcom = sqlcom + " where seq = " & Me.no_dokumen
            connection.koneksi.UpdateRecord(sqlcom)
            Me.loaddata()

            'Daniel
            Me.GL()

            Me.loaddata()
            Me.loadgrid()
            'Daniel
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub save_produk_item()
        Try
            sqlcom = "select * from entry_dokumen_impor_produk where seq_entry = " & Me.vno_dokumen
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If Not reader.HasRows Then
                sqlcom = "select id_product, nama_product, isnull(qty,0) as qty, isnull(unit_price,0) as unit_price,"
                sqlcom = sqlcom + " isnull(discount,0) as discount, seq"
                sqlcom = sqlcom + " from purchase_order_detil"
                sqlcom = sqlcom + " where purchase_order_detil.po_no = " & Me.tb_no_po.Text
                sqlcom = sqlcom + " order by purchase_order_detil.nama_product"
                reader = connection.koneksi.SelectRecord(sqlcom)
                Do While reader.Read
                    sqlcom = "insert into entry_dokumen_impor_produk(seq_entry, id_product, qty, qty_stock, unit_price, nama_product, discount, seq)"
                    sqlcom = sqlcom + "values (" & Me.vno_dokumen & "," & reader.Item("id_product").ToString & "," & reader.Item("qty").ToString
                    sqlcom = sqlcom + "," & reader.Item("qty").ToString & "," & reader.Item("unit_price").ToString & ",'" & reader.Item("nama_product").ToString & "'"
                    sqlcom = sqlcom + "," & reader.Item("discount").ToString & "," & reader.item("seq").Tostring & ")"
                    connection.koneksi.InsertRecord(sqlcom)
                Loop
                reader.Close()
                connection.koneksi.CloseKoneksi()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindtotal_nilai()
        Try
            sqlcom = "select sum(isnull(isnull(entry_dokumen_impor_produk.qty,0) * (isnull(entry_dokumen_impor_produk.unit_price,0) - "
            sqlcom = sqlcom + " isnull(entry_dokumen_impor_produk.unit_price, 0) * isnull(entry_dokumen_impor_produk.discount,0) /100),0)) as vtotal_nilai"
            sqlcom = sqlcom + " from entry_dokumen_impor_produk"
            sqlcom = sqlcom + " where seq_entry = " & Me.vno_dokumen
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString, 3)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id_product, nama_product, isnull(qty,0) as qty, isnull(entry_dokumen_impor_produk.qty_stock,0) as qty_stock, isnull(unit_price,0) as unit_price,"
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
                    Me.btn_update.Visible = True
                    Me.btn_delete.Visible = True
                    Me.tbl_total_harga.Visible = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
                    Me.tbl_total_harga.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()

            Me.bindtotal_nilai()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update entry_dokumen_impor_produk"
                    sqlcom = sqlcom + " set qty = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) & ","
                    sqlcom = sqlcom + " qty_stock = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty_stock"), TextBox).Text) & ","
                    sqlcom = sqlcom + " unit_price = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_harga_jual"), TextBox).Text) & ","
                    sqlcom = sqlcom + " discount = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_discount"), TextBox).Text)
                    sqlcom = sqlcom + " where seq_entry = " & Me.vno_dokumen
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)

                    Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
            Next
            Me.loaddata()
            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete entry_dokumen_impor_produk"
                    sqlcom = sqlcom + " where seq_entry = " & Me.vno_dokumen
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah dihapus", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.tradingClass.Alert("Data masih digunakan di form lain", Me.Page)
            Else
                Me.tradingClass.Alert(ex.Message, Me.Page)
            End If
        End Try
    End Sub

    Sub cek_produk()
        Try
            sqlcom = "select * from entry_dokumen_impor_produk"
            sqlcom = sqlcom + " where seq_entry = " & Me.vno_dokumen
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                reader.Close()
                connection.koneksi.CloseKoneksi()
                Me.loadgrid()
                Me.tbl_produk.Visible = True
            Else
                Me.tbl_produk.Visible = False
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_produk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_produk.Click
        Me.save_produk_item()
        Me.loadgrid()
        Me.tbl_produk.Visible = True
    End Sub

    Protected Sub btn_kurs_idr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_idr.Click
        Me.tb_kurs.Text = "1.00"
    End Sub

    Protected Sub btn_kurs_usd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_usd.Click
        Me.tb_kurs.Text = tradingClass.KursBulanan("[kurs_bulanan]", Me.vid_periode_transaksi)
    End Sub

    Protected Sub btn_unsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_unsubmit.Click
        sqlcom = "update entry_dokumen_impor"
        sqlcom = sqlcom + " set is_submit = 'B'"
        sqlcom = sqlcom + " where seq = " & Me.no_dokumen
        connection.koneksi.UpdateRecord(sqlcom)

        Dim keterangan As String = "Entry Dokumen Impor no. " & Me.lbl_no_po.Text
        Me.tradingClass.DeleteAkunJurnal(keterangan, Me.vid_periode_transaksi)
        Me.tradingClass.DeleteAkunGeneralLedger(keterangan, Me.vid_periode_transaksi)

        Me.tradingClass.Alert("Data sudah diunsubmit!", Me.Page)
        Me.loaddata()
        Me.loadgrid()
    End Sub
End Class



