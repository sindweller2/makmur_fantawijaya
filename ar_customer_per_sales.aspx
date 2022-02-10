<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="ar_customer_per_sales.aspx.vb" Inherits="ar_customer_per_sales" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Collection/daftar_piutang_per_sales.ascx" TagName="daftar_piutang_per_sales"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_piutang_per_sales ID="Daftar_piutang_per_sales1" runat="server" />
</asp:Content>

