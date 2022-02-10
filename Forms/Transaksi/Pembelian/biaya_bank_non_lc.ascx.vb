Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_biaya_bank_non_lc
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
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select entry_dokumen_impor.seq as no_dokumen, entry_dokumen_impor.invoice_no, entry_dokumen_impor.packing_list_no,"
            sqlcom = sqlcom + " isnull(entry_dokumen_impor.biaya_bank_non_lc,0) as biaya_bank_non_lc, purchase_order.po_no_text,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when entry_dokumen_impor.tgl_byr_biayabank_nonlc is null then 'Belum dibayar'"
            sqlcom = sqlcom + " when entry_dokumen_impor.tgl_byr_biayabank_nonlc is not null then 'Sudah dibayar'"
            sqlcom = sqlcom + " end as status_bayar"
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
                    Me.btn_update.Visible = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
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
                    sqlcom = "update entry_dokumen_impor"
                    sqlcom = sqlcom + " set biaya_bank_non_lc = " & CType(Me.dg_data.Items(x).FindControl("tb_jumlah"), TextBox).Text
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
