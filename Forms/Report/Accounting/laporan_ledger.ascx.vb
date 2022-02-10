Imports System.Configuration
Imports System.Data

Partial Class Forms_Report_Accounting_laporan_ledger
    Inherits System.Web.UI.UserControl

    Public tradingClass As tradingClass = New tradingClass()

    Public saldo_akhir As Decimal

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String
    Dim sub_sqlcom As String

    Sub clearakun()
        Me.tb_id_akun.Text = "0"
        Me.lbl_akun.Text = "------"
        Me.link_popup_akun.Visible = True
    End Sub

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

    Sub bindakun()
        sqlcom = "select RTRIM(LTRIM(accountno)) + ' ' + RTRIM(LTRIM(inaname)) as nama_akun from coa_list where accountno = '" & Me.tb_id_akun.Text & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_akun.Text = reader.Item("nama_akun").ToString
            reader.Close()
            connection.koneksi.CloseKoneksi()
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearakun()
          
            Me.tb_tahun.Text = Year(Now.Date)
            Me.bindperiode_transaksi()
            Me.tb_id_akun.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun.Attributes.Add("style", "display: none;")

        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        'Dim thread = New System.Threading.Thread(New Threading.ThreadStart(AddressOf printReport))
        'thread.Start()

        Me.printReport()

    End Sub

    Protected Sub link_refresh_akun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun.Click
        Me.bindakun()
    End Sub

    Sub popup_akun()
        If Me.rd_akun.Checked = True Then
            Me.rd_semua.Checked = False
            Me.link_popup_akun.Enabled = True
            Me.link_popup_akun.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun.ClientID & "', '" & Me.link_refresh_akun.UniqueID & "')")
        Else
            Me.rd_semua.Checked = True
            Me.link_popup_akun.Enabled = False
            Me.link_popup_akun.Attributes.Clear()
        End If
    End Sub

    Protected Sub rd_akun_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rd_akun.CheckedChanged
        Me.popup_akun()
    End Sub

    Protected Sub rd_semua_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rd_semua.CheckedChanged
        Me.popup_akun()
    End Sub

    Protected Sub RadioButtonJ_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonJ.CheckedChanged
        Me.rd_semua.Visible = True
        Me.rd_semua.Checked = False
        Me.rd_akun.Checked = False
        Me.RadioButtonDefault.Visible = True
        Me.RadioButtonDefault.Checked = False
        Me.RadioButtonSummary.Visible = True
        Me.RadioButtonSummary.Checked = False
        Me.RadioButtonGrouping.Visible = True
        Me.RadioButtonGrouping.Checked = False
        Me.RadioButtonPDF.Checked = False
        Me.RadioButtonExcel.Checked = False
        'Me.RadioButtonExcel.Enabled = False
    End Sub

    Protected Sub RadioButtonGL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonGL.CheckedChanged
        Me.rd_semua.Visible = False
        Me.rd_semua.Checked = False
        Me.rd_akun.Checked = False
        Me.RadioButtonDefault.Visible = True
        Me.RadioButtonDefault.Checked = False
        Me.RadioButtonSummary.Visible = False
        Me.RadioButtonSummary.Checked = False
        Me.RadioButtonGrouping.Visible = False
        Me.RadioButtonGrouping.Checked = False
        Me.RadioButtonPDF.Checked = False
        Me.RadioButtonExcel.Checked = False
        'Me.RadioButtonExcel.Enabled = True
    End Sub

    Sub printReport()
        Try

            sub_sqlcom = Nothing
            sub_sqlcom = " (select distinct id_transaksi"
            sub_sqlcom = sub_sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
            sub_sqlcom = sub_sqlcom + " where id_transaction_period = " & Val(Me.dd_bulan.SelectedValue) & " and (coa_code = '" & Me.tb_id_akun.Text.Trim & "' or coa_code_lawan = '" & Me.tb_id_akun.Text.Trim & "')) "

            If Me.RadioButtonPDF.Checked = True And Me.RadioButtonExcel.Checked = False Then
                'Daniel 3/12/2017===============================================================================================================
                'Dim reportPath As String = Server.MapPath("reports\lap_jurnal.rpt")
                Dim reportPath As String = Nothing
                Dim file_pdf As String = Nothing
                If Me.RadioButtonJ.Checked = True Then
                    If Me.RadioButtonSummary.Checked = True Then
                        If Me.rd_semua.Checked = True Then
                            reportPath = Server.MapPath("reports\lap_jurnal_summary.rpt")
                            file_pdf = "Pdf_files/lap_jurnal_summary.pdf"
                        Else
                            reportPath = Server.MapPath("reports\lap_jurnal_per_akun_summary.rpt")
                            file_pdf = "Pdf_files/lap_jurnal_per_akun_summary.pdf"
                        End If
                    ElseIf Me.RadioButtonGrouping.Checked = True Then
                        If Me.rd_semua.Checked = True Then
                            reportPath = Server.MapPath("reports\lap_jurnal_grouping.rpt")
                            file_pdf = "Pdf_files/lap_jurnal_grouping.pdf"
                        Else
                            reportPath = Server.MapPath("reports\lap_jurnal_per_akun_grouping.rpt")
                            file_pdf = "Pdf_files/lap_jurnal_per_akun_grouping.pdf"
                        End If
                    ElseIf Me.RadioButtonDefault.Checked = True Then
                        If Me.rd_semua.Checked = True Then
                            reportPath = Server.MapPath("reports\lap_jurnal.rpt")
                            file_pdf = "Pdf_files/lap_jurnal.pdf"
                        Else
                            reportPath = Server.MapPath("reports\lap_jurnal_per_akun.rpt")
                            file_pdf = "Pdf_files/lap_jurnal_per_akun.pdf"
                        End If
                    End If
                ElseIf Me.RadioButtonGL.Checked = True Then
                    reportPath = Server.MapPath("reports\lap_general_ledger.rpt")
                    file_pdf = "Pdf_files/lap_general_ledger.pdf"
                End If

                '===============================================================================================================Daniel 1/12/2017
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
                oRD.SetParameterValue("coa", Me.tb_id_akun.Text)
                oRD.SetParameterValue("coa_name", Me.lbl_akun.Text)

                If Me.RadioButtonGL.Checked = True Then
                    sqlcom = "select isnull((select isnull(coa_list.saldo_awal,0) from coa_list where coa_list.accountno = '" & Me.tb_id_akun.Text.Trim & "') + (select sum(nilai_debet) from (select sum(isnull(nilai_debet,0)) as nilai_debet from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.id_transaksi in (select akun_general_ledger.id_transaksi from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue) & " and (coa_code = '" & Me.tb_id_akun.Text & "' or coa_code_lawan = '" & Me.tb_id_akun.Text & "'))) as GL) - (select sum(nilai_kredit) from (select sum(isnull(nilai_kredit,0)) as nilai_kredit from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.id_transaksi in (select akun_general_ledger.id_transaksi from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue) & " and (coa_code = '" & Me.tb_id_akun.Text & "' or coa_code_lawan = '" & Me.tb_id_akun.Text & "'))) as GL),0) as saldo from coa_list where coa_list.accountno = '" & Me.tb_id_akun.Text & "'"
                    reader = connection.koneksi.SelectRecord(sqlcom)
                    reader.Read()
                    If reader.HasRows Then
                        Me.saldo_akhir = reader.Item("saldo").ToString
                        oRD.SetParameterValue("saldo_akhir", Me.saldo_akhir)
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                    End If
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                End If

                'Daniel 04/12/2017==========================================================================================
                If Me.RadioButtonSummary.Checked = True Or Me.RadioButtonGrouping.Checked = True Or Me.RadioButtonGL.Checked = True Then
                    oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
                Else
                    oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
                End If
                '==========================================================================================Daniel 4/12/2017
                oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
                oExO = oRD.ExportOptions
                oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                'Daniel 24/08/2017===========================================================================================
                oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(file_pdf))
                '===========================================================================================Daniel 24/08/2017
                Dim vscript As String = ""
                vscript = "<script>" & vbCrLf
                'Daniel 24/08/2017===========================================================================================
                vscript = vscript + "window.open('" & file_pdf & "', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
                '===========================================================================================Daniel 24/08/2017
                vscript = vscript + "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
                connection.koneksi.CloseKoneksi()

            ElseIf Me.RadioButtonPDF.Checked = False And Me.RadioButtonExcel.Checked = True Then
                Dim DataTable As DataTable

                If Me.RadioButtonJ.Checked = True Then
                    If Me.RadioButtonSummary.Checked = True Then

                        If Me.rd_semua.Checked = True Then

                            DataTable = Nothing
                            sqlcom = Nothing

                            sqlcom = "select coa_code, sum(isnull(nilai_debet,0)) as nilai_debet, sum(isnull(nilai_kredit,0)) as nilai_kredit, coa_list.inaname as nama_akun, akun_general_ledger.no_voucher "
                            sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code "
                            sqlcom = sqlcom + " where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                            sqlcom = sqlcom + " group by coa_code, coa_list.inaname, akun_general_ledger.no_voucher"

                            DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                            tradingClass.WriteExcel("~/Excel_files/lap_jurnal_summary.xls", DataTable, 1)
                            tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_jurnal_summary.xls", "800", "600", "no", "yes")

                        Else

                            DataTable = Nothing
                            sqlcom = Nothing

                            sqlcom = "select coa_code, sum(isnull(nilai_debet,0)) as nilai_debet, sum(isnull(nilai_kredit,0)) as nilai_kredit, coa_list.inaname as nama_akun, akun_general_ledger.no_voucher "
                            sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code "
                            sqlcom = sqlcom + " where akun_general_ledger.id_transaksi in (select akun_general_ledger.id_transaksi from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue) & " and (coa_code = '" & Me.tb_id_akun.Text & "' or coa_code_lawan = '" & Me.tb_id_akun.Text & "')) "
                            sqlcom = sqlcom + " group by coa_code, coa_list.inaname, akun_general_ledger.no_voucher"

                            DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                            tradingClass.WriteExcel("~/Excel_files/lap_jurnal_per_akun_summary.xls", DataTable, 1)
                            tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_jurnal_per_akun_summary.xls", "800", "600", "no", "yes")

                        End If


                    ElseIf Me.RadioButtonGrouping.Checked = True Then

                        If Me.rd_semua.Checked = True Then

                            DataTable = Nothing
                            sqlcom = Nothing

                            sqlcom = "select agl.parentacc,cl.inaname,agl.nilai_debet,agl.nilai_kredit from coa_list cl inner join ( "
                            sqlcom = sqlcom + " select coa_list.parentacc, sum(isnull(nilai_debet,0)) as nilai_debet, sum(isnull(nilai_kredit,0)) as nilai_kredit "
                            sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code "
                            sqlcom = sqlcom + " where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                            sqlcom = sqlcom + " group by coa_list.parentacc "
                            sqlcom = sqlcom + " ) as agl on cl.accountno = agl.parentacc"

                            DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                            tradingClass.WriteExcel("~/Excel_files/lap_jurnal_grouping.xls", DataTable, 1)
                            tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_jurnal_grouping.xls", "800", "600", "no", "yes")

                        Else

                            DataTable = Nothing
                            sqlcom = Nothing

                            sqlcom = "select agl.parentacc,cl.inaname,agl.nilai_debet,agl.nilai_kredit from coa_list cl inner join ( "
                            sqlcom = sqlcom + " select coa_list.parentacc, sum(isnull(nilai_debet,0)) as nilai_debet, sum(isnull(nilai_kredit,0)) as nilai_kredit "
                            sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code "
                            sqlcom = sqlcom + " where akun_general_ledger.id_transaksi in (select akun_general_ledger.id_transaksi from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue) & " and (coa_code = '" & Me.tb_id_akun.Text & "' or coa_code_lawan = '" & Me.tb_id_akun.Text & "')) "
                            sqlcom = sqlcom + " group by coa_list.parentacc "
                            sqlcom = sqlcom + " ) as agl on cl.accountno = agl.parentacc"

                            DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                            tradingClass.WriteExcel("~/Excel_files/lap_jurnal_per_akun_grouping.xls", DataTable, 1)
                            tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_jurnal_per_akun_grouping.xls", "800", "600", "no", "yes")

                        End If


                    ElseIf Me.RadioButtonDefault.Checked = True Then
                        If Me.rd_semua.Checked = True Then
                            DataTable = Nothing
                            sqlcom = Nothing

                            sqlcom = "select akun_general_ledger.seq, akun_general_ledger.id_transaksi, akun_general_ledger.tgl_transaksi, akun_general_ledger.coa_code,akun_general_ledger.coa_code_lawan, akun_general_ledger.nilai_debet, akun_general_ledger.nilai_kredit, akun_general_ledger.keterangan, akun_general_ledger.id_transaction_period, akun_general_ledger.id_currency, akun_general_ledger.kurs, coa_asal.InaName AS nama_akun_asal, akun_general_ledger.no_voucher "
                            sqlcom = sqlcom + " from akun_general_ledger INNER JOIN COA_list AS coa_asal ON coa_asal.AccountNo = akun_general_ledger.coa_code"
                            sqlcom = sqlcom + " where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                            sqlcom = sqlcom + " order by 3,2,1"

                            DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                            tradingClass.WriteExcel("~/Excel_files/lap_jurnal.xls", DataTable, 1)
                            tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_jurnal.xls", "800", "600", "no", "yes")

                        Else

                            DataTable = Nothing
                            sqlcom = Nothing

                            sqlcom = "select akun_general_ledger.seq, akun_general_ledger.id_transaksi, akun_general_ledger.tgl_transaksi, akun_general_ledger.coa_code,akun_general_ledger.coa_code_lawan, akun_general_ledger.nilai_debet, akun_general_ledger.nilai_kredit, akun_general_ledger.keterangan, akun_general_ledger.id_transaction_period, akun_general_ledger.id_currency, akun_general_ledger.kurs, coa_asal.InaName AS nama_akun_asal, akun_general_ledger.no_voucher "
                            sqlcom = sqlcom + " from akun_general_ledger INNER JOIN COA_list AS coa_asal ON coa_asal.AccountNo = akun_general_ledger.coa_code"
                            sqlcom = sqlcom + " where akun_general_ledger.id_transaksi in"
                            sqlcom = sqlcom + " (select akun_general_ledger.id_transaksi"
                            sqlcom = sqlcom + " from akun_general_ledger INNER JOIN COA_list AS coa_asal ON coa_asal.AccountNo = akun_general_ledger.coa_code"
                            sqlcom = sqlcom + " where akun_general_ledger.id_transaction_period = " & Val(Me.dd_bulan.SelectedValue)
                            sqlcom = sqlcom + " and (akun_general_ledger.coa_code = '" & Me.tb_id_akun.Text & "' or akun_general_ledger.coa_code_lawan = '" & Me.tb_id_akun.Text & "'))"
                            sqlcom = sqlcom + " order by 3,2,1"

                            DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                            tradingClass.WriteExcel("~/Excel_files/lap_jurnal_per_akun.xls", DataTable, 1)
                            tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_jurnal_per_akun.xls", "800", "600", "no", "yes")

                        End If

                    End If
                ElseIf Me.RadioButtonGL.Checked = True Then
                    DataTable = Nothing
                    sqlcom = Nothing

                    'sqlcom = "select null as 'COA', null as 'No. Voucher', 'SALDO AWAL' as 'Nama COA',cast(isnull(case when saldo_awal > 0 then saldo_awal end,0) as varchar) as 'Debet', cast(isnull(case when saldo_awal < 0 then saldo_awal end,0) as varchar) as 'Kredit'"
                    'sqlcom = sqlcom + " from coa_list"
                    'sqlcom = sqlcom + " where accountno = '" & Me.tb_id_akun.Text.Trim & "'"
                    'sqlcom = sqlcom + " union all"
                    'sqlcom = sqlcom + " select null, null, null, null, null"
                    'sqlcom = sqlcom + " union all"
                    'sqlcom = sqlcom + " select coa_code as 'COA', no_voucher as 'No. Voucher', inaname as 'Nama COA', cast(isnull(nilai_kredit,0) as varchar) as 'Debet', cast(isnull(nilai_debet,0) as varchar) as 'Kredit'"
                    'sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
                    'sqlcom = sqlcom + " where coa_code != '" & Me.tb_id_akun.Text.Trim & "' and coa_code != '82.01' and id_transaksi in"
                    'sqlcom = sqlcom + sub_sqlcom
                    'sqlcom = sqlcom + " union all"
                    'sqlcom = sqlcom + " select null, null, null, null, null"
                    'sqlcom = sqlcom + " union all"
                    'sqlcom = sqlcom + " select null, null, 'TOTAL', cast(sum(isnull(nilai_kredit,0)) as varchar), cast(sum(isnull(nilai_debet,0)) as varchar)"
                    'sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
                    'sqlcom = sqlcom + " where coa_code != '" & Me.tb_id_akun.Text.Trim & "' and coa_code != '82.01' and id_transaksi in"
                    'sqlcom = sqlcom + sub_sqlcom
                    'sqlcom = sqlcom + " union all"
                    'sqlcom = sqlcom + " select null, null, null, null, null"
                    'sqlcom = sqlcom + " union all"
                    'sqlcom = sqlcom + " select null, null, 'SALDO AKHIR', cast(isnull(case when saldo_awal > 0 then saldo_awal + sum(isnull(nilai_debet,0)) - sum(isnull(nilai_kredit,0)) end,0) as varchar), cast(isnull(case when saldo_awal < 0 then sum(isnull(nilai_debet,0)) - sum(isnull(nilai_kredit,0)) - saldo_awal end,0) as varchar)"
                    'sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
                    'sqlcom = sqlcom + " where accountno = '" & Me.tb_id_akun.Text.Trim & "' and coa_code != '82.01' and id_transaksi in"
                    'sqlcom = sqlcom + sub_sqlcom
                    'sqlcom = sqlcom + " group by saldo_awal"

                    sqlcom = "select null as coa_code, null as no_voucher, 'SALDO AWAL' as nama_akun,isnull(case when coa_list.saldo_awal > 0 then coa_list.saldo_awal end,0) as nilai_debet,isnull(case when coa_list.saldo_awal < 0 then coa_list.saldo_awal end,0) as nilai_kredit"
                    sqlcom = sqlcom + " from coa_list "
                    sqlcom = sqlcom + " where coa_list.accountno = '" & Me.tb_id_akun.Text & "' "
                    sqlcom = sqlcom + " union all "
                    sqlcom = sqlcom + " select null, null, null, null, null "
                    sqlcom = sqlcom + " union all "
                    sqlcom = sqlcom + " select coa_code as 'COA', no_voucher as 'No. Voucher', coa_list.inaname as 'Nama COA', isnull(nilai_kredit,0) as 'Debet', isnull(nilai_debet,0) as 'Kredit' "
                    sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code "
                    sqlcom = sqlcom + " where coa_code != '" & Me.tb_id_akun.Text & "' and coa_code != '82.01' and id_transaksi in "
                    sqlcom = sqlcom + " (select distinct id_transaksi "
                    sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code "
                    sqlcom = sqlcom + " where akun_general_ledger.id_transaction_period = '" & Val(Me.dd_bulan.SelectedValue) & "'  and (coa_code =  '" & Me.tb_id_akun.Text & "' or coa_code_lawan =  '" & Me.tb_id_akun.Text & "')) "

                    DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                    tradingClass.WriteExcel("~/Excel_files/lap_general_ledger.xls", DataTable, 1)
                    tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_general_ledger.xls", "800", "600", "no", "yes")

                End If

            End If

        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_refresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        Me.bindperiode_transaksi()
    End Sub
End Class


