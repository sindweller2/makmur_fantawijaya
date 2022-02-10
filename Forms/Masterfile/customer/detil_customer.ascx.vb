Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_customer_detil_customer
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()
    'Daniel

    Private ReadOnly Property vpaging() As String
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Private ReadOnly Property vid_customer() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_customer")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property id_customer() As Integer
        Get
            Dim o As Object = ViewState("id_customer")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_customer") = value
        End Set
    End Property

    Public Property vakun_giro_mundur() As String
        Get
            Dim o As Object = ViewState("vakun_giro_mundur")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_giro_mundur") = value
        End Set
    End Property

    Public Property vakun_piutang_dagang() As String
        Get
            Dim o As Object = ViewState("vakun_piutang_dagang")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vakun_piutang_dagang") = value
        End Set
    End Property

    Sub cleargrup()
        Me.tb_id_grup_customer.Text = 0
        Me.lbl_nama_grup_customer.Text = "------"
        Me.link_popup_grup_customer.Visible = True
    End Sub

    Sub bindgrupcustomer()
        sqlcom = "select name from daftar_group_customer where id = " & Me.tb_id_grup_customer.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_grup_customer.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindakun_giro_mundur(ByVal vid_akun_giro_mundur)
        sqlcom = "select accountno + ' - ' + inaname as nama_akun"
        sqlcom = sqlcom + " from coa_list"
        sqlcom = sqlcom + " where accountno = '" & vid_akun_giro_mundur & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_akun_giro_mundur.Text = reader.Item("nama_akun").ToString
        End If
        reader.Read()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindakun_piutang_dagang(ByVal vid_akun_piutang_dagang)
        sqlcom = "select accountno + ' - ' + inaname as nama_akun"
        sqlcom = sqlcom + " from coa_list"
        sqlcom = sqlcom + " where accountno = '" & vid_akun_piutang_dagang & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_akun_piutang_dagang.Text = reader.Item("nama_akun").ToString
        End If
        reader.Read()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindsales()
        sqlcom = "select code, nama_pegawai"
        sqlcom = sqlcom + " from user_list"
        sqlcom = sqlcom + " where (code_group = 5 or code = 34)"
        sqlcom = sqlcom + " order by nama_pegawai"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_sales.DataSource = reader
        Me.dd_sales.DataTextField = "nama_pegawai"
        Me.dd_sales.DataValueField = "code"
        Me.dd_sales.DataBind()
        Me.dd_sales.Items.Add(New ListItem("--- Nama sales ---", "0"))
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.id_customer = 0
        Me.tb_name.Text = ""
        Me.tb_lama_pembayaran.Text = ""
        Me.tb_limit_pembelian.Text = ""
        Me.tb_npwp.Text = ""
        Me.tb_keterangan_kawasan_berikat.Text = ""
        Me.dd_is_ekspor.SelectedValue = "T"
        Me.tb_tgl_awal.Text = Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year
        Me.tb_tgl_akhir.Text = Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year
    End Sub

    Sub loaddata()
        Try
            If Me.vid_customer <> 0 Then
                Me.id_customer = Me.vid_customer
            End If

            sqlcom = "select name, payment_period, id_group_customer, ar_limit, no_npwp, is_ekspor,"
            sqlcom = sqlcom + " ltrim(rtrim(convert(char, start_date, 103))) as start_date, ltrim(rtrim(convert(char, end_date, 103))) as end_date,"
            sqlcom = sqlcom + " is_polos, code_sales, status, akun_piutang_dagang, akun_piutang_giro_mundur, is_kawasan_berikat,"
            sqlcom = sqlcom + " keterangan_kawasan_berikat"
            sqlcom = sqlcom + " from daftar_customer"
            sqlcom = sqlcom + " where id = " & Me.id_customer
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_name.Text = reader.Item("name").ToString
                Me.tb_id_grup_customer.Text = reader.Item("id_group_customer").ToString
                Me.tb_lama_pembayaran.Text = reader.Item("payment_period").ToString
                Me.tb_limit_pembelian.Text = FormatNumber(reader.Item("ar_limit").ToString, 2)
                Me.tb_npwp.Text = reader.Item("no_npwp").ToString

                If String.IsNullOrEmpty(reader.Item("code_sales").ToString) Then
                    Me.dd_sales.SelectedValue = "0"
                Else
                    Me.dd_sales.SelectedValue = reader.Item("code_sales").ToString
                End If

                If reader.Item("start_date").ToString = "01/01/1900" Then
                    Me.tb_tgl_awal.Text = ""
                Else
                    Me.tb_tgl_awal.Text = reader.Item("start_date").ToString
                End If

                If reader.Item("end_date").ToString = "01/01/1900" Then
                    Me.tb_tgl_akhir.Text = ""
                Else
                    Me.tb_tgl_akhir.Text = reader.Item("end_date").ToString
                End If

                Me.tb_keterangan_kawasan_berikat.Text = reader.Item("keterangan_kawasan_berikat").ToString
                Me.dd_is_polos.SelectedValue = reader.Item("is_polos").ToString
                Me.dd_is_kawasan_berikat.SelectedValue = reader.Item("is_kawasan_berikat").ToString
                Me.dd_is_ekspor.SelectedValue = reader.Item("is_ekspor").ToString
                Me.dd_status.SelectedValue = reader.Item("status").ToString
                Me.tb_id_akun_giro_mundur.Text = reader.Item("akun_piutang_giro_mundur").ToString
                Me.tb_id_akun_piutang_dagang.Text = reader.Item("akun_piutang_dagang").ToString

                Me.bindgrupcustomer()
                Me.bindakun_giro_mundur(Me.tb_id_akun_giro_mundur.Text)
                Me.bindakun_piutang_dagang(Me.tb_id_akun_piutang_dagang.Text)
                Me.btn_alamat.Enabled = True
            Else
                Me.btn_alamat.Enabled = False
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.cleargrup()
            Me.bindsales()
            Me.dd_sales.SelectedValue = "0"
            Me.loaddata()
            Me.tb_id_grup_customer.Attributes.Add("style", "display: none;")
            Me.link_refresh_grup_customer.Attributes.Add("style", "display: none;")
            Me.link_popup_grup_customer.Attributes.Add("onclick", "popup_grup_customer('" & Me.tb_id_grup_customer.ClientID & "', '" & Me.link_refresh_grup_customer.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/customer_list.aspx?vpaging=" & Me.vpaging)
    End Sub

    Protected Sub link_refresh_grup_customer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_grup_customer.Click
        Me.bindgrupcustomer()
    End Sub

    Sub generate_akun_customer()
        Try
            Dim vno As Integer = 0
            Dim vakun As String = Nothing
            Dim vpiutang As String = Nothing

            sqlcom = "select max(Convert(Int, Right(RTrim(AccountNo), 3))) + 1 as vno"
            sqlcom = sqlcom + " from coa_list"
            sqlcom = sqlcom + " where AccountNo like '11.03.01%' and len(AccountNo) > 8"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                vno = reader.Item("vno").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            vakun = "11.03.01." & vno.ToString.PadLeft(3, "0")
            Me.vakun_giro_mundur = vakun
            sqlcom = "insert into coa_list(AccountNo, LAccount, ParentAcc, LParent, EngName, InaName, AccType, ShareAcc, IsControl,"
            sqlcom = sqlcom + " CostType, OffsetAcc, MinAmount, MaxAmount, DefAmount, CDesc, Remark, IsActive, Position, CurrType, CurrencyCode, AddAdjust)"
            sqlcom = sqlcom + " values('" & vakun & "','5', '11.03.01', 4, NULL,' PIUTANG GIRO MUNDUR " & Me.tb_name.Text & "', 11, NULL, 'N'"
            sqlcom = sqlcom + ", NULL, NULL, 0, 0, 0, NULL, NULL, 'Y', 'D', 'L', 'RP', NULL)"
            connection.koneksi.InsertRecord(sqlcom)

            vakun = "11.03.02." & vno.ToString.PadLeft(3, "0")
            Me.vakun_piutang_dagang = vakun
            sqlcom = "insert into coa_list(AccountNo, LAccount, ParentAcc, LParent, EngName, InaName, AccType, ShareAcc, IsControl,"
            sqlcom = sqlcom + " CostType, OffsetAcc, MinAmount, MaxAmount, DefAmount, CDesc, Remark, IsActive, Position, CurrType, CurrencyCode, AddAdjust)"
            sqlcom = sqlcom + " values('" & vakun & "','5', '11.03.01', 4, NULL,' PIUTANG DAGANG " & Me.tb_name.Text & "', 11, NULL, 'N'"
            sqlcom = sqlcom + ", NULL, NULL, 0, 0, 0, NULL, NULL, 'Y', 'D', 'L', 'RP', NULL)"
            connection.koneksi.InsertRecord(sqlcom)

            sqlcom = "update daftar_customer"
            sqlcom = sqlcom + " set akun_piutang_dagang = '" & Me.vakun_giro_mundur & "',"
            sqlcom = sqlcom + " akun_piutang_giro_mundur = '" & Me.vakun_piutang_dagang & "'"
            sqlcom = sqlcom + " where id = " & Me.id_customer
            connection.koneksi.UpdateRecord(sqlcom)

        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama customer terlebih dahulu"
            ElseIf Me.tb_id_grup_customer.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama grup customer terlebih dahulu"
            ElseIf Me.dd_sales.SelectedValue = "0" Then
                Me.lbl_msg.Text = "Silahkan mengisi nama sales terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_lama_pembayaran.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi lama pembayaran terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_limit_pembelian.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi limit hutang pembelian terlebih dahulu"
            Else                
                Dim vtgl_awal As String = ""
                Dim vtgl_akhir As String = ""

                If String.IsNullOrEmpty(Me.tb_tgl_awal.Text) Then
                    vtgl_awal = "01/01/1900"
                Else
                    vtgl_awal = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
                End If

                If String.IsNullOrEmpty(Me.tb_tgl_akhir.Text) Then
                    vtgl_akhir = "01/01/1900"
                Else
                    vtgl_akhir = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)
                End If

                If Me.id_customer = 0 Then
                    'cek nama customer
                    Dim vada As String = "T"
                    sqlcom = "select * from daftar_customer where upper(name) = upper('" & Me.tb_name.Text & "')"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vada = "Y"
                        Me.lbl_msg.Text = "Nama customer tersebut sudah ada"
                    Else
                        vada = "T"
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    If vada = "T" Then
                        Dim vmax As Integer = 0
                        sqlcom = "select isnull(max(id),0) + 1 as vmax from daftar_customer"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        reader.Read()
                        If reader.HasRows Then
                            vmax = reader.Item("vmax").ToString
                        End If
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        sqlcom = "insert into daftar_customer(id, name, payment_period, id_group_customer, ar_limit, no_npwp,"
                        sqlcom = sqlcom + " start_date, end_date, is_polos, code_sales, status, is_kawasan_berikat, is_ekspor,"
                        sqlcom = sqlcom + " keterangan_kawasan_berikat)"
                        sqlcom = sqlcom + " values(" & vmax & ",'" & Me.tb_name.Text & "'," & Decimal.ToDouble(Me.tb_lama_pembayaran.Text) & "," & Me.tb_id_grup_customer.Text
                        sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_limit_pembelian.Text) & ",'" & Me.tb_npwp.Text & "',"
                        sqlcom = sqlcom + "'" & vtgl_awal & "','" & vtgl_akhir & "','" & Me.dd_is_polos.SelectedValue & "'," & Me.dd_sales.SelectedValue & ","
                        sqlcom = sqlcom + "'" & Me.dd_status.SelectedValue & "','" & Me.dd_is_kawasan_berikat.SelectedValue & "','" & Me.dd_is_ekspor.SelectedValue & "',"
                        sqlcom = sqlcom + "'" & Me.tb_keterangan_kawasan_berikat.Text & "')"
                        connection.koneksi.InsertRecord(sqlcom)
                        Me.id_customer = vmax
                        Me.lbl_msg.Text = "Data sudah disimpan"

                        'Daniel
                        'Me.generate_akun_customer()
                        'Daniel
                    End If

                Else
                    sqlcom = "update daftar_customer"
                    sqlcom = sqlcom + " set name = '" & Me.tb_name.Text & "',"
                    sqlcom = sqlcom + " payment_period = " & Decimal.ToDouble(Me.tb_lama_pembayaran.Text) & ","
                    sqlcom = sqlcom + " id_group_customer = " & Me.tb_id_grup_customer.Text & ","
                    sqlcom = sqlcom + " ar_limit = " & Decimal.ToDouble(Me.tb_limit_pembelian.Text) & ","
                    sqlcom = sqlcom + " no_npwp = '" & Me.tb_npwp.Text & "',"
                    sqlcom = sqlcom + " start_date = '" & vtgl_awal & "',"
                    sqlcom = sqlcom + " end_date = '" & vtgl_akhir & "',"
                    sqlcom = sqlcom + " is_polos = '" & Me.dd_is_polos.SelectedValue & "',"
                    sqlcom = sqlcom + " is_kawasan_berikat = '" & Me.dd_is_kawasan_berikat.SelectedValue & "',"
                    sqlcom = sqlcom + " is_ekspor = '" & Me.dd_is_ekspor.SelectedValue & "',"
                    sqlcom = sqlcom + " code_sales = " & Me.dd_sales.SelectedValue & ","
                    sqlcom = sqlcom + " keterangan_kawasan_berikat = '" & Me.tb_keterangan_kawasan_berikat.text & "',"
                    sqlcom = sqlcom + " status = '" & Me.dd_status.SelectedValue & "'"
                    sqlcom = sqlcom + " where id = " & Me.id_customer
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If

                'Daniel
                sqlcom = "select id, name from daftar_customer where akun_piutang_giro_mundur is null and akun_piutang_dagang is null order by id"

                reader = connection.koneksi.SelectRecord(sqlcom)
                Dim no As Integer = 0
                Dim vgiro_mundur As String = Nothing
                Dim vpiutang_dagang As String = Nothing
                Do While reader.Read
                    no = Me.tradingClass.CustomerMaxAccountNo() + 1
                    vgiro_mundur = "11.03.01." & no
                    sqlcom = "insert into coa_list(AccountNo, LAccount, ParentAcc, LParent, EngName, InaName, AccType, ShareAcc, IsControl,"
                    sqlcom = sqlcom + " CostType, OffsetAcc, MinAmount, MaxAmount, DefAmount, CDesc, Remark, IsActive, Position, CurrType, CurrencyCode, AddAdjust)"
                    sqlcom = sqlcom + " values('" & vgiro_mundur & "','5', '11.03.01', 4, NULL,'PIUTANG GIRO MUNDUR " & reader.Item("name").ToString & "', 11, NULL, 'N'"
                    sqlcom = sqlcom + ", NULL, NULL, 0, 0, 0, NULL, NULL, 'Y', 'D', 'L', 'RP', NULL)"
                    connection.koneksi.InsertRecord(sqlcom)

                    sqlcom = "update daftar_customer"
                    sqlcom = sqlcom + " set akun_piutang_giro_mundur = '" & vgiro_mundur & "'"
                    sqlcom = sqlcom + " where id = " & reader.Item("id").ToString
                    connection.koneksi.UpdateRecord(sqlcom)

                    vpiutang_dagang = "11.03.02." & no
                    sqlcom = "insert into coa_list(AccountNo, LAccount, ParentAcc, LParent, EngName, InaName, AccType, ShareAcc, IsControl,"
                    sqlcom = sqlcom + " CostType, OffsetAcc, MinAmount, MaxAmount, DefAmount, CDesc, Remark, IsActive, Position, CurrType, CurrencyCode, AddAdjust)"
                    sqlcom = sqlcom + " values('" & vpiutang_dagang & "','5', '11.03.02', 4, NULL,'PIUTANG DAGANG " & reader.Item("name").ToString & "', 11, NULL, 'N'"
                    sqlcom = sqlcom + ", NULL, NULL, 0, 0, 0, NULL, NULL, 'Y', 'D', 'L', 'RP', NULL)"
                    connection.koneksi.InsertRecord(sqlcom)

                    sqlcom = "update daftar_customer"
                    sqlcom = sqlcom + " set akun_piutang_dagang = '" & vpiutang_dagang & "'"
                    sqlcom = sqlcom + " where id = " & reader.Item("id").ToString
                    connection.koneksi.UpdateRecord(sqlcom)
                Loop
                reader.Close()
                connection.koneksi.CloseKoneksi()
                'Daniel

                Me.loaddata()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_alamat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_alamat.Click
        Response.Redirect("~/alamat_customer.aspx?vid_customer=" & Me.id_customer)
    End Sub

End Class
