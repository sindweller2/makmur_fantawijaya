<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="product_sub_category.aspx.vb" Inherits="product_sub_category" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/produk/sub_kategori_produk.ascx" TagName="sub_kategori_produk"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:sub_kategori_produk ID="Sub_kategori_produk1" runat="server" />
</asp:Content>

