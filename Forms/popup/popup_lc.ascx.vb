Imports System.Configuration
Imports System.Data

Partial Class Forms_Popup_popup_lc
    Inherits System.Web.UI.UserControl

    Public Event CloseClicked(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event LcClicked(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Public Property seq_lc() As Integer
        Get
            Dim o As Object = ViewState("seq_lc")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("seq_lc") = value
        End Set
    End Property

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select lc.seq, lc.no_lc, convert(char, lc.tanggal_lc, 103) as tanggal_lc, purchase_order.po_no_text"
            sqlcom = sqlcom + " from lc"
            sqlcom = sqlcom + " inner join purchase_order on purchase_order.no = lc.no_po"
            sqlcom = sqlcom + " where lc.no_lc is not null" 
            sqlcom = sqlcom + " and lc.seq not in (select seq_lc from entry_dokumen_impor where seq_lc is not null)"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(lc.no_lc) like upper('%" & Me.tb_search.Text & "%')"
            End If
            sqlcom = sqlcom + " order by lc.seq"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "lc")
                Me.dg_data.DataSource = ds.Tables("lc").DefaultView

                If ds.Tables("lc").Rows.Count > 0 Then
                    If ds.Tables("lc").Rows.Count > 10 Then
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
            Me.seq_lc = CType(e.Item.FindControl("lbl_seq"), Label).Text
            RaiseEvent LcClicked(source, e)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class
