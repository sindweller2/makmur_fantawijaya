Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Akuntansi_general_ledger_bagian
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    'Sub bindakun()
    '    sqlcom = "select AccountNo, InaName from coa_list"
    '    sqlcom = sqlcom + " where IsControl = 'N'"
    '    sqlcom = sqlcom + " order by InaName"
    '    reader = connection.koneksi.SelectRecord(sqlcom)
    '    Me.dd_akun.DataSource = reader
    '    Me.dd_akun.DataTextField = "InaName"
    '    Me.dd_akun.DataValueField = "AccountNo"
    '    Me.dd_akun.DataBind()
    '    Me.dd_akun.Items.Add(New ListItem("---Semua akun---", "0"))
    '    reader.Close()
    '    connection.koneksi.CloseKoneksi()
    'End Sub

    sub bindtransaksi()
        try
           sqlcom = "select code_transaksi, nama_transaksi"
           sqlcom = sqlcom + " from gl_transaksi"
           sqlcom = sqlcom + " where bagian = '" & me.dd_bagian.selectedValue & "'"
           sqlcom = sqlcom + " group by code_transaksi, nama_transaksi"
           sqlcom = sqlcom + " order by nama_transaksi"
           reader = connection.koneksi.SelectRecord(sqlcom)
           Me.dd_transaksi.DataSource = reader
           Me.dd_transaksi.DataTextField = "nama_transaksi"
           Me.dd_transaksi.DataValueField = "code_transaksi"
           Me.dd_transaksi.DataBind()
           reader.Close()
           connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    end sub

    Sub bindtotal()
        Try
            Dim vtgl_awal as string = Me.tb_tanggal.Text.Substring(3,2) & "/" & Me.tb_tanggal.Text.Substring(0,2) & "/" & Me.tb_tanggal.Text.Substring(6,4)
            Dim vtgl_akhir as string = Me.tb_tanggal_akhir.Text.Substring(3,2) & "/" & Me.tb_tanggal_akhir.Text.Substring(0,2) & "/" & Me.tb_tanggal_akhir.Text.Substring(6,4)

            sqlcom = "select isnull(sum(isnull(nilai_debet,0)),0) as nilai_debet, isnull(sum(isnull(nilai_kredit,0)),0) as nilai_kredit,"
            sqlcom = sqlcom + " isnull(sum(isnull(nilai_debet,0)) - sum(isnull(nilai_kredit,0)),0) as saldo"
            sqlcom = sqlcom + " from akun_general_ledger"
            sqlcom = sqlcom + " where (tgl_transaksi >= '" & vtgl_awal & "' and tgl_transaksi <= '" & vtgl_akhir & "')"  

            sqlcom = sqlcom + " and kode_transaksi = '" & me.dd_transaksi.selectedValue & "'"

            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_total_debet.Text = FormatNumber(reader.Item("nilai_debet").ToString, 2)
                Me.lbl_total_kredit.Text = FormatNumber(reader.Item("nilai_kredit").ToString, 2)
                Me.lbl_saldo.Text = FormatNumber(reader.Item("saldo").ToString, 2)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub checkdata()
        Try

            Dim vtgl_awal as string = Me.tb_tanggal.Text.Substring(3,2) & "/" & Me.tb_tanggal.Text.Substring(0,2) & "/" & Me.tb_tanggal.Text.Substring(6,4)
            Dim vtgl_akhir as string = Me.tb_tanggal_akhir.Text.Substring(3,2) & "/" & Me.tb_tanggal_akhir.Text.Substring(0,2) & "/" & Me.tb_tanggal_akhir.Text.Substring(6,4)

            sqlcom = "select seq, id_transaksi, convert(char, tgl_transaksi, 103) as tgl_transaksi,"
            sqlcom = sqlcom + " kode_transaksi, coa_code, coa_code_lawan, isnull(nilai_debet,0) as nilai_debet, isnull(nilai_kredit,0) as nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_currency, coa_list.inaname as nama_akun, coa_lawan.inaname as nama_akun_lawan, isnull(kurs,0) as kurs"
            sqlcom = sqlcom + " from akun_general_ledger"
            sqlcom = sqlcom + " inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
            sqlcom = sqlcom + " inner join coa_list coa_lawan on coa_lawan.accountno = akun_general_ledger.coa_code_lawan"
            sqlcom = sqlcom + " where (tgl_transaksi >= '" & vtgl_awal & "' and tgl_transaksi <= '" & vtgl_akhir & "')"           
            sqlcom = sqlcom + " and kode_transaksi = '" & me.dd_transaksi.selectedValue & "'"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.tbl_search.Visible = True
            Else
                Me.tbl_search.Visible = False
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            Dim vtgl_awal as string = Me.tb_tanggal.Text.Substring(3,2) & "/" & Me.tb_tanggal.Text.Substring(0,2) & "/" & Me.tb_tanggal.Text.Substring(6,4)
            Dim vtgl_akhir as string = Me.tb_tanggal_akhir.Text.Substring(3,2) & "/" & Me.tb_tanggal_akhir.Text.Substring(0,2) & "/" & Me.tb_tanggal_akhir.Text.Substring(6,4)

            sqlcom = "select seq, id_transaksi, convert(char, tgl_transaksi, 103) as tgl_transaksi,"
            sqlcom = sqlcom + " kode_transaksi, coa_code, coa_code_lawan, isnull(nilai_debet,0) as nilai_debet, isnull(nilai_kredit,0) as nilai_kredit,"
            sqlcom = sqlcom + " keterangan, id_currency, coa_list.inaname as nama_akun, coa_lawan.inaname as nama_akun_lawan, isnull(kurs,0) as kurs"
            sqlcom = sqlcom + " from akun_general_ledger"
            sqlcom = sqlcom + " inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code"
            sqlcom = sqlcom + " inner join coa_list coa_lawan on coa_lawan.accountno = akun_general_ledger.coa_code_lawan"
            sqlcom = sqlcom + " where (tgl_transaksi >= '" & vtgl_awal & "' and tgl_transaksi <= '" & vtgl_akhir & "')"
            
            sqlcom = sqlcom + " and kode_transaksi = '" & me.dd_transaksi.selectedValue & "'"

            if not string.isnullorempty(me.tb_nilai.text) then
               sqlcom = sqlcom + " and (nilai_debet = " & me.tb_nilai.text
               sqlcom = sqlcom + " or nilai_kredit = " & me.tb_nilai.text & ")"
            end if

            sqlcom = sqlcom + " order by tgl_transaksi, seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "akun_general_ledger")
                Me.dg_data.DataSource = ds.Tables("akun_general_ledger").DefaultView

                If ds.Tables("akun_general_ledger").Rows.Count > 0 Then
                    'If ds.Tables("akun_general_ledger").Rows.Count > 7 Then
                        'Me.dg_data.AllowPaging = True
                        'Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        'Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        'Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        'Me.dg_data.PageSize = 7
                    'Else
                        'Me.dg_data.AllowPaging = False
                    'End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_print.Enabled = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_print.Enabled = False
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

    Sub updatekode_transaksi_gl()
        Try
            'transaksi petty cash Kas IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRANPETTYK'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRANPETTY'"
            sqlcom = sqlcom + " and coa_code = '11.01.01'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRANPETTYK'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRANPETTY'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.01.01'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi petty cash Kas Gudang
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRANPETTYG'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRANPETTY'"
            sqlcom = sqlcom + " and coa_code = '11.01.03'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRANPETTYG'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRANPETTY'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.01.03'"
            connection.koneksi.updaterecord(sqlcom)


            'transaksi penerimaan uang BP IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGBPIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.01'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGBPIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.01'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi penerimaan uang BCA IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGBCAIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.02'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGBCAIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.02'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi penerimaan uang BCA USD
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGBCAUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.03'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGBCAUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.03'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi penerimaan uang Panin IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGPANINIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.04'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGPANINIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.04'"
            connection.koneksi.updaterecord(sqlcom)


            'transaksi penerimaan uang CT IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGCTIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.05'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGCTIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.05'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi penerimaan uang CT USD
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGCTUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.06'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGCTUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.06'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi penerimaan uang UOB IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGUOBIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.07'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGUOBIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.07'"
            connection.koneksi.updaterecord(sqlcom)


            'transaksi penerimaan uang Kas kantor IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGKTRIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code = '11.01.01'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRMUANGKTRIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRMUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.01.01'"
            connection.koneksi.updaterecord(sqlcom)


            'transaksi pengeluaran uang BP IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGBPIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.01'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGBPIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.01'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi pengeluaran uang BCA IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGBCAIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.02'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGBCAIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.02'"
            connection.koneksi.updaterecord(sqlcom)


            'transaksi pengeluaran uang BCA USD
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGBCAUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.03'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGBCAUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.03'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi pengeluaran uang PANIN USD
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGPANINIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.04'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGPANINIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.04'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi pengeluaran uang CT IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGCTIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.05'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGCTIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.05'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi pengeluaran uang CT USD
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGCTUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.06'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGCTUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.06'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi pengeluaran uang UOB IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGUOBIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code = '11.02.07'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'KLRUANGUOBIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'KLRUANG'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.07'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi transfer antar kas BP IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASBPIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code = '11.02.01'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASBPIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.01'"
            connection.koneksi.updaterecord(sqlcom)


            'transaksi transfer antar kas BCA IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASBCAIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code = '11.02.02'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASBCAIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.02'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi transfer antar kas BCA USD
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASBCAUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code = '11.02.03'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASBCAUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.03'"
            connection.koneksi.updaterecord(sqlcom)


            'transaksi transfer antar kas PANIN USD
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASPANINIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code = '11.02.04'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASPANINIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.04'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi transfer antar kas CT IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASCTIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code = '11.02.05'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASCTIDR'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.05'"
            connection.koneksi.updaterecord(sqlcom)

            'transaksi transfer antar kas CT USD
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASCTUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code = '11.02.06'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASCTUSD'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.02.06'"
            connection.koneksi.updaterecord(sqlcom)


            'transaksi transfer antar kas KAS KANTOR IDR
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASK'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code = '11.01.01'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASK'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.01.01'"
            connection.koneksi.updaterecord(sqlcom)


            'transaksi transfer antar kas KAS KANTOR GUDANG
            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASG'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code = '11.01.03'"
            connection.koneksi.updaterecord(sqlcom)

            sqlcom = "update akun_general_ledger"
            sqlcom = sqlcom + " set kode_transaksi = 'TRNFKASG'"
            sqlcom = sqlcom + " where kode_transaksi = 'TRNFKAS'"
            sqlcom = sqlcom + " and coa_code_lawan = '11.01.03'"
            connection.koneksi.updaterecord(sqlcom)

        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.updatekode_transaksi_gl()
            Me.tb_tanggal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.tb_tanggal_akhir.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.bindtransaksi()
            Me.checkdata()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.updatekode_transaksi_gl()
        Me.loadgrid()
    End Sub

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try

            Me.updatekode_transaksi_gl()

            Dim reportPath As String = Nothing

            reportPath = Server.MapPath("reports\general_ledger_per_bagian.rpt")

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

            dim vnama_bagian as string = nothing

            if me.dd_bagian.selectedvalue = "S" then
               vnama_bagian = "Sales Admin"
            elseif me.dd_bagian.selectedvalue = "C" then
               vnama_bagian = "Collection"
            elseif me.dd_bagian.selectedvalue = "I" then
               vnama_bagian = "Import"
            elseif me.dd_bagian.selectedvalue = "F" then
               vnama_bagian = "Finance"
            end if

            Dim vtgl_awal as string = Me.tb_tanggal.Text.Substring(3,2) & "/" & Me.tb_tanggal.Text.Substring(0,2) & "/" & Me.tb_tanggal.Text.Substring(6,4)
            Dim vtgl_akhir as string = Me.tb_tanggal_akhir.Text.Substring(3,2) & "/" & Me.tb_tanggal_akhir.Text.Substring(0,2) & "/" & Me.tb_tanggal_akhir.Text.Substring(6,4)

            oRD.SetParameterValue("vtgl", vtgl_awal)
            oRD.SetParameterValue("vtgl_akhir", vtgl_akhir)
            oRD.SetParameterValue("vnama_bagian", vnama_bagian)
            oRD.SetParameterValue("vid_transaksi", Me.dd_transaksi.selectedValue)

            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/general_ledger_per_bagian.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/general_ledger_per_bagian.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub


    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update akun_general_ledger"
                    sqlcom = sqlcom + " set keterangan = '" & CType(Me.dg_data.Items(x).FindControl("tb_keterangan"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dd_bagian_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bagian.SelectedIndexChanged
        Me.updatekode_transaksi_gl()
        Me.bindtransaksi()
        Me.loadgrid()
    End Sub


    Protected Sub dd_transaksi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_transaksi.SelectedIndexChanged
        Me.updatekode_transaksi_gl()
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
       me.loadgrid()
    End Sub

End Class
