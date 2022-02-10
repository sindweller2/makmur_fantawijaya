Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_akuntansi_akun_piutang_karyawan
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Public tradingClass As tradingClass = New tradingClass

    Sub loadgrid()
        Try

            Me.tb_nama_karyawan.Text = Nothing

            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select * from [trading].[dbo].[COA_list] where [InaName] like '%piutang karyawan%'"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "coa_list")
                Me.dg_data.DataSource = ds.Tables("coa_list").DefaultView

                If ds.Tables("coa_list").Rows.Count > 0 Then
                    If ds.Tables("coa_list").Rows.Count > 10 Then
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

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_nama_karyawan.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama akun terlebih dahulu"
            Else
                Dim vmax As Integer = 0
                If tradingClass.PiutangKaryawanMaxAccountNo() <> Nothing Then
                    vmax = tradingClass.PiutangKaryawanMaxAccountNo() + 1
                End If

                sqlcom = "insert into [trading].[dbo].[COA_list] ([AccountNo] ,[LAccount] ,[ParentAcc] ,[LParent] ,[InaName] ,[AccType] ,[IsControl] ,[MinAmount] ,[MaxAmount] ,[DefAmount] ,[IsActive] ,[Position] ,[CurrType] ,[is_neraca]) values ('11.03.04." + vmax.ToString() + "' ,5 ,'11.03.04' ,4 ,'PIUTANG KARYAWAN " + Me.tb_nama_karyawan.Text.Trim() + "' ,'11' ,'N' ,0.0 ,0.0 ,0.0 ,'Y' ,'D' ,'L' ,'N')"
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
                    sqlcom = "delete from [trading].[dbo].[COA_list] where AutoCoa = " & CType(Me.dg_data.Items(x).FindControl("lbl_AutoCoa"), Label).Text
                    connection.koneksi.DeleteRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah dihapus"
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
