<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="lc_type.aspx.vb" Inherits="lc_type" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/purchasing/daftar_jenis_lc.ascx" TagName="daftar_jenis_lc"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_jenis_lc ID="Daftar_jenis_lc1" runat="server" />
</asp:Content>

