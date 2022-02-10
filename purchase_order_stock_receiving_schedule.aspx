<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="purchase_order_stock_receiving_schedule.aspx.vb" Inherits="purchase_order_stock_receiving_schedule" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Pembelian/monitoring_kedatangan_barang.ascx" TagName="monitoring_kedatangan_barang"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:monitoring_kedatangan_barang ID="Monitoring_kedatangan_barang1" runat="server" />
</asp:Content>

