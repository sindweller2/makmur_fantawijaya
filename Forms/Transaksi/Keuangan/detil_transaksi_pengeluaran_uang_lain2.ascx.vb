Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_transaksi_pengeluaran_uang_lain2
    Inherits System.Web.UI.UserControl
    'Daniel
    Public tradingClass As New tradingClass()

    Sub GL()

        Try
            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim d_kurs As Decimal = Me.tb_kurs.Text  'System.Convert.ToDecimal(tradingClass.KursBulanan(Me.vid_periode_transaksi))
            Dim dr As SqlClient.SqlDataReader

            Dim keterangan As String = "Transaksi Pengeluaran Uang no. " & Me.lbl_no_transaksi.Text
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), id, Me.tradingClass.JurnalType("4"), keterangan, Me.vid_periode_transaksi)

            dr = connection.koneksi.SelectRecord("SELECT id_item_biaya as account_code, isnull(pengeluaran_uang_lain_detil.jumlah,0) as jumlah, keterangan from pengeluaran_uang_lain_detil where id_pengeluaran_lain = " & Me.id_transaksi & "")
            Do While dr.Read

                tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), dr.Item("account_code").ToString(), Me.vakun_bank_account, System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * d_kurs, 0, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, d_kurs, IIf(Me.lbl_mata_uang.Text = "IDR", 0, System.Convert.ToDecimal(dr.Item("jumlah").ToString())), 0, Me.tb_no_voucher.Text.Trim())
                tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), Me.vakun_bank_account, dr.Item("account_code").ToString(), 0, System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * d_kurs, keterangan, Me.vid_periode_transaksi, Me.lbl_mata_uang.Text, d_kurs, 0, IIf(Me.lbl_mata_uang.Text = "IDR", 0, System.Convert.ToDecimal(dr.Item("jumlah").ToString())), Me.tb_no_voucher.Text.Trim())

                ' nilai kredit
                Me.max_seq_history_kas()
                sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq, no_voucher)"
                sqlcom = sqlcom + " values(" & Me.vid_periode_transaksi & "," & Me.dd_kas_petty_cash.SelectedValue & ",'" & Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text) & "','"
                sqlcom = sqlcom + keterangan
                sqlcom = sqlcom + "',0," & System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * d_kurs & "," & Me.vmax_history_kas & ",'" & Me.tb_no_voucher.Text & "')"
                connection.koneksi.InsertRecord(sqlcom)

                'update saldo kas
                sqlcom = "update bank_account"
                sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - isnull(" & System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * d_kurs & ",0)"
                sqlcom = sqlcom + " where id = " & Me.dd_kas_petty_cash.SelectedValue
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

    Private ReadOnly Property vpaging() As Integer
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vid() As Integer
        Get
            Dim o As Object = Request.QueryString("vid")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vpilihan() As String
        Get
            Dim o As Object = Request.QueryString("vpilihan")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property


    Private ReadOnly Property vsearch() As String
        Get
            Dim o As Object = Request.QueryString("vsearch")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property


    Private ReadOnly Property vsearch_tanggal() As String
        Get
            Dim o As Object = Request.QueryString("vsearch_tanggal")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
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

    Public Property vid_periode_transaksi() As Integer
        Get
            Dim o As Object = ViewState("vid_periode_transaksi")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_periode_transaksi") = value
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

    Public Property vtotal_biaya() As Decimal
        Get
            Dim o As Object = ViewState("vtotal_biaya")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vtotal_biaya") = value
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

    Sub bindcashaccount()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from bank_account"
        sqlcom = sqlcom + " where is_petty_cash is null"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_kas_petty_cash.DataSource = reader
        Me.dd_kas_petty_cash.DataTextField = "name"
        Me.dd_kas_petty_cash.DataValueField = "id"
        Me.dd_kas_petty_cash.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearakun()
        Me.tb_id_akun.Text = "0"
        Me.lbl_akun.Text = "------"
        Me.link_popup_akun.Visible = True
    End Sub

    Sub bindakun()
        sqlcom = "select accountno + ' ' + inaname as nama_akun from coa_list where accountno = '" & Me.tb_id_akun.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun.Text = reader.Item("nama_akun").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindmatauang()
        Try
            sqlcom = "select id_currency from bank_account where id = " & Me.dd_kas_petty_cash.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_mata_uang.Text = reader.Item("id_currency").ToString
                If Me.lbl_mata_uang.Text = "USD" Then
                    Me.tb_kurs.Text = tradingClass.KursBulanan("[kurs_bulanan]", Me.vid_periode_transaksi)
                Else
                    Me.tb_kurs.Text = "1.00"
                End If
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clearform()
        Me.tb_keterangan.Text = ""
        Me.tb_jumlah.Text = ""
        Me.id_transaksi = 0
    End Sub

    Sub loaddata()

        If Me.vid <> 0 Then
            Me.id_transaksi = Me.vid
        End If

        sqlcom = "select id, convert(char, tanggal, 103) as tanggal, id_cash_account, keterangan, is_submit, isnull(kurs,0) as kurs,"
        sqlcom = sqlcom + " no_giro, convert(char, tgl_giro, 103) as tgl_giro, convert(char, tgl_jatuh_tempo, 103) as tgl_jatuh_tempo, no_voucher"
        sqlcom = sqlcom + " from pengeluaran_uang_lain"
        sqlcom = sqlcom + " where id = " & Me.id_transaksi
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_no_transaksi.Text = reader.Item("id").ToString
            Me.tb_tgl_transaksi.Text = reader.Item("tanggal").ToString
            Me.dd_kas_petty_cash.SelectedValue = reader.Item("id_cash_account").ToString
            Me.tb_kurs.Text = FormatNumber(reader.Item("kurs").ToString, 2)
            Me.tb_no_giro.Text = reader.Item("no_giro").ToString
            Me.tb_tgl_giro.Text = reader.Item("tgl_giro").ToString
            Me.tb_jatuh_tempo.Text = reader.Item("tgl_jatuh_tempo").ToString
            Me.tb_keterangan_header.Text = reader.Item("keterangan").ToString
            Me.tb_no_voucher.Text = reader.Item("no_voucher").ToString
            Me.tbl_petty_cash.Visible = True

            If reader.Item("is_submit").ToString = "S" Then
                Me.btn_save.Enabled = False
                Me.btn_submit.Enabled = False
                Me.btn_unsubmit.Enabled = True
                Me.dd_kas_petty_cash.Enabled = False
                Me.btn_add.Enabled = False
                Me.btn_update.Enabled = False
                Me.btn_delete.Enabled = False
            ElseIf reader.Item("is_submit").ToString = "B" Then
                Me.btn_save.Enabled = True
                Me.btn_submit.Enabled = True
                Me.btn_unsubmit.Enabled = False
                Me.dd_kas_petty_cash.Enabled = True
                Me.btn_add.Enabled = True
                Me.btn_update.Enabled = True
                Me.btn_delete.Enabled = True
            End If

            'Me.bindmatauang()
        Else
            Me.btn_submit.Enabled = False
            Me.btn_unsubmit.Enabled = True
            Me.tbl_petty_cash.Visible = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            Me.clearform()
            Me.clearakun()
            Me.bindperiodetransaksi()
            Me.bindcashaccount()
            Me.bindmatauang()
            Me.loaddata()
            Me.loadgrid()
            Me.tb_id_akun.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun.Attributes.Add("style", "display: none;")
            Me.link_popup_akun.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun.ClientID & "', '" & Me.link_refresh_akun.UniqueID & "')")

            'If Session.Item("code") <> 1 Then
            '    Me.btn_unsubmit.Visible = False
            'End If

            tradingClass.ViewButtonUnsubmit(Me.btn_unsubmit)

        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        If Me.vpilihan = "0" Then
            Response.Redirect("~/pengeluaran_uang_lain2.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&vpaging=" & Me.vpaging & "&vpilihan=" & Me.vpilihan & "&vsearch=" & Me.vsearch)
        Else
            Response.Redirect("~/pengeluaran_uang_lain2.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&vpaging=" & Me.vpaging & "&vpilihan=" & Me.vpilihan & "&vsearch_tanggal=" & Me.vsearch_tanggal)
        End If
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tgl_transaksi.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal transaksi terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_kurs.Text) Or Me.tb_kurs.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi kurs terlebih dahulu"
            Else
                Dim vtgl As String = ""
                Dim vtgl_giro As String = ""
                Dim vtgl_jatuh_tempo As String = ""

                vtgl = Me.tb_tgl_transaksi.Text.Substring(3, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(0, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(6, 4)

                If Not String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                    vtgl_giro = Me.tb_tgl_giro.Text.Substring(3, 2) & "/" & Me.tb_tgl_giro.Text.Substring(0, 2) & "/" & Me.tb_tgl_giro.Text.Substring(6, 4)
                End If

                If Not String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                    vtgl_jatuh_tempo = Me.tb_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(6, 4)
                End If

                If Me.id_transaksi = 0 Then

                    Dim vmax As String = ""
                    sqlcom = "select isnull(max(convert(int, right(id, 5))),0) + 1 as vid"
                    sqlcom = sqlcom + " from pengeluaran_uang_lain"
                    sqlcom = sqlcom + " where convert(int, substring(convert(char, id), 3,2)) = " & Me.vbulan
                    sqlcom = sqlcom + " and convert(int, left(id, 2)) = " & Right(Me.vtahun, 2)
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = Right(Me.vtahun, 2) & Me.vbulan.ToString.PadLeft(2, "0") & reader.Item("vid").ToString.PadLeft(5, "0")
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into pengeluaran_uang_lain(id, tanggal, id_cash_account, id_transaction_period, is_submit,"
                    sqlcom = sqlcom + " no_giro, tgl_giro, tgl_jatuh_tempo, keterangan, kurs, no_voucher)"
                    sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "'," & Me.dd_kas_petty_cash.SelectedValue
                    sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'B','" & Me.tb_no_giro.Text & "',"

                    If String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                        sqlcom = sqlcom + " NULL,"
                    Else
                        sqlcom = sqlcom + "'" & vtgl_giro & "',"
                    End If

                    If String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                        sqlcom = sqlcom + " NULL"
                    Else
                        sqlcom = sqlcom + "'" & vtgl_jatuh_tempo & "'"
                    End If

                    sqlcom = sqlcom + ",'" & Me.tb_keterangan_header.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ",'" & Me.tb_no_voucher.Text.Trim() & "')"

                    connection.koneksi.InsertRecord(sqlcom)
                    Me.id_transaksi = vmax
                    tradingClass.Alert("Data sudah disimpan", Me.Page)
                Else

                    sqlcom = "update pengeluaran_uang_lain"
                    sqlcom = sqlcom + " set tanggal = '" & vtgl & "',"
                    sqlcom = sqlcom + " id_cash_account = " & Me.dd_kas_petty_cash.SelectedValue & ","
                    sqlcom = sqlcom + " kurs = " & Decimal.ToDouble(Me.tb_kurs.Text) & ","
                    sqlcom = sqlcom + " no_giro = '" & Me.tb_no_giro.Text & "',"

                    If String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                        sqlcom = sqlcom + " tgl_giro = NULL,"
                    Else
                        sqlcom = sqlcom + " tgl_giro = '" & vtgl_giro & "',"
                    End If

                    If String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                        sqlcom = sqlcom + " tgl_jatuh_tempo = NULL,"
                    Else
                        sqlcom = sqlcom + " tgl_jatuh_tempo = '" & vtgl_jatuh_tempo & "',"
                    End If

                    sqlcom = sqlcom + " keterangan = '" & Me.tb_keterangan_header.Text & "',"

                    sqlcom = sqlcom + " no_voucher = '" & Me.tb_no_voucher.Text.Trim() & "'"

                    sqlcom = sqlcom + " where id = " & Me.id_transaksi
                    connection.koneksi.UpdateRecord(sqlcom)
                    tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
                Me.loaddata()

            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try

            Me.tb_keterangan.Text = ""
            Me.tb_jumlah.Text = ""
            Me.clearakun()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select pengeluaran_uang_lain_detil.id_pengeluaran_lain, pengeluaran_uang_lain_detil.seq, pengeluaran_uang_lain_detil.id_item_biaya,"
            sqlcom = sqlcom + " pengeluaran_uang_lain_detil.jumlah,"
            sqlcom = sqlcom + " pengeluaran_uang_lain_detil.keterangan, coa_list.accountno + coa_list.inaname as nama_biaya"
            sqlcom = sqlcom + " from pengeluaran_uang_lain_detil"
            sqlcom = sqlcom + " inner join coa_list on coa_list.accountno = pengeluaran_uang_lain_detil.id_item_biaya"
            sqlcom = sqlcom + " where id_pengeluaran_lain = " & Me.id_transaksi
            sqlcom = sqlcom + " order by seq"
            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "pengeluaran_uang_lain_detil")
                Me.dg_data.DataSource = ds.Tables("pengeluaran_uang_lain_detil").DefaultView

                If ds.Tables("pengeluaran_uang_lain_detil").Rows.Count > 0 Then
                    If ds.Tables("pengeluaran_uang_lain_detil").Rows.Count > 8 Then
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
            sqlcom = "select isnull(sum(isnull(jumlah,0)),0) as total_nilai from pengeluaran_uang_lain_detil"
            sqlcom = sqlcom + " where id_pengeluaran_lain = " & Me.id_transaksi
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
            If String.IsNullOrEmpty(Me.tb_keterangan.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi keterangan terlebih dahulu"
            ElseIf Me.tb_id_akun.Text = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_jumlah.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah nilai terlebih dahulu"
            Else
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vmax from pengeluaran_uang_lain_detil"
                sqlcom = sqlcom + " where id_pengeluaran_lain = " & Me.id_transaksi
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into pengeluaran_uang_lain_detil(id_pengeluaran_lain, seq, id_item_biaya, jumlah, keterangan)"
                sqlcom = sqlcom + " values (" & Me.id_transaksi & "," & vmax & ",'" & Me.tb_id_akun.Text & "'," & Decimal.ToDouble(Me.tb_jumlah.Text) & ","
                sqlcom = sqlcom + "'" & Me.tb_keterangan.Text & "')"
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
                    sqlcom = "delete pengeluaran_uang_lain_detil"
                    sqlcom = sqlcom + " where id_pengeluaran_lain = " & Me.id_transaksi
                    sqlcom = sqlcom + " and seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
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
                    sqlcom = "update pengeluaran_uang_lain_detil"
                    sqlcom = sqlcom + " set jumlah = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_jumlah"), TextBox).Text) & ","
                    sqlcom = sqlcom + " keterangan = '" & CType(Me.dg_data.Items(x).FindControl("tb_keterangan"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where id_pengeluaran_lain = " & Me.id_transaksi
                    sqlcom = sqlcom + " and seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
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

        Dim vtgl As String = Me.tb_tgl_transaksi.Text.Substring(3, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(0, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(6, 4)

        Dim readeritem As SqlClient.SqlDataReader
        sqlcom = "select id_item_biaya as account_code, isnull(pengeluaran_uang_lain_detil.jumlah,0) as jumlah, keterangan"
        sqlcom = sqlcom + " from pengeluaran_uang_lain_detil"
        sqlcom = sqlcom + " where id_pengeluaran_lain = " & Me.id_transaksi
        readeritem = connection.koneksi.SelectRecord(sqlcom)
        Do While readeritem.Read
            'debet
            ' biaya
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl & "','KLRUANG','" & readeritem.Item("account_code").ToString & "',"
            sqlcom = sqlcom + "'" & Me.vakun_bank_account & "'," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) * Decimal.ToDouble(Me.tb_kurs.Text) & ",0"
            sqlcom = sqlcom + ",'Transaksi Pengeluaran Uang. " & Me.lbl_no_transaksi.Text & "(" & readeritem.Item("keterangan").ToString & ")'"
            sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text)

            If Me.lbl_mata_uang.Text = "IDR" Then
                sqlcom = sqlcom + ", 0, 0)"
            Else
                sqlcom = sqlcom + "," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) & ",0)"
            End If

            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' kas
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl & "','KLRUANG','" & Me.vakun_bank_account & "',"
            sqlcom = sqlcom + "'" & readeritem.Item("account_code").ToString & "',0," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) * Decimal.ToDouble(Me.tb_kurs.Text)
            sqlcom = sqlcom + ",'Transaksi Pengeluaran Uang. " & Me.lbl_no_transaksi.Text & "(" & readeritem.Item("keterangan").ToString & ")'"
            sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.lbl_mata_uang.Text & "'," & Decimal.ToDouble(Me.tb_kurs.Text)

            If Me.lbl_mata_uang.Text = "IDR" Then
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

            'cek saldo
            Dim vsaldo_akhir As Decimal = 0
            sqlcom = "select account_code, isnull(saldo_akhir,0) as saldo_akhir"
            sqlcom = sqlcom + " from bank_account"
            sqlcom = sqlcom + " where id = " & Me.dd_kas_petty_cash.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_bank_account = reader.Item("account_code").ToString.Trim
                vsaldo_akhir = reader.Item("saldo_akhir").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_bank_account) Then
                Me.lbl_msg.Text = "Kode akun pada Kas/Bank tersebut tidak ada"
                Exit Sub
            End If

            'If Decimal.ToDouble(vsaldo_akhir) < Decimal.ToDouble(Me.lbl_total_nilai.Text) Then
            'Me.lbl_msg.Text = "Saldo akhir Kas/Bank tersebut tidak mencukupi"
            'Exit Sub
            'End If


            sqlcom = "update pengeluaran_uang_lain"
            sqlcom = sqlcom + " set is_submit = 'S'"
            sqlcom = sqlcom + " where id = " & Me.id_transaksi
            connection.koneksi.UpdateRecord(sqlcom)


            'Daniel
            'Me.jurnal()
            Me.GL()
            'Daniel

            Me.loaddata()
            Me.loadgrid()

        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub dd_kas_petty_cash_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_kas_petty_cash.SelectedIndexChanged
        Me.bindmatauang()
    End Sub

    Protected Sub link_refresh_akun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun.Click
        Me.bindakun()
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_unsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_unsubmit.Click
        Try
            Dim dr As SqlClient.SqlDataReader
            Me.lbl_msg.Text = Nothing
            Dim keterangan As String = "Transaksi Pengeluaran Uang no. " & Me.lbl_no_transaksi.Text

            sqlcom = Nothing
            sqlcom = "update pengeluaran_uang_lain"
            sqlcom = sqlcom + " set is_submit = 'B'"
            sqlcom = sqlcom + " where id = " & Me.id_transaksi
            connection.koneksi.UpdateRecord(sqlcom)

            Me.tradingClass.DeleteAkunJurnal(keterangan, Me.vid_periode_transaksi)
            Me.tradingClass.DeleteAkunGeneralLedger(keterangan, Me.vid_periode_transaksi)

            dr = connection.koneksi.SelectRecord("SELECT id_item_biaya as account_code, isnull(pengeluaran_uang_lain_detil.jumlah,0) as jumlah, keterangan from pengeluaran_uang_lain_detil where id_pengeluaran_lain = " & Me.id_transaksi & "")
            Do While dr.Read
                sqlcom = Nothing
                sqlcom = " delete from history_kas "
                sqlcom = sqlcom + " where id_transaction_period = '" & Me.vid_periode_transaksi & "' "
                sqlcom = sqlcom + " and id_cash_bank = '" & Me.dd_kas_petty_cash.SelectedValue & "' "
                sqlcom = sqlcom + " and tanggal = '" & Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text) & "' "
                sqlcom = sqlcom + " and keterangan = '" & keterangan & "' "
                sqlcom = sqlcom + " and nilai_debet = '0' "
                sqlcom = sqlcom + " and nilai_kredit = '" & System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * Me.tb_kurs.Text.Trim & "' "
                sqlcom = sqlcom + " and no_voucher = '" & Me.tb_no_voucher.Text.Trim() & "' "
                connection.koneksi.DeleteRecord(sqlcom)

                sqlcom = Nothing
                sqlcom = "update bank_account"
                sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) + isnull(" & System.Convert.ToDecimal(dr.Item("jumlah").ToString()) * Me.tb_kurs.Text.Trim & ",0)"
                sqlcom = sqlcom + " where id = " & Me.dd_kas_petty_cash.SelectedValue
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            dr.Close()
            connection.koneksi.CloseKoneksi()

            Me.loaddata()
            Me.loadgrid()

            tradingClass.Alert("Data sudah diunsubmit!", Me.Page)
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class
