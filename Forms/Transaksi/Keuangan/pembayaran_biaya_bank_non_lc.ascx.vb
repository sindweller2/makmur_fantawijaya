Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_pembayaran_biaya_bank_non_lc
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

    Private ReadOnly Property vpilihan() As String
        Get
            Dim o As Object = Request.QueryString("vpilihan")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Private ReadOnly Property vpaging() As String
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Private ReadOnly Property vsearch() As String
        Get
            Dim o As Object = Request.QueryString("vsearch")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
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

    Public Property status_bayar() As String
        Get
            Dim o As Object = ViewState("status_bayar")
            If Not o Is Nothing Then Return CStr(o) Else Return 0
        End Get
        Set(ByVal value As String)
            ViewState("status_bayar") = value
        End Set
    End Property

    Public Property vakun_biaya_non_lc() As String
        Get
            Dim o As Object = ViewState("vakun_biaya_non_lc")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_biaya_non_lc") = value
        End Set
    End Property

    Public Property vakun_kas_bank() As String
        Get
            Dim o As Object = ViewState("vakun_kas_bank")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_kas_bank") = value
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

        sqlcom = "select id from transaction_period where bulan = " & Me.bulan & " and tahun=" & Me.tb_tahun.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.dd_bulan.SelectedValue = reader.Item("id").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

    End Sub

    Sub bindbulan()
        sqlcom = "select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.bulan = reader.Item("bulan").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub checkdata()
        sqlcom = "select *"
        sqlcom = sqlcom + " from entry_dokumen_impor"
        sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
        sqlcom = sqlcom + " where entry_dokumen_impor.id_transaction_period = " & Me.dd_bulan.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.tbl_search.Visible = True
        Else
            Me.tbl_search.Visible = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub total_bayar(ByVal seq_pembayaran As Integer, ByVal nilai_invoice As Decimal)
        Try
            sqlcom = "select isnull(sum(isnull(jumlah_bayar,0)),0) as jumlah_bayar"
            sqlcom = sqlcom + " from pembayaran_invoice_supplier_tt"
            sqlcom = sqlcom + " where seq_entry_dokumen = " & seq_pembayaran
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                If Decimal.ToDouble(reader.Item("jumlah_bayar").ToString) = nilai_invoice Then
                    Me.status_bayar = "Sudah lunas"
                Else
                    Me.status_bayar = "Belum lunas"
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

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select entry_dokumen_impor.seq as no_dokumen, entry_dokumen_impor.invoice_no, entry_dokumen_impor.packing_list_no,"
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.biaya_bank_non_lc,0) as biaya_bank_non_lc, purchase_order.po_no_text,"
            sqlcom = sqlcom + " rtrim(convert(char, tgl_byr_biayabank_nonlc, 103)) as tgl_byr_biayabank_nonlc,"
            sqlcom = sqlcom + " entry_dokumen_impor.id_bank_biayabank_nonlc, no_giro_biayabank_nonlc,"
            sqlcom = sqlcom + " rtrim(convert(char, tgl_giro_biayabank_nonlc, 103)) as tgl_giro_biayabank_nonlc,"
            sqlcom = sqlcom + " rtrim(convert(char, tgl_jatuh_tempo_biayabank_nonlc, 103)) as tgl_jatuh_tempo_biayabank_nonlc,"
            sqlcom = sqlcom + " entry_dokumen_impor.is_submit_non_lc"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
            sqlcom = sqlcom + " where entry_dokumen_impor.id_transaction_period = " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " and purchase_order.is_lc  = 'False'"

            If Me.dd_pilihan.SelectedValue = "0" Then
                sqlcom = sqlcom + " and upper(purchase_order.po_no_text) like upper('%" & Me.tb_search.Text & "%')"
            ElseIf Me.dd_pilihan.SelectedValue = "1" Then
                sqlcom = sqlcom + " and entry_dokumen_impor.seq like upper('%" & Me.tb_search.Text & "%')"
            ElseIf Me.dd_pilihan.SelectedValue = "2" Then
                sqlcom = sqlcom + " and entry_dokumen_impor.invoice_no like upper('%" & Me.tb_search.Text & "%')"
            ElseIf Me.dd_pilihan.SelectedValue = "2" Then
                sqlcom = sqlcom + " and lc.no_lc like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " order by entry_dokumen_impor.seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "entry_dokumen_impor")
                Me.dg_data.DataSource = ds.Tables("entry_dokumen_impor").DefaultView

                If ds.Tables("entry_dokumen_impor").Rows.Count > 0 Then
                    If ds.Tables("entry_dokumen_impor").Rows.Count > 10 Then
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
                    'Daniel
                    Me.Label34.Visible = True
                    Me.DropDownListAccount.Visible = True
                    'Daniel
                    Me.btn_update.Visible = True
                    Me.btn_submit.Visible = True

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name from bank_account order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_kas_bank"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_kas_bank"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_kas_bank"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_kas_bank"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_kas_bank"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_kas_bank"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If CType(Me.dg_data.Items(x).FindControl("lbl_is_submit_non_lc"), Label).Text = "B" Or String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_is_submit_non_lc"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                        End If
                    Next
                Else
                    Me.dg_data.Visible = False
                    'Daniel
                    Me.Label34.Visible = False
                    Me.DropDownListAccount.Visible = False
                    'Daniel
                    Me.btn_update.Visible = False
                    Me.btn_submit.Visible = False
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
            If Me.vtahun = 0 Then
                Me.tb_tahun.Text = Now.Year
            Else
                Me.tb_tahun.Text = Me.vtahun
            End If

            If Me.vbulan = 0 Then
                Me.bulan = Now.Month
            Else
                Me.bulan = Me.vbulan
            End If

            Me.bindperiode_transaksi()

            If Me.vpilihan = "" Then
                Me.dd_pilihan.SelectedValue = "0"
            Else
                Me.dd_pilihan.SelectedValue = Me.vpilihan
            End If

            If Me.vsearch <> "" Then
                Me.tb_search.Text = Me.vsearch
            Else
                Me.tb_search.Text = ""
            End If


            If Me.vpaging <> 0 Then
                Me.dg_data.CurrentPageIndex = vpaging
            End If
            Me.loadgrid()
        End If
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.bindbulan()
        Me.loadgrid()
    End Sub


    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.bindbulan()
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then

                    Dim vtgl_bayar As String = ""
                    Dim tgl_bayar As Date = Nothing
                    If Not String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_bayar"), TextBox).Text) Then
                        vtgl_bayar = CType(Me.dg_data.Items(x).FindControl("tb_tgl_bayar"), TextBox).Text
                        tgl_bayar = vtgl_bayar.Substring(3, 2) & "/" & vtgl_bayar.Substring(0, 2) & "/" & vtgl_bayar.Substring(6, 4)
                    End If

                    Dim vtgl_giro As String = ""
                    Dim tgl_giro As Date = Nothing
                    If Not String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_giro"), TextBox).Text) Then
                        vtgl_giro = CType(Me.dg_data.Items(x).FindControl("tb_tgl_giro"), TextBox).Text
                        tgl_giro = vtgl_giro.Substring(3, 2) & "/" & vtgl_giro.Substring(0, 2) & "/" & vtgl_giro.Substring(6, 4)
                    End If

                    Dim vtgl_jatuh_tempo As String = ""
                    Dim tgl_jatuh_tempo As Date = Nothing
                    If Not String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_jatuh_tempo"), TextBox).Text) Then
                        vtgl_jatuh_tempo = CType(Me.dg_data.Items(x).FindControl("tb_tgl_jatuh_tempo"), TextBox).Text
                        tgl_jatuh_tempo = vtgl_jatuh_tempo.Substring(3, 2) & "/" & vtgl_jatuh_tempo.Substring(0, 2) & "/" & vtgl_jatuh_tempo.Substring(6, 4)
                    End If


                    sqlcom = "update entry_dokumen_impor"
                    sqlcom = sqlcom + " set biaya_bank_non_lc = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_jumlah"), TextBox).Text) & ","
                    sqlcom = sqlcom + " id_bank_biayabank_nonlc = " & CType(Me.dg_data.Items(x).FindControl("dd_kas_bank"), DropDownList).SelectedValue & ","
                    sqlcom = sqlcom + " no_giro_biayabank_nonlc = '" & CType(Me.dg_data.Items(x).FindControl("tb_giro"), TextBox).Text & "',"

                    If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_bayar"), TextBox).Text) Then
                        sqlcom = sqlcom + " tgl_byr_biayabank_nonlc = NULL,"
                    Else
                        sqlcom = sqlcom + " tgl_byr_biayabank_nonlc = '" & tgl_bayar & "',"
                    End If

                    If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_giro"), TextBox).Text) Then
                        sqlcom = sqlcom + " tgl_giro_biayabank_nonlc = NULL,"
                    Else
                        sqlcom = sqlcom + " tgl_giro_biayabank_nonlc = '" & tgl_giro & "',"
                    End If

                    If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_jatuh_tempo"), TextBox).Text) Then
                        sqlcom = sqlcom + " tgl_jatuh_tempo_biayabank_nonlc = NULL,"
                    Else
                        sqlcom = sqlcom + " tgl_jatuh_tempo_biayabank_nonlc = '" & tgl_jatuh_tempo & "',"
                    End If

                    sqlcom = sqlcom + " is_submit_non_lc = 'B'"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
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

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            '========= cek akun biaya bank non lc

            Dim vkurs_akhir_bulan As Decimal = 0
            sqlcom = "select isnull(kurs_bulanan,0) as kurs_bulanan"
            sqlcom = sqlcom + " from transaction_period"
            sqlcom = sqlcom + " where id = " & Me.dd_bulan.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                vkurs_akhir_bulan = Decimal.ToDouble(reader.Item("kurs_bulanan").ToString)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            sqlcom = "select akun_biaya_bank"
            sqlcom = sqlcom + " from akun_biaya_non_lc"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_biaya_non_lc = reader.Item("akun_biaya_bank").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If String.IsNullOrEmpty(Me.vakun_biaya_non_lc) Then
                Me.lbl_msg.Text = "Akun biaya bank non L/C tidak ada"
                Exit Sub
            End If



            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    If Not String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_bayar"), TextBox).Text) Then
                        sqlcom = "update entry_dokumen_impor"
                        sqlcom = sqlcom + " set is_submit_non_lc = 'S'"
                        sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                        connection.koneksi.UpdateRecord(sqlcom)

                        sqlcom = "select account_code"
                        sqlcom = sqlcom + " from bank_account"
                        sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("dd_kas_bank"), DropDownList).SelectedValue
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        reader.Read()
                        If reader.HasRows Then
                            Me.vakun_kas_bank = reader.Item("account_code").ToString.Trim
                        End If
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If String.IsNullOrEmpty(Me.vakun_biaya_non_lc) Then
                            Me.lbl_msg.Text = "Akun kas/bank tersebut tidak ada"
                            Exit Sub
                        End If

                        Dim vseq As Integer = CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                        Dim vtanggal As String = CType(Me.dg_data.Items(x).FindControl("tb_tgl_bayar"), TextBox).Text
                        Dim vtgl As Date = vtanggal.Substring(3, 2) & "/" & vtanggal.Substring(0, 2) & "/" & vtanggal.Substring(6, 4)
                        Dim vnilai As Decimal = Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_jumlah"), TextBox).Text)
                        Dim vid_kasbank As Integer = CType(Me.dg_data.Items(x).FindControl("dd_kas_bank"), DropDownList).SelectedValue

                        'Daniel
                        Dim id As String = Me.tradingClass.IDTransaksiMax
                        Dim d_kurs As Decimal = vkurs_akhir_bulan 'System.Convert.ToDecimal(tradingClass.KursBulanan(Me.vid_periode_transaksi))
                        Dim keterangan As String = "Pembayaran biaya bank non L/C dokumen no. " & vseq & ""

                        Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(vtanggal), id, Me.tradingClass.JurnalType("4"), keterangan, Me.dd_bulan.SelectedValue)

                        tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtanggal), Me.DropDownListAccount.SelectedValue, Me.vakun_kas_bank, vnilai * d_kurs, 0, keterangan, Me.dd_bulan.SelectedValue, "USD", d_kurs, vnilai, 0, String.Empty)
                        tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtanggal), Me.vakun_kas_bank, Me.DropDownListAccount.SelectedValue, 0, vnilai * d_kurs, keterangan, Me.dd_bulan.SelectedValue, "USD", d_kurs, 0, vnilai, String.Empty)



                        '' akun biaya bank non lc -> kas/bank
                        'Me.seq_max()
                        'sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
                        'sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
                        'sqlcom = sqlcom + " values(" & Me.vmax & ",'" & vseq & "','" & vtgl & "','BIANONLC','" & Me.vakun_biaya_non_lc & "',"
                        'sqlcom = sqlcom + "'" & Me.vakun_kas_bank & "'," & vnilai * vkurs_akhir_bulan & ",0, 'Pembayaran biaya bank non L/C dokumen no. " & vseq & "'"
                        'sqlcom = sqlcom + "," & Me.dd_bulan.SelectedValue & ",'USD'," & vkurs_akhir_bulan & ")"
                        'connection.koneksi.InsertRecord(sqlcom)

                        '' akun kas/bank -? biaya bank non L/C
                        'Me.seq_max()
                        'sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
                        'sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
                        'sqlcom = sqlcom + " values(" & Me.vmax & ",'" & vseq & "','" & vtgl & "','BIANONLC','" & Me.vakun_kas_bank & "',"
                        'sqlcom = sqlcom + "'" & Me.vakun_biaya_non_lc & "',0," & vnilai * vkurs_akhir_bulan & ", 'Pembayaran biaya bank non L/C dokumen no. " & vseq & "'"
                        'sqlcom = sqlcom + "," & Me.dd_bulan.SelectedValue & ",'USD'," & vkurs_akhir_bulan & ")"
                        'connection.koneksi.InsertRecord(sqlcom)


                        ' nilai kredit
                        Me.max_seq_history_kas()
                        sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
                        sqlcom = sqlcom + " values(" & Me.dd_bulan.SelectedValue & "," & vid_kasbank & ",'" & Me.tradingClass.DateValidated(vtanggal) & "','"
                        sqlcom = sqlcom + keterangan
                        sqlcom = sqlcom + "',0," & vnilai * d_kurs & "," & Me.vmax_history_kas & ")"
                        connection.koneksi.InsertRecord(sqlcom)

                        'update kas
                        sqlcom = "update bank_account"
                        sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - " & vnilai * d_kurs
                        sqlcom = sqlcom + " where id = " & vid_kasbank
                        connection.koneksi.UpdateRecord(sqlcom)

                        tradingClass.Alert("Data sudah disubmit!", Me.Page)
                        'Daniel

                    End If
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class
