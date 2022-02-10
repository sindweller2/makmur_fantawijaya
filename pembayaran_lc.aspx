<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="pembayaran_lc.aspx.vb" Inherits="pembayaran_lc" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/pembayaran_lc.ascx" TagName="pembayaran_lc"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:pembayaran_lc ID="Pembayaran_lc1" runat="server" />
</asp:Content>

