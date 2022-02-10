<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="entry_stock_awal.aspx.vb" Inherits="entry_stock_awal" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Akuntansi/entry_stock.ascx" TagName="entry_stock"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:entry_stock id="Entry_stock1" runat="server">
    </uc1:entry_stock>
</asp:Content>

