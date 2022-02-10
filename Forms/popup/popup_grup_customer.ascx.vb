Imports System.Configuration
Imports System.Data

Partial Class Forms_Popup_popup_grup_customer
    Inherits System.Web.UI.UserControl

    Public Event CloseClicked(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event GrupCustomerClicked(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Public Property id_group_customer() As Integer
        Get
            Dim o As Object = ViewState("id_group_customer")
            If Not o Is Nothing Then Return CInt(o) Else Return Nothing
        End Get
        Set(ByVal value As Integer)
            ViewState("id_group_customer") = value
        End Set
    End Property

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id, name"
            sqlcom = sqlcom + " from daftar_group_customer"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " where upper(name) like upper('%" & Me.tb_search.Text & "%')"
            End If
            sqlcom = sqlcom + " order by name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "daftar_group_customer")
                Me.dg_data.DataSource = ds.Tables("daftar_group_customer").DefaultView

                If ds.Tables("daftar_group_customer").Rows.Count > 0 Then
                    If ds.Tables("daftar_group_customer").Rows.Count > 10 Then
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
        Me.loadgrid()
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        RaiseEvent CloseClicked(sender, e)
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Me.id_group_customer = CType(e.Item.FindControl("lbl_code"), Label).Text
            RaiseEvent GrupCustomerClicked(source, e)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class