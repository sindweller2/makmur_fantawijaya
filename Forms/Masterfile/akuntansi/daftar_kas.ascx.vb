Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akuntansi_daftar_kas
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub bindmata_uang()
        sqlcom = "select id from currency order by id"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_mata_uang.DataSource = reader
        Me.dd_mata_uang.DataTextField = "id"
        Me.dd_mata_uang.DataValueField = "id"
        Me.dd_mata_uang.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub bindakun()
        sqlcom = "select accountno, inaname "
        sqlcom = sqlcom + " from coa_list"
        sqlcom = sqlcom + " where substring(rtrim(accountno),1,5) = '11.01'"
        sqlcom = sqlcom + " and convert(int, right(rtrim(accountno),2)) < 5"
        sqlcom = sqlcom + " and len(accountno) > 5"
        sqlcom = sqlcom + " order by accountno"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_akun.DataSource = reader
        Me.dd_akun.DataTextField = "inaname"
        Me.dd_akun.DataValueField = "accountno"
        Me.dd_akun.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try

            Me.tb_name.Text = ""

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id, name, account_code, id_currency, is_petty_cash, isnull(saldo_akhir,0) as saldo_akhir"
            sqlcom = sqlcom + " from bank_account"
            sqlcom = sqlcom + " where is_bank = 'T'"
            sqlcom = sqlcom + " order by name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "cash_account")
                Me.dg_data.DataSource = ds.Tables("cash_account").DefaultView

                If ds.Tables("cash_account").Rows.Count > 0 Then
                    If ds.Tables("cash_account").Rows.Count > 10 Then
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

                        sqlcom = "select id from currency order by id"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataTextField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataValueField = "id"
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_mata_uang"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        sqlcom = "select accountno, inaname "
                        sqlcom = sqlcom + " from coa_list"
                        sqlcom = sqlcom + " where substring(rtrim(accountno),1,5) = '11.01'"
                        sqlcom = sqlcom + " and convert(int, right(rtrim(accountno),2)) < 5"
                        sqlcom = sqlcom + " and len(accountno) > 5"
                        sqlcom = sqlcom + " order by accountno"
                        reader = connection.koneksi.SelectRecord(sqlcom)
                        CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).DataSource = reader
                        CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).DataTextField = "inaname"
                        CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).DataValueField = "accountno"
                        CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).DataBind()
                        CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_nama_akun"), Label).Text
                        reader.Close()
                        connection.koneksi.CloseKoneksi()

                        CType(Me.dg_data.Items(x).FindControl("dd_petty_cash"), DropDownList).SelectedValue = CType(Me.dg_data.Items(x).FindControl("lbl_petty_cash"), Label).Text
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
            Me.bindmata_uang()
            Me.bindakun()
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama kas terlebih dahulu"
            Else
                Dim vmax As Integer = 0
                sqlcom = "select isnull(max(id),0) + 1 as vmax from bank_account"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    vmax = reader.Item("vmax").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                sqlcom = "insert into bank_account(id, name, account_code, id_currency, is_bank, is_petty_cash)"
                sqlcom = sqlcom + " values(" & vmax & ",'" & Me.tb_name.Text & "','" & Me.dd_akun.SelectedValue & "',"
                sqlcom = sqlcom + "'" & Me.dd_mata_uang.SelectedValue & "','T','" & Me.dd_petty_cash.SelectedValue & "')"
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
                    sqlcom = "delete bank_account"
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
                    sqlcom = "update bank_account"
                    sqlcom = sqlcom + " set name = '" & CType(Me.dg_data.Items(x).FindControl("tb_nama_kas"), TextBox).Text & "',"
                    sqlcom = sqlcom + " account_code = '" & CType(Me.dg_data.Items(x).FindControl("dd_akun"), DropDownList).SelectedValue & "',"
                    sqlcom = sqlcom + " id_currency = '" & CType(Me.dg_data.Items(x).FindControl("dd_mata_uang"), DropDownList).SelectedValue & "',"
                    sqlcom = sqlcom + " saldo_akhir = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_saldo_akhir"), TextBox).Text) & ","
                    sqlcom = sqlcom + " is_petty_cash = '" & CType(Me.dg_data.Items(x).FindControl("dd_petty_cash"), DropDownList).SelectedValue & "'"
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
End Class
