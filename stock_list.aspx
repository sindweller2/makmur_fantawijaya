<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="stock_list.aspx.vb" Inherits="stock_list" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Gudang/produk_stock_list_temp.ascx" TagName="produk_stock_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:produk_stock_list id="Produk_stock_list1" runat="server">
    </uc1:produk_stock_list>
</asp:Content>

