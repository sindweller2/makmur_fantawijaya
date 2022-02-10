<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="penerimaan_hutang_bank.aspx.vb" Inherits="penerimaan_hutang_bank" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/hutang_bank.ascx" TagName="hutang_bank"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:hutang_bank ID="hutang_bank1" runat="server" />
</asp:Content>

