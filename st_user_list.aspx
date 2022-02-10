<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="st_user_list.aspx.vb" Inherits="st_user_list" title="Untitled Page" %>

<%@ Register Src="Forms/Setting/user_list.ascx" TagName="user_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:user_list ID="User_list1" runat="server" />
</asp:Content>

