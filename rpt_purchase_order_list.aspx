<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false"
    CodeFile="rpt_purchase_order_list.aspx.vb" Inherits="rpt_purchase_order_list"
    Title="Untitled Page" %>

<%@ Register Src="Forms/Report/Pembelian/lap_purchase_order.ascx" TagName="lap_purchase_order"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:lap_purchase_order ID="Lap_purchase_order1" runat="server" />
</asp:Content>
