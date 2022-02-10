<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_pembayaran_pib_purchasing.aspx.vb" Inherits="detil_pembayaran_pib_purchasing" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/detil_pembayaran_pib.ascx" TagName="detil_pembayaran_pib"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_pembayaran_pib ID="Detil_pembayaran_pib1" runat="server" />
</asp:Content>

