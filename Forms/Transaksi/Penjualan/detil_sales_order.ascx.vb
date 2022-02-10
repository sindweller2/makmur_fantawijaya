Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Penjualan_detil_sales_order
    Inherits System.Web.UI.UserControl

    Public tradingClass As New tradingClass()

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

    Sub clearcustomer()
        Me.tb_id_customer.Text = 0
        Me.lbl_nama_customer.Text = "-----"
        Me.link_popup_customer.Visible = True
    End Sub

    Sub bindcustomer()
        sqlcom = "select name, is_polos, code_sales from daftar_customer where id = " & Me.tb_id_customer.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_customer.Text = reader.Item("name").ToString

            If Not String.IsNullOrEmpty(reader.Item("code_sales").ToString) Then
                Me.dd_sales.SelectedValue = reader.Item("code_sales").ToString
            End If

            If reader.Item("is_polos").ToString = "False" Then
                Me.dd_ppn.SelectedValue = "10"
            Else
                Me.dd_ppn.SelectedValue = "0"
            End If
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindmata_uang()
        'Daniel
        sqlcom = "select id from currency where id = 'USD' or id = 'IDR' order by id"
        'Daniel
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_mata_uang.DataSource = reader
        Me.dd_mata_uang.DataTextField = "id"
        Me.dd_mata_uang.DataValueField = "id"
        Me.dd_mata_uang.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.tb_no_sp.Text = ""
        Me.tb_tgl_sp.Text = ""
        Me.tb_kurs.Text = ""
        Me.dd_jenis_penjualan.SelectedValue = "K"
        Me.no_so = 0
        'Daniel
        Me.DropDownListUangMuka.SelectedIndex = 0
        Me.TextBoxNominal.Text = 0
        Me.TextBoxKeterangan.Text = Nothing
        'Daniel
    End Sub

    Sub loaddata()
        Try
            If Me.vno_so <> 0 Then
                Me.no_so = Me.vno_so
            End If

            sqlcom = "select no, convert(char, tanggal, 103) as tanggal, id_customer, no_surat_pesanan, "
            sqlcom = sqlcom + " convert(char, tgl_surat_pesanan, 103) as tgl_surat_pesanan, id_sales, status, id_currency, ppn, jenis_penjualan, rate,"
            sqlcom = sqlcom + " id_transaction_period, so_no_text, is_submit, is_bonus, po_no"
            'Daniel
            sqlcom = sqlcom + " , uang_muka, uang_muka_nominal, uang_muka_keterangan "
            'Daniel
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " where no = " & Me.no_so
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If reader.Item("is_bonus").ToString = "T" Then
                    Me.lbl_no_penjualan.Text = reader.Item("so_no_text").ToString
                    Me.lbl_no_penjualan.Visible = True
                    Me.tb_no_penjualan.Visible = False
                    Me.dd_is_bonus.SelectedIndex = 0
                Else
                    Me.tb_no_penjualan.Text = reader.Item("so_no_text").ToString
                    Me.lbl_no_penjualan.Visible = False
                    Me.tb_no_penjualan.Visible = True
                    Me.dd_is_bonus.SelectedIndex = 1
                End If

                Me.tb_tgl_penjualan.Text = reader.Item("tanggal").ToString
                Me.dd_sales.SelectedValue = reader.Item("id_sales").ToString
                Me.tb_no_sp.Text = reader.Item("no_surat_pesanan").ToString
                Me.tb_tgl_sp.Text = reader.Item("tgl_surat_pesanan").ToString
                Me.dd_jenis_penjualan.SelectedValue = reader.Item("jenis_penjualan").ToString
                Me.dd_ppn.SelectedValue = Decimal.ToDouble(reader.Item("ppn").ToString)
                Me.tb_id_customer.Text = reader.Item("id_customer").ToString
                Me.dd_mata_uang.SelectedValue = reader.Item("id_currency").ToString
                Me.tb_kurs.Text = FormatNumber(reader.Item("rate").ToString, 2)
                Me.tb_po_no.Text = reader.Item("po_no").ToString
                'Daniel
                If reader.Item("uang_muka").ToString <> Nothing Then
                    Me.DropDownListUangMuka.SelectedValue = reader.Item("uang_muka").ToString
                Else
                    Me.DropDownListUangMuka.SelectedValue = 1
                End If
                Me.TextBoxNominal.Text = reader.Item("uang_muka_nominal").ToString
                Me.TextBoxKeterangan.Text = reader.Item("uang_muka_keterangan").ToString
                'Daniel

                If reader.Item("is_submit").ToString = "B" Then
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                    Me.btn_add.Enabled = True
                    Me.btn_update.Enabled = True
                    Me.btn_delete.Enabled = True
                Else
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                    Me.btn_add.Enabled = False
                    Me.btn_update.Enabled = False
                    Me.btn_delete.Enabled = False
                End If

                Me.bindcustomer()

                Me.tbl_produk.Visible = True

                Me.popupproduk()
            Else
                Me.tbl_produk.Visible = False
                Me.btn_submit.Enabled = False

                If Me.dd_is_bonus.SelectedValue = "Y" Then
                    Me.lbl_no_penjualan.Visible = False
                    Me.tb_no_penjualan.Visible = True
                Else
                    Me.lbl_no_penjualan.Visible = True
                    Me.tb_no_penjualan.Visible = False
                End If

            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bindratepajak()
        Try
            Dim vtgl_invoice As String = Me.tb_tgl_penjualan.Text.Substring(3, 2) & "/" & Me.tb_tgl_penjualan.Text.Substring(0, 2) & "/" & Me.tb_tgl_penjualan.Text.Substring(6, 4)

            sqlcom = "select isnull(rate,0) as rate_pajak, tgl_awal, tgl_akhir"
            sqlcom = sqlcom + " from rate_pajak"
            sqlcom = sqlcom + " where tahun = " & Me.vtahun
            sqlcom = sqlcom + " and tgl_akhir >= '" & vtgl_invoice & "'"
            sqlcom = sqlcom + " order by tgl_awal"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.read()
            if reader.hasrows then
               Me.tb_kurs.Text = FormatNumber(reader.Item("rate_pajak").ToString, 2)
            end if
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.clearcustomer()
            Me.clearproduk()
            Me.bindperiodetransaksi()
            Me.bindsales()
            Me.bindmata_uang()
            dim vday as integer = Now.Day + 1
            Me.tb_tgl_penjualan.Text = vday.ToString.PadLeft(2, "0") & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year
            Me.tb_tgl_sp.Text = Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year
            Me.dd_mata_uang.SelectedValue = "IDR"
            Me.tb_kurs.Text = 1
            Me.tb_discount.Text = 0

            If Me.dd_mata_uang.SelectedValue = "USD" And Me.tb_kurs.Text = 1 Then
                Me.bindratepajak()
            End If

            Me.tb_id_customer.Attributes.Add("style", "display: none;")
            Me.tb_id_produk.Attributes.Add("style", "display: none;")
            Me.link_refresh_customer.Attributes.Add("style", "display: none;")
            Me.link_refresh_produk.Attributes.Add("style", "display: none;")
            Me.link_popup_customer.Attributes.Add("onclick", "popup_customer('" & Me.tb_id_customer.ClientID & "','" & Me.link_refresh_customer.UniqueID & "')")
            Me.popupproduk()
            Me.loaddata()
            Me.loadgrid()
        End If
    End Sub

    Sub popupproduk()
        Me.link_popup_produk.Attributes.Add("onclick", "popup_produk('" & Me.dd_sales.SelectedValue & "','" & Me.tb_id_produk.ClientID & "','" & Me.link_refresh_produk.UniqueID & "')")
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/sales_order.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&voption=" & Me.voption & "&vsearch=" & Me.vsearch & "&vpaging=" &Me.vpaging)
    End Sub

    Protected Sub link_refresh_customer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_customer.Click
        Me.bindcustomer()
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tgl_penjualan.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal penjualan terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_no_sp.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor SP terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_sp.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal SP terlebih dahulu"
            ElseIf Me.tb_id_customer.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama customer terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_kurs.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nilai kurs terlebih dahulu"
            ElseIf Not String.IsNullOrEmpty(Me.tb_kurs.Text) And Decimal.ToDouble(Me.tb_kurs.Text) <= 0 Then
                Me.lbl_msg.Text = "Nilai kurs tidak boleh kurang atau sama dengan nol"
            Else

                If Me.dd_is_bonus.SelectedValue = "Y" Then
                    If String.IsNullOrEmpty(Me.tb_no_penjualan.Text) Then
                        Me.lbl_msg.Text = "Silahkan mengisi nomor penjualan bonus terlebih dahulu"
                        Exit Sub
                    ElseIf Me.tb_no_penjualan.Text.Length < 9 Then
                        Me.lbl_msg.Text = "Nomor penjualan bonus tersebut tidak sesuai format (Format : 9 digit dengan 8 angka dan 1 huruf a)"
                        Exit Sub
                    End If
                End If

                Dim vtgl_so As String = Me.tb_tgl_penjualan.Text.Substring(3, 2) & "/" & Me.tb_tgl_penjualan.Text.Substring(0, 2) & "/" & Me.tb_tgl_penjualan.Text.Substring(6, 4)
                Dim vtgl_sp As String = Me.tb_tgl_sp.Text.Substring(3, 2) & "/" & Me.tb_tgl_sp.Text.Substring(0, 2) & "/" & Me.tb_tgl_sp.Text.Substring(6, 4)

                If Me.no_so = 0 Then

                    Dim vmax As Integer = 0

                    sqlcom = "select isnull(max(no),0) + 1 as vmax from sales_order"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = reader.Item("vmax").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    Dim vno_so_text As String = ""

                    If Me.dd_is_bonus.SelectedValue = "T" Then
                        sqlcom = "select isnull(max(convert(int, so_no_text)),0) + 1 as vso_no_text"
                        sqlcom = sqlcom + " from sales_order"
                        sqlcom = sqlcom + " where convert(int, right(rtrim(convert(char, tanggal,103)),4)) = " & Me.vtahun
                        sqlcom = sqlcom + " and len(so_no_text) = 8"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        reader.Read()
                        If reader.HasRows Then
                            vno_so_text = reader.Item("vso_no_text").ToString.PadLeft(8, "0")
                        End If
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                    Else
                        vno_so_text = Me.tb_no_penjualan.Text
                    End If
                    
                    if me.dd_mata_uang.selectedValue = "USD" and decimal.todouble(me.tb_kurs.text) = 1 then
                       Me.bindratepajak()
                    end if

                    sqlcom = "insert into sales_order(no, tanggal, id_customer, no_surat_pesanan, tgl_surat_pesanan,"
                    sqlcom = sqlcom + " id_sales, status, id_currency, ppn, jenis_penjualan, rate, id_transaction_period,"
                    'Daniel
                    sqlcom = sqlcom + " so_no_text, is_submit, is_submit_invoice, status_invoice, is_invoice, is_bonus, po_no"
                    sqlcom = sqlcom + " , uang_muka, uang_muka_nominal, uang_muka_keterangan)"
                    'Daniel
                    sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl_so & "'," & Me.tb_id_customer.Text & ",'" & Me.tb_no_sp.Text & "','" & vtgl_sp & "'"
                    sqlcom = sqlcom + "," & Me.dd_sales.SelectedValue & ",'O','" & Me.dd_mata_uang.SelectedValue & "'," & Me.dd_ppn.SelectedValue
                    sqlcom = sqlcom + ",'" & Me.dd_jenis_penjualan.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & "," & Me.vid_periode_transaksi & ","
                    'Daniel
                    sqlcom = sqlcom + "'" & vno_so_text & "','B', 'B','B', 'B','" & Me.dd_is_bonus.SelectedValue & "','" & Me.tb_po_no.Text & "','" & Me.DropDownListUangMuka.SelectedValue & "','" & Me.TextBoxNominal.Text.Trim() & "','" & Me.TextBoxKeterangan.Text.Trim() & "')"
                    'Daniel
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.no_so = vmax
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else

                    if me.dd_mata_uang.selectedValue = "USD" and decimal.todouble(me.tb_kurs.text) = 1 then
                       Me.bindratepajak()
                    end if

                    sqlcom = "update sales_order"
                    sqlcom = sqlcom + " set tanggal = '" & vtgl_so & "',"
                    sqlcom = sqlcom + " id_customer = " & Me.tb_id_customer.Text & ","
                    sqlcom = sqlcom + " no_surat_pesanan = '" & Me.tb_no_sp.Text & "',"
                    sqlcom = sqlcom + " tgl_surat_pesanan = '" & vtgl_sp & "',"
                    sqlcom = sqlcom + " id_sales = " & Me.dd_sales.SelectedValue & ","
                    sqlcom = sqlcom + " id_currency = '" & Me.dd_mata_uang.SelectedValue & "',"
                    sqlcom = sqlcom + " ppn = " & Me.dd_ppn.SelectedValue & ","
                    sqlcom = sqlcom + " jenis_penjualan = '" & Me.dd_jenis_penjualan.SelectedValue & "',"
                    sqlcom = sqlcom + " rate = " & Decimal.ToDouble(Me.tb_kurs.Text) & ","
                    sqlcom = sqlcom + " is_bonus = '" & Me.dd_is_bonus.SelectedValue & "',"
                    sqlcom = sqlcom + " po_no = '" & Me.tb_po_no.Text & "'"
                    'Daniel
                    sqlcom = sqlcom + " , uang_muka = '" & Me.DropDownListUangMuka.SelectedValue & "' "
                    sqlcom = sqlcom + " , uang_muka_nominal = '" & Me.TextBoxNominal.Text.Trim() & "' "
                    sqlcom = sqlcom + " , uang_muka_keterangan = '" & Me.TextBoxKeterangan.Text.Trim() & "' "
                    'Daniel
                    sqlcom = sqlcom + " where no = " & Me.no_so
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub clearproduk()
        Me.tb_id_produk.Text = "0"
        Me.tb_nama_produk.Text = ""
        Me.link_popup_produk.Visible = True
    End Sub

    Sub bindproduk()
        Dim vkurs_harian As Decimal = 0
        Dim vtgl As String = Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year

        sqlcom = "select kurs_harian from kurs_harian"
        sqlcom = sqlcom + " where convert(char, tanggal, 103) = '" & vtgl & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            vkurs_harian = reader.Item("kurs_harian").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        sqlcom = "select product_item.nama_beli, measurement_unit.name as nama_satuan, product_item.is_packaging,"
        sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + unit_packaging.name + '/' + measurement_unit.name as packaging,"
        sqlcom = sqlcom + " unit_packaging.name as nama_satuan_packaging, product_price.harga_jual"
        sqlcom = sqlcom + " from product_item"
        sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
        sqlcom = sqlcom + " inner join measurement_unit unit_packaging on unit_packaging.id = product_item.id_measurement_conversion"
        sqlcom = sqlcom + " inner join product_price on product_price.id_product = product_item.id"
        sqlcom = sqlcom + " where product_item.id = " & Me.tb_id_produk.Text

        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.tb_nama_produk.Text = reader.Item("nama_beli").ToString
            If reader.Item("is_packaging").ToString = "P" Then
                Me.lbl_satuan.Text = reader.Item("nama_satuan").ToString
            Else
                Me.lbl_satuan.Text = reader.Item("nama_satuan_packaging").ToString
            End If

            Me.lbl_packaging.Text = reader.Item("packaging").ToString

            If Me.dd_mata_uang.SelectedValue = "IDR" Then
                Me.tb_harga.Text = Decimal.ToDouble(reader.Item("harga_jual").ToString) * Decimal.ToDouble(vkurs_harian)
            Else
                Me.tb_harga.Text = Decimal.ToDouble(reader.Item("harga_jual").ToString)
            End If

        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub dd_sales_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_sales.SelectedIndexChanged
        Me.popupproduk()
    End Sub

    Protected Sub link_refresh_produk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_produk.Click
        Me.bindproduk()
    End Sub

    Sub clearitem()
        Me.tb_nama_produk.Text = ""
        Me.lbl_satuan.Text = ""
        Me.lbl_packaging.Text = ""
        Me.tb_qty.Text = ""
        Me.tb_harga.Text = ""
        Me.tb_discount.Text = 0
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
                       Me.lbl_total_nilai.Text = FormatNumber(Math.Round(decimal.todouble(reader.Item("vtotal_nilai").ToString),0), x)
                    end if
                Else
                    if Me.dd_mata_uang.SelectedValue = "USD" then
                       Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString + reader.Item("vtotal_nilai").ToString * 0.1, x)
                    else
                       Me.lbl_total_nilai.Text = FormatNumber(Math.Round(decimal.todouble(reader.Item("vtotal_nilai").ToString) + (decimal.todouble(reader.Item("vtotal_nilai").ToString) * 0.1),0) ,x)
                    end if
                End If
                'Me.lbl_total_nilai_idr.Text = FormatNumber(Math.Round(decimal.toDouble(Me.lbl_total_nilai.Text) * decimal.todouble(Me.tb_kurs.Text),0),2)
            End If



            'If reader.HasRows Then
                'if Me.dd_ppn.SelectedValue = "0" then
                    'Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString, 0)
                'else
                    'Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString + reader.Item("vtotal_nilai").ToString * 0.1, 0)
                'end if
            'End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try
            Me.clearproduk()        
            Me.clearitem()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select no_sales_order, id_product, nama_product, isnull(qty,0) as qty, isnull(harga_beli,0) as harga_beli,"
            sqlcom = sqlcom + " isnull(harga_jual,0) as harga_jual, isnull(discount,0) as discount, isnull(harga_jadi,0) as harga_jadi, isnull(qty_pending,0) as qty_pending,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + packaging.name + '/' + measurement_unit.name as packaging,"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + " when product_item.is_packaging = 'P' then measurement_unit.name"
            sqlcom = sqlcom + " when product_item.is_packaging = 'Q' then packaging.name"
            sqlcom = sqlcom + " end as satuan_produk, "
            
            if Me.dd_mata_uang.SelectedValue = "USD" then
               sqlcom = sqlcom + " isnull(isnull(sales_order_detail.qty,0) * (isnull(sales_order_detail.harga_jual,0) - "
               sqlcom = sqlcom + " isnull(sales_order_detail.harga_jual, 0) * isnull(sales_order_detail.discount,0) /100),0) as sub_total"
            else
               sqlcom = sqlcom + " round(isnull(isnull(sales_order_detail.qty,0) * (isnull(sales_order_detail.harga_jual,0) - "
               sqlcom = sqlcom + " isnull(sales_order_detail.harga_jual, 0) * isnull(sales_order_detail.discount,0) /100),0),0) as sub_total"
            end if            


            sqlcom = sqlcom + " from sales_order_detail"
            sqlcom = sqlcom + " inner join product_item on product_item.id = sales_order_detail.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where sales_order_detail.no_sales_order = " & Me.no_so
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

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        If Me.dd_mata_uang.SelectedValue = "USD" Then
                            CType(Me.dg_data.Items(x).FindControl("lbl_sub_total_display"), Label).Text = FormatNumber(CType(Me.dg_data.Items(x).FindControl("lbl_sub_total"), Label).Text,3)
                        Elseif Me.dd_mata_uang.SelectedValue = "IDR" then
                            CType(Me.dg_data.Items(x).FindControl("lbl_sub_total_display"), Label).Text = FormatNumber(CType(Me.dg_data.Items(x).FindControl("lbl_sub_total"), Label).Text,2)
                        End If                        

                    Next

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
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Try
            If Me.tb_id_produk.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama produk terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_qty.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi qty terlebih dahulu"
            ElseIf Not String.IsNullOrEmpty(Me.tb_qty.Text) And Decimal.ToDouble(Me.tb_qty.Text) <= 0 Then
                Me.lbl_msg.Text = "Qty tidak boleh lebih kecil atau sama dengan Nol"
            ElseIf String.IsNullOrEmpty(Me.tb_harga.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi harga terlebih dahulu"
            Else
                Dim vharga_beli As Decimal = 0
                sqlcom = "select isnull(hpp,0) as hpp from harga_pokok_pembelian_produk"
                sqlcom = sqlcom + " where id_produk = " & Me.tb_id_produk.Text
                sqlcom = sqlcom + " and seq = (select max(x.seq) from harga_pokok_pembelian_produk x where x.id_produk = " & Me.tb_id_produk.Text & ")"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    If reader.Item("hpp") = 0 Then
                        Me.lbl_msg.Text = "Produk tersebut belum ada harga beli"
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                        Exit Sub
                    Else
                        vharga_beli = reader.Item("hpp").ToString
                    End If
                Else
                    Me.lbl_msg.Text = "Produk tersebut belum ada harga beli"
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    Exit Sub
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vharga_jual As Decimal = 0
                sqlcom = "select isnull(harga_jual,0) as harga_jual"
                sqlcom = sqlcom + " from product_price"
                sqlcom = sqlcom + " where id_product = " & Me.tb_id_produk.Text
                sqlcom = sqlcom + " and id_currency = '" & Me.dd_mata_uang.SelectedValue & "'"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    If reader.Item("harga_jual").ToString = 0 Then
                        Me.lbl_msg.Text = "Produk tersebut belum ada harga jual"
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                        Exit Sub
                    Else
                        vharga_jual = reader.Item("harga_jual").ToString
                    End If
                Else
                    Me.lbl_msg.Text = "Produk tersebut belum ada harga jual"
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    Exit Sub
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vharga_jadi As Decimal = 0
                vharga_jadi = Decimal.ToDouble(Me.tb_harga.Text) - (Decimal.ToDouble(Me.tb_harga.Text) * Decimal.ToDouble(Me.tb_discount.Text) / 100)

                If Me.dd_is_bonus.SelectedValue = "T" And (Decimal.ToDouble(vharga_jual) > Decimal.ToDouble(vharga_jadi)) Then
                    Me.lbl_msg.Text = "Harga jual lebih kecil dari harga produk"
                Else
                    Dim vseq As Integer = 0
                    sqlcom = "select isnull(max(seq),0) + 1 as vseq from sales_order_detail"
                    sqlcom = sqlcom + " where no_sales_order = " & Me.no_so
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vseq = reader.Item("vseq").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into sales_order_detail(no_sales_order, seq, id_product, nama_product,"
                    sqlcom = sqlcom + " qty, harga_beli, harga_jual,"
                    sqlcom = sqlcom + " discount, harga_jadi, qty_pending)"
                    sqlcom = sqlcom + " values(" & Me.no_so & "," & vseq & "," & Me.tb_id_produk.Text & ",'" & Me.tb_nama_produk.Text & "'"
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_qty.Text) & "," & Decimal.ToDouble(vharga_beli) & "," & Decimal.ToDouble(Me.tb_harga.Text)
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_discount.Text) & "," & Decimal.ToDouble(vharga_jadi) & "," & Decimal.ToDouble(Me.tb_qty.Text) & ")"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                    Me.loadgrid()
                End If                
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete sales_order_detail"
                    sqlcom = sqlcom + " where no_sales_order = " & Me.no_so
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah dihapus"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.lbl_msg.Text = "Data masih digunakan di form lain"
            Else
                Me.lbl_msg.Text = ex.Message
            End If
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update sales_order_detail"
                    sqlcom = sqlcom + " set nama_product = '" & CType(Me.dg_data.Items(x).FindControl("tb_name"), TextBox).Text & "',"
                    sqlcom = sqlcom + " qty = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) & ","
                    sqlcom = sqlcom + " qty_pending = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) & ","
                    sqlcom = sqlcom + " harga_jual = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_harga_jual"), TextBox).Text) & ","
                    sqlcom = sqlcom + " harga_jadi = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_harga_jual"), TextBox).Text) & ","
                    sqlcom = sqlcom + " discount = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_discount"), TextBox).Text)
                    sqlcom = sqlcom + " where no_sales_order = " & Me.no_so
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            sqlcom = "select * from sales_order_detail"
            sqlcom = sqlcom + " where no_sales_order = " & Me.no_so
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                sqlcom = "update sales_order"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where no = " & Me.no_so
                connection.koneksi.UpdateRecord(sqlcom)
                Me.loaddata()
                Me.lbl_msg.Text = "Data penjualan tersebut sudah disubmit dan tidak dapat dilakukan perubahan kembali"
            Else
                Me.lbl_msg.Text = "Penjualan tersebut belum ada produknya"
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dd_is_bonus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_is_bonus.SelectedIndexChanged
        Me.loaddata()
    End Sub

    Protected Sub dd_mata_uang_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_mata_uang.SelectedIndexChanged
        Me.bindratepajak()
    End Sub

    Protected Sub btn_kurs_idr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_idr.Click
        Me.tb_kurs.Text = "1.00"
    End Sub

    Protected Sub btn_kurs_usd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_usd.Click
        Me.tb_kurs.Text = tradingClass.KursBulanan("[kurs_bulanan]", Me.vid_periode_transaksi)
    End Sub
End Class

