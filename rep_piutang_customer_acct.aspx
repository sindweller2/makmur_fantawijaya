<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_piutang_customer_acct.aspx.vb" Inherits="rep_piutang_customer_acct" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Accounting/piutang_customer.ascx" TagName="piutang_customer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:piutang_customer ID="Piutang_customer1" runat="server" />
</asp:Content>

