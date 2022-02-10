<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="sales_order.aspx.vb" Inherits="sales_order" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Penjualan/sales_order_list.ascx" TagName="sales_order_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:sales_order_list ID="Sales_order_list1" runat="server" />
</asp:Content>

