<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="purchase_order_stock.aspx.vb" Inherits="purchase_order_stock" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/purchase_order_stock.ascx" TagName="purchase_order_stock"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:purchase_order_stock ID="Purchase_order_stock1" runat="server" />
</asp:Content>

