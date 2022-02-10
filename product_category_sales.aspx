<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="product_category_sales.aspx.vb" Inherits="product_category_sales" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/produk/kategori_produk_sales.ascx" TagName="kategori_produk_sales"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:kategori_produk_sales ID="Kategori_produk_sales1" runat="server" />
</asp:Content>

