Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_customer_daftar_customer
    Inherits System.Web.UI.UserControl

    'Daniel
    Public tradingClass As New tradingClass()
    'Daniel

    Private ReadOnly Property vpaging() As String
        Get
            Dim o As Object = Request.QueryString("vpaging")
            If String.IsNullOrEmpty(o) = False Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub checkdata()
        sqlcom = "select *"
        sqlcom = sqlcom + " from daftar_customer"
        sqlcom = sqlcom + " inner join daftar_group_customer on daftar_group_customer.id = daftar_customer.id_group_customer"
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

            sqlcom = "select daftar_customer.id, daftar_customer.name, daftar_group_customer.name as nama_grup,"
            sqlcom = sqlcom + " (select nama_pegawai from user_list where code = daftar_customer.code_sales) as nama_sales,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when daftar_customer.status = 1 then 'Aktif'"
            sqlcom = sqlcom + " when daftar_customer.status = 0 then 'Tidak aktif'"
            sqlcom = sqlcom + " end as status"
            sqlcom = sqlcom + " from daftar_customer"
            sqlcom = sqlcom + " inner join daftar_group_customer on daftar_group_customer.id = daftar_customer.id_group_customer"
            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
                'ElseIf (Me.DropDownListFilter.SelectedIndex = 0) And (Me.tb_search.Text <> Nothing) Then
                '    sqlcom = sqlcom + " where upper(daftar_customer.name) like upper('%" & Me.tb_search.Text & "%')"
                'ElseIf (Me.DropDownListFilter.SelectedIndex = 1) And (Me.tb_search.Text <> Nothing) Then
                '    sqlcom = sqlcom + " where upper(daftar_group_customer.name) like upper('%" & Me.tb_search.Text & "%')"
                'ElseIf (Me.DropDownListFilter.SelectedIndex = 2) And (Me.tb_search.Text <> Nothing) Then
                '    sqlcom = sqlcom + " where upper((select nama_pegawai from user_list where code = daftar_customer.code_sales)) like upper('%" & Me.tb_search.Text & "%')"
            Else
                sqlcom = sqlcom + " where upper(daftar_customer.name) like upper('%" & Me.tb_search.Text & "%') or upper(daftar_group_customer.name) like upper('%" & Me.tb_search.Text & "%') or upper((select nama_pegawai from user_list where code = daftar_customer.code_sales)) like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " order by daftar_customer.name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "daftar_customer")
                Me.dg_data.DataSource = ds.Tables("daftar_customer").DefaultView

                If ds.Tables("daftar_customer").Rows.Count > 0 Then
                    If ds.Tables("daftar_customer").Rows.Count > 8 Then
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
            If Me.vpaging <> 0 then
               Me.dg_data.CurrentPageIndex = Me.vpaging
            End if
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        Response.Redirect("~/detil_customer.aspx")
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete daftar_customer"
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)

                    'hapus akun giro mundur
                    sqlcom = "delete coa_list"
                    sqlcom = sqlcom + " where AccountNo like '11.03.01%' and len(AccountNo) > 8"
                    sqlcom = sqlcom + " and inaname = '" & CType(Me.dg_data.Items(x).FindControl("lbl_name"), LinkButton).Text & "'"
                    connection.koneksi.DeleteRecord(sqlcom)

                    'hapus akun piutang dagang
                    sqlcom = "delete coa_list"
                    sqlcom = sqlcom + " where AccountNo like '11.03.02%' and len(AccountNo) > 8"
                    sqlcom = sqlcom + " and inaname = '" & CType(Me.dg_data.Items(x).FindControl("lbl_name"), LinkButton).Text & "'"
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
            Response.Redirect("~/detil_customer.aspx?vid_customer=" & CType(e.Item.FindControl("lbl_id"), Label).Text & "&vpaging=" & Me.dg_data.CurrentPageIndex)
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_generate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_generate.Click
        Try
            'Daniel
            'sqlcom = "delete coa_list"
            'sqlcom = sqlcom + " where AccountNo like '11.03.01%' and len(AccountNo) > 8"
            'connection.koneksi.DeleteRecord(sqlcom)

            'sqlcom = "delete coa_list"
            'sqlcom = sqlcom + " where AccountNo like '11.03.02%' and len(AccountNo) > 8"
            'connection.koneksi.DeleteRecord(sqlcom)
            'Daniel

            'Daniel
            sqlcom = "select id, name, akun_piutang_giro_mundur,akun_piutang_dagang from daftar_customer where (akun_piutang_giro_mundur = '' and akun_piutang_dagang = '') or (akun_piutang_giro_mundur is null and akun_piutang_dagang is null) order by id"
            'Daniel
            reader = connection.koneksi.SelectRecord(sqlcom)
            Dim no As Integer = 0
            Dim vpiutang_giro_mundur As String = Nothing
            Dim vpiutang_dagang As String = Nothing
            Do While reader.Read
                'Daniel
                no = Me.tradingClass.CustomerMaxAccountNo() + 1
                vpiutang_giro_mundur = "11.03.01." & no
                'Daniel
                sqlcom = "insert into coa_list(AccountNo, LAccount, ParentAcc, LParent, EngName, InaName, AccType, ShareAcc, IsControl,"
                sqlcom = sqlcom + " CostType, OffsetAcc, MinAmount, MaxAmount, DefAmount, CDesc, Remark, IsActive, Position, CurrType, CurrencyCode, AddAdjust)"
                sqlcom = sqlcom + " values('" & vpiutang_giro_mundur & "','5', '11.03.01', 4, NULL,'PIUTANG GIRO MUNDUR " & reader.Item("name").ToString & "', 11, NULL, 'N'"
                sqlcom = sqlcom + ", NULL, NULL, 0, 0, 0, NULL, NULL, 'Y', 'D', 'L', 'RP', NULL)"
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = "update daftar_customer"
                sqlcom = sqlcom + " set akun_piutang_giro_mundur = '" & vpiutang_giro_mundur & "'"
                sqlcom = sqlcom + " where id = " & reader.Item("id").ToString
                connection.koneksi.UpdateRecord(sqlcom)

                'Daniel
                vpiutang_dagang = "11.03.02." & no
                'Daniel
                sqlcom = "insert into coa_list(AccountNo, LAccount, ParentAcc, LParent, EngName, InaName, AccType, ShareAcc, IsControl,"
                sqlcom = sqlcom + " CostType, OffsetAcc, MinAmount, MaxAmount, DefAmount, CDesc, Remark, IsActive, Position, CurrType, CurrencyCode, AddAdjust)"
                sqlcom = sqlcom + " values('" & vpiutang_dagang & "','5', '11.03.02', 4, NULL,'PIUTANG DAGANG " & reader.Item("name").ToString & "', 11, NULL, 'N'"
                sqlcom = sqlcom + ", NULL, NULL, 0, 0, 0, NULL, NULL, 'Y', 'D', 'L', 'RP', NULL)"
                connection.koneksi.InsertRecord(sqlcom)

                sqlcom = "update daftar_customer"
                sqlcom = sqlcom + " set akun_piutang_dagang = '" & vpiutang_dagang & "'"
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
