Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_detil_lc
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

    Private ReadOnly Property vno_lc() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_lc")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property no_lc() As Integer
        Get
            Dim o As Object = ViewState("no_lc")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("no_lc") = value
        End Set
    End Property


    Public Property vid_periode() As Integer
        Get
            Dim o As Object = ViewState("vid_periode")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_periode") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindbank()
        Try
            sqlcom = "select id, name from bank_list order by name"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_bank.DataSource = reader
            Me.dd_bank.DataTextField = "name"
            Me.dd_bank.DataValueField = "id"
            Me.dd_bank.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bindjenis_lc()
        Try
            sqlcom = "select code, name from lc_type order by name"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_jenis_lc.DataSource = reader
            Me.dd_jenis_lc.DataTextField = "name"
            Me.dd_jenis_lc.DataValueField = "code"
            Me.dd_jenis_lc.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bindnegara_koresponden()
        Try
            sqlcom = "select id, name from negara_asal_import order by name"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_negara_koresponden.DataSource = reader
            Me.dd_negara_koresponden.DataTextField = "name"
            Me.dd_negara_koresponden.DataValueField = "id"
            Me.dd_negara_koresponden.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bindnegara_asal()
        Try
            sqlcom = "select id, name from negara_asal_import order by name"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_negara_asal.DataSource = reader
            Me.dd_negara_asal.DataTextField = "name"
            Me.dd_negara_asal.DataValueField = "id"
            Me.dd_negara_asal.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bindpelabuhan_asal()
        Try
            sqlcom = "select id, name from port_of_destination order by name"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_dikapalkan_dari.DataSource = reader
            Me.dd_dikapalkan_dari.DataTextField = "name"
            Me.dd_dikapalkan_dari.DataValueField = "id"
            Me.dd_dikapalkan_dari.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bindpelabuhan_tujuan()
        sqlcom = "select id, name from port_of_destination order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_pelabuhan_tujuan.DataSource = reader
        Me.dd_pelabuhan_tujuan.DataTextField = "name"
        Me.dd_pelabuhan_tujuan.DataValueField = "id"
        Me.dd_pelabuhan_tujuan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindperiode()
        Try
            sqlcom = "select id, name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_periode.Text = reader.Item("name").ToString
                Me.vid_periode = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub clearform()
        Me.tb_no_lc.Text = ""
        Me.tb_tgl_lc.Text = ""
        Me.tb_tgl_berlaku_lc.Text = ""
        Me.tb_tgl_jatuh_tempo_lc.Text = ""
    End Sub

    Sub clearpo()
        Me.tb_no_pembelian.Text = 0
        Me.lbl_no_pembelian.Text = "-----"
        Me.link_popup_pembelian.Visible = True
    End Sub

    Sub bindpo()
        Try
            Dim readerpo As SqlClient.SqlDataReader
            sqlcom = "select po_no_text, convert(char, tanggal, 103) as tanggal, id_currency, daftar_supplier.name as nama_supplier"
            sqlcom = sqlcom + " from purchase_order"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " where no = " & Me.tb_no_pembelian.Text
            readerpo = connection.koneksi.SelectRecord(sqlcom)
            readerpo.Read()
            If readerpo.HasRows Then
                Me.lbl_no_pembelian.Text = readerpo.Item("po_no_text").ToString
                Me.lbl_tgl_pembelian.Text = readerpo.Item("tanggal").ToString
                Me.lbl_mata_uang.Text = readerpo.Item("id_currency").ToString
                Me.lbl_nama_supplier.Text = readerpo.Item("nama_supplier").ToString
            End If
            readerpo.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub


    Sub loaddata()
        Try
            If Me.vno_lc <> 0 Then
               Me.no_lc = Me.vno_lc
            End if

            sqlcom = "select purchase_order.po_no_text, convert(char, purchase_order.tanggal, 103) as tanggal, purchase_order.id_currency,"
            sqlcom = sqlcom + " lc.no_po, lc.no_lc, convert(char, lc.tanggal_lc, 103) as tanggal_lc, convert(char, lc.tgl_berlaku_lc, 103) as tgl_berlaku_lc,"
            sqlcom = sqlcom + " lc.id_lc_type, convert(char, lc.due_date_lc, 103) as due_date_lc, lc.id_negara_koresponden, lc.id_dikapalkan_dari, lc.id_bank,"
            sqlcom = sqlcom + " lc.id_pelabuhan_tujuan, lc.id_negara_asal, lc.remarks_lc, isnull(lc.nilai_lc,0) as nilai_lc, daftar_supplier.name as nama_supplier,"
            sqlcom = sqlcom + " transaction_period.name as nama_periode,"
            sqlcom = sqlcom + " (select isnull(sum(isnull(purchase_order_detil.qty,0) * (isnull(purchase_order_detil.unit_price,0) - "
            sqlcom = sqlcom + " isnull(purchase_order_detil.unit_price, 0) * isnull(purchase_order_detil.discount,0) /100)),0) as vtotal_nilai"
            sqlcom = sqlcom + " from purchase_order_detil"
            sqlcom = sqlcom + " where po_no = lc.no_po) as total_nilai_po"
            sqlcom = sqlcom + " from lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = lc.no_po"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " inner join transaction_period on transaction_period.id = purchase_order.id_transaction_period"
            sqlcom = sqlcom + " where lc.seq = " & Me.no_lc          
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tb_no_pembelian.Text = reader.Item("no_po").ToString
                Me.lbl_periode.Text = reader.Item("nama_periode").ToString
                Me.lbl_no_pembelian.Text = reader.Item("po_no_text").ToString
                Me.lbl_tgl_pembelian.Text = reader.Item("tanggal").ToString
                Me.lbl_mata_uang.Text = reader.Item("id_currency").ToString
                Me.lbl_nilai_pembelian.Text = FormatNumber(reader.Item("total_nilai_po").ToString, 2)
                Me.lbl_nama_supplier.Text = reader.Item("nama_supplier").ToString
                Me.dd_bank.SelectedValue = reader.Item("id_bank").ToString
                Me.tb_no_lc.Text = reader.Item("no_lc").ToString
                Me.tb_nilai_lc.Text = FormatNumber(reader.Item("nilai_lc").ToString,2)
                Me.tb_tgl_lc.Text = reader.Item("tanggal_lc").ToString
                Me.tb_tgl_berlaku_lc.Text = reader.Item("tgl_berlaku_lc").ToString
                Me.dd_jenis_lc.SelectedValue = reader.Item("id_lc_type").ToString
                Me.tb_tgl_jatuh_tempo_lc.Text = reader.Item("due_date_lc").ToString
                Me.dd_negara_koresponden.SelectedValue = reader.Item("id_negara_koresponden").ToString
                Me.dd_dikapalkan_dari.SelectedValue = reader.Item("id_dikapalkan_dari").ToString
                Me.dd_pelabuhan_tujuan.SelectedValue = reader.Item("id_pelabuhan_tujuan").ToString
                Me.dd_negara_asal.SelectedValue = reader.Item("id_negara_asal").ToString

                If String.IsNullOrEmpty(reader.Item("remarks_lc").ToString) Then
                    Me.tb_remarks.Text = "CERTIFICATE OF ANALYS = 3 SETS ORIGINAL" & vbCrLf
                    Me.tb_remarks.Text = Me.tb_remarks.Text + "FORM E = 1 ORIGINAL 1 TRIPLICATE" & vbCrLf
                    Me.tb_remarks.Text = Me.tb_remarks.Text + "QUANTITY AND AMOUNT MORE OR LESS THAN 5 PCT ACCEPTABLE ON L/C"
                Else
                    Me.tb_remarks.Text = reader.Item("remarks_lc").ToString
                End If

                Me.bindpo()
            else
                Me.tb_remarks.Text = "CERTIFICATE OF ANALYS = 3 SETS ORIGINAL" & vbCrLf
                Me.tb_remarks.Text = Me.tb_remarks.Text + "FORM E = 1 ORIGINAL 1 TRIPLICATE" & vbCrLf
                Me.tb_remarks.Text = Me.tb_remarks.Text + "QUANTITY AND AMOUNT MORE OR LESS THAN 5 PCT ACCEPTABLE ON L/C"
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.clearpo()
            Me.bindperiode()
            Me.bindbank()
            Me.bindjenis_lc()
            Me.bindnegara_koresponden()
            Me.bindnegara_asal()
            Me.bindpelabuhan_asal()
            Me.bindpelabuhan_tujuan()
            Me.loaddata()
            Me.tb_no_pembelian.Attributes.Add("style", "display: none;")
            Me.link_refresh_pembelian.Attributes.Add("style", "display: none;")
            Me.link_popup_pembelian.Attributes.Add("onclick", "popup_po('" & Me.tb_no_pembelian.ClientID & "','" & Me.link_refresh_pembelian.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/lc.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Protected Sub link_refresh_pembelian_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_pembelian.Click
        Me.bindpo()
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If me.tb_no_pembelian.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor pembelian terlebih dahulu"
            Elseif String.IsNullOrEmpty(Me.tb_nilai_lc.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nilai LC terlebih dahulu"
            Elseif String.IsNullOrEmpty(Me.tb_tgl_berlaku_lc.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal berlaku sampai dengan terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_jatuh_tempo_lc.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal jatuh tempo dengan terlebih dahulu"
            Else
                Dim vtgl_berlaku_lc As String = Me.tb_tgl_berlaku_lc.Text.Substring(3, 2) & "/" & Me.tb_tgl_berlaku_lc.Text.Substring(0, 2) & "/" & Me.tb_tgl_berlaku_lc.Text.Substring(6, 4)
                Dim vtgl_jatuh_tempo_lc As String = Me.tb_tgl_jatuh_tempo_lc.Text.Substring(3, 2) & "/" & Me.tb_tgl_jatuh_tempo_lc.Text.Substring(0, 2) & "/" & Me.tb_tgl_jatuh_tempo_lc.Text.Substring(6, 4)
                Dim vtgl_lc As String = ""
                Dim vtgl_pembayaran1 As String = ""
                Dim vtgl_pembayaran2 As String = ""

                If Not String.IsNullOrEmpty(Me.tb_tgl_lc.Text) Then
                    vtgl_lc = Me.tb_tgl_lc.Text.Substring(3, 2) & "/" & Me.tb_tgl_lc.Text.Substring(0, 2) & "/" & Me.tb_tgl_lc.Text.Substring(6, 4)
                End If

                If Me.no_lc = 0 Then
                    Dim vmax As Integer = 0
                    sqlcom = "select isnull(max(convert(int, right(seq, 4))),0) + 1 as vid"
                    sqlcom = sqlcom + " from lc"
                    sqlcom = sqlcom + " where convert(int, substring(convert(char, seq), 3,2)) = " & Me.vbulan
                    sqlcom = sqlcom + " and convert(int, left(seq, 2)) = " & Right(Me.vtahun, 2)
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        vmax = Right(Me.vtahun, 2) & Me.vbulan.ToString.PadLeft(2, "0") & reader.Item("vid").ToString.PadLeft(3, "0")
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()

                    sqlcom = "insert into lc(seq, no_po , id_bank, no_lc, tanggal_lc, tgl_berlaku_lc, id_lc_type, due_date_lc, id_negara_koresponden, id_dikapalkan_dari,"
                    sqlcom = sqlcom + " id_pelabuhan_tujuan, id_negara_asal,remarks_lc,  id_transaction_period, nilai_lc, is_submit_bayar)"
                    sqlcom = sqlcom + " values(" & vmax & "," & Me.tb_no_pembelian.Text & "," & Me.dd_bank.SelectedValue

                    If String.IsNullOrEmpty(Me.tb_no_lc.Text) Then
                        sqlcom = sqlcom + ", NULL"
                    Else
                        sqlcom = sqlcom + ",'" & Me.tb_no_lc.Text & "'"
                    End If

                    If String.IsNullOrEmpty(Me.tb_tgl_lc.Text) Then
                        sqlcom = sqlcom + ", NULL,"
                    Else
                        sqlcom = sqlcom + ",'" & vtgl_lc & "',"
                    End If

                    sqlcom = sqlcom + "'" & vtgl_berlaku_lc & "'," & Me.dd_jenis_lc.SelectedValue & ","
                    sqlcom = sqlcom + "'" & vtgl_jatuh_tempo_lc & "'," & Me.dd_negara_koresponden.SelectedValue
                    sqlcom = sqlcom + "," & Me.dd_dikapalkan_dari.SelectedValue & "," & Me.dd_pelabuhan_tujuan.SelectedValue
                    sqlcom = sqlcom + "," & Me.dd_negara_asal.SelectedValue & ","
                    sqlcom = sqlcom + "'" & Me.tb_remarks.Text.Replace("'", "''").ToString & "'," & Me.vid_periode
                    sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_nilai_lc.Text) & ",'B')"
                    Me.lbl_msg.Text = sqlcom
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"

                Else
                    sqlcom = "update lc"
                    sqlcom = sqlcom + " set id_bank = " & Me.dd_bank.SelectedValue & ","
                    sqlcom = sqlcom + " no_po = " & Me.tb_no_pembelian.Text & ","

                    If String.IsNullOrEmpty(Me.tb_no_lc.Text) Then
                        sqlcom = sqlcom + " no_lc = NULL,"
                    Else
                        sqlcom = sqlcom + " no_lc = '" & Me.tb_no_lc.Text & "',"
                    End If

                    If String.IsNullOrEmpty(Me.tb_tgl_lc.Text) Then
                        sqlcom = sqlcom + " tanggal_lc = NULL,"
                    Else
                        sqlcom = sqlcom + " tanggal_lc = '" & vtgl_lc & "',"
                    End If

                    sqlcom = sqlcom + " tgl_berlaku_lc = '" & vtgl_berlaku_lc & "',"
                    sqlcom = sqlcom + " id_lc_type = " & Me.dd_jenis_lc.SelectedValue & ","
                    sqlcom = sqlcom + " due_date_lc = '" & vtgl_jatuh_tempo_lc & "',"
                    sqlcom = sqlcom + " id_negara_koresponden = " & Me.dd_negara_koresponden.SelectedValue & ","
                    sqlcom = sqlcom + " id_dikapalkan_dari = " & Me.dd_dikapalkan_dari.SelectedValue & ","
                    sqlcom = sqlcom + " id_pelabuhan_tujuan = " & Me.dd_pelabuhan_tujuan.SelectedValue & ","
                    sqlcom = sqlcom + " id_negara_asal = " & Me.dd_negara_asal.SelectedValue & ","
                    sqlcom = sqlcom + " remarks_lc = '" & Me.tb_remarks.Text.Replace("'", "''").ToString & "',"
                    sqlcom = sqlcom + " nilai_lc = " & Decimal.ToDouble(Me.tb_nilai_lc.Text)
                    sqlcom = sqlcom + " where seq = " & Me.no_lc
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If            
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
        
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Dim reportPath As String = Server.MapPath("reports\lc.rpt")
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
            oRD.SetParameterValue("no_po", Me.no_lc)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lc.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/lc.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_lampiran_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_lampiran.Click
        Try
            Dim reportPath As String = Server.MapPath("reports\lampiran.rpt")
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
            oRD.SetParameterValue("po_no", Me.tb_no_pembelian.Text)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lampiran.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/lampiran.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
