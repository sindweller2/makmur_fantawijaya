Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Akuntansi_barang_dalam_perjalanan
    Inherits System.Web.UI.UserControl

    Public kurs_bulanan As Decimal

    Public tradingClass As New tradingClass()

    Sub GL(ByVal EDI As String, ByVal vnilai As Decimal, ByVal vtgl As String)
        Dim id As Integer = Me.tradingClass.IDTransaksiMax
        Dim keterangan As String = "Barang Dalam Perjalanan no. " & EDI
        Dim vperiode As Long = Me.dd_bulan.SelectedValue
        Dim vcurrency As String = "IDR"
        Dim d_kurs As Decimal = 1

        Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(vtgl), id, Me.tradingClass.JurnalType("2"), keterangan, vperiode)

        tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtgl), "11.05", "11.04", vnilai, 0, keterangan, vperiode, vcurrency, d_kurs, IIf(vcurrency = "IDR", 0, vnilai), 0, String.Empty)
        tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtgl), "11.04", "11.05", 0, vnilai, keterangan, vperiode, vcurrency, d_kurs, IIf(vcurrency = "IDR", 0, vnilai), 0, String.Empty)

        tradingClass.Alert("Data sudah disubmit!", Me.Page)
    End Sub

    Private Property vid_currency() As String
        Get
            Dim o As Object = ViewState("vid_currency")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vid_currency") = value
        End Set
    End Property

    Private Property vunit_price() As Decimal
        Get
            Dim o As Object = ViewState("vunit_price")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vunit_price") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode_transaksi()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
        sqlcom = sqlcom + " order by bulan"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_bulan.DataSource = reader
        Me.dd_bulan.DataTextField = "name"
        Me.dd_bulan.DataValueField = "id"
        Me.dd_bulan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub cleardokumen_impor()
        Me.tb_no_dokumen_impor.Text = 0
        Me.lbl_dokumen_impor.Text = "------"
        Me.tb_tgl_masuk_gudang.Text = Nothing
        Me.link_popup_dokumen_impor.Visible = True
    End Sub

    Sub binddokumen_impor()
        Me.lbl_dokumen_impor.Text = Me.tb_no_dokumen_impor.Text
        'Me.popup_produk()
    End Sub

    'Sub clearproduk()
    '    Me.tb_id_produk.Text = 0
    '    Me.lbl_nama_produk.Text = "------"
    '    Me.link_popup_produk.Visible = True
    '    Me.lbl_qty.Text = ""
    '    Me.lbl_harga_per_unit.Text = ""
    '    Me.lbl_discount.Text = ""
    'End Sub

    Sub bindproduk()
        Try
            sqlcom = "select nama_product, isnull(qty,0) as qty, isnull(unit_price,0) as unit_price, isnull(discount,0) as discount, purchase_order.id_currency"
            sqlcom = sqlcom + " from entry_dokumen_impor_produk"
            sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq = entry_dokumen_impor_produk.seq_entry"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
            sqlcom = sqlcom + " where seq_entry = " & Me.tb_no_dokumen_impor.Text
            'sqlcom = sqlcom + " and id_product = " & Me.tb_id_produk.Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vid_currency = reader.Item("id_currency").ToString
                Me.vunit_price = reader.Item("unit_price").ToString
                'Me.lbl_nama_produk.Text = reader.Item("nama_product").ToString
                'Me.lbl_qty.Text = reader.Item("qty").ToString
                'Me.lbl_harga_per_unit.Text = reader.Item("id_currency").ToString & " " & reader.Item("unit_price").ToString
                'Me.lbl_discount.Text = reader.Item("discount").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try

            Me.cleardokumen_impor()
            'Me.clearproduk()
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select barang_dalam_perjalanan.id_transaction_period, barang_dalam_perjalanan.seq_entry as no_dokumen_impor ,barang_dalam_perjalanan.is_submit ,convert(char, barang_dalam_perjalanan.tgl_masuk_gudang, 103) as tgl_masuk_gudang ,entry_dokumen_impor.* "
            sqlcom = sqlcom + " from barang_dalam_perjalanan, entry_dokumen_impor "
            sqlcom = sqlcom + " where barang_dalam_perjalanan.seq_entry = entry_dokumen_impor.seq and barang_dalam_perjalanan.id_transaction_period = " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " order by 2 "

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "barang_dalam_perjalanan")
                Me.dg_data.DataSource = ds.Tables("barang_dalam_perjalanan").DefaultView

                If ds.Tables("barang_dalam_perjalanan").Rows.Count > 0 Then
                    If ds.Tables("barang_dalam_perjalanan").Rows.Count > 10 Then
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
                    Me.btn_submit.Visible = True
                    Me.btn_delete.Visible = True

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        If CType(Me.dg_data.Items(x).FindControl("lbl_is_submit"), Label).Text = "B" Or String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_is_submit"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                        End If
                    Next

                Else
                    Me.dg_data.Visible = False
                    Me.btn_submit.Visible = False
                    Me.btn_delete.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.cleardokumen_impor()
            Me.tb_tahun.Text = Now.Year
            Me.bindperiode_transaksi()
            Me.loadgrid()
            Me.tb_no_dokumen_impor.Attributes.Add("style", "display: none;")
            Me.link_refresh_dokumen_impor.Attributes.Add("style", "display: none;")
            Me.link_popup_dokumen_impor.Attributes.Add("onclick", "popup_dokumen_impor_bdp('" & Me.dd_bulan.SelectedValue & "','" & Me.tb_no_dokumen_impor.ClientID & "','" & Me.link_refresh_dokumen_impor.UniqueID & "')")
            'Me.link_popup_produk.Attributes.Clear()
            'Me.link_popup_produk.Enabled = False
            'Me.clearproduk()
            'Me.link_refresh_produk.Attributes.Add("style", "display: none;")
            'Me.tb_id_produk.Attributes.Add("style", "display: none;")
        Else
            Me.link_popup_dokumen_impor.Attributes.Add("onclick", "popup_dokumen_impor_bdp('" & Me.dd_bulan.SelectedValue & "','" & Me.tb_no_dokumen_impor.ClientID & "','" & Me.link_refresh_dokumen_impor.UniqueID & "')")
        End If
    End Sub

    'Sub popup_produk()
    '    Me.link_popup_produk.Enabled = True
    '    Me.link_popup_produk.Attributes.Add("onclick", "popup_dokumen_impor_bdp_produk('" & Me.tb_no_dokumen_impor.Text & "','" & Me.tb_id_produk.ClientID & "','" & Me.link_refresh_produk.UniqueID & "')")
    'End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
        Me.dd_bulan.SelectedIndex = 0
        Me.link_popup_dokumen_impor.Attributes.Add("onclick", "popup_dokumen_impor_bdp('" & Me.dd_bulan.SelectedValue & "','" & Me.tb_no_dokumen_impor.ClientID & "','" & Me.link_refresh_dokumen_impor.UniqueID & "')")
    End Sub

    Protected Sub link_refresh_dokumen_impor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_dokumen_impor.Click
        Me.binddokumen_impor()
        'Me.popup_produk()
    End Sub

    'Protected Sub link_refresh_produk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_produk.Click
    '    Me.bindproduk()
    'End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_no_dokumen_impor.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor dokumen impor terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_masuk_gudang.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tgl. masuk gudang terlebih dahulu"
                'ElseIf Me.tb_id_produk.Text = 0 Then
                '    Me.lbl_msg.Text = "Silahkan mengisi nama produk terlebih dahulu"
            Else
                'sqlcom = "insert into barang_dalam_perjalanan(id_transaction_period, seq_entry, id_product, qty, unit_price, nama_product, discount, id_currency)"
                'sqlcom = sqlcom + " values(" & Me.dd_bulan.SelectedValue & "," & Me.tb_no_dokumen_impor.Text & "," & Me.tb_id_produk.Text
                'sqlcom = sqlcom + "," & Decimal.ToDouble(Me.lbl_qty.Text) & "," & Decimal.ToDouble(Me.vunit_price) & ","
                'sqlcom = sqlcom + "'" & Me.lbl_nama_produk.Text & "'," & Decimal.ToDouble(Me.lbl_discount.Text) & ",'" & Me.vid_currency & "')"
                'connection.koneksi.InsertRecord(sqlcom)

                Dim vtgl_terima_gudang As String = Me.tb_tgl_masuk_gudang.Text.Substring(3, 2) & "/" & Me.tb_tgl_masuk_gudang.Text.Substring(0, 2) & "/" & Me.tb_tgl_masuk_gudang.Text.Substring(6, 4)

                sqlcom = "select *"
                sqlcom = sqlcom + " from barang_dalam_perjalanan"
                sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
                sqlcom = sqlcom + " and seq_entry = " & Me.tb_no_dokumen_impor.Text
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If Not reader.HasRows Then

                    sqlcom = "select isnull(kurs_bulanan,0) as kurs_bulanan"
                    sqlcom = sqlcom + " from transaction_period"
                    sqlcom = sqlcom + " where id = " & Me.dd_bulan.SelectedValue
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        Me.kurs_bulanan = reader.Item("kurs_bulanan").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into barang_dalam_perjalanan(id_transaction_period, seq_entry, is_submit, tgl_masuk_gudang)"
                    sqlcom = sqlcom + " values(" & Me.dd_bulan.SelectedValue & "," & Me.tb_no_dokumen_impor.Text & ", 'B','" & vtgl_terima_gudang & "')"
                    connection.koneksi.InsertRecord(sqlcom)

                    sqlcom = "insert into barang_dalam_perjalanan_detail select seq_entry,id_product,qty,unit_price,nama_product,seq,0 from entry_dokumen_impor_produk where seq_entry = " & Me.tb_no_dokumen_impor.Text
                    connection.koneksi.InsertRecord(sqlcom)

                    sqlcom = "update barang_dalam_perjalanan_detail set total_price = qty * unit_price * " & Me.kurs_bulanan & " where seq_entry = " & Me.tb_no_dokumen_impor.Text
                    connection.koneksi.UpdateRecord(sqlcom)

                    tradingClass.Alert("Data sudah disimpan", Me.Page)
                    Me.loadgrid()
                Else
                    tradingClass.Alert("Data sudah ada", Me.Page)
                End If
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete barang_dalam_perjalanan"
                    sqlcom = sqlcom + " where seq_entry = " & CType(Me.dg_data.Items(x).FindControl("lbl_no_dokumen_impor"), Label).Text
                    'sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)

                    sqlcom = "delete barang_dalam_perjalanan_detail"
                    sqlcom = sqlcom + " where seq_entry = " & CType(Me.dg_data.Items(x).FindControl("lbl_no_dokumen_impor"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)

                    tradingClass.Alert("Data sudah dihapus", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                tradingClass.Alert("Data masih digunakan di form lain", Me.Page)
            Else
                tradingClass.Alert(ex.Message, Me.Page)
            End If
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = " update barang_dalam_perjalanan set is_submit = 'S' "
                    sqlcom = sqlcom + " where seq_entry = " & CType(Me.dg_data.Items(x).FindControl("lbl_no_dokumen_impor"), Label).Text
                    sqlcom = sqlcom + " and id_transaction_period = " & Me.dd_bulan.SelectedValue
                    connection.koneksi.UpdateRecord(sqlcom)

                    Me.GL(CType(Me.dg_data.Items(x).FindControl("lbl_no_dokumen_impor"), Label).Text, CType(Me.dg_data.Items(x).FindControl("lbl_nilai_invoice"), Label).Text, CType(Me.dg_data.Items(x).FindControl("lbl_tgl_masuk_gudang"), Label).Text)

                End If
            Next



            Me.loadgrid()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class
