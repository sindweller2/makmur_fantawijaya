<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="destination_port_list.aspx.vb" Inherits="destination_port_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/purchasing/daftar_pelabuhan_tujuan.ascx" TagName="daftar_pelabuhan_tujuan"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_pelabuhan_tujuan ID="Daftar_pelabuhan_tujuan1" runat="server" />
</asp:Content>

