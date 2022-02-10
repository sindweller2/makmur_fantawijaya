Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Penjualan_retur_sales_order
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

    Private ReadOnly Property voption() As Integer
        Get
            Dim o As Object = Request.QueryString("voption")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vsearch() As String
        Get
            Dim o As Object = Request.QueryString("vsearch")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
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
        sqlcom = sqlcom + " from retur_sales_order"
        sqlcom = sqlcom + " where id_transaction_period = " & Me.bulan
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

            sqlcom = "select retur_sales_order.no, retur_sales_order.no_retur_text, convert(char, retur_sales_order.tanggal, 103) as tanggal,"
            sqlcom = sqlcom + " sales_order.so_no_text, retur_sales_order.is_submit,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when retur_sales_order.is_submit = 'S' then 'Sudah'"
            sqlcom = sqlcom + " when retur_sales_order.is_submit = 'B' then 'Belum'"
            sqlcom = sqlcom + " end as status_submit,"
            sqlcom = sqlcom + " daftar_customer.name as nama_customer, user_list.nama_pegawai as nama_sales"
            sqlcom = sqlcom + " from retur_sales_order"
            sqlcom = sqlcom + " inner join sales_order on sales_order.no = retur_sales_order.no_sales_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
            sqlcom = sqlcom + " where sales_order.id_transaction_period = " & Me.dd_bulan.SelectedValue

            If Me.dd_pilihan.SelectedValue = "0" Then
                If String.IsNullOrEmpty(Me.tb_search.Text) Then
                    sqlcom = sqlcom
                Else
                    sqlcom = sqlcom + " and upper(retur_sales_order.no_retur_text) like upper('%" & Me.tb_search.Text & "%')"
                End If
            Else
                If Me.dd_submit.SelectedValue = "B" Then
                    sqlcom = sqlcom + " and retur_sales_order.is_submit = 'B'"
                Else
                    sqlcom = sqlcom + " and retur_sales_order.is_submit = 'S'"
                End If
            End If

            sqlcom = sqlcom + " order by retur_sales_order.no_retur_text"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "retur_sales_order")
                Me.dg_data.DataSource = ds.Tables("retur_sales_order").DefaultView

                If ds.Tables("retur_sales_order").Rows.Count > 0 Then
                    If ds.Tables("retur_sales_order").Rows.Count > 10 Then
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
            Me.dd_pilihan.SelectedValue = Me.voption

            If Me.voption = 0 Then
                Me.tb_search.Text = Me.vsearch
            Else
                Me.dd_submit.SelectedValue = Me.vsearch
            End If

            If Me.vpaging <> 0 Then
                Me.dg_data.CurrentPageIndex = Me.vpaging
            End If

            Me.pilihan()
            Me.bindbulan()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.bindbulan()
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        Response.Redirect("~/detil_retur_sales_order.aspx?vtahun=" & Me.tb_tahun.Text & "&vbulan=" & Me.bulan)
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete retur_sales_order"
                    sqlcom = sqlcom + " where no = " & CType(Me.dg_data.Items(x).FindControl("lbl_no"), Label).Text
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

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            If Me.dd_pilihan.SelectedValue = 0 Then
                Response.Redirect("~/detil_retur_sales_order.aspx?vno_retur=" & CType(e.Item.FindControl("lbl_no"), Label).Text & "&vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&voption=" & Me.dd_pilihan.SelectedValue & "&vsearch=" & Me.tb_search.Text & "&vpaging=" & Me.dg_data.CurrentPageIndex)
            ElseIf Me.dd_pilihan.SelectedValue = 1 Then
                Response.Redirect("~/detil_retur_sales_order.aspx?vno_retur=" & CType(e.Item.FindControl("lbl_no"), Label).Text & "&vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&voption=" & Me.dd_pilihan.SelectedValue & "&vsearch=" & Me.dd_submit.SelectedValue & "&vpaging=" & Me.dg_data.CurrentPageIndex)
            End If
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
