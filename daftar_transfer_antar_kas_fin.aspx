<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="daftar_transfer_antar_kas_fin.aspx.vb" Inherits="daftar_transfer_antar_kas_fin" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/transfer_antar_kas_fin.ascx" TagName="transfer_antar_kas_fin"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:transfer_antar_kas_fin ID="Transfer_antar_kas_fin1" runat="server" />
</asp:Content>

