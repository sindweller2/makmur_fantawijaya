<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="invoice.aspx.vb" Inherits="invoice" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Penjualan/invoice_sales_order_list.ascx" TagName="invoice_sales_order_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:invoice_sales_order_list ID="Invoice_sales_order_list1" runat="server" />
</asp:Content>

