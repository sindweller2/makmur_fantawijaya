<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="header_tax_no.aspx.vb" Inherits="header_tax_no" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/nomor_depan_faktur_pajak.ascx" TagName="nomor_depan_faktur_pajak"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:nomor_depan_faktur_pajak ID="Nomor_depan_faktur_pajak1" runat="server" />
</asp:Content>

