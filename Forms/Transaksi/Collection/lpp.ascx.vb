Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Collection_lpp
    Inherits System.Web.UI.UserControl

    Private ReadOnly Property vtgl() As Date
        Get
            Dim o As Object = Request.QueryString("vtgl")
            If String.IsNullOrEmpty(o) = False Then Return CDate(o) Else Return Nothing
        End Get
    End Property

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

    Public Property vid_transaksi() As Integer
        Get
            Dim o As Object = ViewState("vid_transaksi")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_transaksi") = value
        End Set
    End Property

    Public Property vtanggal() As String
        Get
            Dim o As Object = ViewState("vtanggal")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vtanggal") = value
        End Set
    End Property

    Public Property vid_customer() As Integer
        Get
            Dim o As Object = ViewState("vid_customer")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_customer") = value
        End Set
    End Property

    Public Property vmax() As Integer
        Get
            Dim o As Object = ViewState("vmax")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vmax") = value
        End Set
    End Property

    Public Property vseq() As Integer
        Get
            Dim o As Object = ViewState("vseq")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vseq") = value
        End Set
    End Property

    Public Property vnilai() As Integer
        Get
            Dim o As Object = ViewState("vnilai")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vnilai") = value
        End Set
    End Property

    Public Property vkurs() As Integer
        Get
            Dim o As Object = ViewState("vkurs")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vkurs") = value
        End Set
    End Property

    Public Property vno_invoice() As String
        Get
            Dim o As Object = ViewState("vno_invoice")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
        Set(ByVal value As String)
            ViewState("vno_invoice") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiodetransaksi()
        sqlcom = "select id, name from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.vid_transaksi = reader.Item("id").ToString
            Me.lbl_periode.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearform()        
        Me.tb_nama_bank.Text = ""
        Me.tb_no_giro.Text = ""
        Me.tb_tgl_cek_giro.Text = ""
        Me.tb_jumlah.Text = ""
        Me.tb_keterangan.Text = ""
    End Sub

    Sub bindjenis_pembayaran()
        sqlcom = "select id, name from jenis_pembayaran order by id"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_jenis_pembayaran.DataSource = reader
        Me.dd_jenis_pembayaran.DataTextField = "name"
        Me.dd_jenis_pembayaran.DataValueField = "id"
        Me.dd_jenis_pembayaran.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindmatauang()
        sqlcom = "select id from currency order by id"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_mata_uang.DataSource = reader
        Me.dd_mata_uang.DataTextField = "id"
        Me.dd_mata_uang.DataValueField = "id"
        Me.dd_mata_uang.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub clearinvoice()
        Me.tb_id_no_invoice.Text = 0
        Me.lbl_no_invoice.Text = "-----"
        Me.lbl_nama_customer.Text = ""
        Me.lbl_nilai_invoice.Text = ""
        Me.link_popup_no_invoice.Visible = True
    End Sub

    Sub bindinvoice()
        sqlcom = "select so_no_text, convert(char, tanggal, 103) as tanggal, daftar_customer.name as nama_customer, id_currency, sales_order.id_customer, sales_order.rate,"
        sqlcom = sqlcom + " sum(sales_order_detail.qty * (sales_order_detail.harga_jual - (sales_order_detail.harga_jual * sales_order_detail.discount /100))) + "
        sqlcom = sqlcom + " sum(sales_order_detail.qty * (sales_order_detail.harga_jual - (sales_order_detail.harga_jual * sales_order_detail.discount /100)) * sales_order.ppn / 100)"
        sqlcom = sqlcom + " as jumlah_nilai"
        sqlcom = sqlcom + " from sales_order"
        sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no"
        sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
        sqlcom = sqlcom + " where no = " & Me.tb_id_no_invoice.Text
        sqlcom = sqlcom + " group by sales_order.so_no_text, sales_order.tanggal, daftar_customer.name, sales_order.id_currency, sales_order.id_customer, sales_order.rate"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_no_invoice.Text = reader.Item("so_no_text").ToString
            Me.lbl_tgl_penjualan.Text = reader.Item("tanggal").ToString
            Me.lbl_nilai_invoice.Text = reader.Item("id_currency").ToString + " " + FormatNumber(reader.Item("jumlah_nilai").ToString, 2)
            Me.lbl_nama_customer.Text = reader.Item("nama_customer").ToString
            Me.dd_mata_uang.SelectedValue = reader.Item("id_currency").ToString
            Me.tb_jumlah.Text = FormatNumber(reader.Item("jumlah_nilai").ToString, 2)
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try
            Me.clearform()
            Me.clearinvoice()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select lpp.seq, convert(char, lpp.tanggal, 103) as tanggal, lpp.id_jenis_pembayaran,"
            sqlcom = sqlcom + " lpp.id_mata_uang, lpp.nama_bank, lpp.no_cek_giro, convert(char, lpp.tgl_cek_giro, 103) as tgl_cek_giro,"
            sqlcom = sqlcom + " isnull(lpp.jumlah_nilai,0) as jumlah_nilai, lpp.keterangan, is_submit,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when lpp.no_so is null then 'N/N'"
            sqlcom = sqlcom + " when lpp.no_so is not null then (select so_no_text from sales_order where no = lpp.no_so)"
            sqlcom = sqlcom + " end as no_invoice,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when lpp.no_so is null then 'N/N'"
            sqlcom = sqlcom + " when lpp.no_so is not null then "
            sqlcom = sqlcom + " (select name from daftar_customer where id = (select id_customer from sales_order where no = lpp.no_so))"
            sqlcom = sqlcom + " end as nama_customer"
            sqlcom = sqlcom + " from lpp"
            sqlcom = sqlcom + " where convert(char, lpp.tanggal, 103) = '" & Me.tb_tanggal.Text & "'"
            sqlcom = sqlcom + " order by seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "lpp")
                Me.dg_data.DataSource = ds.Tables("lpp").DefaultView

                If ds.Tables("lpp").Rows.Count > 0 Then
                    If ds.Tables("lpp").Rows.Count > 8 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 8
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                    Me.btn_update.Visible = True
                    Me.btn_delete.Visible = True
                    Me.btn_submit.Enabled = True
                    Me.btn_print.Enabled = True                    

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name from jenis_pembayaran order by id"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_jenis_pembayaran"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()


                        sqlcom = "select id from currency order by id"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataTextField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_mata_uang"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        If CType(Me.dg_data.Items(x).FindControl("lbl_is_submit"), Label).Text = "B" Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                        End If
                    Next
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
                    Me.btn_submit.Enabled = False
                    Me.btn_print.Enabled = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.clearinvoice()
            Me.bindperiodetransaksi()
            Me.bindjenis_pembayaran()
            Me.bindmatauang()
            If String.IsNullOrEmpty(Me.vtgl) Then
                Me.tb_tanggal.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date)
            Else
                Me.tb_tanggal.Text = Me.vtgl.Day.ToString.PadLeft(2, "0") & "/" & Me.vtgl.Month.ToString.PadLeft(2, "0") & "/" & Me.vtgl.Year.ToString
            End If

            Me.loadgrid()
            Me.tb_id_no_invoice.Attributes.Add("style", "display: none;")
            Me.link_refresh_no_invoicer.Attributes.Add("style", "display: none;")
            Me.link_popup_no_invoice.Attributes.Add("onclick", "popup_so_collection('" & Me.tb_id_no_invoice.ClientID & "','" & Me.link_refresh_no_invoicer.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/lpp.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan)
    End Sub

    Protected Sub link_refresh_no_invoicer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_no_invoicer.Click
        Me.bindinvoice()
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.dd_faktur_nn.SelectedValue = "F" Then
                If Me.tb_id_no_invoice.Text = 0 Then
                    Me.lbl_msg.Text = "Silahkan mengisi nomor invoice terlebih dahulu"
                    Exit Sub
                End If
            End If
            If String.IsNullOrEmpty(Me.tb_jumlah.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi jumlah nilai terlebih dahulu"
            Else
                Dim vtgl As String = Nothing
                Dim vtgl_cek As String = Nothing

                vtgl = Me.tb_tanggal.Text.Substring(3, 2) & "/" & Me.tb_tanggal.Text.Substring(0, 2) & "/" & Me.tb_tanggal.Text.Substring(6, 4)

                If Not String.IsNullOrEmpty(Me.tb_tgl_cek_giro.Text) Then
                    vtgl_cek = Me.tb_tgl_cek_giro.Text.Substring(3, 2) & "/" & Me.tb_tgl_cek_giro.Text.Substring(0, 2) & "/" & Me.tb_tgl_cek_giro.Text.Substring(6, 4)
                End If

                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vmax from lpp"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into lpp (seq, tanggal, id_jenis_pembayaran, id_mata_uang, nama_bank, no_cek_giro, tgl_cek_giro,"
                sqlcom = sqlcom + " jumlah_nilai, no_so, keterangan, is_nn, id_transaction_period, is_submit)"
                sqlcom = sqlcom + " values (" & vmax & ",'" & vtgl & "'," & Me.dd_jenis_pembayaran.SelectedValue & ",'" & Me.dd_mata_uang.SelectedValue & "',"
                sqlcom = sqlcom + "'" & Me.tb_nama_bank.Text & "','" & Me.tb_no_giro.Text & "'"

                If String.IsNullOrEmpty(Me.tb_tgl_cek_giro.Text) Then
                    sqlcom = sqlcom + ", NULL"
                Else
                    sqlcom = sqlcom + ",'" & vtgl_cek & "'"
                End If

                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_jumlah.Text)

                If Me.dd_faktur_nn.SelectedValue = "F" Then
                    sqlcom = sqlcom + "," & Me.tb_id_no_invoice.Text & ","
                Else
                    sqlcom = sqlcom + ", NULL,"
                End If

                sqlcom = sqlcom + "'" & Me.tb_keterangan.Text & "','" & Me.dd_faktur_nn.SelectedValue & "'," & Me.vid_transaksi & ", 'B')"

                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete lpp"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah dihapus"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.lbl_msg.Text = "Data masih digunakan di form lain"
            Else
                Me.lbl_msg.Text = ex.Message
            End If
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    Dim vtgl As String = Nothing
                    If Not String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_cek_giro"), TextBox).Text) Then
                        vtgl = CType(Me.dg_data.Items(x).FindControl("tb_tgl_cek_giro"), TextBox).Text
                        vtgl = vtgl.Substring(3, 2) & "/" & vtgl.Substring(0, 2) & "/" & vtgl.Substring(6, 4)
                    End If

                    sqlcom = "update lpp"
                    sqlcom = sqlcom + " set id_jenis_pembayaran = " & CType(Me.dg_data.Items(x).FindControl("dd_jenis_pembayaran"), DropDownList).SelectedValue & ","
                    sqlcom = sqlcom + " id_mata_uang = '" & CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue & "',"
                    sqlcom = sqlcom + " nama_bank = '" & CType(Me.dg_data.Items(x).FindControl("tb_nama_bank"), TextBox).Text & "',"
                    sqlcom = sqlcom + " no_cek_giro = '" & CType(Me.dg_data.Items(x).FindControl("tb_no_cek_giro"), TextBox).Text & "',"

                    If String.IsNullOrEmpty(CType(Me.dg_data.Items(x).FindControl("tb_tgl_cek_giro"), TextBox).Text) Then
                        sqlcom = sqlcom + " tgl_cek_giro = NULL,"
                    Else
                        sqlcom = sqlcom + " tgl_cek_giro = '" & vtgl & "',"
                    End If
                    sqlcom = sqlcom + " jumlah_nilai = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_jumlah_nilai"), TextBox).Text) & ","
                    sqlcom = sqlcom + " keterangan = '" & CType(Me.dg_data.Items(x).FindControl("tb_keterangan"), TextBox).Text & "'"
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

    Protected Sub btn_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try

            Dim reportPath As String = Server.MapPath("reports\lpp.rpt")
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
            oRD.SetParameterValue("tanggal", Me.tb_tanggal.Text)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/lpp.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/lpp.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dd_faktur_nn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_faktur_nn.SelectedIndexChanged
        If Me.dd_faktur_nn.SelectedValue = "F" Then
            Me.link_popup_no_invoice.Enabled = True
            Me.link_popup_no_invoice.Attributes.Add("onclick", "popup_so_collection('" & Me.tb_id_no_invoice.ClientID & "','" & Me.link_refresh_no_invoicer.UniqueID & "')")
        Else
            Me.link_popup_no_invoice.Enabled = False
            Me.link_popup_no_invoice.Attributes.Clear()
        End If
    End Sub

    Sub seq_max()
        sqlcom = "select isnull(max(seq),0) + 1 as vmax from akun_general_ledger"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.vmax = reader.Item("vmax").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub jurnal()
        Dim vtgl As String = Me.vtanggal.Substring(3, 2) & "/" & Me.vtanggal.Substring(0, 2) & "/" & Me.vtanggal.Substring(6, 4)

        'jurnal collection
        Dim akun_piutang_giro_mundur As String = ""
        Dim akun_piutang_dagang As String = ""

        sqlcom = "select id_customer, rate from sales_order where so_no_text = '" & Me.vno_invoice & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.vkurs = reader.Item("rate").ToString
            Me.vnilai = Decimal.ToDouble(Me.vnilai) * Decimal.ToDouble(Me.vkurs)
            Me.vid_customer = reader.Item("id_customer").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        sqlcom = "select akun_piutang_giro_mundur, akun_piutang_dagang from daftar_customer where id = " & Me.vid_customer
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            akun_piutang_giro_mundur = reader.Item("akun_piutang_giro_mundur").ToString
            akun_piutang_dagang = reader.Item("akun_piutang_dagang").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        'debet
        ' akun_piutang_giro_mundur, akun_piutang_dagang
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vseq & "','" & vtgl & "','COLLECT','" & akun_piutang_giro_mundur & "',"
        sqlcom = sqlcom + "'" & akun_piutang_dagang & "'," & Decimal.ToDouble(Me.vnilai) & ",0, 'LPP no. " & Me.vseq & " (Pembayaran invoice no." & Me.vno_invoice & ")'"
        sqlcom = sqlcom + "," & Me.vid_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.vkurs) & ")"
        connection.koneksi.InsertRecord(sqlcom)

        'kredit
        ' akun_piutang_dagang, akun_piutang_giro_mundur
        Me.seq_max()
        sqlcom = "insert into akun_general_ledger(seq, id_transaksi, tgl_transaksi, kode_transaksi, coa_code, coa_code_lawan, nilai_debet, nilai_kredit,"
        sqlcom = sqlcom + " keterangan, id_transaction_period, id_currency, kurs)"
        sqlcom = sqlcom + " values(" & Me.vmax & ",'" & Me.vseq & "','" & vtgl & "','COLLECT','" & akun_piutang_dagang & "',"
        sqlcom = sqlcom + "'" & akun_piutang_giro_mundur & "'," & Decimal.ToDouble(Me.vnilai) & ",0, 'LPP no. " & Me.vseq & " (Pembayaran invoice no." & Me.vno_invoice & ")'"
        sqlcom = sqlcom + "," & Me.vid_transaksi & ",'" & Me.dd_mata_uang.SelectedValue & "'," & Decimal.ToDouble(Me.vkurs) & ")"
        connection.koneksi.InsertRecord(sqlcom)

    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update lpp"
                    sqlcom = sqlcom + " set is_submit = 'S'"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)

                    Me.vtanggal = CType(Me.dg_data.Items(x).FindControl("lbl_tanggal"), Label).Text.ToString
                    Me.vseq = CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    Me.vnilai = Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_jumlah_nilai"), TextBox).Text)
                    Me.vno_invoice = CType(Me.dg_data.Items(x).FindControl("lbl_invoice"), Label).Text

                    'Me.jurnal()
                    Me.lbl_msg.Text = "Data sudah disubmit dan tidak dapat diubah kembali"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
