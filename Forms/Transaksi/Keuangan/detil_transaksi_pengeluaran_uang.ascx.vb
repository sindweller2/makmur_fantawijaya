Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_transaksi_pengeluaran_uang
    Inherits System.Web.UI.UserControl

    Private ReadOnly Property vpaging() As Integer
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
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

    Private ReadOnly Property vid() As Integer
        Get
            Dim o As Object = Request.QueryString("vid")
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

    Sub binditem_biaya()
        sqlcom = "select id, name from item_biaya order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_biaya.DataSource = reader
        Me.dd_biaya.DataTextField = "name"
        Me.dd_biaya.DataValueField = "id"
        Me.dd_biaya.DataBind()
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
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub clearform()
        Me.tb_keterangan.Text = ""
        Me.id_transaksi = 0
    End Sub

    Sub loaddata()

        If Me.vid <> 0 Then
            Me.id_transaksi = Me.vid
        End If

        sqlcom = "select id, convert(char, tanggal, 103) as tanggal, id_cash_account, is_submit, keterangan, "
        sqlcom = sqlcom + " no_giro, convert(char, tgl_giro, 103) as tgl_giro, convert(char, tgl_jatuh_tempo, 103) as tgl_jatuh_tempo"
        sqlcom = sqlcom + " from pengeluaran_uang"
        sqlcom = sqlcom + " where id = " & Me.id_transaksi
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_no_transaksi.Text = reader.Item("id").ToString
            Me.lbl_tgl_transaksi.Text = reader.Item("tanggal").ToString
            Me.dd_kas_petty_cash.SelectedValue = reader.Item("id_cash_account").ToString
            Me.tb_no_giro.Text = reader.Item("no_giro").ToString
            Me.tb_tgl_giro.Text = reader.Item("tgl_giro").ToString
            Me.tb_jatuh_tempo.Text = reader.Item("tgl_jatuh_tempo").ToString
            Me.tb_keterangan_header.Text = reader.Item("keterangan").ToString
            Me.tbl_petty_cash.Visible = True

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
            Me.tbl_petty_cash.Visible = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.bindperiodetransaksi()
            Me.bindcashaccount()
            Me.bindmatauang()
            Me.binditem_biaya()
            Me.loaddata()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/daftar_transaksi_pengeluaran_uang.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&vpaging=" & Me.vpaging)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            Dim vtgl_giro As String = ""
            Dim vtgl_jatuh_tempo As String = ""

            If Not String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                vtgl_giro = Me.tb_tgl_giro.Text.Substring(3, 2) & "/" & Me.tb_tgl_giro.Text.Substring(0, 2) & "/" & Me.tb_tgl_giro.Text.Substring(6, 4)
            End If

            If Not String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                vtgl_jatuh_tempo = Me.tb_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_jatuh_tempo.Text.Substring(6, 4)
            End If

            If Me.id_transaksi = 0 Then

                Dim vmax As String = ""
                sqlcom = "select isnull(max(convert(int, right(id, 5))),0) + 1 as vid"
                sqlcom = sqlcom + " from pengeluaran_uang"
                sqlcom = sqlcom + " where convert(int, substring(convert(char, id), 3,2)) = " & Me.vbulan
                sqlcom = sqlcom + " and convert(int, left(id, 2)) = " & Right(Me.vtahun, 2)
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = Right(Me.vtahun, 2) & Me.vbulan.ToString.PadLeft(2, "0") & reader.Item("vid").ToString.PadLeft(5, "0")
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vtgl As String = Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Year

                sqlcom = "insert into pengeluaran_uang(id, tanggal, id_cash_account, id_transaction_period, is_submit,"
                sqlcom = sqlcom + " no_giro, tgl_giro, tgl_jatuh_tempo)"
                sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "'," & Me.dd_kas_petty_cash.SelectedValue
                sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'B','" & Me.tb_no_giro.Text & "',"

                If String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                    sqlcom = sqlcom + " NULL,"
                Else
                    sqlcom = sqlcom + "'" & vtgl_giro & "',"
                End If

                If String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                    sqlcom = sqlcom + " NULL)"
                Else
                    sqlcom = sqlcom + "'" & vtgl_jatuh_tempo & "')"
                End If

                connection.koneksi.InsertRecord(sqlcom)
                Me.id_transaksi = vmax
                Me.lbl_msg.Text = "Data sudah disimpan"
            Else
                sqlcom = "update pengeluaran_uang"
                sqlcom = sqlcom + " set id_cash_account = " & Me.dd_kas_petty_cash.SelectedValue & ","
                sqlcom = sqlcom + " no_giro = '" & Me.tb_no_giro.Text & "',"

                If String.IsNullOrEmpty(Me.tb_tgl_giro.Text) Then
                    sqlcom = sqlcom + " tgl_giro = NULL,"
                Else
                    sqlcom = sqlcom + " tgl_giro = '" & vtgl_giro & "',"
                End If

                If String.IsNullOrEmpty(Me.tb_jatuh_tempo.Text) Then
                    sqlcom = sqlcom + " tgl_jatuh_tempo = NULL"
                Else
                    sqlcom = sqlcom + " tgl_jatuh_tempo = '" & vtgl_jatuh_tempo & "'"
                End If

                sqlcom = sqlcom + " where id = " & Me.id_transaksi
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah diupdate"
            End If
            Me.loaddata()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try

            Me.tb_jumlah.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select pengeluaran_uang_biaya.id_pengeluaran, pengeluaran_uang_biaya.id_item_biaya, pengeluaran_uang_biaya.jumlah,"
            sqlcom = sqlcom + " pengeluaran_uang_biaya.keterangan, item_biaya.name as nama_biaya"
            sqlcom = sqlcom + " from pengeluaran_uang_biaya"
            sqlcom = sqlcom + " inner join item_biaya on item_biaya.id = pengeluaran_uang_biaya.id_item_biaya"
            sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
            sqlcom = sqlcom + " order by item_biaya.name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "pengeluaran_uang_biaya")
                Me.dg_data.DataSource = ds.Tables("pengeluaran_uang_biaya").DefaultView

                If ds.Tables("pengeluaran_uang_biaya").Rows.Count > 0 Then
                    If ds.Tables("pengeluaran_uang_biaya").Rows.Count > 10 Then
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
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bindtotal()
        Try
            sqlcom = "select isnull(sum(isnull(jumlah,0)),0) as total_nilai from pengeluaran_uang_biaya"
            sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_total_nilai.Text = FormatNumber(reader.Item("total_nilai").ToString, 2)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Try
            If String.IsNullOrEmpty(Me.tb_keterangan.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi keterangan terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_jumlah.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah nilai terlebih dahulu"
            Else
                sqlcom = "insert into pengeluaran_uang_biaya(id_pengeluaran, id_item_biaya, jumlah, keterangan)"
                sqlcom = sqlcom + " values (" & Me.id_transaksi & "," & Me.dd_biaya.SelectedValue & "," & Decimal.ToDouble(Me.tb_jumlah.Text) & ","
                sqlcom = sqlcom + "'" & Me.tb_keterangan.Text & "')"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete pengeluaran_uang_biaya"
                    sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
                    sqlcom = sqlcom + " and id_item_biaya = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_biaya"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah dihapus"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.lbl_msg.Text = "Data masih digunakan di form lain"
            Else
                Me.lbl_msg.Text = ex.Message
            End If
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update pengeluaran_uang_biaya"
                    sqlcom = sqlcom + " set jumlah = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_jumlah"), TextBox).Text) & ","
                    sqlcom = sqlcom + " keterangan = '" & CType(Me.dg_data.Items(x).FindControl("tb_keterangan"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
                    sqlcom = sqlcom + " and id_item_biaya = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_biaya"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
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

    Sub insert_history_kas()
        Try
            Dim vtgl As String = Me.lbl_tgl_transaksi.Text.Substring(3, 2) & "/" & Me.lbl_tgl_transaksi.Text.Substring(0, 2) & "/" & Me.lbl_tgl_transaksi.Text.Substring(6, 4)

            ' nilai kredit
            Me.max_seq_history_kas()
            sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
            sqlcom = sqlcom + " values(" & Me.vid_periode_transaksi & "," & Me.dd_kas_petty_cash.SelectedValue & ",'" & vtgl & "',"
            sqlcom = sqlcom + "'Transaksi Pengeluaran Uang. " & Me.lbl_no_transaksi.Text & "'"
            sqlcom = sqlcom + ",0," & Decimal.ToDouble(Me.vtotal_biaya) & "," & Me.vmax_history_kas & ")"
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub update_kas()
        Try
            sqlcom = "update bank_account"
            sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - isnull(" & Decimal.ToDouble(Me.vtotal_biaya) & ",0)"
            sqlcom = sqlcom + " where id = " & Me.dd_kas_petty_cash.SelectedValue
            connection.koneksi.UpdateRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try    
    End Sub

    Sub jurnal()
        Dim vakun_kas As String = ""
        Dim vakun_biaya As String = ""

        Dim vtgl As String = Me.lbl_tgl_transaksi.Text.Substring(3, 2) & "/" & Me.lbl_tgl_transaksi.Text.Substring(0, 2) & "/" & Me.lbl_tgl_transaksi.Text.Substring(6, 4)

        'akun kas
        sqlcom = "select account_code"
        sqlcom = sqlcom + " from bank_account"
        sqlcom = sqlcom + " where id = " & Me.dd_kas_petty_cash.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            vakun_kas = reader.Item("account_code").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        Dim readeritem As SqlClient.SqlDataReader
        sqlcom = "select item_biaya.account_code, isnull(pengeluaran_uang_biaya.jumlah,0) as jumlah, keterangan"
        sqlcom = sqlcom + " from pengeluaran_uang_biaya"
        sqlcom = sqlcom + " inner join item_biaya on item_biaya.id = pengeluaran_uang_biaya.id_item_biaya"
        sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
        readeritem = connection.koneksi.SelectRecord(sqlcom)
        Do While readeritem.Read
            'debet
            ' biaya
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl & "','KLRUANG','" & readeritem.Item("account_code").ToString & "',"
            sqlcom = sqlcom + "'" & vakun_kas & "'," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) & ",0,'" & readeritem.Item("keterangan").ToString & " (Transaksi Pengeluaran Uang. " & Me.lbl_no_transaksi.Text & ")'"
            sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.lbl_mata_uang.Text & "',1)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' kas
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl & "','KLRUANG','" & vakun_kas & "',"
            sqlcom = sqlcom + "'" & readeritem.Item("account_code").ToString & "',0," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) & ",'" & readeritem.Item("keterangan").ToString & " (Transaksi Pengeluaran Uang. " & Me.lbl_no_transaksi.Text & ")'"
            sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'" & Me.lbl_mata_uang.Text & "',1)"
            connection.koneksi.InsertRecord(sqlcom)
        Loop
        readeritem.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            'cek saldo
            Dim vsaldo_akhir As Decimal = 0
            sqlcom = "select isnull(saldo_akhir,0) as saldo_akhir"
            sqlcom = sqlcom + " from bank_account"
            sqlcom = sqlcom + " where id = " & Me.dd_kas_petty_cash.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                vsaldo_akhir = reader.Item("saldo_akhir").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            'cek total biaya
            sqlcom = "select isnull(sum(isnull(jumlah,0)),0) as jumlah_biaya"
            sqlcom = sqlcom + " from pengeluaran_uang_biaya"
            sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vtotal_biaya = reader.Item("jumlah_biaya").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If Decimal.ToDouble(vsaldo_akhir) < Decimal.ToDouble(Me.vtotal_biaya) Then
                Me.lbl_msg.Text = "Total saldo kas tersebut tidak mencukupi"
            Else
                sqlcom = "update pengeluaran_uang"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where id = " & Me.id_transaksi
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data tersebut sudah disubmit dan tidak dapat diubah kembali"
                Me.insert_history_kas()
                Me.update_kas()
                'Me.jurnal()
                Me.loaddata()
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dd_kas_petty_cash_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_kas_petty_cash.SelectedIndexChanged
        Me.bindmatauang()
    End Sub
End Class
