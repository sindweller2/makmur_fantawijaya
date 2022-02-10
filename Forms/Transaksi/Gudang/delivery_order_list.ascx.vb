Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Gudang_delivery_order_list
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

    Private ReadOnly Property vpaging() As String
        Get
            Dim o As Object = Request.QueryString("vpaging")
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
        sqlcom = sqlcom + " from stock_delivery_order"
        sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = stock_delivery_order.id_customer"
        sqlcom = sqlcom + " inner join sales_order on sales_order.no = stock_delivery_order.sales_order_no"
        sqlcom = sqlcom + " where stock_delivery_order.id_transaction_period = " & Me.dd_bulan.SelectedValue
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

            sqlcom = "select stock_delivery_order.no, stock_delivery_order.do_no_text, convert(char, stock_delivery_order.tanggal, 103) as tanggal,"
            sqlcom = sqlcom + " convert(char, stock_delivery_order.tanggal_kirim, 103) as tanggal_kirim,"
            sqlcom = sqlcom + " sales_order.so_no_text as no_so_text,"
            sqlcom = sqlcom + " stock_delivery_order.is_submit,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when stock_delivery_order.is_submit_gudang = 'S' then 'Sudah'"
            sqlcom = sqlcom + " when stock_delivery_order.is_submit_gudang = 'B' then 'Belum'"
            sqlcom = sqlcom + " end as status_submit,"
            sqlcom = sqlcom + " convert(char, do_received_date, 103) as do_received_date,"
            sqlcom = sqlcom + " convert(char, delivery_date, 103) as delivery_date,"
            sqlcom = sqlcom + " daftar_customer.name as nama_customer"
            sqlcom = sqlcom + " from stock_delivery_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = stock_delivery_order.id_customer"
            sqlcom = sqlcom + " inner join sales_order on sales_order.no = stock_delivery_order.sales_order_no"
            sqlcom = sqlcom + " where stock_delivery_order.id_transaction_period = " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " and stock_delivery_order.is_submit = 'S'"

            If Me.dd_pilihan.SelectedValue = "0" Then
                If String.IsNullOrEmpty(Me.tb_search.Text) Then
                    sqlcom = sqlcom
                Else
                    sqlcom = sqlcom + " and upper(stock_delivery_order.do_no_text) like upper('%" & Me.tb_search.Text & "%')"
                End If
            ElseIf Me.dd_pilihan.SelectedValue = "1" Then
                If String.IsNullOrEmpty(Me.tb_search.Text) Then
                    sqlcom = sqlcom
                Else
                    sqlcom = sqlcom + " and upper(sales_order.so_no_text) like upper('%" & Me.tb_search.Text & "%')"
                End If
            ElseIf Me.dd_pilihan.SelectedValue = 2 Then
                If Me.dd_submit.SelectedValue = "B" Then
                    sqlcom = sqlcom + " and stock_delivery_order.is_submit_gudang = 'B'"
                Else
                    sqlcom = sqlcom + " and stock_delivery_order.is_submit_gudang = 'S'"
                End If
            ElseIf Me.dd_pilihan.SelectedValue = 3 Then
                If Me.dd_sts_terima.SelectedValue = "B" Then
                    sqlcom = sqlcom + " and stock_delivery_order.do_received_date is null"
                Else
                    sqlcom = sqlcom + " and stock_delivery_order.do_received_date is not null"
                End If
            ElseIf Me.dd_pilihan.SelectedValue = 4 Then
                If Me.dd_sts_kirim.SelectedValue = "B" Then
                    sqlcom = sqlcom + " and stock_delivery_order.delivery_date is null"
                Else
                    sqlcom = sqlcom + " and stock_delivery_order.delivery_date is not null"
                End If
            End If
            sqlcom = sqlcom + " order by stock_delivery_order.no"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "stock_delivery_order")
                Me.dg_data.DataSource = ds.Tables("stock_delivery_order").DefaultView

                If ds.Tables("stock_delivery_order").Rows.Count > 0 Then
                    If ds.Tables("stock_delivery_order").Rows.Count > 10 Then
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
                Else
                    Me.dg_data.Visible = False
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

            If Me.vpaging <> 0 Then
                Me.dg_data.CurrentPageIndex = Me.vpaging
            End If

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

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Response.Redirect("~/detil_delivery_order_list.aspx?vno_do=" & CType(e.Item.FindControl("lbl_no"), Label).Text & "&vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&vpaging=" & Me.dg_data.CurrentPageIndex)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.bindbulan()
        Me.loadgrid()
    End Sub

    Sub pilihan()
        If Me.dd_pilihan.SelectedValue = "0" Or Me.dd_pilihan.SelectedValue = "1" Then
            Me.tb_search.Visible = True
            Me.btn_search.Visible = True
            Me.dd_submit.Visible = False
            Me.dd_sts_terima.Visible = False
            Me.dd_sts_kirim.Visible = False
        ElseIf Me.dd_pilihan.SelectedValue = 2 Then
            Me.tb_search.Visible = False
            Me.btn_search.Visible = False
            Me.dd_submit.Visible = True
            Me.dd_sts_terima.Visible = False
            Me.dd_sts_kirim.Visible = False
        ElseIf Me.dd_pilihan.SelectedValue = 3 Then
            Me.tb_search.Visible = False
            Me.btn_search.Visible = False
            Me.dd_submit.Visible = False
            Me.dd_sts_terima.Visible = True
            Me.dd_sts_kirim.Visible = False
        ElseIf Me.dd_pilihan.SelectedValue = 4 Then
            Me.tb_search.Visible = False
            Me.btn_search.Visible = False
            Me.dd_submit.Visible = False
            Me.dd_sts_terima.Visible = False
            Me.dd_sts_kirim.Visible = True
        End If
        Me.dg_data.CurrentPageIndex = 1
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

    Protected Sub dd_sts_terima_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_sts_terima.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub dd_sts_kirim_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_sts_kirim.SelectedIndexChanged
        Me.loadgrid()
    End Sub
End Class


