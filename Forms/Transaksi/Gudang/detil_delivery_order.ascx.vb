Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Gudang_detil_delivery_order
    Inherits System.Web.UI.UserControl

    Public tradingClass As tradingClass = New tradingClass()

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

    Private ReadOnly Property vno_do() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_do")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vpaging() As String
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Public Property no_do() As Integer
        Get
            Dim o As Object = ViewState("no_do")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("no_do") = value
        End Set
    End Property

    Public Property vid_periode_transaksi() As Integer
        Get
            Dim o As Object = ViewState("vid_periode_transaksi")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_periode_transaksi") = value
        End Set
    End Property

    Public Property vqty_packaging() As Decimal
        Get
            Dim o As Object = ViewState("vqty_packaging")
            If Not o Is Nothing Then Return CDec(o) Else Return 0
        End Get
        Set(ByVal value As Decimal)
            ViewState("vqty_packaging") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiodetransaksi()
        sqlcom = "select id, name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_periode_transaksi.Text = reader.Item("name").ToString
            Me.vid_periode_transaksi = reader.Item("id").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindcustomer()
        sqlcom = "select name from daftar_customer where id = " & Me.tb_id_customer.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_customer.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindalamatcustomer()
        sqlcom = "select alamat from daftar_customer_alamat"
        sqlcom = sqlcom + " where id_customer = " & Me.tb_id_customer.Text
        sqlcom = sqlcom + " and seq = " & Me.tb_id_alamat_customer.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_alamat_customer.Text = reader.Item("alamat").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindsales_order()
        sqlcom = "select so_no_text, convert(char, tanggal, 103) as tanggal from sales_order"
        sqlcom = sqlcom + " where no = " & Me.tb_id_no_so.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_no_so.Text = reader.Item("so_no_text").ToString
            Me.lbl_tgl_sales_order.Text = reader.Item("tanggal").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()
        Me.tb_tgl_kirim.Text = ""
        Me.no_do = 0
    End Sub

    Sub loaddata()
        If Me.vno_do <> 0 Then
            Me.no_do = Me.vno_do
        End If

        sqlcom = "select no, convert(char, tanggal, 103) as tanggal, do_type, id_customer, seq_alamat, convert(char, tanggal_kirim, 103) as tanggal_kirim,"
        sqlcom = sqlcom + " sales_order_no, status, do_no_text, is_submit_gudang, convert(char, do_received_date, 103) as do_received_date,"
        sqlcom = sqlcom + " convert(char, delivery_date, 103) as delivery_date, convert(char, tgl_diterima_customer, 103) as tgl_diterima_customer"
        sqlcom = sqlcom + " from stock_delivery_order"
        sqlcom = sqlcom + " where no = " & Me.no_do
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_no_pengiriman.Text = reader.Item("do_no_text").ToString
            Me.lbl_tgl_pengiriman.Text = reader.Item("tanggal").ToString
            Me.dd_jenis_pengiriman.SelectedValue = reader.Item("do_type").ToString
            Me.tb_id_customer.Text = reader.Item("id_customer").ToString
            Me.tb_id_alamat_customer.Text = reader.Item("seq_alamat").ToString
            Me.tb_id_no_so.Text = reader.Item("sales_order_no").ToString
            Me.tb_tgl_kirim.Text = reader.Item("tanggal_kirim").ToString
            Me.tb_tgl_terima_gudang.Text = reader.Item("do_received_date").ToString
            Me.tb_tgl_kirim_gudang.Text = reader.Item("delivery_date").ToString
            Me.tb_tgl_terima_customer.Text = reader.Item("tgl_diterima_customer").ToString

            If reader.Item("is_submit_gudang").ToString = "B" Then
                Me.btn_save.Enabled = True
                Me.btn_submit.Enabled = True
            Else
                Me.btn_save.Enabled = False
                Me.btn_submit.Enabled = False
            End If

            Me.bindcustomer()
            Me.bindalamatcustomer()
            Me.bindsales_order()
            Me.tbl_produk.Visible = True
        Else
            Me.btn_submit.Enabled = False
            Me.tbl_produk.Visible = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.bindperiodetransaksi()
            Me.loaddata()
            Me.loadgrid()
            Me.tb_id_customer.Attributes.Add("style", "display: none;")
            Me.tb_id_alamat_customer.Attributes.Add("style", "display: none;")
            Me.tb_id_no_so.Attributes.Add("style", "display: none;")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/delivery_order_list.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&vpaging=" & Me.vpaging)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_customer.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama customer terlebih dahulu"
            ElseIf Me.tb_id_alamat_customer.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi alamat customer terlebih dahulu"
            ElseIf Me.tb_id_no_so.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor sales order terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_kirim.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi estimasi tanggal kirim terlebih dahulu"
            Else
                Dim vtgl_terima_gudang As String = ""
                Dim vtgl_kirim As String = ""
                Dim vtgl_terima_customer As String = ""

                If Not String.IsNullOrEmpty(Me.tb_tgl_terima_gudang.Text) Then
                    vtgl_terima_gudang = Me.tb_tgl_terima_gudang.Text.Substring(3, 2) & "/" & Me.tb_tgl_terima_gudang.Text.Substring(0, 2) & "/" & Me.tb_tgl_terima_gudang.Text.Substring(6, 4)
                End If

                If Not String.IsNullOrEmpty(Me.tb_tgl_kirim_gudang.Text) Then
                    vtgl_kirim = Me.tb_tgl_kirim_gudang.Text.Substring(3, 2) & "/" & Me.tb_tgl_kirim_gudang.Text.Substring(0, 2) & "/" & Me.tb_tgl_kirim_gudang.Text.Substring(6, 4)
                End If

                If Not String.IsNullOrEmpty(Me.tb_tgl_terima_customer.Text) Then
                    vtgl_terima_customer = Me.tb_tgl_terima_customer.Text.Substring(3, 2) & "/" & Me.tb_tgl_terima_customer.Text.Substring(0, 2) & "/" & Me.tb_tgl_terima_customer.Text.Substring(6, 4)
                End If

                sqlcom = "update stock_delivery_order"

                If String.IsNullOrEmpty(Me.tb_tgl_terima_gudang.Text) Then
                    sqlcom = sqlcom + " set do_received_date = NULL,"
                Else
                    sqlcom = sqlcom + " set do_received_date = '" & vtgl_terima_gudang & "',"
                End If

                If String.IsNullOrEmpty(Me.tb_tgl_kirim_gudang.Text) Then
                    sqlcom = sqlcom + " delivery_date = NULL,"
                Else
                    sqlcom = sqlcom + " delivery_date = '" & vtgl_kirim & "',"
                End If

                If String.IsNullOrEmpty(Me.tb_tgl_terima_customer.Text) Then
                    sqlcom = sqlcom + " tgl_diterima_customer = NULL"
                Else
                    sqlcom = sqlcom + " tgl_diterima_customer = '" & vtgl_terima_customer & "'"
                End If
                sqlcom = sqlcom + " where no = " & Me.no_do
                connection.koneksi.UpdateRecord(sqlcom)
                Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
            End If
            Me.loaddata()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select stock_delivery_order_detil.id_product, stock_delivery_order_detil.qty,"
            sqlcom = sqlcom + " sales_order_detail.nama_product, "
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + unit_packaging.name + '/' + measurement_unit.name as packaging,"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + " when product_item.is_packaging = 'P' then measurement_unit.name"
            sqlcom = sqlcom + " when product_item.is_packaging = 'Q' then unit_packaging.name"
            sqlcom = sqlcom + " end as satuan_produk "
            sqlcom = sqlcom + " from stock_delivery_order_detil"
            sqlcom = sqlcom + " inner join stock_delivery_order on stock_delivery_order.no = stock_delivery_order_detil.no_delivery_order"
            sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.id_product = stock_delivery_order_detil.id_product"
            sqlcom = sqlcom + " inner join product_item on product_item.id = stock_delivery_order_detil.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit unit_packaging on unit_packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where no_delivery_order = " & Me.no_do
            sqlcom = sqlcom + " and stock_delivery_order.sales_order_no = sales_order_detail.no_sales_order"
            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "stock_delivery_order_detil")
                Me.dg_data.DataSource = ds.Tables("stock_delivery_order_detil").DefaultView

                If ds.Tables("stock_delivery_order_detil").Rows.Count > 0 Then
                    If ds.Tables("stock_delivery_order_detil").Rows.Count > 10 Then
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
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tgl_terima_gudang.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terima gudang terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_kirim_gudang.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal kirim gudang terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_terima_customer.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terima customer terlebih dahulu"
            Else
                sqlcom = "update stock_delivery_order"
                sqlcom = sqlcom + " set is_submit_gudang = 'S'"
                sqlcom = sqlcom + " where no = " & Me.no_do
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disubmit dan tidak dapat diubah kembali"
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Dim reportPath As String = Server.MapPath("reports\delivery_order.rpt")
            Me.CrystalReportSource1.Report.FileName = reportPath
            Me.CrystalReportSource1.ReportDocument.Close()
            Me.CrystalReportSource1.ReportDocument.Refresh()
            Dim oExO As CrystalDecisions.Shared.ExportOptions
            Dim oExDo As New CrystalDecisions.Shared.DiskFileDestinationOptions()
            Dim con As New System.Data.SqlClient.SqlConnectionStringBuilder

            con.ConnectionString = ConfigurationManager.ConnectionStrings("trading").ToString
            Dim consinfo As New CrystalDecisions.Shared.ConnectionInfo
            consinfo.ServerName = con.DataSource
            consinfo.UserID = con.UserID
            consinfo.DatabaseName = con.InitialCatalog
            consinfo.Password = con.Password
            consinfo.Type = CrystalDecisions.Shared.ConnectionInfoType.SQL
            Dim oRD As CrystalDecisions.CrystalReports.Engine.ReportDocument = Me.CrystalReportSource1.ReportDocument
            Dim myTables As CrystalDecisions.CrystalReports.Engine.Tables = oRD.Database.Tables
            For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                Dim myTableLogonInfo As CrystalDecisions.Shared.TableLogOnInfo = myTable.LogOnInfo
                myTableLogonInfo.ConnectionInfo = consinfo
                myTable.ApplyLogOnInfo(myTableLogonInfo)
            Next
            oRD.Load(reportPath)
            oRD.SetParameterValue("do_no", Me.no_do)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/delivery_order.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/delivery_order.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class

