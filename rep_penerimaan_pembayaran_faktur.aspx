<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_penerimaan_pembayaran_faktur.aspx.vb" Inherits="rep_penerimaan_pembayaran_faktur" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Collection/lap_penerimaan_pembayaran_faktur.ascx"
    TagName="lap_penerimaan_pembayaran_faktur" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_penerimaan_pembayaran_faktur ID="Lap_penerimaan_pembayaran_faktur1" runat="server" />
</asp:Content>

