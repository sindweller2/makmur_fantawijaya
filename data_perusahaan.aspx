<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="data_perusahaan.aspx.vb" Inherits="data_perusahaan" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/data_perusahaan.ascx" TagName="data_perusahaan"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:data_perusahaan ID="Data_perusahaan1" runat="server" />
</asp:Content>

