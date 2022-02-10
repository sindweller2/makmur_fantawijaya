Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_daftar_supplier_produk
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()
    'Daniel

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub checkdata()
        sqlcom = "select *"
        sqlcom = sqlcom + " from daftar_supplier"
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

            sqlcom = "select daftar_supplier.id, daftar_supplier.name, daftar_supplier.alamat,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when daftar_supplier.status = 1 then 'Aktif'"
            sqlcom = sqlcom + " when daftar_supplier.status = 0 then 'Tidak aktif'"
            sqlcom = sqlcom + " end as status"
            sqlcom = sqlcom + " from daftar_supplier"
            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " where upper(daftar_supplier.name) like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " order by daftar_supplier.name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "daftar_supplier")
                Me.dg_data.DataSource = ds.Tables("daftar_supplier").DefaultView

                If ds.Tables("daftar_supplier").Rows.Count > 0 Then
                    If ds.Tables("daftar_supplier").Rows.Count > 8 Then
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
                    Me.btn_delete.Visible = True
                Else
                    Me.dg_data.Visible = False
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
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        Response.Redirect("~/detil_supplier.aspx")
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete daftar_supplier"
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
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

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_data.ItemCommand
        If e.CommandName = "LinkItem" Then
            Response.Redirect("~/detil_supplier.aspx?vid_supplier=" & CType(e.Item.FindControl("lbl_id"), Label).Text)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_generate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_generate.Click
        Try
            sqlcom = "select id, name, akun_hutang_dagang from daftar_supplier where (akun_hutang_dagang = '') or (akun_hutang_dagang is null) order by 1"

            reader = connection.koneksi.SelectRecord(sqlcom)
            Dim no As Integer = 0
            Dim vhutang_dagang As String = Nothing
            Do While reader.Read
                no = Me.tradingClass.SupplierMaxAccountNo() + 1

                vhutang_dagang = "21.02.02." & no
                'Daniel
                sqlcom = "insert into coa_list(AccountNo, LAccount, ParentAcc, LParent, EngName, InaName, AccType, ShareAcc, IsControl,"
                sqlcom = sqlcom + " CostType, OffsetAcc, MinAmount, MaxAmount, DefAmount, CDesc, Remark, IsActive, Position, CurrType, CurrencyCode, AddAdjust)"
                sqlcom = sqlcom + " values('" & vhutang_dagang & "','5', '21.02.02', 4, NULL,'HUTANG DAGANG LUAR NEGERI " & reader.Item("name").ToString & "', 21, NULL, 'N'"
                sqlcom = sqlcom + ", NULL, NULL, 0, 0, 0, NULL, NULL, 'Y', 'C', 'L', 'NULL', NULL)"
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = "update daftar_supplier"
                sqlcom = sqlcom + " set akun_hutang_dagang = '" & vhutang_dagang & "'"
                sqlcom = sqlcom + " where id = " & reader.Item("id").ToString
                connection.koneksi.UpdateRecord(sqlcom)
            Loop
            reader.Close()
            connection.koneksi.CloseKoneksi()
            Me.lbl_msg.Text = "Proses sudah selesai"
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
