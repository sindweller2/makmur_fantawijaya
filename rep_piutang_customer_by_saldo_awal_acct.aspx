<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_piutang_customer_by_saldo_awal_acct.aspx.vb" Inherits="rep_piutang_customer_by_saldo_awal_acct" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Accounting/piutang_customer_by_saldo_awal.ascx" TagName="piutang_customer_by_saldo_awal"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:piutang_customer_by_saldo_awal ID="Piutang_customer_by_saldo_awal1" runat="server" />
</asp:Content>

