Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Penjualan_detil_retur_sales_order
    Inherits System.Web.UI.UserControl
    Dim at As New KWATerbilang.cKWATerbilang

    'Daniel
    Public tradingClass As New tradingClass()
    Public status As String

    Sub GL()
        Try

            sqlcom = "select akun_piutang_dagang from daftar_customer where id = " & Me.vid_customer
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_piutang_dagang = reader.Item("akun_piutang_dagang").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_piutang_dagang) Then
                Me.lbl_msg.Text = "Akun piutang dagang pada data customer tersebut tidak ada"
                Exit Sub
            End If

            sqlcom = "select akun_ppn_masukan from akun_penerimaan_inv_eks_imp"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_ppn_masukan = reader.Item("akun_ppn_masukan").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_ppn_masukan) Then
                Me.lbl_msg.Text = "Akun ppn masukan di Akun penerimaan invoice ekspedisi impor tidak ada"
                Exit Sub
            End If

            Dim nonppn As Decimal = (System.Convert.ToDecimal(Me.lbl_total_nilai.Text) * (100 - System.Convert.ToDecimal(Me.lbl_ppn.Text))) / 100
            Dim nonppn_idr As Decimal = nonppn * System.Convert.ToDecimal(Me.lbl_kurs.Text)
            Dim ppn As Decimal = (System.Convert.ToDecimal(Me.lbl_total_nilai.Text) * System.Convert.ToDecimal(Me.lbl_ppn.Text)) / 100
            Dim ppn_idr As Decimal = ppn * System.Convert.ToDecimal(Me.lbl_kurs.Text)
            Dim totalnilai_idr As Decimal = Val(nonppn_idr) + Val(ppn_idr)

            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim keterangan As String = "Retur Penjualan Barang dari Sales Order no. " & Me.lbl_no_penjualan.Text
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_retur.Text), id, Me.tradingClass.JurnalType("1"), keterangan, Me.vid_periode_transaksi)

            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_invoice.Text), Me.tradingClass.COA("RETUR PENJUALAN/LOKAL"), Me.vakun_piutang_dagang, nonppn_idr, 0, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, Me.lbl_kurs.Text, IIf(Me.lbl_mata_uang.Text = "IDR", 0, nonppn), 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_invoice.Text), Me.vakun_piutang_dagang, Me.tradingClass.COA("RETUR PENJUALAN/LOKAL"), 0, nonppn_idr, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, Me.lbl_kurs.Text, 0, IIf(Me.lbl_mata_uang.Text = "IDR", 0, nonppn), String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_invoice.Text), Me.vakun_ppn_masukan, Me.vakun_piutang_dagang, ppn_idr, 0, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, Me.lbl_kurs.Text, IIf(Me.lbl_mata_uang.Text = "IDR", 0, ppn), 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.lbl_tgl_invoice.Text), Me.vakun_piutang_dagang, Me.vakun_ppn_masukan, 0, ppn_idr, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, Me.lbl_kurs.Text, 0, IIf(Me.lbl_mata_uang.Text = "IDR", 0, ppn), String.Empty)

            sqlcom = "UPDATE retur_sales_order SET is_submit = 'S' WHERE no = " & Me.no_retur
            connection.koneksi.UpdateRecord(sqlcom)

            Me.tradingClass.Alert("Data sudah disubmit!", Me.Page)
            Me.loaddata()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Public Property vakun_piutang_dagang() As String
        Get
            Dim o As Object = ViewState("vakun_piutang_dagang")
          If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_piutang_dagang") = value
        End Set
    End Property

    Public Property vakun_ppn_masukan() As String
        Get
            Dim o As Object = ViewState("vakun_ppn_masukan")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_ppn_masukan") = value
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

    Private ReadOnly Property vno_retur() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_retur")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vpaging() As String
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Public Property no_retur() As Integer
        Get
            Dim o As Object = ViewState("no_retur")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("no_retur") = value
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

    Public Property vid_customer() As Integer
        Get
            Dim o As Object = ViewState("vid_customer")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_customer") = value
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
        Try
            sqlcom = "select id, name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_periode_transaksi.Text = reader.Item("name").ToString
                Me.vid_periode_transaksi = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindsales()
        Try
            Dim readersales As SqlClient.SqlDataReader
            sqlcom = "select nama_pegawai"
            sqlcom = sqlcom + " from user_list"
            sqlcom = sqlcom + " where code = (select id_sales from sales_order where no = " & Me.tb_no_penjualan.Text & ")"
            readersales = connection.koneksi.SelectRecord(sqlcom)
            readersales.Read()
            If readersales.HasRows Then
                Me.lbl_nama_sales.Text = readersales("nama_pegawai").ToString
            End If
            readersales.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindcustomer()
        Try
            Dim readercustomer As SqlClient.SqlDataReader
            sqlcom = "select id, name"
            sqlcom = sqlcom + " from daftar_customer"
            sqlcom = sqlcom + " where id = (select id_customer from sales_order where no = " & Me.tb_no_penjualan.Text & ")"
            readercustomer = connection.koneksi.SelectRecord(sqlcom)
            readercustomer.Read()
            If reader.HasRows Then
                Me.lbl_nama_customer.Text = readercustomer.Item("name").ToString
                Me.vid_customer = readercustomer.Item("id").ToString
            End If
            readercustomer.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clearform()
        Me.tb_tgl_retur.Text = ""
        Me.no_retur = 0
    End Sub

    Sub clearpenjualan()
        Me.tb_no_penjualan.Text = 0
        Me.lbl_no_penjualan.Text = "------"
        Me.link_popup_no_penjualan.Visible = True
    End Sub


    Sub bindpenjualan()
        Try
            sqlcom = "select so_no_text, convert(char, tanggal, 103) as tanggal, id_currency, isnull(rate,0) as kurs, isnull(ppn,0) as ppn"
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " where no = " & Me.tb_no_penjualan.Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_penjualan.Text = reader.Item("so_no_text").ToString
                Me.lbl_tgl_penjualan.Text = reader.Item("tanggal").ToString
                Me.lbl_mata_uang.Text = reader.Item("id_currency").ToString
                Me.lbl_kurs.Text = FormatNumber(Decimal.ToDouble(reader.Item("kurs").ToString), 2)
                Me.lbl_ppn.Text = Decimal.ToDouble(reader.Item("ppn").ToString)
                Me.bindsales()
                Me.bindcustomer()
                Me.popupproduk()
                Me.popuinvoice_penjualan()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clearinvoicepenjualan()
        Me.tb_no_invoice_penjualan.Text = 0
        Me.lbl_no_invoice_penjualan.Text = "------"
        Me.link_popup_invoice_penjualan.Visible = True
    End Sub

    Sub bindinvoicepenjualan()
        Try
            sqlcom = "select so_no_text, convert(char, tgl_invoice, 103) as tanggal,  id_currency, isnull(rate,0) as kurs, isnull(ppn,0) as ppn"
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " where no = " & Me.tb_no_invoice_penjualan.Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_invoice_penjualan.Text = reader.Item("so_no_text").ToString
                Me.lbl_tgl_invoice.Text = reader.Item("tanggal").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clearproduk()
        Me.tb_id_produk.Text = 0
        Me.lbl_nama_produk.Text = "------"
        Me.link_popup_produk.Visible = True
        Me.lbl_packaging.Text = ""
        Me.tb_qty.Text = ""
        Me.tb_harga.Text = ""
        Me.lbl_discount.Text = ""
        Me.tb_keterangan_item.Text = ""
    End Sub

    Sub bindproduk()
        Try
            sqlcom = "select product_item.nama_beli, measurement_unit.name as nama_satuan, product_item.is_packaging,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + unit_packaging.name + '/' + measurement_unit.name as packaging,"
            sqlcom = sqlcom + " unit_packaging.name as nama_satuan_packaging, sales_order_detail.harga_jual, sales_order_detail.discount"
            sqlcom = sqlcom + " from product_item"
            sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.id_product = product_item.id"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit unit_packaging on unit_packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " inner join product_price on product_price.id_product = product_item.id"
            sqlcom = sqlcom + " where product_item.id = " & Me.tb_id_produk.Text
            sqlcom = sqlcom + " and sales_order_detail.no_sales_order = " & Me.tb_no_penjualan.Text
            sqlcom = sqlcom + " group by product_item.nama_beli, measurement_unit.name, product_item.is_packaging, "
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + unit_packaging.name + '/' + measurement_unit.name, "
            sqlcom = sqlcom + " unit_packaging.name, sales_order_detail.harga_jual, sales_order_detail.discount"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_nama_produk.Text = reader.Item("nama_beli").ToString
                Me.lbl_packaging.Text = reader.Item("packaging").ToString
                'Me.tb_harga.Text = FormatNumber(Decimal.ToDouble(reader.Item("harga_jual").ToString), 2)
                Me.tb_harga.Text = reader.Item("harga_jual").ToString
                Me.lbl_discount.Text = FormatNumber(Decimal.ToDouble(reader.Item("discount").ToString), 2)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loaddata()
        Try
            If Me.vno_retur <> 0 Then
                Me.no_retur = Me.vno_retur
            End If

            sqlcom = "select no, no_retur_text, convert(char, tanggal, 103) as tanggal,"
            sqlcom = sqlcom + " no_sales_order, keterangan, is_submit, id_transaction_period, no_invoice"
            sqlcom = sqlcom + " from retur_sales_order"
            sqlcom = sqlcom + " where no = " & Me.no_retur
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_retur.Text = reader.Item("no_retur_text").ToString
                Me.tb_tgl_retur.Text = reader.Item("tanggal").ToString
                Me.tb_keterangan.Text = reader.Item("keterangan").ToString
                Me.tb_no_penjualan.Text = reader.Item("no_sales_order").ToString
                Me.tb_no_invoice_penjualan.Text = reader.Item("no_invoice").ToString

                'Daniel
                If reader.Item("is_submit").ToString = "B" Then
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                Else
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                End If
                'Daniel

                Me.bindpenjualan()
                Me.bindinvoicepenjualan()

                Me.tbl_produk.Visible = True
                Me.btn_print.Visible = True
                Me.btn_submit.Visible = True

            Else

                Me.tbl_produk.Visible = False
                Me.btn_print.Visible = False
                Me.btn_submit.Visible = False

            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.clearpenjualan()
            Me.clearinvoicepenjualan()
            Me.bindperiodetransaksi()
            Me.lbl_total_nilai.Text = 0
            Me.tb_tgl_retur.Text = Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year
            Me.tb_no_penjualan.Attributes.Add("style", "display: none;")
            Me.tb_no_invoice_penjualan.Attributes.Add("style", "display: none;")
            Me.tb_id_produk.Attributes.Add("style", "display: none;")
            Me.link_refresh_no_penjualan.Attributes.Add("style", "display: none;")
            Me.link_refresh_invoice_penjualan.Attributes.Add("style", "display: none;")
            Me.link_refresh_produk.Attributes.Add("style", "display: none;")

            'Me.link_popup_no_penjualan.Attributes.Add("onclick", "popup_penjualan('" & Me.tb_no_penjualan.ClientID & "','" & Me.link_refresh_no_penjualan.UniqueID & "')")
            'Me.link_popup_invoice_penjualan.Attributes.Add("onclick", "popup_penjualan('" & Me.tb_no_invoice_penjualan.ClientID & "','" & Me.link_refresh_invoice_penjualan.UniqueID & "')")

            Me.link_popup_no_penjualan.Attributes.Add("onclick", "popup_penjualan('" & Me.tb_no_penjualan.ClientID & "','" & Me.link_refresh_no_penjualan.UniqueID & "')")

            Me.link_popup_produk.Attributes.Clear()
            Me.link_popup_produk.Enabled = False
            Me.link_popup_invoice_penjualan.Attributes.Clear()
            Me.link_popup_invoice_penjualan.Enabled = False
            Me.loaddata()
            Me.loadgrid()
        End If
    End Sub

    Sub popupproduk()
        Me.link_popup_produk.Enabled = True
        Me.link_popup_produk.Attributes.Add("onclick", "popup_produk_retur('" & Me.tb_no_penjualan.Text & "','" & Me.tb_id_produk.ClientID & "','" & Me.link_refresh_produk.UniqueID & "')")
    End Sub

    Sub popuinvoice_penjualan()
        Me.link_popup_invoice_penjualan.Enabled = True
        Me.link_popup_invoice_penjualan.Attributes.Add("onclick", "popup_invoice_potong_penjualan('" & Me.vid_customer & "','" & Me.tb_no_invoice_penjualan.ClientID & "','" & Me.link_refresh_invoice_penjualan.UniqueID & "')")
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/retur_sales_order_list.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&voption=" & Me.voption & "&vsearch=" & Me.vsearch & "&vpaging=" & Me.vpaging)
    End Sub

    Protected Sub link_refresh_no_penjualan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_no_penjualan.Click
        Me.bindpenjualan()
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tgl_retur.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terlebih dahulu"
            ElseIf Me.tb_no_penjualan.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor penjualan terlebih dahulu"
            ElseIf Me.tb_no_invoice_penjualan.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor invoice penjualan yang dipotong terlebih dahulu"
            Else
                Dim vtgl As String = Me.tb_tgl_retur.Text.Substring(3, 2) & "/" & Me.tb_tgl_retur.Text.Substring(0, 2) & "/" & Me.tb_tgl_retur.Text.Substring(6, 4)

                If Me.no_retur = 0 Then
                    Dim vmax As Integer = 0
                    sqlcom = "select isnull(max(no),0) + 1 as vmax from retur_sales_order"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = reader.Item("vmax").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    Dim vno_retur_text As String = Nothing
                    sqlcom = "select isnull(max(convert(int, no_retur_text)),0) + 1 as vno_retur_text"
                    sqlcom = sqlcom + " from retur_sales_order"
                    sqlcom = sqlcom + " where convert(int, right(rtrim(convert(char, tanggal,103)),4)) = " & Me.vtahun
                    sqlcom = sqlcom + " and convert(int, substring(convert(char, tanggal, 103), 4,2)) = " & Me.vbulan
                    sqlcom = sqlcom + " and len(no_retur_text) = 3"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vno_retur_text = reader.Item("vno_retur_text").ToString.PadLeft(3, "0")
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into retur_sales_order(no, no_retur_text, tanggal, no_sales_order, keterangan, is_submit, id_transaction_period, no_invoice)"
                    sqlcom = sqlcom + " values(" & vmax & ",'" & vno_retur_text & "','" & vtgl & "'," & Me.tb_no_penjualan.Text & ",'" & Me.tb_keterangan.Text & "',"
                    sqlcom = sqlcom + "'B', " & Me.vid_periode_transaksi & "," & Me.tb_no_invoice_penjualan.Text & ")"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.no_retur = vmax
                    Me.tradingClass.Alert("Data sudah disimpan", Me.Page)
                Else
                    sqlcom = "update retur_sales_order"
                    sqlcom = sqlcom + " set tanggal = '" & vtgl & "',"
                    sqlcom = sqlcom + " no_sales_order = " & Me.tb_no_penjualan.Text & ","
                    sqlcom = sqlcom + " no_invoice = " & Me.tb_no_invoice_penjualan.Text & ","
                    sqlcom = sqlcom + " keterangan = '" & Me.tb_keterangan.Text & "'"
                    sqlcom = sqlcom + " where no = " & Me.no_retur
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If

                Me.loaddata()
            End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub link_refresh_produk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_produk.Click
        Me.bindproduk()
    End Sub

    Sub update_terbilang()
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

            sqlcom = "update retur_sales_order"
            sqlcom = sqlcom + " set terbilang = '" & Me.terbilang & "'"
            sqlcom = sqlcom + " where no = " & Me.no_retur
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindtotal_nilai()
        Try
            Dim x As Integer = 0
            If Me.lbl_mata_uang.Text = "USD" Then
                x = 3
            Else
                x = 0
            End If

            sqlcom = "select isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) - "
            sqlcom = sqlcom + " (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) as vtotal_nilai"
            sqlcom = sqlcom + " from retur_sales_order_detil"
            sqlcom = sqlcom + " inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so"
            sqlcom = sqlcom + " inner join sales_order on sales_order.no = retur_sales_order.no_sales_order"
            sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no"
            sqlcom = sqlcom + " and sales_order_detail.id_product = retur_sales_order_detil.id_produk"
            sqlcom = sqlcom + " where retur_sales_order_detil.no_retur_so = " & Me.no_retur
            sqlcom = sqlcom + " group by sales_order.ppn"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()

            If reader.HasRows Then
                If Me.lbl_ppn.Text = "0" Then
                    If Me.lbl_mata_uang.Text = "USD" Then
                        Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString, x)
                    Else
                        Me.lbl_total_nilai.Text = FormatNumber(Math.Round(Decimal.ToDouble(reader.Item("vtotal_nilai").ToString), 0), x)
                    End If
                Else
                    If Me.lbl_mata_uang.Text = "USD" Then
                        Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString + reader.Item("vtotal_nilai").ToString * 0.1, x)
                    Else
                        Me.lbl_total_nilai.Text = FormatNumber(Math.Round(Decimal.ToDouble(reader.Item("vtotal_nilai").ToString) + (Decimal.ToDouble(reader.Item("vtotal_nilai").ToString) * 0.1), 0), x)
                    End If
                End If
                'Me.lbl_total_nilai_idr.Text = FormatNumber(Math.Round(decimal.toDouble(Me.lbl_total_nilai.Text) * decimal.todouble(Me.tb_kurs.Text),0),2)
            End If

            Me.update_terbilang()

            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try
            Me.clearproduk()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)


            sqlcom = "select retur_sales_order_detil.no_retur_so, retur_sales_order_detil.id_produk, isnull(retur_sales_order_detil.qty,0) as qty, isnull(retur_sales_order_detil.harga,0) as harga, "
            sqlcom = sqlcom + " retur_sales_order_detil.seq, retur_sales_order_detil.keterangan,"
            sqlcom = sqlcom + " isnull(harga_jual,0) as harga_jual, isnull(discount,0) as discount, isnull(harga_jadi,0) as harga_jadi, isnull(qty_pending,0) as qty_pending,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + packaging.name + '/' + measurement_unit.name as packaging,"
            sqlcom = sqlcom + " product_item.nama_beli as nama_product,"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + " when product_item.is_packaging = 'P' then measurement_unit.name"
            sqlcom = sqlcom + " when product_item.is_packaging = 'Q' then packaging.name"
            sqlcom = sqlcom + " end as satuan_produk, "

            If Me.lbl_mata_uang.Text = "USD" Then
                sqlcom = sqlcom + " isnull(isnull(retur_sales_order_detil.qty,0) * (isnull(retur_sales_order_detil.harga,0) - "
                sqlcom = sqlcom + " isnull(retur_sales_order_detil.harga, 0) * isnull(sales_order_detail.discount,0) /100),0) as sub_total"
            Else
                sqlcom = sqlcom + " round(isnull(isnull(retur_sales_order_detil.qty,0) * (isnull(retur_sales_order_detil.harga,0) - "
                sqlcom = sqlcom + " isnull(retur_sales_order_detil.harga, 0) * isnull(sales_order_detail.discount,0) /100),0),0) as sub_total"
            End If


            sqlcom = sqlcom + " from retur_sales_order_detil"
            sqlcom = sqlcom + " inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so"
            sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = retur_sales_order.no_sales_order"
            sqlcom = sqlcom + " and sales_order_detail.id_product = retur_sales_order_detil.id_produk"
            sqlcom = sqlcom + " inner join product_item on product_item.id = sales_order_detail.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where retur_sales_order_detil.no_retur_so = " & Me.no_retur
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

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Try
            If Me.tb_id_produk.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama produk terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_qty.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi qty terlebih dahulu"
            Else
                Dim vseq As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vseq from retur_sales_order_detil"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vseq = reader.Item("vseq").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into retur_sales_order_detil(no_retur_so, id_produk, qty, seq, keterangan, harga)"
                sqlcom = sqlcom + " values(" & Me.no_retur & "," & Me.tb_id_produk.Text & "," & Decimal.ToDouble(Me.tb_qty.Text) & "," & vseq
                sqlcom = sqlcom + ",'" & Me.tb_keterangan_item.Text & "','" & Me.tb_harga.Text & "')"
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
                    sqlcom = "delete retur_sales_order_detil"
                    sqlcom = sqlcom + " where no_retur_so = " & Me.no_retur
                    sqlcom = sqlcom + " and id_produk = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah dihapus", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.tradingClass.Alert("Data tersebut masih digunakan di form lain", Me.Page)
            Else
                Me.tradingClass.Alert(ex.Message, Me.Page)
            End If
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update retur_sales_order_detil"
                    sqlcom = sqlcom + " set qty = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) & ","
                    sqlcom = sqlcom + " harga = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_harga"), TextBox).Text) & ","
                    sqlcom = sqlcom + " keterangan = '" & CType(Me.dg_data.Items(x).FindControl("tb_keterangan"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where no_retur_so = " & Me.no_retur
                    sqlcom = sqlcom + " and id_produk = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub link_refresh_invoice_penjualan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_invoice_penjualan.Click
        Me.bindinvoicepenjualan()
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Dim reportPath As String

            reportPath = Server.MapPath("reports\credit_note.rpt")

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
            oRD.SetParameterValue("no_retur", Me.no_retur)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/credit_note.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/credit_note.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        'Daniel
        Try
            Me.GL()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
        'Daniel
    End Sub
End Class