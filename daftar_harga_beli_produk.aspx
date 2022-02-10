<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="daftar_harga_beli_produk.aspx.vb" Inherits="daftar_harga_beli_produk" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/purchasing/harga_pokok_pembelian.ascx" TagName="harga_pokok_pembelian"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:harga_pokok_pembelian ID="harga_pokok_pembelian1" runat="server" />
</asp:Content>

