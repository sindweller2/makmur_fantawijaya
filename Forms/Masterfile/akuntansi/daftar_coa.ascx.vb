Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_accounting_daftar_coa
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select coa_list.accountno, coa_list.inaname, coa_list.laccount, coa_list.lparent,"
            sqlcom = sqlcom + " parent.inaname as nama_parent, acctype.inaname as kelompok,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when coa_list.iscontrol = 'Y' then 'Ya'"
            sqlcom = sqlcom + " when coa_list.iscontrol = 'N' then 'Bukan'"
            sqlcom = sqlcom + " end as item_kontrol,"
            sqlcom = sqlcom + " case"
            sqlcom = sqlcom + " when coa_list.position = 'D' then 'Debet'"
            sqlcom = sqlcom + " when coa_list.position = 'C' then 'Kredit'"
            sqlcom = sqlcom + " end as posisi,"
            sqlcom = sqlcom + " coa_list.currencycode as mata_uang"
            sqlcom = sqlcom + " from coa_list"
            'Daniel
            sqlcom = sqlcom + " left join coa_list parent on parent.accountno = coa_list.parentacc"
            sqlcom = sqlcom + " left join acctype on acctype.acctype = coa_list.acctype"
            'Daniel

            If String.IsNullOrEmpty(Me.tb_search.Text) Then
               sqlcom = sqlcom
            Else
               if Me.dd_pilihan.SelectedValue = "N" then
                  sqlcom = sqlcom + " where upper(coa_list.inaname) like upper('%" & me.tb_search.Text & "%')"
               else
                  sqlcom = sqlcom + " where rtrim(coa_list.accountno) like upper('%" & me.tb_search.Text & "%')"
               end if
            End If

            sqlcom = sqlcom + " order by coa_list.autocoa"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "coa_list")
                Me.dg_data.DataSource = ds.Tables("coa_list").DefaultView

                If ds.Tables("coa_list").Rows.Count > 0 Then
                    If ds.Tables("coa_list").Rows.Count > 15 Then
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

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        Response.Redirect("~/coa_new.aspx")
    End Sub
End Class
