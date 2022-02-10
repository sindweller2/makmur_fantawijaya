<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="jurnal.aspx.vb" Inherits="jurnal" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Akuntansi/jurnal_list.ascx" TagName="jurnal_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:jurnal_list ID="jurnal_list1" runat="server" />
</asp:Content>

