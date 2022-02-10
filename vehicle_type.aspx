<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="vehicle_type.aspx.vb" Inherits="vehicle_type" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/gudang/jenis_kendaraan.ascx" TagName="jenis_kendaraan"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:jenis_kendaraan ID="Jenis_kendaraan1" runat="server" />
</asp:Content>

