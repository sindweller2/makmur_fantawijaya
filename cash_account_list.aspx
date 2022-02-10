<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="cash_account_list.aspx.vb" Inherits="cash_account_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/daftar_kas.ascx" TagName="daftar_kas"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_kas ID="Daftar_kas1" runat="server" />
</asp:Content>

