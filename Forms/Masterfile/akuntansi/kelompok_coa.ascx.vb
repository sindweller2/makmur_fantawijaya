Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_accounting_kelompok_coa
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select acctype.acctype, acctype.inaname, parent.inaname as nama_parent,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when acctype.position = 'D' then 'Debet'"
            sqlcom = sqlcom + " when acctype.position = 'C' then 'Kredit'"
            sqlcom = sqlcom + " end as posisi,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when acctype.type = 'R' then 'Rugi/Laba'"
            sqlcom = sqlcom + " when acctype.type = 'N' then 'Neraca'"
            sqlcom = sqlcom + " end as jenis"
            sqlcom = sqlcom + " from acctype"
            sqlcom = sqlcom + " inner join acctype parent on parent.acctype = acctype.parent"
            sqlcom = sqlcom + " order by acctype"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "acctype")
                Me.dg_data.DataSource = ds.Tables("acctype").DefaultView

                If ds.Tables("acctype").Rows.Count > 0 Then
                    If ds.Tables("acctype").Rows.Count > 15 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 15
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
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
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
End Class
