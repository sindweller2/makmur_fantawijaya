Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_penerimaan_pembayaran_penjualan
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    'Daniel
    Public tradingClass As New tradingClass()

    Sub GL()
        Try
            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim keterangan As String = "Penerimaan Pembayaran Penjualan no. " & Me.lbl_no_so.Text
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.lbl_tgl_so.Text), id, Me.tradingClass.JurnalType("3"), keterangan, Me.vid_transaksi)

            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then


                    Me.vid_transaksi = CType(Me.dg_data.Items(x).FindControl("lbl_periode"), Label).Text
                    Me.vnilai = Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_nilai_pembayaran"), TextBox).Text)
                    Me.vbiaya_bank = Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_biaya_bank"), TextBox).Text)
                    Me.vbiaya_pengiriman_barang = Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_biaya_pengiriman_barang"), TextBox).Text)
                    Me.vno_invoice = Me.lbl_no_so.Text

                    sqlcom = "select sales_order.rate from sales_order inner join daftar_customer on daftar_customer.id = sales_order.id_customer where so_no_text = '" & Me.vno_invoice & "'"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        Me.vkurs = reader.Item("rate").ToString.Trim
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "SELECT [account_code] FROM [bank_account] where id = '" & CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text & "'"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        Me.vakun_biaya_bank = reader.Item("account_code").ToString.Trim
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_so.Text), Me.vakun_piutang_giro_mundur, "82.01", Me.vnilai, 0, keterangan, Me.vid_transaksi, Me.dd_mata_uang.SelectedValue, Me.vkurs, IIf(Me.dd_mata_uang.SelectedValue = "IDR", 0, Me.vnilai), 0, String.Empty)
                    Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_so.Text), "82.01", Me.vakun_piutang_giro_mundur, 0, Me.vnilai, keterangan, Me.vid_transaksi, Me.dd_mata_uang.SelectedValue, Me.vkurs, IIf(Me.dd_mata_uang.SelectedValue = "IDR", 0, Me.vnilai), 0, String.Empty)

                    If Me.vbiaya_bank <> 0 Then
                        Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_so.Text), Me.tradingClass.COA("BIAYA BANK DAN MATERAI"), "82.01", Me.vbiaya_bank, 0, keterangan, Me.vid_transaksi, Me.dd_mata_uang.SelectedValue, Me.vkurs, IIf(Me.dd_mata_uang.SelectedValue = "IDR", 0, Me.vbiaya_bank), 0, String.Empty)
                        Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_so.Text), "82.01", Me.tradingClass.COA("BIAYA BANK DAN MATERAI"), 0, Me.vbiaya_bank, keterangan, Me.vid_transaksi, Me.dd_mata_uang.SelectedValue, Me.vkurs, IIf(Me.dd_mata_uang.SelectedValue = "IDR", 0, Me.vbiaya_bank), 0, String.Empty)
                    End If


                    If Me.vbiaya_pengiriman_barang <> 0 Then
                        Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_so.Text), Me.tradingClass.COA("BIAYA PENGANGKUTAN/PENGIRIMAN BARANG (LOKAL)"), vakun_biaya_bank, Me.vbiaya_pengiriman_barang, 0, keterangan, Me.vid_transaksi, Me.dd_mata_uang.SelectedValue, Me.vkurs, IIf(Me.dd_mata_uang.SelectedValue = "IDR", 0, Me.vbiaya_pengiriman_barang), 0, String.Empty)
                        Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_so.Text), vakun_biaya_bank, Me.tradingClass.COA("BIAYA PENGANGKUTAN/PENGIRIMAN BARANG (LOKAL)"), 0, Me.vbiaya_pengiriman_barang, keterangan, Me.vid_transaksi, Me.dd_mata_uang.SelectedValue, Me.vkurs, IIf(Me.dd_mata_uang.SelectedValue = "IDR", 0, Me.vbiaya_pengiriman_barang), 0, String.Empty)
                    End If

                    Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_so.Text), Me.vakun_piutang_dagang, "82.01", Val(Me.vnilai + Me.vbiaya_bank), 0, keterangan, Me.vid_transaksi, Me.dd_mata_uang.SelectedValue, Me.vkurs, 0, IIf(Me.dd_mata_uang.SelectedValue = "IDR", 0, Val(Me.vnilai + Me.vbiaya_bank + Me.vbiaya_pengiriman_barang)), String.Empty)
                    Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_so.Text), "82.01", Me.vakun_piutang_dagang, 0, Val(Me.vnilai + Me.vbiaya_bank), keterangan, Me.vid_transaksi, Me.dd_mata_uang.SelectedValue, Me.vkurs, 0, IIf(Me.dd_mata_uang.SelectedValue = "IDR", 0, Val(Me.vnilai + Me.vbiaya_bank + Me.vbiaya_pengiriman_barang)), String.Empty)

                End If
            Next

            Me.tradingClass.Alert("Data sudah disubmit!", Me.Page)
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Public Property vbiaya_pengiriman_barang() As Decimal
        Get
            Dim o As Object = ViewState("vbiaya_pengiriman_barang")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vbiaya_pengiriman_barang") = value
        End Set
    End Property
    'Daniel

    Private ReadOnly Property vno_so() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_so")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

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

    Private ReadOnly Property voption() As String
        Get
            Dim o As Object = Request.QueryString("voption")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return "0"
        End Get
    End Property

    Private ReadOnly Property vpaging() As Integer
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property vid_customer() As Integer
        Get
            Dim o As Object = ViewState("vid_customer")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_customer") = value
        End Set
    End Property

    Public Property vtanggal() As String
        Get
            Dim o As Object = ViewState("vtanggal")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vtanggal") = value
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

    Public Property vseq() As Integer
        Get
            Dim o As Object = ViewState("vseq")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vseq") = value
        End Set
    End Property

    Public Property vnilai() As Decimal
        Get
            Dim o As Object = ViewState("vnilai")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vnilai") = value
        End Set
    End Property


    Public Property vkelebihan() As Decimal
        Get
            Dim o As Object = ViewState("vkelebihan")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vkelebihan") = value
        End Set
    End Property


    Public Property vbiaya_bank() As Decimal
        Get
            Dim o As Object = ViewState("vbiaya_bank")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vbiaya_bank") = value
        End Set
    End Property

    Public Property vkurs() As Integer
        Get
            Dim o As Object = ViewState("vkurs")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vkurs") = value
        End Set
    End Property

    Public Property vno_invoice() As String
        Get
            Dim o As Object = ViewState("vno_invoice")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vno_invoice") = value
        End Set
    End Property

    Public Property vid_transaksi() As Integer
        Get
            Dim o As Object = ViewState("vid_transaksi")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_transaksi") = value
        End Set
    End Property

    Public Property vid_kas() As Integer
        Get
            Dim o As Object = ViewState("vid_kas")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_kas") = value
        End Set
    End Property

    Public Property vakun_piutang_giro_mundur() As String
        Get
            Dim o As Object = ViewState("vakun_piutang_giro_mundur")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_piutang_giro_mundur") = value
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

    Public Property vakun_biaya_bank() As String
        Get
            Dim o As Object = ViewState("vakun_biaya_bank")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_biaya_bank") = value
        End Set
    End Property

    Public Property vid_currency() As String
        Get
            Dim o As Object = ViewState("vid_currency")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vid_currency") = value
        End Set
    End Property


    Public Property vnama_customer() As String
        Get
            Dim o As Object = ViewState("vnama_customer")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vnama_customer") = value
        End Set
    End Property

    Public Property vnilai_retur() As Decimal
        Get
            Dim o As Object = ViewState("vnilai_retur")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vnilai_retur") = value
        End Set
    End Property

    ''dendi
    'Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
    '    If e.CommandName = "LinkItem" Then
    '        Response.Redirect("~/detil_penerimaan_pembayaran_penjualan.aspx?vno_so=" & CType(e.Item.FindControl("lbl_no"), Label).Text & "&vpaging=" & Me.dg_data.CurrentPageIndex)
    '    End If
    'End Sub
    ''dendi

    Sub bindperiodepembayaran()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where tahun = " & Me.tb_tahun_pembayaran.Text
        sqlcom = sqlcom + " order by bulan"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_periode_pembayaran.DataSource = reader
        Me.dd_periode_pembayaran.DataTextField = "name"
        Me.dd_periode_pembayaran.DataValueField = "id"
        Me.dd_periode_pembayaran.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindjenispembayaran()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from jenis_pembayaran"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_jenis_pembayaran.DataSource = reader
        Me.dd_jenis_pembayaran.DataTextField = "name"
        Me.dd_jenis_pembayaran.DataValueField = "id"
        Me.dd_jenis_pembayaran.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindbank()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from bank_account"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_bank_account.DataSource = reader
        Me.dd_bank_account.DataTextField = "name"
        Me.dd_bank_account.DataValueField = "id"
        Me.dd_bank_account.DataBind()
        Me.dd_bank_account.Items.Add(New ListItem("--Rekening bank--", "0"))
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindmata_uang()
        sqlcom = "select id"
        sqlcom = sqlcom + " from currency"
        'Daniel
        sqlcom = sqlcom + " where id = 'USD' or id = 'IDR'"
        'Daniel
        sqlcom = sqlcom + " order by id"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_mata_uang.DataSource = reader
        Me.dd_mata_uang.DataTextField = "id"
        Me.dd_mata_uang.DataValueField = "id"
        Me.dd_mata_uang.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindpenjualan()
        Try
            sqlcom = "select sales_order.no, sales_order.so_no_text, sales_order.id_customer, convert(char, sales_order.tanggal, 103) as tanggal,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when jenis_penjualan = 'T' then 'Tunai'"
            sqlcom = sqlcom + " when jenis_penjualan = 'K' then 'Kredit'"
            sqlcom = sqlcom + " end as jenis_penjualan,"
            sqlcom = sqlcom + " sales_order.id_currency as mata_uang, isnull(sales_order.rate,0) as rate, isnull(sales_order.ppn,0) as ppn,  "
            sqlcom = sqlcom + " sales_order.lama_pembayaran, convert(char, sales_order.tgl_invoice, 103) as tgl_invoice,"
            sqlcom = sqlcom + " convert(char, sales_order.tgl_jatuh_tempo, 103) as tgl_jatuh_tempo,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when isnull(sales_order.ppn,0) = 0 then"
            sqlcom = sqlcom + " isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100))"
            sqlcom = sqlcom + " from sales_order_detail where no_sales_order = sales_order.no),0)"
            sqlcom = sqlcom + " when isnull(sales_order.ppn,0) = 10 then"
            sqlcom = sqlcom + " isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100))"
            sqlcom = sqlcom + " from sales_order_detail where no_sales_order = sales_order.no),0) + "
            sqlcom = sqlcom + " isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100))"
            sqlcom = sqlcom + " from sales_order_detail where no_sales_order = sales_order.no),0) * 0.1"
            sqlcom = sqlcom + " end as total_nilai,"
            sqlcom = sqlcom + " daftar_customer.name as nama_customer, user_list.nama_pegawai as nama_sales"
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
            sqlcom = sqlcom + " where sales_order.no = " & Me.vno_so
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_so.Text = reader.Item("so_no_text").ToString
                Me.lbl_tgl_so.Text = reader.Item("tanggal").ToString
                Me.vid_customer = reader.Item("id_customer").ToString
                Me.lbl_jenis_penjualan.Text = reader.Item("jenis_penjualan").ToString
                Me.lbl_nama_customer.Text = reader.Item("nama_customer").ToString
                Me.lbl_lama_pembayaran.Text = reader.Item("lama_pembayaran").ToString
                Me.lbl_mata_uang.Text = reader.Item("mata_uang").ToString
                Me.lbl_kurs.Text = reader.Item("rate").ToString
                Me.lbl_ppn.Text = reader.Item("ppn").ToString
                Me.lbl_tgl_invoice.Text = reader.Item("tgl_invoice").ToString
                Me.lbl_tgl_jatuh_tempo.Text = reader.Item("tgl_jatuh_tempo").ToString

                If Me.lbl_mata_uang.Text = "IDR" Then
                    Me.lbl_total_nilai.Text = FormatNumber(reader.Item("total_nilai").ToString, 2)
                Else
                    Me.lbl_total_nilai.Text = FormatNumber(reader.Item("total_nilai").ToString, 3)
                End If

            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception

        End Try
    End Sub

    Sub bindnilai_retur()
        Try
            Dim x As Integer = 0
            If Me.lbl_mata_uang.Text = "USD" Then
                x = 3
            Else
                x = 2
            End If

            sqlcom = " select isnull("
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when x.ppn = 0 then "
            sqlcom = sqlcom + " isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) - "
            sqlcom = sqlcom + " (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) "
            sqlcom = sqlcom + "    when x.ppn = 10 then "
            sqlcom = sqlcom + " (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) - "
            sqlcom = sqlcom + " (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +  "
            sqlcom = sqlcom + " ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) - "
            sqlcom = sqlcom + " (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1)) "
            sqlcom = sqlcom + " end, 0) as nilai_retur "
            sqlcom = sqlcom + " from retur_sales_order_detil "
            sqlcom = sqlcom + " inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so "
            sqlcom = sqlcom + " inner join sales_order x on x.no = retur_sales_order.no_sales_order "
            sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = x.no "
            sqlcom = sqlcom + " and sales_order_detail.id_product = retur_sales_order_detil.id_produk "
            sqlcom = sqlcom + " where retur_sales_order.no_sales_order = " & Me.vno_so
            sqlcom = sqlcom + " group by x.ppn, x.rate"

            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vnilai_retur = Decimal.ToDouble(reader.Item("nilai_retur").ToString)

                Me.lbl_nilai_retur_idr.Text = FormatNumber(Me.vnilai_retur * Decimal.ToDouble(Me.lbl_kurs.Text), x)
                Me.lbl_nilai_retur_usd.Text = FormatNumber(Me.vnilai_retur, x)
            Else
                Me.vnilai_retur = 0
                Me.lbl_nilai_retur_idr.Text = FormatNumber(0, x)
                Me.lbl_nilai_retur_usd.Text = FormatNumber(0, x)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception

        End Try
    End Sub

    Sub hitungsisa()
        Try
            Dim vtotal_invoice As Decimal = 0
            Dim vtotal_pembayaran As Decimal = 0

            'sqlcom = "select id_currency, isnull(sum(isnull(round(nilai_pembayaran,3),0) + isnull(round(potongan,3),0) - isnull(round(kelebihan,3),0)),0) as total_pembayaran"
            'sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
            'sqlcom = sqlcom + " where no_so = " & Me.vno_so
            'sqlcom = sqlcom + " group by id_currency"
            'reader = connection.koneksi.SelectRecord(sqlcom)
            'Do While reader.Read
            'If reader.Item("id_currency").ToString = "IDR" Then
            'vtotal_pembayaran = Math.round(vtotal_pembayaran,3) + Math.Round(Decimal.toDouble(reader.Item("total_pembayaran").ToString),2)
            'Else
            'vtotal_pembayaran = Math.round(vtotal_pembayaran,3) + Math.Round(Decimal.ToDouble(reader.Item("total_pembayaran").ToString),3) * Math.Round(Decimal.ToDouble(Me.lbl_kurs.Text),3)
            'End If
            'Loop
            'reader.Close()
            'connection.koneksi.CloseKoneksi()

            vtotal_invoice = Decimal.ToDouble(Me.lbl_total_nilai.Text) * Decimal.ToDouble(Me.lbl_kurs.Text)


            sqlcom = "select "
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when isnull(sales_order.ppn,0) = 0 then "
            sqlcom = sqlcom + "         isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) "
            sqlcom = sqlcom + "         from sales_order_detail where no_sales_order = sales_order.no),0) "
            sqlcom = sqlcom + "    when isnull(sales_order.ppn,0) = 10 then "
            sqlcom = sqlcom + "         isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) "
            sqlcom = sqlcom + "         from sales_order_detail where no_sales_order = sales_order.no),0) +  "
            sqlcom = sqlcom + "         isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) "
            sqlcom = sqlcom + "         from sales_order_detail where no_sales_order = sales_order.no),0) * 0.1  "
            sqlcom = sqlcom + "    end * sales_order.rate "
            sqlcom = sqlcom + " -  "
            sqlcom = sqlcom + " ((select isnull(sum((isnull(nilai_pembayaran,0) + isnull(biaya_bank,0) + isnull(potongan,0) - isnull(kelebihan,0))),0) * sales_order.rate "
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no "
            sqlcom = sqlcom + " and id_currency = 'USD')  "
            sqlcom = sqlcom + " +  "
            sqlcom = sqlcom + " (select isnull(sum(isnull(nilai_pembayaran,0) + isnull(biaya_bank,0) + isnull(potongan,0) - isnull(kelebihan,0)),0) "
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no "
            sqlcom = sqlcom + " and id_currency = 'IDR')) "
            sqlcom = sqlcom + " as total_nilai "
            sqlcom = sqlcom + " from sales_order "
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer "
            sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales "
            sqlcom = sqlcom + " where sales_order.no = " & Me.vno_so
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If Me.lbl_mata_uang.Text = "IDR" Then
                    Me.lbl_sisa_nilai_invoice.Text = FormatNumber(Decimal.ToDouble(reader.Item("total_nilai").ToString) - Decimal.ToDouble(Me.lbl_nilai_retur_idr.Text), 2)
                Else
                    Me.lbl_sisa_nilai_invoice.Text = FormatNumber(Decimal.ToDouble(reader.Item("total_nilai").ToString) - Decimal.ToDouble(Me.lbl_nilai_retur_idr.Text), 3)
                End If
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            Dim vstatus As String = ""
            If Decimal.ToDouble(Me.lbl_sisa_nilai_invoice.Text) = 0 Then
                Me.lbl_status.Text = "Sudah lunas"
                vstatus = "S"
            Else
                Me.lbl_status.Text = "Belum lunas"
                vstatus = "B"
            End If

            sqlcom = "update sales_order"
            sqlcom = sqlcom + " set status_invoice = '" & vstatus & "'"
            sqlcom = sqlcom + " where no = " & Me.vno_so
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clearform()
        Me.tb_tgl_bayar.Text = ""
        Me.tb_no_cek_giro.Text = ""
        Me.tb_tgl_cek_giro.Text = ""
        Me.tb_tgl_jatuh_tempo_cek_giro.Text = ""
        Me.tb_nilai_pembayaran.Text = ""
        Me.tb_potongan.Text = ""
        Me.tb_kelebihan.Text = ""
    End Sub

    Sub loadgrid()
        Try

            Me.clearform()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, no_so, id_periode_pembayaran, convert(char, tanggal, 103) as tanggal, id_jenis_pembayaran, id_bank_account, no_cek_giro,"
            sqlcom = sqlcom + " convert(char, tgl_cek_giro, 103) as tgl_cek_giro, convert(char, tgl_jatuh_tempo_cek_giro, 103) as tgl_jatuh_tempo_cek_giro,"
            sqlcom = sqlcom + " isnull(nilai_pembayaran,0) as nilai_pembayaran, isnull(potongan,0) as potongan,"
            sqlcom = sqlcom + " isnull(kelebihan,0) as kelebihan, isnull(biaya_bank,0) as biaya_bank, id_currency, is_submit, keterangan,"
            'Daniel
            sqlcom = sqlcom + " isnull(biaya_pengiriman_barang,0) as biaya_pengiriman_barang"
            'Daniel
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
            sqlcom = sqlcom + " where no_so = " & Me.vno_so
            sqlcom = sqlcom + " order by seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "pembayaran_invoice_penjualan")
                Me.dg_data.DataSource = ds.Tables("pembayaran_invoice_penjualan").DefaultView

                If ds.Tables("pembayaran_invoice_penjualan").Rows.Count > 0 Then
                    If ds.Tables("pembayaran_invoice_penjualan").Rows.Count > 10 Then
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
                    Me.btn_submit.Visible = True

                    'If Session.Item("code") = 1 Then
                    '    Me.btn_unsubmit.Visible = True
                    'End If



                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name"
                        sqlcom = sqlcom + " from transaction_period"
                        sqlcom = sqlcom + " order by bulan"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_periode"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        sqlcom = "select id, name"
                        sqlcom = sqlcom + " from jenis_pembayaran"
                        sqlcom = sqlcom + " order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_jenis"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        sqlcom = "select id, name"
                        sqlcom = sqlcom + " from bank_account"
                        sqlcom = sqlcom + " order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).Items.Add(New ListItem("--Nama bank--", "0"))

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).SelectedValue = "0"
                        Else
                            CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text
                        End If

                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        sqlcom = "select id"
                        sqlcom = sqlcom + " from currency"
                        sqlcom = sqlcom + " order by id"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataTextField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_mata_uang"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If CType(Me.dg_data.Items(x).FindControl("lbl_is_submit"), Label).Text = "B" Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                        End If

                    Next
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
                    Me.btn_submit.Visible = False

                    Me.btn_unsubmit.Visible = False


                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()

            Me.hitungsisa()

        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.bindpenjualan()
            Me.bindnilai_retur()
            Me.tb_tahun_pembayaran.Text = Now.Year
            Me.bindperiodepembayaran()
            Me.bindjenispembayaran()
            Me.bindbank()
            Me.bindmata_uang()
            Me.pilihanjenis_pembayaran()
            Me.dd_bank_account.SelectedValue = "0"
            'dendi
            If Me.vpaging <> 0 Then
                Me.dg_data.CurrentPageIndex = Me.vpaging
            End If
            'dendi
            Me.dg_data.CurrentPageIndex = 0
            Me.loadgrid()

            'If Session.Item("code") <> 1 Then
            '    Me.btn_unsubmit.Visible = False
            'End If

            'tradingClass.ViewButtonUnsubmit(Me.btn_unsubmit)

        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("payment_sales_order.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&voption=" & Me.voption & "&vpaging=" & Me.vpaging)
    End Sub

    Protected Sub btn_refresh_bulan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_refresh_bulan.Click
        Me.bindperiodepembayaran()
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tgl_bayar.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran terlebih dahulu"
            Else
                If Me.dd_jenis_pembayaran.SelectedValue = 2 Or Me.dd_jenis_pembayaran.SelectedValue = 3 Then
                    If String.IsNullOrEmpty(Me.tb_no_cek_giro.Text) Then
                        Me.lbl_msg.Text = "Silahkan mengisi no. cek/giro terlebih dahulu"
                        Exit Sub
                    ElseIf String.IsNullOrEmpty(Me.tb_tgl_jatuh_tempo_cek_giro.Text) Then
                        Me.lbl_msg.Text = "Silahkan mengisi tgl. jatuh tempo cek/giro terlebih dahulu"
                        Exit Sub
                    End If
                End If

                If Me.dd_mata_uang.SelectedValue = "IDR" And Me.dd_jenis_pembayaran.SelectedValue = 5 Then
                    If Decimal.ToDouble(Me.lbl_jumlah_deposit_idr.Text) < Decimal.ToDouble(Me.tb_nilai_pembayaran.Text) Then
                        Me.lbl_msg.Text = "Jumlah Deposit customer lebih kecil dari Jumlah pembayaran"
                        Exit Sub
                    End If
                ElseIf Me.dd_mata_uang.SelectedValue = "USD" And Me.dd_jenis_pembayaran.SelectedValue = 5 Then
                    If Decimal.ToDouble(Me.lbl_jumlah_deposit_usd.Text) < Decimal.ToDouble(Me.tb_nilai_pembayaran.Text) Then
                        Me.lbl_msg.Text = "Jumlah Deposit customer lebih kecil dari Jumlah pembayaran"
                        Exit Sub
                    End If
                End If

                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vmax from pembayaran_invoice_penjualan"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vtgl As String = Me.tb_tgl_bayar.Text.Substring(3, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(0, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(6, 4)
                Dim vtgl_cek_giro As String = ""
                Dim vtgl_jatuh_tempo_cek_giro As String = ""

                If Not String.IsNullOrEmpty(Me.tb_tgl_cek_giro.Text) Then
                    vtgl_cek_giro = Me.tb_tgl_cek_giro.Text.Substring(3, 2) & "/" & Me.tb_tgl_cek_giro.Text.Substring(0, 2) & "/" & Me.tb_tgl_cek_giro.Text.Substring(6, 4)
                End If

                If Not String.IsNullOrEmpty(Me.tb_tgl_jatuh_tempo_cek_giro.Text) Then
                    vtgl_jatuh_tempo_cek_giro = Me.tb_tgl_jatuh_tempo_cek_giro.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo_cek_giro.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo_cek_giro.Text.Substring(6, 4)
                End If

                sqlcom = "insert into pembayaran_invoice_penjualan(seq, no_so, id_periode_pembayaran, tanggal,"
                sqlcom = sqlcom + " id_jenis_pembayaran, id_bank_account, no_cek_giro, tgl_cek_giro, tgl_jatuh_tempo_cek_giro, nilai_pembayaran, potongan,"
                sqlcom = sqlcom + " kelebihan, biaya_bank, id_currency, is_submit, keterangan,"
                'Daniel
                sqlcom = sqlcom + " biaya_pengiriman_barang)"
                'Daniel
                sqlcom = sqlcom + " values(" & vmax & "," & Me.vno_so & "," & Me.dd_periode_pembayaran.SelectedValue & ",'" & vtgl & "'"
                sqlcom = sqlcom + "," & Me.dd_jenis_pembayaran.SelectedValue

                If Me.dd_jenis_pembayaran.SelectedValue = 1 Or Me.dd_jenis_pembayaran.SelectedValue = 5 Then
                    sqlcom = sqlcom + ", NULL"
                Else
                    sqlcom = sqlcom + "," & Me.dd_bank_account.SelectedValue
                End If

                If Me.dd_jenis_pembayaran.SelectedValue = 1 Or Me.dd_jenis_pembayaran.SelectedValue = 5 Then
                    sqlcom = sqlcom + ", NULL"
                Else
                    sqlcom = sqlcom + ",'" & Me.tb_no_cek_giro.Text & "'"
                End If

                If String.IsNullOrEmpty(Me.tb_tgl_cek_giro.Text) Then
                    sqlcom = sqlcom + ", NULL"
                Else
                    sqlcom = sqlcom + ",'" & vtgl_cek_giro & "'"
                End If

                If String.IsNullOrEmpty(Me.tb_tgl_jatuh_tempo_cek_giro.Text) Then
                    sqlcom = sqlcom + ", NULL"
                Else
                    sqlcom = sqlcom + ",'" & vtgl_jatuh_tempo_cek_giro & "'"
                End If

                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_nilai_pembayaran.Text)

                If String.IsNullOrEmpty(Me.tb_potongan.Text) Then
                    sqlcom = sqlcom + ", 0"
                Else
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_potongan.Text)
                End If

                If String.IsNullOrEmpty(Me.tb_kelebihan.Text) Then
                    sqlcom = sqlcom + ", 0"
                Else
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_kelebihan.Text)
                End If

                If String.IsNullOrEmpty(Me.tb_biaya_bank.Text) Then
                    sqlcom = sqlcom + ", 0"
                Else
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_biaya_bank.Text)
                End If

                sqlcom = sqlcom + ",'" & Me.dd_mata_uang.SelectedValue & "','B', '" & Me.tb_keterangan.Text

                'Daniel
                If String.IsNullOrEmpty(Me.tb_biaya_pengiriman_barang.Text) Then
                    sqlcom = sqlcom + "', 0)"
                Else
                    sqlcom = sqlcom + "'," & Decimal.ToDouble(Me.tb_biaya_pengiriman_barang.Text) & ")"
                End If
                'Daniel

                connection.koneksi.InsertRecord(sqlcom)
                Me.tradingClass.Alert("Data sudah disimpan", Me.Page)
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete pembayaran_invoice_penjualan"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
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

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tanggal"), TextBox).Text) Then
                        Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran terlebih dahulu"
                    Else
                        Dim vtgl As String = CType(Me.dg_data.Items(x).FindControl("tb_tanggal"), TextBox).Text
                        vtgl = vtgl.Substring(3, 2) & "/" & vtgl.Substring(0, 2) & "/" & vtgl.Substring(6, 4)

                        Dim vtgl_cek_giro As String = ""
                        If Not String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_no_cek_giro"), TextBox).Text) Then
                            vtgl_cek_giro = CType(Me.dg_data.Items(x).FindControl("tb_tgl_no_cek_giro"), TextBox).Text
                            vtgl_cek_giro = vtgl_cek_giro.Substring(3, 2) & "/" & vtgl_cek_giro.Substring(0, 2) & "/" & vtgl_cek_giro.Substring(6, 4)
                        End If

                        Dim vtgl_jatuh_tempo_cek_giro As String = ""
                        If Not String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_jatuh_tempo_cek_giro"), TextBox).Text) Then
                            vtgl_jatuh_tempo_cek_giro = CType(Me.dg_data.Items(x).FindControl("tb_tgl_jatuh_tempo_cek_giro"), TextBox).Text
                            vtgl_jatuh_tempo_cek_giro = vtgl_jatuh_tempo_cek_giro.Substring(3, 2) & "/" & vtgl_jatuh_tempo_cek_giro.Substring(0, 2) & "/" & vtgl_jatuh_tempo_cek_giro.Substring(6, 4)
                        End If

                        sqlcom = "update pembayaran_invoice_penjualan"
                        sqlcom = sqlcom + " set id_periode_pembayaran = " & CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).SelectedValue & ","
                        sqlcom = sqlcom + " tanggal = '" & vtgl & "',"
                        sqlcom = sqlcom + " id_jenis_pembayaran = " & CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).SelectedValue & ","

                        If CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).SelectedValue = 1 Then
                            sqlcom = sqlcom + " id_bank_account = NULL,"
                        Else
                            sqlcom = sqlcom + " id_bank_account = " & CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).SelectedValue & ","
                        End If

                        sqlcom = sqlcom + " no_cek_giro = '" & CType(Me.dg_data.Items(x).FindControl("tb_no_cek_giro"), TextBox).Text & "',"

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_no_cek_giro"), TextBox).Text) Then
                            sqlcom = sqlcom + " tgl_cek_giro = NULL,"
                        Else
                            sqlcom = sqlcom + " tgl_cek_giro = '" & vtgl_cek_giro & "',"
                        End If

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_jatuh_tempo_cek_giro"), TextBox).Text) Then
                            sqlcom = sqlcom + " tgl_jatuh_tempo_cek_giro = NULL,"
                        Else
                            sqlcom = sqlcom + " tgl_jatuh_tempo_cek_giro = '" & vtgl_jatuh_tempo_cek_giro & "',"
                        End If

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_nilai_pembayaran"), TextBox).Text) Then
                            sqlcom = sqlcom + " nilai_pembayaran = 0,"
                        Else
                            sqlcom = sqlcom + " nilai_pembayaran = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_nilai_pembayaran"), TextBox).Text) & ","
                        End If

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_potongan"), TextBox).Text) Then
                            sqlcom = sqlcom + " potongan = 0,"
                        Else
                            sqlcom = sqlcom + " potongan = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_potongan"), TextBox).Text) & ","
                        End If

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_kelebihan"), TextBox).Text) Then
                            sqlcom = sqlcom + " kelebihan = 0,"
                        Else
                            sqlcom = sqlcom + " kelebihan = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kelebihan"), TextBox).Text) & ","
                        End If

                        sqlcom = sqlcom + " id_currency = '" & CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue & "',"
                        sqlcom = sqlcom + " keterangan = '" & CType(Me.dg_data.Items(x).FindControl("tb_keterangan"), TextBox).Text & "',"

                        'Daniel
                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_biaya_pengiriman_barang"), TextBox).Text) Then
                            sqlcom = sqlcom + " biaya_pengiriman_barang = 0,"
                        Else
                            sqlcom = sqlcom + " biaya_pengiriman_barang = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_biaya_pengiriman_barang"), TextBox).Text)
                        End If
                        'Daniel

                        sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                        connection.koneksi.UpdateRecord(sqlcom)
                        Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
                    End If
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub pilihanjenis_pembayaran()
        If Me.dd_jenis_pembayaran.SelectedValue = 1 Then
            Me.dd_bank_account.Enabled = False
            Me.tb_no_cek_giro.Enabled = False
            Me.tb_tgl_cek_giro.Enabled = False
            Me.tb_tgl_jatuh_tempo_cek_giro.Enabled = False
            Me.tbl_deposit.Visible = False
            Me.dd_bank_account.SelectedValue = "0"
        ElseIf Me.dd_jenis_pembayaran.SelectedValue = 4 Then
            Me.dd_bank_account.Enabled = True
            Me.tb_no_cek_giro.Enabled = False
            Me.tb_tgl_cek_giro.Enabled = False
            Me.tb_tgl_jatuh_tempo_cek_giro.Enabled = False
            Me.tbl_deposit.Visible = False
        ElseIf Me.dd_jenis_pembayaran.SelectedValue = 2 Or Me.dd_jenis_pembayaran.SelectedValue = 3 Then
            Me.dd_bank_account.Enabled = True
            Me.tb_no_cek_giro.Enabled = True
            Me.tb_tgl_cek_giro.Enabled = True
            Me.tb_tgl_jatuh_tempo_cek_giro.Enabled = True
            Me.tbl_deposit.Visible = False
        ElseIf Me.dd_jenis_pembayaran.SelectedValue = 5 Then
            Me.dd_bank_account.Enabled = True
            Me.tb_no_cek_giro.Enabled = False
            Me.tb_tgl_cek_giro.Enabled = False
            Me.tb_tgl_jatuh_tempo_cek_giro.Enabled = False
            Me.tbl_deposit.Visible = True
            Me.dd_bank_account.SelectedValue = "0"
            Me.hitungdeposit()
        End If
    End Sub

    Protected Sub dd_jenis_pembayaran_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_jenis_pembayaran.SelectedIndexChanged
        Me.pilihanjenis_pembayaran()
    End Sub

    Sub hitungdeposit()
        Try
            'hitung kelebihan
            Dim vkelebihan_idr As Decimal = 0
            Dim vkelebihan_usd As Decimal = 0
            sqlcom = "select isnull(sum(isnull(kelebihan,0)),0) as vkelebihan, id_currency"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
            sqlcom = sqlcom + " where no_so in (select no from sales_order where id_customer = " & Me.vid_customer & ")"
            sqlcom = sqlcom + " group by id_currency"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read()
                If reader.Item("id_currency").ToString = "IDR" Then
                    vkelebihan_idr = reader.Item("vkelebihan").ToString
                Else
                    vkelebihan_usd = reader.Item("vkelebihan").ToString
                End If
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()

            'hitung pembayaran dengan kelebihan
            Dim vpakai_kelebihan_idr As Decimal = 0
            Dim vpakai_kelebihan_usd As Decimal = 0
            sqlcom = "select isnull(sum(isnull(kelebihan,0)),0) as vpakai_kelebihan, id_currency"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
            sqlcom = sqlcom + " where no_so in (select no from sales_order where id_customer = " & Me.vid_customer & ")"
            sqlcom = sqlcom + " and id_jenis_pembayaran = 5"
            sqlcom = sqlcom + " group by id_currency"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read()
                If reader.Item("id_currency").ToString = "IDR" Then
                    vpakai_kelebihan_idr = reader.Item("vpakai_kelebihan").ToString
                Else
                    vpakai_kelebihan_usd = reader.Item("vpakai_kelebihan").ToString
                End If
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()

            Me.lbl_jumlah_deposit_idr.Text = FormatNumber(Decimal.ToDouble(vkelebihan_idr) - Decimal.ToDouble(vpakai_kelebihan_idr), 3)
            Me.lbl_jumlah_deposit_usd.Text = FormatNumber(Decimal.ToDouble(vkelebihan_usd) - Decimal.ToDouble(vpakai_kelebihan_usd), 3)

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
        Dim vtgl As String = Me.vtanggal.Substring(3, 2) & "/" & Me.vtanggal.Substring(0, 2) & "/" & Me.vtanggal.Substring(6, 4)

        sqlcom = "select sales_order.id_customer, sales_order.rate, daftar_customer.name as nama_customer"
        sqlcom = sqlcom + " from sales_order"
        sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
        sqlcom = sqlcom + " where so_no_text = '" & Me.vno_invoice & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.vnama_customer = reader.Item("nama_customer").ToString
            Me.vkurs = reader.Item("rate").ToString
            If Me.vid_currency = "IDR" Then
                'Me.vnilai = Decimal.ToDouble(Me.vnilai) - Decimal.ToDouble(Me.vkelebihan)
                Me.vnilai = Decimal.ToDouble(Me.vnilai)

                If Decimal.ToDouble(Me.vbiaya_bank) <> 0 Then
                    Me.vnilai = Decimal.ToDouble(Me.vnilai) - Decimal.ToDouble(Me.vbiaya_bank)
                End If
            Else
                'Me.vnilai = Math.round((Decimal.ToDouble(Me.vnilai) - Decimal.ToDouble(Me.vkelebihan)) * Decimal.ToDouble(Me.vkurs),0)
                Me.vnilai = Math.Round((Decimal.ToDouble(Me.vnilai)) * Decimal.ToDouble(Me.vkurs), 0)

                If Decimal.ToDouble(Me.vbiaya_bank) <> 0 Then
                    Me.vnilai = Math.Round((Decimal.ToDouble(Me.vnilai) - Decimal.ToDouble(Me.vbiaya_bank)) * Decimal.ToDouble(Me.vkurs), 0)
                End If
            End If
            Me.vid_customer = reader.Item("id_customer").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        'debet
        ' akun_giro mundur, piutang dagang
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vseq & "','" & vtgl & "','TRMBYRJUAL','" & Me.vakun_piutang_giro_mundur & "',"
        sqlcom = sqlcom + "'" & Me.vakun_piutang_dagang & "'," & Decimal.ToDouble(Me.vnilai) & ",0, 'Penerimaan pembayaran penjualan invoice no." & Me.vno_invoice & "(" & Me.vnama_customer & ")'"
        sqlcom = sqlcom + "," & Me.vid_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.vkurs)

        If Me.vid_currency = "IDR" Then
            sqlcom = sqlcom + ", 0, 0)"
        Else
            sqlcom = sqlcom + "," & Decimal.ToDouble(Me.vnilai) & ", 0)"
        End If

        connection.koneksi.InsertRecord(sqlcom)

        'kredit
        ' akun_piutang_dagang, akun_giro mundur
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vseq & "','" & vtgl & "','TRMBYRJUAL','" & Me.vakun_piutang_dagang & "',"
        sqlcom = sqlcom + "'" & Me.vakun_piutang_giro_mundur & "', 0," & Decimal.ToDouble(Me.vnilai) & ",'Penerimaan pembayaran penjualan invoice no." & Me.vno_invoice & "(" & Me.vnama_customer & ")'"
        sqlcom = sqlcom + "," & Me.vid_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.vkurs)

        If Me.vid_currency = "IDR" Then
            sqlcom = sqlcom + ", 0, 0)"
        Else
            sqlcom = sqlcom + "," & Decimal.ToDouble(Me.vnilai) & ", 0)"
        End If

        connection.koneksi.InsertRecord(sqlcom)



        If Decimal.ToDouble(Me.vbiaya_bank) <> 0 Then

            'debet
            ' biaya bank, piutang dagang
            Me.vakun_biaya_bank = "63.12"
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vseq & "','" & vtgl & "','TRMBYRJUAL','" & Me.vakun_biaya_bank & "',"
            sqlcom = sqlcom + "'" & Me.vakun_piutang_dagang & "'," & Decimal.ToDouble(Me.vbiaya_bank) & ",0, 'Penerimaan pembayaran penjualan invoice no." & Me.vno_invoice & "(" & Me.vnama_customer & ")'"
            sqlcom = sqlcom + "," & Me.vid_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.vkurs)

            If Me.vid_currency = "IDR" Then
                sqlcom = sqlcom + ", 0, 0)"
            Else
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.vnilai) & ", 0)"
            End If

            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' akun_piutang_dagang, akun_giro mundur
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vseq & "','" & vtgl & "','TRMBYRJUAL','" & Me.vakun_piutang_dagang & "',"
            sqlcom = sqlcom + "'" & Me.vakun_biaya_bank & "', 0," & Decimal.ToDouble(Me.vbiaya_bank) & ",'Penerimaan pembayaran penjualan invoice no." & Me.vno_invoice & "(" & Me.vnama_customer & ")'"
            sqlcom = sqlcom + "," & Me.vid_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.vkurs)

            If Me.vid_currency = "IDR" Then
                sqlcom = sqlcom + ", 0, 0)"
            Else
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.vnilai) & ", 0)"
            End If

            connection.koneksi.InsertRecord(sqlcom)

        End If

    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            'ceak akun giro mundur, akun piutang dagang customer
            sqlcom = "select akun_piutang_giro_mundur, akun_piutang_dagang"
            sqlcom = sqlcom + " from daftar_customer"
            sqlcom = sqlcom + " where id = " & Me.vid_customer
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_piutang_giro_mundur = reader.Item("akun_piutang_giro_mundur").ToString.Trim
                Me.vakun_piutang_dagang = reader.Item("akun_piutang_dagang").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_piutang_giro_mundur) Then
                Me.lbl_msg.Text = "Akun piutang giro mundur untuk customer tersebut tidak ada"
                Exit Sub
            End If

            If String.IsNullOrEmpty(Me.vakun_piutang_dagang) Then
                Me.lbl_msg.Text = "Akun piutang dagang untuk customer tersebut tidak ada"
                Exit Sub
            End If


            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then

                    sqlcom = "update pembayaran_invoice_penjualan"
                    sqlcom = sqlcom + " set is_submit = 'S'"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)

                    Me.vtanggal = CType(Me.dg_data.Items(x).FindControl("tb_tanggal"), TextBox).Text.ToString
                    Me.vid_transaksi = CType(Me.dg_data.Items(x).FindControl("lbl_periode"), Label).Text
                    Me.vseq = CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    Me.vnilai = Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_nilai_pembayaran"), TextBox).Text)

                    Me.vkelebihan = Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kelebihan"), TextBox).Text)

                    Me.vbiaya_bank = Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_biaya_bank"), TextBox).Text)

                    'Me.vid_kas = CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text
                    Me.vid_currency = CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue
                    Me.vno_invoice = Me.lbl_no_so.Text

                    If Me.dd_jenis_pembayaran.SelectedValue <> 5 Then 'pembayaran dengan deposit
                        'Daniel
                        'Me.jurnal()
                        Me.GL()
                        'Daniel
                    End If

                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_unsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_unsubmit.Click
        Try
            Me.lbl_msg.Text = Nothing
            Dim keterangan As String = "Penerimaan Pembayaran Penjualan no. " & Me.lbl_no_so.Text

            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                sqlcom = "update pembayaran_invoice_penjualan"
                sqlcom = sqlcom + " set is_submit = 'B'"
                sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                connection.koneksi.UpdateRecord(sqlcom)

                Me.vid_transaksi = CType(Me.dg_data.Items(x).FindControl("lbl_periode"), Label).Text

                Me.tradingClass.DeleteAkunJurnal(keterangan, Me.vid_transaksi)
                Me.tradingClass.DeleteAkunGeneralLedger(keterangan, Me.vid_transaksi)

            Next

            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class
