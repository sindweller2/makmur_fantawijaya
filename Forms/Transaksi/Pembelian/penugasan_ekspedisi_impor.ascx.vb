Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Pembelian_penugasan_ekspedisi_impor
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Public Property vid_penugasan() As Integer
        Get
            Dim o As Object = ViewState("vid_penugasan")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vid_penugasan") = value
        End Set
    End Property

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

    Sub clearekspedisi()
        Me.tb_id_ekspedisi.Text = 0
        Me.lbl_nama_ekspedisi.Text = "------"
        Me.link_popup_ekspedisi.Visible = True
    End Sub

    Sub bindekspedisi()
        sqlcom = "select name from daftar_expedition where id = " & Me.tb_id_ekspedisi.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_ekspedisi.Text = reader.Item("name").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub cleardokumen_impor()
        Me.tb_id_dokumen_impor.Text = 0
        Me.lbl_no_dokumen_impor.Text = "------"
        Me.link_popup_dokumen_impor.Visible = True
    End Sub

    Sub binddokumen_impor()
        Me.lbl_no_dokumen_impor.Text = Me.tb_id_dokumen_impor.Text
    End Sub

    Sub checkdata()
        sqlcom = "select *"
        sqlcom = sqlcom + " from penugasan_ekspedisi_impor"
        sqlcom = sqlcom + " where penugasan_ekspedisi_impor.id_transaction_period = " & Me.dd_bulan.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.tbl_search.Visible = True
        Else
            Me.tbl_search.Visible = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select daftar_expedition.name as nama_ekspedisi, penugasan_ekspedisi_impor.seq, penugasan_ekspedisi_impor.id_expedition,"
            sqlcom = sqlcom + " convert(char, penugasan_ekspedisi_impor.tanggal, 103) as tanggal, penugasan_ekspedisi_impor.no_aju"
            sqlcom = sqlcom + " from penugasan_ekspedisi_impor"
            sqlcom = sqlcom + " inner join daftar_expedition on daftar_expedition.id = penugasan_ekspedisi_impor.id_expedition"
            sqlcom = sqlcom + " where penugasan_ekspedisi_impor.id_transaction_period = " & Me.dd_bulan.SelectedValue


            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else

                sqlcom = sqlcom + " and upper(penugasan_ekspedisi_impor.no_aju) like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " order by penugasan_ekspedisi_impor.seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "penugasan_ekspedisi_impor")
                Me.dg_data.DataSource = ds.Tables("penugasan_ekspedisi_impor").DefaultView

                If ds.Tables("penugasan_ekspedisi_impor").Rows.Count > 0 Then
                    If ds.Tables("penugasan_ekspedisi_impor").Rows.Count > 6 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 6
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
                    Me.tbl_dokumen.Visible = False
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
            Me.tb_tahun.Text = Now.Year
            Me.clearekspedisi()
            Me.cleardokumen_impor()
            Me.bindperiode_transaksi()
            Me.loadgrid()
            Me.tbl_dokumen.Visible = False
            Me.tb_id_ekspedisi.Attributes.Add("style", "display: none;")
            Me.tb_id_dokumen_impor.Attributes.Add("style", "display: none;")
            Me.link_refresh_ekspedisi.Attributes.Add("style", "display: none;")
            Me.link_refresh_dokumen_impor.Attributes.Add("style", "display: none;")
            Me.link_popup_ekspedisi.Attributes.Add("onclick", "popup_ekspedisi_impor('" & Me.tb_id_ekspedisi.ClientID & "','" & Me.link_refresh_ekspedisi.UniqueID & "')")
            Me.link_popup_dokumen_impor.Attributes.Add("onclick", "popup_dokumen_impor('" & Me.tb_id_dokumen_impor.ClientID & "','" & Me.link_refresh_dokumen_impor.UniqueID & "')")
        End If
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.loadgrid()
        Me.loadgrid_dokumen()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then            
            sqlcom = "select name from daftar_expedition where id = " & CType(e.Item.FindControl("lbl_id_expedition"), Label).Text
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_nama_ekspedisi_dokumen.Text = reader.Item("name").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
            Me.tbl_dokumen.Visible = True

            Me.vid_penugasan = CType(e.Item.FindControl("lbl_seq"), Label).Text
            Me.loadgrid_dokumen()
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If Me.tb_id_ekspedisi.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nama ekspedisi terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_tanggal.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi tanggal terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_no_aju.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi no. aju ekspedisi terlebih dahulu"
            Else

                Dim vbulan As Integer = 0
                sqlcom = "select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vbulan = reader.Item("bulan").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vmax As String = ""
                sqlcom = "select isnull(max(convert(int, right(seq, 5))),0) + 1 as vseq"
                sqlcom = sqlcom + " from penugasan_ekspedisi_impor"
                sqlcom = sqlcom + " where convert(int, substring(convert(char, seq), 3,2)) = " & vbulan
                sqlcom = sqlcom + " and convert(int, left(seq, 2)) = " & Right(Me.tb_tahun.Text, 2)
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = Right(Me.tb_tahun.Text, 2) & vbulan.ToString.PadLeft(2, "0") & reader.Item("vseq").ToString.PadLeft(5, "0")
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                Dim vtgl As String = Me.tb_tanggal.Text.Substring(3, 2) & "/" & Me.tb_tanggal.Text.Substring(0, 2) & "/" & Me.tb_tanggal.Text.Substring(6, 4)

                sqlcom = "insert into penugasan_ekspedisi_impor(seq, id_transaction_period, id_expedition, tanggal, no_aju)"
                sqlcom = sqlcom + " values(" & vmax & "," & Me.dd_bulan.SelectedValue & "," & Me.tb_id_ekspedisi.Text & ",'" & vtgl & "','" & Me.tb_no_aju.Text & "')"
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
                    sqlcom = "delete penugasan_ekspedisi_impor"
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
                    sqlcom = "update penugasan_ekspedisi_impor"
                    sqlcom = sqlcom + " set no_aju = '" & CType(Me.dg_data.Items(x).FindControl("tb_no_aju"), TextBox).Text & "'"
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
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

    Protected Sub link_refresh_ekspedisi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_ekspedisi.Click
        Me.bindekspedisi()
    End Sub

    Protected Sub link_refresh_dokumen_impor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_dokumen_impor.Click
        Me.binddokumen_impor()
    End Sub

    Sub loadgrid_dokumen()
        Try

            Me.cleardokumen_impor()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select entry_dokumen_impor.seq, entry_dokumen_impor.no_po, entry_dokumen_impor.bl_no, entry_dokumen_impor.packing_list_no,"
            sqlcom = sqlcom + " entry_dokumen_impor.invoice_no, penugasan_ekspedisi_impor_detil.seq_entry_dokumen_impor"
            sqlcom = sqlcom + " from penugasan_ekspedisi_impor_detil"
            sqlcom = sqlcom + " inner join entry_dokumen_impor on entry_dokumen_impor.seq = penugasan_ekspedisi_impor_detil.seq_entry_dokumen_impor"
            sqlcom = sqlcom + " where seq_penugasan = " & Me.vid_penugasan
            sqlcom = sqlcom + " order by entry_dokumen_impor.seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "penugasan_ekspedisi_impor_detil")
                Me.dg_data_dokumen.DataSource = ds.Tables("penugasan_ekspedisi_impor_detil").DefaultView

                If ds.Tables("penugasan_ekspedisi_impor_detil").Rows.Count > 0 Then
                    If ds.Tables("penugasan_ekspedisi_impor_detil").Rows.Count > 10 Then
                        Me.dg_data_dokumen.AllowPaging = True
                        Me.dg_data_dokumen.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data_dokumen.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data_dokumen.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data_dokumen.PageSize = 10
                    Else
                        Me.dg_data_dokumen.AllowPaging = False
                    End If
                    Me.dg_data_dokumen.DataBind()
                    Me.dg_data_dokumen.Visible = True
                    Me.btn_delete_dokumen.Visible = True
                Else
                    Me.dg_data_dokumen.Visible = False
                    Me.btn_delete_dokumen.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_close_dokumen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close_dokumen.Click
        Me.tbl_dokumen.Visible = False
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Try
            If Me.tb_id_dokumen_impor.Text = 0 Then
                Me.lbl_msg.Text = "Silahkan mengisi nomor dokumen terlebih dahulu"
            Else
                sqlcom = "insert into penugasan_ekspedisi_impor_detil(seq_penugasan, seq_entry_dokumen_impor)"
                sqlcom = sqlcom + " values(" & Me.vid_penugasan & "," & Me.tb_id_dokumen_impor.Text & ")"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loadgrid_dokumen()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_delete_dokumen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete_dokumen.Click
        Try
            For x As Integer = 0 To Me.dg_data_dokumen.Items.Count - 1
                If CType(Me.dg_data_dokumen.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete penugasan_ekspedisi_impor_detil"
                    sqlcom = sqlcom + " where seq_penugasan = " & Me.vid_penugasan
                    sqlcom = sqlcom + " and seq_entry_dokumen_impor = " & CType(Me.dg_data_dokumen.Items(x).FindControl("lbl_seq_dokumen"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah dihapus"
                End If
            Next
            Me.loadgrid_dokumen()
        Catch ex As Exception
            If Err.Number = 5 Then
                Me.lbl_msg.Text = "Data masih digunakan di form lain"
            Else
                Me.lbl_msg.Text = ex.Message
            End If
        End Try
    End Sub

    
End Class
