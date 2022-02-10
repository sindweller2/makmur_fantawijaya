<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="daftar_hs_produk.aspx.vb" Inherits="daftar_hs_produk" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/purchasing/hs_no_produk.ascx" TagName="hs_no_produk"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:hs_no_produk ID="Hs_no_produk1" runat="server" />
</asp:Content>

