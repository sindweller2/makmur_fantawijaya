Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_detil_received_expedition_invoice
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()
    'Daniel

    Sub GL()
        Try
            Dim ppn As Decimal
            Dim reader As SqlClient.SqlDataReader
            sqlcom = "select sum(isnull(jumlah,0)) as jumlah from received_import_expedition_invoice_detail where item_ppn = 'Y' and id_invoice = '" & Me.lbl_no.Text.Trim & "'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                ppn = reader.Item("jumlah").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()


            Dim nonppn As Decimal = System.Convert.ToDecimal(Me.lbl_total_nilai.Text) - ppn
            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim keterangan As String = "Penerimaan Invoice Ekspedisi no. " & Me.lbl_no.Text
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tanggal.Text), id, Me.tradingClass.JurnalType("2"), keterangan, Me.vid_periode_transaksi)

            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tanggal.Text), Me.vakun_biaya_impor, Me.vakun_hutang_lain_lain, nonppn, 0, keterangan, Me.vid_periode_transaksi, "IDR", Me.tb_kurs.Text, nonppn, 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tanggal.Text), Me.vakun_hutang_lain_lain, Me.vakun_biaya_impor, 0, nonppn, keterangan, Me.vid_periode_transaksi, "IDR", Me.tb_kurs.Text, 0, nonppn, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tanggal.Text), Me.vakun_ppn_masukan, Me.vakun_hutang_lain_lain, ppn, 0, keterangan, Me.vid_periode_transaksi, "IDR", Me.tb_kurs.Text, ppn, 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tanggal.Text), Me.vakun_hutang_lain_lain, Me.vakun_ppn_masukan, 0, ppn, keterangan, Me.vid_periode_transaksi, "IDR", Me.tb_kurs.Text, 0, ppn, String.Empty)

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

    Private ReadOnly Property vid_received() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_received")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property id_received() As Integer
        Get
            Dim o As Object = ViewState("id_received")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_received") = value
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

    Public Property vid_ekspedisi() As Integer
        Get
            Dim o As Object = ViewState("vid_ekspedisi")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_ekspedisi") = value
        End Set
    End Property

    Public Property vakun_biaya_impor() As String
        Get
            Dim o As Object = ViewState("vakun_biaya_impor")
            If Not o Is Nothing Then Return CStr(o) Else Return 0
        End Get
        Set(ByVal value As String)
            ViewState("vakun_biaya_impor") = value
        End Set
    End Property


    Public Property vakun_ppn_masukan() As String
        Get
            Dim o As Object = ViewState("vakun_ppn_masukan")
            If Not o Is Nothing Then Return CStr(o) Else Return 0
        End Get
        Set(ByVal value As String)
            ViewState("vakun_ppn_masukan") = value
        End Set
    End Property

    Public Property vakun_hutang_lain_lain() As String
        Get
            Dim o As Object = ViewState("vakun_hutang_lain_lain")
            If Not o Is Nothing Then Return CStr(o) Else Return 0
        End Get
        Set(ByVal value As String)
            ViewState("vakun_hutang_lain_lain") = value
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

    Sub bindmata_uang()
        Try
            sqlcom = "select id from currency order by id"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_mata_uang.DataSource = reader
            Me.dd_mata_uang.DataTextField = "id"
            Me.dd_mata_uang.DataValueField = "id"
            Me.dd_mata_uang.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clearno_aju()
        Me.tb_no_aju.Text = 0
        Me.lbl_no_aju.Text = "-----"
        Me.link_popup_no_aju.Visible = True
    End Sub

    Sub bindno_aju()
        Dim readeraju As SqlClient.SqlDataReader
        sqlcom = "select no_aju, id_expedition, daftar_expedition.name as nama_ekspedisi"
        sqlcom = sqlcom + " from penugasan_ekspedisi_impor"
        sqlcom = sqlcom + " inner join daftar_expedition on daftar_expedition.id = penugasan_ekspedisi_impor.id_expedition"
        sqlcom = sqlcom + " where seq = " & Me.tb_no_aju.Text
        readeraju = connection.koneksi.SelectRecord(sqlcom)
        readeraju.Read()
        If readeraju.HasRows Then
            Me.lbl_no_aju.Text = readeraju.Item("no_aju").ToString
            Me.lbl_no_penugasan.Text = Me.tb_no_aju.Text
            Me.lbl_nama_ekspedisi.Text = readeraju.Item("nama_ekspedisi").ToString
            Me.vid_ekspedisi = readeraju.Item("id_expedition").ToString
        End If
        readeraju.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            tradingClass.ViewButtonUnsubmit(Me.btn_unsubmit)
            Me.bindperiodetransaksi()
            Me.bindmata_uang()
            Me.clearno_aju()
            Me.loaddata()
            Me.loadgrid()

            If String.IsNullOrEmpty(Me.tb_tanggal.Text) Then
                Me.tb_tanggal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            End If

            Me.tb_no_aju.Attributes.Add("style", "display: none;")
            Me.link_refresh_no_aju.Attributes.Add("style", "display: none;")
            Me.link_popup_no_aju.Attributes.Add("onclick", "popup_penugasan('" & Me.tb_no_aju.ClientID & "','" & Me.link_refresh_no_aju.UniqueID & "')")
        End If
    End Sub

    Protected Sub link_refresh_no_aju_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_no_aju.Click
        Me.bindno_aju()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/received_import_expedition_invoice.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Sub loaddata()
        Try

            If Me.vid_received <> 0 Then
                Me.id_received = Me.vid_received
            End If

            sqlcom = "select id, rtrim(convert(char, tanggal, 103)) as tanggal, id_expedition,"
            sqlcom = sqlcom + " invoice_no, rtrim(convert(char, invoice_date, 103)) as invoice_date,"
            sqlcom = sqlcom + " is_bayar, rtrim(convert(char, tanggal_bayar, 103)) as tanggal_bayar, seq_penugasan_ekspedisi,"
            sqlcom = sqlcom + " is_submit, id_transaction_period, rtrim(convert(char, tgl_jatuh_tempo, 103)) as tanggal_jatuh_tempo,"
            sqlcom = sqlcom + " rtrim(convert(char, tanggal_bayar, 103)) as tanggal_bayar"
            sqlcom = sqlcom + " from received_import_expedition_invoice"
            sqlcom = sqlcom + " where id = " & Me.id_received
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows() Then
                Me.lbl_no.Text = reader.Item("id").ToString
                Me.tb_tanggal.Text = reader.Item("tanggal").ToString
                Me.tb_no_aju.Text = reader.Item("seq_penugasan_ekspedisi").ToString
                Me.tb_no_invoice.Text = reader.Item("invoice_no").ToString
                Me.tb_tgl_invoice.Text = reader.Item("invoice_date").ToString
                Me.tb_tgl_jatuh_tempo.Text = reader.Item("tanggal_jatuh_tempo").ToString

                If reader.Item("is_submit").ToString = "B" Then
                    Me.lbl_status_submit.Text = "Belum disubmit"
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                    Me.btn_unsubmit.Enabled = False
                    Me.btn_add.Enabled = True
                    Me.btn_update.Enabled = True
                    Me.btn_delete.Enabled = True
                ElseIf reader.Item("is_submit").ToString = "S" Then
                    Me.lbl_status_submit.Text = "Sudah disubmit"
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                    Me.btn_unsubmit.Enabled = True
                    Me.btn_add.Enabled = False
                    Me.btn_update.Enabled = False
                    Me.btn_delete.Enabled = False
                End If

                'If reader.Item("is_bayar").ToString = "B" Then
                '    Me.lbl_status_bayar.Text = "Belum dibayar"
                'ElseIf reader.Item("is_bayar").ToString = "S" Then
                '    Me.lbl_status_bayar.Text = "Sudah dibayar"
                'End If

                If Not String.IsNullOrEmpty(reader.Item("tanggal_bayar").ToString) Then
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                    Me.btn_unsubmit.Enabled = False
                    Me.btn_add.Enabled = False
                    Me.btn_update.Enabled = False
                    Me.btn_delete.Enabled = False
                End If

                Me.bindno_aju()
            Else
                Me.btn_save.Enabled = True
                Me.btn_submit.Enabled = True
                Me.btn_unsubmit.Enabled = False
                Me.btn_add.Enabled = True
                Me.btn_update.Enabled = True
                Me.btn_delete.Enabled = True
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tanggal.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terlebih dahulu"
            ElseIf Me.tb_no_aju.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi no. aju terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_no_invoice.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor invoice terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_invoice.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal invoice terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_jatuh_tempo.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal jatuh tempo invoice terlebih dahulu"
            Else
                Dim vtgl As String = ""
                Dim vtgl_invoice As String = ""
                Dim vtgl_jatuh_tempo As String = ""

                vtgl = Me.tb_tanggal.Text.Substring(3, 2) & "/" & Me.tb_tanggal.Text.Substring(0, 2) & "/" & Me.tb_tanggal.Text.Substring(6, 4)
                vtgl_invoice = Me.tb_tgl_invoice.Text.Substring(3, 2) & "/" & Me.tb_tgl_invoice.Text.Substring(0, 2) & "/" & Me.tb_tgl_invoice.Text.Substring(6, 4)
                vtgl_jatuh_tempo = Me.tb_tgl_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(6, 4)

                If Me.id_received = 0 Then
                    Dim vmax As Integer = 0
                    sqlcom = "select isnull((max(id) - right(max(id), 4))/10000,0) + 1 as vmax"
                    sqlcom = sqlcom + " from received_import_expedition_invoice"
                    sqlcom = sqlcom + " where year(tanggal) = " & Year(Now.Date)
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = reader.Item("vmax").ToString & Year(Now.Date)
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into received_import_expedition_invoice(id, tanggal, id_expedition, invoice_no, invoice_date, is_bayar,"
                    sqlcom = sqlcom + " tanggal_bayar, seq_penugasan_ekspedisi, is_submit, is_submit_bayar, id_transaction_period, tgl_jatuh_tempo)"
                    sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "'," & Me.vid_ekspedisi & ",'" & Me.tb_no_invoice.Text & "','" & vtgl_invoice & "',"
                    sqlcom = sqlcom + "'B', NULL, " & Me.tb_no_aju.Text & ",'B', 'B'," & Me.vid_periode_transaksi & ",'" & vtgl_jatuh_tempo & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.id_received = vmax
                    Me.tradingClass.Alert("Data sudah disimpan", Me.Page)
                Else
                    sqlcom = "update received_import_expedition_invoice"
                    sqlcom = sqlcom + " set tanggal = '" & vtgl & "',"
                    sqlcom = sqlcom + " id_expedition = " & Me.vid_ekspedisi & ","
                    sqlcom = sqlcom + " invoice_no = '" & Me.tb_no_invoice.Text & "',"
                    sqlcom = sqlcom + " invoice_date = '" & vtgl_invoice & "',"
                    sqlcom = sqlcom + " seq_penugasan_ekspedisi = " & Me.tb_no_aju.Text & ","
                    sqlcom = sqlcom + " tgl_jatuh_tempo = '" & vtgl_jatuh_tempo & "'"
                    sqlcom = sqlcom + " where id = " & Me.id_received
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clearitem()
        Me.tb_nama_item.Text = ""
        Me.tb_nilai_invoice.Text = ""
        Me.tb_kurs.Text = 1
    End Sub

    Sub bindtotal()
        Try
            sqlcom = "select isnull(sum(isnull(jumlah,0)),0) as vtotal"
            sqlcom = sqlcom + " from received_import_expedition_invoice_detail"
            sqlcom = sqlcom + " where id_invoice = " & Me.id_received
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal").ToString, 2)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try

            Me.clearitem()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, description, id_currency, isnull(nilai_invoice,0) as nilai_invoice, isnull(kurs,0) as kurs, item_hpp, item_ppn, isnull(jumlah,0) as jumlah"
            sqlcom = sqlcom + " from received_import_expedition_invoice_detail"
            sqlcom = sqlcom + " where id_invoice = " & Me.id_received
            sqlcom = sqlcom + " order by seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "received_import_expedition_invoice_detail")
                Me.dg_data.DataSource = ds.Tables("received_import_expedition_invoice_detail").DefaultView

                If ds.Tables("received_import_expedition_invoice_detail").Rows.Count > 0 Then
                    If ds.Tables("received_import_expedition_invoice_detail").Rows.Count > 10 Then
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

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id from currency order by id"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataTextField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_id_currency"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        CType(Me.dg_data.Items(x).FindControl("dd_item_hpp"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_item_hpp"), Label).Text
                        CType(Me.dg_data.Items(x).FindControl("dd_item_ppn"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_item_ppn"), Label).Text
                    Next
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()

            Me.bindtotal()

        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Try
            If String.IsNullOrEmpty(Me.tb_nama_item.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama item pembayaran terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_nilai_invoice.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nilai invoiceterlebih dahulu"
            Else
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vmax from received_import_expedition_invoice_detail"
                sqlcom = sqlcom + " where id_invoice = " & Me.id_received
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vjumlah As Decimal = Decimal.ToDouble(Me.tb_nilai_invoice.Text) * Decimal.ToDouble(Me.tb_kurs.Text)

                sqlcom = "insert into received_import_expedition_invoice_detail(id_invoice, seq, description, nilai_invoice, kurs, id_currency, item_hpp, item_ppn, jumlah)"
                sqlcom = sqlcom + " values(" & Me.id_received & "," & vmax & ",'" & Me.tb_nama_item.Text & "'"
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_nilai_invoice.Text) & "," & Decimal.ToDouble(Me.tb_kurs.Text) & ",'" & Me.dd_mata_uang.SelectedValue & "',"
                sqlcom = sqlcom + "'" & Me.dd_item_hpp.SelectedValue & "','" & Me.dd_item_ppn.SelectedValue & "'," & vjumlah & ")"
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
                    sqlcom = "delete received_import_expedition_invoice_detail"
                    sqlcom = sqlcom + " where id_invoice = " & Me.id_received
                    sqlcom = sqlcom + " and seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
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
                    Dim vjumlah As Decimal = Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_nilai_invoice"), TextBox).Text) * Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kurs"), TextBox).Text)
                    sqlcom = "update received_import_expedition_invoice_detail"
                    sqlcom = sqlcom + " set description = '" & CType(Me.dg_data.Items(x).FindControl("tb_item"), TextBox).Text & "',"
                    sqlcom = sqlcom + " id_currency = '" & CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue & "',"
                    sqlcom = sqlcom + " nilai_invoice = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_nilai_invoice"), TextBox).Text) & ","
                    sqlcom = sqlcom + " kurs = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kurs"), TextBox).Text) & ","
                    sqlcom = sqlcom + " item_hpp = '" & CType(Me.dg_data.Items(x).FindControl("dd_item_hpp"), DropDownList).SelectedValue & "',"
                    sqlcom = sqlcom + " item_ppn = '" & CType(Me.dg_data.Items(x).FindControl("dd_item_ppn"), DropDownList).SelectedValue & "',"
                    sqlcom = sqlcom + " jumlah = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_nilai_invoice"), TextBox).Text) * Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kurs"), TextBox).Text)
                    sqlcom = sqlcom + " where id_invoice = " & Me.id_received
                    sqlcom = sqlcom + " and seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try

            '======== cek akun biaya impor
            sqlcom = "select akun_biaya_impor, akun_ppn_masukan"
            sqlcom = sqlcom + " from akun_penerimaan_inv_eks_imp"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_biaya_impor = reader.Item("akun_biaya_impor").ToString.Trim
                Me.vakun_ppn_masukan = reader.Item("akun_ppn_masukan").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_biaya_impor) Then
                Me.lbl_msg.Text = "Akun biaya impor di Akun penerimaan invoice ekspedisi impor tidak ada"
                Exit Sub
            ElseIf String.IsNullOrEmpty(Me.vakun_ppn_masukan) Then
                Me.lbl_msg.Text = "Akun ppn masukan di Akun penerimaan invoice ekspedisi impor tidak ada"
                Exit Sub
            End If


            '======== cek akun hutang lain-lain ekspedisi impor
            sqlcom = "select akun_hutang_lain2"
            sqlcom = sqlcom + " from daftar_expedition"
            sqlcom = sqlcom + " where id = " & Me.vid_ekspedisi
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_hutang_lain_lain = reader.Item("akun_hutang_lain2").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_hutang_lain_lain) Then
                Me.lbl_msg.Text = "Akun hutang lain-lain di Ekspedisi impor tidak ada"
                Exit Sub
            End If

            sqlcom = "select * from received_import_expedition_invoice_detail"
            sqlcom = sqlcom + " where id_invoice = " & Me.id_received
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                sqlcom = "update received_import_expedition_invoice"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where id = " & Me.id_received
                connection.koneksi.UpdateRecord(sqlcom)
                Me.loaddata()
                'Daniel
                'Me.jurnal()
                Me.GL()
                'Daniel

            Else
                Me.lbl_msg.Text = "Penerimaan invoice tersebut belum ada item pembayarannya"
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
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
        Dim vtgl As String = Me.tb_tanggal.Text.Substring(3, 2) & "/" & Me.tb_tanggal.Text.Substring(0, 2) & "/" & Me.tb_tanggal.Text.Substring(6, 4)
        Dim total_nilai As Decimal = 0
        Dim total_ppn As Decimal = 0

        sqlcom = "select isnull(sum(isnull(jumlah,0)),0) as jumlah"
        sqlcom = sqlcom + " from received_import_expedition_invoice_detail"
        sqlcom = sqlcom + " where id_invoice = " & Me.id_received
        sqlcom = sqlcom + " and item_ppn = 'T'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            total_nilai = reader.Item("jumlah").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        sqlcom = "select isnull(sum(isnull(jumlah,0)),0) as jumlah"
        sqlcom = sqlcom + " from received_import_expedition_invoice_detail"
        sqlcom = sqlcom + " where id_invoice = " & Me.id_received
        sqlcom = sqlcom + " and item_ppn = 'Y'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            total_ppn = reader.Item("jumlah").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()


        'debet
        ' akun_biaya_impor -> akun_hutang_lain_lain

        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no.Text & "','" & vtgl & "','TRMINVEKSIMP','" & Me.vakun_biaya_impor & "',"
        sqlcom = sqlcom + "'" & Me.vakun_hutang_lain_lain & "'," & Decimal.ToDouble(total_nilai) & ",0, 'Penerimaan invoice ekspedisi impor no. " & Me.lbl_no.Text & "'"
        sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'IDR', 1)"
        connection.koneksi.InsertRecord(sqlcom)


        ' akun_ppn_masukan -> akun_hutang_lain_lain

        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no.Text & "','" & vtgl & "','TRMINVEKSIMP','" & Me.vakun_ppn_masukan & "',"
        sqlcom = sqlcom + "'" & Me.vakun_hutang_lain_lain & "'," & Decimal.ToDouble(total_ppn) & ",0, 'Penerimaan invoice ekspedisi impor no. " & Me.lbl_no.Text & "'"
        sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'IDR', 1)"
        connection.koneksi.InsertRecord(sqlcom)


        'kredit
        ' akun_hutang_lain_lain -> akun_biaya_impor
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no.Text & "','" & vtgl & "','TRMINVEKSIMP','" & Me.vakun_hutang_lain_lain & "',"
        sqlcom = sqlcom + "'" & Me.vakun_biaya_impor & "', 0," & Decimal.ToDouble(total_nilai) & ", 'Penerimaan invoice ekspedisi impor no. " & Me.lbl_no.Text & "'"
        sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'IDR', 1)"
        connection.koneksi.InsertRecord(sqlcom)

        ' akun_hutang_lain_lain -> akun_ppn_masukan
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no.Text & "','" & vtgl & "','TRMINVEKSIMP','" & Me.vakun_hutang_lain_lain & "',"
        sqlcom = sqlcom + "'" & Me.vakun_ppn_masukan & "', 0," & Decimal.ToDouble(total_ppn) & ", 'Penerimaan invoice ekspedisi impor no. " & Me.lbl_no.Text & "'"
        sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'IDR', 1)"
        connection.koneksi.InsertRecord(sqlcom)

    End Sub

    Protected Sub btn_kurs_idr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_idr.Click
        Me.tb_kurs.Text = "1.00"
    End Sub

    Protected Sub btn_kurs_usd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_usd.Click
        Me.tb_kurs.Text = tradingClass.KursBulanan("[kurs_bulanan]", Me.vid_periode_transaksi)
    End Sub

    Protected Sub btn_unsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_unsubmit.Click
        sqlcom = "update received_import_expedition_invoice"
        sqlcom = sqlcom + " set is_submit = 'B'"
        sqlcom = sqlcom + " where id = " & Me.id_received
        connection.koneksi.UpdateRecord(sqlcom)

        Dim keterangan As String = "Penerimaan Invoice Ekspedisi no. " & Me.lbl_no.Text
        Me.tradingClass.DeleteAkunJurnal(keterangan, Me.vid_periode_transaksi)
        Me.tradingClass.DeleteAkunGeneralLedger(keterangan, Me.vid_periode_transaksi)
        Me.tradingClass.Alert("Data sudah diunsubmit!", Me.Page)

        Me.loaddata()
        Me.loadgrid()
    End Sub
End Class
