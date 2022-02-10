<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="stock_received.aspx.vb" Inherits="stock_received" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Gudang/penerimaan_barang.ascx" TagName="penerimaan_barang"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:penerimaan_barang ID="Penerimaan_barang1" runat="server" />
</asp:Content>

