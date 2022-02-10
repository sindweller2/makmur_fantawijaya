Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_purchase_order_stock
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

    Private ReadOnly Property vpaging() As Integer
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
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
        sqlcom = sqlcom + " from purchase_order"
        sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
        sqlcom = sqlcom + " where purchase_order.id_transaction_period = " & Me.dd_bulan.SelectedValue
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

    Sub loadgrid()
        Try

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select purchase_order.no, purchase_order.po_no_text, convert(char, purchase_order.tanggal, 103) as tanggal,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when purchase_order.jenis_pembelian = 'T' then 'Tunai'"
            sqlcom = sqlcom + " when purchase_order.jenis_pembelian = 'K' then 'Kredit'"
            sqlcom = sqlcom + " end as jenis_penjualan,"
            sqlcom = sqlcom + " purchase_order.is_submit,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when purchase_order.is_submit = 'S' then 'Sudah'"
            sqlcom = sqlcom + " when purchase_order.is_submit = 'B' then 'Belum'"
            sqlcom = sqlcom + " end as status_submit,"
            sqlcom = sqlcom + " convert(char, purchase_order.received_date_request, 103) as received_date_request,"
            sqlcom = sqlcom + " daftar_supplier.name as nama_supplier"
            sqlcom = sqlcom + " from purchase_order"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " where purchase_order.id_transaction_period = " & Me.dd_bulan.SelectedValue

            If Me.dd_pilihan.SelectedValue = "0" Then
                If String.IsNullOrEmpty(Me.tb_search.Text) Then
                    sqlcom = sqlcom
                Else
                    sqlcom = sqlcom + " and upper(purchase_order.po_no_text) like upper('%" & Me.tb_search.Text & "%')"
                End If
            Else
                If Me.dd_submit.SelectedValue = "B" Then
                    sqlcom = sqlcom + " and purchase_order.is_submit = 'B'"
                Else
                    sqlcom = sqlcom + " and purchase_order.is_submit = 'S'"
                End If
            End If
            sqlcom = sqlcom + " order by purchase_order.no"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "purchase_order")
                Me.dg_data.DataSource = ds.Tables("purchase_order").DefaultView

                If ds.Tables("purchase_order").Rows.Count > 0 Then
                    If ds.Tables("purchase_order").Rows.Count > 10 Then
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
                    Me.btn_delete.Visible = True

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        If CType(Me.dg_data.Items(x).FindControl("lbl_is_submit"), Label).Text = "B" Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                        End If
                    Next
                Else
                    Me.dg_data.Visible = False
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
            Me.dd_pilihan.SelectedValue = "0"
            Me.pilihan()
            Me.bindbulan()

            if me.vpaging <> 0 then
                Me.dg_data.CurrentPageIndex = me.vpaging
            end if

            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        Response.Redirect("~/detil_purchase_order.aspx?vtahun=" & Me.tb_tahun.Text & "&vbulan=" & Me.bulan)
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete purchase_order"
                    sqlcom = sqlcom + " where no = " & CType(Me.dg_data.Items(x).FindControl("lbl_no"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)

                    'daniel 31/10/2017 ========================================================================================
                    sqlcom = String.Empty
                    sqlcom = "delete purchase_order_detil"
                    sqlcom = sqlcom + " where po_no = " & CType(Me.dg_data.Items(x).FindControl("lbl_no"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    '======================================================================================== daniel 31/10/2017

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

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Response.Redirect("~/detil_purchase_order.aspx?vno_po=" & CType(e.Item.FindControl("lbl_no"), Label).Text & "&vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&vpaging=" & Me.dg_data.CurrentPageIndex)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.bindbulan()
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    Sub pilihan()
        If Me.dd_pilihan.SelectedValue = "0" Then
            Me.tb_search.Visible = True
            Me.btn_search.Visible = True
            Me.dd_submit.Visible = False
        Else
            Me.tb_search.Visible = False
            Me.btn_search.Visible = False
            Me.dd_submit.Visible = True
        End If
    End Sub

    Protected Sub dd_pilihan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_pilihan.SelectedIndexChanged
        Me.pilihan()
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub dd_submit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_submit.SelectedIndexChanged
        Me.loadgrid()
    End Sub
End Class
