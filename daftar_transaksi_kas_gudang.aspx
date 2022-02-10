<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="daftar_transaksi_kas_gudang.aspx.vb" Inherits="daftar_transaksi_kas_gudang" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Gudang/transaksi_petty_cash.ascx" TagName="transaksi_petty_cash"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:transaksi_petty_cash ID="Transaksi_petty_cash1" runat="server" />
</asp:Content>

