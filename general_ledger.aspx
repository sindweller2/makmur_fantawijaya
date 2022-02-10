<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="general_ledger.aspx.vb" Inherits="general_ledger" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Akuntansi/general_ledger.ascx" TagName="general_ledger"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:general_ledger ID="General_ledger1" runat="server" />
</asp:Content>

