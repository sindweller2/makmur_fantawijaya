Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_detil_pembayaran_lc
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

    Private ReadOnly Property vno_lc() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_lc")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property vno_po() As Integer
        Get
            Dim o As Object = ViewState("vno_po")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vno_po") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindjenis_lc()
        sqlcom = "select code, name from lc_type order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_jenis_lc.DataSource = reader
        Me.dd_jenis_lc.DataTextField = "name"
        Me.dd_jenis_lc.DataValueField = "code"
        Me.dd_jenis_lc.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindperiode()
        sqlcom = "select name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_periode.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.tb_kurs.Text = ""
        Me.tb_prosentase.Text = ""
        Me.tb_jumlah_pembayaran.Text = ""
        Me.lbl_nilai_idr.Text = ""
    End Sub

    Sub loaddata()
        Try
            sqlcom = "select purchase_order.no as no_po, purchase_order.po_no_text, convert(char, purchase_order.tanggal, 103) as tanggal, purchase_order.id_currency,"
            sqlcom = sqlcom + " lc.no_lc, convert(char, lc.tanggal_lc, 103) as tanggal_lc, convert(char, lc.tgl_berlaku_lc, 103) as tgl_berlaku_lc,"
            sqlcom = sqlcom + " lc.id_lc_type, convert(char, lc.due_date_lc, 103) as due_date_lc, lc.id_negara_koresponden, lc.id_dikapalkan_dari,"
            sqlcom = sqlcom + " lc.id_pelabuhan_tujuan, lc.id_negara_asal, daftar_supplier.name as nama_supplier,"
            sqlcom = sqlcom + " isnull(lc.nilai_lc,0) as total_pembelian, transaction_period.name as nama_periode, lc.is_lc_lunas"
            sqlcom = sqlcom + " from lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = lc.no_po"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " inner join transaction_period on transaction_period.id = purchase_order.id_transaction_period"
            sqlcom = sqlcom + " where lc.seq = " & Me.vno_lc
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.vno_po = reader.Item("no_po").ToString
                Me.lbl_periode.Text = reader.Item("nama_periode").ToString
                Me.lbl_no_pembelian.Text = reader.Item("po_no_text").ToString
                Me.lbl_tgl_pembelian.Text = reader.Item("tanggal").ToString
                Me.lbl_mata_uang.Text = reader.Item("id_currency").ToString
                Me.lbl_total_nilai_pembelian.Text = FormatNumber(reader.Item("total_pembelian").ToString, 2)
                Me.lbl_nama_supplier.Text = reader.Item("nama_supplier").ToString
                Me.lbl_no_lc.Text = reader.Item("no_lc").ToString
                Me.lbl_tgl_lc.Text = reader.Item("tanggal_lc").ToString
                Me.lbl_tgl_berlaku_lc.Text = reader.Item("tgl_berlaku_lc").ToString
                Me.dd_jenis_lc.SelectedValue = reader.Item("id_lc_type").ToString
                Me.lbl_tgl_jatuh_tempo_lc.Text = reader.Item("due_date_lc").ToString

                if reader.Item("is_lc_lunas").ToString = "S" then
                   Me.lbl_status_lunas.Text = "Sudah lunas"
                Else
                   Me.lbl_status_lunas.Text = "Belum lunas"
                End if
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    sub loadnilai_invoice()
        Try
           sqlcom = "select isnull(sum(isnull(nilai_invoice,0)),0) as nilai_invoice"
           sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " where seq_lc = " & Me.vno_lc
           reader = connection.koneksi.SelectRecord(sqlcom)
           reader.Read()
           If reader.HasRows Then
              Me.lbl_total_nilai_invoice.Text = FormatNumber(reader.item("nilai_invoice").ToString,2)
           End If
           reader.Close()
           connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try

            Me.clearform()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select po_no, pembayaran_ke, prosentase, kurs, jumlah_nilai, (kurs * jumlah_nilai) jumlah_nilai_idr,"
            sqlcom = sqlcom + " id_bank, jenis_bayar, convert(char, tanggal_bayar, 103) as tanggal_bayar,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when pembayaran_ke = 1 then 'I'"
            sqlcom = sqlcom + " when pembayaran_ke = 2 then 'II'"
            sqlcom = sqlcom + " when pembayaran_ke = 3 then 'III'"
            sqlcom = sqlcom + " end as pembayaran_ke_nama"
            sqlcom = sqlcom + " from pembayaran_lc"
            sqlcom = sqlcom + " where seq_lc = " & Me.vno_lc
            sqlcom = sqlcom + " order by pembayaran_ke"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "pembayaran_lc")
                Me.dg_data.DataSource = ds.Tables("pembayaran_lc").DefaultView

                If ds.Tables("pembayaran_lc").Rows.Count > 0 Then
                    If ds.Tables("pembayaran_lc").Rows.Count > 8 Then
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

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name from bank_account order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).Items.Add(New ListItem("---Kas/Bank----", 0))

                        If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue = 0
                            'CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                        Else
                            CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text
                            'CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                        End If

                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_bayar"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_jenis_bayar"), Label).Text

                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                    Next
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.bindperiode()
            Me.bindjenis_lc()
            Me.loaddata()
            Me.loadnilai_invoice()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/pembayaran_lc_purchasing.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_prosentase.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah % pembayaran terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_kurs.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi kurs terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_jumlah_pembayaran.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah nilai pembayaran terlebih dahulu"
            Else
                sqlcom = "select * from pembayaran_lc"
                sqlcom = sqlcom + " where seq_lc = " & Me.vno_lc
                sqlcom = sqlcom + " and pembayaran_ke = " & Me.dd_pembayaran_ke.SelectedValue
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.lbl_msg.Text = "Pembayaran tersebut sudah ada"
                Else
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    sqlcom = "insert into pembayaran_lc(po_no, pembayaran_ke, prosentase, jumlah_nilai, kurs, jenis_bayar, seq_lc, is_submit)"
                    sqlcom = sqlcom + " values (" & Me.vno_po & "," & Me.dd_pembayaran_ke.SelectedValue & "," & Decimal.ToDouble(Me.tb_prosentase.Text)
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_jumlah_pembayaran.Text) & "," & Decimal.ToDouble(Me.tb_kurs.Text) & ","
                    sqlcom = sqlcom + "'" & Me.dd_jenis_bayar.SelectedValue & "'," & Me.vno_lc & ", 'B')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                    Me.loadgrid()
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()            
                'Me.update_status_lunas_lc()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete pembayaran_lc"
                    sqlcom = sqlcom + " where seq_lc = " & Me.vno_lc
                    sqlcom = sqlcom + " and pembayaran_ke = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah dihapus"
                End If
            Next
            Me.loadgrid()
            'Me.update_status_lunas_lc()
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
                    sqlcom = "update pembayaran_lc"
                    sqlcom = sqlcom + " set prosentase = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_prosentase"), TextBox).Text) & ","
                    sqlcom = sqlcom + " jumlah_nilai = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_jumlah"), TextBox).Text) & ","
                    sqlcom = sqlcom + " kurs = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kurs"), TextBox).Text) & ","
                    sqlcom = sqlcom + " jenis_bayar = '" & CType(Me.dg_data.Items(x).FindControl("dd_jenis_bayar"), DropDownList).SelectedValue & "'"
                    sqlcom = sqlcom + " where seq_lc = " & Me.vno_lc
                    sqlcom = sqlcom + " and pembayaran_ke = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
            'Me.update_status_lunas_lc()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    'sub update_status_lunas_lc()
        'Try 
            'Dim readerlunas As SqlClient.SqlDataReader
            'dim vstatus as string    
            'sqlcom = "select isnull(sum(isnull(jumlah_nilai,0)),0) as jumlah_nilai"
            'sqlcom = sqlcom + " from pembayaran_lc"
            'sqlcom = sqlcom + " where seq_lc = " & Me.vno_lc
            'readerlunas = connection.koneksi.selectRecord(sqlcom)
            'readerlunas.Read()
            'If readerlunas.HasRows Then
                'if decimal.todouble(readerlunas.item("jumlah_nilai").toString) = decimal.todouble(Me.lbl_total_nilai_invoice.Text) Then
                   'vstatus = "S"
                   'Me.lbl_status_lunas.Text = "Sudah lunas"
                'else
                   'vstatus = "B"
                   'Me.lbl_status_lunas.Text = "Belum lunas"
                'end if
                'sqlcom = "update lc set is_lc_lunas = '" & vstatus & "' where seq = " & Me.vno_lc
                'connection.koneksi.UpdateRecord(sqlcom)
            'End If
            'readerlunas.Close()
            'connection.koneksi.CloseKoneksi()
        'Catch ex As Exception
            'Me.lbl_msg.Text = ex.Message
        'End Try        
    'End Sub

    Protected Sub btn_hitung_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_hitung.Click
        If String.IsNullOrEmpty(Me.tb_prosentase.Text) Then
            Me.lbl_msg.Text = "Silahkan mengisi jumlah % pembayaran terlebih dahulu"
        Else
            Me.lbl_msg.Text = ""
            Me.tb_jumlah_pembayaran.Text = FormatNumber(Decimal.ToDouble(Me.lbl_total_nilai_invoice.Text) * Decimal.ToDouble(Me.tb_prosentase.Text) / 100, 2)            
        End If
    End Sub

   
    Protected Sub btn_hitung_idr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_hitung_idr.Click
        If String.IsNullOrEmpty(Me.tb_kurs.Text) Then
            Me.lbl_msg.Text = "Silahkan mengisi kurs terlebih dahulu"
        ElseIf String.IsNullOrEmpty(Me.tb_jumlah_pembayaran.Text) Then
            Me.lbl_msg.Text = "Silahkan mengisi jumlah nilai pembayaran USD terlebih dahulu"
        Else
            Me.lbl_msg.Text = ""
            Me.lbl_nilai_idr.Text = FormatNumber(Decimal.ToDouble(Me.tb_kurs.Text) * Decimal.ToDouble(Me.tb_jumlah_pembayaran.Text), 2)
        End If
    End Sub
End Class
