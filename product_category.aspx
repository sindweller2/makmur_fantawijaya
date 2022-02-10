<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="product_category.aspx.vb" Inherits="product_category" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/produk/kategori_produk.ascx" TagName="kategori_produk"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:kategori_produk ID="Kategori_produk1" runat="server" />
</asp:Content>

