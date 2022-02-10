Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Collection_daftar_piutang_per_sales
    Inherits System.Web.UI.UserControl

    Public Property per_sales() As String
        Get
            Dim o As Object = ViewState("per_sales")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("per_sales") = value
        End Set
    End Property

    Public Property vid_sales() As Integer
        Get
            Dim o As Object = ViewState("vid_sales")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_sales") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindtotal()
        Try
            sqlcom = " select isnull(sum(isnull(nilai_idr,0)),0) as nilai_idr, isnull(sum(isnull(nilai_usd,0)),0) as nilai_usd"
            sqlcom = sqlcom + " from temp_piutang_customer"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_total_idr.Text = FormatNumber(reader.Item("nilai_idr").ToString, 2)
                Me.lbl_total_usd.Text = FormatNumber(reader.Item("nilai_usd").ToString, 2)
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

            sqlcom = " select nama_sales, id_sales, isnull(sum(isnull(nilai_idr,0)),0) as nilai_idr, isnull(sum(isnull(nilai_usd,0)),0) as nilai_usd"
            sqlcom = sqlcom + " from temp_piutang_customer"
            sqlcom = sqlcom + " group by nama_sales, id_sales"
            sqlcom = sqlcom + " order by nama_sales"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "temp_piutang_customer")
                Me.dg_data.DataSource = ds.Tables("temp_piutang_customer").DefaultView

                If ds.Tables("temp_piutang_customer").Rows.Count > 0 Then
                    If ds.Tables("temp_piutang_customer").Rows.Count > 20 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 20
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_print.Enabled = True
                    Me.tbl_search.Visible = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_print.Enabled = False
                    Me.tbl_search.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()

            Me.bindtotal()

        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub hitung_piutang()
        Try

            sqlcom = "delete temp_piutang_customer"
            connection.koneksi.DeleteRecord(sqlcom)

            Dim vtgl_jatuh_tempo As String = Me.tb_tgl_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(6, 4)
            Dim vtgl_jatuh_tempo_akhir As String = Me.tb_tgl_jatuh_tempo_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo_akhir.Text.Substring(6, 4)

            sqlcom = "insert into temp_piutang_customer(id_sales, id_customer, no_so_text, nama_customer, nama_sales, tgl_invoice, tgl_jatuh_tempo,"
            sqlcom = sqlcom + " nilai_idr, nilai_usd, nilai_ppn_usd)"
            sqlcom = sqlcom + " select sales_order.id_sales, sales_order.id_customer, sales_order.so_no_text, daftar_customer.name, user_list.nama_pegawai, sales_order.tanggal,"
            sqlcom = sqlcom + " sales_order.tgl_jatuh_tempo,"

            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then "
            sqlcom = sqlcom + "        case"
            sqlcom = sqlcom + "           when sales_order.ppn = 0 then          "
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no)"
            sqlcom = sqlcom + "           when sales_order.ppn = 10 then"
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no) +"
            sqlcom = sqlcom + "                (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                 where no_sales_order = sales_order.no) * 0.1"
            sqlcom = sqlcom + "        end"
            sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then 0"
            sqlcom = sqlcom + " end"

            sqlcom = sqlcom + " - "

            sqlcom = sqlcom + " isnull((select isnull(sum(isnull(nilai_pembayaran,0) +  isnull(potongan,0) - isnull(kelebihan,0)),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no"
            sqlcom = sqlcom + " and id_currency = 'IDR'),0)"

            sqlcom = sqlcom + " as total_nilai_idr, "

            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then 0 "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then  "
            sqlcom = sqlcom + "        case"
            sqlcom = sqlcom + "          when sales_order.ppn = 0 then"
            sqlcom = sqlcom + "               (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                where no_sales_order = sales_order.no) "
            sqlcom = sqlcom + "          when sales_order.ppn = 10 then"
            sqlcom = sqlcom + "               (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                where no_sales_order = sales_order.no) +"
            sqlcom = sqlcom + "               (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                where no_sales_order = sales_order.no) * 0.1"
            sqlcom = sqlcom + "       end"
            sqlcom = sqlcom + " end"

            sqlcom = sqlcom + " - "

            sqlcom = sqlcom + " isnull((select isnull(sum((isnull(nilai_pembayaran,0) + isnull(potongan,0) - isnull(kelebihan,0))),0)"
            sqlcom = sqlcom + " from pembayaran_invoice_penjualan where no_so = sales_order.no"
            sqlcom = sqlcom + " and id_currency = 'USD'),0) "

            sqlcom = sqlcom + " as total_nilai_usd,"

            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then 0 "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then  "
            sqlcom = sqlcom + "        case"
            sqlcom = sqlcom + "          when sales_order.ppn = 0 then 0"
            sqlcom = sqlcom + "          when sales_order.ppn = 10 then"
            sqlcom = sqlcom + "               round(isnull((select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                where no_sales_order = sales_order.no) / 1.1 * 0.1 * isnull(sales_order.rate,0),0),0)"
            sqlcom = sqlcom + "       end"

            sqlcom = sqlcom + " end as total_nilai_pajak_usd"
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " inner join user_list on user_list.code = sales_order.id_sales"
            sqlcom = sqlcom + " where sales_order.tgl_jatuh_tempo >= '" & vtgl_jatuh_tempo & "'"
            sqlcom = sqlcom + " and sales_order.tgl_jatuh_tempo <= '" & vtgl_jatuh_tempo_akhir & "'"
            sqlcom = sqlcom + " and sales_order.tanggal >= '01/01/2012'"
            sqlcom = sqlcom + " and sales_order.status_invoice = 'B'"
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tgl_jatuh_tempo.Text = "01" & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year
            Me.tb_tgl_jatuh_tempo_akhir.Text = Date.DaysInMonth(Now.Year, Now.Month) & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year
            Me.tbl_search.Visible = False
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.hitung_piutang()
        Me.loadgrid()
    End Sub

    Sub print()
        Try
            Dim reportPath As String

            If Me.per_sales = "N" Then
                reportPath = Server.MapPath("reports\lap_piutang_per_sales_all.rpt")
            Else
                reportPath = Server.MapPath("reports\lap_piutang_per_sales.rpt")
            End If

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
            Dim vtgl_jatuh_tempo As String = Me.tb_tgl_jatuh_tempo.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo.Text.Substring(6, 4)
            Dim vtgl_jatuh_tempo_akhir As String = Me.tb_tgl_jatuh_tempo_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo_akhir.Text.Substring(6, 4)

            oRD.SetParameterValue("tgl_jatuh_tempo", vtgl_jatuh_tempo)
            oRD.SetParameterValue("tgl_jatuh_tempo_akhir", vtgl_jatuh_tempo_akhir)

            If Me.per_sales = "Y" Then
                oRD.SetParameterValue("id_sales", Me.vid_sales)
            End If

            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_piutang_per_sales.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/lap_piutang_per_sales.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Me.per_sales = "N"
        Me.print()
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkPrint" Then
            Me.per_sales = "Y"
            Me.vid_sales = CType(e.Item.FindControl("lbl_id_sales"), Label).Text
            Me.print()
        End If
    End Sub
End Class
