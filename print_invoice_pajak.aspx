<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="print_invoice_pajak.aspx.vb" Inherits="print_invoice_pajak" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Penjualan/print_invoice_faktur_pajak.ascx" TagName="print_invoice_faktur_pajak"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:print_invoice_faktur_pajak ID="Print_invoice_faktur_pajak1" runat="server" />
</asp:Content>

