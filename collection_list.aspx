<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="collection_list.aspx.vb" Inherits="collection_list" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Collection/daftar_invoice_jatuh_tempo.ascx" TagName="daftar_invoice_jatuh_tempo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_invoice_jatuh_tempo ID="Daftar_invoice_jatuh_tempo1" runat="server" />
</asp:Content>

