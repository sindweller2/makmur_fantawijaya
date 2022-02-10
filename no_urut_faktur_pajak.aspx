<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="no_urut_faktur_pajak.aspx.vb" Inherits="no_urut_faktur_pajak" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/nomor_urut_faktur_pajak.ascx" TagName="nomor_urut_faktur_pajak"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:nomor_urut_faktur_pajak ID="nomor_urut_faktur_pajak1" runat="server" />
</asp:Content>

