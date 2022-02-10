<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_produk.aspx.vb" Inherits="detil_produk" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/produk/produk_item_detil.ascx" TagName="produk_item_detil"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:produk_item_detil ID="produk_item_detil1" runat="server" />
</asp:Content>

