<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="daftar_transaksi_penerimaan_uang.aspx.vb" Inherits="daftar_transaksi_penerimaan_uang" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Collection/daftar_penerimaan_uang.ascx" TagName="daftar_penerimaan_uang"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_penerimaan_uang ID="Daftar_penerimaan_uang1" runat="server" />
</asp:Content>

