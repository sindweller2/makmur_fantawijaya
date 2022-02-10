Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akuntansi_akun_biaya
    Inherits System.Web.UI.UserControl
    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindcoabiaya()
        sqlcom = "select accountno, inaname from coa_list"
        sqlcom = sqlcom + " where  (InaName LIKE 'BIAYA%')"
        'sqlcom = sqlcom + " and iscontrol = 'N'"
        sqlcom = sqlcom + " order by AutoCoa"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_akun_biaya.DataSource = reader
        Me.dd_akun_biaya.DataTextField = "inaname"
        Me.dd_akun_biaya.DataValueField = "accountno"
        Me.dd_akun_biaya.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try
            Me.tb_name.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id, name, account_code"
            sqlcom = sqlcom + " from item_biaya"
            sqlcom = sqlcom + " order by name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "item_biaya")
                Me.dg_data.DataSource = ds.Tables("item_biaya").DefaultView

                If ds.Tables("item_biaya").Rows.Count > 0 Then
                    If ds.Tables("item_biaya").Rows.Count > 10 Then
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
                    Me.btn_update.Visible = True
                    Me.btn_delete.Visible = True

                    For x As Integer = 0 To Me.dg_data.Items.Count - 1
                        sqlcom = "select accountno, inaname from coa_list"
                        sqlcom = sqlcom + " where (InaName LIKE 'BIAYA%')"
                        'sqlcom = sqlcom + " and iscontrol = 'N'"
                        sqlcom = sqlcom + " order by AutoCoa"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).DataTextField = "inaname"
                        CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).DataValueField = "accountno"
                        CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_id_akun"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()
                    Next

                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
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
            Me.bindcoabiaya()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama biaya terlebih dahulu"
            Else
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(id),0) + 1 as vmax from item_biaya"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into item_biaya(id, name, account_code)"
                sqlcom = sqlcom + " values(" & vmax & ",'" & Me.tb_name.Text & "','" & Me.dd_akun_biaya.SelectedValue & "')"
                connection.koneksi.InsertRecord(sqlcom)
                Me.lbl_msg.Text = "Data sudah disimpan"
                Me.loadgrid()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "delete item_biaya"
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

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = "update item_biaya"
                    sqlcom = sqlcom + " set name = '" & CType(Me.dg_data.Items(x).FindControl("tb_name"), TextBox).Text & "',"
                    sqlcom = sqlcom + " account_code = '" & CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).SelectedValue & "'"
                    sqlcom = sqlcom + " where id = " & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text
                    connection.koneksi.UpdateRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
            Next
            Me.loadgrid()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class
