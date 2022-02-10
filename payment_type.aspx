<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="payment_type.aspx.vb" Inherits="payment_type" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/purchasing/jenis_pembayaran_import.ascx" TagName="jenis_pembayaran_import"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:jenis_pembayaran_import ID="Jenis_pembayaran_import1" runat="server" />
</asp:Content>

