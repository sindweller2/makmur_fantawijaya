<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_bad_debt.aspx.vb" Inherits="rep_bad_debt" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Accounting/laporan_bad_debt.ascx" TagName="laporan_bad_debt"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:laporan_bad_debt ID="Laporan_bad_debt1" runat="server" />
</asp:Content>

