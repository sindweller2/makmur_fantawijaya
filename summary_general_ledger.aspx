<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="summary_general_ledger.aspx.vb" Inherits="summary_general_ledger" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Akuntansi/general_ledger_bagian_total.ascx" TagName="general_ledger_bagian_total"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:general_ledger_bagian_total ID="General_ledger_bagian_total1" runat="server" />
</asp:Content>

