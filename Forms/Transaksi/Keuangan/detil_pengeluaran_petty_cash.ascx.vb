Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_pengeluaran_petty_cash
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

    Private ReadOnly Property vid() As Integer
        Get
            Dim o As Object = Request.QueryString("vid")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
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

    Public Property id_transaction_period() As Integer
        Get
            Dim o As Object = ViewState("id_transaction_period")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_transaction_period") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiodepembayaran()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where tahun = " & Me.vtahun
        sqlcom = sqlcom + " and bulan = " & Me.vbulan
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.id_transaction_period = reader.Item("id").ToString
            Me.lbl_periode.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindcashaccount_untuk()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from bank_account"
        sqlcom = sqlcom + " where is_bank = 'T'"
        sqlcom = sqlcom + " and is_petty_cash = 'Y'"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_kas_untuk.DataSource = reader
        Me.dd_kas_untuk.DataTextField = "name"
        Me.dd_kas_untuk.DataValueField = "id"
        Me.dd_kas_untuk.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindbankaccount_dari()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from bank_account"
        'sqlcom = sqlcom + " where is_bank = 'Y'"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_kas_dari.DataSource = reader
        Me.dd_kas_dari.DataTextField = "name"
        Me.dd_kas_dari.DataValueField = "id"
        Me.dd_kas_dari.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loaddata()
        Try
            sqlcom = "select id, convert(char, tanggal, 103) as tanggal, id_currency,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when is_cash = 'T' then 'Tunai'"
            sqlcom = sqlcom + " when is_cash = 'N' then 'Non tunai'"
            sqlcom = sqlcom + " end as is_cash,"
            sqlcom = sqlcom + " isnull(nilai,0) as nilai, keterangan, diminta_oleh, id_petty_cash, id_cash_bank, tanggal_pengeluaran, no_cek_giro,"
            sqlcom = sqlcom + " convert(char, tgl_cek_giro, 103) as tgl_cek_giro, is_submit_keuangan, isnull(jumlah_dikeluarkan,0) as jumlah_dikeluarkan"
            sqlcom = sqlcom + " from permintaan_petty_cash"
            sqlcom = sqlcom + " where id = " & Me.vid
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_permintaan.Text = reader.Item("id").ToString
                Me.lbl_tgl_permintaan.Text = reader.Item("tanggal").ToString
                Me.lbl_mata_uang.Text = reader.Item("id_currency").ToString
                Me.lbl_tunai_non_tunai.Text = reader.Item("is_cash").ToString
                Me.dd_kas_untuk.SelectedValue = reader.Item("id_petty_cash").ToString
                Me.dd_kas_dari.SelectedValue = reader.Item("id_cash_bank").ToString
                Me.lbl_jumlah.Text = FormatNumber(reader.Item("nilai").ToString, 2)
                Me.lbl_keterangan.Text = reader.Item("keterangan").ToString
                Me.tb_no_cek_giro.Text = reader.Item("no_cek_giro").ToString

                If String.IsNullOrEmpty(reader.Item("tgl_cek_giro").ToString) Then
                    Me.tb_tgl_cek_giro.Text = ""
                Else
                    Me.tb_tgl_cek_giro.Text = reader.Item("tgl_cek_giro").ToString
                End If

                Me.tb_jumlah_dikeluarkan.Text = FormatNumber(reader.Item("jumlah_dikeluarkan"), 2)

                If reader.Item("is_submit_keuangan").ToString = "S" Then
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                Else
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try       
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.bindperiodepembayaran()
            Me.bindcashaccount_untuk()
            Me.bindbankaccount_dari()
            Me.tb_tgl_dikeluarkan.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
            Me.loaddata()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/petty_cash_request_list.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
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

    Sub max_seq_history_kas()
        sqlcom = "select isnull(max(seq),0) + 1 as vmax from history_kas"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.vmax_history_kas = reader.Item("vmax").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub insert_history_kas()        

        Dim vtgl As String = Me.lbl_tgl_permintaan.Text.Substring(3, 2) & "/" & Me.lbl_tgl_permintaan.Text.Substring(0, 2) & "/" & Me.lbl_tgl_permintaan.Text.Substring(6, 4)

        ' nilai debet
        Me.max_seq_history_kas()
        sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
        sqlcom = sqlcom + " values(" & Me.id_transaction_period & "," & Me.dd_kas_untuk.SelectedValue & ",'" & vtgl & "',"
        sqlcom = sqlcom + "'" & Me.lbl_keterangan.Text & " (Permintaan Petty Cash. " & Me.lbl_no_permintaan.Text & ")'"
        sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_jumlah_dikeluarkan.Text) & ",0," & Me.vmax_history_kas & ")"
        connection.koneksi.InsertRecord(sqlcom)

        ' nilai kredit
        Me.max_seq_history_kas()
        sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
        sqlcom = sqlcom + " values(" & Me.id_transaction_period & "," & Me.dd_kas_dari.SelectedValue & ",'" & vtgl & "',"
        sqlcom = sqlcom + "'" & Me.lbl_keterangan.Text & " (Permintaan Petty Cash. " & Me.lbl_no_permintaan.Text & ")'"
        sqlcom = sqlcom + ",0," & Decimal.ToDouble(Me.tb_jumlah_dikeluarkan.Text) & "," & Me.vmax_history_kas & ")"
        connection.koneksi.InsertRecord(sqlcom)
    End Sub

    Sub update_kas()
        'kas tujuan
        sqlcom = "update bank_account"
        sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) + " & Decimal.ToDouble(Me.tb_jumlah_dikeluarkan.Text)
        sqlcom = sqlcom + " where id = " & Me.dd_kas_untuk.SelectedValue
        connection.koneksi.UpdateRecord(sqlcom)

        'kas asal
        sqlcom = "update bank_account"
        sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - " & Decimal.ToDouble(Me.tb_jumlah_dikeluarkan.Text)
        sqlcom = sqlcom + " where id = " & Me.dd_kas_dari.SelectedValue
        connection.koneksi.UpdateRecord(sqlcom)
    End Sub

    Sub jurnal()
        Dim vakun_kas_asal As String = ""
        Dim vakun_kas_tujuan As String = ""

        'akun kas tujuan
        sqlcom = "select account_code"
        sqlcom = sqlcom + " from bank_account"
        sqlcom = sqlcom + " where id = " & Me.dd_kas_dari.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            vakun_kas_asal = reader.Item("account_code").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        'akun kas tujuan
        sqlcom = "select account_code"
        sqlcom = sqlcom + " from bank_account"
        sqlcom = sqlcom + " where id = " & Me.dd_kas_untuk.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            vakun_kas_tujuan = reader.Item("account_code").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()


        Dim vtgl As String = Me.lbl_tgl_permintaan.Text.Substring(3, 2) & "/" & Me.lbl_tgl_permintaan.Text.Substring(0, 2) & "/" & Me.lbl_tgl_permintaan.Text.Substring(6, 4)

        'debet
        ' kas tujuan kas asal
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_permintaan.Text & "','" & vtgl & "','KLRPETTY','" & vakun_kas_tujuan & "',"
        sqlcom = sqlcom + "'" & vakun_kas_asal & "'," & Decimal.ToDouble(Me.tb_jumlah_dikeluarkan.Text) & ",0,'" & Me.lbl_keterangan.Text & " (Permintaan Petty Cash. " & Me.lbl_no_permintaan.Text & ")'"
        sqlcom = sqlcom + "," & Me.id_transaction_period & ",'" & Me.lbl_mata_uang.Text & "',1)"
        connection.koneksi.InsertRecord(sqlcom)

        'kredit
        ' kas asal kas tujuan
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_permintaan.Text & "','" & vtgl & "','KLRPETTY','" & vakun_kas_asal & "',"
        sqlcom = sqlcom + "'" & vakun_kas_tujuan & "',0," & Decimal.ToDouble(Me.tb_jumlah_dikeluarkan.Text) & ",'" & Me.lbl_keterangan.Text & " (Permintaan Petty Cash. " & Me.lbl_no_permintaan.Text & ")'"
        sqlcom = sqlcom + "," & Me.id_transaction_period & ",'" & Me.lbl_mata_uang.Text & "',1)"
        connection.koneksi.InsertRecord(sqlcom)

    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_jumlah_dikeluarkan.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah yang dikeluarkan terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_dikeluarkan.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal dikeluarkan terlebih dahulu"
            Else
                Dim vtgl_cek_giro As String = Nothing
                If Not String.IsNullOrEmpty(Me.tb_tgl_cek_giro.Text) Then
                    vtgl_cek_giro = Me.tb_tgl_cek_giro.Text.Substring(3, 2) & "/" & Me.tb_tgl_cek_giro.Text.Substring(0, 2) & "/" & Me.tb_tgl_cek_giro.Text.Substring(6, 4)
                End If

                Dim vtgl_dikeluarkan As String = Me.tb_tgl_dikeluarkan.Text.Substring(3, 2) & "/" & Me.tb_tgl_dikeluarkan.Text.Substring(0, 2) & "/" & Me.tb_tgl_dikeluarkan.Text.Substring(6, 4)

                sqlcom = "update permintaan_petty_cash"
                sqlcom = sqlcom + " set id_petty_cash = " & Me.dd_kas_untuk.SelectedValue & ","
                sqlcom = sqlcom + " id_cash_bank = " & Me.dd_kas_dari.SelectedValue & ","
                sqlcom = sqlcom + " no_cek_giro = '" & Me.tb_no_cek_giro.Text & "',"

                If String.IsNullOrEmpty(Me.tb_tgl_cek_giro.Text) Then
                    sqlcom = sqlcom + " tgl_cek_giro = NULL,"
                Else
                    sqlcom = sqlcom + " tgl_cek_giro = '" & vtgl_cek_giro & "',"
                End If

                sqlcom = sqlcom + " jumlah_dikeluarkan = " & Decimal.ToDouble(Me.tb_jumlah_dikeluarkan.Text) & ","
                sqlcom = sqlcom + " tanggal_pengeluaran = '" & vtgl_dikeluarkan & "'"
                sqlcom = sqlcom + " where id = " & Me.vid
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            sqlcom = "select tanggal_pengeluaran"
            sqlcom = sqlcom + " from permintaan_petty_cash"
            sqlcom = sqlcom + " where id = " & Me.vid
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If String.IsNullOrEmpty(reader.Item("tanggal_pengeluaran").ToString) Then
                    Me.lbl_msg.Text = "Data tersebut belum disimpan"
                Else
                    'cek saldo
                    Dim vsaldo_akhir As Decimal = 0
                    sqlcom = "select isnull(saldo_akhir,0) as saldo_akhir"
                    sqlcom = sqlcom + " from bank_account"
                    sqlcom = sqlcom + " where id = " & Me.dd_kas_dari.SelectedValue
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vsaldo_akhir = reader.Item("saldo_akhir").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    If Decimal.ToDouble(vsaldo_akhir) < Decimal.ToDouble(Me.tb_jumlah_dikeluarkan.Text) Then
                        Me.lbl_msg.Text = "Saldo akhir dari Kas/Bank asal tersebut tidak mencukupi"
                    Else
                        sqlcom = "update permintaan_petty_cash"
                        sqlcom = sqlcom + " set is_submit_keuangan = 'S', status = 'O'"
                        sqlcom = sqlcom + " where id = " & Me.vid
                        connection.koneksi.UpdateRecord(sqlcom)
                        Me.lbl_msg.Text = "Data sudah disubmit dan tidak bisa diubah kembali"
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                        'Me.jurnal()
                        Me.insert_history_kas()
                        Me.update_kas()
                        Me.loaddata()
                    End If
                End If
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
