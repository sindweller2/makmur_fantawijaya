Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_posting_penerimaan_pembayaran_penjualan
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()
    'Daniel

    Public Property vakun_piutang_giro_mundur() As String
        Get
            Dim o As Object = ViewState("vakun_piutang_giro_mundur")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_piutang_giro_mundur") = value
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

    Public Property vnama_customer() As String
        Get
            Dim o As Object = ViewState("vnama_customer")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vnama_customer") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub updatetanggaltransfer()
        Try
            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Trim
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Trim

            'vtgl_awal = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            'vtgl_akhir = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            sqlcom = "update pembayaran_invoice_penjualan"
            sqlcom = sqlcom + " set tgl_terima_uang = tanggal"
            sqlcom = sqlcom + " where id_jenis_pembayaran = 4"
            sqlcom = sqlcom + " and (pembayaran_invoice_penjualan.tanggal >= '" & Me.tradingClass.DateValidated(vtgl_awal) & "'"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.tanggal <= '" & Me.tradingClass.DateValidated(vtgl_akhir) & "')"
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub checkdata()
        Try

            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Trim
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Trim

            'vtgl_awal = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            'vtgl_akhir = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            sqlcom = "select *"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
            sqlcom = sqlcom + " inner join sales_order on sales_order.no = pembayaran_invoice_penjualan.no_so"
            sqlcom = sqlcom + " where (pembayaran_invoice_penjualan.tanggal >= '" & Me.tradingClass.DateValidated(vtgl_awal) & "'"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.tanggal <= '" & Me.tradingClass.DateValidated(vtgl_akhir) & "')"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.is_submit = 'S'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tbl_search.Visible = True
            Else
                Me.tbl_search.Visible = False
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try

            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Trim
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Trim

            'vtgl_awal = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            'vtgl_akhir = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select pembayaran_invoice_penjualan.seq, pembayaran_invoice_penjualan.no_so, pembayaran_invoice_penjualan.id_periode_pembayaran, "
            sqlcom = sqlcom + " convert(char, pembayaran_invoice_penjualan.tanggal, 103) as tanggal, pembayaran_invoice_penjualan.id_jenis_pembayaran, "
            sqlcom = sqlcom + " pembayaran_invoice_penjualan.id_bank_account, pembayaran_invoice_penjualan.no_cek_giro, "
            sqlcom = sqlcom + " convert(char, pembayaran_invoice_penjualan.tgl_cek_giro, 103) as tgl_cek_giro,"
            sqlcom = sqlcom + " convert(char, pembayaran_invoice_penjualan.tgl_jatuh_tempo_cek_giro, 103) as tgl_jatuh_tempo_cek_giro,"
            sqlcom = sqlcom + " convert(char, pembayaran_invoice_penjualan.tgl_terima_uang, 103) as tgl_terima_uang,"
            sqlcom = sqlcom + " isnull(pembayaran_invoice_penjualan.nilai_pembayaran,0) as nilai_pembayaran,"
            sqlcom = sqlcom + " isnull(pembayaran_invoice_penjualan.potongan,0) as potongan, "
            sqlcom = sqlcom + " isnull(pembayaran_invoice_penjualan.kelebihan,0) as kelebihan, pembayaran_invoice_penjualan.id_currency, "
            sqlcom = sqlcom + " pembayaran_invoice_penjualan.is_posting, sales_order.so_no_text as no_invoice, sales_order.rate as kurs, pembayaran_invoice_penjualan.no_voucher"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
            sqlcom = sqlcom + " inner join sales_order on sales_order.no = pembayaran_invoice_penjualan.no_so"
            sqlcom = sqlcom + " where (pembayaran_invoice_penjualan.tanggal >= '" & Me.tradingClass.DateValidated(vtgl_awal) & "'"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.tanggal <= '" & Me.tradingClass.DateValidated(vtgl_akhir) & "')"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.is_submit = 'S'"

            If Not String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom + " and upper(sales_order.so_no_text) like upper('%" & Me.tb_search.Text & "%')"
                sqlcom = sqlcom + " or no_voucher like '%" & Me.tb_search.Text & "%'"
                sqlcom = sqlcom + " or pembayaran_invoice_penjualan.nilai_pembayaran like '%" & Me.tb_search.Text & "%'"
            End If

            sqlcom = sqlcom + " order by pembayaran_invoice_penjualan.seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "pembayaran_invoice_penjualan")
                Me.dg_data.DataSource = ds.Tables("pembayaran_invoice_penjualan").DefaultView

                If ds.Tables("pembayaran_invoice_penjualan").Rows.Count > 0 Then
                    If ds.Tables("pembayaran_invoice_penjualan").Rows.Count > 10 Then
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
                    Me.btn_posting.Visible = True

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name"
                        sqlcom = sqlcom + " from transaction_period"
                        sqlcom = sqlcom + " order by bulan"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_periode"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_periode"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        sqlcom = "select id, name"
                        sqlcom = sqlcom + " from jenis_pembayaran"
                        sqlcom = sqlcom + " order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_jenis"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        sqlcom = "select id, name"
                        sqlcom = sqlcom + " from bank_account"
                        sqlcom = sqlcom + " order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).Items.Add(New ListItem("--Nama bank--", "0"))

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).SelectedValue = "0"
                        Else
                            CType(Me.dg_data.Items(x).FindControl("dd_bank_account"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text
                        End If

                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        sqlcom = "select id"
                        sqlcom = sqlcom + " from currency"
                        sqlcom = sqlcom + " order by id"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataTextField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_mata_uang"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If CType(Me.dg_data.Items(x).FindControl("lbl_is_posting"), Label).Text = "B" Or String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_is_posting"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                            CType(Me.dg_data.Items(x).FindControl("btn_unsubmit"), Button).Enabled = False

                            'If Session.Item("code") <> 1 Then
                            '    CType(Me.dg_data.Items(x).FindControl("btn_unsubmit"), Button).Visible = False
                            'End If

                            tradingClass.ViewButtonUnsubmit(CType(Me.dg_data.Items(x).FindControl("btn_unsubmit"), Button))
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                            CType(Me.dg_data.Items(x).FindControl("btn_unsubmit"), Button).Enabled = True

                            'If Session.Item("code") <> 1 Then
                            '    CType(Me.dg_data.Items(x).FindControl("btn_unsubmit"), Button).Visible = False
                            'End If

                            tradingClass.ViewButtonUnsubmit(CType(Me.dg_data.Items(x).FindControl("btn_unsubmit"), Button))
                        End If
                    Next
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_posting.Visible = False
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
            Me.tb_tgl_awal.Text = "01" & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.tb_tgl_akhir.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.updatetanggaltransfer()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.lbl_msg.Text = Nothing
        If String.IsNullOrEmpty(Me.tb_tgl_awal.Text) Then
            Me.lbl_msg.Text = "Silahkan mengisi tanggal awal terlebih dahulu"
        ElseIf String.IsNullOrEmpty(Me.tb_tgl_akhir.Text) Then
            Me.lbl_msg.Text = "Silahkan mengisi tanggal akhir terlebih dahulu"
        Else
            Me.dg_data.CurrentPageIndex = 0
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
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

    Protected Sub btn_posting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_posting.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    Dim vflag As String = "T"
                    If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_terima"), TextBox).Text) Then
                        Me.lbl_msg.Text = "Silahkan mengisi tanggal terima uang terlebih dahulu"
                    Else
                        sqlcom = "select rtrim(convert(char, tgl_terima_uang, 103)) as tgl_terima_uang"
                        sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
                        sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        reader.Read()
                        If reader.HasRows Then
                            Me.lbl_msg.Text = reader.Item("tgl_terima_uang").ToString
                            If String.IsNullOrEmpty(reader.Item("tgl_terima_uang").ToString) Then
                                vflag = "T"
                            Else
                                vflag = "Y"
                            End If
                        End If
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                    End If

                    If vflag = "T" Then
                        Me.lbl_msg.Text = "Silahkan mengisi tanggal terima uang dan simpan terlebih dahulu"
                    Else
                        'akun piutang giro mundur

                        sqlcom = "select akun_piutang_giro_mundur, name from daftar_customer"
                        sqlcom = sqlcom + " where id = (select id_customer from sales_order where no = " & CType(Me.dg_data.Items(x).FindControl("lbl_no_so"), Label).Text & ")"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        reader.Read()
                        If reader.HasRows Then
                            Me.vnama_customer = reader.Item("name").ToString
                            Me.vakun_piutang_giro_mundur = reader.Item("akun_piutang_giro_mundur").ToString
                        End If
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If String.IsNullOrEmpty(Me.vakun_piutang_giro_mundur) Then
                            Me.lbl_msg.Text = "Akun giro mundur customer tersebut tidak ada"
                            Exit Sub
                        End If

                        'akun kas bank
                        sqlcom = "select account_code from bank_account"
                        sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text
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

                        Dim vtgl As String = CType(Me.dg_data.Items(x).FindControl("tb_tanggal"), Label).Text.Trim
                        'vtgl = vtgl.Substring(3, 2) & "/" & vtgl.Substring(0, 2) & "/" & vtgl.Substring(6, 4)

                        Dim vseq As String = CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                        Dim vnilai As Decimal = CType(Me.dg_data.Items(x).FindControl("tb_nilai_pembayaran"), Label).Text
                        Dim vso_no As String = CType(Me.dg_data.Items(x).FindControl("lbl_no_so"), Label).Text
                        Dim vno_invoice As String = CType(Me.dg_data.Items(x).FindControl("lbl_no_invoice"), Label).Text
                        Dim vcurrency As String = CType(Me.dg_data.Items(x).FindControl("lbl_mata_uang"), Label).Text
                        Dim vkurs As Decimal = CType(Me.dg_data.Items(x).FindControl("lbl_kurs"), Label).Text
                        Dim vperiode As Integer = CType(Me.dg_data.Items(x).FindControl("lbl_periode"), Label).Text
                        Dim vid_kasbank As Integer = CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text
                        Dim vno_voucher As String = CType(Me.dg_data.Items(x).FindControl("tb_no_voucher"), TextBox).Text

                        'Daniel
                        Dim id As String = Me.tradingClass.IDTransaksiMax
                        Dim d_kurs As Decimal = vkurs 'System.Convert.ToDecimal(tradingClass.KursBulanan(Me.vid_periode_transaksi))
                        Dim keterangan As String = "Penerimaan Pembayaran Penjualan Barang no. " & vno_invoice & ""

                        If vcurrency = "USD" Then
                            d_kurs = System.Convert.ToDecimal(tradingClass.KursBulanan("[kurs_bulanan]", vperiode))
                        Else
                            d_kurs = System.Convert.ToDecimal("1")
                        End If

                        Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(vtgl), id, Me.tradingClass.JurnalType("4"), keterangan, vperiode)

                        tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtgl), Me.vakun_kasbank, Me.vakun_piutang_giro_mundur, vnilai * d_kurs, 0, keterangan, vperiode, vcurrency, d_kurs, IIf(vcurrency = "IDR", 0, vnilai), 0, vno_voucher)
                        tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(vtgl), Me.vakun_piutang_giro_mundur, Me.vakun_kasbank, 0, vnilai * d_kurs, keterangan, vperiode, vcurrency, d_kurs, 0, IIf(vcurrency = "IDR", 0, vnilai), vno_voucher)

                        
                        ''debet
                        '' kas -> akun giro mundur

                        'Me.seq_max()
                        'sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
                        'sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd, nama_customer)"
                        'sqlcom = sqlcom + " values(" & Me.vmax & ",'" & vseq & "','" & vtgl & "','TRMUANGJUAL','" & Me.vakun_kasbank & "',"
                        'sqlcom = sqlcom + "'" & Me.vakun_piutang_giro_mundur & "'," & Decimal.ToDouble(vnilai) & ",0, 'Penerimaan uang penjualan invoice no." & vno_invoice & "'"
                        'sqlcom = sqlcom + "," & vperiode & ",'" & vcurrency & "'," & Decimal.ToDouble(vkurs)

                        'If vcurrency = "IDR" Then
                        '    sqlcom = sqlcom + ", 0, 0,"
                        'Else
                        '    sqlcom = sqlcom + "," & Decimal.ToDouble(vnilai) & ", 0,"
                        'End If

                        'sqlcom = sqlcom + "'" & Me.vnama_customer & "')"

                        'connection.koneksi.InsertRecord(sqlcom)

                        ''kredit
                        '' akun giro mundur -> kas
                        'Me.seq_max()
                        'sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
                        'sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs, nilai_debet_usd, nilai_kredit_usd, nama_customer)"
                        'sqlcom = sqlcom + " values(" & Me.vmax & ",'" & vseq & "','" & vtgl & "','TRMUANGJUAL','" & Me.vakun_piutang_giro_mundur & "',"
                        'sqlcom = sqlcom + "'" & Me.vakun_kasbank & "',0," & Decimal.ToDouble(vnilai) & ", 'Penerimaan uang penjualan invoice no." & vno_invoice & "'"
                        'sqlcom = sqlcom + "," & vperiode & ",'" & vcurrency & "'," & Decimal.ToDouble(vkurs)

                        'If vcurrency = "IDR" Then
                        '    sqlcom = sqlcom + ", 0, 0,"
                        'Else
                        '    sqlcom = sqlcom + "," & Decimal.ToDouble(vnilai) & ", 0,"
                        'End If

                        'sqlcom = sqlcom + "'" & Me.vnama_customer & "')"

                        'connection.koneksi.InsertRecord(sqlcom)


                        ' insert history kas
                        Me.max_seq_history_kas()
                        sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq, no_voucher)"
                        sqlcom = sqlcom + " values(" & vperiode & "," & vid_kasbank & ",'" & Me.tradingClass.DateValidated(vtgl) & "','"
                        sqlcom = sqlcom + keterangan
                        sqlcom = sqlcom + "'," & vnilai * d_kurs & ",0," & Me.vmax_history_kas & ",'" & vno_voucher & "')"
                        connection.koneksi.InsertRecord(sqlcom)


                        'update kas
                        sqlcom = "update bank_account"
                        sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) + " & vnilai * d_kurs
                        sqlcom = sqlcom + " where id = " & vid_kasbank
                        connection.koneksi.UpdateRecord(sqlcom)

                        sqlcom = "update pembayaran_invoice_penjualan set is_posting = 'S'"
                        sqlcom = sqlcom + " where seq = " & vseq
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

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    Dim vflag As String = "T"
                    If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_terima"), TextBox).Text) Then
                        Me.lbl_msg.Text = "Silahkan mengisi tanggal terima uang terlebih dahulu"
                    Else
                        Dim vno_voucher As String = CType(Me.dg_data.Items(x).FindControl("tb_no_voucher"), TextBox).Text
                        Dim vtgl As String = CType(Me.dg_data.Items(x).FindControl("tb_tgl_terima"), TextBox).Text
                        'vtgl = vtgl.Substring(3, 2) & "/" & vtgl.Substring(0, 2) & "/" & vtgl.Substring(6, 4)
                        sqlcom = "update pembayaran_invoice_penjualan"
                        sqlcom = sqlcom + " set tgl_terima_uang = '" & Me.tradingClass.DateValidated(vtgl) & "',"
                        sqlcom = sqlcom + " no_voucher = '" & vno_voucher & "'"
                        sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                        connection.koneksi.UpdateRecord(sqlcom)
                    End If
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_unsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Me.lbl_msg.Text = Nothing

            Dim rb As Button = DirectCast(sender, Button)
            Dim row As DataGridItem = DirectCast(rb.NamingContainer, DataGridItem)
            Dim vno_invoice As String = CType(Me.dg_data.Items(row.ItemIndex).FindControl("lbl_no_invoice"), Label).Text
            Dim vperiode As Integer = CType(Me.dg_data.Items(row.ItemIndex).FindControl("lbl_periode"), Label).Text
            Dim vnilai As Decimal = CType(Me.dg_data.Items(row.ItemIndex).FindControl("tb_nilai_pembayaran"), Label).Text
            Dim vkurs As Decimal = CType(Me.dg_data.Items(row.ItemIndex).FindControl("lbl_kurs"), Label).Text
            Dim vid_kasbank As Integer = CType(Me.dg_data.Items(row.ItemIndex).FindControl("lbl_bank"), Label).Text
            Dim vseq As String = CType(Me.dg_data.Items(row.ItemIndex).FindControl("lbl_seq"), Label).Text
            Dim vcurrency As String = CType(Me.dg_data.Items(row.ItemIndex).FindControl("lbl_mata_uang"), Label).Text

            Dim d_kurs As Decimal = vkurs
            Dim keterangan As String = "Penerimaan Pembayaran Penjualan Barang no. " & vno_invoice & ""

            If vcurrency = "USD" Then
                d_kurs = System.Convert.ToDecimal(tradingClass.KursBulanan("[kurs_bulanan]", vperiode))
            Else
                d_kurs = System.Convert.ToDecimal("1")
            End If
            

            Me.tradingClass.DeleteAkunJurnal(keterangan, vperiode)
            Me.tradingClass.DeleteAkunGeneralLedger(keterangan, vperiode)

            Me.tradingClass.DeleteHistoryKas(keterangan, vperiode)

            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - " & vnilai * d_kurs
            sqlcom = sqlcom + " where id = " & vid_kasbank
            connection.koneksi.UpdateRecord(sqlcom)

            sqlcom = "update pembayaran_invoice_penjualan set is_posting = 'B'"
            sqlcom = sqlcom + " where seq = " & vseq
            connection.koneksi.UpdateRecord(sqlcom)

            Me.loadgrid()

        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class
