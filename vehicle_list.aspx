<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="vehicle_list.aspx.vb" Inherits="vehicle_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/gudang/daftar_kendaraan.ascx" TagName="daftar_kendaraan"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_kendaraan ID="Daftar_kendaraan1" runat="server" />
</asp:Content>

