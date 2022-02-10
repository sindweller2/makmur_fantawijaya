<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="product_hs_no.aspx.vb" Inherits="product_hs_no" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/produk/daftar_hs_no_produk.ascx" TagName="daftar_hs_no_produk"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_hs_no_produk ID="Daftar_hs_no_produk1" runat="server" />
</asp:Content>

