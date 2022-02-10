Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Penjualan_detil_delivery_order
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
            Me.popupalamatcustomer()
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
        Me.bindalamatcustomer()
    End Sub

    Sub clearalamatcustomer()
        Me.tb_id_alamat_customer.Text = 0
        Me.lbl_alamat_customer.Text = "-----"
        Me.link_popup_alamat_customer.Visible = True
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

    Sub clearsales_order()
        Me.tb_id_no_so.Text = 0
        Me.lbl_no_so.Text = "-----"
        Me.link_popup_no_so.Visible = True
    End Sub

    Sub bindsales_order()
        sqlcom = "select so_no_text, convert(char, tanggal, 103) as tanggal, id_customer"
        sqlcom = sqlcom + " from sales_order"
        sqlcom = sqlcom + " where no = " & Me.tb_id_no_so.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_no_so.Text = reader.Item("so_no_text").ToString
            Me.lbl_tgl_sales_order.Text = reader.Item("tanggal").ToString
            Me.tb_id_customer.Text = reader.Item("id_customer").ToString

            Me.bindcustomer()
            'Me.popupprodukso()

            'Dim vtgl_estimasi As Date
            'Dim vtgl As String = Me.lbl_tgl_sales_order.Text.Substring(3, 2) & "/" & Me.lbl_tgl_sales_order.Text.Substring(0, 2) & "/" & Me.lbl_tgl_sales_order.Text.Substring(6, 4)
            'vtgl_estimasi = DateAdd(DateInterval.Day, 2, CDate(vtgl))
            'Me.tb_tgl_kirim.Text = Day(vtgl_estimasi) & "/" & Month(vtgl_estimasi) & "/" & Year(vtgl_estimasi)
            If String.IsNullOrEmpty(Me.tb_tgl_kirim.Text) Then
                Me.tb_tgl_kirim.Text = Me.lbl_tgl_sales_order.Text
            End If
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
        sqlcom = sqlcom + " sales_order_no, status, do_no_text, is_submit, convert(char, do_received_date, 103) as do_received_date,"
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
            Me.lbl_tgl_terima_gudang.Text = reader.Item("do_received_date").ToString
            Me.lbl_tgl_kirim_gudang.Text = reader.Item("delivery_date").ToString
            Me.lbl_tgl_terima_customer.Text = reader.Item("tgl_diterima_customer").ToString

            If reader.Item("is_submit").ToString = "B" Then
                Me.btn_save.Enabled = True
                Me.btn_submit.Enabled = True
                Me.btn_unsubmit.Enabled = False
                Me.btn_print.Enabled = False
                Me.btn_update.Enabled = True
                Me.btn_delete.Enabled = True
                Me.link_popup_produk.Visible = True
            Else
                Me.btn_save.Enabled = False
                Me.btn_submit.Enabled = False
                Me.btn_unsubmit.Enabled = True
                Me.btn_print.Enabled = True
                Me.btn_update.Enabled = False
                Me.btn_delete.Enabled = False
                Me.link_popup_produk.Visible = False
            End If

            Me.bindcustomer()
            Me.bindalamatcustomer()
            Me.bindsales_order()
            Me.tbl_produk.Visible = True
            Me.popupprodukso()
        Else
            Me.btn_submit.Enabled = False
            Me.btn_unsubmit.Enabled = True
            Me.tbl_produk.Visible = False
            Me.link_popup_produk.Visible = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.bindperiodetransaksi()
            Me.clearalamatcustomer()
            Me.clearsales_order()
            Me.clearproduct()            
            Me.tb_id_customer.Attributes.Add("style", "display: none;")
            Me.tb_id_alamat_customer.Attributes.Add("style", "display: none;")
            Me.tb_id_no_so.Attributes.Add("style", "display: none;")
            Me.tb_id_produk.Attributes.Add("style", "display: none;")
            Me.link_refresh_alamat_customer.Attributes.Add("style", "display: none;")
            Me.link_refresh_no_so.Attributes.Add("style", "display: none;")
            Me.link_refresh_produk.Attributes.Add("style", "display: none;")
            Me.link_popup_no_so.Attributes.Add("onclick", "popup_so_do('" & Me.tb_id_no_so.ClientID & "','" & Me.link_refresh_no_so.UniqueID & "')")
            Me.loaddata()
            Me.loadgrid()

            'If Session.Item("code") <> 1 Then
            '    Me.btn_unsubmit.Visible = False
            'End If

            tradingClass.ViewButtonUnsubmit(Me.btn_unsubmit)

        End If
    End Sub

    Sub popupalamatcustomer()
        Me.link_popup_alamat_customer.Attributes.Add("onclick", "popup_alamat_customer('" & Me.tb_id_customer.Text & "','" & Me.tb_id_alamat_customer.ClientID & "','" & Me.link_refresh_alamat_customer.UniqueID & "')")
    End Sub

    Sub popupprodukso()
        Me.link_popup_produk.Attributes.Add("onclick", "popup_produk_so('" & Me.no_do & "','" & Me.tb_id_no_so.Text & "','" & Me.tb_id_produk.ClientID & "','" & Me.link_refresh_produk.UniqueID & "')")
        Me.link_popup_produk.Enabled = True
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/delivery_order.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&voption=" & Me.voption & "&vsearch=" & Me.vsearch & "&vpaging=" & Me.vpaging)
    End Sub

    Protected Sub link_refresh_alamat_customer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_alamat_customer.Click
        Me.bindalamatcustomer()
    End Sub

    Protected Sub link_refresh_no_so_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_no_so.Click
        Me.bindsales_order()
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
                Dim vtgl_kirim As String = Me.tb_tgl_kirim.Text.Substring(3, 2) & "/" & Me.tb_tgl_kirim.Text.Substring(0, 2) & "/" & Me.tb_tgl_kirim.Text.Substring(6, 4)

                If Me.no_do = 0 Then
                    Dim vtgl_do As String = Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Year
                    Dim vmax As Integer = 0

                    sqlcom = "select isnull(max(no),0) + 1 as vmax from stock_delivery_order"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = reader.Item("vmax").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    Dim vno_do_text As String = ""
                    sqlcom = "select isnull(max(convert(int, right(do_no_text, 5))),0) + 1 as vno_do_text"
                    sqlcom = sqlcom + " from stock_delivery_order"
                    sqlcom = sqlcom + " where convert(int, substring(convert(char, do_no_text), 3,2)) = " & Me.vbulan
                    sqlcom = sqlcom + " and convert(int, left(do_no_text, 2)) = " & Right(Me.vtahun, 2)
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vno_do_text = Right(Me.vtahun, 2) & Me.vbulan.ToString.PadLeft(2, "0") & reader.Item("vno_do_text").ToString.PadLeft(5, "0")
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into stock_delivery_order(no, tanggal, do_type, id_customer,"
                    sqlcom = sqlcom + " seq_alamat, tanggal_kirim, sales_order_no, status,"
                    sqlcom = sqlcom + " do_no_text, is_submit, id_transaction_period, is_received, is_submit_gudang)"
                    sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl_do & "','" & Me.dd_jenis_pengiriman.SelectedValue & "'," & Me.tb_id_customer.Text
                    sqlcom = sqlcom + "," & Me.tb_id_alamat_customer.Text & ",'" & vtgl_kirim & "'," & Me.tb_id_no_so.Text & ",'O',"
                    sqlcom = sqlcom + "'" & vno_do_text & "','B'," & Me.vid_periode_transaksi & ",'B', 'B')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.no_do = vmax
                    Me.tradingClass.Alert("Data sudah disimpan", Me.Page)
                Else
                    sqlcom = "update stock_delivery_order"
                    sqlcom = sqlcom + " set do_type = '" & Me.dd_jenis_pengiriman.SelectedValue & "',"
                    sqlcom = sqlcom + " id_customer = " & Me.tb_id_customer.Text & ","
                    sqlcom = sqlcom + " seq_alamat = " & Me.tb_id_alamat_customer.Text & ","
                    sqlcom = sqlcom + " tanggal_kirim = '" & vtgl_kirim & "',"
                    sqlcom = sqlcom + " sales_order_no = " & Me.tb_id_no_so.Text
                    sqlcom = sqlcom + " where no = " & Me.no_do
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
                Me.loaddata()
            End If
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Sub clearproduct()
        Me.tb_id_produk.Text = 0
        'Me.link_popup_produk.Visible = True
    End Sub

    Sub loadgrid()
        Try
            Me.clearproduct()

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
            sqlcom = sqlcom + " and sales_order_detail.seq = stock_delivery_order_detil.seq_jual_detil"
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
                    Me.btn_update.Visible = True
                    Me.btn_delete.Visible = True
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
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub link_refresh_produk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_produk.Click
        Me.loadgrid()
    End Sub


    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete stock_delivery_order_detil"
                    sqlcom = sqlcom + " where no_delivery_order = " & Me.no_do
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)

                    sqlcom = "update sales_order_detail"
                    sqlcom = sqlcom + " set qty_pending = qty_pending + " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text)
                    sqlcom = sqlcom + " where no_sales_order = " & Me.tb_id_no_so.Text
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.tradingClass.Alert("Data sudah dihapus", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then                    

                    Dim vqty_old As Decimal = 0
                    sqlcom = "select isnull(qty,0) as qty"
                    sqlcom = sqlcom + " from stock_delivery_order_detil"
                    sqlcom = sqlcom + " where no_delivery_order = " & Me.no_do
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vqty_old = reader.Item("qty").ToString
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    If Decimal.ToDouble(vqty_old) <> Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) Then
                        sqlcom = "update stock_delivery_order_detil"
                        sqlcom = sqlcom + " set qty = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text)
                        sqlcom = sqlcom + " where no_delivery_order = " & Me.no_do
                        sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                        connection.koneksi.UpdateRecord(sqlcom)

                        sqlcom = "update sales_order_detail"
                        sqlcom = sqlcom + " set qty_pending = isnull(qty_pending,0) + " & Decimal.ToDouble(vqty_old) & " - " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text)
                        sqlcom = sqlcom + " where no_sales_order = " & Me.tb_id_no_so.Text
                        sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                        connection.koneksi.UpdateRecord(sqlcom)
                    End If

                    Me.tradingClass.Alert("Data sudah diupdate", Me.Page)
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            sqlcom = "select * from stock_delivery_order_detil"
            sqlcom = sqlcom + " where no_delivery_order = " & Me.no_do
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                sqlcom = "update stock_delivery_order"
                sqlcom = sqlcom + " set is_submit = 'S'"
                sqlcom = sqlcom + " where no = " & Me.no_do
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disubmit dan tidak dapat diubah kembali"
                Me.loaddata()
            Else
                Me.tradingClass.Alert("Pengiriman tersebut belum ada item produknya", Me.Page)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
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

    Protected Sub btn_unsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_unsubmit.Click
        Try
            Me.lbl_msg.Text = Nothing

            sqlcom = "select * from stock_delivery_order_detil"
            sqlcom = sqlcom + " where no_delivery_order = " & Me.no_do
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                sqlcom = "update stock_delivery_order"
                sqlcom = sqlcom + " set is_submit = 'B'"
                sqlcom = sqlcom + " where no = " & Me.no_do
                connection.koneksi.UpdateRecord(sqlcom)
                Me.loaddata()
            Else
                Me.tradingClass.Alert("Pengiriman tersebut belum ada item produknya", Me.Page)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class
