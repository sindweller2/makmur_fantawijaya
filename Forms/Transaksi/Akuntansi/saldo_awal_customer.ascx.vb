Imports System.Configuration
Imports System.Data

Partial Class Forms_Transaksi_Akuntansi_saldo_awal_customer
    Inherits System.Web.UI.UserControl

    Dim tradingClass As tradingClass = New tradingClass()

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = Nothing
            sqlcom = "select saldo_awal_customer.id, daftar_customer.name, isnull(saldo_awal_customer.saldo,0) as saldo"
            sqlcom = sqlcom + " from saldo_awal_customer,daftar_customer"
            sqlcom = sqlcom + " where saldo_awal_customer.id_customer = daftar_customer.id"

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
                sqlcom = sqlcom
            Else
                sqlcom = sqlcom + " and daftar_customer.name like '%" & Me.tb_search.Text & "%'"
            End If

            sqlcom = sqlcom + " order by daftar_customer.name"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "saldo_awal_customer")
                Me.dg_data.DataSource = ds.Tables("saldo_awal_customer").DefaultView

                If ds.Tables("saldo_awal_customer").Rows.Count > 0 Then
                    If ds.Tables("saldo_awal_customer").Rows.Count > 12 Then
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
                    sqlcom = "update saldo_awal_customer"
                    sqlcom = sqlcom + " set saldo = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_saldo"), TextBox).Text.Trim)
                    sqlcom = sqlcom + " where id = '" & CType(Me.dg_data.Items(x).FindControl("lbl_id"), Label).Text.Trim & "'"
                    connection.koneksi.UpdateRecord(sqlcom)
                End If
            Next

            Me.tradingClass.Alert("Data sudah diupdate", Me.Page)

            Me.loadgrid()
        Catch ex As Exception
            Me.tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class
