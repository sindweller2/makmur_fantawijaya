<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_sales_order.aspx.vb" Inherits="detil_sales_order" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Penjualan/detil_sales_order.ascx" TagName="detil_sales_order"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_sales_order ID="Detil_sales_order1" runat="server" />
</asp:Content>

