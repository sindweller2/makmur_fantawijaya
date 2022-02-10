<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_penerimaan_piutang.aspx.vb" Inherits="rep_penerimaan_piutang" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Accounting/laporan_penerimaan_piutang.ascx" TagName="laporan_penerimaan_piutang"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:laporan_penerimaan_piutang ID="laporan_penerimaan_piutang1" runat="server" />
</asp:Content>

