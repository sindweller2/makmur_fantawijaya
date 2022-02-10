<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_omset_penjualan_perbulan.aspx.vb" Inherits="rep_omset_penjualan_perbulan" title="Untitled Page" %>

<%@ Register Src="Forms/Report/lap_omset_penjualan_perbulan.ascx" TagName="lap_omset_penjualan_perbulan"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_omset_penjualan_perbulan ID="Lap_omset_penjualan_perbulan1" runat="server" />
</asp:Content>

