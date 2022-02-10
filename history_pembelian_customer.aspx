<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="history_pembelian_customer.aspx.vb" Inherits="history_pembelian_customer" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/history_pembelian_customer.ascx" TagName="history_pembelian_customer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:history_pembelian_customer ID="History_pembelian_customer1" runat="server" />
</asp:Content>

