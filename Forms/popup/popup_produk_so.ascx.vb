Imports System.Configuration
Imports System.Data

Partial Class Forms_Popup_popup_produk_so
    Inherits System.Web.UI.UserControl

    Private ReadOnly Property TargetControlID() As String
        Get
            Dim o As Object = Request.QueryString("tcid1")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Private ReadOnly Property RefreshControlID() As String
        Get
            Dim o As Object = Request.QueryString("tcid2")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Private ReadOnly Property vid_so() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_so")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Private ReadOnly Property vid_do() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_do")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Public Property id_produk() As Integer
        Get
            Dim o As Object = ViewState("id_produk")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_produk") = value
        End Set
    End Property

    Public Property vpilih_semua() As Integer
        Get
            Dim o As Object = ViewState("vpilih_semua")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vpilih_semua") = value
        End Set
    End Property

    Sub bindso()
        sqlcom = "select so_no_text from sales_order where no = " & Me.vid_so
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_no_so.Text = reader.Item("so_no_text").ToString
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select sales_order_detail.seq, sales_order_detail.id_product, sales_order_detail.nama_product, sales_order_detail.qty_pending,"
            sqlcom = sqlcom + " rtrim(convert(char, product_item.qty_conversion)) + ' ' + satuan_packaging_produk.name + '/' + satuan_produk.name as packaging"
            sqlcom = sqlcom + " from sales_order_detail"
            sqlcom = sqlcom + " inner join product_item on product_item.id = sales_order_detail.id_product"
            sqlcom = sqlcom + " inner join measurement_unit satuan_produk on satuan_produk.id = product_item.id_measurement"
            sqlcom = sqlcom + " inner join measurement_unit satuan_packaging_produk on satuan_packaging_produk.id = product_item.id_measurement_conversion"
            sqlcom = sqlcom + " where no_sales_order = " & Me.vid_so
            sqlcom = sqlcom + " and sales_order_detail.qty_pending <> 0"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and upper(sales_order_detail.nama_product) like upper('%" & Me.tb_search.Text & "%')"
            End If
            sqlcom = sqlcom + " order by sales_order_detail.nama_product"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "sales_order_detail")
                Me.dg_data.DataSource = ds.Tables("sales_order_detail").DefaultView

                If ds.Tables("sales_order_detail").Rows.Count > 0 Then
                    If ds.Tables("sales_order_detail").Rows.Count > 10 Then
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
                    Me.tbl_search.Visible = True
                    Me.btn_save.Visible = True
                    Me.btn_select_all.Visible = True
                Else
                    Me.dg_data.Visible = False
                    Me.tbl_search.Visible = False
                    Me.btn_save.Visible = False
                    Me.btn_select_all.Visible = False
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

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.vpilih_semua = 0
            Me.bindso()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_select_all_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_select_all.Click
        If Me.vpilih_semua = 0 Then
            Me.vpilih_semua = 1
            Me.btn_select_all.Text = "Batal semua"
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True
            Next
        Else
            Me.vpilih_semua = 0
            Me.btn_select_all.Text = "Pilih semua"
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = False
            Next
        End If

    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "insert into stock_delivery_order_detil(no_delivery_order, id_product, qty, seq_jual_detil)"
                    sqlcom = sqlcom + " values(" & Me.vid_do & "," & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    sqlcom = sqlcom + "," & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("lbl_qty_pending"), Label).Text)
                    sqlcom = sqlcom + "," & CType(Me.dg_data.Items(x).FindControl("lbl_seq"), Label).Text & ")"
                    'me.lbl_msg.Text = sqlcom
                    connection.koneksi.InsertRecord(sqlcom)
                    

                    sqlcom = "update sales_order_detail"
                    sqlcom = sqlcom + " set qty_pending = qty_pending - " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("lbl_qty_pending"), Label).Text)
                    sqlcom = sqlcom + " where no_sales_order = " & Me.vid_so
                    sqlcom = sqlcom + " and id_product = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                End If
            Next

            Dim vscript As String = ""
            vscript = vscript + "<script language=""javascript"" type=""text/javascript"">"
            vscript = vscript + "window.opener.document.getElementById('" & Me.TargetControlID & "').innerText = " & id_produk & vbCrLf
            vscript = vscript + "window.opener.__doPostBack('" & Me.RefreshControlID & "', '');"
            vscript = vscript + "window.close();"
            vscript = vscript + "</" & "script" & ">"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Dim vscript As String = ""
        vscript = vscript + "<script language=""javascript"" type=""text/javascript"">"
        vscript = vscript + "window.close();"
        vscript = vscript + "</" & "script" & ">"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "xyz", vscript, False)
    End Sub
End Class
