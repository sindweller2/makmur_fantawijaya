<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="ar_per_customer.aspx.vb" Inherits="ar_per_customer" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Collection/daftar_piutang_per_customer.ascx" TagName="daftar_piutang_per_customer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_piutang_per_customer ID="Daftar_piutang_per_customer1" runat="server" />
</asp:Content>

