<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="st_pegawai_pajak.aspx.vb" Inherits="st_pegawai_pajak" title="Untitled Page" %>

<%@ Register Src="Forms/Setting/pegawai_faktur_pajak.ascx" TagName="pegawai_faktur_pajak"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:pegawai_faktur_pajak ID="Pegawai_faktur_pajak1" runat="server" />
</asp:Content>

