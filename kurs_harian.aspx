<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="kurs_harian.aspx.vb" Inherits="kurs_harian" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Penjualan/kurs_harian.ascx" TagName="kurs_harian"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:kurs_harian ID="Kurs_harian1" runat="server" />
</asp:Content>

