<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="transaction_period.aspx.vb" Inherits="transaction_period" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/periode_transaksi.ascx" TagName="periode_transaksi"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:periode_transaksi ID="Periode_transaksi1" runat="server" />
</asp:Content>

