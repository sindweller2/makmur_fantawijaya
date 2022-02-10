<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="petty_cash_transaction.aspx.vb" Inherits="petty_cash_transaction" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Collection/transaksi_petty_cash.ascx" TagName="transaksi_petty_cash"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:transaksi_petty_cash ID="Transaksi_petty_cash1" runat="server" />
</asp:Content>

