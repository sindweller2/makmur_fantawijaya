<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="daftar_transfer_antar_kas.aspx.vb" Inherits="daftar_transfer_antar_kas" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Collection/transfer_antar_kas.ascx" TagName="transfer_antar_kas"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:transfer_antar_kas ID="Transfer_antar_kas1" runat="server" />
</asp:Content>

