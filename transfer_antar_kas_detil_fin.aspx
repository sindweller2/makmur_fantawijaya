<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="transfer_antar_kas_detil_fin.aspx.vb" Inherits="transfer_antar_kas_detil_fin" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/detil_transfer_antar_kas_fin.ascx" TagName="detil_transfer_antar_kas_fin"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_transfer_antar_kas_fin ID="Detil_transfer_antar_kas_fin1" runat="server" />
</asp:Content>

