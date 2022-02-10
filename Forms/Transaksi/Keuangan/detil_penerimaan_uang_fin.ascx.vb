Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_penerimaan_uang_fin
    Inherits System.Web.UI.UserControl
    'Daniel
    Public tradingClass As New tradingClass()

    Sub GL()

        Try
            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim d_kurs As Decimal = Me.tb_kurs.Text  'System.Convert.ToDecimal(tradingClass.KursBulanan(Me.vid_periode_transaksi))
            Dim dr As SqlClient.SqlDataReader

            Dim keterangan As String = "Transaksi Penerimaan Uang no. " & Me.lbl_no_transaksi.Text
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), id, Me.tradingClass.JurnalType("3"), keterangan, Me.vid_periode)

            If Me.DropDownListJenisTransaksi.SelectedIndex = 1 Then
                sqlcom = "select item_biaya.account_code, isnull(penerimaan_uang_detil.jumlah,0) as jumlah"
                sqlcom = sqlcom + " from penerimaan_uang_detil"
                sqlcom = sqlcom + " inner join item_biaya on item_biaya.id = penerimaan_uang_detil.id_item_pendapatan"
                sqlcom = sqlcom + " where id_pendapatan = " & Me.id_transaksi
            Else
                sqlcom = "select item_pendapatan.account_code, isnull(penerimaan_uang_detil.jumlah,0) as jumlah"
                sqlcom = sqlcom + " from penerimaan_uang_detil"
                sqlcom = sqlcom + " inner join item_pendapatan on item_pendapatan.id = penerimaan_uang_detil.id_item_pendapatan"
                sqlcom = sqlcom + " where id_pendapatan = " & Me.id_transaksi
            End If

            dr = connection.koneksi.SelectRecord(sqlcom)
            Do While dr.Read

                tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), Me.vakun_bank_account, dr.Item("account_code").ToString(), System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * d_kurs, 0, keterangan, Me.vid_periode, Me.dd_mata_uang.SelectedValue, d_kurs, IIf(Me.dd_mata_uang.SelectedValue = "IDR", 0, System.Convert.ToDecimal(dr.Item("jumlah").ToString())), 0, String.Empty)
                tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), dr.Item("account_code").ToString(), Me.vakun_bank_account, 0, System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * d_kurs, keterangan, Me.vid_periode, Me.dd_mata_uang.SelectedValue, d_kurs, 0, IIf(Me.dd_mata_uang.SelectedValue = "IDR", 0, System.Convert.ToDecimal(dr.Item("jumlah").ToString())), String.Empty)

                ' nilai debet
                Me.max_seq_history_kas()
                sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
                sqlcom = sqlcom + " values(" & Me.vid_periode & "," & Me.dd_kas_bank.SelectedValue & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text) & "','"
                sqlcom = sqlcom + keterangan
                sqlcom = sqlcom + "'," & System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * d_kurs & ",0," & Me.vmax_history_kas & ")"
                connection.koneksi.InsertRecord(sqlcom)

                'update kas/bank
                sqlcom = "update bank_account"
                sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) + " & System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * d_kurs
                sqlcom = sqlcom + " where id = " & Me.dd_kas_bank.SelectedValue
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
    Public Property id_user() As Integer
        Get
            Dim o As Object = ViewState("id_user")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_user") = value
        End Set
    End Property

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

    Private ReadOnly Property vid_periode() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_periode")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vid_transaksi() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_transaksi")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property id_transaksi() As Integer
        Get
            Dim o As Object = ViewState("id_transaksi")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_transaksi") = value
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

    Public Property vtotal_pendapatan() As Decimal
        Get
            Dim o As Object = ViewState("vtotal_pendapatan")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vtotal_pendapatan") = value
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

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode_transaksi()
        sqlcom = "select name from transaction_period where id = " & Me.vid_periode
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_periode_transaksi.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindjenis_pembayaran()
        sqlcom = "select id, name from jenis_pembayaran"
        sqlcom = sqlcom + " where id <> 5"
        sqlcom = sqlcom + " order by id"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_jenis_penerimaan.DataSource = reader
        Me.dd_jenis_penerimaan.DataTextField = "name"
        Me.dd_jenis_penerimaan.DataValueField = "id"
        Me.dd_jenis_penerimaan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindkas_bank()
        sqlcom = "select id, name from bank_account order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_kas_bank.DataSource = reader
        Me.dd_kas_bank.DataTextField = "name"
        Me.dd_kas_bank.DataValueField = "id"
        Me.dd_kas_bank.DataBind()
        Me.dd_kas_bank.Items.Add(New ListItem("--No. rekening--", "0"))
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

    Sub bind_item_penerimaan_piutang_karyawan()

        sqlcom = "Select autocoa,InaName FROM COA_list where InaName like upper('%piutang karyawan %')"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_pendapatan.DataSource = reader
        Me.dd_pendapatan.DataTextField = "InaName"
        Me.dd_pendapatan.DataValueField = "autocoa"
        Me.dd_pendapatan.DataBind()
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bind_item_penerimaan_hutang()
        sqlcom = "Select autocoa,InaName FROM COA_list where  InaName like upper('%Hutang %')"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_pendapatan.DataSource = reader
        Me.dd_pendapatan.DataTextField = "InaName"
        Me.dd_pendapatan.DataValueField = "autocoa"
        Me.dd_pendapatan.DataBind()
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub binditem_pendapatan()
        sqlcom = "select id, name from item_pendapatan order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_pendapatan.DataSource = reader
        Me.dd_pendapatan.DataTextField = "name"
        Me.dd_pendapatan.DataValueField = "id"
        Me.dd_pendapatan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    'Daniel
    Sub binditem_biaya()
        sqlcom = "select id, name from item_biaya order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_pendapatan.DataSource = reader
        Me.dd_pendapatan.DataTextField = "name"
        Me.dd_pendapatan.DataValueField = "id"
        Me.dd_pendapatan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub
    'Daniel

    Sub loaddata()
        Try
            If Me.vid_transaksi <> 0 Then
                Me.id_transaksi = Me.vid_transaksi
            End If

            sqlcom = "select id, convert(char, tanggal, 103) as tanggal, id_currency, id_cash_bank, isnull(nilai,0) as nilai, keterangan,"
            sqlcom = sqlcom + " no_cek_giro, convert(char, tgl_cek_giro, 103) as tgl_cek_giro, convert(char, tgl_transaksi, 103) as tgl_transaksi,"
            sqlcom = sqlcom + " convert(char, tgl_jatuh_tempo_cek_giro, 103) as tgl_jatuh_tempo_cek_giro,"
            sqlcom = sqlcom + " id_jenis_pembayaran, is_submit, isnull(kurs,0) as kurs"
            'Daniel
            sqlcom = sqlcom + " ,jenis_transaksi"
            'Daniel
            sqlcom = sqlcom + " from penerimaan_uang"
            sqlcom = sqlcom + " where id = " & Me.id_transaksi
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_transaksi.Text = reader.Item("id").ToString
                Me.lbl_tanggal.Text = reader.Item("tanggal").ToString
                Me.tb_tgl_transaksi.Text = reader.Item("tgl_transaksi").ToString
                Me.dd_jenis_penerimaan.Text = reader.Item("id_jenis_pembayaran").ToString
                Me.dd_mata_uang.SelectedValue = reader.Item("id_currency").ToString
                Me.tb_kurs.Text = FormatNumber(reader.Item("kurs").ToString, 2)
                If String.IsNullOrEmpty(reader.Item("id_cash_bank").ToString) Then
                    Me.dd_kas_bank.SelectedValue = 0
                Else
                    Me.dd_kas_bank.Text = reader.Item("id_cash_bank").ToString
                End If

                'Daniel
                Me.DropDownListJenisTransaksi.SelectedValue = reader.Item("jenis_transaksi").ToString
                Me.DropDownListJenisTransaksi.Enabled = False
                'Daniel

                Me.tb_no_cek_giro.Text = reader.Item("no_cek_giro").ToString
                Me.tb_tgl_cek_giro.Text = reader.Item("tgl_cek_giro").ToString
                Me.tb_tgl_jatuh_tempo.Text = reader.Item("tgl_jatuh_tempo_cek_giro").ToString
                Me.tb_nilai.Text = FormatNumber(reader.Item("nilai").ToString, 3)
                Me.tb_keterangan.Text = reader.Item("keterangan").ToString
                Me.tbl_item_pendapatan.Visible = True

                If reader.Item("is_submit").ToString = "S" Then
                    Me.btn_save.Enabled = False
                    Me.btn_submit.Enabled = False
                    Me.btn_add.Enabled = False
                    Me.btn_update.Enabled = False
                    Me.btn_delete.Enabled = False
                ElseIf reader.Item("is_submit").ToString = "B" Then
                    Me.btn_save.Enabled = True
                    Me.btn_submit.Enabled = True
                    Me.btn_add.Enabled = True
                    Me.btn_update.Enabled = True
                    Me.btn_delete.Enabled = True
                End If
            Else
                Me.btn_submit.Enabled = False
                Me.tbl_item_pendapatan.Visible = False
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.id_user = HttpContext.Current.Session("UserID")
            Me.bindperiode_transaksi()
            Me.bindjenis_pembayaran()
            Me.bindkas_bank()
            Me.bindmata_uang()
            Me.dd_jenis_penerimaan.SelectedValue = "0"
            Me.rekening()
            Me.loaddata()
            Me.loadgrid()
            'Daniel
            If Me.DropDownListJenisTransaksi.SelectedIndex = 1 Then
                Me.binditem_biaya()

            ElseIf Me.DropDownListJenisTransaksi.SelectedIndex = 3 Then
                Me.bind_item_penerimaan_piutang_karyawan()

            ElseIf Me.DropDownListJenisTransaksi.SelectedIndex = 4 Then
                Me.bind_item_penerimaan_hutang()
            Else
                Me.binditem_pendapatan()

            End If
            'Daniel    
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        'Response.Redirect("~/daftar_transaksi_penerimaan_uang.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
        Response.Redirect("~/daftar_transaksi_penerimaan_uang_fin.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.dd_jenis_penerimaan.SelectedValue <> 1 And Me.dd_kas_bank.SelectedValue = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama kas/bank terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_transaksi.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal transaksi terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_kurs.Text) Or Me.tb_kurs.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nilai kurs terlebih dahulu"
            ElseIf (Me.dd_jenis_penerimaan.SelectedValue = 2 Or Me.dd_jenis_penerimaan.SelectedValue = 3) And String.IsNullOrEmpty(Me.tb_no_cek_giro.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi no. cek/giro terlebih dahulu"
            ElseIf (Me.dd_jenis_penerimaan.SelectedValue = 2 Or Me.dd_jenis_penerimaan.SelectedValue = 3) And String.IsNullOrEmpty(Me.tb_tgl_cek_giro.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tgl. cek/giro terlebih dahulu"
            ElseIf (Me.dd_jenis_penerimaan.SelectedValue = 2 Or Me.dd_jenis_penerimaan.SelectedValue = 3) And String.IsNullOrEmpty(Me.tb_tgl_jatuh_tempo.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tgl. jatuh tempo cek/giro terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_nilai.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah nilai terlebih dahulu"
            ElseIf Not String.IsNullOrEmpty(Me.tb_nilai.Text) And Decimal.ToDouble(Me.tb_nilai.Text) <= 0 Then
                Me.lbl_msg.Text = "Jumlah nilai tidak boleh lebih kecil atau sama dengan Nol"
            ElseIf String.IsNullOrEmpty(Me.tb_keterangan.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi keterangan terlebih dahulu"
            Else
                Dim vtgl_transaksi As String = ""
                Dim vtgl_cek_giro As String = ""
                Dim vtgl_jatuh_tempo_giro_cek As String = ""

                If Not String.IsNullOrEmpty(Me.tb_tgl_transaksi.Text) Then
                    vtgl_transaksi = Me.tb_tgl_transaksi.Text.Substring(3, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(0, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(6, 4)
                End If

                If Not String.IsNullOrEmpty(Me.tb_tgl_cek_giro.Text) Then
                    vtgl_cek_giro = Me.tb_tgl_cek_giro.Text.Substring(3, 2) & "/" & Me.tb_tgl_cek_giro.Text.Substring(0, 2) & "/" & Me.tb_tgl_cek_giro.Text.Substring(6, 4)
                End If

                If Not String.IsNullOrEmpty(Me.tb_tgl_jatuh_tempo.Text) Then
                    vtgl_jatuh_tempo_giro_cek = Me.tb_tgl_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(6, 4)
                End If

                If Me.id_transaksi = 0 Then
                    Dim vtgl As String = Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)

                    Dim vmax As String = ""
                    sqlcom = "select isnull(max(convert(int, right(id, 5))),0) + 1 as vid"
                    sqlcom = sqlcom + " from penerimaan_uang"
                    sqlcom = sqlcom + " where convert(int, substring(convert(char, id), 3,2)) = " & Me.vbulan
                    sqlcom = sqlcom + " and convert(int, left(id, 2)) = " & Right(Me.vtahun, 2)
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = Right(Me.vtahun, 2) & Me.vbulan.ToString.PadLeft(2, "0") & reader.Item("vid").ToString.PadLeft(5, "0")
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()


                    sqlcom = "insert into penerimaan_uang(id, tanggal, id_cash_bank, id_transaction_period, nilai, keterangan, tgl_transaksi,"
                    'Daniel
                    sqlcom = sqlcom + " dibuat_oleh, no_cek_giro, tgl_cek_giro, tgl_jatuh_tempo_cek_giro, id_currency, id_jenis_pembayaran, is_submit, kurs,jenis_transaksi)"
                    'Daniel
                    sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "'"

                    If Me.dd_kas_bank.SelectedValue = "0" Then
                        sqlcom = sqlcom + ", NULL"
                    Else
                        sqlcom = sqlcom + "," & Me.dd_kas_bank.SelectedValue
                    End If

                    sqlcom = sqlcom + "," & Me.vid_periode & "," & Decimal.ToDouble(Me.tb_nilai.Text) & ",'" & Me.tb_keterangan.Text & "','" & vtgl_transaksi & "'"
                    sqlcom = sqlcom + ",'1','" & Me.tb_no_cek_giro.Text & "',"

                    If String.IsNullOrEmpty(Me.tb_tgl_cek_giro.Text) Then
                        sqlcom = sqlcom + " NULL,"
                    Else
                        sqlcom = sqlcom + "'" & vtgl_cek_giro & "',"
                    End If

                    If String.IsNullOrEmpty(Me.tb_tgl_jatuh_tempo.Text) Then
                        sqlcom = sqlcom + " NULL,"
                    Else
                        sqlcom = sqlcom + "'" & vtgl_jatuh_tempo_giro_cek & "',"
                    End If
                    'Daniel
                    sqlcom = sqlcom + "'" & Me.dd_mata_uang.SelectedValue & "'," & Me.dd_jenis_penerimaan.SelectedValue & ",'B'," & Decimal.ToDouble(Me.tb_kurs.Text) & ",'" & Me.DropDownListJenisTransaksi.SelectedValue & "')"
                    'Daniel
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.id_transaksi = vmax
                    tradingClass.Alert("Data sudah disimpan", Me.Page)
                Else
                    sqlcom = " update penerimaan_uang"
                    sqlcom = sqlcom + " set tgl_transaksi = '" & vtgl_transaksi & "',"

                    If Me.dd_kas_bank.SelectedValue = "0" Then
                        sqlcom = sqlcom + " id_cash_bank = NULL,"
                    Else
                        sqlcom = sqlcom + " id_cash_bank = " & Me.dd_kas_bank.SelectedValue & ","
                    End If

                    sqlcom = sqlcom + " kurs = " & Decimal.ToDouble(Me.tb_kurs.Text) & ","
                    sqlcom = sqlcom + " nilai = " & Decimal.ToDouble(Me.tb_nilai.Text) & ","
                    sqlcom = sqlcom + " keterangan = '" & Me.tb_keterangan.Text & "',"
                    sqlcom = sqlcom + " no_cek_giro = '" & Me.tb_no_cek_giro.Text & "',"
                    sqlcom = sqlcom + " tgl_cek_giro = '" & vtgl_cek_giro & "',"
                    sqlcom = sqlcom + " tgl_jatuh_tempo_cek_giro = '" & vtgl_jatuh_tempo_giro_cek & "',"
                    sqlcom = sqlcom + " id_currency = '" & Me.dd_mata_uang.SelectedValue & "',"
                    sqlcom = sqlcom + " id_jenis_pembayaran = " & Me.dd_jenis_penerimaan.SelectedValue
                    'Daniel
                    sqlcom = sqlcom + " ,jenis_transaksi = '" & Me.DropDownListJenisTransaksi.SelectedValue & "' "
                    'Daniel
                    sqlcom = sqlcom + " where id = " & Me.id_transaksi
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = Nothing
                    tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
                Me.loaddata()

            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub rekening()
        If Me.dd_jenis_penerimaan.SelectedValue = 1 Then
            Me.tb_no_cek_giro.Enabled = False
            Me.tb_tgl_cek_giro.Enabled = False
            Me.tb_tgl_jatuh_tempo.Enabled = False
        ElseIf Me.dd_jenis_penerimaan.SelectedValue = 4 Then
            Me.tb_no_cek_giro.Enabled = False
            Me.tb_tgl_cek_giro.Enabled = False
            Me.tb_tgl_jatuh_tempo.Enabled = False
        Else
            Me.tb_no_cek_giro.Enabled = True
            Me.tb_tgl_cek_giro.Enabled = True
            Me.tb_tgl_jatuh_tempo.Enabled = True
        End If
    End Sub

    Protected Sub dd_jenis_penerimaan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_jenis_penerimaan.SelectedIndexChanged
        Me.rekening()
    End Sub

    Sub loadgrid()
        Try

            Me.tb_jumlah.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            'Daniel
            If Me.DropDownListJenisTransaksi.SelectedIndex = 1 Then
                sqlcom = "select penerimaan_uang_detil.id_pendapatan, penerimaan_uang_detil.id_item_pendapatan, isnull(penerimaan_uang_detil.jumlah,0) as jumlah,"
                sqlcom = sqlcom + " item_biaya.name as nama_item"
                sqlcom = sqlcom + " from penerimaan_uang_detil"
                sqlcom = sqlcom + " inner join item_biaya on item_biaya.id = penerimaan_uang_detil.id_item_pendapatan"
                sqlcom = sqlcom + " where id_pendapatan = " & Me.id_transaksi
                sqlcom = sqlcom + " order by item_biaya.name"
            ElseIf Me.DropDownListJenisTransaksi.SelectedIndex = 3 Then
                sqlcom = "select penerimaan_uang_detil.id_pendapatan,penerimaan_uang_detil.id_item_pendapatan,ISNULL(penerimaan_uang_detil.jumlah,0)  as jumlah,"
                sqlcom = sqlcom + " COA_list.InaName as nama_item"
                sqlcom = sqlcom + " from penerimaan_uang_detil "
                sqlcom = sqlcom + " inner join COA_list  on COA_list.AutoCoa = penerimaan_uang_detil.id_item_pendapatan"
                sqlcom = sqlcom + " where id_pendapatan =  " & Me.id_transaksi
                sqlcom = sqlcom + "order by COA_list.InaName"
            ElseIf Me.DropDownListJenisTransaksi.SelectedIndex = 4 Then
                sqlcom = "select penerimaan_uang_detil.id_pendapatan,penerimaan_uang_detil.id_item_pendapatan,ISNULL(penerimaan_uang_detil.jumlah,0)  as jumlah,"
                sqlcom = sqlcom + " COA_list.InaName as nama_item"
                sqlcom = sqlcom + " from penerimaan_uang_detil "
                sqlcom = sqlcom + " inner join COA_list  on COA_list.AutoCoa = penerimaan_uang_detil.id_item_pendapatan"
                sqlcom = sqlcom + " where id_pendapatan =  " & Me.id_transaksi
                sqlcom = sqlcom + "order by COA_list.InaName"
            Else
                sqlcom = "select penerimaan_uang_detil.id_pendapatan, penerimaan_uang_detil.id_item_pendapatan, isnull(penerimaan_uang_detil.jumlah,0) as jumlah,"
                sqlcom = sqlcom + " item_pendapatan.name as nama_item"
                sqlcom = sqlcom + " from penerimaan_uang_detil"
                sqlcom = sqlcom + " inner join item_pendapatan on item_pendapatan.id = penerimaan_uang_detil.id_item_pendapatan"
                sqlcom = sqlcom + " where id_pendapatan = " & Me.id_transaksi
                sqlcom = sqlcom + " order by item_pendapatan.name"
            End If
            'Daniel

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "penerimaan_uang_detil")
                Me.dg_data.DataSource = ds.Tables("penerimaan_uang_detil").DefaultView

                If ds.Tables("penerimaan_uang_detil").Rows.Count > 0 Then
                    If ds.Tables("penerimaan_uang_detil").Rows.Count > 10 Then
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

            Me.bindtotal()

        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindtotal()
        Try
            sqlcom = "select isnull(sum(isnull(jumlah,0)),0) as total_nilai from penerimaan_uang_detil"
            sqlcom = sqlcom + " where id_pendapatan = " & Me.id_transaksi
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_total_nilai.Text = FormatNumber(reader.Item("total_nilai").ToString, 2)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Try
            If String.IsNullOrEmpty(Me.tb_jumlah.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah nilai terlebih dahulu"
            Else
                sqlcom = "insert into penerimaan_uang_detil(id_pendapatan, id_item_pendapatan, jumlah)"
                sqlcom = sqlcom + " values (" & Me.id_transaksi & "," & Me.dd_pendapatan.SelectedValue & "," & Decimal.ToDouble(Me.tb_jumlah.Text) & ")"
                connection.koneksi.InsertRecord(sqlcom)
                tradingClass.Alert("Data sudah disimpan", Me.Page)
                Me.loadgrid()
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete penerimaan_uang_detil"
                    sqlcom = sqlcom + " where id_pendapatan = " & Me.id_transaksi
                    sqlcom = sqlcom + " and id_item_pendapatan = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_pendapatan"), Label).Text
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

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update penerimaan_uang_detil"
                    sqlcom = sqlcom + " set jumlah = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_jumlah"), TextBox).Text)
                    sqlcom = sqlcom + " where id_pendapatan = " & Me.id_transaksi
                    sqlcom = sqlcom + " and id_item_pendapatan = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_pendapatan"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
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

    Sub jurnal()

        Dim vtgl As Date = Me.tb_tgl_transaksi.Text.Substring(3, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(0, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(6, 4)

        Dim readeritem As SqlClient.SqlDataReader
        sqlcom = "select item_pendapatan.account_code, isnull(penerimaan_uang_detil.jumlah,0) as jumlah"
        sqlcom = sqlcom + " from penerimaan_uang_detil"
        sqlcom = sqlcom + " inner join item_pendapatan on item_pendapatan.id = penerimaan_uang_detil.id_item_pendapatan"
        sqlcom = sqlcom + " where id_pendapatan = " & Me.id_transaksi
        readeritem = connection.koneksi.SelectRecord(sqlcom)
        Do While readeritem.Read
            'debet
            ' biaya
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl & "','TRMUANG','" & readeritem.Item("account_code").ToString & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) * Decimal.ToDouble(Me.tb_kurs.Text) & ",0, 'Transaksi Penerimaan Uang. " & Me.lbl_no_transaksi.Text & " (" & Me.tb_keterangan.Text & ")'"
            sqlcom = sqlcom + "," & Me.vid_periode & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text)

            If Me.dd_mata_uang.SelectedValue = "IDR" Then
                sqlcom = sqlcom + ", 0, 0)"
            Else
                sqlcom = sqlcom + "," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) & ", 0)"
            End If

            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' kas
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl & "','TRMUANG','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & readeritem.Item("account_code").ToString & "',0," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) * Decimal.ToDouble(Me.tb_kurs.Text) & ",'Transaksi Penerimaan Uang. " & Me.lbl_no_transaksi.Text & " (" & Me.tb_keterangan.Text & ")'"
            sqlcom = sqlcom + "," & Me.vid_periode & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text)

            If Me.dd_mata_uang.SelectedValue = "IDR" Then
                sqlcom = sqlcom + ", 0, 0)"
            Else
                sqlcom = sqlcom + ", 0," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) & ")"
            End If

            connection.koneksi.InsertRecord(sqlcom)
        Loop
        readeritem.Close()
        connection.koneksi.CloseKoneksi()

       

    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try

            If Me.tb_tgl_transaksi.Text <> Nothing Then



                'cek akun dan saldo akhir kas/bank
                Dim vsaldo_akhir As Decimal = 0
                sqlcom = "select account_code, isnull(saldo_akhir,0) as saldo_akhir"
                sqlcom = sqlcom + " from bank_account"
                sqlcom = sqlcom + " where id = " & Me.dd_kas_bank.SelectedValue
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

                'cek total biaya
                sqlcom = "select isnull(sum(isnull(jumlah,0)),0) as jumlah_pendapatan"
                sqlcom = sqlcom + " from penerimaan_uang_detil"
                sqlcom = sqlcom + " where id_pendapatan = " & Me.id_transaksi
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.vtotal_pendapatan = reader.Item("jumlah_pendapatan").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                'If Decimal.ToDouble(vsaldo_akhir) < Decimal.ToDouble(Me.vtotal_pendapatan) Then
                'Me.lbl_msg.Text = "Saldo akhir Kas/Bank tersebut tidak mencukupi"
                'Exit Sub
                'End If

                'cek akun item pendapatan
                Dim flag As String = "Y"
                sqlcom = "select item_pendapatan.account_code"
                sqlcom = sqlcom + " from item_pendapatan"
                sqlcom = sqlcom + " inner join penerimaan_uang_detil on penerimaan_uang_detil.id_item_pendapatan = item_pendapatan.id"
                sqlcom = sqlcom + " where id_pendapatan = " & Me.id_transaksi
                reader = connection.koneksi.SelectRecord(sqlcom)
                Do While reader.Read
                    If String.IsNullOrEmpty(reader.Item("account_code").ToString) Then
                        flag = "T"
                        Me.lbl_msg.Text = "Ada kode akun yang tidak ada dari item pendapatan tersebut"
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                        Exit Do
                    End If
                Loop
                reader.Close()
                connection.koneksi.CloseKoneksi()

                If flag = "T" Then
                    Exit Sub
                End If

                sqlcom = "update penerimaan_uang"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where id = " & Me.id_transaksi
                connection.koneksi.UpdateRecord(sqlcom)


                '<--adien 24 des 2013
                'Me.jurnal()
                Me.GL()
                'adien-->

                Me.loaddata()

            Else
                Me.lbl_msg.Text = "Tanggal transaksi masih kosong"
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    'Daniel
    Protected Sub DropDownListJenisTransaksi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownListJenisTransaksi.SelectedIndexChanged
        If Me.DropDownListJenisTransaksi.SelectedIndex = 1 Then
            Me.binditem_biaya()
        ElseIf Me.DropDownListJenisTransaksi.SelectedIndex = 3 Then
            Me.bind_item_penerimaan_piutang_karyawan()
        ElseIf Me.DropDownListJenisTransaksi.SelectedIndex = 4 Then
            Me.bind_item_penerimaan_hutang()
        Else
            Me.binditem_pendapatan()
        End If
    End Sub
    'Daniel

End Class
