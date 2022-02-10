<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="transfer_antar_kas_detil.aspx.vb" Inherits="transfer_antar_kas_detil" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Collection/detil_transfer_antar_kas.ascx" TagName="detil_transfer_antar_kas"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_transfer_antar_kas ID="Detil_transfer_antar_kas1" runat="server" />
</asp:Content>

