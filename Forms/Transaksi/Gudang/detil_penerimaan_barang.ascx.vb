Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Gudang_detil_penerimaan_barang
    Inherits System.Web.UI.UserControl

    Private ReadOnly Property vid_periode() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_periode")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vseq() As Integer
        Get
            Dim o As Object = Request.QueryString("vseq")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property


    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode()
        sqlcom = "select name from transaction_period where id = " & Me.vid_periode
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_periode.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub


    Sub loaddokumen()
        Try
            sqlcom = "select seq, bl_no, invoice_no, convert(char, tgl_invoice, 103) as tgl_invoice, convert(char, tgl_bayar_invoice, 103) as tgl_bayar_invoice,"
            sqlcom = sqlcom + " isnull(nilai_invoice,0) as nilai_invoice, packing_list_no, daftar_supplier.name as nama_supplier, is_submit_gudang"
            sqlcom = sqlcom + " from entry_dokumen_impor"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = entry_dokumen_impor.no_po"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"
            sqlcom = sqlcom + " where seq = " & Me.vseq
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_nama_supplier.Text = reader.Item("nama_supplier").ToString
                Me.lbl_no_bl.Text = reader.Item("bl_no").ToString
                Me.lbl_no_packing_list.Text = reader.Item("packing_list_no").ToString
                If reader.Item("is_submit_gudang").ToString = "B" Then
                    Me.btn_update.Enabled = True
                Else
                    Me.btn_update.Enabled = False
                End If
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id_product, nama_product, isnull(qty,0) as qty, isnull(unit_price,0) as unit_price,"
            sqlcom = sqlcom + " isnull(discount,0) as discount, isnull(qty_terima,0) as qty_terima,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + packaging.name + '/' + measurement_unit.name as packaging,"
            sqlcom = sqlcom + " case "
            sqlcom = sqlcom + " when product_item.is_packaging = 'P' then measurement_unit.name"
            sqlcom = sqlcom + " when product_item.is_packaging = 'Q' then packaging.name"
            sqlcom = sqlcom + " end as satuan_produk, "
            sqlcom = sqlcom + " isnull(isnull(entry_dokumen_impor_produk.qty,0) * (isnull(entry_dokumen_impor_produk.unit_price,0) - "
            sqlcom = sqlcom + " isnull(entry_dokumen_impor_produk.unit_price, 0) * isnull(entry_dokumen_impor_produk.discount,0) /100),0) as sub_total"
            sqlcom = sqlcom + " from entry_dokumen_impor_produk"
            sqlcom = sqlcom + " inner join product_item on product_item.id = entry_dokumen_impor_produk.id_product"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where entry_dokumen_impor_produk.seq_entry = " & Me.vseq
            sqlcom = sqlcom + " order by entry_dokumen_impor_produk.nama_product"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "entry_dokumen_impor_produk")
                Me.dg_data.DataSource = ds.Tables("entry_dokumen_impor_produk").DefaultView

                If ds.Tables("entry_dokumen_impor_produk").Rows.Count > 0 Then
                    If ds.Tables("entry_dokumen_impor_produk").Rows.Count > 10 Then
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
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
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
            Me.bindperiode()
            Me.loaddokumen()
            Me.lbl_no_dokumen.Text = Me.vseq
            Me.loadgrid()
        End If
    End Sub

    Private Function CloseString() As String
        Return "<script language=""javascript"" type=""text/javascript"">window.close();<" & "/" & "script>"
    End Function

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Me.ltl_js.Text = Me.CloseString
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked Then
                    sqlcom = "update entry_dokumen_impor_produk"
                    sqlcom = sqlcom + " set qty_terima = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty_terima"), TextBox).Text)
                    sqlcom = sqlcom + " where seq_entry = " & Me.vseq
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id_produk"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
