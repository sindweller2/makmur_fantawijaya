<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="product_brand.aspx.vb" Inherits="product_brand" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/produk/merek_produk.ascx" TagName="merek_produk"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:merek_produk ID="Merek_produk1" runat="server" />
</asp:Content>

