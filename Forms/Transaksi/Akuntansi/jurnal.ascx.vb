Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Akuntansi_jurnal
    Inherits System.Web.UI.UserControl

    Public tradingClass As New tradingClass()

    'Daniel
    Sub GL()
        Try

            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim keterangan As String = Me.tb_keterangan.Text.Trim()

            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_jurnal.Text), id, Me.tradingClass.JurnalType("5"), keterangan, Me.vid_periode_transaksi)

            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_jurnal.Text), Me.vakun_debet, Me.vakun_kredit, System.Convert.ToDecimal(Me.vnilai_debet), 0, keterangan, Me.vid_periode_transaksi, "IDR", 1, 0, 0, String.Empty)
            Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_jurnal.Text), Me.vakun_kredit, Me.vakun_debet, 0, System.Convert.ToDecimal(Me.vnilai_kredit), keterangan, Me.vid_periode_transaksi, "IDR", 1, 0, 0, String.Empty)

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

    'Daniel
    Private ReadOnly Property vid() As String
        Get
            Dim o As Object = Request.QueryString("vid")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property
    'Daniel

    Private ReadOnly Property vjenis_jurnal() As String
        Get
            Dim o As Object = Request.QueryString("vjenis_jurnal")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Private ReadOnly Property vpaging() As String
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Public Property id_jurnal() As Integer
        Get
            Dim o As Object = ViewState("id_jurnal")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_jurnal") = value
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

    Public Property id_user() As Integer
        Get
            Dim o As Object = ViewState("id_user")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_user") = value
        End Set
    End Property

    Public Property vakun_debet() As String
        Get
            Dim o As Object = ViewState("vakun_debet")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_debet") = value
        End Set
    End Property

    Public Property vakun_kredit() As String
        Get
            Dim o As Object = ViewState("vakun_kredit")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_kredit") = value
        End Set
    End Property

    Public Property vnilai_debet() As Decimal
        Get
            Dim o As Object = ViewState("vnilai_debet")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vnilai_debet") = value
        End Set
    End Property

    Public Property vnilai_kredit() As Decimal
        Get
            Dim o As Object = ViewState("vnilai_kredit")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vnilai_kredit") = value
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
            'Daniel
            'Me.lbl_periode.Text = reader.Item("name").ToString
            'Daniel
            Me.vid_periode_transaksi = reader.Item("id").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindjenis_jurnal()
        'Daniel
        'If Me.vjenis_jurnal = "U" Then
        '    Me.lbl_jenis_jurnal.Text = "Jurnal Umum"
        'ElseIf (Me.vjenis_jurnal = "P") Then
        '    Me.lbl_jenis_jurnal.Text = "Jurnal Penyesuaian"
        'End If
        'Daniel
    End Sub

    Sub bindmata_uang()
        sqlcom = "select id from currency order by id"
        reader = connection.koneksi.SelectRecord(sqlcom)
        'Daniel
        'Me.dd_mata_uang.DataSource = reader
        'Me.dd_mata_uang.DataTextField = "id"
        'Me.dd_mata_uang.DataValueField = "id"
        'Me.dd_mata_uang.DataBind()
        'Daniel
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

    Sub clearform()
        Me.tb_tgl_jurnal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
        'Daniel
        'Me.tb_kurs.Text = ""
        'Daniel
        Me.id_jurnal = 0
    End Sub

    Sub loaddata()
        Try
            'Daniel
            'If Me.vid_jurnal <> 0 Then
            '    Me.id_jurnal = Me.vid_jurnal
            'End If

            'sqlcom = "select id, convert(char, tanggal, 103) as tanggal, id_currency, id_text, is_submit, isnull(kurs,0) as kurs, is_submit, keterangan"
            'sqlcom = sqlcom + " from jurnal_umum"
            'sqlcom = sqlcom + " where id = " & Me.id_jurnal

            sqlcom = "select [seq],[id_jurnal],convert(char, [tgl_jurnal], 103) as tgl_jurnal,[nama_jurnal],[keterangan],[id_transaction_period] from [akun_jurnal] where [id_jurnal] = '" & Me.vid & "'"
            'Daniel

            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_jurnal.Text = reader.Item("id_jurnal").ToString
                Me.tb_tgl_jurnal.Text = reader.Item("tgl_jurnal").ToString
                'Daniel
                'Me.dd_mata_uang.SelectedValue = reader.Item("id_currency").ToString
                'Me.tb_kurs.Text = reader.Item("kurs").ToString
                'Daniel
                Me.tb_keterangan.Text = reader.Item("keterangan").ToString

                'Daniel
                'If reader.Item("is_submit").ToString = "B" Then
                '    Me.btn_save.Enabled = True
                '    Me.btn_submit.Enabled = True
                '    Me.btn_add.Enabled = True
                '    Me.btn_update.Enabled = True
                '    Me.btn_delete.Enabled = True
                'Else
                '    Me.btn_save.Enabled = False
                '    Me.btn_submit.Enabled = False
                '    Me.btn_add.Enabled = False
                '    Me.btn_update.Enabled = False
                '    Me.btn_delete.Enabled = False
                'End If
                'Else
                '    Me.btn_submit.Enabled = False
                'Daniel
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.clearakun()
            Me.bindperiodetransaksi()
            'Daniel
            'Me.bindjenis_jurnal()
            'Me.bindmata_uang()
            'Me.tb_kurs.Text = 1
            'Daniel
            Me.id_user = HttpContext.Current.Session("UserID")
            Me.loaddata()
            Me.loadgrid()
            Me.tb_id_akun.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun.Attributes.Add("style", "display: none;")
            Me.link_popup_akun.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun.ClientID & "', '" & Me.link_refresh_akun.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/jurnal.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&vjenis_jurnal=" & Me.vjenis_jurnal)
    End Sub

    'Daniel
    'Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
    '    Try
    '        If String.IsNullOrEmpty(Me.tb_tgl_jurnal.Text) Then
    '            Me.lbl_msg.Text = "Silahkan mengisi tanggal jurnal terlebih dahulu"

    '        ElseIf String.IsNullOrEmpty(Me.tb_kurs.Text) Then
    '            Me.lbl_msg.Text = "Silahkan mengisi kurs terlebih dahulu"

    '        ElseIf String.IsNullOrEmpty(Me.tb_keterangan.Text) Then
    '            Me.lbl_msg.Text = "Silahkan mengisi keterangan terlebih dahulu"
    '        Else

    '            Dim vtgl As String = Me.tb_tgl_jurnal.Text.Substring(3, 2) & "/" & Me.tb_tgl_jurnal.Text.Substring(0, 2) & "/" & Me.tb_tgl_jurnal.Text.Substring(6, 4)

    '            If Me.id_jurnal = 0 Then
    '                Dim vmax As Integer = 0
    '                sqlcom = "select isnull(max(id),0) + 1 as vmax from jurnal_umum"
    '                reader = connection.koneksi.SelectRecord(sqlcom)
    '                reader.Read()
    '                If reader.HasRows Then
    '                    vmax = reader.Item("vmax").ToString
    '                End If
    '                reader.Close()
    '                connection.koneksi.CloseKoneksi()

    '                Dim vid_text As String = Nothing
    '                sqlcom = "select isnull(convert(int, max(right(id_text,5))),0) + 1 as vid_text from jurnal_umum"
    '                sqlcom = sqlcom + " where jenis_jurnal = '" & Me.vjenis_jurnal & "'"
    '                sqlcom = sqlcom + " and year(tanggal) = " & Year(Now.Date)
    '                reader = connection.koneksi.SelectRecord(sqlcom)
    '                reader.Read()
    '                If reader.HasRows Then
    '                    vid_text = Me.vjenis_jurnal + Me.vtahun.ToString + reader.Item("vid_text").ToString.PadLeft(5, "0")
    '                End If
    '                reader.Close()
    '                connection.koneksi.CloseKoneksi()

    '                sqlcom = "insert into jurnal_umum(id, tanggal, dibuat_oleh, id_transaction_period, id_currency, jenis_jurnal, is_submit,"
    '                sqlcom = sqlcom + " id_text, kurs, keterangan)"
    '                sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "'," & Me.id_user & "," & Me.vid_periode_transaksi & ","
    '                sqlcom = sqlcom + "'" & Me.dd_mata_uang.SelectedValue & "','" & Me.vjenis_jurnal & "','B','" & vid_text & "'"
    '                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_kurs.Text) & ",'" & Me.tb_keterangan.Text & "')"
    '                connection.koneksi.InsertRecord(sqlcom)
    '                Me.id_jurnal = vmax
    '                Me.lbl_msg.Text = "Data sudah disimpan"
    '            Else
    '                sqlcom = "update jurnal_umum"
    '                sqlcom = sqlcom + " set tanggal = '" & vtgl & "',"
    '                sqlcom = sqlcom + " id_currency = '" & Me.dd_mata_uang.SelectedValue & "',"
    '                sqlcom = sqlcom + " kurs = " & Decimal.ToDouble(Me.tb_kurs.Text) & ","
    '                sqlcom = sqlcom + " keterangan = '" & Me.tb_keterangan.Text & "'"
    '                sqlcom = sqlcom + " where id = " & Me.id_jurnal
    '                connection.koneksi.UpdateRecord(sqlcom)
    '                Me.lbl_msg.Text = "Data sudah diupdate"
    '            End If
    '            Me.loaddata()
    '        End If
    '    Catch ex As Exception
    '        Me.lbl_msg.Text = ex.Message
    '    End Try
    'End Sub
    'Daniel

    Protected Sub link_refresh_akun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun.Click
        Me.bindakun()
    End Sub

    Sub loadgrid()
        Try

            Me.clearakun()
            Me.tb_nilai.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            'Daniel
            'If Me.vid_jurnal <> 0 Then
            '    Me.id_jurnal = Me.vid_jurnal
            'End If


            'sqlcom = "select jurnal_item.id_jurnal, jurnal_item.account_code, isnull(jurnal_item.debet,0) as debet, isnull(jurnal_item.credit,0) as credit,"
            'sqlcom = sqlcom + " coa_list.inaname as nama_akun"
            'sqlcom = sqlcom + " from jurnal_item"
            'sqlcom = sqlcom + " inner join coa_list on coa_list.accountno = jurnal_item.account_code"
            'sqlcom = sqlcom + " where id_jurnal = " & Me.id_jurnal
            'sqlcom = sqlcom + " order by seq"
            'Daniel

            sqlcom = "select akun_general_ledger.seq, akun_general_ledger.coa_code, coa_list.inaname as nama_akun,isnull(akun_general_ledger.nilai_debet,0) as nilai_debet,isnull(akun_general_ledger.nilai_kredit,0) as nilai_kredit from akun_general_ledger,coa_list where akun_general_ledger.coa_code = coa_list.accountno and akun_general_ledger.id_transaksi = '" & Me.vid & "'"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "jurnal_umum")
                Me.dg_data.DataSource = ds.Tables("jurnal_umum").DefaultView

                If ds.Tables("jurnal_umum").Rows.Count > 0 Then
                    'If ds.Tables("jurnal_umum").Rows.Count > 10 Then
                    '    Me.dg_data.AllowPaging = True
                    '    Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                    '    Me.dg_data.PagerStyle.Position = PagerPosition.Top
                    '    Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                    '    Me.dg_data.PageSize = 10
                    'Else
                    '    Me.dg_data.AllowPaging = False
                    'End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_update.Visible = True
                    Me.btn_delete.Visible = True
                    'Daniel
                    Me.btn_save.Visible = False
                    'Daniel
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
                    'Daniel
                    If Me.lbl_no_jurnal.Text <> String.Empty Then
                        Me.btn_save.Visible = False
                    Else
                        Me.btn_save.Visible = True
                    End If
                    'Daniel
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Try
            'If Me.tb_id_akun.Text = "0" Then
            '    Me.lbl_msg.Text = "Silahkan mengisi nama akun terlebih dahulu"
            'ElseIf String.IsNullOrEmpty(Me.tb_nilai.Text) Then
            '    Me.lbl_msg.Text = "Silahkan mengisi nilai terlebih dahulu"
            'Else
            '    Dim vseq As Integer = 0

            '    sqlcom = "select isnull(max(seq),0) as vmax"
            '    sqlcom = sqlcom + " from jurnal_item"
            '    sqlcom = sqlcom + " where id_jurnal = " & Me.id_jurnal
            '    reader = connection.koneksi.SelectRecord(sqlcom)
            '    reader.Read()
            '    If reader.HasRows Then
            '        vseq = reader.Item("vmax").ToString
            '    End If
            '    reader.Close()
            '    connection.koneksi.CloseKoneksi()

            '    If vseq = 2 Then
            '        Me.lbl_msg.Text = "Jurnal tersebut hanya dapat terdiri dari 1 debet dan 1 kredit"
            '    Else
            '        sqlcom = "select isnull(max(seq),0) + 1 as vmax"
            '        sqlcom = sqlcom + " from jurnal_item"
            '        sqlcom = sqlcom + " where id_jurnal = " & Me.id_jurnal
            '        reader = connection.koneksi.SelectRecord(sqlcom)
            '        reader.Read()
            '        If reader.HasRows Then
            '            vseq = reader.Item("vmax").ToString
            '        End If
            '        reader.Close()
            '        connection.koneksi.CloseKoneksi()

            '        sqlcom = "insert into jurnal_item(id_jurnal, account_code, debet, credit, seq)"
            '        sqlcom = sqlcom + " values(" & Me.id_jurnal & ",'" & Me.tb_id_akun.Text.Trim & "'"

            '        If Me.dd_debet_kredit.SelectedValue = "D" Then
            '            sqlcom = sqlcom + "," & Decimal.ToDouble(tb_nilai.Text) & ",0, " & vseq & ")"
            '        Else
            '            sqlcom = sqlcom + ",0," & Decimal.ToDouble(tb_nilai.Text) & "," & vseq & ")"
            '        End If
            '        connection.koneksi.InsertRecord(sqlcom)





            '        Me.lbl_msg.Text = "Data sudah disimpan"
            '        Me.loadgrid()
            '    End If            
            'End If


            If Me.lbl_no_jurnal.Text <> String.Empty Then
                Dim keterangan As String = Me.tb_keterangan.Text.Trim()

                If Me.dd_debet_kredit.SelectedValue = "D" Then
                    Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, Me.lbl_no_jurnal.Text, Me.tradingClass.DateValidated(Me.tb_tgl_jurnal.Text), Me.tb_id_akun.Text.Trim, String.Empty, Decimal.ToDouble(tb_nilai.Text), 0, keterangan, Me.vid_periode_transaksi, "IDR", 1, 0, 0, String.Empty)
                Else
                    Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, Me.lbl_no_jurnal.Text, Me.tradingClass.DateValidated(Me.tb_tgl_jurnal.Text), Me.tb_id_akun.Text.Trim, String.Empty, 0, Decimal.ToDouble(tb_nilai.Text), keterangan, Me.vid_periode_transaksi, "IDR", 1, 0, 0, String.Empty)
                End If

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
                    'Daniel
                    'sqlcom = "delete jurnal_item"
                    'sqlcom = sqlcom + " where id_jurnal = " & Me.id_jurnal
                    'sqlcom = sqlcom + " and account_code = '" & CType(Me.dg_data.Items(x).FindControl("lbl_kode_akun"), Label).Text & "'"
                    sqlcom = "delete akun_general_ledger"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    'Daniel
                    connection.koneksi.DeleteRecord(sqlcom)
                    tradingClass.Alert("Data sudah dihapus", Me.Page)
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
                    'Daniel
                    'If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_debet"), TextBox).Text) Or String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_kredit"), TextBox).Text) = 0 Then
                    '    Me.lbl_msg.Text = "Silahkan mengisi nilai debet atau kredit terlebih dahulu"
                    'ElseIf Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_debet"), TextBox).Text) = 0 And Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kredit"), TextBox).Text) = 0 Then
                    '    Me.lbl_msg.Text = "Silahkan mengisi nilai debet atau kredit terlebih dahulu"
                    'ElseIf Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_debet"), TextBox).Text) <> 0 And Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kredit"), TextBox).Text) <> 0 Then
                    '    Me.lbl_msg.Text = "Anda hanya diperbolehkan untuk mengisi nilai debet atau kredit, tidak boleh kedua-duanya"
                    'Else
                    'Daniel
                    'sqlcom = "update jurnal_item"
                    'sqlcom = sqlcom + " set debet = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_debet"), TextBox).Text) & ","
                    'sqlcom = sqlcom + " credit = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kredit"), TextBox).Text)
                    'sqlcom = sqlcom + " where id_jurnal = " & Me.id_jurnal
                    'sqlcom = sqlcom + " and account_code = '" & CType(Me.dg_data.Items(x).FindControl("lbl_kode_akun"), Label).Text & "'"
                    sqlcom = "update akun_general_ledger"
                    sqlcom = sqlcom + " set nilai_debet = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_debet"), TextBox).Text) & ","
                    sqlcom = sqlcom + " nilai_kredit = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kredit"), TextBox).Text)
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    'Daniel
                    connection.koneksi.UpdateRecord(sqlcom)
                    tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
                'Daniel
                'End If
                'Daniel
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

    Sub checkakunitem()
        Try
            sqlcom = "select account_code, isnull(debet,0) as debet from jurnal_item"
            sqlcom = sqlcom + " where id_jurnal = " & Me.id_jurnal
            sqlcom = sqlcom + " and debet > 0"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_debet = reader.Item("account_code").ToString
                Me.vnilai_debet = reader.Item("debet").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            sqlcom = "select account_code, isnull(credit,0) as kredit from jurnal_item"
            sqlcom = sqlcom + " where id_jurnal = " & Me.id_jurnal
            sqlcom = sqlcom + " and credit > 0"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vakun_kredit = reader.Item("account_code").ToString
                Me.vnilai_kredit = reader.Item("kredit").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub checkkasbank_debet()
        'Try
        '    Dim vtgl As String = Me.tb_tgl_jurnal.Text.Substring(3, 2) & "/" & Me.tb_tgl_jurnal.Text.Substring(0, 2) & "/" & Me.tb_tgl_jurnal.Text.Substring(6, 4)

        '    Dim readerkas As SqlClient.SqlDataReader
        '    sqlcom = "select id from bank_account where account_code ="
        '    sqlcom = sqlcom + " (select account_code from jurnal_item"
        '    sqlcom = sqlcom + " where id_jurnal = " & Me.id_jurnal
        '    sqlcom = sqlcom + " and debet > 0)"
        '    readerkas = connection.koneksi.SelectRecord(sqlcom)
        '    readerkas.Read()
        '    If readerkas.HasRows Then
        '        sqlcom = "update bank_account"
        '        sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) + " & Decimal.ToDouble(Me.vnilai_debet)
        '        sqlcom = sqlcom + " where id = " & readerkas.Item("id").ToString
        '        connection.koneksi.UpdateRecord(sqlcom)

        '        Dim vseq As Integer = 0
        '        sqlcom = "select isnull(max(seq),0) + 1 as vseq from history_kas"
        '        reader = connection.koneksi.SelectRecord(sqlcom)
        '        reader.Read()
        '        If reader.HasRows Then
        '            vseq = reader.Item("vseq").ToString
        '        End If
        '        reader.Close()
        '        connection.koneksi.CloseKoneksi()

        '        sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
        '        sqlcom = sqlcom + " values(" & Me.vid_periode_transaksi & "," & readerkas.Item("id").ToString & ",'" & vtgl & "',"
        '        sqlcom = sqlcom + "'" & Me.tb_keterangan.Text + " - " + Me.lbl_jenis_jurnal.Text + " (Jurnal no. " & Me.lbl_no_jurnal.Text & ")'," & Decimal.ToDouble(Me.vnilai_debet)
        '        sqlcom = sqlcom + ",0, " & vseq & ")"
        '        connection.koneksi.InsertRecord(sqlcom)
        '    End If
        '    readerkas.Close()
        '    connection.koneksi.CloseKoneksi()

        'Catch ex As Exception
        '    Me.lbl_msg.Text = ex.Message
        'End Try
    End Sub

    Sub checkkasbank_kredit()
        'Try
        '    Dim vtgl As String = Me.tb_tgl_jurnal.Text.Substring(3, 2) & "/" & Me.tb_tgl_jurnal.Text.Substring(0, 2) & "/" & Me.tb_tgl_jurnal.Text.Substring(6, 4)

        '    Dim readerkas As SqlClient.SqlDataReader
        '    sqlcom = "select id from bank_account where account_code ="
        '    sqlcom = sqlcom + " (select account_code from jurnal_item"
        '    sqlcom = sqlcom + " where id_jurnal = " & Me.id_jurnal
        '    sqlcom = sqlcom + " and credit > 0)"
        '    readerkas = connection.koneksi.SelectRecord(sqlcom)
        '    readerkas.Read()
        '    If readerkas.HasRows Then
        '        sqlcom = "update bank_account"
        '        sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - " & Decimal.ToDouble(Me.vnilai_debet)
        '        sqlcom = sqlcom + " where id = " & readerkas.Item("id").ToString
        '        connection.koneksi.UpdateRecord(sqlcom)

        '        Dim vseq As Integer = 0
        '        sqlcom = "select isnull(max(seq),0) + 1 as vseq from history_kas"
        '        reader = connection.koneksi.SelectRecord(sqlcom)
        '        reader.Read()
        '        If reader.HasRows Then
        '            vseq = reader.Item("vseq").ToString
        '        End If
        '        reader.Close()
        '        connection.koneksi.CloseKoneksi()

        '        sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
        '        sqlcom = sqlcom + " values(" & Me.vid_periode_transaksi & "," & readerkas.Item("id").ToString & ",'" & vtgl & "',"
        '        sqlcom = sqlcom + "'" & Me.lbl_jenis_jurnal.Text & " (Jurnal no. " & Me.lbl_no_jurnal.Text & ")',0," & Decimal.ToDouble(Me.vnilai_debet)
        '        sqlcom = sqlcom + "," & vseq & ")"
        '        connection.koneksi.InsertRecord(sqlcom)
        '    End If
        '    readerkas.Close()
        '    connection.koneksi.CloseKoneksi()

        'Catch ex As Exception
        '    Me.lbl_msg.Text = ex.Message
        'End Try
    End Sub

    'Sub jurnal()        
    '    Dim vtgl As String = Me.tb_tgl_jurnal.Text.Substring(3, 2) & "/" & Me.tb_tgl_jurnal.Text.Substring(0, 2) & "/" & Me.tb_tgl_jurnal.Text.Substring(6, 4)

    '    'debet
    '    Me.seq_max()
    '    sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
    '    sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
    '    sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.id_jurnal & "','" & vtgl & "','JURNAL',"
    '    sqlcom = sqlcom + "'" & Me.vakun_debet & "',"
    '    sqlcom = sqlcom + "'" & Me.vakun_kredit & "'," & Decimal.ToDouble(Me.vnilai_debet) & ",0, '" & Me.lbl_jenis_jurnal.Text & " (Jurnal no. " & Me.lbl_no_jurnal.Text & ")'"
    '    sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
    '    connection.koneksi.InsertRecord(sqlcom)

    '    'kredit
    '    Me.seq_max()
    '    sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
    '    sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
    '    sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.id_jurnal & "','" & vtgl & "','JURNAL',"
    '    sqlcom = sqlcom + "'" & Me.vakun_kredit & "',"
    '    sqlcom = sqlcom + "'" & Me.vakun_debet & "',0," & Decimal.ToDouble(Me.vnilai_kredit) & ",'" & Me.lbl_jenis_jurnal.Text & " (Jurnal no. " & Me.lbl_no_jurnal.Text & ")'"
    '    sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
    '    connection.koneksi.InsertRecord(sqlcom)
    'End Sub

    'Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
    '    Try
    '        Dim readersubmit As SqlClient.SqlDataReader
    '        sqlcom = "select sum(isnull(debet,0)) as debet, sum(isnull(credit,0)) as kredit"
    '        sqlcom = sqlcom + " from jurnal_item"
    '        sqlcom = sqlcom + " where id_jurnal = " & Me.id_jurnal
    '        readersubmit = connection.koneksi.SelectRecord(sqlcom)
    '        readersubmit.Read()
    '        If Not readersubmit.HasRows Then
    '            Me.lbl_msg.Text = "Jurnal tersebut belum ada item jurnalnya"
    '        Else
    '            If Decimal.ToDouble(readersubmit.Item("debet").ToString) <> Decimal.ToDouble(readersubmit.Item("kredit").ToString) Then
    '                Me.lbl_msg.Text = "Nilai debet dan kredit pada jurnal tersebut belum sama"
    '            Else
    '                sqlcom = "update jurnal_umum"
    '                sqlcom = sqlcom + " set is_submit = 'S'"
    '                sqlcom = sqlcom + " where id = " & Me.id_jurnal
    '                connection.koneksi.UpdateRecord(sqlcom)
    '                Me.checkakunitem()
    '                Me.checkkasbank_debet()
    '                Me.checkkasbank_kredit()
    '                'Daniel
    '                'Me.jurnal()
    '                Me.GL()
    '                'Daniel
    '                Me.lbl_msg.Text = "Data sudah disubmit dan tidak dapat diubah kembali"
    '                Me.loaddata()
    '                Me.loadgrid()
    '            End If
    '        End If
    '        readersubmit.Close()
    '        connection.koneksi.CloseKoneksi()
    '    Catch ex As Exception
    '        Me.lbl_msg.Text = ex.Message
    '    End Try
    'End Sub

    'Daniel
    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            Dim id As String = Me.tradingClass.IDJurnalMax
            Dim keterangan As String = Me.tb_keterangan.Text.Trim()
            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_jurnal.Text), id, Me.tradingClass.JurnalType("5"), keterangan, Me.vid_periode_transaksi)
            tradingClass.Alert("Data sudah disimpan", Me.Page)
            Response.Redirect("~/detil_jurnal.aspx?vbulan=" & Me.vbulan & "&vtahun=" & Me.vtahun & "&vid=" & id)
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
    'Daniel
End Class
