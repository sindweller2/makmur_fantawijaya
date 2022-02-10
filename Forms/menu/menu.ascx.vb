Imports System.Web.Configuration
Imports System.Data.SqlClient
Imports System.Data

Partial Class form_Menu_menu
    Inherits System.Web.UI.UserControl

    Sub PopulateRootLevel()

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("trading").ConnectionString
        Dim con As New SqlConnection(connectionString)
        Dim sqlcom As String

        sqlcom = "select pd.code, pd.name, pd.url, (select count(*) from menu_list"
        sqlcom = sqlcom + " where code_parent = pd.code) as childnodecount"
        sqlcom = sqlcom + " from menu_list pd "
        sqlcom = sqlcom + " where pd.code_parent is null "
        sqlcom = sqlcom + " and pd.code in (select x.code_parent from menu_list x where code in (select code_menu from group_menu where code_group = "
        sqlcom = sqlcom + " (select code_group from user_list where code = " & HttpContext.Current.Session("UserId") & ")))"
        sqlcom = sqlcom + " or pd.code in ('LOG', 'PWD')"
        sqlcom = sqlcom + " order by pd.sort_no"
        Dim da As New SqlDataAdapter(sqlcom, con)

        Dim dt As New DataTable()
        Using con
            con.Open()
            da.Fill(dt)
            PopulateNodes(dt, TreeView1.Nodes)
        End Using
        connection.koneksi.CloseKoneksi()
    End Sub

    Private Sub PopulateNodes(ByVal dt As DataTable, ByVal nodes As TreeNodeCollection)
        For Each dr As DataRow In dt.Rows
            Dim tn As New TreeNode()
            tn.Text = dr("name").ToString()
            tn.Value = dr("code").ToString()
            tn.NavigateUrl = dr("url").ToString
            tn.Target = "ContentFrame"
            nodes.Add(tn)

            'If node has child nodes, then enable on-demand populating
            tn.PopulateOnDemand = (CInt(dr("childnodecount")) > 0)
        Next
    End Sub


    Private Sub PopulateSubLevel(ByVal parentid As String, ByVal parentNode As TreeNode)
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("trading").ConnectionString
        Dim con As New SqlConnection(connectionString)
        Dim sqlcom As String
        Dim objcommand As New SqlCommand

        Dim vparent_id As String = ""
        objcommand.Parameters.Add("@parentID", SqlDbType.VarChar).Value = parentid
        vparent_id = objcommand.Parameters.Add("@parentID", SqlDbType.VarChar).Value
        vparent_id = parentid


        sqlcom = "select pd.code, pd.name, pd.url, (select count(*) from menu_list where code_parent = pd.code) as childnodecount"
        sqlcom = sqlcom + " from menu_list pd  where pd.code_parent = '" & vparent_id & "'"
        sqlcom = sqlcom + " and pd.code in "
        sqlcom = sqlcom + " (select code_menu from group_menu where code_group = "
        sqlcom = sqlcom + " (select code_group from user_list where code = " & HttpContext.Current.Session("UserID") & "))"
        sqlcom = sqlcom + " order by pd.sort_no"

        Dim da As New SqlDataAdapter(sqlcom, con)

        Dim dt As New DataTable()
        Using con
            con.Open()
            da.Fill(dt)
            PopulateNodes(dt, parentNode.ChildNodes)
        End Using
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then            
            PopulateRootLevel()
        End If
    End Sub

    Protected Sub TreeView1_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodePopulate
        PopulateSubLevel(CStr(e.Node.Value), e.Node)
    End Sub
End Class
