Imports System.Configuration
Imports System.Data

Partial Class Forms_report_lap_stock_barang_gudang
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode_transaksi()
        try
          sqlcom = "select id, name"
          sqlcom = sqlcom + " from transaction_period"
          sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
          sqlcom = sqlcom + " and id > 11"
          sqlcom = sqlcom + " order by bulan"
          reader = connection.koneksi.SelectRecord(sqlcom)
          Me.dd_bulan.DataSource = reader
          Me.dd_bulan.DataTextField = "name"
          Me.dd_bulan.DataValueField = "id"
          Me.dd_bulan.DataBind()
          reader.Close()
          connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tahun.Text = Now.Year
            Me.bindperiode_transaksi()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_refresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        Me.bindperiode_transaksi()
    End Sub

    Sub hitung_stock()
        Try
               if me.dd_bulan.selectedvalue > 17 then
                   'delete stok bulan yang dipilih
                   sqlcom = "delete stock_barang_gudang"
                   sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
                   connection.koneksi.deleterecord(sqlcom)
                   
                    'insert stok bulan yang dipilih
                sqlcom = "select * from stock_barang_gudang"
                    sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
                    reader = connection.koneksi.selectRecord(sqlcom)
                    reader.read()
                    if not reader.hasRows then
                       sqlcom = "insert into stock_barang_gudang(id_transaction_period, id_product, qty_stock)"
                       sqlcom = sqlcom + " select " & me.dd_bulan.selectedvalue & ", id_product, isnull(qty_stock,0) + isnull(qty_masuk,0) - isnull(qty_jual,0)"
                       sqlcom = sqlcom + " from stock_barang_gudang"
                       sqlcom = sqlcom + " where id_transaction_period = " & me.dd_bulan.selectedvalue - 1
                       connection.koneksi.insertrecord(sqlcom)
                    End if
                    reader.close()
                    connection.koneksi.CloseKoneksi()
               end if

            'update qty masuk
                sqlcom = "update stock_barang_gudang"
                sqlcom = sqlcom + " set qty_masuk = "
                sqlcom = sqlcom + " isnull((select isnull(sum(isnull(entry_dokumen_impor_produk.qty_terima,0)),0) from entry_dokumen_impor_produk"
                sqlcom = sqlcom + " where entry_dokumen_impor_produk.id_product = stock_barang_gudang.id_product"
                sqlcom = sqlcom + " and entry_dokumen_impor_produk.seq_entry in (select seq from entry_dokumen_impor" 
                sqlcom = sqlcom + " where entry_dokumen_impor.id_transaksi_terima_barang = stock_barang_gudang.id_transaction_period)),0)"
                sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
                connection.koneksi.updaterecord(sqlcom)

            'update qty keluar
                sqlcom = "update stock_barang_gudang"
                sqlcom = sqlcom + " set qty_jual = "
                sqlcom = sqlcom + " isnull((select isnull(sum(isnull(sales_order_detail.qty,0)),0) from sales_order_detail "
                sqlcom = sqlcom + " where sales_order_detail.id_product = stock_barang_gudang.id_product "
                sqlcom = sqlcom + " and sales_order_detail.no_sales_order in (select sales_order.no from sales_order "
                sqlcom = sqlcom + " where sales_order.id_transaction_period = stock_barang_gudang.id_transaction_period)),0) "
                sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue 
                connection.koneksi.updaterecord(sqlcom)                
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try    
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
                Dim reportPath As String = nothing
                if me.dd_bulan.selectedvalue > 12 then
                   Me.hitung_stock()
                   reportPath = Server.MapPath("reports\lap_stock_barang_gudang.rpt")
                else
                   reportPath = Server.MapPath("reports\lap_stock_barang_gudang082012.rpt")	
                end if


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

                oRD.SetParameterValue("id_periode", Me.dd_bulan.SelectedValue)

                oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
                oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
                oExO = oRD.ExportOptions
                oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_stock_barang_gudang.pdf"))
                Dim vscript As String = ""
                vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/lap_stock_barang_gudang.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
                vscript = vscript + "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
                connection.koneksi.CloseKoneksi()
            
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub



End Class
