Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Keuangan_pembayaran_biaya_lc
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

    Public Property bulan() As Integer
        Get
            Dim o As Object = ViewState("bulan")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("bulan") = value
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

        sqlcom = "select id from transaction_period where bulan = " & Me.bulan & " and tahun=" & Me.tb_tahun.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.dd_bulan.SelectedValue = reader.Item("id").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()

    End Sub

    Sub bindbulan()
        sqlcom = "select bulan from transaction_period where id = " & Me.dd_bulan.SelectedValue
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.bulan = reader.Item("bulan").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub checkdata()
        sqlcom = "select *"
        sqlcom = sqlcom + " from lc"
        sqlcom = sqlcom + " where lc.id_transaction_period = " & Me.dd_bulan.SelectedValue
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

            sqlcom = "select lc.seq, purchase_order.no, purchase_order.po_no_text, convert(char, purchase_order.tanggal, 103) as tanggal,"
            sqlcom = sqlcom + " lc.seq as seq_lc, lc.no_lc, convert(char, lc.tanggal_lc, 103) as tanggal_lc,"
            sqlcom = sqlcom + " purchase_order.id_currency, isnull(lc.nilai_lc,0) as total_nilai,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when lc.is_lc_lunas = 'B' then 'Belum lunas'"
            sqlcom = sqlcom + " when lc.is_lc_lunas = 'S' then 'Sudah lunas'"
            sqlcom = sqlcom + " end as status_lc, lc.id_bank_biaya_lc,"
            sqlcom = sqlcom + " isnull(lc.biaya_komisi_bank,0) as biaya_komisi_bank, isnull(lc.biaya_ongkos_kawat,0) as biaya_ongkos_kawat,"
            sqlcom = sqlcom + " isnull(lc.biaya_porto_materai,0) as biaya_porto_materai, isnull(lc.biaya_courier,0) as biaya_courier,"
            sqlcom = sqlcom + " isnull(lc.biaya_lc_amendment,0) as biaya_lc_amendment,"
            sqlcom = sqlcom + " convert(char, lc.tgl_bayar_biaya_lc, 103) as tgl_bayar_biaya_lc"
            sqlcom = sqlcom + " from lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = lc.no_po"
            sqlcom = sqlcom + " where lc.id_transaction_period = " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " and purchase_order.is_lc = 'True'"
            sqlcom = sqlcom + " and lc.no_lc is not null"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                If Me.dd_pilihan.SelectedValue = "0" Then
                    sqlcom = sqlcom + " and upper(purchase_order.po_no_text) like upper('%" & Me.tb_search.Text & "%')"
                ElseIf Me.dd_pilihan.SelectedValue = "1" Then
                    sqlcom = sqlcom + " and upper(purchase_order.no_lc) like upper('%" & Me.tb_search.Text & "%')"
                End If
            End If

            sqlcom = sqlcom + " order by purchase_order.no"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "purchase_order")
                Me.dg_data.DataSource = ds.Tables("purchase_order").DefaultView

                If ds.Tables("purchase_order").Rows.Count > 0 Then
                    If ds.Tables("purchase_order").Rows.Count > 10 Then
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

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select id, name from bank_account order by name"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataTextField = "name"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).Items.Add(New ListItem("---Kas/Bank---", 0))

                        If CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text = "0" or string.isnullOrEmpty(CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text) Then
                            CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue = "0"
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = True
                        Else
                            CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_bank"), Label).Text
                            CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Enabled = False
                        End If
                    Next
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
            If Me.vtahun = 0 Then
                Me.tb_tahun.Text = Now.Year
            Else
                Me.tb_tahun.Text = Me.vtahun
            End If

            If Me.vbulan = 0 Then
                Me.bulan = Now.Month
            Else
                Me.bulan = Me.vbulan
            End If

            Me.bindperiode_transaksi()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.bindbulan()
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Or e.CommandName = "LinkLC" Then
            Response.Redirect("~/detil_pembayaran_lc.aspx?vno_po=" & CType(e.Item.FindControl("lbl_no"), Label).Text & "&vtahun=" & Me.tb_tahun.Text & "&vbulan=" & Me.bulan)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update lc"
                    sqlcom = sqlcom + " set biaya_komisi_bank = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_komisi_bank"), TextBox).Text) & ","
                    sqlcom = sqlcom + " biaya_ongkos_kawat = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_ongkos_kawat"), TextBox).Text) & ","
                    sqlcom = sqlcom + " biaya_porto_materai = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_porto_materai"), TextBox).Text) & ","
                    sqlcom = sqlcom + " biaya_courier = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_biaya_courier"), TextBox).Text) & ","
                    sqlcom = sqlcom + " biaya_lc_amendment = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_biaya_lc_amendment"), TextBox).Text) & ","
                    sqlcom = sqlcom + " id_bank_biaya_lc = " & CType(Me.dg_data.Items(x).FindControl("dd_bank"), DropDownList).SelectedValue & ","
                    sqlcom = sqlcom + " tgl_bayar_biaya_lc = tanggal_lc "
                    sqlcom = sqlcom + " where seq = " & CType(Me.dg_data.Items(x).FindControl("lbl_no"), Label).Text
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
