<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="general_ledger_per_bagian.aspx.vb" Inherits="general_ledger_per_bagian" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Akuntansi/general_ledger_bagian.ascx" TagName="general_ledger_bagian"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:general_ledger_bagian ID="General_ledger_bagian1" runat="server" />
</asp:Content>

