Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_hutang_bank
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

    Private ReadOnly Property vno_transaksi() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_transaksi")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property no_transaksi() As Integer
        Get
            Dim o As Object = ViewState("no_transaksi")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("no_transaksi") = value
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


    Public Property vakun_loan_idr() As String
        Get
            Dim o As Object = ViewState("vakun_loan_idr")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_loan_idr") = value
        End Set
    End Property


    Public Property vakun_loan_usd() As String
        Get
            Dim o As Object = ViewState("vakun_loan_usd")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_loan_usd") = value
        End Set
    End Property

    Public Property vakun_loan() As String
        Get
            Dim o As Object = ViewState("vakun_loan")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_loan") = value
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
            Me.lbl_periode_transaksi.Text = reader.Item("name").ToString
            Me.vid_periode_transaksi = reader.Item("id").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindrekening()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from bank_account"
        sqlcom = sqlcom + " where id in (5,6)"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_no_rekening.DataSource = reader
        Me.dd_no_rekening.DataTextField = "name"
        Me.dd_no_rekening.DataValueField = "id"
        Me.dd_no_rekening.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindbank()
        Dim readerbank As SqlClient.SqlDataReader
        sqlcom = "select name from bank_list"
        sqlcom = sqlcom + " where id = (select id_bank from bank_account where id = " & Me.dd_no_rekening.SelectedValue & ")"
        readerbank = connection.koneksi.SelectRecord(sqlcom)
        readerbank.Read()
        If readerbank.HasRows Then
            Me.lbl_nama_bank.Text = readerbank.Item("name").ToString
        Else
            Me.lbl_nama_bank.Text = "-"
        End If
        readerbank.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindmata_uang()
        Dim readermata_uang As SqlClient.SqlDataReader
        sqlcom = "select id_currency from bank_account where id = " & Me.dd_no_rekening.SelectedValue
        readermata_uang = connection.koneksi.SelectRecord(sqlcom)
        readermata_uang.Read()
        If readermata_uang.HasRows Then
            Me.lbl_mata_uang.Text = readermata_uang("id_currency").ToString
        End If
        readermata_uang.Close()
        connection.koneksi.CloseKoneksi()
    End Sub


    Sub loaddata()
        Try
            If Me.vno_transaksi <> 0 Then
                Me.no_transaksi = Me.vno_transaksi
            End If

            sqlcom = "select seq, convert(char, tanggal, 103) as tanggal, convert(char, tanggal_terima, 103) as tgl_terima, id_bank_account,"
            sqlcom = sqlcom + " isnull(jumlah,0) as jumlah, is_submit, keterangan"
            sqlcom = sqlcom + " from hutang_bank"
            sqlcom = sqlcom + " where seq = " & Me.no_transaksi
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_transaksi.Text = reader.Item("seq").ToString
                Me.tb_tanggal.Text = reader.Item("tanggal").ToString
                Me.tb_tgl_terima.Text = reader.Item("tgl_terima").ToString
                Me.dd_no_rekening.SelectedValue = reader.Item("id_bank_account").ToString
                Me.tb_jumlah.Text = FormatNumber(reader.Item("jumlah").ToString, 2)
                Me.tb_keterangan.Text = reader.Item("keterangan").ToString

                If reader.Item("is_submit").ToString = "B" Then
                    Me.lbl_status_submit.Text = "Belum disubmit"
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                Else
                    Me.lbl_status_submit.Text = "Sudah disubmit"
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                End If

                Me.bindbank()
                Me.bindmata_uang()
            Else
                Me.btn_submit.Enabled = False
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.bindperiodetransaksi()
            Me.bindrekening()
            Me.bindbank()
            Me.bindmata_uang()
            Me.tb_tanggal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.loaddata()
        End If
    End Sub


    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/penerimaan_hutang_bank.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tanggal.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_terima.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terima terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_jumlah.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah terlebih dahulu"
            Else
                Dim vtgl As String = Me.tb_tanggal.Text.Substring(3, 2) & "/" & Me.tb_tanggal.Text.Substring(0, 2) & "/" & Me.tb_tanggal.Text.Substring(6, 4)
                Dim vtgl_terima As String = Me.tb_tgl_terima.Text.Substring(3, 2) & "/" & Me.tb_tgl_terima.Text.Substring(0, 2) & "/" & Me.tb_tgl_terima.Text.Substring(6, 4)

                If Me.no_transaksi = 0 Then

                    Dim vmax As Integer = 0

                    sqlcom = "select isnull(max(convert(int, right(seq,3))),0) + 1 as vmax"
                    sqlcom = sqlcom + " from hutang_bank"
                    sqlcom = sqlcom + " where substring(convert(char,seq),1,2) = right(" & Me.vtahun & ",2)"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = Right(Me.vtahun.ToString, 2) & vbulan.ToString.PadLeft(2, "0") & reader.Item("vmax").ToString.PadLeft(3, "0")
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()


                    sqlcom = "insert into hutang_bank(seq, tanggal, tanggal_terima, id_bank_account, jumlah, is_submit, id_transaction_period, keterangan)"
                    sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "','" & vtgl_terima & "','" & Me.dd_no_rekening.SelectedValue & "'," & Decimal.ToDouble(Me.tb_jumlah.Text)
                    sqlcom = sqlcom + ",'B'," & Me.vid_periode_transaksi & ",'" & Me.tb_keterangan.Text & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.no_transaksi = vmax
                    tradingClass.Alert("Data sudah disimpan", Me.Page)
                Else
                    sqlcom = "update hutang_bank"
                    sqlcom = sqlcom + " set tanggal = '" & vtgl & "',"
                    sqlcom = sqlcom + " tanggal_terima = '" & vtgl_terima & "',"
                    sqlcom = sqlcom + " id_bank_account = '" & Me.dd_no_rekening.SelectedValue & "',"
                    sqlcom = sqlcom + " jumlah = " & Decimal.ToDouble(Me.tb_jumlah.Text) & ","
                    sqlcom = sqlcom + " keterangan = '" & Me.tb_keterangan.Text & "'"
                    sqlcom = sqlcom + " where seq = " & Me.no_transaksi
                    connection.koneksi.UpdateRecord(sqlcom)
                    tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If

                Me.loaddata()
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub dd_no_rekening_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_no_rekening.SelectedIndexChanged
        Me.bindbank()
        Me.bindmata_uang()
    End Sub


    Sub seq_max()
        Try
            sqlcom = "select isnull(max(seq),0) + 1 as vmax from akun_general_ledger"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vmax = reader.Item("vmax").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub max_seq_history_kas()
        Try
            sqlcom = "select isnull(max(seq),0) + 1 as vmax from history_kas"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vmax_history_kas = reader.Item("vmax").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try

            Me.vakun_loan_idr = "21.02.01.01"
            Me.vakun_loan_usd = "21.02.01.02"

            If Me.lbl_mata_uang.Text = "IDR" Then
                Me.vakun_loan = Me.vakun_loan_idr
            Else
                Me.vakun_loan = Me.vakun_loan_usd
            End If

            'akun kas bank
            sqlcom = "select account_code from bank_account"
            sqlcom = sqlcom + " where id = " & Me.dd_no_rekening.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_kasbank = reader.Item("account_code").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_kasbank) Then
                Me.lbl_msg.Text = "Akun kas/bank tersebut tidak ada"
                Exit Sub
            End If

            Dim vtgl As String = Me.tb_tgl_terima.Text
            vtgl = vtgl.Substring(3, 2) & "/" & vtgl.Substring(0, 2) & "/" & vtgl.Substring(6, 4)

            'Daniel
            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim d_kurs As Decimal = System.Convert.ToDecimal(tradingClass.KursBulanan("[kurs_bulanan]", Me.vid_periode_transaksi))
            Dim keterangan As String = "Penerimaan hutang bank no." & Me.no_transaksi

            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_terima.Text), id, Me.tradingClass.JurnalType("3"), keterangan, Me.vid_periode_transaksi)

            tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_terima.Text), Me.vakun_kasbank, Me.vakun_loan, System.Convert.ToDecimal(Me.tb_jumlah.Text) * d_kurs, 0, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, d_kurs, IIf(Me.lbl_mata_uang.Text = "IDR", 0, System.Convert.ToDecimal(Me.tb_jumlah.Text)), 0, String.Empty)
            tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_terima.Text), Me.vakun_loan, Me.vakun_kasbank, 0, System.Convert.ToDecimal(Me.tb_jumlah.Text) * d_kurs, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, d_kurs, 0, IIf(Me.lbl_mata_uang.Text = "IDR", 0, System.Convert.ToDecimal(Me.tb_jumlah.Text)), String.Empty)


            ''debet
            'Me.seq_max()
            'sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            'sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            'sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.no_transaksi & "','" & vtgl & "','TRMLOANB','" & Me.vakun_kasbank & "',"
            'sqlcom = sqlcom + "'" & Me.vakun_loan & "'," & Decimal.ToDouble(Me.tb_jumlah.Text) & ",0, 'Penerimaan hutang bank no." & Me.no_transaksi & "'"
            'sqlcom = sqlcom + "," & vid_periode_transaksi & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(0)
            'sqlcom = sqlcom + ", 0, 0)"
            'connection.koneksi.InsertRecord(sqlcom)

            ''kredit
            'Me.seq_max()
            'sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            'sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            'sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.no_transaksi & "','" & vtgl & "','TRMLOANB','" & Me.vakun_loan & "',"
            'sqlcom = sqlcom + "'" & Me.vakun_kasbank & "',0," & Decimal.ToDouble(Me.tb_jumlah.Text) & ", 'Penerimaan hutang bank no." & Me.no_transaksi & "'"
            'sqlcom = sqlcom + "," & vid_periode_transaksi & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(0)
            'sqlcom = sqlcom + ", 0, 0)"
            'connection.koneksi.InsertRecord(sqlcom)


            ' insert history kas
            Me.max_seq_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
            sqlcom = sqlcom + " values(" & vid_periode_transaksi & "," & Me.dd_no_rekening.SelectedValue & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_terima.Text) & "','"
            sqlcom = sqlcom + keterangan
            sqlcom = sqlcom + "'," & System.Convert.ToDecimal(Me.tb_jumlah.Text) * d_kurs & ",0," & Me.vmax_history_kas & ")"
            connection.koneksi.InsertRecord(sqlcom)


            'update kas
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) + " & System.Convert.ToDecimal(Me.tb_jumlah.Text) * d_kurs
            sqlcom = sqlcom + " where id = " & Me.dd_no_rekening.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)
            'Daniel
            'update status submit

            sqlcom = "update hutang_bank set is_submit = 'S'"
            sqlcom = sqlcom + " where seq = " & Me.no_transaksi
            connection.koneksi.UpdateRecord(sqlcom)

            tradingClass.Alert("Data sudah disubmit!", Me.Page)
            Me.loaddata()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

End Class

