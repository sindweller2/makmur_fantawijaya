<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="delivery_order_list.aspx.vb" Inherits="delivery_order_list" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Gudang/delivery_order_list.ascx" TagName="delivery_order_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:delivery_order_list ID="Delivery_order_list1" runat="server" />
</asp:Content>

