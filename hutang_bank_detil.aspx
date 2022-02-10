<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="hutang_bank_detil.aspx.vb" Inherits="hutang_bank_detil" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/detil_hutang_bank.ascx" TagName="detil_hutang_bank"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_hutang_bank ID="detil_hutang_bank1" runat="server" />
</asp:Content>

