<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="supplier_list.aspx.vb" Inherits="supplier_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/supplier/daftar_supplier_produk.ascx" TagName="daftar_supplier_produk"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_supplier_produk ID="Daftar_supplier_produk1" runat="server" />
</asp:Content>

