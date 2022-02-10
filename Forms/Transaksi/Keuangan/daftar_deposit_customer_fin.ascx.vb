Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_daftar_deposit_customer_fin
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub hitungdeposit()
        Try
            sqlcom = "delete deposit_customer"
            connection.koneksi.DeleteRecord(sqlcom)

            'kelebihan IDR
            sqlcom = "insert into deposit_customer(id_customer, jumlah_idr)"
            sqlcom = sqlcom + "select sales_order.id_customer, isnull(sum(isnull(pembayaran_invoice_penjualan.kelebihan,0)),0) as vkelebihan"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
            sqlcom = sqlcom + " inner join sales_order on sales_order.no = pembayaran_invoice_penjualan.no_so"
            sqlcom = sqlcom + " where pembayaran_invoice_penjualan.id_currency = 'IDR'"
            sqlcom = sqlcom + " group by sales_order.id_customer"
            connection.koneksi.InsertRecord(sqlcom)


            'kelebihan USD
            Dim readerusd As SqlClient.SqlDataReader
            sqlcom = "select id_customer from deposit_customer order by id_customer"
            readerusd = connection.koneksi.SelectRecord(sqlcom)
            Do While readerusd.Read()
                sqlcom = "select isnull(sum(isnull(pembayaran_invoice_penjualan.kelebihan,0)),0) as vkelebihan"
                sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
                sqlcom = sqlcom + " inner join sales_order on sales_order.no = pembayaran_invoice_penjualan.no_so"
                sqlcom = sqlcom + " where pembayaran_invoice_penjualan.id_currency = 'USD'"
                sqlcom = sqlcom + " and sales_order.id_customer = " & readerusd.Item("id_customer").ToString
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    sqlcom = "update deposit_customer"
                    sqlcom = sqlcom + " set jumlah_usd = " & Decimal.ToDouble(reader.Item("vkelebihan").ToString)
                    sqlcom = sqlcom + " where id_customer = " & readerusd.Item("id_customer").ToString
                    connection.koneksi.UpdateRecord(sqlcom)
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()
            Loop
            readerusd.Close()
            connection.koneksi.CloseKoneksi()

            'pembayaran dengan deposit IDR
            sqlcom = "select sales_order.id_customer, isnull(sum(isnull(pembayaran_invoice_penjualan.kelebihan,0)),0) as vpakai_kelebihan"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
            sqlcom = sqlcom + " inner join sales_order on sales_order.no = pembayaran_invoice_penjualan.no_so"
            sqlcom = sqlcom + " where pembayaran_invoice_penjualan.id_jenis_pembayaran = 5"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.id_currency = 'IDR'"
            sqlcom = sqlcom + " group by sales_order.id_customer"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                sqlcom = "update deposit_customer"
                sqlcom = sqlcom + " set jumlah_idr = isnull(jumlah_idr,0) - " & Decimal.ToDouble(reader.Item("vpakai_kelebihan").ToString)
                sqlcom = sqlcom + " where id_customer = " & reader.Item("id_customer").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()


            'pembayaran dengan deposit USD
            sqlcom = "select sales_order.id_customer, isnull(sum(isnull(pembayaran_invoice_penjualan.kelebihan,0)),0) as vpakai_kelebihan"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan"
            sqlcom = sqlcom + " inner join sales_order on sales_order.no = pembayaran_invoice_penjualan.no_so"
            sqlcom = sqlcom + " where pembayaran_invoice_penjualan.id_jenis_pembayaran = 5"
            sqlcom = sqlcom + " and pembayaran_invoice_penjualan.id_currency = 'USD'"
            sqlcom = sqlcom + " group by sales_order.id_customer"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                sqlcom = "update deposit_customer"
                sqlcom = sqlcom + " set jumlah_usd = isnull(jumlah_usd,0) - " & Decimal.ToDouble(reader.Item("vpakai_kelebihan").ToString)
                sqlcom = sqlcom + " where id_customer = " & reader.Item("id_customer").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id_customer, isnull(jumlah_idr,0) as jumlah_idr, isnull(jumlah_usd,0) as jumlah_usd,"
            sqlcom = sqlcom + " daftar_customer.name as nama_customer"
            sqlcom = sqlcom + " from deposit_customer"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = deposit_customer.id_customer"
            sqlcom = sqlcom + " where isnull(jumlah_idr,0) <> 0 or isnull(jumlah_usd,0) <> 0"
            sqlcom = sqlcom + " order by daftar_customer.name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "deposit_customer")
                Me.dg_data.DataSource = ds.Tables("deposit_customer").DefaultView

                If ds.Tables("deposit_customer").Rows.Count > 0 Then
                    If ds.Tables("deposit_customer").Rows.Count > 15 Then
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
            Me.hitungdanload()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Sub hitungdanload()
        Dim thread = New System.Threading.Thread(New Threading.ThreadStart(AddressOf hitungdeposit))
        thread.IsBackground = True

        Me.loadgrid()
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.hitungdanload()
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class
