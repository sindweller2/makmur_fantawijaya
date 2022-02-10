Imports System.Configuration
Imports System.Data

Partial Class Forms_Popup_popup_penjualan
    Inherits System.Web.UI.UserControl

    Public Event CloseClicked(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event PenjualanClicked(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Public Property no_so() As Integer
        Get
            Dim o As Object = ViewState("no_so")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("no_so") = value
        End Set
    End Property

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select no, so_no_text, convert(char, tanggal, 103) as tanggal"
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " where id_transaction_period in (select id from transaction_period where tahun = " & Me.tb_tahun.Text & ")"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(so_no_text) like upper('%" & Me.tb_search.Text & "%')"
            End If
            sqlcom = sqlcom + " order by so_no_text"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "sales_order")
                Me.dg_data.DataSource = ds.Tables("sales_order").DefaultView

                If ds.Tables("sales_order").Rows.Count > 0 Then
                    If ds.Tables("sales_order").Rows.Count > 10 Then
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
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub tb_search_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_search.TextChanged
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        If String.IsNullOrEmpty(Me.tb_tahun.Text) Then
            Me.lbl_msg.Text = "Silahkan mengisi tahun terlebih dahulu"
        Else
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        RaiseEvent CloseClicked(sender, e)
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Me.no_so = CType(e.Item.FindControl("lbl_id"), Label).Text
            RaiseEvent PenjualanClicked(source, e)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.tb_tahun.Text = Year(Now.Date)
            Me.loadgrid()
        End If
    End Sub
End Class
