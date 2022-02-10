<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="expedition.aspx.vb" Inherits="expedition" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/purchasing/daftar_ekspedisi_import.ascx" TagName="daftar_ekspedisi_import"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_ekspedisi_import ID="Daftar_ekspedisi_import1" runat="server" />
</asp:Content>

