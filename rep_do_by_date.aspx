<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_do_by_date.aspx.vb" Inherits="rep_do_by_date" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Penjualan/lap_do_by_date.ascx" TagName="lap_do_by_date"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_do_by_date ID="Lap_do_by_date1" runat="server" />
</asp:Content>

