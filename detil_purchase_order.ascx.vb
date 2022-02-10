Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_detil_purchase_order
    Inherits System.Web.UI.UserControl

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

    Public Property no_po() As Integer
        Get
            Dim o As Object = ViewState("no_po")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("no_po") = value
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

    Sub clearsupplier()
        Me.tb_id_supplier.Text = 0
        Me.lbl_nama_supplier.Text = "-----"
        Me.link_popup_supplier.Visible = True
    End Sub

    Sub bindsupplier()
        sqlcom = "select name, isnull(kredit,0) as kredit"
        sqlcom = sqlcom + " from daftar_supplier"
        sqlcom = sqlcom + " where id = " & Me.tb_id_supplier.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_supplier.Text = reader.Item("name").ToString
            Me.tb_lama_pembayaran.Text = reader.Item("kredit").ToString
            Me.link_popup_kontak_person.Attributes.Add("onclick", "popup_kontak_person_supplier('" & Me.tb_id_supplier.Text & "','" & Me.tb_id_kontak_person.ClientID & "','" & Me.link_refresh_kontak_person.UniqueID & "')")
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearkontakpersonsupplier()
        Me.tb_id_kontak_person.Text = 0
        Me.lbl_nama_kontak_person.Text = "-----"
        Me.link_popup_kontak_person.Visible = True
    End Sub

    Sub bindkontakpersonsupplier()
        sqlcom = "select contact_person from daftar_supplier_contact_person"
        sqlcom = sqlcom + " where id_supplier = " & Me.tb_id_supplier.Text
        sqlcom = sqlcom + " and seq = " & Me.tb_id_kontak_person.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_kontak_person.Text = reader.Item("contact_person").ToString
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

    Sub bindterminpembayaran()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from term_of_payment"
        sqlcom = sqlcom + " where is_lc = '" & Me.dd_is_lc.SelectedValue & "'"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_term_pembayaran.DataSource = reader
        Me.dd_term_pembayaran.DataTextField = "name"
        Me.dd_term_pembayaran.DataValueField = "id"
        Me.dd_term_pembayaran.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindjenispembayaran()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from payment_type"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_payment_type.DataSource = reader
        Me.dd_payment_type.DataTextField = "name"
        Me.dd_payment_type.DataValueField = "id"
        Me.dd_payment_type.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        'Me.tb_no_s.Text = ""
        'Me.tb_tgl_sp.Text = ""
        Me.no_po = 0
    End Sub

    Sub loaddata()
        Try
            If Me.vno_po <> 0 Then
                Me.no_po = Me.vno_po
            End If

            sqlcom = "select no, convert(char, tanggal, 103) as tanggal, id_customer, no_surat_pesanan, "
            sqlcom = sqlcom + " convert(char, tgl_surat_pesanan, 103) as tgl_surat_pesanan, id_sales, status, id_currency, ppn, jenis_penjualan, rate,"
            sqlcom = sqlcom + " id_transaction_period, so_no_text, is_submit"
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " where no = " & Me.no_po
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                'Me.lbl_no_penjualan.Text = reader.Item("so_no_text").ToString
                'Me.lbl_tgl_penjualan.Text = reader.Item("tanggal").ToString
                'Me.dd_sales.SelectedValue = reader.Item("id_sales").ToString
                'Me.tb_no_sp.Text = reader.Item("no_surat_pesanan").ToString
                'Me.tb_tgl_sp.Text = reader.Item("tgl_surat_pesanan").ToString
                'Me.dd_jenis_penjualan.SelectedValue = reader.Item("jenis_penjualan").ToString
                'Me.dd_ppn.SelectedValue = Decimal.ToDouble(reader.Item("ppn").ToString)
                'Me.tb_id_customer.Text = reader.Item("id_customer").ToString
                'Me.dd_mata_uang.SelectedValue = reader.Item("id_currency").ToString
                'Me.tb_kurs.Text = FormatNumber(reader.Item("rate").ToString, 2)

                'If reader.Item("is_submit").ToString = "B" Then
                '    Me.btn_save.Enabled = True
                '    Me.btn_submit.Enabled = True
                '    Me.btn_add.Enabled = True
                '    Me.btn_update.Enabled = True
                '    Me.btn_delete.Enabled = True
                'Else
                '    Me.btn_save.Enabled = False
                '    Me.btn_submit.Enabled = False
                '    Me.btn_add.Enabled = False
                '    Me.btn_update.Enabled = False
                '    Me.btn_delete.Enabled = False
                'End If

                'Me.bindcustomer()
                'Me.tbl_produk.Visible = True
                'Me.popupproduk()
            Else
                Me.tbl_produk.Visible = False
                Me.btn_submit.Enabled = False
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.clearsupplier()
            Me.clearkontakpersonsupplier()
            Me.clearproduk()
            Me.bindperiodetransaksi()
            Me.bindjenispembayaran()
            Me.bindmata_uang()
            Me.dd_mata_uang.SelectedValue = "IDR"
            Me.dd_is_lc.SelectedValue = "T"
            Me.bindterminpembayaran()
            Me.tb_discount.Text = 0
            Me.tb_id_supplier.Attributes.Add("style", "display: none;")
            Me.tb_id_kontak_person.Attributes.Add("style", "display: none;")
            Me.tb_id_produk.Attributes.Add("style", "display: none;")
            Me.link_refresh_supplier.Attributes.Add("style", "display: none;")
            Me.link_refresh_kontak_person.Attributes.Add("style", "display: none;")
            Me.link_refresh_produk.Attributes.Add("style", "display: none;")
            Me.link_popup_supplier.Attributes.Add("onclick", "popup_supplier('" & Me.tb_id_supplier.ClientID & "','" & Me.link_refresh_supplier.UniqueID & "')")
            Me.link_popup_produk.Attributes.Add("onclick", "popup_produk_item(" & Me.tb_id_produk.ClientID & "','" & Me.link_refresh_produk.UniqueID & "')")
            Me.loaddata()
            Me.loadgrid()
        End If
    End Sub


    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/purchase_order_stock.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Protected Sub link_refresh_supplier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_supplier.Click
        Me.bindsupplier()
    End Sub

    'Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
    '    Try
    '        If String.IsNullOrEmpty(Me.tb_no_sp.Text) Then
    '            Me.lbl_msg.Text = "Silahkan mengisi nomor SP terlebih dahulu"
    '        ElseIf String.IsNullOrEmpty(Me.tb_tgl_sp.Text) Then
    '            Me.lbl_msg.Text = "Silahkan mengisi tanggal SP terlebih dahulu"
    '        ElseIf Me.tb_id_customer.Text = 0 Then
    '            Me.lbl_msg.Text = "Silahkan mengisi nama customer terlebih dahulu"
    '        ElseIf String.IsNullOrEmpty(Me.tb_kurs.Text) Then
    '            Me.lbl_msg.Text = "Silahkan mengisi nilai kurs terlebih dahulu"
    '        ElseIf Not String.IsNullOrEmpty(Me.tb_kurs.Text) And Decimal.ToDouble(Me.tb_kurs.Text) <= 0 Then
    '            Me.lbl_msg.Text = "Nilai kurs tidak boleh kurang atau sama dengan nol"
    '        Else

    '            Dim vtgl_sp As String = Me.tb_tgl_sp.Text.Substring(3, 2) & "/" & Me.tb_tgl_sp.Text.Substring(0, 2) & "/" & Me.tb_tgl_sp.Text.Substring(6, 4)

    '            If Me.no_so = 0 Then

    '                Dim vtgl_so As String = Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Year
    '                Dim vmax As Integer = 0

    '                sqlcom = "select isnull(max(no),0) + 1 as vmax from sales_order"
    '                reader = connection.koneksi.SelectRecord(sqlcom)
    '                reader.Read()
    '                If reader.HasRows Then
    '                    vmax = reader.Item("vmax").ToString
    '                End If
    '                reader.Close()
    '                connection.koneksi.CloseKoneksi()

    '                Dim vno_so_text As String = ""
    '                sqlcom = "select isnull(max(convert(int, so_no_text)),0) + 1 as vso_no_text"
    '                sqlcom = sqlcom + " from sales_order"
    '                sqlcom = sqlcom + " where convert(int, right(rtrim(convert(char, tanggal,103)),4)) = " & Now.Year
    '                reader = connection.koneksi.SelectRecord(sqlcom)
    '                reader.Read()
    '                If reader.HasRows Then
    '                    vno_so_text = reader.Item("vso_no_text").ToString.PadLeft(5, "0")
    '                End If
    '                reader.Close()
    '                connection.koneksi.CloseKoneksi()

    '                sqlcom = "insert into sales_order(no, tanggal, id_customer, no_surat_pesanan, tgl_surat_pesanan,"
    '                sqlcom = sqlcom + " id_sales, status, id_currency, ppn, jenis_penjualan, rate, id_transaction_period,"
    '                sqlcom = sqlcom + " so_no_text, is_submit, is_submit_invoice, status_invoice, is_invoice)"
    '                sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl_so & "'," & Me.tb_id_customer.Text & ",'" & Me.tb_no_sp.Text & "','" & vtgl_sp & "'"
    '                sqlcom = sqlcom + "," & Me.dd_sales.SelectedValue & ",'O','" & Me.dd_mata_uang.SelectedValue & "'," & Me.dd_ppn.SelectedValue
    '                sqlcom = sqlcom + ",'" & Me.dd_jenis_penjualan.SelectedValue & "'," & Me.tb_kurs.Text & "," & Me.vid_periode_transaksi & ","
    '                sqlcom = sqlcom + "'" & vno_so_text.PadLeft(8, "0") & "','B', 'B','B', 'B')"
    '                connection.koneksi.InsertRecord(sqlcom)
    '                Me.no_so = vmax
    '                Me.lbl_msg.Text = "Data sudah disimpan"
    '            Else
    '                sqlcom = "update sales_order"
    '                sqlcom = sqlcom + " set id_customer = " & Me.tb_id_customer.Text & ","
    '                sqlcom = sqlcom + " no_surat_pesanan = '" & Me.tb_no_sp.Text & "',"
    '                sqlcom = sqlcom + " tgl_surat_pesanan = '" & vtgl_sp & "',"
    '                sqlcom = sqlcom + " id_sales = " & Me.dd_sales.SelectedValue & ","
    '                sqlcom = sqlcom + " id_currency = '" & Me.dd_mata_uang.SelectedValue & "',"
    '                sqlcom = sqlcom + " ppn = " & Me.dd_ppn.SelectedValue & ","
    '                sqlcom = sqlcom + " jenis_penjualan = '" & Me.dd_jenis_penjualan.SelectedValue & "',"
    '                sqlcom = sqlcom + " rate = " & Me.tb_kurs.Text
    '                sqlcom = sqlcom + " where no = " & Me.no_so
    '                connection.koneksi.UpdateRecord(sqlcom)
    '                Me.lbl_msg.Text = "Data sudah diupdate"
    '            End If
    '            Me.loaddata()
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

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
            sqlcom = "select isnull(sum(isnull(qty,0) * (isnull(harga_jadi,0) + (isnull(harga_jadi,0) * isnull(discount,0)/100))),0) as vtotal_nilai"
            sqlcom = sqlcom + " from sales_order_detail"
            sqlcom = sqlcom + " where no_sales_order = " & Me.no_po
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If Me.dd_ppn.SelectedValue = "0" Then
                    Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString, 3)
                Else
                    Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString + reader.Item("vtotal_nilai").ToString * 0.1, 3)
                End If
            End If
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
            sqlcom = sqlcom + " isnull(isnull(sales_order_detail.qty,0) * (isnull(sales_order_detail.harga_jual,0) - "
            sqlcom = sqlcom + " isnull(sales_order_detail.harga_jual, 0) * isnull(sales_order_detail.discount,0) /100),0) as sub_total"
            sqlcom = sqlcom + " from sales_order_detail"
            sqlcom = sqlcom + " inner join product_item on product_item.id = sales_order_detail.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where sales_order_detail.no_sales_order = " & Me.no_po
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
                sqlcom = "select * from sales_order_detail"
                sqlcom = sqlcom + " where no_sales_order = " & Me.no_po
                sqlcom = sqlcom + " and id_product = " & Me.tb_id_produk.Text
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.lbl_msg.Text = "Produk tersebut sudah ada"
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    Exit Sub
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vharga_beli As Decimal = 0
                sqlcom = "select isnull(harga_beli,0) as harga_beli, isnull(harga_jual,0) as harga_jual"
                sqlcom = sqlcom + " from product_price"
                sqlcom = sqlcom + " where id_product = " & Me.tb_id_produk.Text
                'sqlcom = sqlcom + " and id_currency = '" & Me.dd_mata_uang.SelectedValue & "'"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vharga_beli = reader.Item("harga_beli").ToString
                    If reader.Item("harga_beli").ToString = 0 Then
                        Me.lbl_msg.Text = "Produk tersebut belum ada harga beli"
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                        Exit Sub
                    ElseIf reader.Item("harga_jual").ToString = 0 Then
                        Me.lbl_msg.Text = "Produk tersebut belum ada harga jual"
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                        Exit Sub
                    End If
                Else
                    Me.lbl_msg.Text = "Produk tersebut belum ada harga"
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    Exit Sub
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vharga_jadi As Decimal = 0
                vharga_jadi = Decimal.ToDouble(Me.tb_harga.Text) - (Decimal.ToDouble(Me.tb_harga.Text) * Decimal.ToDouble(Me.tb_discount.Text) / 100)

                Dim vseq As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vseq from sales_order_detail"
                sqlcom = sqlcom + " where no_sales_order = " & Me.no_po
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
                sqlcom = sqlcom + " values(" & Me.no_po & "," & vseq & "," & Me.tb_id_produk.Text & ",'" & Me.tb_nama_produk.Text & "'"
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_qty.Text) & "," & Decimal.ToDouble(vharga_beli) & "," & Decimal.ToDouble(Me.tb_harga.Text)
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_discount.Text) & "," & Decimal.ToDouble(vharga_jadi) & "," & Decimal.ToDouble(Me.tb_qty.Text) & ")"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loadgrid()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete sales_order_detail"
                    sqlcom = sqlcom + " where no_sales_order = " & Me.no_po
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
                    sqlcom = sqlcom + " where no_sales_order = " & Me.no_po
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
            sqlcom = sqlcom + " where no_sales_order = " & Me.no_po
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                sqlcom = "update sales_order"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where no = " & Me.no_po
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

    Protected Sub link_refresh_kontak_person_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_kontak_person.Click
        Me.bindkontakpersonsupplier()
    End Sub

    Protected Sub dd_is_lc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_is_lc.SelectedIndexChanged
        Me.bindterminpembayaran()
    End Sub
End Class

