<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="product_price.aspx.vb" Inherits="product_price" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/produk/harga_jual_produk.ascx" TagName="harga_jual_produk"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:harga_jual_produk ID="Harga_jual_produk1" runat="server" />
</asp:Content>

