Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_lap_sales_order_list
    Inherits System.Web.UI.UserControl

    Public tradingClass As tradingClass = New tradingClass()

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    sub print()
	Try
            Dim reportPath As String = Server.MapPath("reports\lap_sales_order_listing_by_date.rpt")
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
            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            oRD.SetParameterValue("tgl_awal", vtgl_awal)
            oRD.SetParameterValue("tgl_akhir", vtgl_akhir)
'            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_sales_order_listing_by_date.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/lap_sales_order_listing_by_date.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    end sub


    sub print_new()
        Try
            If Me.RadioButtonPDF.Checked = True And Me.RadioButtonExcel.Checked = False Then


                'Dim reportPath As String = Server.MapPath("reports\lap_sales_order_listing_by_date_new.rpt")

                'dendi
                ' Dim reportPath As String = Server.MapPath("reports\lap_sales_order_listing_by_date_newtes.rpt")

                'Dim reportPath As String = Server.MapPath("reports\lap_sales_order_listing_by_date_newtes1.rpt")
                'Dim reportPath As String = Server.MapPath("reports\testing.rpt")
                Dim reportPath As String = Server.MapPath("reports\testing2.rpt")
                'dendi
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
                Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
                Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

                oRD.SetParameterValue("tgl_awal", vtgl_awal)
                oRD.SetParameterValue("tgl_akhir", vtgl_akhir)
                '            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
                oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
                oExO = oRD.ExportOptions
                oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lap_sales_order_listing_by_date.pdf"))
                Dim vscript As String = ""
                vscript = "<script>" & vbCrLf
                vscript = vscript + "window.open('Pdf_files/lap_sales_order_listing_by_date.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
                vscript = vscript + "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
                connection.koneksi.CloseKoneksi()

            ElseIf Me.RadioButtonPDF.Checked = False And Me.RadioButtonExcel.Checked = True Then

                Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
                Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

                Dim DataTable As DataTable
                sqlcom = Nothing

                sqlcom = "select rtrim(convert(char, sales_order.tanggal, 103)) as 'Date',"
                sqlcom = sqlcom + " sales_order.so_no_text as 'S.O No',"
                sqlcom = sqlcom + " sales_order.no_pajak as 'Faktur Pajak',"
                sqlcom = sqlcom + " daftar_customer.name as 'Customer Name',"
                sqlcom = sqlcom + " sales_order_detail.nama_product as 'Material Name',"
                sqlcom = sqlcom + " sales_order_detail.qty as 'Quantity',"
                sqlcom = sqlcom + " measurement_unit.name as 'Satuan',"
                sqlcom = sqlcom + " case    when sales_order.ppn = 0 then       case           when daftar_customer.is_kawasan_berikat = 'Y' then               case                  when sales_order.id_currency = 'IDR' then                     round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order_detail.qty,0)                  when sales_order.id_currency = 'USD' then                     round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order_detail.qty,3)               end           when daftar_customer.is_kawasan_berikat = 'T' then               case                  when sales_order.id_currency = 'IDR' then                       round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100) / 1.1  * sales_order_detail.qty,0)                    when sales_order.id_currency = 'USD' then                       round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100) / 1.1  * sales_order_detail.qty,3)                 end       end    when sales_order.ppn = 10 then         case             when sales_order.id_currency = 'IDR' then                 round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order_detail.qty,0)             when sales_order.id_currency = 'USD' then 	            round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order_detail.qty,3)         end end as 'Amount',"
                sqlcom = sqlcom + " round( case when sales_order.uang_muka ='Ya' then null  when sales_order.uang_muka = 'Tidak' then case    when sales_order.id_currency = 'USD' then 0    when sales_order.id_currency = 'IDR' then         case            when sales_order.ppn = 0 then                case 				   when daftar_customer.is_kawasan_berikat = 'Y' then 						isnull((select sum((y.harga_jual - y.harga_jual * y.discount /100) * x.rate * y.qty) 						from sales_order x 						inner join sales_order_detail y on y.no_sales_order = sales_order.no 						where x.no = sales_order.no 						and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 						group by x.no),0) 				   when daftar_customer.is_kawasan_berikat = 'T' then 						isnull((select sum((y.harga_jual - y.harga_jual * y.discount /100) / 1.1  * x.rate * y.qty)    						from sales_order x 						inner join sales_order_detail y on y.no_sales_order = sales_order.no 						where x.no = sales_order.no 						and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "'  and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 						group by x.no),0) 				end     		   when sales_order.ppn = 10 then 			  isnull((select sum((y.harga_jual - y.harga_jual * y.discount /100)  * x.rate * y.qty) 			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "'  and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 			   group by x.no),0)        end 	   end end ,0) as 'Total (IDR)',"
                sqlcom = sqlcom + " case    when id_currency = 'RP' then 0    when id_currency = 'USD' then       case       when sales_order.ppn = 0 then         (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty) 	     from sales_order x          inner join sales_order_detail y on y.no_sales_order = sales_order.no          where x.no = sales_order.no          and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "')          group by x.no)       when sales_order.ppn = 10 then          (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty) 	     from sales_order x          inner join sales_order_detail y on y.no_sales_order = sales_order.no          where x.no = sales_order.no          and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "')          group by x.no) +          (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty) * 0.1 	     from sales_order x          inner join sales_order_detail y on y.no_sales_order = sales_order.no          where x.no = sales_order.no          and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "')          group by x.no)       end end as 'Total (USD)',"
                sqlcom = sqlcom + " round( case when sales_order.uang_muka ='Ya' then null  when sales_order.uang_muka = 'Tidak' then case    when sales_order.ppn = 0 then       case          when sales_order.id_currency = 'IDR' then               case                   when daftar_customer.is_kawasan_berikat = 'T' then 					 (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0)  * 					   x.rate) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no 					   where x.no = sales_order.no 					   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 					   group by x.no)                   when daftar_customer.is_kawasan_berikat = 'Y' then 						(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 0)  * 					   x.rate) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no 					   where x.no = sales_order.no 					   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 					   group by x.no)               end          when sales_order.id_currency = 'USD' then               case                  when daftar_customer.is_kawasan_berikat = 'T' then 						(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3)  * 						   x.rate) 						   from sales_order x 						   inner join sales_order_detail y on y.no_sales_order = sales_order.no 						   where x.no = sales_order.no 						   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 						   group by x.no)                  when daftar_customer.is_kawasan_berikat = 'Y' then 						(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3)  * 						   x.rate) 						   from sales_order x 						   inner join sales_order_detail y on y.no_sales_order = sales_order.no 						   where x.no = sales_order.no 						   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 						   group by x.no)               end                    end    when sales_order.ppn = 10 then         case            when sales_order.id_currency = 'IDR' then 			   (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty , 0)  * x.rate) 			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 			   group by x.no)            when sales_order.id_currency = 'USD' then 				(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3) * x.rate) 			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 			   group by x.no)         end 		end end ,0) as 'Total IDR',"
                sqlcom = sqlcom + " round( case when sales_order.uang_muka ='Ya' then null  when sales_order.uang_muka = 'Tidak' then case    when sales_order.ppn = 0 then       case          when sales_order.id_currency = 'IDR' then               case                   when daftar_customer.is_kawasan_berikat = 'T' then 					 (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0) ) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no 					   where x.no = sales_order.no 					   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 					   group by x.no)                   when daftar_customer.is_kawasan_berikat = 'Y' then 0               end          when sales_order.id_currency = 'USD' then               case                  when daftar_customer.is_kawasan_berikat = 'T' then 						(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3)) 						   from sales_order x 						   inner join sales_order_detail y on y.no_sales_order = sales_order.no 						   where x.no = sales_order.no 						   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 						   group by x.no)                  when daftar_customer.is_kawasan_berikat = 'Y' then 0						               end                    end    when sales_order.ppn = 10 then         case            when sales_order.id_currency = 'IDR' then                (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty) 			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 			   group by x.no)            when sales_order.id_currency = 'USD' then                (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty) 			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 			   group by x.no)         end 		end end * 0.1  * sales_order.rate , 0)  as 'PPn',"
                sqlcom = sqlcom + " case when sales_order.uang_muka ='Ya' then null  when sales_order.uang_muka = 'Tidak' then case    when sales_order.ppn = 0 then       case          when sales_order.id_currency = 'IDR' then               case                   when daftar_customer.is_kawasan_berikat = 'T' then 					 round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0)  * 					   x.rate) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no 					   where x.no = sales_order.no 					   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 					   group by x.no),0) +                      round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0)  * 					   x.rate) * 0.1 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no 					   where x.no = sales_order.no 					   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 					   group by x.no),0)                   when daftar_customer.is_kawasan_berikat = 'Y' then 						round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 0)  * 					   x.rate) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no 					   where x.no = sales_order.no 					   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 					   group by x.no),0)               end          when sales_order.id_currency = 'USD' then               case                  when daftar_customer.is_kawasan_berikat = 'T' then 						round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3)  * 						   x.rate) 						   from sales_order x 						   inner join sales_order_detail y on y.no_sales_order = sales_order.no 						   where x.no = sales_order.no 						   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 						   group by x.no),0) +                          round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3)  * 						   x.rate) * 0.1 						   from sales_order x 						   inner join sales_order_detail y on y.no_sales_order = sales_order.no 						   where x.no = sales_order.no 						   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 						   group by x.no),0)                  when daftar_customer.is_kawasan_berikat = 'Y' then 						round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3)  * 						   x.rate) 						   from sales_order x 						   inner join sales_order_detail y on y.no_sales_order = sales_order.no 						   where x.no = sales_order.no 						   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 						   group by x.no),0)               end                    end    when sales_order.ppn = 10 then         case            when sales_order.id_currency = 'IDR' then 			   round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty , 0)  * x.rate) 			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 			   group by x.no),0) +                 round(round(                 (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty) 			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 			   group by x.no) * 0.1 ,3) * sales_order.rate, 0)             when sales_order.id_currency = 'USD' then 				round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3) * x.rate) 			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 			   group by x.no),0) +                                round(round(                 (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty) 			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" & tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim) & "' and x.tanggal <= '" & tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim) & "') 			   group by x.no) * 0.1 ,3) * sales_order.rate, 0)          end 		end end as 'Total Paid',"
                sqlcom = sqlcom + " case when sales_order.id_currency =  'USD' and sales_order.uang_muka = 'Ya' then sales_order.uang_muka_nominal * sales_order.rate  when sales_order.id_currency = 'IDR' and sales_order.uang_muka = 'Ya' then sales_order.uang_muka_nominal  end as 'Total IDR UANG MUKA'"
                sqlcom = sqlcom + " from sales_order inner join daftar_customer on daftar_customer.id = sales_order.id_customer inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no inner join product_item on product_item.id = sales_order_detail.id_product inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion"
                sqlcom = sqlcom + " where (sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "')"
                sqlcom = sqlcom + " order by tanggal"
                DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                tradingClass.WriteExcel("~/Excel_files/lap_sales_order_listing_by_date.xls", DataTable, 1)
                tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_sales_order_listing_by_date.xls", "800", "600", "no", "yes")

            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    end sub

    sub grand_total()
        Try
            sqlcom = "delete temp_grand_total_sos"
            connection.koneksi.DeleteRecord(sqlcom)


            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            'sqlcom = "insert into temp_grand_total_sos (grand_total) select round(case   when sales_order.ppn = 0 then      case         when sales_order.id_currency = 'IDR' then               case                  when daftar_customer.is_kawasan_berikat = 'T' then                       (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0)  * 					   x.rate) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no					   where x.no = sales_order.no 					   and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product					   group by x.no)                  when daftar_customer.is_kawasan_berikat = 'Y' then						(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 0)  * 					   x.rate) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no					   where x.no = sales_order.no 					   and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product					   group by x.no)              end         when sales_order.id_currency = 'USD' then              case                 when daftar_customer.is_kawasan_berikat = 'T' then		      (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3)  * 			x.rate)			from sales_order x 			inner join sales_order_detail y on y.no_sales_order = sales_order.no			where x.no = sales_order.no 			and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product			group by x.no)                 when daftar_customer.is_kawasan_berikat = 'Y' then		      (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3)  * 			x.rate)			from sales_order x 			inner join sales_order_detail y on y.no_sales_order = sales_order.no			where x.no = sales_order.no 			and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product			group by x.no)              end                   end   when sales_order.ppn = 10 then        case           when sales_order.id_currency = 'IDR' then		(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty , 0)  * x.rate)		 from sales_order x 		 inner join sales_order_detail y on y.no_sales_order = sales_order.no		 where x.no = sales_order.no 		 and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product		 group by x.no)           when sales_order.id_currency = 'USD' then		(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3) * x.rate)		 from sales_order x 		 inner join sales_order_detail y on y.no_sales_order = sales_order.no		 where x.no = sales_order.no 		 and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product		 group by x.no)        end end ,0) as total from sales_order inner join daftar_customer on daftar_customer.id = sales_order.id_customer inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no inner join product_item on product_item.id = sales_order_detail.id_product inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion inner join measurement_unit packaging on packaging.id = product_item.id_measurement where (sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "') order by sales_order.tanggal"
            sqlcom = "insert into temp_grand_total_sos (grand_total)"

            'Daniel
            sqlcom = sqlcom + " select"
            sqlcom = sqlcom + " round("
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + "    when sales_order.ppn = 0 then"
            sqlcom = sqlcom + "       case"
            sqlcom = sqlcom + "          when sales_order.id_currency = 'IDR' then "
            sqlcom = sqlcom + "               case"
            sqlcom = sqlcom + "                   when daftar_customer.is_kawasan_berikat = 'T' then"
            sqlcom = sqlcom + "                        (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0)  * "
            sqlcom = sqlcom + " 					   x.rate) "
            sqlcom = sqlcom + " 					   from sales_order x "
            sqlcom = sqlcom + " 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no"
            sqlcom = sqlcom + " 					   where x.no = sales_order.no "
            sqlcom = sqlcom + " 					   and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')"
            sqlcom = sqlcom + " 					   group by x.no)"
            sqlcom = sqlcom + "                   when daftar_customer.is_kawasan_berikat = 'Y' then"
            sqlcom = sqlcom + " 						(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 0)  * "
            sqlcom = sqlcom + " 					   x.rate) "
            sqlcom = sqlcom + " 					   from sales_order x "
            sqlcom = sqlcom + " 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no"
            sqlcom = sqlcom + " 					   where x.no = sales_order.no "
            sqlcom = sqlcom + " 					   and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')"
            sqlcom = sqlcom + " 					   group by x.no)"
            sqlcom = sqlcom + "               end"
            sqlcom = sqlcom + "          when sales_order.id_currency = 'USD' then"
            sqlcom = sqlcom + "               case"
            sqlcom = sqlcom + "                  when daftar_customer.is_kawasan_berikat = 'T' then"
            sqlcom = sqlcom + " 		      (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3)  * "
            sqlcom = sqlcom + " 			x.rate)"
            sqlcom = sqlcom + " 			from sales_order x "
            sqlcom = sqlcom + " 			inner join sales_order_detail y on y.no_sales_order = sales_order.no"
            sqlcom = sqlcom + " 			where x.no = sales_order.no "
            sqlcom = sqlcom + " 			and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')"
            sqlcom = sqlcom + " 			group by x.no)"
            sqlcom = sqlcom + "                  when daftar_customer.is_kawasan_berikat = 'Y' then"
            sqlcom = sqlcom + " 		      (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3)  * "
            sqlcom = sqlcom + " 			x.rate)"
            sqlcom = sqlcom + " 			from sales_order x "
            sqlcom = sqlcom + " 			inner join sales_order_detail y on y.no_sales_order = sales_order.no"
            sqlcom = sqlcom + " 			where x.no = sales_order.no "
            sqlcom = sqlcom + " 			and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')"
            sqlcom = sqlcom + " 			group by x.no)"
            sqlcom = sqlcom + "               end"
            sqlcom = sqlcom + "        end"
            sqlcom = sqlcom + "    when sales_order.ppn = 10 then"
            sqlcom = sqlcom + "         case"
            sqlcom = sqlcom + "            when sales_order.id_currency = 'IDR' then"
            sqlcom = sqlcom + " 		(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty , 0)  * x.rate)"
            sqlcom = sqlcom + " 		 from sales_order x "
            sqlcom = sqlcom + " 		 inner join sales_order_detail y on y.no_sales_order = sales_order.no"
            sqlcom = sqlcom + " 		 where x.no = sales_order.no "
            sqlcom = sqlcom + " 		 and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')"
            sqlcom = sqlcom + " 		 group by x.no)"
            sqlcom = sqlcom + "            when sales_order.id_currency = 'USD' then"
            sqlcom = sqlcom + " 		(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3) * x.rate)"
            sqlcom = sqlcom + " 		 from sales_order x "
            sqlcom = sqlcom + " 		 inner join sales_order_detail y on y.no_sales_order = sales_order.no"
            sqlcom = sqlcom + " 		 where x.no = sales_order.no "
            sqlcom = sqlcom + " 		 and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')"
            sqlcom = sqlcom + " 		 group by x.no)"
            sqlcom = sqlcom + "         end"
            sqlcom = sqlcom + " end ,0) as grand_total_idr"
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no"
            sqlcom = sqlcom + " inner join product_item on product_item.id = sales_order_detail.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where (sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "') and sales_order.uang_muka = 'Tidak'"
            sqlcom = sqlcom + " group by sales_order.so_no_text, sales_order.no, sales_order.ppn, sales_order.id_currency,"
            sqlcom = sqlcom + " sales_order.tanggal, daftar_customer.is_kawasan_berikat"
            sqlcom = sqlcom + " order by tanggal"

            'sqlcom = sqlcom + " select case when sales_order.ppn = 0 then case when daftar_customer.is_kawasan_berikat = 'Y' then round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order.rate * sales_order_detail.qty,0) when daftar_customer.is_kawasan_berikat = 'T' then round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100) / 1.1  * sales_order.rate * sales_order_detail.qty,0)   end when sales_order.ppn = 10 then round((sales_order_detail.harga_jual - sales_order_detail.harga_jual * sales_order_detail.discount /100)  * sales_order.rate * sales_order_detail.qty,0) end as total_amount from sales_order inner join daftar_customer on daftar_customer.id = sales_order.id_customer inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no inner join product_item on product_item.id = sales_order_detail.id_product inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion where (sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "') order by tanggal "
            'Daniel
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    end sub
    'dendi
    Sub total_uangmuka()
        Try
            sqlcom = "delete temp_total_uang_muka"
            connection.koneksi.DeleteRecord(sqlcom)


            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            'sqlcom = "insert into temp_grand_total_sos (grand_total) select round(case   when sales_order.ppn = 0 then      case         when sales_order.id_currency = 'IDR' then               case                  when daftar_customer.is_kawasan_berikat = 'T' then                       (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0)  * 					   x.rate) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no					   where x.no = sales_order.no 					   and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product					   group by x.no)                  when daftar_customer.is_kawasan_berikat = 'Y' then						(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 0)  * 					   x.rate) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no					   where x.no = sales_order.no 					   and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product					   group by x.no)              end         when sales_order.id_currency = 'USD' then              case                 when daftar_customer.is_kawasan_berikat = 'T' then		      (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3)  * 			x.rate)			from sales_order x 			inner join sales_order_detail y on y.no_sales_order = sales_order.no			where x.no = sales_order.no 			and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product			group by x.no)                 when daftar_customer.is_kawasan_berikat = 'Y' then		      (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3)  * 			x.rate)			from sales_order x 			inner join sales_order_detail y on y.no_sales_order = sales_order.no			where x.no = sales_order.no 			and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product			group by x.no)              end                   end   when sales_order.ppn = 10 then        case           when sales_order.id_currency = 'IDR' then		(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty , 0)  * x.rate)		 from sales_order x 		 inner join sales_order_detail y on y.no_sales_order = sales_order.no		 where x.no = sales_order.no 		 and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product		 group by x.no)           when sales_order.id_currency = 'USD' then		(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3) * x.rate)		 from sales_order x 		 inner join sales_order_detail y on y.no_sales_order = sales_order.no		 where x.no = sales_order.no 		 and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')                       and y.id_product = sales_order_detail.id_product		 group by x.no)        end end ,0) as total from sales_order inner join daftar_customer on daftar_customer.id = sales_order.id_customer inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no inner join product_item on product_item.id = sales_order_detail.id_product inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion inner join measurement_unit packaging on packaging.id = product_item.id_measurement where (sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "') order by sales_order.tanggal"
            'sqlcom = "insert into temp_total_uang_muka (total_uang_muka) select convert(decimal(18,2),uang_muka_nominal) as uang_muka from sales_order where (tanggal >= '" & vtgl_awal & "' and tanggal <= '" & vtgl_akhir & "') and uang_muka = 'Ya'"
            sqlcom = "insert into temp_total_uang_muka(total_uang_muka)"
            sqlcom = sqlcom + " select convert(decimal(18,2),"
            sqlcom = sqlcom + " case when id_currency = 'IDR' and uang_muka = 'Ya'  then uang_muka_nominal"
            sqlcom = sqlcom + " when id_currency = 'USD' and uang_muka = 'Ya' then"
            sqlcom = sqlcom + " uang_muka_nominal * rate "

            sqlcom = sqlcom + "  end )  as uang_muka"
            sqlcom = sqlcom + "  from sales_order where (tanggal >= '" & vtgl_awal & "' and tanggal <= '" & vtgl_akhir & "') and uang_muka = 'Ya'  "
            'dendi
            'sqlcom = " select convert(decimal(18,2),uang_muka_nominal) as uang_muka from sales_order where (tanggal >= '" & vtgl_awal & "' and tanggal <= '" & vtgl_akhir & "') and uang_muka = 'Ya'"

            'dendi
            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    'dendi

    sub grand_ppn()
        Try
            sqlcom = "delete temp_grand_ppn_sos"
            connection.koneksi.DeleteRecord(sqlcom)

            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            sqlcom = "insert into temp_grand_ppn_sos(grand_ppn)"
            'Daniel
            'sqlcom = sqlcom + " select "
            'sqlcom = sqlcom + " round(round("
            'sqlcom = sqlcom + " case"
            'sqlcom = sqlcom + "    when sales_order.ppn = 0 then"
            'sqlcom = sqlcom + "       case"
            'sqlcom = sqlcom + "          when sales_order.id_currency = 'IDR' then "
            'sqlcom = sqlcom + "               case"
            'sqlcom = sqlcom + "                   when daftar_customer.is_kawasan_berikat = 'T' then"
            'sqlcom = sqlcom + " 		       (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0))" 
            'sqlcom = sqlcom + " 					   from sales_order x" 
            'sqlcom = sqlcom + " 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no"
            'sqlcom = sqlcom + " 					   where x.no = sales_order.no "
            'sqlcom = sqlcom + " 					   and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')"
            'sqlcom = sqlcom + " 					   group by x.no)"
            'sqlcom = sqlcom + "                   when daftar_customer.is_kawasan_berikat = 'Y' then 0"
            'sqlcom = sqlcom + "               end"
            'sqlcom = sqlcom + "          when sales_order.id_currency = 'USD' then"
            'sqlcom = sqlcom + "               case"
            'sqlcom = sqlcom + "                  when daftar_customer.is_kawasan_berikat = 'T' then"
            'sqlcom = sqlcom + " 						(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3))"
            'sqlcom = sqlcom + " 						   from sales_order x "
            'sqlcom = sqlcom + " 						   inner join sales_order_detail y on y.no_sales_order = sales_order.no"
            'sqlcom = sqlcom + " 						   where x.no = sales_order.no "
            'sqlcom = sqlcom + " 						   and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')"
            'sqlcom = sqlcom + " 						   group by x.no)"
            'sqlcom = sqlcom + "                  when daftar_customer.is_kawasan_berikat = 'Y' then 0"
            'sqlcom = sqlcom + "               end"
            'sqlcom = sqlcom + "        end"
            'sqlcom = sqlcom + "    when sales_order.ppn = 10 then"
            'sqlcom = sqlcom + "         case"
            'sqlcom = sqlcom + "            when sales_order.id_currency = 'IDR' then"
            'sqlcom = sqlcom + "                (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty)"
            'sqlcom = sqlcom + " 			   from sales_order x "
            'sqlcom = sqlcom + " 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no"
            'sqlcom = sqlcom + " 			   where x.no = sales_order.no "
            'sqlcom = sqlcom + " 			   and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')"
            'sqlcom = sqlcom + " 			   group by x.no)"
            'sqlcom = sqlcom + "            when sales_order.id_currency = 'USD' then"
            'sqlcom = sqlcom + "                (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty)"
            'sqlcom = sqlcom + " 			   from sales_order x "
            'sqlcom = sqlcom + " 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no"
            'sqlcom = sqlcom + " 			   where x.no = sales_order.no "
            'sqlcom = sqlcom + " 			   and (x.tanggal >= '" & vtgl_awal & "' and x.tanggal <= '" & vtgl_akhir & "')"
            'sqlcom = sqlcom + " 			   group by x.no)"
            'sqlcom = sqlcom + "         end"
            'sqlcom = sqlcom + " end * 0.1 ,3) * sales_order.rate, 0)"
            'sqlcom = sqlcom + " from sales_order"
            'sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            'sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no"
            'sqlcom = sqlcom + " inner join product_item on product_item.id = sales_order_detail.id_product"
            'sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion"
            'sqlcom = sqlcom + " where (sales_order.tanggal >= '" & vtgl_awal & "' and sales_order.tanggal <= '" & vtgl_akhir & "')"
            'sqlcom = sqlcom + " group by sales_order.so_no_text, sales_order.no, sales_order.ppn, sales_order.id_currency,"
            'sqlcom = sqlcom + " sales_order.tanggal, sales_order.rate, daftar_customer.is_kawasan_berikat"
            'sqlcom = sqlcom + " order by tanggal"
            'Daniel


            sqlcom = sqlcom + " select total_ppn FROM ( select distinct (sales_order.so_no_text), round(case   when sales_order.ppn = 0 then      case         when sales_order.id_currency = 'IDR' then               case                  when daftar_customer.is_kawasan_berikat = 'T' then					 (select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0) ) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no					   where x.no = sales_order.no 					   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "')					   group by x.no)                  when daftar_customer.is_kawasan_berikat = 'Y' then 0              end         when sales_order.id_currency = 'USD' then              case                 when daftar_customer.is_kawasan_berikat = 'T' then						(select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3))						   from sales_order x 						   inner join sales_order_detail y on y.no_sales_order = sales_order.no						   where x.no = sales_order.no 						   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "')						   group by x.no)                 when daftar_customer.is_kawasan_berikat = 'Y' then 0						              end                    end   when sales_order.ppn = 10 then        case           when sales_order.id_currency = 'IDR' then               (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty)			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no			   where x.no = sales_order.no 			   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "')			   group by x.no)           when sales_order.id_currency = 'USD' then               (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty)			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no			   where x.no = sales_order.no 			   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "')			   group by x.no)        end end * 0.1  * sales_order.rate , 0)  as total_ppn from sales_order inner join daftar_customer on daftar_customer.id = sales_order.id_customer inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no inner join product_item on product_item.id = sales_order_detail.id_product inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion where (sales_order.tanggal >= '" + vtgl_awal + "' and sales_order.tanggal <= '" + vtgl_akhir + "')and sales_order.uang_muka = 'Tidak'  ) as temp"




            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    'Daniel
    Sub grand_total_paid()
        Try
            sqlcom = "delete temp_grand_total_paid_sos"
            connection.koneksi.DeleteRecord(sqlcom)

            Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
            Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

            sqlcom = "insert into temp_grand_total_paid_sos(grand_total_paid)"
            'Daniel


            'dendi
            sqlcom = sqlcom + " select total_paid from  (select distinct (sales_order.so_no_text), case    when sales_order.ppn = 0 then       case          when sales_order.id_currency = 'IDR' then                case                   when daftar_customer.is_kawasan_berikat = 'T' then 					 round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0)  * 					   x.rate) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no 					   where x.no = sales_order.no 					   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "') 					   group by x.no),0) +                      round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty /1.1, 0)  * 					   x.rate) * 0.1 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no 					   where x.no = sales_order.no 					   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "') 					   group by x.no),0)                   when daftar_customer.is_kawasan_berikat = 'Y' then 						round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 0)  * 					   x.rate) 					   from sales_order x 					   inner join sales_order_detail y on y.no_sales_order = sales_order.no 					   where x.no = sales_order.no 					   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "') 					   group by x.no),0)               end          when sales_order.id_currency = 'USD' then               case                  when daftar_customer.is_kawasan_berikat = 'T' then 						round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3)  *  						   x.rate) 						   from sales_order x 						   inner join sales_order_detail y on y.no_sales_order = sales_order.no 						   where x.no = sales_order.no 						   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "') 						   group by x.no),0) +                          round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty  /1.1, 3)  *  						   x.rate) * 0.1 						   from sales_order x  						   inner join sales_order_detail y on y.no_sales_order = sales_order.no 						   where x.no = sales_order.no  						   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "') 						   group by x.no),0)                  when daftar_customer.is_kawasan_berikat = 'Y' then 						round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3)  * 						   x.rate) 						   from sales_order x  						   inner join sales_order_detail y on y.no_sales_order = sales_order.no 						   where x.no = sales_order.no  						   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "') 						   group by x.no),0)               end                    end    when sales_order.ppn = 10 then         case            when sales_order.id_currency = 'IDR' then 			   round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty , 0)  * x.rate) 			   from sales_order x 			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no  			   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "') 			   group by x.no),0) +                round(round(                 (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty) 			   from sales_order x  			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "') 			   group by x.no) * 0.1 ,3) * sales_order.rate, 0)            when sales_order.id_currency = 'USD' then 				round((select sum(round((y.harga_jual - y.harga_jual * y.discount /100) * y.qty, 3) * x.rate) 			   from sales_order x  			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no  			   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "') 			   group by x.no),0) +                                round(round(                 (select sum((y.harga_jual - y.harga_jual * y.discount /100) * y.qty) 			   from sales_order x  			   inner join sales_order_detail y on y.no_sales_order = sales_order.no 			   where x.no = sales_order.no 			   and (x.tanggal >= '" + vtgl_awal + "' and x.tanggal <= '" + vtgl_akhir + "') 			   group by x.no) * 0.1 ,3) * sales_order.rate, 0)          end end as total_paid from sales_order inner join daftar_customer on daftar_customer.id = sales_order.id_customer inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no inner join product_item on product_item.id = sales_order_detail.id_product inner join measurement_unit on measurement_unit.id = product_item.id_measurement_conversion where (sales_order.tanggal >= '" + vtgl_awal + "' and sales_order.tanggal <= '" + vtgl_akhir + "') and sales_order.uang_muka = 'Tidak' ) as total_paid  "
            'dendi



            connection.koneksi.InsertRecord(sqlcom)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
    'Daniel


    Protected Sub btn_print13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print13.Click
        Me.grand_total()
        Me.grand_ppn()
        Me.print()
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Me.grand_total()
        Me.grand_ppn()

        'Daniel
        Me.grand_total_paid()
        'Daniel

        'dendi
        Me.total_uangmuka()
        'dendi

        Me.print_new()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tgl_awal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
            Me.tb_tgl_akhir.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
        End If
    End Sub
End Class
