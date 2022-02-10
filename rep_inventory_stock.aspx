<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_inventory_stock.aspx.vb" Inherits="rep_inventory_stock" title="Untitled Page" %>

<%@ Register Src="Forms/Report/Accounting/inventory_stock_barang.ascx" TagName="inventory_stock_barang"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:inventory_stock_barang ID="Inventory_stock_barang1" runat="server" />
</asp:Content>

