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
            Me.tb_tgl_awal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.tb_tgl_akhir.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.tb_id_akun.Attributes.Add("style", "display: none;")
            Me.link_refresh_akun.Attributes.Add("style", "display: none;")
            Me.popup_akun()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try
            Dim vtgl_awal As String = tradingClass.DateValidated(Me.tb_tgl_awal.Text.Trim)
            Dim vtgl_akhir As String = tradingClass.DateValidated(Me.tb_tgl_akhir.Text.Trim)

            sub_sqlcom = Nothing
            sub_sqlcom = " (select distinct id_transaksi"
            sub_sqlcom = sub_sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
            sub_sqlcom = sub_sqlcom + " where tgl_transaksi >= '" & vtgl_awal & "' and tgl_transaksi <=  '" & vtgl_akhir & "' and (coa_code = '" & Me.tb_id_akun.Text.Trim & "' or coa_code_lawan = '" & Me.tb_id_akun.Text.Trim & "')) "

            If Me.RadioButtonPDF.Checked = True And Me.RadioButtonExcel.Checked = False Then
                Dim reportPath As String = Nothing
                Dim file_pdf As String = Nothing

                reportPath = Server.MapPath("reports\lap_buku_bank.rpt")
                file_pdf = "Pdf_files/lap_buku_bank.pdf"

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

                'Dim vtgl_awal As String = Me.tb_tgl_awal.Text.Substring(3, 2) & "/" & Me.tb_tgl_awal.Text.Substring(0, 2) & "/" & Me.tb_tgl_awal.Text.Substring(6, 4)
                'Dim vtgl_akhir As String = Me.tb_tgl_akhir.Text.Substring(3, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(0, 2) & "/" & Me.tb_tgl_akhir.Text.Substring(6, 4)

                oRD.SetParameterValue("tgl_awal", vtgl_awal)
                oRD.SetParameterValue("tgl_akhir", vtgl_akhir)
                oRD.SetParameterValue("coa", Me.tb_id_akun.Text)
                oRD.SetParameterValue("coa_name", Me.lbl_akun.Text)

                sqlcom = "select isnull((select isnull(coa_list.saldo_awal,0) from coa_list where coa_list.accountno = '" & Me.tb_id_akun.Text.Trim & "') + (select sum(nilai_debet) from (select sum(isnull(nilai_debet,0)) as nilai_debet from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.id_transaksi in (select akun_general_ledger.id_transaksi from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.tgl_transaksi >= '" & vtgl_awal & "' and akun_general_ledger.tgl_transaksi <= '" & vtgl_akhir & "' and (coa_code = '" & Me.tb_id_akun.Text & "' or coa_code_lawan = '" & Me.tb_id_akun.Text & "'))) as GL) - (select sum(nilai_kredit) from (select sum(isnull(nilai_kredit,0)) as nilai_kredit from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.id_transaksi in (select akun_general_ledger.id_transaksi from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code where akun_general_ledger.tgl_transaksi >= '" & vtgl_awal & "' and akun_general_ledger.tgl_transaksi <= '" & vtgl_akhir & "' and (coa_code = '" & Me.tb_id_akun.Text & "' or coa_code_lawan = '" & Me.tb_id_akun.Text & "'))) as GL),0) as saldo from coa_list where coa_list.accountno = '" & Me.tb_id_akun.Text & "'"
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
                oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
                oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
                oExO = oRD.ExportOptions
                oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(file_pdf))
                Dim vscript As String = ""
                vscript = "<script>" & vbCrLf
                vscript = vscript + "window.open('" & file_pdf & "', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
                vscript = vscript + "</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
                connection.koneksi.CloseKoneksi()

            ElseIf Me.RadioButtonPDF.Checked = False And Me.RadioButtonExcel.Checked = True Then

                Dim DataTable As DataTable
                sqlcom = Nothing
                sqlcom = "select null as 'COA', null as 'No. Voucher', 'SALDO AWAL' as 'Nama COA',cast(isnull(case when saldo_awal < 0 then saldo_awal end,0) as varchar) as 'Debet', cast(isnull(case when saldo_awal > 0 then saldo_awal end,0) as varchar) as 'Kredit'"
                sqlcom = sqlcom + " from coa_list"
                sqlcom = sqlcom + " where accountno = '" & Me.tb_id_akun.Text.Trim & "'"
                sqlcom = sqlcom + " union all"
                sqlcom = sqlcom + " select null, null, null, null, null"
                sqlcom = sqlcom + " union all"
                sqlcom = sqlcom + " select coa_code as 'COA', no_voucher as 'No. Voucher', inaname as 'Nama COA', cast(isnull(nilai_debet,0) as varchar) as 'Debet', cast(isnull(nilai_kredit,0) as varchar) as 'Kredit'"
                sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
                sqlcom = sqlcom + " where coa_code != '" & Me.tb_id_akun.Text.Trim & "' and coa_code != '82.01' and id_transaksi in"
                sqlcom = sqlcom + sub_sqlcom
                sqlcom = sqlcom + " union all"
                sqlcom = sqlcom + " select null, null, null, null, null"
                sqlcom = sqlcom + " union all"
                sqlcom = sqlcom + " select null, null, 'TOTAL', cast(sum(isnull(nilai_debet,0)) as varchar), cast(sum(isnull(nilai_kredit,0)) as varchar)"
                sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
                sqlcom = sqlcom + " where coa_code != '" & Me.tb_id_akun.Text.Trim & "' and coa_code != '82.01' and id_transaksi in"
                sqlcom = sqlcom + sub_sqlcom
                sqlcom = sqlcom + " union all"
                sqlcom = sqlcom + " select null, null, null, null, null"
                sqlcom = sqlcom + " union all"
                sqlcom = sqlcom + " select null, null, 'SALDO AKHIR', cast(isnull(case when saldo_awal < 0 then saldo_awal + sum(isnull(nilai_kredit,0)) - sum(isnull(nilai_debet,0)) end,0) as varchar), cast(isnull(case when saldo_awal > 0 then saldo_awal - sum(isnull(nilai_kredit,0)) + sum(isnull(nilai_debet,0)) end,0) as varchar)"
                sqlcom = sqlcom + " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
                sqlcom = sqlcom + " where accountno = '" & Me.tb_id_akun.Text.Trim & "' and coa_code != '82.01' and id_transaksi in"
                sqlcom = sqlcom + sub_sqlcom
                sqlcom = sqlcom + " group by saldo_awal"

                DataTable = Me.tradingClass.DataTableQuery(sqlcom)

                tradingClass.WriteExcel("~/Excel_files/lap_buku_bank.xls", DataTable, 1)
                tradingClass.OpenBrowser(Me.Page, "Excel_files/lap_buku_bank.xls", "800", "600", "no", "yes")

            End If

        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub link_refresh_akun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_akun.Click
        Me.bindakun()
    End Sub

    Sub popup_akun()
        Me.link_popup_akun.Enabled = True
        Me.link_popup_akun.Attributes.Add("onclick", "popup_coa('" & Me.tb_id_akun.ClientID & "', '" & Me.link_refresh_akun.UniqueID & "')")
    End Sub
End Class
