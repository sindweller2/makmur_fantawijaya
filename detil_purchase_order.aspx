<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_purchase_order.aspx.vb" Inherits="detil_purchase_order" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/detil_purchase_order.ascx" TagName="detil_purchase_order"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_purchase_order ID="Detil_purchase_order1" runat="server" />
</asp:Content>

