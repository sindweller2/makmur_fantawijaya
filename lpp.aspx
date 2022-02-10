<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="lpp.aspx.vb" Inherits="lpp" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Collection/lpp_list.ascx" TagName="lpp_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lpp_list ID="lpp_list1" runat="server" />
</asp:Content>

