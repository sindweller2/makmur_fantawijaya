<%@ Page Language="VB" MasterPageFile="~/popup_master.master" Title="Daftar Produk" %>

<%@ Register Src="forms/PopUp/popup_produk_retur.ascx" TagName="popup_produk_retur" TagPrefix="uc1" %>


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
   
    Private Function CloseString() As String
        Return "<script language=""javascript"" type=""text/javascript"">window.close();<" & "/" & "script>"
    End Function
    
    Protected Sub popup_produk_retur1_CloseClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.ltl_js.Text = Me.CloseString
    End Sub

    Protected Sub popup_produk_retur1_ProdukClicked(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Me.ltl_js.Text = Me.CreateJS(Me.popup_produk_retur1.id_produk)
        Me.ltl_js.Text += Me.CloseString
    End Sub
       
    Private Function CreateJS(ByVal id_produk As Integer) As String
        Dim js As New StringBuilder
        With js
            .Append("<script language=""javascript"" type=""text/javascript"">")
            '.Append("window.opener.parent.frames.contentFrame.document.getElementById('" & Me.TargetControlID & "').innerText = " & id_pasien & vbCrLf)
            .Append("window.opener.document.getElementById('" & Me.TargetControlID & "').innerText = " & id_produk & vbCrLf)
            '.Append("window.opener.parent.frames.contentFrame.__doPostBack('" & Me.RefreshControlID & "', '');" & vbCrLf)
            .Append("window.opener.__doPostBack('" & Me.RefreshControlID & "', '');")
            .Append("</" & "script" & ">")
        End With
        Return js.ToString
    End Function
  
    
    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
    
    <asp:Literal runat="server" ID="ltl_js" />    
    <uc1:popup_produk_retur ID="popup_produk_retur1" runat="server" OnCloseClicked="popup_produk_retur1_CloseClicked" OnProdukClicked="popup_produk_retur1_ProdukClicked" />
</asp:Content>