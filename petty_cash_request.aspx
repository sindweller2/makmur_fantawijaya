<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="petty_cash_request.aspx.vb" Inherits="petty_cash_request" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Collection/permintaan_petty_cash.ascx" TagName="permintaan_petty_cash"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:permintaan_petty_cash ID="Permintaan_petty_cash1" runat="server" />
</asp:Content>

