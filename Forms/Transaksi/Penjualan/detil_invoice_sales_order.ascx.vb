Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Penjualan_detil_invoice_sales_order
    Inherits System.Web.UI.UserControl
    Dim at As New KWATerbilang.cKWATerbilang

    'Daniel
    Public tradingClass As tradingClass = New tradingClass()
    Public status As String

    Sub GL()
        Try
            Dim vberikat As String = Nothing
            sqlcom = "select is_kawasan_berikat from daftar_customer where id = " & Me.tb_id_customer.Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                vberikat = reader.Item("is_kawasan_berikat").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            Dim penjualan_bruto As Decimal
            Dim penjualan_bruto_idr As Decimal
            Dim ppn As Decimal
            Dim ppn_idr As Decimal = ppn * System.Convert.ToDecimal(Me.tb_kurs.Text)
            Dim total_nilai As Decimal
            Dim total_nilai_idr As Decimal

            If vberikat = "T" Then
                penjualan_bruto = System.Convert.ToDecimal(Me.lbl_total_nilai.Text) / 1.1
                ppn = System.Convert.ToDecimal(Me.lbl_total_nilai.Text) / 1.1 * 0.1
            ElseIf vberikat = "Y" Then '<--Adien 5 Maret 2014 ,tadinya vberikat = "T"
                penjualan_bruto = System.Convert.ToDecimal(Me.lbl_total_nilai.Text)
                ppn = 0
            End If


            penjualan_bruto_idr = penjualan_bruto * System.Convert.ToDecimal(Me.tb_kurs.Text)
            ppn_idr = ppn * System.Convert.ToDecimal(Me.tb_kurs.Text)
            total_nilai = penjualan_bruto + ppn
            total_nilai_idr = total_nilai * System.Convert.ToDecimal(Me.tb_kurs.Text)

            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim keterangan As String = "Invoice Faktur Pajak dari Penjualan no. " & Me.lbl_no_penjualan.Text
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_penjualan.Text), id, Me.tradingClass.JurnalType("1"), keterangan, Me.vid_periode_transaksi)

            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_penjualan.Text), Me.vakun_piutang_dagang, Me.vakun_penjualan, penjualan_bruto_idr, 0, keterangan, Me.vid_periode_transaksi, Me.dd_mata_uang.SelectedItem.Text, Me.tb_kurs.Text, penjualan_bruto, 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_penjualan.Text), Me.vakun_penjualan, Me.vakun_piutang_dagang, 0, penjualan_bruto_idr, keterangan, Me.vid_periode_transaksi, Me.dd_mata_uang.SelectedItem.Text, Me.tb_kurs.Text, 0, penjualan_bruto, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_penjualan.Text), Me.vakun_piutang_dagang, Me.vakun_hutang_ppn, ppn_idr, 0, keterangan, Me.vid_periode_transaksi, Me.dd_mata_uang.SelectedItem.Text, Me.tb_kurs.Text, ppn, 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_penjualan.Text), Me.vakun_hutang_ppn, Me.vakun_piutang_dagang, 0, ppn_idr, keterangan, Me.vid_periode_transaksi, Me.dd_mata_uang.SelectedItem.Text, Me.tb_kurs.Text, 0, ppn, String.Empty)

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

    Private ReadOnly Property vno_so() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_so")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property


    Private ReadOnly Property vpaging() As String
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Public Property no_so() As Integer
        Get
            Dim o As Object = ViewState("no_so")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("no_so") = value
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

    Public Property vakun_piutang_dagang() As String
        Get
            Dim o As Object = ViewState("vakun_piutang_dagang")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_piutang_dagang") = value
        End Set
    End Property

    Public Property vakun_penjualan() As String
        Get
            Dim o As Object = ViewState("vakun_penjualan")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_penjualan") = value
        End Set
    End Property

    Public Property vakun_hutang_ppn() As String
        Get
            Dim o As Object = ViewState("vakun_hutang_ppn")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_hutang_ppn") = value
        End Set
    End Property


    Public Property vakun_hpp_penjualan() As String
        Get
            Dim o As Object = ViewState("vakun_hpp_penjualan")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_hpp_penjualan") = value
        End Set
    End Property

    Public Property vakun_persediaan_barang() As String
        Get
            Dim o As Object = ViewState("vakun_persediaan_barang")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_persediaan_barang") = value
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

    Public Property terbilang() As String
        Get
            Dim o As Object = ViewState("terbilang")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("terbilang") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiodetransaksi()
        sqlcom = "select id, name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_periode_transaksi.Text = reader.Item("name").ToString
            Me.vid_periode_transaksi = reader.Item("id").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindsales()
        sqlcom = "select code, nama_pegawai"
        sqlcom = sqlcom + " from user_list"
        sqlcom = sqlcom + " where (code_group = 5 or code = 34)"
        sqlcom = sqlcom + " order by nama_pegawai"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_sales.DataSource = reader
        Me.dd_sales.DataTextField = "nama_pegawai"
        Me.dd_sales.DataValueField = "code"
        Me.dd_sales.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindcustomer()
        sqlcom = "select name, payment_period from daftar_customer where id = " & Me.tb_id_customer.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_customer.Text = reader.Item("name").ToString
            If String.IsNullOrEmpty(Me.tb_lama_pembayaran.Text) Then
                Me.tb_lama_pembayaran.Text = Int(reader.Item("payment_period").ToString)
            End If

            If String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                Me.hitung_jatuh_tempo()
            End If
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindmata_uang()
        sqlcom = "select id from currency order by id"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_mata_uang.DataSource = reader
        Me.dd_mata_uang.DataTextField = "id"
        Me.dd_mata_uang.DataValueField = "id"
        Me.dd_mata_uang.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindttd_faktur_pajak()
        sqlcom = "select code, nama from pegawai_pajak order by is_default desc"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_ttd_faktur_pajak.DataSource = reader
        Me.dd_ttd_faktur_pajak.DataTextField = "nama"
        Me.dd_ttd_faktur_pajak.DataValueField = "code"
        Me.dd_ttd_faktur_pajak.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.tb_no_sp.Text = ""
        Me.tb_tgl_sp.Text = ""
        Me.tb_kurs.Text = ""
        Me.no_so = 0
    End Sub

    Sub clearalamat()
        Me.tb_id_alamat_customer.Text = 0
        Me.lbl_alamat_customer.Text = "------"
        Me.link_popup_alamat_customer.Visible = True
    End Sub

    Sub bindalamat()
        sqlcom = "select alamat from daftar_customer_alamat"
        sqlcom = sqlcom + " where id_customer = " & Me.tb_id_customer.Text

        If Me.tb_id_alamat_customer.Text = 0 Then
            sqlcom = sqlcom + " and seq = 1"
        Else
            sqlcom = sqlcom + " and seq = " & Me.tb_id_alamat_customer.Text
        End If

        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_alamat_customer.Text = reader.Item("alamat").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearalamat_pajak()
        Me.tb_id_alamat_faktur_pajak.Text = 0
        Me.lbl_alamat_faktur_pajak.Text = "------"
        Me.link_popup_alamat_faktur_pajak.Visible = True
    End Sub

    Sub bindalamat_pajak()
        sqlcom = "select alamat from daftar_customer_alamat"
        sqlcom = sqlcom + " where id_customer = " & Me.tb_id_customer.Text

        If Me.tb_id_alamat_faktur_pajak.Text = 0 Then
            sqlcom = sqlcom + " and seq = 1"
        Else
            sqlcom = sqlcom + " and seq = " & Me.tb_id_alamat_faktur_pajak.Text
        End If
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_alamat_faktur_pajak.Text = reader.Item("alamat").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub


    Sub loaddata()
        Try
            If Me.vno_so <> 0 Then
                Me.no_so = Me.vno_so
            End If

            sqlcom = "select no, convert(char, tanggal, 103) as tanggal, id_customer, no_surat_pesanan, no_pajak,"
            sqlcom = sqlcom + " convert(char, tgl_surat_pesanan, 103) as tgl_surat_pesanan, id_sales, status, id_currency, ppn, jenis_penjualan, rate,"
            sqlcom = sqlcom + " id_transaction_period, so_no_text, is_submit_invoice, seq_alamat_invoice, seq_alamat_faktur_pajak, id_ttd_pajak,"
            sqlcom = sqlcom + " convert(char, tgl_invoice, 103) as tgl_invoice, convert(char, tgl_jatuh_tempo, 103) as tgl_jatuh_tempo,"
            sqlcom = sqlcom + " lama_pembayaran"
            'Daniel
            sqlcom = sqlcom + " , uang_muka, uang_muka_nominal, uang_muka_keterangan "
            'Daniel
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " where no = " & Me.no_so
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_penjualan.Text = reader.Item("so_no_text").ToString
                Me.tb_tgl_penjualan.Text = reader.Item("tanggal").ToString
                Me.lbl_no_faktur_pajak.Text = reader.Item("no_pajak").ToString
                Me.dd_sales.SelectedValue = reader.Item("id_sales").ToString
                Me.tb_no_sp.Text = reader.Item("no_surat_pesanan").ToString
                Me.tb_tgl_sp.Text = reader.Item("tgl_surat_pesanan").ToString
                Me.dd_jenis_penjualan.SelectedValue = reader.Item("jenis_penjualan").ToString
                Me.dd_ppn.SelectedValue = Decimal.ToDouble(reader.Item("ppn").ToString)
                Me.tb_id_customer.Text = reader.Item("id_customer").ToString
                Me.dd_mata_uang.SelectedValue = reader.Item("id_currency").ToString
                Me.tb_kurs.Text = reader.Item("rate").ToString
                Me.tb_tgl_invoice.Text = reader.Item("tgl_invoice").ToString
                Me.tb_jatuh_tempo.Text = reader.Item("tgl_jatuh_tempo").ToString
                'Daniel
                If reader.Item("uang_muka").ToString <> Nothing Then
                    Me.DropDownListUangMuka.SelectedValue = reader.Item("uang_muka").ToString
                Else
                    Me.DropDownListUangMuka.SelectedIndex = 0
                End If
                Me.TextBoxNominal.Text = reader.Item("uang_muka_nominal").ToString
                Me.LabelKeterangan.Text = reader.Item("uang_muka_keterangan").ToString
                'Daniel

                If Not String.IsNullOrEmpty(reader.Item("lama_pembayaran").ToString) Then
                    Me.tb_lama_pembayaran.Text = Int(reader.Item("lama_pembayaran").ToString)
                Else
                    Me.tb_lama_pembayaran.Text = ""
                End If

                If String.IsNullOrEmpty(reader.Item("seq_alamat_invoice").ToString) Then
                    Me.tb_id_alamat_customer.Text = 1
                Else
                    Me.tb_id_alamat_customer.Text = reader.Item("seq_alamat_invoice").ToString
                End If

                If String.IsNullOrEmpty(reader.Item("seq_alamat_faktur_pajak").ToString) Then
                    Me.tb_id_alamat_faktur_pajak.Text = 1
                Else
                    Me.tb_id_alamat_faktur_pajak.Text = reader.Item("seq_alamat_faktur_pajak").ToString
                End If

                Me.dd_ttd_faktur_pajak.SelectedValue = reader.Item("id_ttd_pajak").ToString

                If reader.Item("is_submit_invoice").ToString = "B" Then
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                    Me.btn_unsubmit.Enabled = False
                    'Me.btn_print_invoice.Enabled = False
                    'Me.btn_print_faktur_pajak.Enabled = False
                Else
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                    Me.btn_unsubmit.Enabled = True
                    Me.btn_print_invoice.Enabled = True
                    Me.btn_print_faktur_pajak.Enabled = True
                End If

                Me.bindcustomer()
                Me.bindalamat()
                Me.bindalamat_pajak()

                Me.tbl_produk.Visible = True

            Else
                Me.tbl_produk.Visible = False
            End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindratepajak()
        Try
            Dim vtgl_invoice As String = Me.tb_tgl_invoice.Text.Substring(3, 2) & "/" & Me.tb_tgl_invoice.Text.Substring(0, 2) & "/" & Me.tb_tgl_invoice.Text.Substring(6, 4)
            'Dim vtgl_invoice As String = Me.tb_tgl_invoice.Text
            sqlcom = "select isnull(rate,0) as rate_pajak, tgl_awal, tgl_akhir"
            sqlcom = sqlcom + " from rate_pajak"
            sqlcom = sqlcom + " where tahun = " & Me.vtahun
            sqlcom = sqlcom + " order by tgl_awal"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read
                If cdate(reader.Item("tgl_awal").ToString) >= cdate(RTrim(vtgl_invoice)) Then
                    Me.tb_kurs.Text = FormatNumber(reader.Item("rate_pajak").ToString, 2)
                    Exit Do
                End If
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.clearalamat()
            Me.clearalamat_pajak()
            Me.bindperiodetransaksi()
            Me.bindsales()
            Me.bindmata_uang()
            Me.bindttd_faktur_pajak()
            Me.dd_mata_uang.SelectedValue = "IDR"
            Me.tb_kurs.Text = 1


            Me.loaddata()
            Me.loadgrid()

            If String.IsNullOrEmpty(Me.tb_tgl_invoice.Text) Then
                Me.tb_tgl_invoice.Text = Me.tb_tgl_penjualan.Text
            End If


            If Me.dd_mata_uang.SelectedValue = "USD" And Me.tb_kurs.Text = 1 Then
                Me.bindratepajak()
            End If

            Me.bindtotal_nilai()

            Me.tb_id_customer.Attributes.Add("style", "display: none;")
            Me.tb_id_alamat_customer.Attributes.Add("style", "display: none;")
            Me.tb_id_alamat_faktur_pajak.Attributes.Add("style", "display: none;")
            Me.link_refresh_alamat_customer.Attributes.Add("style", "display: none;")
            Me.link_refresh_alamat_faktur_pajak.Attributes.Add("style", "display: none;")
            Me.link_popup_alamat_customer.Attributes.Add("onclick", "popup_alamat_customer('" & Me.tb_id_customer.Text & "','" & Me.tb_id_alamat_customer.ClientID & "','" & Me.link_refresh_alamat_customer.UniqueID & "')")
            Me.link_popup_alamat_faktur_pajak.Attributes.Add("onclick", "popup_alamat_customer('" & Me.tb_id_customer.Text & "','" & Me.tb_id_alamat_faktur_pajak.ClientID & "','" & Me.link_refresh_alamat_faktur_pajak.UniqueID & "')")

            'If Session.Item("code") <> 1 Then
            '    Me.btn_unsubmit.Visible = False
            'End If

            tradingClass.ViewButtonUnsubmit(Me.btn_unsubmit)

        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/invoice.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&voption=" & Me.voption & "&vsearch=" & Me.vsearch & "&vpaging=" & Me.vpaging)
    End Sub


    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_alamat_customer.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi alamat invoice terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_invoice.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal invoice terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal jatuh tempo terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_lama_pembayaran.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi lama pembayaran terlebih dahulu"
            Else

                dim no_depan_pajak as string = nothing
                sqlcom = "select no_depan from nomor_depan_faktur_pajak"
                sqlcom = sqlcom + " where tahun = year(getdate()) and is_kawasan_berikat = (select is_kawasan_berikat"
                sqlcom = sqlcom + " from daftar_customer where id = " & Me.tb_id_customer.Text & ")"
                reader = connection.koneksi.selectrecord(sqlcom)
                reader.read()
                if reader.hasrows() then
                   no_depan_pajak = reader.item("no_depan").ToString
                end if
                reader.close()
                connection.koneksi.closekoneksi()
                
                dim no_urut_pajak as string = nothing
                'if int(Me.tb_tgl_invoice.Text.substring(6,4)) >= 2013 and int(me.tb_tgl_invoice.text.Substring(3,2)) < 4 then
                '   no_urut_pajak = Me.lbl_no_penjualan.Text
                'elseif int(Me.tb_tgl_invoice.Text.substring(6,4)) >= 2013 and int(me.tb_tgl_invoice.text.Substring(3,2)) >= 4 then
                sqlcom = "select no_berjalan + 1 as no_akhir"
                sqlcom = sqlcom + " from no_akhir_pajak"
                sqlcom = sqlcom + " where seq = (select max(seq) from no_akhir_pajak)"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows() Then
                    no_urut_pajak = reader.Item("no_akhir").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "update no_akhir_pajak"
                sqlcom = sqlcom + " set no_berjalan = isnull(no_berjalan,0) + 1"
                sqlcom = sqlcom + " where seq = (select max(seq) from no_akhir_pajak)"
                connection.koneksi.UpdateRecord(sqlcom)
                'End If

                Dim no_pajak As String = no_depan_pajak & no_urut_pajak

                Dim vtgl_so As String = Me.tb_tgl_penjualan.Text.Substring(3, 2) & "/" & Me.tb_tgl_penjualan.Text.Substring(0, 2) & "/" & Me.tb_tgl_penjualan.Text.Substring(6, 4)
                Dim vtgl_invoice As String = Me.tb_tgl_invoice.Text.Substring(3, 2) & "/" & Me.tb_tgl_invoice.Text.Substring(0, 2) & "/" & Me.tb_tgl_invoice.Text.Substring(6, 4)
                Dim vtgl_jatuh_tempo As String = Me.tb_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(6, 4)

                sqlcom = "update sales_order"
                sqlcom = sqlcom + " set tanggal = '" & vtgl_so & "',"
                sqlcom = sqlcom + " rate = " & Decimal.ToDouble(Me.tb_kurs.Text) & ","
                sqlcom = sqlcom + " seq_alamat_invoice = " & Me.tb_id_alamat_customer.Text & ","
                sqlcom = sqlcom + " seq_alamat_faktur_pajak = " & Me.tb_id_alamat_faktur_pajak.Text & ","
                sqlcom = sqlcom + " id_ttd_pajak = " & Me.dd_ttd_faktur_pajak.SelectedValue & ","
                sqlcom = sqlcom + " tgl_invoice = '" & vtgl_invoice & "',"
                sqlcom = sqlcom + " tgl_jatuh_tempo = '" & vtgl_jatuh_tempo & "',"
                sqlcom = sqlcom + " lama_pembayaran = " & Me.tb_lama_pembayaran.Text & ","
                sqlcom = sqlcom + " is_invoice = 'S',"
                sqlcom = sqlcom + " no_pajak = '" & no_pajak & "'"
                sqlcom = sqlcom + " where no = " & Me.no_so
                connection.koneksi.UpdateRecord(sqlcom)
                Me.update_terbilang(Me.no_so)
                Me.tradingClass.Alert("Data sudah diupdate", Me.Page)


                Me.loaddata()
            End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindtotal_nilai()
        Try
            dim x as integer = 0
            if Me.dd_mata_uang.SelectedValue = "USD" then
               x = 3
            else
               x = 0
            end if
            
            sqlcom = "select isnull(sum(isnull(qty,0) * (isnull(harga_jadi,0) + (isnull(harga_jadi,0) * isnull(discount,0)/100))),0) as vtotal_nilai"
            sqlcom = sqlcom + " from sales_order_detail"
            sqlcom = sqlcom + " where no_sales_order = " & Me.no_so
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If Me.dd_ppn.SelectedValue = "0" Then
                    if Me.dd_mata_uang.SelectedValue = "USD" then
                       Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString, x)
                    else
                       Me.lbl_total_nilai.Text = FormatNumber(Math.Round(decimal.todouble(reader.Item("vtotal_nilai").ToString),2), x)
                    end if
                Else
                    if Me.dd_mata_uang.SelectedValue = "USD" then
                        Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString + reader.Item("vtotal_nilai").ToString * 0.1, x)
                    else
                       Me.lbl_total_nilai.Text = FormatNumber(Math.Round(decimal.todouble(reader.Item("vtotal_nilai").ToString) + (decimal.todouble(reader.Item("vtotal_nilai").ToString) * 0.1),0) ,x)
                    end if
                End If
                Me.lbl_total_nilai_idr.Text = FormatNumber(Math.Round(decimal.toDouble(Me.lbl_total_nilai.Text) * decimal.todouble(Me.tb_kurs.Text),0),2)
                Me.lbl_subtotal.Text = FormatNumber(reader.Item("vtotal_nilai").ToString, x)
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

            sqlcom = "select no_sales_order, id_product, nama_product, qty, harga_beli, harga_jual, discount, harga_jadi, qty_pending,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + packaging.name + '/' + measurement_unit.name as packaging,"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + " when product_item.is_packaging = 'P' then measurement_unit.name"
            sqlcom = sqlcom + " when product_item.is_packaging = 'Q' then packaging.name"
            sqlcom = sqlcom + " end as satuan_produk, "

            if Me.dd_mata_uang.SelectedValue = "USD" then
               sqlcom = sqlcom + " isnull(sales_order_detail.qty,0) * (isnull(sales_order_detail.harga_jual,0) - "
               sqlcom = sqlcom + " isnull(sales_order_detail.harga_jual, 0) * isnull(sales_order_detail.discount,0) /100) as sub_total"
            Else
               sqlcom = sqlcom + " round(isnull(sales_order_detail.qty,0) * (isnull(sales_order_detail.harga_jual,0) - "
               sqlcom = sqlcom + " isnull(sales_order_detail.harga_jual, 0) * isnull(sales_order_detail.discount,0) /100),0) as sub_total"
            End if


            sqlcom = sqlcom + " from sales_order_detail"
            sqlcom = sqlcom + " inner join product_item on product_item.id = sales_order_detail.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where no_sales_order = " & Me.no_so
            sqlcom = sqlcom + " order by sales_order_detail.seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "sales_order_detail")
                Me.dg_data.DataSource = ds.Tables("sales_order_detail").DefaultView

                If ds.Tables("sales_order_detail").Rows.Count > 0 Then
                    If ds.Tables("sales_order_detail").Rows.Count > 10 Then
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

            Me.bindtotal_nilai()

        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            '========= cek akun piutang_dagang

            sqlcom = "select akun_piutang_dagang from daftar_customer where id = " & Me.tb_id_customer.Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_piutang_dagang = reader.Item("akun_piutang_dagang").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_piutang_dagang) Then
                Me.lbl_msg.Text = "Akun piutang dagang untuk customer tersebut tidak ada"
                Exit Sub
            End If

            '========= cek akun penjualan, akun_hutang_ppn

            Dim vis_ekspor As String = "T"
            sqlcom = "select is_ekspor from daftar_customer where id = " & Me.tb_id_customer.Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                vis_ekspor = reader.Item("is_ekspor").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()


            sqlcom = "select akun_penjualan, akun_penjualan_ekspor, akun_hutang_ppn"
            sqlcom = sqlcom + " from akun_invoice_penjualan"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then

                If vis_ekspor = "T" Then
                    Me.vakun_penjualan = reader.Item("akun_penjualan").ToString.Trim
                Else
                    Me.vakun_penjualan = reader.Item("akun_penjualan_ekspor").ToString.Trim
                End If

                Me.vakun_hutang_ppn = reader.Item("akun_hutang_ppn").ToString.Trim

            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_penjualan) Then
                Me.lbl_msg.Text = "Akun penjualan di Akun invoice penjualan tidak ada"
                Exit Sub
            ElseIf String.IsNullOrEmpty(Me.vakun_hutang_ppn) Then
                Me.lbl_msg.Text = "Akun hutang PPN di Akun invoice penjualan tidak ada"
                Exit Sub
            End If

            '=========== cek akun hpp penjualan, akun persediaan barang

            sqlcom = "select akun_hpp_penjualan, akun_persediaan_barang"
            sqlcom = sqlcom + " from akun_persediaan_penjualan"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_hpp_penjualan = reader.Item("akun_hpp_penjualan").ToString.Trim
                Me.vakun_persediaan_barang = reader.Item("akun_persediaan_barang").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_hpp_penjualan) Then
                Me.lbl_msg.Text = "Akun hpp penjualan tidak ada di COA"
                Exit Sub
            ElseIf String.IsNullOrEmpty(Me.vakun_persediaan_barang) Then
                Me.lbl_msg.Text = "Akun persediaan barang tidak ada di COA"
                Exit Sub
            End If

            '================ submit
            sqlcom = "select * from sales_order_detail"
            sqlcom = sqlcom + " where no_sales_order = " & Me.no_so
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            'Daniel
            'If reader.HasRows Then
            'Daniel
            sqlcom = "update sales_order"
            sqlcom = sqlcom + " set is_submit_invoice = 'S'"
            sqlcom = sqlcom + " where no = " & Me.no_so
            connection.koneksi.UpdateRecord(sqlcom)
            reader.Close()
            connection.koneksi.CloseKoneksi()
            Me.loaddata()
            'Me.jurnal()
            Me.GL()
            'Else
            'Me.lbl_msg.Text = "Penjualan tersebut belum ada produknya"
            'End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
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

    Sub jurnal()
        Dim vberikat As String = Nothing
        sqlcom = "select is_kawasan_berikat from daftar_customer where id = " & me.tb_id_customer.text
        reader = connection.koneksi.selectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            vberikat = reader.Item("is_kawasan_berikat").ToString.trim
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        Dim vtgl As String = Me.tb_tgl_penjualan.Text.Substring(3, 2) & "/" & Me.tb_tgl_penjualan.Text.Substring(0, 2) & "/" & Me.tb_tgl_penjualan.Text.Substring(6, 4)

        'jurnal invoice penjualan
        

        'debet
        ' akun_piutang_dagang -> akun_penjualan

        Me.seq_max()
        if vberikat = "T" then
           sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
           sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
           sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penjualan.Text & "','" & vtgl & "','INVJUAL','" & Me.vakun_piutang_dagang & "',"
           sqlcom = sqlcom + "'" & Me.vakun_penjualan & "'," & Decimal.ToDouble(Me.lbl_total_nilai.Text) / 1.1 * Decimal.ToDouble(Me.tb_kurs.Text) & ",0, 'Pembuatan invoice penjualan no. " & Me.lbl_no_penjualan.Text & "'"
           sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
        else
           sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
           sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
           sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penjualan.Text & "','" & vtgl & "','INVJUAL','" & Me.vakun_piutang_dagang & "',"
           sqlcom = sqlcom + "'" & Me.vakun_penjualan & "'," & Decimal.ToDouble(Me.lbl_total_nilai.Text) * Decimal.ToDouble(Me.tb_kurs.Text) & ",0, 'Pembuatan invoice penjualan no. " & Me.lbl_no_penjualan.Text & "'"
           sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
        end if
        connection.koneksi.InsertRecord(sqlcom)

        ' akun_piutang_dagang -> akun_hutang_ppn
         
        Me.seq_max()

        if vberikat = "T" then
           sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
           sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
           sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penjualan.Text & "','" & vtgl & "','INVJUAL','" & Me.vakun_piutang_dagang & "',"
           sqlcom = sqlcom + "'" & Me.vakun_hutang_ppn & "'," & Decimal.ToDouble(Me.lbl_total_nilai.Text) / 1.1 * 0.1 * Decimal.ToDouble(Me.tb_kurs.Text) & ",0, 'Pembuatan invoice penjualan no. " & Me.lbl_no_penjualan.Text & "'"
           sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
        else
           sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
           sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
           sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penjualan.Text & "','" & vtgl & "','INVJUAL','" & Me.vakun_piutang_dagang & "',"
           sqlcom = sqlcom + "'" & Me.vakun_hutang_ppn & "', 0, 0, 'Pembuatan invoice penjualan no. " & Me.lbl_no_penjualan.Text & "'"
           sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
        end if
        connection.koneksi.InsertRecord(sqlcom)

        
        'kredit
        ' akun_penjualan -> akun_piutang_dagang
        Me.seq_max()
        if vberikat = "T" then
           sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
           sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
           sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penjualan.Text & "','" & vtgl & "','INVJUAL','" & Me.vakun_penjualan & "',"
           sqlcom = sqlcom + "'" & Me.vakun_piutang_dagang & "',0," & Decimal.ToDouble(Me.lbl_total_nilai.Text) / 1.1 * Decimal.ToDouble(Me.tb_kurs.Text) & ", 'Pembuatan invoice penjualan no. " & Me.lbl_no_penjualan.Text & "'"
           sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
        else
           sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
           sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
           sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penjualan.Text & "','" & vtgl & "','INVJUAL','" & Me.vakun_penjualan & "',"
           sqlcom = sqlcom + "'" & Me.vakun_piutang_dagang & "',0," & Decimal.ToDouble(Me.lbl_total_nilai.Text)  * Decimal.ToDouble(Me.tb_kurs.Text) & ", 'Pembuatan invoice penjualan no. " & Me.lbl_no_penjualan.Text & "'"
           sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
        end if
        connection.koneksi.InsertRecord(sqlcom)

        ' akun_hutang_ppn -> akun_piutang_dagang
        Me.seq_max()
        if vberikat = "T" then
           sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
           sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
           sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penjualan.Text & "','" & vtgl & "','INVJUAL','" & Me.vakun_hutang_ppn & "',"
           sqlcom = sqlcom + "'" & Me.vakun_piutang_dagang & "',0," & Decimal.ToDouble(Me.lbl_total_nilai.Text) / 1.1 * 0.1 * Decimal.ToDouble(Me.tb_kurs.Text) & ", 'Pembuatan invoice penjualan no. " & Me.lbl_no_penjualan.Text & "'"
           sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
        else
           sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
           sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
           sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penjualan.Text & "','" & vtgl & "','INVJUAL','" & Me.vakun_hutang_ppn & "',"
           sqlcom = sqlcom + "'" & Me.vakun_piutang_dagang & "',0, 0, 'Pembuatan invoice penjualan no. " & Me.lbl_no_penjualan.Text & "'"
           sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
        end if
        connection.koneksi.InsertRecord(sqlcom)


        '===========
        'jurnal persediaan barang penjualan

        Dim vtotal_nilai_beli As Decimal = 0

        sqlcom = "select isnull(sum(isnull(harga_beli,0) * isnull(qty,0)),0) as total_nilai_beli"
        sqlcom = sqlcom + " from sales_order_detail"
        sqlcom = sqlcom + " where no_sales_order = " & Me.no_so
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            vtotal_nilai_beli = reader.Item("total_nilai_beli").ToString.Trim
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        'debet
        'akun_persediaan_barang -> akun_hpp_penjualan
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penjualan.Text & "','" & vtgl & "','BRGJUAL','" & Me.vakun_hpp_penjualan & "',"
        sqlcom = sqlcom + "'" & Me.vakun_persediaan_barang & "'," & vtotal_nilai_beli & ",0, ' HPP penjualan no. " & Me.lbl_no_penjualan.Text & "'"
        sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "')"
        connection.koneksi.InsertRecord(sqlcom)

        'kredit
        'akun_hpp_penjualan -> akun_persediaan_barang
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_penjualan.Text & "','" & vtgl & "','BRGJUAL','" & Me.vakun_persediaan_barang & "',"
        sqlcom = sqlcom + "'" & Me.vakun_hpp_penjualan & "',0," & vtotal_nilai_beli & ", ' HPP penjualan no. " & Me.lbl_no_penjualan.Text & "'"
        sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "')"
        connection.koneksi.InsertRecord(sqlcom)
    End Sub

    Protected Sub link_refresh_alamat_customer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_alamat_customer.Click
        Me.bindalamat()
    End Sub

    Sub hitung_jatuh_tempo()
        Dim vtgl_jatuh_tempo As Date
        Dim vtgl As String = Me.tb_tgl_penjualan.Text.Substring(3, 2) & "/" & Me.tb_tgl_penjualan.Text.Substring(0, 2) & "/" & Me.tb_tgl_penjualan.Text.Substring(6, 4)
        vtgl_jatuh_tempo = DateAdd(DateInterval.Day, Decimal.ToDouble(Me.tb_lama_pembayaran.Text), CDate(vtgl))
        Me.tb_jatuh_tempo.Text = Day(vtgl_jatuh_tempo) & "/" & Month(vtgl_jatuh_tempo) & "/" & Year(vtgl_jatuh_tempo)
    End Sub

    Protected Sub btn_hitung_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_hitung.Click
        If String.IsNullOrEmpty(Me.tb_lama_pembayaran.Text) Then
            Me.lbl_msg.Text = "Silahkan mengisi lama pembayaran terlebih dahulu"
        Else
            Me.hitung_jatuh_tempo()
        End If
    End Sub

    Sub update_terbilang(ByRef no_so As String)
        Try
            Dim nilai As Decimal = Decimal.ToDouble(Me.lbl_total_nilai.Text)
            Dim bulat As Decimal = Math.Truncate(Decimal.ToDouble(Me.lbl_total_nilai.Text))
            Dim selisih_angka As Decimal = nilai - bulat
            Dim selisih_string As String = FormatNumber(nilai - bulat, 3)

            Me.terbilang = at.KwaTerbilangDecimalSpecial(bulat)

            terbilang = Replace(terbilang, "Seribu", "Satu Ribu")

            If selisih_angka = 0 Then
                terbilang = terbilang
            Else
                If selisih_string.Substring(2, 1) = "0" And selisih_string.Substring(3, 1) = "0" Then
                    terbilang = terbilang + " Dan Nol Nol "
                    terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(4, 1))

                ElseIf selisih_string.Substring(2, 1) = "0" And selisih_string.Substring(3, 1) <> "0" And selisih_string.Substring(4, 1) = "0" Then
                    terbilang = terbilang + " Dan Nol "
                    terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(3, 1))
                    terbilang = terbilang + " Nol"

                ElseIf selisih_string.Substring(2, 1) = "0" And selisih_string.Substring(3, 1) <> "0" And selisih_string.Substring(4, 1) <> "0" Then
                    terbilang = terbilang + " Dan Nol "
                    terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(3, 1)) & " "
                    terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(4, 1))

                ElseIf selisih_string.Substring(2, 1) <> "0" And selisih_string.Substring(3, 1) = "0" And selisih_string.Substring(4, 1) = "0" Then
                    terbilang = terbilang + " Dan "
                    terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(2, 1)) & " "
                    terbilang = terbilang + "Nol Nol"

                ElseIf selisih_string.Substring(2, 1) <> "0" And selisih_string.Substring(3, 1) <> "0" And selisih_string.Substring(4, 1) = "0" Then
                    terbilang = terbilang + " Dan "
                    terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(2, 1)) & " "
                    terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(3, 1)) & " "
                    terbilang = terbilang + "Nol"
                ElseIf selisih_string.Substring(2, 1) <> "0" And selisih_string.Substring(3, 1) <> "0" And selisih_string.Substring(4, 1) <> "0" Then
                    terbilang = terbilang + " Dan "
                    terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(2, 1)) & " "
                    terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(3, 1)) & " "
                    terbilang = terbilang + at.KwaTerbilangDecimalSpecial(selisih_string.Substring(4, 1))
                End If
                terbilang = terbilang + " Sen"
            End If

            sqlcom = "update sales_order"
            sqlcom = sqlcom + " set terbilang = '" & Me.terbilang & "'"
            sqlcom = sqlcom + " where no = " & no_so
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_print_invoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print_invoice.Click
        Try
            Me.update_terbilang(Me.no_so)

            Dim reportPath As String = Nothing
            If Me.dd_mata_uang.SelectedValue = "IDR" Then
                'dendi()
                If Me.DropDownListUangMuka.SelectedIndex = 0 Then
                    reportPath = Server.MapPath("reports\invoice.rpt")
                Else
                    reportPath = Server.MapPath("reports\invoice_uang_muka.rpt")
                End If
                'dendi


                'reportPath = Server.MapPath("reports\invoice.rpt")
                'dendi
            ElseIf Me.dd_mata_uang.SelectedValue = "USD" Then
                'dendi
                If Me.DropDownListUangMuka.SelectedIndex = 0 Then
                    reportPath = Server.MapPath("reports\invoice_usd.rpt")
                Else
                    reportPath = Server.MapPath("reports\invoice_uang_muka_usd.rpt")
                End If
                ' dendi
                'reportPath = Server.MapPath("reports\invoice_usd.rpt")
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
            oRD.SetParameterValue("so_no", Me.no_so)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/invoice.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/invoice.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_print_faktur_pajak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print_faktur_pajak.Click
        Try
            Dim reportPath As String

            If Me.dd_mata_uang.SelectedValue = "IDR" Then
                'Daniel
                If Me.DropDownListUangMuka.SelectedIndex = 0 Then
                    reportPath = Server.MapPath("reports\faktur_pajak.rpt")
                Else
                    reportPath = Server.MapPath("reports\faktur_pajak_uang_muka.rpt")
                End If
                'Daniel
            ElseIf Me.dd_mata_uang.SelectedValue = "USD" Then
                'Daniel
                If Me.DropDownListUangMuka.SelectedIndex = 0 Then
                    reportPath = Server.MapPath("reports\faktur_pajak_usd.rpt")
                Else
                    reportPath = Server.MapPath("reports\faktur_pajak_uang_muka_usd.rpt")
                End If
                'Daniel
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
            oRD.SetParameterValue("so_no", Me.no_so)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/faktur_pajak.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/faktur_pajak.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub link_refresh_alamat_faktur_pajak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_alamat_faktur_pajak.Click
        Me.bindalamat_pajak()
    End Sub

    Protected Sub btn_kurs_idr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_idr.Click
        Me.tb_kurs.Text = "1.00"
    End Sub

    Protected Sub btn_kurs_usd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_usd.Click
        Me.tb_kurs.Text = tradingClass.KursBulanan("[kurs_bulanan]", Me.vid_periode_transaksi)
    End Sub

    Protected Sub btn_unsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_unsubmit.Click
        Try
            Me.lbl_msg.Text = Nothing
            Dim keterangan As String = "Invoice Faktur Pajak dari Penjualan no. " & Me.lbl_no_penjualan.Text

            sqlcom = "select * from sales_order_detail"
            sqlcom = sqlcom + " where no_sales_order = " & Me.no_so
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            sqlcom = "update sales_order"
            sqlcom = sqlcom + " set is_submit_invoice = 'B'"
            sqlcom = sqlcom + " where no = " & Me.no_so
            connection.koneksi.UpdateRecord(sqlcom)
            reader.Close()
            connection.koneksi.CloseKoneksi()

            Me.tradingClass.DeleteAkunJurnal(keterangan, Me.vid_periode_transaksi)
            Me.tradingClass.DeleteAkunGeneralLedger(keterangan, Me.vid_periode_transaksi)

            Me.loaddata()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class

