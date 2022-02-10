'Daniel 27/3/2017 ===================================================================================

Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_detil_purchase_order_local
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


    Private ReadOnly Property vpaging() As Integer
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property


    Private ReadOnly Property vno_po() As Integer
        Get
            Dim o As Object = Request.QueryString("vno_po")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property no_po() As Integer
        Get
            Dim o As Object = ViewState("no_po")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("no_po") = value
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

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiodetransaksi()
        Try
            sqlcom = "select id, name from transaction_period where bulan = " & Me.vbulan & " and tahun = " & Me.vtahun
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_periode_transaksi.Text = reader.Item("name").ToString
                Me.vid_periode_transaksi = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub binddibuatoleh()
        Try
            Dim readerdibuat As SqlClient.SqlDataReader
            sqlcom = "select code, nama_pegawai from user_list where code <> 1 order by nama_pegawai"
            readerdibuat = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_dibuat_oleh.DataSource = readerdibuat
            Me.dd_dibuat_oleh.DataTextField = "nama_pegawai"
            Me.dd_dibuat_oleh.DataValueField = "code"
            Me.dd_dibuat_oleh.DataBind()
            readerdibuat.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub clearform()
        Try
            Me.lbl_no_pembelian.Text = ""
            Me.tb_tgl_pembelian.Text = ""
            Me.no_po = 0
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loaddata()
        Try
            If Me.vno_po <> 0 Then
                Me.no_po = Me.vno_po
            End If

            sqlcom = "select [no],[po_no_text],[inv_no], convert(char, [tanggal], 103) as tanggal,[id_transaction_period],[note],[dibuat_oleh]"
            sqlcom = sqlcom + " from [purchase_order_local]"
            sqlcom = sqlcom + " where no = " & Me.no_po
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_no_pembelian.Text = reader.Item("po_no_text").ToString
                Me.tb_inv_pembelian.Text = reader.Item("inv_no").ToString
                Me.tb_tgl_pembelian.Text = reader.Item("tanggal").ToString
                Me.tb_note.Text = reader.Item("note").ToString
                Me.dd_dibuat_oleh.SelectedValue = reader.Item("dibuat_oleh").ToString
                Me.tbl_produk.Visible = True
            Else
                Me.tbl_produk.Visible = False
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.clearform()
                Me.clearproduk()
                Me.bindperiodetransaksi()
                Me.binddibuatoleh()
                Me.tb_id_produk.Attributes.Add("style", "display: none;")
                Me.link_refresh_produk.Attributes.Add("style", "display: none;")
                Me.link_popup_produk.Attributes.Add("onclick", "popup_produk_item('" & Me.tb_id_produk.ClientID & "','" & Me.link_refresh_produk.UniqueID & "')")
                Me.loaddata()
                Me.loadgrid()
            Catch ex As Exception
                Me.lbl_msg.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Try
            Response.Redirect("~/purchase_order_local.aspx?vtahun=" & Me.vtahun & "&vbulan=" & Me.vbulan & "&vpaging=" & Me.vpaging)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub


    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            Dim vtgl_po As String = Me.tb_tgl_pembelian.Text.Substring(3, 2) & "/" & Me.tb_tgl_pembelian.Text.Substring(0, 2) & "/" & Me.tb_tgl_pembelian.Text.Substring(6, 4)

            If Me.no_po = 0 Then
                Dim vmax As Integer = 0

                sqlcom = "select isnull(max(no),0) + 1 as vmax from purchase_order_local"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vno_po_text As String = ""
                sqlcom = "select isnull(max(convert(int, right(po_no_text,3))),0) + 1 as vpo_no_text"
                sqlcom = sqlcom + " from purchase_order_local"
                sqlcom = sqlcom + " where substring(po_no_text,5,4) = " & Me.vtahun
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    'PBL-201111-100
                    vno_po_text = "PBL-" & Me.vtahun & Me.vbulan.ToString.PadLeft(2, "0") & "-" & reader.Item("vpo_no_text").ToString.PadLeft(3, "0")
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into purchase_order_local([no],[po_no_text],[inv_no],[tanggal],[id_transaction_period],[note],[dibuat_oleh])"
                sqlcom = sqlcom + " values('" & vmax & "','" & vno_po_text & "','" & Me.tb_inv_pembelian.Text.Trim() & "','" & vtgl_po & "','" & Me.vid_periode_transaksi & "','" & Me.tb_note.Text.Trim() & "','" & Me.dd_dibuat_oleh.SelectedValue & "')"
                connection.koneksi.InsertRecord(sqlcom)
                Me.no_po = vmax
                Me.lbl_msg.Text = "Data sudah disimpan"
            Else
                sqlcom = "update purchase_order_local"
                sqlcom = sqlcom + " set [inv_no] = '" & Me.tb_inv_pembelian.Text.Trim() & "',"
                sqlcom = sqlcom + " [tanggal] = '" & vtgl_po & "',"
                sqlcom = sqlcom + " [note] = '" & Me.tb_note.Text.Trim() & "',"
                sqlcom = sqlcom + " [dibuat_oleh] = " & Me.dd_dibuat_oleh.SelectedValue
                sqlcom = sqlcom + " where [no] = " & Me.no_po
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah diupdate"
            End If

            Me.loaddata()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub clearproduk()
        Me.tb_nama_produk.Text = ""
        Me.tb_nama_produk.Text = ""
        Me.link_popup_produk.Visible = True
    End Sub

    Sub bindproduk()
        Dim vkurs_harian As Decimal = 0
        Dim vtgl As String = Now.Day.ToString.PadLeft(2, "0") & "/" & Now.Month.ToString.PadLeft(2, "0") & "/" & Now.Year

        sqlcom = "select kurs_harian from kurs_harian"
        sqlcom = sqlcom + " where convert(char, tanggal, 103) = '" & vtgl & "'"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            vkurs_harian = reader.Item("kurs_harian").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        sqlcom = "select product_item.nama_beli, measurement_unit.name as nama_satuan, product_item.is_packaging,"
        sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + unit_packaging.name + '/' + measurement_unit.name as packaging,"
        sqlcom = sqlcom + " unit_packaging.name as nama_satuan_packaging"
        'sqlcom = sqlcom + " isnull((select isnull(harga_jual,0) from product_price where id_product = product_item.id),0) as harga_jual"
        sqlcom = sqlcom + " from product_item"
        sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
        sqlcom = sqlcom + " inner join measurement_unit unit_packaging on unit_packaging.id = product_item.id_measurement_conversion"
        sqlcom = sqlcom + " where product_item.id = " & Me.tb_id_produk.Text

        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.tb_nama_produk.Text = reader.Item("nama_beli").ToString
            If reader.Item("is_packaging").ToString = "P" Then
                Me.lbl_satuan.Text = reader.Item("nama_satuan").ToString
            Else
                Me.lbl_satuan.Text = reader.Item("nama_satuan_packaging").ToString
            End If

            Me.lbl_packaging.Text = reader.Item("packaging").ToString

            'If Me.dd_mata_uang.SelectedValue = "IDR" Then
            'Me.tb_harga.Text = Decimal.ToDouble(reader.Item("harga_jual").ToString) * Decimal.ToDouble(vkurs_harian)
            'Else
            'Me.tb_harga.Text = Decimal.ToDouble(reader.Item("harga_jual").ToString)
            'End If

        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub link_refresh_produk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_produk.Click
        Me.bindproduk()
    End Sub

    Sub clearitem()
        Me.tb_nama_produk.Text = ""
        Me.lbl_satuan.Text = ""
        Me.lbl_packaging.Text = ""
        Me.tb_qty.Text = ""
        Me.tb_harga.Text = ""
    End Sub

    Sub bindtotal_nilai()
        Try
            sqlcom = "select isnull((sum(isnull(qty,0) * isnull(unit_price,0))),0) as vtotal_nilai"
            sqlcom = sqlcom + " from purchase_order_local_detil"
            sqlcom = sqlcom + " where po_no = " & Me.no_po
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_total_nilai.Text = FormatNumber(reader.Item("vtotal_nilai").ToString(), 3)
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try
            Me.clearproduk()
            Me.clearitem()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select po_no, id_product, nama_product, isnull(qty,0) as qty, isnull(unit_price,0) as unit_price,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + packaging.name + '/' + measurement_unit.name as packaging,"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + " when product_item.is_packaging = 'P' then measurement_unit.name"
            sqlcom = sqlcom + " when product_item.is_packaging = 'Q' then packaging.name"
            sqlcom = sqlcom + " end as satuan_produk, "
            sqlcom = sqlcom + " (isnull([qty], 0) * isnull([unit_price],0)) as sub_total"
            sqlcom = sqlcom + " from purchase_order_local_detil"
            sqlcom = sqlcom + " inner join product_item on product_item.id = purchase_order_local_detil.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where purchase_order_local_detil.po_no = " & Me.no_po
            sqlcom = sqlcom + " order by purchase_order_local_detil.seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "purchase_order_local_detil")
                Me.dg_data.DataSource = ds.Tables("purchase_order_local_detil").DefaultView

                If ds.Tables("purchase_order_local_detil").Rows.Count > 0 Then
                    If ds.Tables("purchase_order_local_detil").Rows.Count > 10 Then
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
                    Me.tbl_total_harga.Visible = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
                    Me.tbl_total_harga.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()

            Me.bindtotal_nilai()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Try
            If String.IsNullOrEmpty(Me.tb_nama_produk.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama produk terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_qty.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi quantity terlebih dahulu"
            ElseIf Not String.IsNullOrEmpty(Me.tb_qty.Text) And Decimal.ToDouble(Me.tb_qty.Text) <= 0 Then
                Me.lbl_msg.Text = "Quantity tidak boleh lebih kecil atau sama dengan Nol"
            ElseIf String.IsNullOrEmpty(Me.tb_harga.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi harga terlebih dahulu"
            Else

                sqlcom = "select * from purchase_order_local_detil"
                sqlcom = sqlcom + " where po_no = " & Me.no_po
                sqlcom = sqlcom + " and id_product = " & Me.tb_id_produk.Text
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    Me.lbl_msg.Text = "Produk tersebut sudah ada"
                    reader.Close()
                    connection.koneksi.CloseKoneksi()
                    Exit Sub
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vseq As Integer = 0
                sqlcom = "select isnull(max(seq),0) + 1 as vseq from purchase_order_local_detil"
                sqlcom = sqlcom + " where po_no = " & Me.no_po
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vseq = reader.Item("vseq").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into purchase_order_local_detil(po_no, id_product, nama_product, qty, unit_price, seq)"
                sqlcom = sqlcom + " values(" & Me.no_po & "," & Me.tb_id_produk.Text & ",'" & Me.tb_nama_produk.Text & "'"
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_qty.Text) & "," & Decimal.ToDouble(Me.tb_harga.Text) & "," & vseq & ")"
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
                    sqlcom = "delete purchase_order_local_detil"
                    sqlcom = sqlcom + " where po_no = " & Me.no_po
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
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
                    sqlcom = "update purchase_order_local_detil"
                    sqlcom = sqlcom + " set nama_product = '" & CType(Me.dg_data.Items(x).FindControl("tb_name"), TextBox).Text & "',"
                    sqlcom = sqlcom + " qty = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) & ","
                    sqlcom = sqlcom + " unit_price = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_harga"), TextBox).Text)
                    sqlcom = sqlcom + " where po_no = " & Me.no_po
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
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
            Dim reportPath As String = Server.MapPath("reports\purchase_order_local.rpt")
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
            oRD.SetParameterValue("po_no", Me.no_po)
            oRD.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            oRD.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
            oExO = oRD.ExportOptions
            oExO.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            oRD.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Pdf_files/purchase_order_local.pdf"))
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('Pdf_files/purchase_order_local.pdf', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class

' =================================================================================== Daniel 27/3/2017