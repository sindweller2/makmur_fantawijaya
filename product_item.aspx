<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="product_item.aspx.vb" Inherits="product_item" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/produk/produk_item_list.ascx" TagName="produk_item_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:produk_item_list ID="Produk_item_list1" runat="server" />
</asp:Content>

