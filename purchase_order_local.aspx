<%-- Daniel 7/3/2017 ============================================================================ --%>

<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="purchase_order_local.aspx.vb" Inherits="purchase_order_local" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/purchase_order_local.ascx" TagName="purchase_order_local"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:purchase_order_local ID="Purchase_order_local" runat="server" />
</asp:Content>

<%-- ============================================================================ Daniel 7/3/2017 --%>