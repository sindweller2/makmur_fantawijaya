<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_piutang_customer.aspx.vb" Inherits="rep_piutang_customer" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Collection/lap_piutang_customer.ascx" TagName="lap_piutang_customer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_piutang_customer ID="Lap_piutang_customer1" runat="server" />
</asp:Content>

