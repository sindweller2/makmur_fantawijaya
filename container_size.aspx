<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="container_size.aspx.vb" Inherits="container_size" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/purchasing/daftar_jenis_kontainer.ascx" TagName="daftar_jenis_kontainer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_jenis_kontainer ID="Daftar_jenis_kontainer1" runat="server" />
</asp:Content>

