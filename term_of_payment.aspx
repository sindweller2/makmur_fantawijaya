<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="term_of_payment.aspx.vb" Inherits="term_of_payment" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/purchasing/daftar_termin_pembayaran_import.ascx"
    TagName="daftar_termin_pembayaran_import" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_termin_pembayaran_import ID="Daftar_termin_pembayaran_import1" runat="server" />
</asp:Content>

