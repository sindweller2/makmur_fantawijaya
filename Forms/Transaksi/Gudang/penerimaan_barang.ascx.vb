Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Gudang_penerimaan_barang
    Inherits System.Web.UI.UserControl

    Public Property id_periode() As Integer
        Get
            Dim o As Object = ViewState("id_periode")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_periode") = value
        End Set
    End Property

    Public Property id_user() As Integer
        Get
            Dim o As Object = ViewState("id_user")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_user") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub cleardokumen()
        Me.tb_no_dokumen.Text = 0
        Me.lbl_no_dokumen.Text = "-----"
        Me.lbl_no_packing_list.Text = ""
        Me.lbl_no_bl.Text = ""
        Me.lbl_nama_supplier.Text = ""
        Me.link_popup_no_dokumen.Visible = True
    End Sub

    Sub binddokumen()
        Me.lbl_no_dokumen.Text = Me.tb_no_dokumen.Text
        sqlcom = "select entry_dokumen_impor.packing_list_no,  entry_dokumen_impor.bl_no, daftar_supplier.name as nama_supplier"
        sqlcom = sqlcom + " from entry_dokumen_impor"
        sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
        sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
        sqlcom = sqlcom + " where seq = " & Me.tb_no_dokumen.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_no_packing_list.Text = reader.Item("packing_list_no").ToString
            Me.lbl_no_bl.Text = reader.Item("bl_no").ToString
            Me.lbl_nama_supplier.Text = reader.Item("nama_supplier").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindperiode_transaksi()
        sqlcom = "select id, name"
        sqlcom = sqlcom + " from transaction_period"
        sqlcom = sqlcom + " where id >= 12"
        sqlcom = sqlcom + " and tahun = " & Me.tb_tahun.Text
        sqlcom = sqlcom + " order by id"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_bulan.DataSource = reader
        Me.dd_bulan.DataTextField = "name"
        Me.dd_bulan.DataValueField = "id"
        Me.dd_bulan.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try

            Me.cleardokumen()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select seq, convert(char, tgl_terima, 103) as tgl_terima, bl_no, invoice_no, convert(char, tgl_invoice, 103) as tgl_invoice,"
            sqlcom = sqlcom + " convert(char, tgl_jatuh_tempo_invoice_supplier, 103) as tgl_jatuh_tempo_invoice_supplier,"
            sqlcom = sqlcom + " isnull(nilai_invoice,0) as nilai_invoice, packing_list_no, is_submit_gudang, daftar_supplier.name as nama_supplier,"
            sqlcom = sqlcom + " rtrim(convert(char, tgl_terima_barang, 103)) as tgl_terima_barang"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " where id_transaksi_terima_barang = " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " order by seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "entry_dokumen_impor")
                Me.dg_data.DataSource = ds.Tables("entry_dokumen_impor").DefaultView

                If ds.Tables("entry_dokumen_impor").Rows.Count > 0 Then
                    If ds.Tables("entry_dokumen_impor").Rows.Count > 10 Then
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
                    Me.btn_delete.Visible = True
                    Me.btn_submit.Visible = True

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        If CType(Me.dg_data.Items(x).FindControl("lbl_submit"), Label).Text = "B" Then
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                        Else
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                        End If
                    Next
                Else
                    Me.dg_data.Visible = False
                    Me.btn_delete.Visible = False
                    Me.btn_submit.Visible = False
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
            Me.tb_tahun.Text = Year(Now.Date)
            Me.bindperiode_transaksi()
            Me.tb_tgl_terima.Text = Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString
            Me.id_user = HttpContext.Current.Session("UserID")
            Me.loadgrid()
            Me.tb_no_dokumen.Attributes.Add("style", "display: none;")
            Me.link_refresh_no_dokumen.Attributes.Add("style", "display: none;")
            Me.link_popup_no_dokumen.Attributes.Add("onclick", "popup_dokumen_gudang('" & Me.tb_no_dokumen.ClientID & "','" & Me.link_refresh_no_dokumen.UniqueID & "')")
        End If
    End Sub

    Protected Sub link_refresh_no_dokumen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_no_dokumen.Click
        Me.binddokumen()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_no_dokumen.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor dokumen terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tgl_terima.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terima barang terlebih dahulu"
            Else
                Dim vtgl As String = Me.tb_tgl_terima.Text.Substring(3, 2) & "/" & Me.tb_tgl_terima.Text.Substring(0, 2) & "/" & Me.tb_tgl_terima.Text.Substring(6, 4)

                sqlcom = "update entry_dokumen_impor"
                sqlcom = sqlcom + " set id_transaksi_terima_barang = " & Me.dd_bulan.SelectedValue & ","
                sqlcom = sqlcom + " tgl_terima_barang = '" & vtgl & "',"
                sqlcom = sqlcom + " id_penerima_barang = " & Me.id_user & ","
                sqlcom = sqlcom + " is_submit_gudang = 'B'"
                sqlcom = sqlcom + " where seq = " & Me.tb_no_dokumen.Text
                connection.koneksi.UpdateRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Dim vscript As String = ""
            vscript = "<script>" & vbCrLf
            vscript = vscript + "window.open('detil_penerimaan_barang.aspx?vid_periode=" & Me.dd_bulan.SelectedValue & "&vseq=" & CType(e.Item.FindControl("lbl_seq"), Label).Text & "', '_blank', 'resizable=yes,scrollbars=yes');" & vbCrLf
            vscript = vscript + "</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
        End If
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.loadgrid()
    End Sub


    Protected Sub btn_refresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update entry_dokumen_impor"
                    sqlcom = sqlcom + " set id_transaksi_terima_barang = NULL,"
                    sqlcom = sqlcom + " tgl_terima_barang = NULL,"
                    sqlcom = sqlcom + " id_penerima_barang = NULL,"
                    sqlcom = sqlcom + " is_submit_gudang = NULL"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah dihapus"
                End If
            Next            
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try            
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update entry_dokumen_impor"
                    sqlcom = sqlcom + " set is_submit_gudang = 'S'"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disubmit dan tidak dapat diubah kembali"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

End Class
