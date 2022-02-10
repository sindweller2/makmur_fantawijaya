<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="pengeluaran_sample.aspx.vb" Inherits="pengeluaran_sample" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Penjualan/pengeluaran_sample.ascx" TagName="pengeluaran_sample"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:pengeluaran_sample ID="Pengeluaran_sample1" runat="server" />
</asp:Content>

