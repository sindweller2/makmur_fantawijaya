<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_piutang_customer_by_payment_date_acct.aspx.vb" Inherits="rep_piutang_customer_by_payment_date_acct" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Accounting/piutang_customer_by_payment_date.ascx" TagName="piutang_customer_by_payment_date"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:piutang_customer_by_payment_date ID="Piutang_customer_by_payment_date1" runat="server" />
</asp:Content>

