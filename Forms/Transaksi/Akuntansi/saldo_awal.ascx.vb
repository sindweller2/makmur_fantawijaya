Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Akuntansi_saldo_awal
    Inherits System.Web.UI.UserControl

    Dim tradingClass As tradingClass = New tradingClass()

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = Nothing
            sqlcom = "select coa_list.accountno, coa_list.inaname, isnull(coa_list.saldo_awal,0) as saldo_awal"
            sqlcom = sqlcom + " from coa_list"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " where upper(coa_list.inaname) like upper('%" & Me.tb_search.Text & "%')"
            End If

            sqlcom = sqlcom + " order by coa_list.autocoa"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "coa_list")
                Me.dg_data.DataSource = ds.Tables("coa_list").DefaultView

                If ds.Tables("coa_list").Rows.Count > 0 Then
                    If ds.Tables("coa_list").Rows.Count > 12 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 12
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                Else
                    Me.dg_data.Visible = False
                End If
            End Using
            con.Dispose()
            con.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub

    Function get_parentacc(ByVal accountno As String) As String
        Dim result As String = Nothing
        Dim dataTable As DataTable

        dataTable = tradingClass.DataTableQuery("select parentacc from COA_list where accountno = '" & accountno & "'")

        result = dataTable.Rows(0).Item(0).ToString

        dataTable = Nothing

        Return result
    End Function

    Function sum_saldo_awal(ByVal accountno As String) As String
        Dim result As String = Nothing
        Dim dataTable As DataTable

        dataTable = tradingClass.DataTableQuery("select sum(saldo_awal) from coa_list where parentacc = (select parentacc from COA_list where accountno = '" & accountno & "')")

        result = dataTable.Rows(0).Item(0).ToString

        dataTable = Nothing

        Return result
    End Function

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.loadgrid()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.loadgrid()
        End If
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked = True Then
                    sqlcom = Nothing
                    sqlcom = "update coa_list"
                    sqlcom = sqlcom + " set saldo_awal = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_saldo_awal"), TextBox).Text.Trim)
                    sqlcom = sqlcom + " where accountno = '" & CType(Me.dg_data.Items(x).FindControl("lbl_accountno"), Label).Text.Trim & "'"
                    connection.koneksi.UpdateRecord(sqlcom)

                    sqlcom = Nothing
                    sqlcom = "delete from saldo"
                    sqlcom = sqlcom + " where accountno = '" & CType(Me.dg_data.Items(x).FindControl("lbl_accountno"), Label).Text.Trim & "'"
                    sqlcom = sqlcom + " and id_transaction_period = '" + Me.tradingClass.IDPeriodConverter(Date.Now.Year, 1) + "'"
                    connection.koneksi.DeleteRecord(sqlcom)

                    sqlcom = Nothing
                    sqlcom = "insert into saldo"
                    sqlcom = sqlcom + " values ('" & Me.tradingClass.IDPeriodConverter(Date.Now.Year, 1) & "','" & CType(Me.dg_data.Items(x).FindControl("lbl_accountno"), Label).Text.Trim & "','" & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_saldo_awal"), TextBox).Text.Trim) & "')"
                    connection.koneksi.InsertRecord(sqlcom)

                    sqlcom = Nothing
                    sqlcom = "delete from saldo"
                    sqlcom = sqlcom + " where accountno = '" & Me.get_parentacc(CType(Me.dg_data.Items(x).FindControl("lbl_accountno"), Label).Text.Trim) & "'"
                    sqlcom = sqlcom + " and id_transaction_period = '" & Me.tradingClass.IDPeriodConverter(Date.Now.Year, 1) & "'"
                    connection.koneksi.DeleteRecord(sqlcom)

                    sqlcom = Nothing
                    sqlcom = "insert into saldo"
                    sqlcom = sqlcom + " values ('" & Me.tradingClass.IDPeriodConverter(Date.Now.Year, 1) & "','" & Me.get_parentacc(CType(Me.dg_data.Items(x).FindControl("lbl_accountno"), Label).Text.Trim) & "','" & Me.sum_saldo_awal(CType(Me.dg_data.Items(x).FindControl("lbl_accountno"), Label).Text.Trim) & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                End If
            Next

            Me.tradingClass.Alert("Data sudah diupdate", Me.Page)

            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class
