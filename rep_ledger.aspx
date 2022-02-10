<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_ledger.aspx.vb" Inherits="rep_ledger" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Accounting/laporan_ledger.ascx" TagName="laporan_ledger"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:laporan_ledger ID="Laporan_ledger1" runat="server" />
</asp:Content>

