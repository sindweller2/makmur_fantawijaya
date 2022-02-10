<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="entry_stock_awal_gudang.aspx.vb" Inherits="entry_stock_awal_gudang" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Penjualan/entry_stock.ascx" TagName="entry_stock"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:entry_stock ID="entry_stock1" runat="server" />
</asp:Content>

