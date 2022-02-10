Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_pembayaran_expedition_invoice
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()

    Sub GL()

        Try
            Dim DebitAccount As String = String.Empty
            Dim id As String = Me.tradingClass.IDTransaksiMax
            'Dim d_kurs As Decimal = 1 'System.Convert.ToDecimal(tradingClass.KursBulanan(Me.vid_periode_transaksi))
            Dim dr As SqlClient.SqlDataReader

            Dim keterangan As String = "Pembayaran invoice ekspedisi impor no. " & Me.lbl_no.Text & ""
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_bayar.Text), id, Me.tradingClass.JurnalType("4"), keterangan, Me.vid_periode_transaksi)



            dr = connection.koneksi.SelectRecord("select bank_account.id as id_bank_account, bank_account.account_code, " & _
                    "isnull(sum(isnull(received_import_expedition_invoice_detail.nilai_invoice,0)),0) as jumlah, bank_account.id_currency, received_import_expedition_invoice_detail.kurs " & _
                    "from bank_account inner join received_import_expedition_invoice_detail on received_import_expedition_invoice_detail.id_bank_account = bank_account.id " & _
                    "where (received_import_expedition_invoice_detail.id_invoice = " & Me.id_received & ") " & _
                    "group by bank_account.id, bank_account.account_code ,bank_account.id_currency, received_import_expedition_invoice_detail.kurs ")

            Do While dr.Read


                If Me.DropDownListAccount.SelectedIndex = 0 Then

                    DebitAccount = Me.DropDownListAccount.SelectedValue

                ElseIf Me.DropDownListAccount.SelectedIndex = 1 Then

                    DebitAccount = Me.vakun_hutang_lain_lain

                End If

                tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_bayar.Text), DebitAccount, dr.Item("account_code").ToString(), System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * System.Convert.ToDecimal(dr.Item("kurs").ToString()), 0, keterangan, Me.vid_periode_transaksi, dr.Item("id_currency").ToString(), System.Convert.ToDecimal(dr.Item("kurs").ToString()), IIf(dr.Item("id_currency").ToString() = "IDR", 0, System.Convert.ToDecimal(dr.Item("jumlah").ToString())), 0, String.Empty)
                tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_bayar.Text), dr.Item("account_code").ToString(), DebitAccount, 0, System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * System.Convert.ToDecimal(dr.Item("kurs").ToString()), keterangan, Me.vid_periode_transaksi, dr.Item("id_currency").ToString(), System.Convert.ToDecimal(dr.Item("kurs").ToString()), 0, IIf(dr.Item("id_currency").ToString() = "IDR", 0, System.Convert.ToDecimal(dr.Item("jumlah").ToString())), String.Empty)

                Dim id_periode As Integer = 0

                Dim vtgl As String = Me.tb_tgl_bayar.Text.Substring(3, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(0, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(6, 4)

                sqlcom = "select id from transaction_period"
                sqlcom = sqlcom + " where tgl_awal <= '" & vtgl & "' and tgl_akhir >= '" & vtgl & "'"
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
                sqlcom = sqlcom + " values (" & id_periode & "," & dr.Item("id_bank_account").ToString & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_bayar.Text) & "','"
                sqlcom = sqlcom + keterangan & "',0," & System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * System.Convert.ToDecimal(dr.Item("kurs").ToString())
                sqlcom = sqlcom + "," & Me.vmax_history_kas & ")"
                connection.koneksi.InsertRecord(sqlcom)

                'mengurangi saldo kas/bank
                sqlcom = "update bank_account"
                sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - isnull(" & System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * System.Convert.ToDecimal(dr.Item("kurs").ToString()) & ",0)"
                sqlcom = sqlcom + " where id = " & dr.Item("id_bank_account").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            dr.Close()
            connection.koneksi.CloseKoneksi()

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

    Public Property vakun_hutang_lain_lain() As String
        Get
            Dim o As Object = ViewState("vakun_hutang_lain_lain")
            If Not o Is Nothing Then Return CStr(o) Else Return 0
        End Get
        Set(ByVal value As String)
            ViewState("vakun_hutang_lain_lain") = value
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

    Sub clearno_aju()
        Me.tb_no_aju.Text = 0
        Me.lbl_no_aju.Text = "-----"
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
            Me.bindperiodetransaksi()
            Me.clearno_aju()
            Me.loaddata()
            Me.loadgrid()
            Me.tb_no_aju.Attributes.Add("style", "display: none;")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/pembayaran_invoice_ekspedisi.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Sub loaddata()
        Try
            If Me.vid_received <> 0 Then
                Me.id_received = Me.vid_received
            End If

            sqlcom = "select id, rtrim(convert(char, tanggal, 103)) as tanggal, id_expedition,"
            sqlcom = sqlcom + " invoice_no, rtrim(convert(char, invoice_date, 103)) as invoice_date,"
            sqlcom = sqlcom + " is_bayar, rtrim(convert(char, tanggal_bayar, 103)) as tanggal_bayar, seq_penugasan_ekspedisi,"
            sqlcom = sqlcom + " is_submit_bayar, id_transaction_period, rtrim(convert(char, tgl_jatuh_tempo, 103)) as tanggal_jatuh_tempo,"
            sqlcom = sqlcom + " rtrim(convert(char, tanggal_bayar, 103)) as tanggal_bayar"
            sqlcom = sqlcom + " from received_import_expedition_invoice"
            sqlcom = sqlcom + " where id = " & Me.id_received
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows() Then
                Me.lbl_no.Text = reader.Item("id").ToString
                Me.lbl_tanggal.Text = reader.Item("tanggal").ToString
                Me.tb_no_aju.Text = reader.Item("seq_penugasan_ekspedisi").ToString
                Me.lbl_no_invoice.Text = reader.Item("invoice_no").ToString
                Me.lbl_tgl_invoice.Text = reader.Item("invoice_date").ToString
                Me.lbl_tgl_jatuh_tempo.Text = reader.Item("tanggal_jatuh_tempo").ToString
                Me.tb_tgl_bayar.Text = reader.Item("tanggal_bayar").ToString

                If reader.Item("is_submit_bayar").ToString = "B" Then
                    Me.lbl_status_submit.Text = "Belum disubmit"
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                    Me.btn_update.Enabled = True
                ElseIf reader.Item("is_submit_bayar").ToString = "S" Then
                    Me.lbl_status_submit.Text = "Sudah disubmit"
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                    Me.btn_update.Enabled = False
                End If

                If reader.Item("is_bayar").ToString = "B" Then
                    Me.lbl_status_bayar.Text = "Belum dibayar"
                ElseIf reader.Item("is_bayar").ToString = "S" Then
                    Me.lbl_status_bayar.Text = "Sudah dibayar"
                End If

                Me.bindno_aju()
            Else
                Me.btn_save.Enabled = True
                Me.btn_submit.Enabled = True
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tgl_bayar.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran terlebih dahulu"
            Else
                Dim vtgl As String = ""
                Dim vtgl_invoice As String = ""
                Dim vtgl_jatuh_tempo As String = ""

                vtgl = Me.tb_tgl_bayar.Text.Substring(3, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(0, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(6, 4)

                sqlcom = "update received_import_expedition_invoice"
                sqlcom = sqlcom + " set tanggal_bayar = '" & vtgl & "',"
                sqlcom = sqlcom + " is_bayar = 'S'"
                sqlcom = sqlcom + " where id = " & Me.id_received
                connection.koneksi.UpdateRecord(sqlcom)
                tradingClass.Alert("Data sudah disimpan", Me.Page)
                Me.loaddata()
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
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
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, description, id_currency, isnull(nilai_invoice,0) as nilai_invoice,"
            sqlcom = sqlcom + "isnull(kurs,0) as kurs, isnull(jumlah,0) as jumlah, id_bank_account,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when item_hpp = 'Y' then 'Ya'"
            sqlcom = sqlcom + " when item_hpp = 'T' then 'Bukan'"
            sqlcom = sqlcom + " end as item_hpp,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when item_ppn = 'Y' then 'Ya'"
            sqlcom = sqlcom + " when item_ppn = 'T' then 'Bukan'"
            sqlcom = sqlcom + " end as item_ppn"
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

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name from bank_account order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).Items.Add(New ListItem("--- Kas/Bank---", 0))
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_id_bank"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).SelectedValue = 0
                            Me.btn_submit.Enabled = False
                        Else
                            CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_id_bank"), Label).Text
                            Me.btn_submit.Enabled = True
                        End If

                    Next
                Else
                    Me.dg_data.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()

            Me.bindtotal()

        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    If CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).SelectedValue = 0 Then
                        Me.lbl_msg.Text = "Silahkan memilih Kas/Bank terlebih dahulu"
                    Else
                        sqlcom = "select id_currency from bank_account where id = " & CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).SelectedValue
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        reader.Read()
                        If reader.HasRows Then
                            If reader.Item("id_currency").ToString <> CType(Me.dg_data.Items(x).FindControl("lbl_id_currency"), Label).Text Then
                                Me.lbl_msg.Text = "Mata uang Kas/Bank yang digunakan tidak sesuai dengan mata uang item pembayaran tersebut"
                                reader.Close()
                                connection.koneksi.CloseKoneksi()
                                Exit For
                            End If
                        End If
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        sqlcom = "update received_import_expedition_invoice_detail"
                        sqlcom = sqlcom + " set id_bank_account = " & CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).SelectedValue
                        sqlcom = sqlcom + " where id_invoice = " & Me.id_received
                        sqlcom = sqlcom + " and seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                        connection.koneksi.InsertRecord(sqlcom)
                        tradingClass.Alert("Data sudah diupdate", Me.Page)
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

    Sub jurnal()
        Try

            Dim id_periode As Integer = 0
            Dim vtgl As String = Me.tb_tgl_bayar.Text.Substring(3, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(0, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(6, 4)

            sqlcom = "select id from transaction_period"
            sqlcom = sqlcom + " where tgl_awal <= '" & vtgl & "' and tgl_akhir >= '" & vtgl & "'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                id_periode = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            sqlcom = "select bank_account.id as id_bank_account, bank_account.account_code,"
            sqlcom = sqlcom + " isnull(sum(isnull(received_import_expedition_invoice_detail.nilai_invoice,0)),0) as jumlah"
            sqlcom = sqlcom + " from bank_account"
            sqlcom = sqlcom + " inner join received_import_expedition_invoice_detail"
            sqlcom = sqlcom + " on received_import_expedition_invoice_detail.id_bank_account = bank_account.id"
            sqlcom = sqlcom + " where received_import_expedition_invoice_detail.id_invoice = " & Me.id_received
            sqlcom = sqlcom + " group by bank_account.id, bank_account.account_code"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Do While reader.Read()

                'debet
                ' akun_hutang_lain_lain -> akun kas/bank

                Me.seq_max()
                sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
                sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
                sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no.Text & "','" & vtgl & "','BYRINVEKSIMP','" & Me.vakun_hutang_lain_lain & "',"
                sqlcom = sqlcom + "'" & reader.Item("account_code").ToString & "'," & Decimal.ToDouble(reader.Item("jumlah").ToString) & ",0, 'Pembayaran invoice ekspedisi impor no. " & Me.lbl_no.Text & "'"
                sqlcom = sqlcom + "," & id_periode & ",'IDR', 1)"
                connection.koneksi.InsertRecord(sqlcom)

                'kredit
                ' akun kas/bank -> akun_hutang_lain_lain
                Me.seq_max()
                sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
                sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
                sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no.Text & "','" & vtgl & "','BYRINVEKSIMP','" & reader.Item("account_code").ToString & "',"
                sqlcom = sqlcom + "'" & Me.vakun_hutang_lain_lain & "', 0," & Decimal.ToDouble(reader.Item("jumlah").ToString) & ", 'Pembayaran invoice ekspedisi impor no. " & Me.lbl_no.Text & "'"
                sqlcom = sqlcom + "," & id_periode & ",'IDR', 1)"
                connection.koneksi.InsertRecord(sqlcom)

              
            Loop
            reader.Close()
            connection.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            Dim readersubmit As SqlClient.SqlDataReader
            sqlcom = "select tanggal_bayar from received_import_expedition_invoice"
            sqlcom = sqlcom + " where id = " & Me.id_received
            readersubmit = connection.koneksi.SelectRecord(sqlcom)
            readersubmit.Read()
            If readersubmit.HasRows Then
                If String.IsNullOrEmpty(readersubmit.Item("tanggal_bayar").ToString) Then
                    Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran dan disimpan terlebih dahulu"
                Else
                    sqlcom = "select akun_hutang_lain2"
                    sqlcom = sqlcom + " from daftar_expedition"
                    sqlcom = sqlcom + " where id = " & Me.vid_ekspedisi
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        Me.vakun_hutang_lain_lain = reader.Item("akun_hutang_lain2").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    If String.IsNullOrEmpty(Me.vakun_hutang_lain_lain) Then
                        Me.lbl_msg.Text = "Akun hutang lain-lain di Ekspedisi impor tidak ada"
                        Exit Sub
                    End If

                    Dim flag As String = "T"
                    sqlcom = "select isnull(sum(isnull(bank_account.saldo_akhir,0)),0) as saldo_akhir,"
                    sqlcom = sqlcom + " isnull(sum(isnull(received_import_expedition_invoice_detail.nilai_invoice,0)),0) as jumlah_invoice"
                    sqlcom = sqlcom + " from bank_account"
                    sqlcom = sqlcom + " inner join received_import_expedition_invoice_detail"
                    sqlcom = sqlcom + " on received_import_expedition_invoice_detail.id_bank_account = bank_account.id"
                    sqlcom = sqlcom + " where received_import_expedition_invoice_detail.id_invoice = " & Me.id_received
                    sqlcom = sqlcom + " group by bank_account.id"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    Do While reader.Read
                        If Decimal.ToDouble(reader.Item("saldo_akhir").ToString) < Decimal.ToDouble(reader.Item("jumlah_invoice").ToString) Then
                            flag = "T"
                        Else
                            flag = "Y"
                        End If
                    Loop
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    If flag = "T" Then
                        Me.lbl_msg.Text = "Ada saldo akhir kas/bank yang digunakan tidak mencukupi"
                        Exit Sub
                    End If

                    sqlcom = "update received_import_expedition_invoice"
                    sqlcom = sqlcom + " set is_submit_bayar = 'S'"
                    sqlcom = sqlcom + " where id = " & Me.id_received
                    connection.koneksi.UpdateRecord(sqlcom)

                    'Daniel
                    'Me.jurnal()
                    Me.GL()
                    'Daniel

                    Me.loaddata()
                End If
            End If
            readersubmit.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub


End Class
