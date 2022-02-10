Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient


Partial Class Forms_Masterfile_akuntansi_kurs_bulanan
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select id, bulan, tahun, name, convert(char, tgl_awal, 103) as tgl_awal,"
            sqlcom = sqlcom + " convert(char, tgl_akhir, 103) as tgl_akhir, isnull(kurs_bulanan,0) as kurs_bulanan,isnull(EUR,0) as EUR,isnull(JPY,0) as JPY,isnull(CNY,0) as CNY,isnull(GBP,0) as GBP,isnull(AUD,0) as AUD,isnull(SGD,0) as SGD,isnull(CHF,0) as CHF,isnull(KRW,0) as KRW,isnull(MYR,0) as MYR,isnull(HKD,0) as HKD"
            sqlcom = sqlcom + " from transaction_period"
            sqlcom = sqlcom + " where tahun = " & Me.tb_tahun.Text
            sqlcom = sqlcom + " order by bulan"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "transaction_period")
                Me.dg_data.DataSource = ds.Tables("transaction_period").DefaultView

                If ds.Tables("transaction_period").Rows.Count > 0 Then
                    If ds.Tables("transaction_period").Rows.Count > 12 Then
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
                    Me.btn_update.Visible = True
                Else
                    Me.dg_data.Visible = False
                    Me.btn_update.Visible = False
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
            Me.tb_tahun.Text = Year(Now.Date)
            Me.loadgrid()
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
        Try
            For x As Integer = 0 To Me.dg_data.Items.Count - 1
                If CType(Me.dg_data.Items(x).FindControl("cb_data"), CheckBox).Checked Then
                    sqlcom = "update transaction_period"
                    sqlcom = sqlcom + " set kurs_bulanan = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_kurs_bulanan"), TextBox).Text)
                    'Daniel
                    sqlcom = sqlcom + " , EUR = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_EUR"), TextBox).Text)
                    sqlcom = sqlcom + " , JPY = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_JPY"), TextBox).Text)
                    sqlcom = sqlcom + " , CNY = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_CNY"), TextBox).Text)
                    sqlcom = sqlcom + " , GBP = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_GBP"), TextBox).Text)
                    sqlcom = sqlcom + " , AUD = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_AUD"), TextBox).Text)
                    sqlcom = sqlcom + " , SGD = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_SGD"), TextBox).Text)
                    sqlcom = sqlcom + " , CHF = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_CHF"), TextBox).Text)
                    sqlcom = sqlcom + " , KRW = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_KRW"), TextBox).Text)
                    sqlcom = sqlcom + " , MYR = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_MYR"), TextBox).Text)
                    sqlcom = sqlcom + " , HKD = " & Decimal.ToDouble(CType(Me.dg_data.Items(x).FindControl("tb_HKD"), TextBox).Text)
                    'Daniel
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

    Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        Me.loadgrid()
    End Sub

    Protected Sub dg_data_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_data.PageIndexChanged
        Me.dg_data.CurrentPageIndex = e.NewPageIndex
        Me.loadgrid()
    End Sub
End Class
