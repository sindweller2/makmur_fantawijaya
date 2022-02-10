<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_daftar_transaksi_kas_gudang.aspx.vb" Inherits="detil_daftar_transaksi_kas_gudang" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Gudang/detil_transaksi_petty_cash.ascx" TagName="detil_transaksi_petty_cash"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_transaksi_petty_cash ID="Detil_transaksi_petty_cash1" runat="server" />
</asp:Content>

