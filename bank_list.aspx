<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="bank_list.aspx.vb" Inherits="bank_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/daftar_bank.ascx" TagName="daftar_bank"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_bank ID="Daftar_bank1" runat="server" />
</asp:Content>

