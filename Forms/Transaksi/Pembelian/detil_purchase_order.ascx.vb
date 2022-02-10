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

    
    Private ReadOnly Property vpaging() As Integer
        Get
            Dim o As Object = Request.QueryString("vpaging")
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

    Sub binddibuatoleh()
        Dim readerdibuat As SqlClient.SqlDataReader
        sqlcom = "select code, nama_pegawai from user_list where code <> 1 order by nama_pegawai"
        readerdibuat = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_dibuat_oleh.Datasource = readerdibuat
        Me.dd_dibuat_oleh.DataTextField = "nama_pegawai"
        Me.dd_dibuat_oleh.DataValueField = "code"
        Me.dd_dibuat_oleh.DataBind()
        readerdibuat.Close()
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
            'Me.tb_lama_pembayaran.Text = reader.Item("kredit").ToString
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
        Dim readerterminpembayaran As SqlClient.SqlDataReader
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from term_of_payment"
        sqlcom = sqlcom + " where is_lc = '" & Me.dd_is_lc.SelectedValue & "'"
        sqlcom = sqlcom + " order by name"
        readerterminpembayaran = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_term_pembayaran.DataSource = readerterminpembayaran
        Me.dd_term_pembayaran.DataTextField = "name"
        Me.dd_term_pembayaran.DataValueField = "id"
        Me.dd_term_pembayaran.DataBind()
        readerterminpembayaran.Close()
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

    Sub bindpelabuhan_tujuan()
        sqlcom = "select id, name from port_of_destination order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_port_of_destination.DataSource = reader
        Me.dd_port_of_destination.DataTextField = "name"
        Me.dd_port_of_destination.DataValueField = "id"
        Me.dd_port_of_destination.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.lbl_no_pembelian.Text = ""
        Me.lbl_tgl_pembelian.Text = ""
        Me.tb_lama_pembayaran.Text = ""
        Me.tb_estimasi_tgl_terima.Text = ""
        Me.no_po = 0
    End Sub

    Sub remarks()
        Me.tb_remarks.Text = "- DO NOT USE FORWARDING AGENCY" & vbCrLf
        Me.tb_remarks.Text = Me.tb_remarks.Text + "- PLEASE USE THE SHIPPING LINE AGENCY" & vbCrLf
        Me.tb_remarks.Text = Me.tb_remarks.Text + "- KINDLY REQUEST 14 DAYS OF DEMURRAGE" & vbCrLf
        Me.tb_remarks.Text = Me.tb_remarks.Text + "- PLEASE ATTACH COA IN YOUR DOCUMENTS"
    End Sub

    Sub loaddata()
        Try
            If Me.vno_po <> 0 Then
                Me.no_po = Me.vno_po
            End If

            

            sqlcom = "select no, convert(char, tanggal, 103) as tanggal, jenis_pembelian, id_supplier, id_contact_person,"
            sqlcom = sqlcom + " is_lc, id_term_of_payment, id_payment_type, payment_period, id_currency, ppn,"
            sqlcom = sqlcom + " convert(char, received_date_request,103) as received_date_request,"
            sqlcom = sqlcom + " po_no_text, is_submit, id_transaction_period, dibuat_oleh,"
            sqlcom = sqlcom + " id_negara_koresponden, id_dikapalkan_dari, id_pelabuhan_tujuan, id_negara_asal, termin_pengapalan, remarks, pi_no"
            sqlcom = sqlcom + " from purchase_order"
            sqlcom = sqlcom + " where no = " & Me.no_po
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_pembelian.Text = reader.Item("po_no_text").ToString
                Me.lbl_tgl_pembelian.Text = reader.Item("tanggal").ToString
                Me.dd_jenis_pembelian.SelectedValue = reader.Item("jenis_pembelian").ToString
                Me.tb_id_supplier.Text = reader.Item("id_supplier").ToString
                Me.tb_id_kontak_person.Text = reader.Item("id_contact_person").ToString
                Me.dd_is_lc.SelectedValue = reader.Item("is_lc").ToString
                Me.bindterminpembayaran()
                Me.dd_term_pembayaran.SelectedValue = reader.Item("id_term_of_payment").ToString
                Me.dd_payment_type.SelectedValue = reader.Item("id_payment_type").ToString
                Me.tb_lama_pembayaran.Text = Decimal.ToInt32(reader.Item("payment_period").ToString)
                Me.dd_mata_uang.SelectedValue = reader.Item("id_currency").ToString
                Me.tb_pi_no.Text = reader.Item("pi_no").ToString
                Me.dd_ppn.SelectedValue = Decimal.ToDouble(reader.Item("ppn").ToString)
                Me.tb_estimasi_tgl_terima.Text = reader.Item("received_date_request").ToString
                Me.tb_term_of_shipment.Text = reader.Item("termin_pengapalan").ToString
                Me.dd_dibuat_oleh.SelectedValue = reader.Item("dibuat_oleh").ToString
                Me.dd_port_of_destination.SelectedValue = reader.Item("id_pelabuhan_tujuan").ToString

                If String.IsNullOrEmpty(reader.Item("remarks").ToString) Then
                    Me.remarks()
                Else
                    Me.tb_remarks.Text = reader.Item("remarks").ToString
                End If

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

                Me.bindsupplier()
                Me.bindkontakpersonsupplier()

                Me.tbl_produk.Visible = True
            Else
                Me.tbl_produk.Visible = False
                Me.btn_submit.Enabled = False
                Me.tb_remarks.Text = "- DO NOT USE FORWARDING AGENCY" & vbCrLf
                Me.tb_remarks.Text = Me.tb_remarks.Text + "- PLEASE USE THE SHIPPING LINE AGENCY" & vbCrLf
                Me.tb_remarks.Text = Me.tb_remarks.Text + "- KINDLY REQUEST 14 DAYS OF DEMURRAGE" & vbCrLf
                Me.tb_remarks.Text = Me.tb_remarks.Text + "- PLEASE ATTACH COA IN YOUR DOCUMENTS"
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
            Me.bindpelabuhan_tujuan()
            Me.binddibuatoleh()
            Me.dd_mata_uang.SelectedValue = "IDR"
            Me.dd_is_lc.SelectedValue = "T"
            Me.bindterminpembayaran()
            Me.tb_discount.Text = 0
            Me.dd_ppn.SelectedValue = "0"            
            Me.tb_id_supplier.Attributes.Add("style", "display: none;")
            Me.tb_id_kontak_person.Attributes.Add("style", "display: none;")
            Me.tb_id_produk.Attributes.Add("style", "display: none;")
            Me.link_refresh_supplier.Attributes.Add("style", "display: none;")
            Me.link_refresh_kontak_person.Attributes.Add("style", "display: none;")
            Me.link_refresh_produk.Attributes.Add("style", "display: none;")
            Me.link_popup_supplier.Attributes.Add("onclick", "popup_supplier('" & Me.tb_id_supplier.ClientID & "','" & Me.link_refresh_supplier.UniqueID & "')")
            Me.link_popup_produk.Attributes.Add("onclick", "popup_produk_item('" & Me.tb_id_produk.ClientID & "','" & Me.link_refresh_produk.UniqueID & "')")
            Me.loaddata()
            Me.loadgrid()
        End If
    End Sub


    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/purchase_order_stock.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&vpaging=" & me.vpaging)
    End Sub

    Protected Sub link_refresh_supplier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_supplier.Click
        Me.bindsupplier()
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_supplier.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama supplier terlebih dahulu"
            ElseIf Me.tb_id_kontak_person.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama kontak person terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_lama_pembayaran.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi lama pembayaran terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_term_of_shipment.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi termijn pengapalan terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_remarks.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi remarks terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_estimasi_tgl_terima.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi estimasi tanggal terima terlebih dahulu"
            Else
                Dim vtgl_estimasi As String = Me.tb_estimasi_tgl_terima.Text.Substring(3, 2) & "/" & Me.tb_estimasi_tgl_terima.Text.Substring(0, 2) & "/" & Me.tb_estimasi_tgl_terima.Text.Substring(6, 4)
                Me.dd_ppn.SelectedValue = "0"
                If Me.no_po = 0 Then

                    Dim vtgl_po As String = Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Year
                    Dim vmax As Integer = 0

                    sqlcom = "select isnull(max(no),0) + 1 as vmax from purchase_order"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = reader.Item("vmax").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    Dim vno_po_text As String = ""
                    sqlcom = "select isnull(max(convert(int, right(po_no_text,3))),0) + 1 as vpo_no_text"
                    sqlcom = sqlcom + " from purchase_order"
                    sqlcom = sqlcom + " where substring(po_no_text,5,4) = " & me.vtahun
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        'MFW-201111-100
                        vno_po_text = "MFW-" & Year(Now.Date) & Month(Now.Date).ToString.PadLeft(2, "0") & "-" & reader.Item("vpo_no_text").ToString.PadLeft(3, "0")
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()


                    sqlcom = "insert into purchase_order(no, tanggal, jenis_pembelian, id_supplier, id_contact_person,"
                    sqlcom = sqlcom + " is_lc, id_term_of_payment, id_payment_type, payment_period, id_currency, ppn, received_date_request,"
                    sqlcom = sqlcom + " po_no_text, is_submit, id_transaction_period, id_pelabuhan_tujuan, termin_pengapalan, remarks,"
                    sqlcom = sqlcom + " pi_no, is_lc_lunas, dibuat_oleh)"
                    sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl_po & "','" & Me.dd_jenis_pembelian.SelectedValue & "'," & Me.tb_id_supplier.Text
                    sqlcom = sqlcom + "," & Me.tb_id_kontak_person.Text & ",'" & Me.dd_is_lc.SelectedValue & "'," & Me.dd_term_pembayaran.SelectedValue
                    sqlcom = sqlcom + "," & Me.dd_payment_type.SelectedValue & "," & Decimal.ToDouble(Me.tb_lama_pembayaran.Text) & ","
                    sqlcom = sqlcom + "'" & Me.dd_mata_uang.SelectedValue & "'," & Me.dd_ppn.SelectedValue
                    sqlcom = sqlcom + ",'" & vtgl_estimasi & "','" & vno_po_text & "','B'," & Me.vid_periode_transaksi & "," & Me.dd_port_of_destination.SelectedValue & ","
                    sqlcom = sqlcom + "'" & Me.tb_term_of_shipment.Text & "','" & Me.tb_remarks.Text & "','" & Me.tb_pi_no.Text & "','B'," & Me.dd_dibuat_oleh.SelectedValue & ")"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.no_po = vmax
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update purchase_order"
                    sqlcom = sqlcom + " set jenis_pembelian = '" & Me.dd_jenis_pembelian.SelectedValue & "',"
                    sqlcom = sqlcom + " id_supplier = " & Me.tb_id_supplier.Text & ","
                    sqlcom = sqlcom + " id_contact_person = " & Me.tb_id_kontak_person.Text & ","
                    sqlcom = sqlcom + " is_lc = '" & Me.dd_is_lc.SelectedValue & "',"
                    sqlcom = sqlcom + " id_term_of_payment = " & Me.dd_term_pembayaran.SelectedValue & ","
                    sqlcom = sqlcom + " id_payment_type = " & Me.dd_payment_type.SelectedValue & ","
                    sqlcom = sqlcom + " payment_period = " & Decimal.ToDouble(Me.tb_lama_pembayaran.Text) & ","
                    sqlcom = sqlcom + " id_currency = '" & Me.dd_mata_uang.SelectedValue & "',"
                    sqlcom = sqlcom + " ppn = " & Me.dd_ppn.SelectedValue & ","
                    sqlcom = sqlcom + " received_date_request = '" & vtgl_estimasi & "',"
                    sqlcom = sqlcom + " id_pelabuhan_tujuan = " & Me.dd_port_of_destination.SelectedValue & ","
                    sqlcom = sqlcom + " termin_pengapalan = '" & Me.tb_term_of_shipment.Text & "',"
                    sqlcom = sqlcom + " remarks = '" & Me.tb_remarks.Text & "',"
                    sqlcom = sqlcom + " pi_no = '" & Me.tb_pi_no.Text & "',"
                    sqlcom = sqlcom + " dibuat_oleh = " & Me.dd_dibuat_oleh.SelectedValue
                    sqlcom = sqlcom + " where no = " & Me.no_po
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
        sqlcom = sqlcom + " unit_packaging.name as nama_satuan_packaging"
        'sqlcom = sqlcom + " isnull((select isnull(harga_jual,0) from product_price where id_product = product_item.id),0) as harga_jual"
        sqlcom = sqlcom + " from product_item"
        sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
        sqlcom = sqlcom + " inner join measurement_unit unit_packaging on unit_packaging.id = product_item.id_measurement_conversion"
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

            'If Me.dd_mata_uang.SelectedValue = "IDR" Then
                'Me.tb_harga.Text = Decimal.ToDouble(reader.Item("harga_jual").ToString) * Decimal.ToDouble(vkurs_harian)
            'Else
                'Me.tb_harga.Text = Decimal.ToDouble(reader.Item("harga_jual").ToString)
            'End If

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
            sqlcom = "select isnull(sum(isnull(purchase_order_detil.qty,0) * (isnull(purchase_order_detil.unit_price,0) - "
            sqlcom = sqlcom + " isnull(purchase_order_detil.unit_price, 0) * isnull(purchase_order_detil.discount,0) /100)),0) as vtotal_nilai"
            sqlcom = sqlcom + " from purchase_order_detil"
            sqlcom = sqlcom + " where po_no = " & Me.no_po
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

            sqlcom = "select po_no, id_product, nama_product, isnull(qty,0) as qty, isnull(unit_price,0) as unit_price,"
            sqlcom = sqlcom + " isnull(discount,0) as discount, isnull(qty_pending,0) as qty_pending,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + packaging.name + '/' + measurement_unit.name as packaging,"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + " when product_item.is_packaging = 'P' then measurement_unit.name"
            sqlcom = sqlcom + " when product_item.is_packaging = 'Q' then packaging.name"
            sqlcom = sqlcom + " end as satuan_produk, "
            sqlcom = sqlcom + " isnull(isnull(purchase_order_detil.qty,0) * (isnull(purchase_order_detil.unit_price,0) - "
            sqlcom = sqlcom + " isnull(purchase_order_detil.unit_price, 0) * isnull(purchase_order_detil.discount,0) /100),0) as sub_total"
            sqlcom = sqlcom + " from purchase_order_detil"
            sqlcom = sqlcom + " inner join product_item on product_item.id = purchase_order_detil.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where purchase_order_detil.po_no = " & Me.no_po
            sqlcom = sqlcom + " order by purchase_order_detil.seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "purchase_order_detil")
                Me.dg_data.DataSource = ds.Tables("purchase_order_detil").DefaultView

                If ds.Tables("purchase_order_detil").Rows.Count > 0 Then
                    If ds.Tables("purchase_order_detil").Rows.Count > 10 Then
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
                sqlcom = "select * from purchase_order_detil"
                sqlcom = sqlcom + " where po_no = " & Me.no_po
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

                Dim vseq As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vseq from purchase_order_detil"
                sqlcom = sqlcom + " where po_no = " & Me.no_po
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vseq = reader.Item("vseq").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vharga_jadi As Decimal = 0
                vharga_jadi = Decimal.ToDouble(Me.tb_harga.Text) - (Decimal.ToDouble(Me.tb_harga.Text) * Decimal.ToDouble(Me.tb_discount.Text) / 100)

                sqlcom = "insert into purchase_order_detil(po_no, id_product, nama_product,"
                sqlcom = sqlcom + " qty, unit_price, discount, qty_pending, seq)"
                sqlcom = sqlcom + " values(" & Me.no_po & "," & Me.tb_id_produk.Text & ",'" & Me.tb_nama_produk.Text & "'"
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_qty.Text) & "," & Decimal.ToDouble(Me.tb_harga.Text)
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_discount.Text) & "," & Decimal.ToDouble(Me.tb_qty.Text) & "," & vseq & ")"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.lbl_msg.text = ex.message
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete purchase_order_detil"
                    sqlcom = sqlcom + " where po_no = " & Me.no_po
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
                    sqlcom = "update purchase_order_detil"
                    sqlcom = sqlcom + " set nama_product = '" & CType(Me.dg_data.Items(x).FindControl("tb_name"), TextBox).Text & "',"
                    sqlcom = sqlcom + " qty = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) & ","
                    sqlcom = sqlcom + " qty_pending = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) & ","
                    sqlcom = sqlcom + " unit_price = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_harga_jual"), TextBox).Text) & ","
                    sqlcom = sqlcom + " discount = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_discount"), TextBox).Text)
                    sqlcom = sqlcom + " where po_no = " & Me.no_po
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
            sqlcom = "select * from purchase_order_detil"
            sqlcom = sqlcom + " where po_no = " & Me.no_po
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                sqlcom = "update purchase_order"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where no = " & Me.no_po
                connection.koneksi.UpdateRecord(sqlcom)
                Me.loaddata()
                Me.lbl_msg.Text = "Data pembelian tersebut sudah disubmit dan tidak dapat dilakukan perubahan kembali"
            Else
                Me.lbl_msg.Text = "Pembelian tersebut belum ada produknya"
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

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Dim reportPath As String = Server.MapPath("reports\purchase_order.rpt")
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
            oRD.SetParameterValue("po_no", Me.no_po)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/purchase_order.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/purchase_order.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class

