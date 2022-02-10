Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_detil_supplier_produk
    Inherits System.Web.UI.UserControl
    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    'Daniel
    Public tradingClass As New tradingClass()
    'Daniel

    Private ReadOnly Property vid_supplier() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_supplier")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property id_supplier() As Integer
        Get
            Dim o As Object = ViewState("id_supplier")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_supplier") = value
        End Set
    End Property

    Sub clearform()
        Me.id_supplier = 0
        Me.tb_name.Text = ""
        Me.tb_alamat.Text = ""
        Me.tb_lama_pembayaran.Text = ""
        Me.tb_discount.Text = ""
        Me.tb_plus_minus.Text = ""
    End Sub

    Sub bindakun_hutang_dagang()
        Dim readerhutang_dagang As SqlClient.SqlDataReader
        sqlcom = "select accountno + ' - ' + inaname as nama_akun"
        sqlcom = sqlcom + " from coa_list"
        sqlcom = sqlcom + " where accountno = '" & Me.tb_id_akun_hutang_dagang.Text & "'"
        readerhutang_dagang = connection.koneksi.SelectRecord(sqlcom)
        readerhutang_dagang.Read()
        If readerhutang_dagang.HasRows Then
            Me.lbl_nama_akun_hutang_dagang.Text = readerhutang_dagang.Item("nama_akun").ToString
        End If
        readerhutang_dagang.Read()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loaddata()
        Try
            If Me.vid_supplier <> 0 Then
                Me.id_supplier = Me.vid_supplier
            End If

            sqlcom = "select name, alamat, isnull(discount,0) as discount, isnull(kredit,0) as kredit, telp, fax, "
            sqlcom = sqlcom + " isnull(plus_minus,0) as plus_minus, status, nama_bank, akun_hutang_dagang, is_lc"
            sqlcom = sqlcom + " from daftar_supplier"
            sqlcom = sqlcom + " where id = " & Me.id_supplier
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_name.Text = reader.Item("name").ToString
                Me.tb_alamat.Text = reader.Item("alamat").ToString
                Me.tb_telp.Text = reader.Item("telp").ToString
                Me.tb_fax.Text = reader.Item("fax").ToString
                Me.tb_lama_pembayaran.Text = reader.Item("kredit").ToString
                Me.tb_discount.Text = reader.Item("discount").ToString
                Me.tb_plus_minus.Text = reader.Item("plus_minus").ToString
                Me.dd_status.SelectedValue = reader.Item("status").ToString
                Me.tb_nama_bank.Text = reader.Item("nama_bank").ToString
                Me.dd_jenis_pembelian.SelectedValue = reader.Item("is_lc").ToString
                Me.tb_id_akun_hutang_dagang.Text = reader.Item("akun_hutang_dagang").ToString.Trim
                Me.btn_kontak_person.Enabled = True

                Me.bindakun_hutang_dagang()
            Else
                Me.btn_kontak_person.Enabled = False
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
            Me.loaddata()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/supplier_list.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama supplier terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_alamat.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi alamat terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_discount.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi discount terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_lama_pembayaran.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi lama pembayaran terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_plus_minus.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi plus/minus terlebih dahulu"
            Else
                If Me.id_supplier = 0 Then
                    Dim vmax As Integer = 0
                    sqlcom = "select isnull(max(id),0) + 1 as vmax from daftar_supplier"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = reader.Item("vmax").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    'Daniel
                    sqlcom = "insert into daftar_supplier(id, name, alamat, discount, kredit, plus_minus, status, telp, fax, nama_bank, is_lc)"
                    'sqlcom = sqlcom + " akun_hutang_dagang)"
                    'Daniel
                    sqlcom = sqlcom + " values(" & vmax & ",'" & Me.tb_name.Text & "','" & Me.tb_alamat.Text & "'," & Decimal.ToDouble(Me.tb_discount.Text)                    
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_lama_pembayaran.Text) & "," & Decimal.ToDouble(Me.tb_plus_minus.Text) & ","
                    sqlcom = sqlcom + "'" & Me.dd_status.SelectedValue & "','" & Me.tb_telp.Text & "','" & Me.tb_fax.Text & "',"
                    'Daniel
                    sqlcom = sqlcom + "'" & Me.tb_nama_bank.Text.Replace("'", "''") & "','" & Me.dd_jenis_pembelian.SelectedValue & "')"
                    'sqlcom = sqlcom + "'" & Me.tb_id_akun_hutang_dagang.Text.Trim & "')"
                    'Daniel
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.id_supplier = vmax
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update daftar_supplier"
                    sqlcom = sqlcom + " set name = '" & Me.tb_name.Text & "',"
                    sqlcom = sqlcom + " alamat = '" & Me.tb_alamat.Text & "',"
                    sqlcom = sqlcom + " telp = '" & Me.tb_telp.Text & "',"
                    sqlcom = sqlcom + " fax = '" & Me.tb_fax.Text & "',"
                    sqlcom = sqlcom + " discount = " & Decimal.ToDouble(Me.tb_discount.Text) & ","
                    sqlcom = sqlcom + " kredit = " & Decimal.ToDouble(Me.tb_lama_pembayaran.Text) & ","
                    sqlcom = sqlcom + " plus_minus = " & Decimal.ToDouble(Me.tb_plus_minus.Text) & ","
                    sqlcom = sqlcom + " status = '" & Me.dd_status.SelectedValue & "',"
                    sqlcom = sqlcom + " nama_bank = '" & Me.tb_nama_bank.Text.Replace("'", "''") & "',"
                    'Daniel
                    sqlcom = sqlcom + " is_lc = '" & Me.dd_jenis_pembelian.SelectedValue & "'"
                    'sqlcom = sqlcom + " akun_hutang_dagang = '" & Me.tb_id_akun_hutang_dagang.Text.Trim & "'"
                    'Daniel
                    sqlcom = sqlcom + " where id = " & Me.id_supplier
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If

                'Daniel
                sqlcom = "update daftar_supplier set akun_hutang_dagang = null where akun_hutang_dagang = ''"
                connection.koneksi.UpdateRecord(sqlcom)


                sqlcom = "select id, name from daftar_supplier where akun_hutang_dagang is null order by id"
                reader = connection.koneksi.SelectRecord(sqlcom)
                Dim no As Integer = 0
                Dim vpiutang_dagang As String = Nothing
                Do While reader.Read
                    no = Me.tradingClass.SupplierMaxAccountNo() + 1

                    vpiutang_dagang = "21.02.02." & no
                    sqlcom = "insert into coa_list(AccountNo, LAccount, ParentAcc, LParent, EngName, InaName, AccType, ShareAcc, IsControl,"
                    sqlcom = sqlcom + " CostType, OffsetAcc, MinAmount, MaxAmount, DefAmount, CDesc, Remark, IsActive, Position, CurrType, CurrencyCode, AddAdjust)"
                    sqlcom = sqlcom + " values('" & vpiutang_dagang & "','5', '21.02.02', 4, NULL,'HUTANG DAGANG LUAR NEGERI " & reader.Item("name").ToString & "', 21, NULL, 'N'"
                    sqlcom = sqlcom + ", NULL, NULL, 0, 0, 0, NULL, NULL, 'Y', 'C', 'L', NULL, NULL)"
                    connection.koneksi.InsertRecord(sqlcom)

                    sqlcom = "update daftar_supplier"
                    sqlcom = sqlcom + " set akun_hutang_dagang = '" & vpiutang_dagang & "'"
                    sqlcom = sqlcom + " where id = " & reader.Item("id").ToString
                    connection.koneksi.UpdateRecord(sqlcom)
                Loop
                reader.Close()
                connection.koneksi.CloseKoneksi()
                'Daniel

                Me.loaddata()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_kontak_person_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kontak_person.Click
        Response.Redirect("~/daftar_supplier_kontak_person.aspx?vid_supplier=" & Me.vid_supplier)
    End Sub
End Class
