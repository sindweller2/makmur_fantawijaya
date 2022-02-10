Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Collection_detil_transaksi_petty_cash
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()

    Sub GL()
        Try
            Dim vakun_kas As String = Nothing

            sqlcom = "select account_code from bank_account where id = " & Me.dd_kas_petty_cash.SelectedValue
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                vakun_kas = reader.Item("account_code").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            Dim id As String = Me.tradingClass.IDTransaksiMax
            Dim keterangan As String = "Transaksi Petty Cash no. " & Me.lbl_no_transaksi.Text

            Me.tradingClass.InsertAkunJurnal(Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), id, Me.tradingClass.JurnalType("4"), keterangan, Me.vid_periode_transaksi)

            Dim readeritem As SqlClient.SqlDataReader
            sqlcom = "select item_biaya.account_code, isnull(pengeluaran_petty_cash_biaya.jumlah,0) as jumlah, keterangan from pengeluaran_petty_cash_biaya inner join item_biaya on item_biaya.id = pengeluaran_petty_cash_biaya.id_item_biaya where id_pengeluaran = " & Me.id_transaksi
            readeritem = connection.koneksi.SelectRecord(sqlcom)
            Do While readeritem.Read

                Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), readeritem.Item("account_code").ToString, vakun_kas, System.Convert.ToDecimal(readeritem.Item("jumlah").ToString), 0, keterangan, Me.vid_periode_transaksi, "IDR", 1, 0, 0, String.Empty)
                Me.tradingClass.InsertAkunGeneralLedger(Me.tradingClass.SeqMax, id, Me.tradingClass.DateValidated(Me.tb_tgl_transaksi.Text), vakun_kas, readeritem.Item("account_code").ToString, 0, System.Convert.ToDecimal(readeritem.Item("jumlah").ToString), keterangan, Me.vid_periode_transaksi, "IDR", 1, 0, 0, String.Empty)

            Loop
            readeritem.Close()
            connection.koneksi.CloseKoneksi()

            Me.tradingClass.Alert("Data sudah disubmit!", Me.Page)
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
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

    Private ReadOnly Property vid() As Integer
        Get
            Dim o As Object = Request.QueryString("vid")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vpaging() As String
        Get
            Dim o As Object = Request.QueryString("vpaging")
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

    Public Property vketerangan() As Decimal
        Get
            Dim o As Object = ViewState("vketerangan")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vketerangan") = value
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
        sqlcom = sqlcom + " where is_petty_cash = 'Y'"
        sqlcom = sqlcom + " and id in (8,9)"
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

    Sub clearform()
        Me.tb_keterangan.Text = ""
        Me.id_transaksi = 0
    End Sub

    Sub loaddata()

        If Me.vid <> 0 Then
            Me.id_transaksi = Me.vid
        End If

        sqlcom = "select id, convert(char, tanggal, 103) as tanggal, id_cash_account, is_submit, keterangan"
        sqlcom = sqlcom + " from pengeluaran_petty_cash"
        sqlcom = sqlcom + " where id = " & Me.id_transaksi
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_no_transaksi.Text = reader.Item("id").ToString
            Me.tb_tgl_transaksi.Text = reader.Item("tanggal").ToString
            Me.dd_kas_petty_cash.SelectedValue = reader.Item("id_cash_account").ToString
            Me.tb_keterangan_header.text = reader.item("keterangan").toString
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

        Me.tb_keterangan.Text = Me.tb_keterangan_header.text
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.bindperiodetransaksi()
            Me.bindcashaccount()
            Me.binditem_biaya()
            Me.dd_kas_petty_cash.SelectedValue = 8
            Me.loaddata()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/petty_cash_transaction.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&vpaging=" & Me.vpaging)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try            
            if string.isnullorEmpty(me.tb_tgl_transaksi.text) then
               me.lbl_msg.text = "Silahkan mengisi tanggal terlebih dahulu"
            elseif string.isnullorEmpty(me.tb_keterangan_header.text) then
               me.lbl_msg.text = "Silahkan mengisi keterangan terlebih dahulu"
            else

                Dim vtgl As String = Me.tb_tgl_transaksi.Text.SubString(3,2) & "/" & Me.tb_tgl_transaksi.Text.SubString(0,2) & "/" & Me.tb_tgl_transaksi.Text.Substring(6,4)
                If Me.id_transaksi = 0 Then
                   Dim vmax As String = ""
                   sqlcom = "select isnull(max(convert(int, right(id, 5))),0) + 1 as vid"
                   sqlcom = sqlcom + " from pengeluaran_petty_cash"
                   sqlcom = sqlcom + " where convert(int, substring(convert(char, id), 3,2)) = " & Me.vbulan
                   sqlcom = sqlcom + " and convert(int, left(id, 2)) = " & Right(Me.vtahun, 2)
                   reader = connection.koneksi.SelectRecord(sqlcom)
                   reader.Read()
                   If reader.HasRows Then
                       vmax = Right(Me.vtahun, 2) & Me.vbulan.ToString.PadLeft(2, "0") & reader.Item("vid").ToString.PadLeft(5, "0")
                   End If
                   reader.Close()
                   connection.koneksi.CloseKoneksi()


                   sqlcom = "insert into pengeluaran_petty_cash(id, tanggal, id_cash_account, id_transaction_period, is_submit, keterangan)"
                   sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "'," & Me.dd_kas_petty_cash.SelectedValue
                   sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'B','" & me.tb_keterangan_header.text & "')"
                   connection.koneksi.InsertRecord(sqlcom)
                   Me.id_transaksi = vmax
                    Me.tradingClass.Alert("Data sudah disimpan", Me.Page)
                Else
                   sqlcom = "update pengeluaran_petty_cash"
                   sqlcom = sqlcom + " set id_cash_account = " & Me.dd_kas_petty_cash.SelectedValue & ","
                   sqlcom = sqlcom + " tanggal = '" & vtgl & "',"
                   sqlcom = sqlcom + " keterangan = '" & me.tb_keterangan_header.text & "'"
                   sqlcom = sqlcom + " where id = " & Me.id_transaksi
                    connection.koneksi.UpdateRecord(sqlcom)

                    Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
               End If
               Me.loaddata()
            end if
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try

            Me.tb_jumlah.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select pengeluaran_petty_cash_biaya.id_pengeluaran, pengeluaran_petty_cash_biaya.id_item_biaya,"
            sqlcom = sqlcom + " isnull(pengeluaran_petty_cash_biaya.jumlah,0) as jumlah,"
            sqlcom = sqlcom + " pengeluaran_petty_cash_biaya.keterangan, item_biaya.name as nama_biaya"
            sqlcom = sqlcom + " from pengeluaran_petty_cash_biaya"
            sqlcom = sqlcom + " inner join item_biaya on item_biaya.id = pengeluaran_petty_cash_biaya.id_item_biaya"
            sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
            sqlcom = sqlcom + " order by item_biaya.name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "pengeluaran_petty_cash_biaya")
                Me.dg_data.DataSource = ds.Tables("pengeluaran_petty_cash_biaya").DefaultView

                If ds.Tables("pengeluaran_petty_cash_biaya").Rows.Count > 0 Then
                    If ds.Tables("pengeluaran_petty_cash_biaya").Rows.Count > 10 Then
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
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub bindtotal()
        Try
            sqlcom = "select isnull(sum(isnull(jumlah,0)),0) as total_nilai from pengeluaran_petty_cash_biaya"
            sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_total_nilai.Text = FormatNumber(reader.Item("total_nilai").ToString, 2)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Try
            If String.IsNullOrEmpty(Me.tb_jumlah.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah nilai terlebih dahulu"
            Else
                sqlcom = "insert into pengeluaran_petty_cash_biaya(id_pengeluaran, id_item_biaya, jumlah, keterangan)"
                sqlcom = sqlcom + " values (" & Me.id_transaksi & "," & Me.dd_biaya.SelectedValue & "," & Decimal.ToDouble(Me.tb_jumlah.Text) & ","
                sqlcom = sqlcom + "'" & Me.tb_keterangan.Text & "')"
                connection.koneksi.InsertRecord(sqlcom)
                Me.tradingClass.Alert("Data sudah disimpan", Me.Page)
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete pengeluaran_petty_cash_biaya"
                    sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
                    sqlcom = sqlcom + " and id_item_biaya = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_biaya"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah dihapus", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.tradingClass.Alert("Data masih digunakan di form lain", Me.Page)
            Else
                Me.tradingClass.Alert(ex.Message, Me.Page)
            End If
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update pengeluaran_petty_cash_biaya"
                    sqlcom = sqlcom + " set keterangan = '" & CType(Me.dg_data.Items(x).FindControl("tb_keterangan"), TextBox).Text & "',"
                    sqlcom = sqlcom + " jumlah = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_jumlah"), TextBox).Text)
                    sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
                    sqlcom = sqlcom + " and id_item_biaya = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_biaya"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
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

        Dim vtgl As String = Me.tb_tgl_transaksi.Text.Substring(3, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(0, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(6, 4)

        ' nilai kredit
        Me.max_seq_history_kas()
        sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
        sqlcom = sqlcom + " values(" & Me.vid_periode_transaksi & "," & Me.dd_kas_petty_cash.SelectedValue & ",'" & vtgl & "'"
        sqlcom = sqlcom + ",'Transaksi Petty Cash. " & Me.lbl_no_transaksi.Text & " ( " & Me.tb_keterangan_header.text & " )'"
        sqlcom = sqlcom + ",0," & Decimal.ToDouble(Me.vtotal_biaya) & "," & Me.vmax_history_kas & ")"
        connection.koneksi.InsertRecord(sqlcom)
    End Sub

    Sub update_kas()
        sqlcom = "update bank_account"
        sqlcom = sqlcom + " set saldo_akhir = isnull(saldo_akhir,0) - " & Decimal.ToDouble(Me.vtotal_biaya)
        sqlcom = sqlcom + " where id = " & Me.dd_kas_petty_cash.SelectedValue
        connection.koneksi.UpdateRecord(sqlcom)
    End Sub

    Sub jurnal()
        Dim vakun_kas As String = ""
        Dim vakun_biaya As String = ""

        Dim vtgl As String = Me.tb_tgl_transaksi.Text.Substring(3, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(0, 2) & "/" & Me.tb_tgl_transaksi.Text.Substring(6, 4)

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
        sqlcom = "select item_biaya.account_code, isnull(pengeluaran_petty_cash_biaya.jumlah,0) as jumlah, keterangan"
        sqlcom = sqlcom + " from pengeluaran_petty_cash_biaya"
        sqlcom = sqlcom + " inner join item_biaya on item_biaya.id = pengeluaran_petty_cash_biaya.id_item_biaya"
        sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
        readeritem = connection.koneksi.SelectRecord(sqlcom)
        Do While readeritem.Read
            'debet
            ' biaya
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl & "','TRANPETTY','" & readeritem.Item("account_code").ToString & "',"
            sqlcom = sqlcom + "'" & vakun_kas & "'," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) & ",0,"
            sqlcom = sqlcom + "'" & readeritem.Item("keterangan").ToString & " (Transaksi Petty Cash. " & Me.lbl_no_transaksi.Text & ")'"
            sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'IDR',1)"
            connection.koneksi.InsertRecord(sqlcom)

            'kredit
            ' kas
            Me.seq_max()
            sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
            sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.lbl_no_transaksi.Text & "','" & vtgl & "','TRANPETTY','" & vakun_kas & "',"
            sqlcom = sqlcom + "'" & readeritem.Item("account_code").ToString & "',0," & Decimal.ToDouble(readeritem.Item("jumlah").ToString) & ","
            sqlcom = sqlcom + "'" & readeritem.Item("keterangan").ToString & " (Transaksi Petty Cash. " & Me.lbl_no_transaksi.Text & ")'"
            sqlcom = sqlcom + "," & Me.vid_periode_transaksi & ",'IDR',1)"
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
                vsaldo_akhir = reader.Item("saldo_akhir").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            'cek total biaya
            sqlcom = "select isnull(sum(isnull(jumlah,0)),0) as jumlah_biaya"
            sqlcom = sqlcom + " from pengeluaran_petty_cash_biaya"
            sqlcom = sqlcom + " where id_pengeluaran = " & Me.id_transaksi
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vtotal_biaya = reader.Item("jumlah_biaya").ToString.Trim
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If Decimal.ToDouble(vsaldo_akhir) < Decimal.ToDouble(Me.vtotal_biaya) Then
                Me.lbl_msg.Text = "Total saldo kas tersebut tidak mencukupi"
            Else
                sqlcom = "update pengeluaran_petty_cash"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where id = " & Me.id_transaksi
                connection.koneksi.UpdateRecord(sqlcom)
                Me.insert_history_kas()
                Me.update_kas()
                'Daniel
                Me.GL()
                'Me.jurnal()
                'Daniel
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class
