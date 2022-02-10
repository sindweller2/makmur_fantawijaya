Imports System.Configuration
Imports System.Data

Partial Class Forms_Popup_popup_po_all
    Inherits System.Web.UI.UserControl

    Public Event CloseClicked(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event POClicked(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Public Property no_po() As Integer
        Get
            Dim o As Object = ViewState("no_po")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("no_po") = value
        End Set
    End Property

    Sub checkdata()
        sqlcom = "select sales_order.no, sales_order.so_no_text, convert(char, sales_order.tanggal, 103) as tanggal"
        sqlcom = sqlcom + " from sales_order"
        sqlcom = sqlcom + " where tgl_invoice is not null"
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

            sqlcom = "select purchase_order.no, purchase_order.po_no_text, convert(char, purchase_order.tanggal, 103) as tanggal,"
            sqlcom = sqlcom + " daftar_supplier.name as nama_supplier"
            sqlcom = sqlcom + " from purchase_order"
            sqlcom = sqlcom + " inner join daftar_supplier on daftar_supplier.id = purchase_order.id_supplier"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(purchase_order.po_no_text) like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " order by purchase_order.no"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "purchase_order")
                Me.dg_data.DataSource = ds.Tables("purchase_order").DefaultView

                If ds.Tables("purchase_order").Rows.Count > 0 Then
                    If ds.Tables("purchase_order").Rows.Count > 8 Then
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
                Else
                    Me.dg_data.Visible = False
                End If
            End Using
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        RaiseEvent CloseClicked(sender, e)
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Me.no_po = CType(e.Item.FindControl("lbl_id"), Label).Text
            RaiseEvent POClicked(source, e)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub
End Class
