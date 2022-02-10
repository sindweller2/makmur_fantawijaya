<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="lc.aspx.vb" Inherits="lc" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/daftar_lc.ascx" TagName="daftar_lc" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_lc ID="Daftar_lc1" runat="server" />
</asp:Content>

