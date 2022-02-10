Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_monitoring_kedatangan_barang
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub checkdata()
        Dim vtgl As String = Me.tb_tgl_kedatangan.Text.Substring(3, 2) & "/" & Me.tb_tgl_kedatangan.Text.Substring(0, 2) & "/" & Me.tb_tgl_kedatangan.Text.Substring(6, 4)
        sqlcom = "select *"
        sqlcom = sqlcom + " from purchase_order_detil"
        sqlcom = sqlcom + " inner join product_item on product_item.id = purchase_order_detil.id_product"
        sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
        sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement_conversion"
        sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = purchase_order_detil.po_no"
        sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
        sqlcom = sqlcom + " where purchase_order.received_date_request <= '" & vtgl & "'"
        sqlcom = sqlcom + " and purchase_order_detil.qty_pending > 0"
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

            Dim vtgl As String = Me.tb_tgl_kedatangan.Text.Substring(3, 2) & "/" & Me.tb_tgl_kedatangan.Text.Substring(0, 2) & "/" & Me.tb_tgl_kedatangan.Text.Substring(6, 4)

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select purchase_order_detil.id_product, purchase_order_detil.nama_product,"
            sqlcom = sqlcom + " isnull(purchase_order_detil.qty,0) as qty, isnull(purchase_order_detil.unit_price,0) as unit_price,"
            sqlcom = sqlcom + " isnull(purchase_order_detil.discount,0) as discount, isnull(purchase_order_detil.qty_pending,0) as qty_pending,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + packaging.name + '/' + measurement_unit.name as packaging,"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + " when product_item.is_packaging = 'P' then measurement_unit.name"
            sqlcom = sqlcom + " when product_item.is_packaging = 'Q' then packaging.name"
            sqlcom = sqlcom + " end as satuan_produk, "
            sqlcom = sqlcom + " purchase_order.po_no_text as po_no, convert(char, purchase_order.tanggal, 103) as tgl_pembelian,"
            sqlcom = sqlcom + " convert(char, purchase_order.received_date_request, 103) as tgl_estimasi, daftar_supplier.name as nama_supplier"
            sqlcom = sqlcom + " from purchase_order_detil"
            sqlcom = sqlcom + " inner join product_item on product_item.id = purchase_order_detil.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = purchase_order_detil.po_no"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " where purchase_order.received_date_request <= '" & vtgl & "'"
            sqlcom = sqlcom + " and purchase_order_detil.qty_pending > 0"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(purchase_order_detil.nama_product) like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " order by purchase_order_detil.nama_product"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "purchase_order_detil")
                Me.dg_data.DataSource = ds.Tables("purchase_order_detil").DefaultView

                If ds.Tables("purchase_order_detil").Rows.Count > 0 Then
                    If ds.Tables("purchase_order_detil").Rows.Count > 15 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 15
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
            Me.tb_tgl_kedatangan.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

End Class
