<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="city_list.aspx.vb" Inherits="city_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/customer/daftar_kota.ascx" TagName="daftar_kota"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_kota ID="Daftar_kota1" runat="server" />
</asp:Content>

