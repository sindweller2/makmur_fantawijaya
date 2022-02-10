<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="currency_list.aspx.vb" Inherits="currency_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/mata_uang.ascx" TagName="mata_uang"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:mata_uang ID="Mata_uang1" runat="server" />
</asp:Content>

