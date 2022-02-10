Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_pembayaran_lc
    Inherits System.Web.UI.UserControl
    'Daniel
    Public tradingClass As New tradingClass()

    Sub GL(ByVal jenis_pembayaran As String, ByVal seq_pembayaran As Integer)
        Try
            Dim jumlah_nilai As String = Nothing
            Dim kurs As String = Nothing
            Dim jumlah_bayar As String = Nothing
            Dim vtgl As String = Nothing
            Dim no_lc_text As String = Nothing
            Dim mata_uang As String = Nothing
            Dim id_bank_account As Integer = 0
            Dim keterangan As String = Nothing
            Dim vakun_hutang_tr As String = Nothing

            sqlcom = "select isnull(pembayaran_lc.jumlah_nilai,0) as jumlah_nilai, isnull(pembayaran_lc.kurs,0) as kurs,"
            sqlcom = sqlcom + "isnull(pembayaran_lc.jumlah_nilai,0) * isnull(pembayaran_lc.kurs,0) as jumlah_nilai_idr,"
            sqlcom = sqlcom + " convert(char, pembayaran_lc.tanggal_bayar, 103) as tgl_bayar, pembayaran_lc.id_bank, lc.no_lc, purchase_order.id_currency"
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join lc on lc.seq = pembayaran_lc.seq_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = lc.no_po"
            sqlcom = sqlcom + " where pembayaran_lc.seq_lc = " & Me.vno_lc
            sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = " & seq_pembayaran
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()

            If reader.HasRows Then
                jumlah_nilai = reader.Item("jumlah_nilai").ToString
                kurs = reader.Item("kurs").ToString
                jumlah_bayar = reader.Item("jumlah_nilai_idr").ToString
                vtgl = reader.Item("tgl_bayar").ToString.Trim
                no_lc_text = reader.Item("no_lc").ToString
                mata_uang = reader.Item("id_currency").ToString

                If Not String.IsNullOrEmpty(reader.Item("id_bank").ToString) Then
                    id_bank_account = reader.Item("id_bank").ToString
                End If

            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim d_kurs As Decimal = kurs 'System.Convert.ToDecimal(tradingClass.KursBulanan(Me.vid_periode_transaksi))



            If jenis_pembayaran = "L/C" Then
                keterangan = "Pembayaran L/C no. " & Me.vno_lc & " (Pembayaran ke " & seq_pembayaran & ", L/C no" & no_lc_text & ")"

                Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(vtgl), id, Me.tradingClass.JurnalType("4"), keterangan, Me.vid_periode)

                tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtgl), Me.DropDownListAccount.SelectedValue, Me.vakun_bank_account, jumlah_bayar, 0, keterangan, Me.vid_periode, mata_uang, d_kurs, jumlah_nilai, 0, String.Empty)
                tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtgl), Me.vakun_bank_account, Me.DropDownListAccount.SelectedValue, 0, jumlah_bayar, keterangan, Me.vid_periode, mata_uang, d_kurs, 0, jumlah_nilai, String.Empty)

            Else
                If Me.lbl_mata_uang.Text = "IDR" Then
                    vakun_hutang_tr = Me.vakun_hutang_tr_idr
                Else
                    vakun_hutang_tr = Me.vakun_hutang_tr_usd
                End If

                keterangan = "Pembayaran L/C no. " & Me.vno_lc & " (Pembayaran ke " & seq_pembayaran & ", L/C no" & no_lc_text & ")"

                Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(vtgl), id, Me.tradingClass.JurnalType("4"), keterangan, Me.vid_periode)

                tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtgl), Me.vakun_pembelian_impor, Me.vakun_bank_account, jumlah_bayar, 0, keterangan, Me.vid_periode, mata_uang, d_kurs, jumlah_nilai, 0, String.Empty)
                tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtgl), Me.vakun_bank_account, Me.vakun_pembelian_impor, 0, jumlah_bayar, keterangan, Me.vid_periode, mata_uang, d_kurs, 0, jumlah_nilai, String.Empty)

            End If

            Dim id_periode As Integer = 0
            sqlcom = "select id from transaction_period"
            sqlcom = sqlcom + " where tgl_awal <= '" & Me.tradingClass.DateValidated(vtgl) & "' and tgl_akhir >= '" & Me.tradingClass.DateValidated(vtgl) & "'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                id_periode = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            'history kas
            Me.seq_max_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
            sqlcom = sqlcom + " values (" & id_periode & "," & id_bank_account & ",'" & Me.tradingClass.DateValidated(vtgl) & "','"
            sqlcom = sqlcom + keterangan & "', 0"
            sqlcom = sqlcom + ", " & Decimal.ToDouble(jumlah_bayar) & "," & Me.vmax_history_kas & ")"
            connection.koneksi.InsertRecord(sqlcom)

            'mengurangi saldo kas/bank
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - isnull(" & Decimal.ToDouble(jumlah_bayar) & ",0)"
            sqlcom = sqlcom + " where id = " & id_bank_account
            connection.koneksi.UpdateRecord(sqlcom)
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

    Private ReadOnly Property vno_lc() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_lc")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property vno_po() As Integer
        Get
            Dim o As Object = ViewState("vno_po")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vno_po") = value
        End Set
    End Property

    Public Property vid_bank() As Integer
        Get
            Dim o As Object = ViewState("vid_bank")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_bank") = value
        End Set
    End Property

    Public Property vid_periode() As Integer
        Get
            Dim o As Object = ViewState("vid_periode")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_periode") = value
        End Set
    End Property

    Public Property vakun_uang_muka_lc() As String
        Get
            Dim o As Object = ViewState("vakun_uang_muka_lc")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_uang_muka_lc") = value
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

    Public Property vakun_hutang_tr_idr() As String
        Get
            Dim o As Object = ViewState("vakun_hutang_tr_idr")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_hutang_tr_idr") = value
        End Set
    End Property

    Public Property vakun_hutang_tr_usd() As String
        Get
            Dim o As Object = ViewState("vakun_hutang_tr_usd")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_hutang_tr_usd") = value
        End Set
    End Property

    Public Property vakun_bank_account() As String
        Get
            Dim o As Object = ViewState("vakun_bank_account")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_bank_account") = value
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


    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindjenis_lc()
        sqlcom = "select code, name from lc_type order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_jenis_lc.DataSource = reader
        Me.dd_jenis_lc.DataTextField = "name"
        Me.dd_jenis_lc.DataValueField = "code"
        Me.dd_jenis_lc.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindperiode()
        sqlcom = "select name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_periode.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loaddata()
        Try
            sqlcom = "select purchase_order.no as no_po, purchase_order.po_no_text, convert(char, purchase_order.tanggal, 103) as tanggal, purchase_order.id_currency,"
            sqlcom = sqlcom + " lc.no_lc, convert(char, lc.tanggal_lc, 103) as tanggal_lc, convert(char, lc.tgl_berlaku_lc, 103) as tgl_berlaku_lc,"
            sqlcom = sqlcom + " lc.id_lc_type, convert(char, lc.due_date_lc, 103) as due_date_lc, lc.id_negara_koresponden, lc.id_dikapalkan_dari,"
            sqlcom = sqlcom + " lc.id_pelabuhan_tujuan, lc.id_negara_asal, daftar_supplier.name as nama_supplier,"
            sqlcom = sqlcom + " isnull(lc.nilai_lc,0) as total_pembelian, transaction_period.name as nama_periode, lc.is_lc_lunas,"
            sqlcom = sqlcom + " lc.id_bank, lc.id_transaction_period, bank_list.name as nama_bank"
            sqlcom = sqlcom + " from lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = lc.no_po"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " inner join transaction_period on transaction_period.id = lc.id_transaction_period"
            sqlcom = sqlcom + " inner join bank_list on bank_list.id = lc.id_bank"
            sqlcom = sqlcom + " where lc.seq = " & Me.vno_lc
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vno_po = reader.Item("no_po").ToString
                Me.vid_bank = reader.Item("id_bank").ToString
                Me.vid_periode = reader.Item("id_transaction_period").ToString
                Me.lbl_periode.Text = reader.Item("nama_periode").ToString
                Me.lbl_no_pembelian.Text = reader.Item("po_no_text").ToString
                Me.lbl_tgl_pembelian.Text = reader.Item("tanggal").ToString
                Me.lbl_mata_uang.Text = reader.Item("id_currency").ToString
                Me.lbl_total_nilai_pembelian.Text = FormatNumber(reader.Item("total_pembelian").ToString, 2)
                Me.lbl_nama_supplier.Text = reader.Item("nama_supplier").ToString
                Me.lbl_nama_bank.Text = reader.Item("nama_bank").ToString
                Me.lbl_no_lc.Text = reader.Item("no_lc").ToString
                Me.lbl_tgl_lc.Text = reader.Item("tanggal_lc").ToString
                Me.lbl_tgl_berlaku_lc.Text = reader.Item("tgl_berlaku_lc").ToString
                Me.dd_jenis_lc.SelectedValue = reader.Item("id_lc_type").ToString
                Me.lbl_tgl_jatuh_tempo_lc.Text = reader.Item("due_date_lc").ToString

                If reader.Item("is_lc_lunas").ToString = "S" Then
                    Me.lbl_status_lunas.Text = "Sudah lunas"
                Else
                    Me.lbl_status_lunas.Text = "Belum lunas"
                End If
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select po_no, pembayaran_ke, prosentase, jumlah_nilai, id_bank, convert(char, tanggal_bayar, 103) as tanggal_bayar,"
            sqlcom = sqlcom + " kurs, (kurs * jumlah_nilai) as jumlah_nilai_idr, no_giro, is_submit,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when jenis_bayar = 'L' then 'L/C'"
            sqlcom = sqlcom + " when jenis_bayar = 'T' then 'T/R'"
            sqlcom = sqlcom + " end as jenis_bayar_text, jenis_bayar,"
            sqlcom = sqlcom + " convert(char, tgl_giro, 103) as tgl_giro, convert(char, tgl_jatuh_tempo, 103) as tgl_jatuh_tempo,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when pembayaran_ke = 1 then 'I'"
            sqlcom = sqlcom + " when pembayaran_ke = 2 then 'II'"
            sqlcom = sqlcom + " when pembayaran_ke = 3 then 'III'"
            sqlcom = sqlcom + " end as pembayaran_ke_nama"
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " where seq_lc = " & Me.vno_lc
            sqlcom = sqlcom + " order by pembayaran_ke"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "pembayaran_lc")
                Me.dg_data.DataSource = ds.Tables("pembayaran_lc").DefaultView

                If ds.Tables("pembayaran_lc").Rows.Count > 0 Then
                    If ds.Tables("pembayaran_lc").Rows.Count > 8 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 8
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_update.Visible = True

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name from bank_account where id_currency = '" & Me.lbl_mata_uang.Text & "' order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).Items.Add(New ListItem("---Kas/Bank---", 0))
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue = 0
                        Else
                            CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text
                        End If

                        If CType(Me.dg_data.Items(x).FindControl("lbl_jenis_bayar"), Label).Text = "L" Then
                            CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).Enabled = "True"
                        Else
                            CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).Enabled = "False"
                        End If

                        If CType(Me.dg_data.Items(x).FindControl("lbl_is_submit"), Label).Text = "S" Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                            CType(Me.dg_data.Items(x).FindControl("link_submit"), LinkButton).Enabled = False
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                            CType(Me.dg_data.Items(x).FindControl("link_submit"), LinkButton).Enabled = True
                        End If
                    Next
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
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
            Me.bindperiode()
            Me.bindjenis_lc()
            Me.loaddata()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/pembayaran_lc.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Sub update_status_lc_lunas()
        Try
            sqlcom = "select sum(isnull(jumlah_nilai,0)) as vtotal"
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " where seq_lc = " & Me.vno_lc
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                sqlcom = "update lc"
                If Decimal.ToDouble(reader.Item("vtotal").ToString) >= Decimal.ToDouble(Me.lbl_total_nilai_pembelian.Text) Then
                    sqlcom = sqlcom + " set is_lc_lunas = 'S'"
                Else
                    sqlcom = sqlcom + " set is_lc_lunas = 'B'"
                End If
                sqlcom = sqlcom + " where seq = " & Me.vno_lc
                connection.koneksi.UpdateRecord(sqlcom)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_bayar"), TextBox).Text) Then
                        Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran terlebih dahulu"
                    ElseIf CType(Me.dg_data.Items(x).FindControl("lbl_jenis_bayar"), Label).Text = "L" And CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue = 0 Then
                        Me.lbl_msg.Text = "Silahkan mengisi Kas/Bank terlebih dahulu"
                    Else
                        Dim vtgl_bayar As String = CType(Me.dg_data.Items(x).FindControl("tb_tgl_bayar"), TextBox).Text
                        Dim vtgl_giro As String = ""
                        Dim vtgl_jatuh_tempo As String = ""

                        vtgl_bayar = vtgl_bayar.Substring(3, 2) & "/" & vtgl_bayar.Substring(0, 2) & "/" & vtgl_bayar.Substring(6, 4)

                        If Not String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_giro"), TextBox).Text) Then
                            vtgl_giro = CType(Me.dg_data.Items(x).FindControl("tb_tgl_giro"), TextBox).Text.Substring(3, 2) & "/" & CType(Me.dg_data.Items(x).FindControl("tb_tgl_giro"), TextBox).Text.Substring(0, 2) & "/" & CType(Me.dg_data.Items(x).FindControl("tb_tgl_giro"), TextBox).Text.Substring(6, 4)
                        End If

                        If Not String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_jth_tempo"), TextBox).Text) Then
                            vtgl_jatuh_tempo = CType(Me.dg_data.Items(x).FindControl("tb_tgl_jth_tempo"), TextBox).Text.Substring(3, 2) & "/" & CType(Me.dg_data.Items(x).FindControl("tb_tgl_jth_tempo"), TextBox).Text.Substring(0, 2) & "/" & CType(Me.dg_data.Items(x).FindControl("tb_tgl_jth_tempo"), TextBox).Text.Substring(6, 4)
                        End If


                        sqlcom = "update pembayaran_lc"
                        sqlcom = sqlcom + " set tanggal_bayar = '" & vtgl_bayar & "',"

                        If CType(Me.dg_data.Items(x).FindControl("lbl_jenis_bayar"), Label).Text = "L" Then
                            sqlcom = sqlcom + " id_bank = " & CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue & ","
                        Else
                            sqlcom = sqlcom + " id_bank = NULL ,"
                        End If

                        sqlcom = sqlcom + " no_giro = '" & CType(Me.dg_data.Items(x).FindControl("tb_no_giro"), TextBox).Text & "',"

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_jth_tempo"), TextBox).Text) Then
                            sqlcom = sqlcom + " tgl_giro = NULL,"
                        Else
                            sqlcom = sqlcom + " tgl_giro = '" & vtgl_giro & "',"
                        End If

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_jth_tempo"), TextBox).Text) Then
                            sqlcom = sqlcom + " tgl_jatuh_tempo = NULL"
                        Else
                            sqlcom = sqlcom + " tgl_jatuh_tempo = '" & vtgl_jatuh_tempo & "'"
                        End If

                        sqlcom = sqlcom + " where seq_lc = " & Me.vno_lc
                        sqlcom = sqlcom + " and pembayaran_ke = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                        connection.koneksi.UpdateRecord(sqlcom)
                        tradingClass.Alert("Data sudah diupdate", Me.Page)
                        Me.update_status_lc_lunas()
                    End If
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub seq_max()
        Dim readermax As SqlClient.SqlDataReader
        sqlcom = "select isnull(max(seq),0) + 1 as vmax from akun_general_ledger"
        readermax = connection.koneksi.SelectRecord(sqlcom)
        readermax.Read()
        If readermax.HasRows Then
            Me.vmax = readermax.Item("vmax").ToString
        End If
        readermax.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub seq_max_history_kas()
        Dim readermax As SqlClient.SqlDataReader
        sqlcom = "select isnull(max(seq),0) + 1 as vmax from history_kas"
        readermax = connection.koneksi.SelectRecord(sqlcom)
        readermax.Read()
        If readermax.HasRows Then
            Me.vmax_history_kas = readermax.Item("vmax").ToString
        End If
        readermax.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub jurnal(ByVal jenis_pembayaran As String, ByVal seq_pembayaran As Integer)
        Try
            Dim jumlah_nilai As String = Nothing
            Dim kurs As String = Nothing
            Dim jumlah_bayar As String = Nothing
            Dim vtgl As String = Nothing
            Dim no_lc_text As String = Nothing
            Dim mata_uang As String = Nothing
            Dim id_bank_account As Integer = 0

            sqlcom = "select isnull(pembayaran_lc.jumlah_nilai,0) as jumlah_nilai, isnull(pembayaran_lc.kurs,0) as kurs,"
            sqlcom = sqlcom + "isnull(pembayaran_lc.jumlah_nilai,0) * isnull(pembayaran_lc.kurs,0) as jumlah_nilai_idr,"
            sqlcom = sqlcom + " convert(char, pembayaran_lc.tanggal_bayar, 103) as tgl_bayar, pembayaran_lc.id_bank, lc.no_lc, purchase_order.id_currency"
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " inner join lc on lc.seq = pembayaran_lc.seq_lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = lc.no_po"
            sqlcom = sqlcom + " where pembayaran_lc.seq_lc = " & Me.vno_lc
            sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = " & seq_pembayaran
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                jumlah_nilai = reader.Item("jumlah_nilai").ToString
                kurs = reader.Item("kurs").ToString
                jumlah_bayar = reader.Item("jumlah_nilai_idr").ToString
                vtgl = reader.Item("tgl_bayar").ToString.Trim
                no_lc_text = reader.Item("no_lc").ToString
                mata_uang = reader.Item("id_currency").ToString

                If Not String.IsNullOrEmpty(reader.Item("id_bank").ToString) Then
                    id_bank_account = reader.Item("id_bank").ToString
                End If

            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            vtgl = vtgl.Substring(3, 2) & "/" & vtgl.Substring(0, 2) & "/" & vtgl.Substring(6, 4)

            Dim id_periode As Integer = 0
            sqlcom = "select id from transaction_period"
            sqlcom = sqlcom + " where tgl_awal <= '" & vtgl & "' and tgl_akhir >= '" & Me.tradingClass.DateValidated(vtgl) & "'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                id_periode = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()


            If jenis_pembayaran = "L/C" Then
                'debet akun_uang_muka_lc -> kas_bank
                Me.seq_max()
                sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
                sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
                sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRLC','" & Me.vakun_uang_muka_lc & "',"
                sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(jumlah_bayar) & ",0, 'Pembayaran L/C no. " & Me.vno_lc & " (Pembayaran ke " & seq_pembayaran & ", L/C no" & no_lc_text & ")'"
                sqlcom = sqlcom + "," & id_periode & ",'" & mata_uang & "'," & kurs & "," & jumlah_nilai & ",0)"
                connection.koneksi.InsertRecord(sqlcom)

                'kredit
                ' akun kas/bank -> akun_hutang_lain_lain
                Me.seq_max()
                sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
                sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
                sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRLC','" & Me.vakun_bank_account & "',"
                sqlcom = sqlcom + "'" & Me.vakun_uang_muka_lc & "', 0," & Decimal.ToDouble(jumlah_bayar) & ", 'Pembayaran L/C no. " & Me.vno_lc & " (Pembayaran ke " & seq_pembayaran & ", L/C no" & no_lc_text & ")'"
                sqlcom = sqlcom + "," & id_periode & ",'" & mata_uang & "'," & kurs & ", 0," & jumlah_nilai & ")"
                connection.koneksi.InsertRecord(sqlcom)

               

            Else

                Dim vakun_hutang_tr As String = Nothing

                If Me.lbl_mata_uang.Text = "IDR" Then
                    vakun_hutang_tr = Me.vakun_hutang_tr_idr
                Else
                    vakun_hutang_tr = Me.vakun_hutang_tr_usd
                End If

                'debet akun_pembelian impor -> hutang tr
                Me.seq_max()
                sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
                sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
                sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRLC','" & Me.vakun_pembelian_impor & "',"
                sqlcom = sqlcom + "'" & vakun_hutang_tr & "'," & Decimal.ToDouble(jumlah_bayar) & ",0, 'Pembayaran L/C no. " & Me.vno_lc & " (Pembayaran ke " & seq_pembayaran & ", L/C no" & no_lc_text & ")'"
                sqlcom = sqlcom + "," & id_periode & ",'" & mata_uang & "'," & kurs & "," & jumlah_nilai & ",0)"
                connection.koneksi.InsertRecord(sqlcom)

                'kredit
                ' akun hutang tr -> pembelian impor
                Me.seq_max()
                sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
                sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
                sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vno_lc & "','" & vtgl & "','BYRLC','" & vakun_hutang_tr & "',"
                sqlcom = sqlcom + "'" & Me.vakun_pembelian_impor & "', 0," & Decimal.ToDouble(jumlah_bayar) & ", 'Pembayaran L/C no. " & Me.vno_lc & " (Pembayaran ke " & seq_pembayaran & ", L/C no" & no_lc_text & ")'"
                sqlcom = sqlcom + "," & id_periode & ",'" & mata_uang & "'," & kurs & ", 0," & jumlah_nilai & ")"
                connection.koneksi.InsertRecord(sqlcom)
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub submit(ByVal jenis_pembayaran As String, ByVal seq_pembayaran As Integer, ByVal id_bank_account As Integer, ByVal jumlah_nilai As String)
        Try
            'Daniel
            If id_bank_account <> 0 Then
                'Daniel
           

                'cek akun uang muka lc, pembelian impor
                sqlcom = "select akun_uang_muka_lc, akun_pembelian_impor"
                sqlcom = sqlcom + " from akun_pembayaran_lc"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.vakun_uang_muka_lc = reader.Item("akun_uang_muka_lc").ToString
                    Me.vakun_pembelian_impor = reader.Item("akun_pembelian_impor").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                If String.IsNullOrEmpty(Me.vakun_uang_muka_lc) Then
                    Me.lbl_msg.Text = "Akun uang muka L/C pada akun pembayaran L/C tidak ada"
                    Exit Sub
                End If

                If String.IsNullOrEmpty(Me.vakun_pembelian_impor) Then
                    Me.lbl_msg.Text = "Akun pembelian impor pada akun pembayaran L/C tidak ada"
                    Exit Sub
                End If

                If jenis_pembayaran = "L/C" Then

                    'cek akun bank account dan saldo akhir
                    Dim vsaldo_akhir As Decimal = 0
                    sqlcom = "select account_code, isnull(saldo_akhir,0) as saldo_akhir from bank_account where id = " & id_bank_account
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        Me.vakun_bank_account = reader.Item("account_code").ToString
                        vsaldo_akhir = reader.Item("saldo_akhir").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    If String.IsNullOrEmpty(Me.vakun_bank_account) Then
                        Me.lbl_msg.Text = "Kode akun pada Kas/Bank tersebut tidak ada"
                        Exit Sub
                    End If

                    'If vsaldo_akhir < Decimal.ToDouble(jumlah_nilai) Then
                    'Me.lbl_msg.Text = "Saldo akhir Kas/Bank tersebut tidak mencukupi"
                    'Exit Sub
                    'End If

                Else

                    'cek akun hutang T/R pada bank

                    sqlcom = "select akun_hutang_tr_idr, akun_hutang_tr_usd"
                    sqlcom = sqlcom + " from bank_list"
                    sqlcom = sqlcom + " where id = " & vid_bank
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        Me.vakun_hutang_tr_idr = reader.Item("akun_hutang_tr_idr").ToString
                        Me.vakun_hutang_tr_usd = reader.Item("akun_hutang_tr_usd").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    If Me.lbl_mata_uang.Text = "IDR" And String.IsNullOrEmpty(Me.vakun_hutang_tr_idr) Then
                        Me.lbl_msg.Text = "Akun uang hutang T/R IDR pada bank tersebut tidak ada"
                        Exit Sub
                    End If

                    If Me.lbl_mata_uang.Text = "USD" And String.IsNullOrEmpty(Me.vakun_hutang_tr_usd) Then
                        Me.lbl_msg.Text = "Akun uang hutang T/R USD pada bank tersebut tidak ada"
                        Exit Sub
                    End If
                End If


                sqlcom = "update pembayaran_lc"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where seq_lc = " & Me.vno_lc
                sqlcom = sqlcom + " and pembayaran_ke = " & seq_pembayaran
                connection.koneksi.UpdateRecord(sqlcom)

                'Daniel
                'Me.jurnal(jenis_pembayaran, seq_pembayaran)
                Me.GL(jenis_pembayaran, seq_pembayaran)

                'Daniel
                Me.loadgrid()



                'Daniel
            Else
                tradingClass.Alert("Tidak ada nominal di bank", Me.Page)
            End If
            'Daniel
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkSubmit" Then
            sqlcom = "select convert(char, tanggal_bayar, 103) as tgl_bayar"
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " where pembayaran_lc.seq_lc = " & Me.vno_lc
            sqlcom = sqlcom + " and pembayaran_lc.pembayaran_ke = " & CType(e.Item.FindControl("lbl_seq"), Label).Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If String.IsNullOrEmpty(reader.Item("tgl_bayar").ToString) Then
                    Me.lbl_msg.Text = "Silahkan mengisi tanggal bayar terlebih dahulu"
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    Exit Sub
                Else
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    Me.submit(CType(e.Item.FindControl("lbl_jenis_bayar_text"), Label).Text, CType(e.Item.FindControl("lbl_seq"), Label).Text, CType(e.Item.FindControl("dd_bank"), DropDownList).SelectedValue, CType(e.Item.FindControl("lbl_jumlah"), Label).Text)
                End If
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        End If
    End Sub
End Class
