<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="pembayaran_biaya_lc_purchasing.aspx.vb" Inherits="pembayaran_biaya_lc_purchasing" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/pembayaran_biaya_lc.ascx" TagName="pembayaran_biaya_lc"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:pembayaran_biaya_lc ID="Pembayaran_biaya_lc1" runat="server" />
</asp:Content>

