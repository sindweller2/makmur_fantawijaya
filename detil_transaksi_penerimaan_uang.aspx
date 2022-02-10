<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_transaksi_penerimaan_uang.aspx.vb" Inherits="detil_transaksi_penerimaan_uang" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Collection/detil_penerimaan_uang.ascx" TagName="detil_penerimaan_uang"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_penerimaan_uang ID="Detil_penerimaan_uang1" runat="server" />
</asp:Content>

