<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_buku_bank.aspx.vb" Inherits="rep_ledger" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Accounting/laporan_buku_bank.ascx" TagName="laporan_buku_bank"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:laporan_buku_bank ID="Laporan_buku_bank" runat="server" />
</asp:Content>

