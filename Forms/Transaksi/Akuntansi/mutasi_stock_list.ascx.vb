Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Accounting_mutasi_stock_list
    Inherits System.Web.UI.UserControl

    Public Property vid_user() As Integer
        Get
            Dim o As Object = ViewState("vid_user")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_user") = value
        End Set
    End Property


    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

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

    Sub clearproduk_asal()
        Me.tb_id_produk_asal.Text = 0
        Me.lbl_nama_produk_asal.Text = "-----"
        Me.link_popup_produk_asal.Visible = True
        Me.lbl_satuan_asal.text = ""
        Me.lbl_packaging_asal.text = ""
        Me.lbl_qty_asal.text = ""
    End Sub

    Sub bindproduk_asal()
        sqlcom = "select product_item.nama_beli, measurement_unit.name as nama_satuan, product_item.is_packaging,"
        sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + unit_packaging.name + '/' + measurement_unit.name as packaging,"
        sqlcom = sqlcom + " unit_packaging.name as nama_satuan_packaging, product_price.harga_jual"
        sqlcom = sqlcom + " from product_item"
        sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
        sqlcom = sqlcom + " inner join measurement_unit unit_packaging on unit_packaging.id = product_item.id_measurement_conversion"
        sqlcom = sqlcom + " inner join product_price on product_price.id_product = product_item.id"
        sqlcom = sqlcom + " where product_item.id = " & Me.tb_id_produk_asal.Text

        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_produk_asal.Text = reader.Item("nama_beli").ToString
            If reader.Item("is_packaging").ToString = "P" Then
                Me.lbl_satuan_asal.Text = reader.Item("nama_satuan").ToString
            Else
                Me.lbl_satuan_asal.Text = reader.Item("nama_satuan_packaging").ToString
            End If

            Me.lbl_packaging_asal.Text = reader.Item("packaging").ToString

        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

        sqlcom = "select isnull(qty_stock,0) as qty_stock"

        'Daniel - 22052016
        sqlcom = sqlcom + " from inventory_stock_barang"
        sqlcom = sqlcom + " where id_produk = " & Me.tb_id_produk_asal.Text

        'sqlcom = sqlcom + " from stock_barang_gudang"
        'sqlcom = sqlcom + " where id_product = " & Me.tb_id_produk_asal.Text
        'Daniel - 22052016

        sqlcom = sqlcom + " and id_transaction_period = " & Me.dd_bulan.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_qty_asal.Text = FormatNumber(reader.Item("qty_stock").ToString, 2)
            'dendi
        Else

            Me.lbl_qty_asal.Text = "Data ini tidak ada pada periode ini"
            'dendi

        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

    End Sub


    Sub clearproduk_tujuan()
        Me.tb_id_produk_tujuan.Text = 0
        Me.lbl_nama_produk_tujuan.Text = "-----"
        Me.link_popup_produk_tujuan.Visible = True
        Me.lbl_satuan_tujuan.text = ""
        Me.lbl_packaging_tujuan.text = ""
    End Sub

    Sub bindproduk_tujuan()
        sqlcom = "select product_item.nama_beli, measurement_unit.name as nama_satuan, product_item.is_packaging,"
        sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + unit_packaging.name + '/' + measurement_unit.name as packaging,"
        sqlcom = sqlcom + " unit_packaging.name as nama_satuan_packaging, product_price.harga_jual"
        sqlcom = sqlcom + " from product_item"
        sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = product_item.id_measurement"
        sqlcom = sqlcom + " inner join measurement_unit unit_packaging on unit_packaging.id = product_item.id_measurement_conversion"
        sqlcom = sqlcom + " inner join product_price on product_price.id_product = product_item.id"
        sqlcom = sqlcom + " where product_item.id = " & Me.tb_id_produk_tujuan.Text

        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_produk_tujuan.Text = reader.Item("nama_beli").ToString
            If reader.Item("is_packaging").ToString = "P" Then
                Me.lbl_satuan_tujuan.Text = reader.Item("nama_satuan").ToString
            Else
                Me.lbl_satuan_tujuan.Text = reader.Item("nama_satuan_packaging").ToString
            End If

            Me.lbl_packaging_tujuan.Text = reader.Item("packaging").ToString

        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub
    
    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select mutasi_stock.no, rtrim(convert(char, mutasi_stock.tanggal,103)) as tanggal,"
            sqlcom = sqlcom + " mutasi_stock.id_product_from, mutasi_stock.id_product_to,"
            sqlcom = sqlcom + " isnull(mutasi_stock.qty,0) as qty, isnull(mutasi_stock.qty,0) as temp_qty, mutasi_stock.keterangan, " 
            sqlcom = sqlcom + " asal.nama_beli as nama_beli_asal, measurement_asal.name as nama_satuan_asal,"
            sqlcom = sqlcom + " rtrim(convert(char, asal.qty_conversion)) + ' ' + unit_packaging_asal.name + '/' + measurement_asal.name as packaging_asal,"
            sqlcom = sqlcom + " unit_packaging_asal.name as nama_satuan_packaging_asal,"
            sqlcom = sqlcom + " tujuan.nama_beli as nama_beli_tujuan, measurement_tujuan.name as nama_satuan_tujuan,"
            sqlcom = sqlcom + " rtrim(convert(char, tujuan.qty_conversion)) + ' ' + unit_packaging_tujuan.name + '/' + measurement_tujuan.name as packaging_tujuan,"
            sqlcom = sqlcom + " unit_packaging_tujuan.name as nama_satuan_packaging_tujuan"
            sqlcom = sqlcom + " from mutasi_stock"
            sqlcom = sqlcom + " inner join product_item asal on asal.id = mutasi_stock.id_product_from"
            sqlcom = sqlcom + " inner join product_item tujuan on tujuan.id = mutasi_stock.id_product_to"
            sqlcom = sqlcom + " inner join measurement_unit measurement_asal on measurement_asal.id = asal.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit measurement_tujuan on measurement_tujuan.id = tujuan.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit unit_packaging_asal on unit_packaging_asal.id = asal.id_measurement_conversion"
            sqlcom = sqlcom + " inner join measurement_unit unit_packaging_tujuan on unit_packaging_tujuan.id = tujuan.id_measurement_conversion"
            sqlcom = sqlcom + " where mutasi_stock.id_transaction_period = " & Me.dd_bulan.SelectedValue          
            sqlcom = sqlcom + " order by mutasi_stock.no"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "mutasi_stock")
                Me.dg_data.DataSource = ds.Tables("mutasi_stock").DefaultView

                If ds.Tables("mutasi_stock").Rows.Count > 0 Then
                    If ds.Tables("mutasi_stock").Rows.Count > 10 Then
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
 
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
                    Me.btn_delete.Visible = False
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
            Me.vid_user = HttpContext.Current.Session("UserID")
            Me.clearproduk_asal()
            Me.clearproduk_tujuan()
            Me.tb_tanggal.Text = Day(now.date).ToString.Padleft(2,"0") & "/" & Month(now.date).ToString.Padleft(2,"0") & "/" & Year(now.date).ToString
            Me.tb_tahun.Text = Now.Year
            Me.bindperiode_transaksi()

            Me.tb_id_produk_asal.Attributes.Add("style", "display: none;")
            Me.tb_id_produk_tujuan.Attributes.Add("style", "display: none;")
            Me.link_refresh_produk_asal.Attributes.Add("style", "display: none;")
            Me.link_refresh_produk_tujuan.Attributes.Add("style", "display: none;")            

            Me.loadgrid()
            Me.link_popup_produk_asal.Attributes.Add("onclick", "popup_produk('" & Me.tb_id_produk_asal.ClientID & "','" & Me.link_refresh_produk_asal.UniqueID & "')")
            Me.link_popup_produk_tujuan.Attributes.Add("onclick", "popup_produk('" & Me.tb_id_produk_tujuan.ClientID & "','" & Me.link_refresh_produk_tujuan.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

   Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            if string.isnullorEmpty(me.tb_tanggal.text) then
               me.lbl_msg.text = "Silahkan mengisi tanggal terlebih dahulu"
            elseif me.tb_id_produk_asal.text = 0 then
               me.lbl_msg.text = "Silahkan mengisi nama produk asal terlebih dahulu"
            elseif me.tb_id_produk_tujuan.text = 0 then
               me.lbl_msg.text = "Silahkan mengisi nama produk tujuan terlebih dahulu"
            elseif string.isnullorempty(me.tb_qty.text) then
               me.lbl_msg.text = "Silahkan mengisi qty terlebih dahulu"
            elseif string.isnullorempty(me.tb_keterangan.text) then
               me.lbl_msg.text = "Silahkan mengisi keterangan terlebih dahulu"
            else
               dim is_ok as string = "T"
                sqlcom = "select isnull(qty_stock,0) as qty_stock"

                'Daniel - 23052016

                sqlcom = sqlcom + " from inventory_stock_barang"
                sqlcom = sqlcom + " where id_produk = " & Me.tb_id_produk_asal.Text

                'sqlcom = sqlcom + " from stock_barang_gudang"
                'sqlcom = sqlcom + " where id_product = " & Me.tb_id_produk_asal.Text

                'Daniel - 23052016

               sqlcom = sqlcom + " and id_transaction_period = " & me.dd_bulan.selectedvalue
               reader = connection.koneksi.selectrecord(sqlcom)
                reader.Read()

                'Daniel - 27052016
                is_ok = "Y"

                'if reader.hasrows then
                '   is_ok = "Y"
                '   if decimal.todouble(reader.item("qty_stock").ToString) > decimal.todouble(me.tb_qty.text) then
                '      is_ok = "Y"
                '   else
                '      Me.lbl_msg.text = "Qty barang asal tersebut di daftar stock barang gudang pada periode tersebut tidak mencukupi "
                '      is_ok = "T"
                '   end if
                'else
                '   is_ok = "T"
                '   Me.lbl_msg.text = "Barang asal tersebut tidak ada di daftar stock barang gudang pada periode tersebut"
                ' End If

                'Daniel - 27052016

               reader.close()
               connection.koneksi.closekoneksi()
               

               if is_ok = "Y" then
                    sqlcom = "select *"
                    

                    'Daniel - 23052016

                    sqlcom = sqlcom + " from inventory_stock_barang"
                    sqlcom = sqlcom + " where id_produk = " & Me.tb_id_produk_asal.Text

                    'sqlcom = sqlcom + " from stock_barang_gudang"
                    'sqlcom = sqlcom + " where id_product = " & Me.tb_id_produk_tujuan.Text

                    'Daniel - 23052016

                    sqlcom = sqlcom + " and id_transaction_period = " & Me.dd_bulan.selectedvalue
                    reader = connection.koneksi.selectrecord(sqlcom)
                    reader.Read()

                    'Daniel - 27052016

                    'If reader.hasrows Then
                    '    is_ok = "Y"
                    'Else
                    '    is_ok = "T"
                    '    Me.lbl_msg.text = "Barang tujuan tersebut tidak ada di daftar stock barang gudang pada periode tersebut"
                    'End If

                    'Daniel - 27052016

                    reader.close()
                    connection.koneksi.closekoneksi()

                    If is_ok = "Y" Then
                        Dim vtgl As Date = Me.tb_tanggal.text.substring(3, 2) & "/" & Me.tb_tanggal.text.substring(0, 2) & "/" & Me.tb_tanggal.text.substring(6, 4)
                        Dim vmax As Integer = 0
                        sqlcom = "select isnull(max(right(no,6)),0) + 1 as vmax"
                        sqlcom = sqlcom + " from mutasi_stock"
                        sqlcom = sqlcom + " where substring(rtrim(convert(char, no)), 1, 2) = right(" & Me.tb_tahun.text & ",2)"
                        reader = connection.koneksi.selectrecord(sqlcom)
                        reader.read()
                        If reader.hasrows Then
                            vmax = reader.item("vmax").ToString
                        End If
                        reader.close()
                        connection.koneksi.closekoneksi()

                        vmax = right(Me.tb_tahun.text, 2) & vmax.ToString.PadLeft(6, "0")

                        sqlcom = "insert into mutasi_stock(no, tanggal, id_transaction_period, id_product_from, qty,"
                        sqlcom = sqlcom + " id_product_to, created_by, keterangan)"
                        sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "'," & Me.dd_bulan.selectedvalue & "," & Me.tb_id_produk_asal.text
                        sqlcom = sqlcom + "," & Decimal.todouble(Me.tb_qty.text) & "," & Me.tb_id_produk_tujuan.text & "," & Me.vid_user
                        sqlcom = sqlcom + ",'" & Me.tb_keterangan.text & "')"
                        connection.koneksi.insertrecord(sqlcom)

                        Me.update_stock(Me.tb_id_produk_asal.text, Me.tb_id_produk_tujuan.Text, "A", Decimal.todouble(Me.tb_qty.text), Decimal.todouble(Me.tb_qty.text))

                        Me.lbl_msg.text = "Data tersebut sudah disimpan"
                        Me.clearproduk_asal()
                        Me.clearproduk_tujuan()
                        Me.tb_qty.Text = ""
                        Me.tb_keterangan.text = ""

                    End If

                End If

            End If
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then

                    Dim is_ok As String = "T"
                    Dim id_produk_asal As Integer = CType(Me.dg_data.Items(x).FindControl("lbl_id_produk_asal"), Label).Text
                    Dim id_produk_tujuan As Integer = CType(Me.dg_data.Items(x).FindControl("lbl_id_produk_tujuan"), Label).Text
                    Dim qty As Decimal = Decimal.todouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text)
                    Dim temp_qty As Decimal = Decimal.todouble(CType(Me.dg_data.Items(x).FindControl("lbl_temp_qty"), Label).Text)

                    sqlcom = "select isnull(qty_stock,0) as qty_stock"
                    sqlcom = sqlcom + " from stock_barang_gudang"
                    sqlcom = sqlcom + " where id_product = " & id_produk_asal
                    sqlcom = sqlcom + " and id_transaction_period = " & Me.dd_bulan.selectedvalue
                    reader = connection.koneksi.selectrecord(sqlcom)
                    reader.read()
                    If reader.hasrows Then
                        is_ok = "Y"
                        If Decimal.todouble(reader.item("qty_stock").ToString) > Decimal.todouble(qty) - Decimal.todouble(temp_qty) Then
                            is_ok = "Y"
                        Else
                            Me.lbl_msg.text = "Qty barang asal tersebut di daftar stock barang gudang pada periode tersebut tidak mencukupi "
                            is_ok = "T"
                        End If
                    Else
                        is_ok = "T"
                        Me.lbl_msg.text = "Barang asal tersebut tidak ada di daftar stock barang gudang pada periode tersebut"
                    End If
                    reader.close()
                    connection.koneksi.closekoneksi()

                    If is_ok = "Y" Then

                        sqlcom = "select *"
                        sqlcom = sqlcom + " from stock_barang_gudang"
                        sqlcom = sqlcom + " where id_product = " & id_produk_tujuan
                        sqlcom = sqlcom + " and id_transaction_period = " & Me.dd_bulan.selectedvalue
                        reader = connection.koneksi.selectrecord(sqlcom)
                        reader.read()
                        If reader.hasrows Then
                            is_ok = "Y"
                        Else
                            is_ok = "T"
                            Me.lbl_msg.text = "Barang tujuan tersebut tidak ada di daftar stock barang gudang pada periode tersebut"
                        End If
                        reader.close()
                        connection.koneksi.closekoneksi()

                        If is_ok = "Y" Then
                            Me.update_stock(id_produk_asal, id_produk_tujuan, "U", Decimal.todouble(qty), Decimal.todouble(temp_qty))

                            sqlcom = "update mutasi_stock"
                            sqlcom = sqlcom + " set qty = " & Decimal.todouble(qty) & ","
                            sqlcom = sqlcom + " keterangan = '" & CType(Me.dg_data.Items(x).FindControl("tb_keterangan"), TextBox).Text & "'"
                            sqlcom = sqlcom + " where no = " & CType(Me.dg_data.Items(x).FindControl("lbl_no"), Label).Text
                            connection.koneksi.DeleteRecord(sqlcom)

                            Me.lbl_msg.Text = "Data sudah diupdate"

                        End If

                    End If

                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub


   Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then

                    dim id_produk_asal as integer = CType(Me.dg_data.Items(x).FindControl("lbl_id_produk_asal"), Label).Text
                    dim id_produk_tujuan as integer = CType(Me.dg_data.Items(x).FindControl("lbl_id_produk_tujuan"), Label).Text
                    dim qty as decimal = decimal.todouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text)

                    Me.update_stock(id_produk_asal, id_produk_tujuan, "D", decimal.todouble(qty), decimal.todouble(qty))

                    sqlcom = "delete mutasi_stock"
                    sqlcom = sqlcom + " where no = " & CType(Me.dg_data.Items(x).FindControl("lbl_no"), Label).Text
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

    Sub update_stock(ByVal id_produk_asal as integer, Byval id_produk_tujuan as integer, ByVal flag as string, ByVal qty as decimal, ByVal temp_qty as decimal)
        Try
           if flag = "A" then
              sqlcom = "update stock_barang_gudang"
              sqlcom = sqlcom + " set qty_stock = isnull(qty_stock,0) - " & decimal.todouble(qty)
              sqlcom = sqlcom + " where id_product = " & id_produk_asal
              sqlcom = sqlcom + " and id_transaction_period = " & me.dd_bulan.selectedvalue
              connection.koneksi.updaterecord(sqlcom)

              sqlcom = "update stock_barang_gudang"
              sqlcom = sqlcom + " set qty_stock = isnull(qty_stock,0) + " & decimal.todouble(qty)
              sqlcom = sqlcom + " where id_product = " & id_produk_tujuan
              sqlcom = sqlcom + " and id_transaction_period = " & me.dd_bulan.selectedvalue
              connection.koneksi.updaterecord(sqlcom)

           elseif flag = "U" then
              sqlcom = "update stock_barang_gudang"
              sqlcom = sqlcom + " set qty_stock = isnull(qty_stock,0) - " & decimal.todouble(qty) & " + " & decimal.todouble(temp_qty)
              sqlcom = sqlcom + " where id_product = " & id_produk_asal
              sqlcom = sqlcom + " and id_transaction_period = " & me.dd_bulan.selectedvalue
              connection.koneksi.updaterecord(sqlcom)

              sqlcom = "update stock_barang_gudang"
              sqlcom = sqlcom + " set qty_stock = isnull(qty_stock,0) + " & decimal.todouble(qty) & " -  " & decimal.todouble(temp_qty)
              sqlcom = sqlcom + " where id_product = " & id_produk_tujuan
              sqlcom = sqlcom + " and id_transaction_period = " & me.dd_bulan.selectedvalue
              connection.koneksi.updaterecord(sqlcom)

           elseif flag = "D" then
              sqlcom = "update stock_barang_gudang"
              sqlcom = sqlcom + " set qty_stock = isnull(qty_stock,0) + " & decimal.todouble(qty)
              sqlcom = sqlcom + " where id_product = " & id_produk_asal
              sqlcom = sqlcom + " and id_transaction_period = " & me.dd_bulan.selectedvalue
              connection.koneksi.updaterecord(sqlcom)

              sqlcom = "update stock_barang_gudang"
              sqlcom = sqlcom + " set qty_stock = isnull(qty_stock,0) - " & decimal.todouble(qty)
              sqlcom = sqlcom + " where id_product = " & id_produk_tujuan
              sqlcom = sqlcom + " and id_transaction_period = " & me.dd_bulan.selectedvalue
              connection.koneksi.updaterecord(sqlcom)
           end if
        Catch ex As Exception
           Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    'dendi

    Sub update_dari_dendi()

    End Sub
    'dendi

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    Protected Sub link_refresh_produk_asal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_produk_asal.Click
        Me.bindproduk_asal()
    End Sub

    Protected Sub link_refresh_produk_tujuan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_produk_tujuan.Click
        Me.bindproduk_tujuan()
    End Sub

End Class

