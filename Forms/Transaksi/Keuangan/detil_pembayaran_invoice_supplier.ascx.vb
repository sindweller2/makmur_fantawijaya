Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_pembayaran_invoice_supplier
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()

    Sub GL(ByVal vtgl_bayar As String, ByVal nilai_bayar As String, ByVal id_kasbank As Integer)
        Try
            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim d_kurs As Decimal = Me.lbl_kurs.Text  'System.Convert.ToDecimal(tradingClass.KursBulanan(Me.vid_periode_transaksi))
            Dim keterangan As String = "Pembayaran invoice supplier impor TT no. " & Me.lbl_no_dokumen.Text & "( Inv. Supplier no. " & Me.lbl_no_invoice.Text & ")"

            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_bayar.Text), id, Me.tradingClass.JurnalType("4"), keterangan, Me.vid_transaction_period)

            tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtgl_bayar), Me.vakun_hutang_dagang, Me.vakun_kasbank, System.Convert.ToDecimal(nilai_bayar) * d_kurs, 0, keterangan, Me.vid_transaction_period, Me.lbl_mata_uang.Text, d_kurs, IIf(Me.lbl_mata_uang.Text = "IDR", 0, System.Convert.ToDecimal(nilai_bayar)), 0, String.Empty)
            tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtgl_bayar), Me.vakun_kasbank, Me.vakun_hutang_dagang, 0, System.Convert.ToDecimal(nilai_bayar) * d_kurs, keterangan, Me.vid_transaction_period, Me.lbl_mata_uang.Text, d_kurs, 0, IIf(Me.lbl_mata_uang.Text = "IDR", 0, System.Convert.ToDecimal(nilai_bayar)), String.Empty)

            'history kas
            Me.seq_max_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
            sqlcom = sqlcom + " values (" & Me.vid_transaction_period & "," & id_kasbank & ",'" & Me.tradingClass.DateValidated(vtgl_bayar) & "','"
            sqlcom = sqlcom + keterangan
            sqlcom = sqlcom + "',0," & System.Convert.ToDecimal(nilai_bayar) * d_kurs & "," & Me.vmax_history_kas & ")"
            connection.koneksi.InsertRecord(sqlcom)

            'mengurangi saldo kas/bank
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - isnull(" & System.Convert.ToDecimal(nilai_bayar) * d_kurs & ",0)"
            sqlcom = sqlcom + " where id = " & id_kasbank
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

    Private ReadOnly Property vseq() As Integer
        Get
            Dim o As Object = Request.QueryString("vseq")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property bulan() As Integer
        Get
            Dim o As Object = ViewState("bulan")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("bulan") = value
        End Set
    End Property

    Public Property vid_transaction_period() As Integer
        Get
            Dim o As Object = ViewState("vid_transaction_period")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_transaction_period") = value
        End Set
    End Property

    Public Property vid_supplier() As Integer
        Get
            Dim o As Object = ViewState("vid_supplier")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_supplier") = value
        End Set
    End Property

    Public Property vakun_hutang_dagang() As String
        Get
            Dim o As Object = ViewState("vakun_hutang_dagang")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_hutang_dagang") = value
        End Set
    End Property

    Public Property vakun_kasbank() As String
        Get
            Dim o As Object = ViewState("vakun_kasbank")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_kasbank") = value
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

    Sub bindperiodetransaksi()
        sqlcom = "select id, name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_periode.Text = reader.Item("name").ToString
            Me.vid_transaction_period = reader.Item("id").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindbank()
        Dim readerinvoice As SqlClient.SqlDataReader
        sqlcom = "select id, name from bank_account where id_currency = '" & Me.lbl_mata_uang.Text & "' order by name"
        readerinvoice = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_bank.DataSource = readerinvoice
        Me.dd_bank.DataTextField = "name"
        Me.dd_bank.DataValueField = "id"
        Me.dd_bank.DataBind()
        readerinvoice.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindjenis_pembayaram()
        sqlcom = "select id, name from jenis_pembayaran order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_jenis_pembayaran.DataSource = reader
        Me.dd_jenis_pembayaran.DataTextField = "name"
        Me.dd_jenis_pembayaran.DataValueField = "id"
        Me.dd_jenis_pembayaran.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.tb_tgl_bayar.Text = ""
        Me.tb_no_giro.Text = ""
        Me.tb_tgl_giro.Text = ""
        Me.tb_jatuh_tempo.Text = ""
        Me.tb_nilai.Text = 0
    End Sub

    Sub bindinvoice()
        sqlcom = "select entry_dokumen_impor.seq, entry_dokumen_impor.invoice_no, rtrim(convert(char, entry_dokumen_impor.tgl_invoice, 103)) as tgl_invoice,"
        sqlcom = sqlcom + " rtrim(convert(char, entry_dokumen_impor.tgl_terima, 103)) as tgl_terima, isnull(entry_dokumen_impor.kurs,0) as kurs,"
        sqlcom = sqlcom + " rtrim(convert(char, entry_dokumen_impor.tgl_jatuh_tempo_invoice_supplier, 103)) as tgl_jatuh_tempo_invoice_supplier,"
        sqlcom = sqlcom + " isnull(nilai_invoice,0) as nilai_invoice, daftar_supplier.name as nama_supplier, purchase_order.id_currency as mata_uang,"
        sqlcom = sqlcom + " purchase_order.id_supplier"
        sqlcom = sqlcom + " from entry_dokumen_impor"
        sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
        sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
        sqlcom = sqlcom + " where entry_dokumen_impor.seq = " & Me.vseq
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.vid_supplier = reader.Item("id_supplier").ToString
            Me.lbl_tgl_terima.Text = reader.Item("tgl_terima").ToString
            Me.lbl_no_dokumen.Text = reader.Item("seq").ToString
            Me.lbl_no_invoice.Text = reader.Item("invoice_no").ToString
            Me.lbl_tgl_invoice.Text = reader.Item("tgl_invoice").ToString
            Me.lbl_tgl_jatuh_tempo.Text = reader.Item("tgl_jatuh_tempo_invoice_supplier").ToString
            Me.lbl_nama_supplier.Text = reader.Item("nama_supplier").ToString
            Me.lbl_mata_uang.Text = reader.Item("mata_uang").ToString
            Me.lbl_nilai_invoice.Text = FormatNumber(reader.Item("nilai_invoice").ToString, 2)
            Me.lbl_kurs.Text = FormatNumber(reader.Item("kurs").ToString, 2)
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.bindperiodetransaksi()
            Me.bindjenis_pembayaram()
            Me.clearform()
            Me.bindinvoice()
            Me.bindbank()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/pembayaran_invoice_supplier.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq_entry_dokumen, seq_pembayaran, is_submit_bayar, convert(char, tgl_bayar_invoice, 103) as tgl_bayar, id_bank_invoice, "
            sqlcom = sqlcom + " id_jenis_pembayaran_invoice, no_giro_invoice, convert(char, tgl_giro_invoice, 103) as tgl_giro_invoice,"
            sqlcom = sqlcom + " isnull(jumlah_bayar,0) as nilai_bayar,"
            sqlcom = sqlcom + " convert(char, tgl_jatuh_tempo_invoice, 103) as tgl_jatuh_tempo_invoice, id_transaction_period,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when seq_pembayaran = 1 then 'I'"
            sqlcom = sqlcom + " when seq_pembayaran = 2 then 'II'"
            sqlcom = sqlcom + " when seq_pembayaran = 3 then 'III'"
            sqlcom = sqlcom + " when seq_pembayaran = 4 then 'IV'"
            sqlcom = sqlcom + " when seq_pembayaran = 5 then 'V'"
            sqlcom = sqlcom + " end as pembayaran_ke"
            sqlcom = sqlcom + " from pembayaran_invoice_supplier_tt"
            sqlcom = sqlcom + " where pembayaran_invoice_supplier_tt.seq_entry_dokumen = " & Me.vseq
            sqlcom = sqlcom + " order by pembayaran_invoice_supplier_tt.seq_pembayaran"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "pembayaran_invoice_supplier_tt")
                Me.dg_data.DataSource = ds.Tables("pembayaran_invoice_supplier_tt").DefaultView

                If ds.Tables("pembayaran_invoice_supplier_tt").Rows.Count > 0 Then
                    If ds.Tables("pembayaran_invoice_supplier_tt").Rows.Count > 10 Then
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

                        sqlcom = "select id, name from jenis_pembayaran order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_bayar"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_bayar"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_bayar"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_bayar"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_bayar"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_jenis_bayar"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()


                        sqlcom = "select id, name from bank_account where id_currency = '" & Me.lbl_mata_uang.Text & "' order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If CType(Me.dg_data.Items(x).FindControl("lbl_is_submit_bayar"), Label).Text = "S" Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                            CType(Me.dg_data.Items(x).FindControl("lbl_submit"), LinkButton).Enabled = False
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                            CType(Me.dg_data.Items(x).FindControl("lbl_submit"), LinkButton).Enabled = True
                        End If
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

            Me.total_bayar()

        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub total_bayar()
        Try
            sqlcom = "select isnull(sum(isnull(jumlah_bayar,0)),0) as jumlah_bayar"
            sqlcom = sqlcom + " from pembayaran_invoice_supplier_tt"
            sqlcom = sqlcom + " where seq_entry_dokumen = " & Me.vseq
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If Decimal.ToDouble(reader.Item("jumlah_bayar").ToString) = Decimal.ToDouble(Me.lbl_nilai_invoice.Text) Then
                    Me.lbl_status_bayar.Text = "Sudah lunas"
                Else
                    Me.lbl_status_bayar.Text = "Belum lunas"
                End If
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
            ElseIf String.IsNullOrEmpty(Me.tb_nilai.Text) Or Me.tb_nilai.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nilai pembayaran terlebih dahulu"
            ElseIf Me.dd_jenis_pembayaran.SelectedValue = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi jenis pembayaran terlebih dahulu"
            Else

                sqlcom = "select * from pembayaran_invoice_supplier_tt"
                sqlcom = sqlcom + " where seq_entry_dokumen = " & Me.vseq
                sqlcom = sqlcom + " and seq_pembayaran = " & Me.dd_pembayaran_ke.SelectedValue
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.lbl_msg.Text = "Pembayaran tersebut ke " & Me.dd_pembayaran_ke.SelectedValue & " sudah ada"
                Else
                    Dim vtgl_bayar As String = Me.tb_tgl_bayar.Text.Substring(3, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(0, 2) & "/" & Me.tb_tgl_bayar.Text.Substring(6, 4)
                    Dim vtgl_giro As String = Nothing
                    Dim vtgl_jatuh_tempo As String = Nothing

                    If Not String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                        vtgl_giro = Me.tb_tgl_giro.Text.Substring(3, 2) & "/" & Me.tb_tgl_giro.Text.Substring(0, 2) & "/" & Me.tb_tgl_giro.Text.Substring(6, 4)
                    End If

                    If Not String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                        vtgl_jatuh_tempo = Me.tb_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(6, 4)
                    End If

                    sqlcom = "insert into pembayaran_invoice_supplier_tt(seq_entry_dokumen, seq_pembayaran, jumlah_bayar, is_submit_bayar, tgl_bayar_invoice, id_bank_invoice,"
                    sqlcom = sqlcom + " id_jenis_pembayaran_invoice, no_giro_invoice, tgl_giro_invoice, tgl_jatuh_tempo_invoice, id_transaction_period)"
                    sqlcom = sqlcom + " values(" & Me.vseq & "," & Me.dd_pembayaran_ke.SelectedValue & "," & Decimal.ToDouble(Me.tb_nilai.Text) & ",'B','" & vtgl_bayar & "'," & Me.dd_bank.SelectedValue
                    sqlcom = sqlcom + "," & Me.dd_jenis_pembayaran.SelectedValue & ",'" & Me.tb_no_giro.Text & "',"

                    If String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                        sqlcom = sqlcom + " NULL,"
                    Else
                        sqlcom = sqlcom + "'" & vtgl_giro & "',"
                    End If

                    If String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                        sqlcom = sqlcom + " NULL"
                    Else
                        sqlcom = sqlcom + "'" & vtgl_jatuh_tempo & "'"
                    End If
                    sqlcom = sqlcom + "," & Me.vid_transaction_period & ")"
                    connection.koneksi.InsertRecord(sqlcom)
                    tradingClass.Alert("Data sudah disimpan", Me.Page)
                    Me.loadgrid()
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete pembayaran_invoice_supplier_tt"
                    sqlcom = sqlcom + " where seq_entry_dokumen = " & Me.vseq
                    sqlcom = sqlcom + " and seq_pembayaran = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    tradingClass.Alert("Data sudah dihapus", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                tradingClass.Alert("Data tersebut masih digunakan di form lain", Me.Page)
            Else
                tradingClass.Alert(ex.Message, Me.Page)
            End If
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_bayar"), TextBox).Text) Then
                        Me.lbl_msg.Text = "Silahkan mengisi tanggal pembayaran terlebih dahulu"
                    ElseIf String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_nilai_bayar"), TextBox).Text) Or CType(Me.dg_data.Items(x).FindControl("tb_nilai_bayar"), TextBox).Text = 0 Then
                        Me.lbl_msg.Text = "Silahkan mengisi nilai pembayaran terlebih dahulu"
                    Else
                        Dim vtgl_bayar As String = CType(Me.dg_data.Items(x).FindControl("tb_tgl_bayar"), TextBox).Text
                        vtgl_bayar = vtgl_bayar.Substring(3, 2) & "/" & vtgl_bayar.Substring(0, 2) & "/" & vtgl_bayar.Substring(6, 4)

                        Dim vtgl_giro As String = CType(Me.dg_data.Items(x).FindControl("tb_tgl_giro"), TextBox).Text
                        If Not String.IsNullOrEmpty(vtgl_giro) Then
                            vtgl_giro = vtgl_giro.Substring(3, 2) & "/" & vtgl_giro.Substring(0, 2) & "/" & vtgl_giro.Substring(6, 4)
                        End If


                        Dim vtgl_jatuh_tempo As String = CType(Me.dg_data.Items(x).FindControl("tb_tgl_jatuh_tempo_giro"), TextBox).Text
                        If Not String.IsNullOrEmpty(vtgl_jatuh_tempo) Then
                            vtgl_jatuh_tempo = vtgl_jatuh_tempo.Substring(3, 2) & "/" & vtgl_jatuh_tempo.Substring(0, 2) & "/" & vtgl_jatuh_tempo.Substring(6, 4)
                        End If

                        sqlcom = "update pembayaran_invoice_supplier_tt"
                        sqlcom = sqlcom + " set tgl_bayar_invoice = '" & vtgl_bayar & "',"
                        sqlcom = sqlcom + " jumlah_bayar = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_nilai_bayar"), TextBox).Text) & ","
                        sqlcom = sqlcom + " id_bank_invoice = " & CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue & ","
                        sqlcom = sqlcom + " id_jenis_pembayaran_invoice = " & CType(Me.dg_data.Items(x).FindControl("dd_jenis_bayar"), DropDownList).SelectedValue & ","
                        sqlcom = sqlcom + " no_giro_invoice = '" & CType(Me.dg_data.Items(x).FindControl("tb_no_giro"), TextBox).Text & "',"

                        If String.IsNullOrEmpty(vtgl_giro) Then
                            sqlcom = sqlcom + " tgl_giro_invoice = NULL,"
                        Else
                            sqlcom = sqlcom + " tgl_giro_invoice = '" & vtgl_giro & "',"
                        End If

                        If String.IsNullOrEmpty(vtgl_jatuh_tempo) Then
                            sqlcom = sqlcom + " tgl_jatuh_tempo_invoice = NULL "
                        Else
                            sqlcom = sqlcom + " tgl_jatuh_tempo_invoice = '" & vtgl_jatuh_tempo & "'"
                        End If

                        sqlcom = sqlcom + " where seq_entry_dokumen = " & Me.vseq
                        sqlcom = sqlcom + " and seq_pembayaran = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                        connection.koneksi.UpdateRecord(sqlcom)
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

    Sub jurnal(ByVal vtgl_bayar As String, ByVal nilai_bayar As String, ByVal id_kasbank As Integer)
        Try
            Dim vtgl As String = vtgl_bayar.Substring(3, 2) & "/" & vtgl_bayar.Substring(0, 2) & "/" & vtgl_bayar.Substring(6, 4)

            'debet
            ' akun hutang dagang -> kas/bank

            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_dokumen.Text & "','" & vtgl & "','BYRINVSUPIMP','" & Me.vakun_hutang_dagang & "',"
            sqlcom = sqlcom + "'" & Me.vakun_kasbank & "'," & Decimal.ToDouble(nilai_bayar) * Decimal.ToDouble(Me.lbl_kurs.Text) & ",0, 'Pembayaran invoice supplier impor TT no. " & Me.lbl_no_dokumen.Text & "( Inv. Supplier no. " & Me.lbl_no_invoice.Text & ")'"
            sqlcom = sqlcom + "," & Me.vid_transaction_period & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.lbl_kurs.Text) & "," & Decimal.ToDouble(nilai_bayar) & ",0)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' kas/bank -> akun hutang dagang

            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_dokumen.Text & "','" & vtgl & "','BYRINVSUPIMP','" & Me.vakun_kasbank & "',"
            sqlcom = sqlcom + "'" & Me.vakun_hutang_dagang & "', 0," & Decimal.ToDouble(nilai_bayar) * Decimal.ToDouble(Me.lbl_kurs.Text) & ",'Pembayaran invoice supplier impor TT no. " & Me.lbl_no_dokumen.Text & "( Inv. Supplier no. " & Me.lbl_no_invoice.Text & ")'"
            sqlcom = sqlcom + "," & Me.vid_transaction_period & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.lbl_kurs.Text) & "," & Decimal.ToDouble(nilai_bayar) & ",0)"
            connection.koneksi.InsertRecord(sqlcom)

           

        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub submit(ByVal seq_pembayaran As Integer, ByVal id_bank As Integer)
        Try
            ' cek akun hutang dagang supplier
            sqlcom = "select akun_hutang_dagang from daftar_supplier where id = " & Me.vid_supplier
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_hutang_dagang = reader.Item("akun_hutang_dagang").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_hutang_dagang) Then
                Me.lbl_msg.Text = "Akun hutang dagang pada data supplier tersebut tidak ada"
                Exit Sub
            End If

            ' cek akun kas/bank
            sqlcom = "select account_code from bank_account where id = " & id_bank
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_kasbank = reader.Item("account_code").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_kasbank) Then
                Me.lbl_msg.Text = "Akun pada data kas/bank tersebut tidak ada"
                Exit Sub
            End If

            sqlcom = "update pembayaran_invoice_supplier_tt"
            sqlcom = sqlcom + " set is_submit_bayar = 'S'"
            sqlcom = sqlcom + " where seq_entry_dokumen = " & Me.vseq
            sqlcom = sqlcom + " and seq_pembayaran = " & seq_pembayaran
            connection.koneksi.UpdateRecord(sqlcom)
            Me.loadgrid()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkSubmit" Then
            Me.submit(CType(e.Item.FindControl("lbl_seq"), Label).Text, CType(e.Item.FindControl("lbl_bank"), Label).Text)

            'Daniel
            'Me.jurnal(CType(e.Item.FindControl("tb_tgl_bayar"), TextBox).Text, CType(e.Item.FindControl("tb_nilai_bayar"), TextBox).Text, CType(e.Item.FindControl("lbl_bank"), Label).Text)
            Me.GL(CType(e.Item.FindControl("tb_tgl_bayar"), TextBox).Text, CType(e.Item.FindControl("tb_nilai_bayar"), TextBox).Text, CType(e.Item.FindControl("lbl_bank"), Label).Text)
            'Daniel
        End If
    End Sub
End Class
