<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="print_pengiriman_barang.aspx.vb" Inherits="print_pengiriman_barang" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Gudang/print_pengiriman_barang.ascx" TagName="print_pengiriman_barang"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:print_pengiriman_barang ID="print_pengiriman_barang1" runat="server" />
</asp:Content>

