<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="daftar_barang_masuk.aspx.vb" Inherits="daftar_barang_masuk" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/barang_masuk.ascx" TagName="barang_masuk"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:barang_masuk id="Barang_masuk1" runat="server">
    </uc1:barang_masuk>
</asp:Content>

