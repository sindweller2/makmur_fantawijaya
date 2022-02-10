<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="daftar_transaksi_pengeluaran_uang.aspx.vb" Inherits="daftar_transaksi_pengeluaran_uang" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/transaksi_pengeluaran_uang.ascx" TagName="transaksi_pengeluaran_uang"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:transaksi_pengeluaran_uang ID="Transaksi_pengeluaran_uang1" runat="server" />
</asp:Content>

