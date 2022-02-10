<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_stock_produk.aspx.vb" Inherits="Forms_Masterfile_produk_detil_stock_produk" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/produk/produk_stock_detil.ascx" TagName="produk_stock_detil"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:produk_stock_detil ID="Produk_stock_detil1" runat="server" />
</asp:Content>

