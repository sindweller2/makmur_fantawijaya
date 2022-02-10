Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Collection_daftar_pengeluaran_petty_cash
    Inherits System.Web.UI.UserControl

    Private ReadOnly Property vbulan() As Integer
        Get
            Dim o As Object = Request.QueryString("vbulan")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vtahun() As Integer
        Get
            Dim o As Object = Request.QueryString("vtahun")
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

        If Me.vbulan <> 0 Then
            sqlcom = "select id from transaction_period where bulan = " & Me.vbulan & " and tahun=" & Me.vtahun
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.dd_bulan.SelectedValue = reader.Item("id").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        End If
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

    Sub loadgrid()
        Try

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id, convert(char, tanggal, 103) as tanggal, id_currency,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when is_cash = 'T' then 'Tunai'"
            sqlcom = sqlcom + " when is_cash = 'N' then 'Non tunai'"
            sqlcom = sqlcom + " end as is_cash,"
            sqlcom = sqlcom + " nilai, keterangan, diminta_oleh,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when status = 'P' then 'Pending'"
            sqlcom = sqlcom + " when status = 'O' then 'Ok'"
            sqlcom = sqlcom + " end as status"
            sqlcom = sqlcom + " from permintaan_petty_cash"
            sqlcom = sqlcom + " where id_transaction_period = " & Me.dd_bulan.SelectedValue
            sqlcom = sqlcom + " and is_submit = 'S'"
            sqlcom = sqlcom + " order by id"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "permintaan_petty_cash")
                Me.dg_data.DataSource = ds.Tables("permintaan_petty_cash").DefaultView

                If ds.Tables("permintaan_petty_cash").Rows.Count > 0 Then
                    If ds.Tables("permintaan_petty_cash").Rows.Count > 10 Then
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

                Else
                    Me.dg_data.Visible = False
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
            Me.id_user = HttpContext.Current.Session("UserID")
            If Me.vtahun = 0 Then
                Me.tb_tahun.Text = Now.Year
            Else
                Me.tb_tahun.Text = Me.vtahun
            End If
            Me.bindperiode_transaksi()
            Me.bindbulan()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.bindperiode_transaksi()
    End Sub

    Protected Sub dd_bulan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_bulan.SelectedIndexChanged
        Me.bindbulan()
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Response.Redirect("~/detil_pengeluaran_petty_cash.aspx?vid=" & CType(e.Item.FindControl("lbl_id"), LinkButton).Text & "&vtahun=" & Me.tb_tahun.Text & "&vbulan=" & Me.bulan)
        End If
    End Sub

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        Response.Redirect("~/detil_pengeluaran_petty_cash.aspx?vtahun=" & Me.tb_tahun.Text & "&vbulan=" & Me.bulan)
    End Sub
End Class
