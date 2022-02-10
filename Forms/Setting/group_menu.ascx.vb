Imports System.Web.Configuration
Imports System.Data

Partial Class form_setting_group_menu
    Inherits System.Web.UI.UserControl

    Dim sqlcom As String
    Dim reader As SqlClient.SqlDataReader

    Sub checkmenu()
        sqlcom = "select code, name"
        sqlcom = sqlcom + " from menu_list"
        sqlcom = sqlcom + " where code not in (select code_menu from group_menu where code_group = " & Me.dd_user_group.SelectedValue & ")"
        sqlcom = sqlcom + " and code_parent is not null"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.tbl_assign.Visible = True
        Else
            Me.tbl_assign.Visible = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try

            Me.checkmenu()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select code, name"
            sqlcom = sqlcom + " from menu_list"
            sqlcom = sqlcom + " where code not in (select code_menu from group_menu where code_group = " & Me.dd_user_group.SelectedValue & ")"
            sqlcom = sqlcom + " and code_parent is not null"
            sqlcom = sqlcom + " order by name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)

            Dim ds As New DataSet()
            Using con
                con.Open()
                da.Fill(ds, "menu_list")
                Me.dg_data.DataSource = ds.Tables("menu_list").DefaultView

                If ds.Tables("menu_list").Rows.Count > 0 Then
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

    Sub bindusergroup()
        sqlcom = "select code, name from user_group order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_user_group.DataSource = reader
        Me.dd_user_group.DataTextField = "name"
        Me.dd_user_group.DataValueField = "code"
        Me.dd_user_group.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub checkdata()
        sqlcom = "select code, name"
        sqlcom = sqlcom + " from menu_list"
        sqlcom = sqlcom + " where code in (select code_menu from group_menu where code_group = " & Me.dd_user_group.SelectedValue & ")"
        sqlcom = sqlcom + " and code_parent is not null"
        sqlcom = sqlcom + " order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.tbl_group_menu.Visible = True
        Else
            Me.tbl_group_menu.Visible = False
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid_usergroup()
        Try

            Me.checkdata()

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select code, name"
            sqlcom = sqlcom + " from menu_list"
            sqlcom = sqlcom + " where code in (select code_menu from group_menu where code_group = " & Me.dd_user_group.SelectedValue & ")"
            sqlcom = sqlcom + " and code_parent is not null"
            sqlcom = sqlcom + " order by name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)

            Dim ds As New DataSet()
            Using con
                con.Open()
                da.Fill(ds, "menu_list")
                Me.dg_group_menu.DataSource = ds.Tables("menu_list").DefaultView

                If ds.Tables("menu_list").Rows.Count > 0 Then
                    Me.dg_group_menu.DataBind()
                    Me.dg_group_menu.Visible = True
                Else
                    Me.dg_group_menu.Visible = False
                End If
            End Using
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.bindusergroup()
            Me.loadgrid()
            loadgrid_usergroup()
        End If
    End Sub

    Protected Sub btn_assign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_assign.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "insert into group_menu(code_group, code_menu, allow_add, allow_delete, allow_update)"
                    sqlcom = sqlcom + " values (" & Me.dd_user_group.SelectedValue & ",'" & CType(Me.dg_data.Items(x).FindControl("lbl_code"), Label).Text & "','True', 'True', 'True')"
                    connection.koneksi.InsertRecord(sqlcom)
                End If
            Next
            Me.loadgrid()
            Me.loadgrid_usergroup()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub dd_user_group_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_user_group.SelectedIndexChanged
        Me.loadgrid()
        Me.loadgrid_usergroup()
    End Sub

    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Try
            For x As Integer = 0 To Me.dg_group_menu.Items.Count - 1
                If CType(Me.dg_group_menu.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete group_menu"
                    sqlcom = sqlcom + " where code_group = " & Me.dd_user_group.SelectedValue
                    sqlcom = sqlcom + " and code_menu = '" & CType(Me.dg_group_menu.Items(x).FindControl("lbl_code"), Label).Text & "'"
                    connection.koneksi.DeleteRecord(sqlcom)
                End If
            Next
            Me.loadgrid()
            Me.loadgrid_usergroup()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
