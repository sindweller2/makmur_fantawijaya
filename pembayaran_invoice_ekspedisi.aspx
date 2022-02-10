<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="pembayaran_invoice_ekspedisi.aspx.vb" Inherits="pembayaran_invoice_ekspedisi" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/pembayaran_expedition_invoice.ascx" TagName="pembayaran_expedition_invoice"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:pembayaran_expedition_invoice ID="Pembayaran_expedition_invoice1" runat="server" />
</asp:Content>

