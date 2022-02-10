<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="coa_list.aspx.vb" Inherits="coa_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/daftar_coa.ascx" TagName="daftar_coa"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_coa ID="Daftar_coa1" runat="server" />
</asp:Content>

