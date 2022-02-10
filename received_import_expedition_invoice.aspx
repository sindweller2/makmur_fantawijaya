<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="received_import_expedition_invoice.aspx.vb" Inherits="received_import_expedition_invoice" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/received_expedition_invoice.ascx" TagName="received_expedition_invoice"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:received_expedition_invoice ID="Received_expedition_invoice1" runat="server" />
</asp:Content>

