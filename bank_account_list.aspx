<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="bank_account_list.aspx.vb" Inherits="bank_account_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/daftar_rekening_bank.ascx" TagName="daftar_rekening_bank"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_rekening_bank ID="Daftar_rekening_bank1" runat="server" />
</asp:Content>

