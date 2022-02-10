<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_stock_barang_gudang.aspx.vb" Inherits="rep_stock_barang_gudang" title="Untitled Page" %>

<%@ Register Src="Forms/Report/lap_stock_barang_gudang.ascx" TagName="lap_stock_barang_gudang"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_stock_barang_gudang ID="lap_stock_barang_gudang1" runat="server" />
</asp:Content>

