Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_Accounting_laporan_bad_debt
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub print()
        Try
            Dim reportPath As String = Nothing

            If Me.dd_pilihan.SelectedValue = "A" Then
                reportPath = Server.MapPath("reports\lap_bad_debt.rpt")
            Else
                reportPath = Server.MapPath("reports\lap_bad_debt_hari.rpt")
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

            If Me.dd_pilihan.SelectedValue <> "A" Then
                oRD.SetParameterValue("jml_hari", Me.tb_hari.Text)
                oRD.SetParameterValue("jml_hari_ke", Me.tb_hari_ke.Text)
            End If

            oRD.Load(reportPath)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_bad_debt.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/lap_bad_debt.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Me.hitung_piutang()
        Me.print()
    End Sub

    Sub hitung_piutang()
        Try

            sqlcom = "delete temp_piutang_customer"
            connection.koneksi.DeleteRecord(sqlcom)

            Dim vtgl_jatuh_tempo As String = Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString

            sqlcom = "insert into temp_piutang_customer(id_sales, id_customer, no_so_text, nama_customer,"
            sqlcom = sqlcom + "nama_sales, tgl_invoice, tgl_jatuh_tempo, nilai_idr, nilai_usd, nilai_ppn_usd)"
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
            sqlcom = sqlcom + " end as total_nilai_idr, "
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'IDR' then 0 "
            sqlcom = sqlcom + "    when sales_order.id_currency = 'USD' then  "
            sqlcom = sqlcom + "               (select sum(isnull(qty,0) * (isnull(harga_jual,0) - isnull(harga_jual,0) * isnull(discount,0) / 100)) from sales_order_detail "
            sqlcom = sqlcom + "                where no_sales_order = sales_order.no) "
            sqlcom = sqlcom + " end as total_nilai_usd,"
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
            sqlcom = sqlcom + " where sales_order.tgl_jatuh_tempo <= '" & vtgl_jatuh_tempo & "'"
            sqlcom = sqlcom + " and sales_order.tanggal >= '01/01/2012'"
            sqlcom = sqlcom + " and sales_order.status_invoice = 'B'"
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_hari.Visible = False
            Me.lbl_sd.Visible = False
            Me.tb_hari_ke.Visible = False
        End If
    End Sub

    Protected Sub dd_pilihan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_pilihan.SelectedIndexChanged
        If Me.dd_pilihan.SelectedValue = "A" Then
            Me.tb_hari.Visible = False
            Me.lbl_sd.Visible = False
            Me.tb_hari_ke.Visible = False
        Else
            Me.tb_hari.Visible = True
            Me.lbl_sd.Visible = True
            Me.tb_hari_ke.Visible = True
        End If
    End Sub
End Class
