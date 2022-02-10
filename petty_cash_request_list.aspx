<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="petty_cash_request_list.aspx.vb" Inherits="petty_cash_request_list" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/daftar_pengeluaran_petty_cash.ascx" TagName="daftar_pengeluaran_petty_cash"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_pengeluaran_petty_cash ID="Daftar_pengeluaran_petty_cash1" runat="server" />
</asp:Content>

