<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="entry_saldo_awal_customer.aspx.vb" Inherits="entry_saldo_awal_customer" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Akuntansi/saldo_awal_customer.ascx" TagName="saldo_awal_customer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:saldo_awal_customer ID="Saldo_awal_customer1" runat="server" />
</asp:Content>

