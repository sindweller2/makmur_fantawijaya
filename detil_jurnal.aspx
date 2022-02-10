<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_jurnal.aspx.vb" Inherits="detil_jurnal" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Akuntansi/jurnal.ascx" TagName="jurnal" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:jurnal ID="Jurnal1" runat="server" />
</asp:Content>

