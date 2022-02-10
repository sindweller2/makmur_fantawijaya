<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false"
    CodeFile="rep_keuangan.aspx.vb" Inherits="rep_keuangan" Title="Untitled Page" %>

<%@ Register Src="Forms/Report/Accounting/laporan_keuangan.ascx" TagName="laporan_keuangan"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:laporan_keuangan ID="laporan_keuangan" runat="server" />
</asp:Content>
