<%@ Page Language="VB" MasterPageFile="~/popup_master.master" Title="Daftar grup customer" %>

<%@ Register Src="forms/PopUp/popup_grup_customer.ascx" TagName="popup_grup_customer" TagPrefix="uc1" %>


<script runat="server">
    Private ReadOnly Property TargetControlID() As String
        Get
            Dim o As Object = Request.QueryString("tcid1")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
    End Property
    
    Private ReadOnly Property RefreshControlID() As String
        Get
            Dim o As Object = Request.QueryString("tcid2")
            If Not o Is Nothing Then Return CStr(o) Else Return Nothing
        End Get
    End Property

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            Me.popup_grup_customer1.loadgrid()
        End If
    End Sub
    
    Private Function CloseString() As String
        Return "<script language=""javascript"" type=""text/javascript"">window.close();<" & "/" & "script>"
    End Function
    
    Protected Sub popup_grup_customer1_CloseClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.ltl_js.Text = Me.CloseString
    End Sub

    Protected Sub popup_grup_customer1_GrupCustomerClicked(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Me.ltl_js.Text = Me.CreateJS(Me.popup_grup_customer1.id_group_customer)
        Me.ltl_js.Text += Me.CloseString
    End Sub
       
    Private Function CreateJS(ByVal id_group_customer As Integer) As String
        Dim js As New StringBuilder
        With js
            .Append("<script language=""javascript"" type=""text/javascript"">")
            '.Append("window.opener.parent.frames.contentFrame.document.getElementById('" & Me.TargetControlID & "').innerText = " & id_pasien & vbCrLf)
            .Append("window.opener.document.getElementById('" & Me.TargetControlID & "').innerText = " & id_group_customer & vbCrLf)
            '.Append("window.opener.parent.frames.contentFrame.__doPostBack('" & Me.RefreshControlID & "', '');" & vbCrLf)
            .Append("window.opener.__doPostBack('" & Me.RefreshControlID & "', '');")
            .Append("</" & "script" & ">")
        End With
        Return js.ToString
    End Function

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
    
    <asp:Literal runat="server" ID="ltl_js" />    
    <uc1:popup_grup_customer ID="popup_grup_customer1" runat="server" OnCloseClicked="popup_grup_customer1_CloseClicked" OnGrupCustomerClicked="popup_grup_customer1_GrupCustomerClicked" />
</asp:Content>