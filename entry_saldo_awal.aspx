<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="entry_saldo_awal.aspx.vb" Inherits="entry_saldo_awal" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Akuntansi/saldo_awal.ascx" TagName="saldo_awal"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:saldo_awal ID="Saldo_awal1" runat="server" />
</asp:Content>

