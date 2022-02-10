Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_penerimaan_pembayaran_penjualan_list
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

    Public Property bulan() As Integer
        Get
            Dim o As Object = ViewState("bulan")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("bulan") = value
        End Set
    End Property

    Private ReadOnly Property voption() As string
        Get
            Dim o As Object = Request.QueryString("voption")
            If String.IsNullOrEmpty(o) = False Then Return Cstr(o) Else Return "0"
        End Get
    End Property

    Private ReadOnly Property vpaging() As integer
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return Cint(o) Else Return 0
        End Get
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
        sqlcom = sqlcom + " from sales_order"
        sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
        sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
        sqlcom = sqlcom + " where sales_order.id_transaction_period = " & Me.dd_bulan.SelectedValue
        sqlcom = sqlcom + " and sales_order.is_invoice = 'S'"
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

            sqlcom = "select sales_order.no, sales_order.so_no_text, convert(char, sales_order.tanggal, 103) as tanggal, sales_order.no_surat_pesanan,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when jenis_penjualan = 'T' then 'Tunai'"
            sqlcom = sqlcom + " when jenis_penjualan = 'K' then 'Kredit'"
            sqlcom = sqlcom + " end as jenis_penjualan,"
            sqlcom = sqlcom + " sales_order.id_currency as mata_uang, "


            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + "    when isnull(sales_order.ppn,0) = 0 then"
            sqlcom = sqlcom + "         isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100))"
            sqlcom = sqlcom + "         from sales_order_detail where no_sales_order = sales_order.no),0)"
            sqlcom = sqlcom + "    when isnull(sales_order.ppn,0) = 10 then"
            sqlcom = sqlcom + "         isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100))"
            sqlcom = sqlcom + "         from sales_order_detail where no_sales_order = sales_order.no),0) + "
            sqlcom = sqlcom + "         isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100))"
            sqlcom = sqlcom + "         from sales_order_detail where no_sales_order = sales_order.no),0) * 0.1 "
            sqlcom = sqlcom + " end * sales_order.rate"
            sqlcom = sqlcom + "  - "
            sqlcom = sqlcom + " ((select isnull(sum((isnull(nilai_pembayaran,0) + isnull(potongan,0) - isnull(kelebihan,0) + isnull(biaya_bank,0))),0) * sales_order.rate"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no"
            sqlcom = sqlcom + " and id_currency = 'USD') "
            sqlcom = sqlcom + " + "
            sqlcom = sqlcom + " (select isnull(sum(isnull(nilai_pembayaran,0) +  isnull(potongan,0) - isnull(kelebihan,0) + isnull(biaya_bank,0)),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no"
            sqlcom = sqlcom + " and id_currency = 'IDR'))"

                                'retur penjualan
            sqlcom = sqlcom + " - "
            sqlcom = sqlcom + " isnull((select "
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when x.ppn = 0 then "
            sqlcom = sqlcom + " isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) - "
            sqlcom = sqlcom + " (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) * x.rate "
            sqlcom = sqlcom + "    when x.ppn = 10 then "
            sqlcom = sqlcom + " (isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) - "
            sqlcom = sqlcom + " (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0) +  "
            sqlcom = sqlcom + " ((isnull(sum(isnull(retur_sales_order_detil.qty,0) * (isnull(sales_order_detail.harga_jadi,0) - "
            sqlcom = sqlcom + " (isnull(sales_order_detail.harga_jadi,0) * isnull(sales_order_detail.discount,0)/100))),0)) * 0.1))  * x.rate "
            sqlcom = sqlcom + " end "
            sqlcom = sqlcom + " from retur_sales_order_detil "
            sqlcom = sqlcom + " inner join retur_sales_order on retur_sales_order.no = retur_sales_order_detil.no_retur_so "
            sqlcom = sqlcom + " inner join sales_order x on x.no = retur_sales_order.no_sales_order "
            sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = x.no "
            sqlcom = sqlcom + " and sales_order_detail.id_product = retur_sales_order_detil.id_produk "
            sqlcom = sqlcom + " where retur_sales_order.no_sales_order = sales_order.no "
            sqlcom = sqlcom + " group by x.ppn, x.rate),0) "

            sqlcom = sqlcom + " as total_nilai,"
            sqlcom = sqlcom + " is_submit,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when is_submit = 'S' then 'Sudah'"
            sqlcom = sqlcom + " when is_submit = 'B' then 'Belum'"
            sqlcom = sqlcom + " end as status_submit,"
            sqlcom = sqlcom + " daftar_customer.name as nama_customer, user_list.nama_pegawai as nama_sales,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when sales_order.status_invoice = 'B' then 'Belum lunas'"
            sqlcom = sqlcom + " when sales_order.status_invoice = 'S' then 'Sudah lunas'"
            sqlcom = sqlcom + " end as status"
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
            sqlcom = sqlcom + " where sales_order.id_transaction_period = " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " and sales_order.is_invoice = 'S'"

            If Me.dd_pilihan.SelectedValue = "0" Then
                If String.IsNullOrEmpty(Me.tb_search.Text) Then
                    sqlcom = sqlcom
                Else
                    sqlcom = sqlcom + " and upper(sales_order.so_no_text) like upper('%" & Me.tb_search.Text & "%')"
                End If
            ElseIf Me.dd_pilihan.SelectedValue = "1" Then
                sqlcom = sqlcom + " and sales_order.status_invoice = 'S'"
            ElseIf Me.dd_pilihan.SelectedValue = "2" Then
                sqlcom = sqlcom + " and sales_order.status_invoice = 'B'"
            End If

            sqlcom = sqlcom + " order by sales_order.no"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "sales_order")
                Me.dg_data.DataSource = ds.Tables("sales_order").DefaultView

                If ds.Tables("sales_order").Rows.Count > 0 Then
                    If ds.Tables("sales_order").Rows.Count > 10 Then
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

                    for x as integer = 0 to me.dg_data.items.count()- 1
                        dim y as integer = 0
                        dim vis_submit as string = ""
                        sqlcom = "select is_submit from pembayaran_invoice_penjualan"
                        sqlcom = sqlcom + " where no_so = " & ctype(me.dg_data.items(x).findcontrol("lbl_no"), label).text
                        sqlcom = sqlcom + " group by is_submit"
                        reader = connection.koneksi.selectRecord(sqlcom)
                        do while reader.read()
                           y = y + 1
                           vis_submit = reader.item("is_submit").toString
                        loop
                        Reader.Close()
                        connection.koneksi.CloseKoneksi()

                        if y > 1 then
                           ctype(me.dg_data.items(x).findcontrol("lbl_submit"), Label).text = "Belum submit"
                        else
                           if vis_submit = "S" then
                              ctype(me.dg_data.items(x).findcontrol("lbl_submit"), Label).text = "Sudah submit"
                           else
                              ctype(me.dg_data.items(x).findcontrol("lbl_submit"), Label).text = "Belum submit"
                           end if
                        end if
                    Next
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
            Me.bindperiode_transaksi()

            If Me.vbulan <> 0 Then
               sqlcom = "select id from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
               reader = connection.koneksi.SelectRecord(sqlcom)
               reader.Read()
               If reader.HasRows Then
                  Me.dd_bulan.SelectedValue = reader.Item("id").ToString
               End If
               reader.Close()
               connection.koneksi.CloseKoneksi()
            End If


            Me.dd_pilihan.SelectedValue = Me.voption


            if me.vpaging <> 0 then
               Me.dg_data.CurrentPageIndex = me.vpaging
            end if
           
            Me.pilihan()
            Me.bindbulan()
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
            Response.Redirect("~/detil_penerimaan_pembayaran_penjualan.aspx?vno_so=" & CType(e.Item.FindControl("lbl_no"), Label).Text & "&vbulan=" & Me.bulan & "&vtahun=" & Me.tb_tahun.Text & "&voption=" & Me.dd_pilihan.SelectedValue & "&vpaging=" & Me.dg_data.CurrentPageIndex)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.dg_data.CurrentPageIndex = 0
        Me.bindbulan()
        Me.loadgrid()
    End Sub

    Sub pilihan()
        If Me.dd_pilihan.SelectedValue = "0" Then
            Me.tb_search.Visible = True
            Me.btn_search.Visible = True
        Else
            Me.tb_search.Visible = False
            Me.btn_search.Visible = False
        End If
    End Sub

    Protected Sub dd_pilihan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_pilihan.SelectedIndexChanged
        Me.pilihan()
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

End Class
