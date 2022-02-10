Imports System.Configuration
Imports System.Data

Partial Class Forms_Popup_popup_so_collection
    Inherits System.Web.UI.UserControl

    Public Event CloseClicked(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SOClicked(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Public Property id_no_so() As Integer
        Get
            Dim o As Object = ViewState("id_no_so")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_no_so") = value
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

            sqlcom = "select sales_order.no, sales_order.so_no_text, convert(char, sales_order.tanggal, 103) as tanggal, daftar_customer.name as nama_customer,"
            sqlcom = sqlcom + " sales_order.id_currency as mata_uang,"
            sqlcom = sqlcom + " sum(sales_order_detail.qty * (sales_order_detail.harga_jual - (sales_order_detail.harga_jual * sales_order_detail.discount /100))) + "
            sqlcom = sqlcom + " sum(sales_order_detail.qty * (sales_order_detail.harga_jual - (sales_order_detail.harga_jual * sales_order_detail.discount /100)) * sales_order.ppn / 100)"
            sqlcom = sqlcom + " as jumlah_nilai"
            sqlcom = sqlcom + " from sales_order"
            sqlcom = sqlcom + " inner join sales_order_detail on sales_order_detail.no_sales_order = sales_order.no"
            sqlcom = sqlcom + " inner join daftar_customer on daftar_customer.id = sales_order.id_customer"
            sqlcom = sqlcom + " where tgl_invoice is not null"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(so_no_text) like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " group by sales_order.no, sales_order.so_no_text, sales_order.tanggal, daftar_customer.name, sales_order.id_currency"
            sqlcom = sqlcom + " order by no"

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

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        RaiseEvent CloseClicked(sender, e)
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Me.id_no_so = CType(e.Item.FindControl("lbl_id"), Label).Text
            RaiseEvent SOClicked(source, e)
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
