<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="pembayaran_biaya_pib_purchasing.aspx.vb" Inherits="pembayaran_biaya_pib_purchasing" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/pembayaran_pib.ascx" TagName="pembayaran_pib"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:pembayaran_pib ID="Pembayaran_pib1" runat="server" />
</asp:Content>

