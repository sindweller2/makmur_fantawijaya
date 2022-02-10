<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="akun_persediaan_penjualan.aspx.vb" Inherits="akun_persediaan_penjualan" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/akun_persediaan_barang_penjualan.ascx"
    TagName="akun_persediaan_barang_penjualan" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:akun_persediaan_barang_penjualan ID="Akun_persediaan_barang_penjualan1" runat="server" />
</asp:Content>

