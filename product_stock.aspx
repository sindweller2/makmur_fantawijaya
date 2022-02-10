<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="product_stock.aspx.vb" Inherits="product_stock" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/produk/produk_stock_list_temp.ascx" TagName="produk_stock_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:produk_stock_list ID="Produk_stock_list1" runat="server" />
</asp:Content>

