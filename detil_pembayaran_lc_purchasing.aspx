<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_pembayaran_lc_purchasing.aspx.vb" Inherits="detil_pembayaran_lc_purchasing" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/detil_pembayaran_lc.ascx" TagName="detil_pembayaran_lc"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_pembayaran_lc ID="Detil_pembayaran_lc1" runat="server" />
</asp:Content>

