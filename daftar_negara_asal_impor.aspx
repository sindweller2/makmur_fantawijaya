<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="daftar_negara_asal_impor.aspx.vb" Inherits="daftar_negara_asal_impor" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/purchasing/daftar_negara_import.ascx" TagName="daftar_negara_import"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_negara_import ID="Daftar_negara_import1" runat="server" />
</asp:Content>

