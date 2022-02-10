<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="history_kas_bank.aspx.vb" Inherits="history_kas_bank" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/history_kas.ascx" TagName="history_kas"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:history_kas ID="History_kas1" runat="server" />
</asp:Content>

