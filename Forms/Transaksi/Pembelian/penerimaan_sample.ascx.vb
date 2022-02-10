Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_penerimaan_sample
    Inherits System.Web.UI.UserControl

    Public tradingClass As New tradingClass()

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindperiode_transaksi()
        Try
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
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub bindmata_uang()
        Try
            sqlcom = "select id from currency order by id"
            reader = connection.koneksi.SelectRecord(sqlcom)
            Me.dd_mata_uang.DataSource = reader
            Me.dd_mata_uang.DataTextField = "id"
            Me.dd_mata_uang.DataValueField = "id"
            Me.dd_mata_uang.DataBind()
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub clearsupplier()
        Me.tb_id_supplier.Text = 0
        Me.lbl_nama_supplier.Text = "-----"
        Me.link_popup_supplier.Visible = True
    End Sub

    Sub bindsupplier()
        Try
            sqlcom = "select name"
            sqlcom = sqlcom + " from daftar_supplier"
            sqlcom = sqlcom + " where id = " & Me.tb_id_supplier.Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_nama_supplier.Text = reader.Item("name").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub clearsample()
        Me.tb_id_sample.Text = 0
        Me.lbl_nama_sample.Text = "-----"
        Me.link_popup_sample.Visible = True
    End Sub

    Sub bindsample()
        Try
            sqlcom = "select id, name, id_satuan"
            sqlcom = sqlcom + " from sample"
            sqlcom = sqlcom + " where id = " & Me.tb_id_sample.Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_nama_sample.Text = reader.Item("name").ToString

                Dim readersatuan As SqlClient.SqlDataReader
                sqlcom = "select name from measurement_unit where id = " & reader.Item("id_satuan").ToString
                readersatuan = connection.koneksi.SelectRecord(sqlcom)
                readersatuan.Read()
                If readersatuan.HasRows Then
                    Me.lbl_satuan.Text = readersatuan.Item("name").ToString
                End If
                readersatuan.Close()
                connection.koneksi.CloseKoneksi()
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub checkdata()
        Try
            sqlcom = "select  '*'"
            sqlcom = sqlcom + " from penerimaan_sample"
            sqlcom = sqlcom + " inner join sample on sample.id = penerimaan_sample.id_sample"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = sample.id_satuan"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = penerimaan_sample.id_supplier"
            sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
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

            sqlcom = "select  penerimaan_sample.id, convert(char, penerimaan_sample.tanggal, 103) as tanggal, penerimaan_sample.id_supplier,"
            sqlcom = sqlcom + " isnull(penerimaan_sample.qty,0) as qty, penerimaan_sample.id_transaction_period, penerimaan_sample.id_sample,"
            sqlcom = sqlcom + " penerimaan_sample.id_currency, isnull(penerimaan_sample.harga,0) as harga, isnull(penerimaan_sample.kurs,0) as kurs,"
            sqlcom = sqlcom + " sample.name as nama_sample, measurement_unit.name as nama_satuan, daftar_supplier.name as nama_supplier"
            sqlcom = sqlcom + " from penerimaan_sample"
            sqlcom = sqlcom + " inner join sample on sample.id = penerimaan_sample.id_sample"
            sqlcom = sqlcom + " inner join measurement_unit on measurement_unit.id = sample.id_satuan"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = penerimaan_sample.id_supplier"
            sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(sample.name) like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " order by penerimaan_sample.tanggal"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "penerimaan_sample")
                Me.dg_data.DataSource = ds.Tables("penerimaan_sample").DefaultView

                If ds.Tables("penerimaan_sample").Rows.Count > 0 Then
                    If ds.Tables("penerimaan_sample").Rows.Count > 8 Then
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

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id from currency order by id"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataTextField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataBind()
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_mata_uang"), Label).Text
                    Next
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
            Me.clearsupplier()
            Me.clearsample()
            Me.tb_tahun.Text = Year(Now.Date)
            Me.bindperiode_transaksi()
            Me.bindmata_uang()
            Me.tb_kurs.Text = "1.00"
            Me.tb_harga.Text = "0.00"
            Me.loadgrid()
            Me.tb_id_supplier.Attributes.Add("style", "display: none;")
            Me.tb_id_sample.Attributes.Add("style", "display: none;")
            Me.link_refresh_supplier.Attributes.Add("style", "display: none;")
            Me.link_refresh_sample.Attributes.Add("style", "display: none;")
            Me.link_popup_supplier.Attributes.Add("onclick", "popup_supplier('" & Me.tb_id_supplier.ClientID & "','" & Me.link_refresh_supplier.UniqueID & "')")
            Me.link_popup_sample.Attributes.Add("onclick", "popup_sample('" & Me.tb_id_sample.ClientID & "','" & Me.link_refresh_sample.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_tanggal.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terlebih dahulu"
            ElseIf Me.tb_id_supplier.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama supplier terlebih dahulu"
            ElseIf Me.tb_id_sample.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama sample terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_kurs.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi kurs terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_qty.Text) Or Me.tb_qty.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi qty terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_harga.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi harga terlebih dahulu"
            Else
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(id),0) + 1 as vmax from penerimaan_sample"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vtgl As String = Me.tb_tanggal.Text.Substring(3, 2) & "/" & Me.tb_tanggal.Text.Substring(0, 2) & "/" & Me.tb_tanggal.Text.Substring(6, 4)

                sqlcom = "insert into penerimaan_sample (id, tanggal, id_supplier, qty, id_transaction_period, id_sample, id_currency, harga, kurs)"
                sqlcom = sqlcom + " values(" & vmax & ",'" & vtgl & "'," & Me.tb_id_supplier.Text & "," & Decimal.ToDouble(Me.tb_qty.Text)
                sqlcom = sqlcom + "," & Me.dd_bulan.SelectedValue & "," & Me.tb_id_sample.Text & ",'" & Me.dd_mata_uang.SelectedValue & "'"
                sqlcom = sqlcom + "," & Decimal.ToDouble(Me.tb_harga.Text) & "," & Decimal.ToDouble(Me.tb_kurs.Text) & ")"
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
                    sqlcom = "delete penerimaan_sample"
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah dihapus"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.lbl_msg.Text = "Data tersebut masih digunakan di form lain"
            Else
                Me.lbl_msg.Text = ex.Message
            End If
        End Try
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    Dim vtgl As String = CType(Me.dg_data.Items(x).FindControl("tb_tanggal"), TextBox).Text
                    vtgl = vtgl.Substring(3, 2) & "/" & vtgl.Substring(0, 2) & "/" & vtgl.Substring(6, 4)

                    sqlcom = "update penerimaan_sample"
                    sqlcom = sqlcom + " set tanggal = '" & vtgl & "',"
                    sqlcom = sqlcom + " qty = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_qty"), TextBox).Text) & ","
                    sqlcom = sqlcom + " id_currency = '" & CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue & "',"
                    sqlcom = sqlcom + " kurs = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kurs"), TextBox).Text) & ","
                    sqlcom = sqlcom + " harga = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_harga"), TextBox).Text)
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        'Me.bindperiode_transaksi()
        Me.dg_data.CurrentPageIndex = 0
        Me.loadgrid()
    End Sub

    Protected Sub link_refresh_supplier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_supplier.Click
        Me.bindsupplier()
    End Sub

    Protected Sub link_refresh_sample_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_sample.Click
        Me.bindsample()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_kurs_idr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_idr.Click
        Me.tb_kurs.Text = "1.00"
    End Sub

    Protected Sub btn_kurs_usd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_kurs_usd.Click
        Me.tb_kurs.Text = tradingClass.KursBulanan("[kurs_bulanan]", Me.dd_bulan.SelectedValue)
    End Sub
End Class
