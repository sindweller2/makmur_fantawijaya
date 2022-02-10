<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="payment_sales_order.aspx.vb" Inherits="payment_sales_order" title="Untitled Page" %>

<%@ Register Src="Forms/Transaksi/Keuangan/penerimaan_pembayaran_penjualan_list.ascx"
    TagName="penerimaan_pembayaran_penjualan_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:penerimaan_pembayaran_penjualan_list ID="Penerimaan_pembayaran_penjualan_list1"
        runat="server" />
</asp:Content>

