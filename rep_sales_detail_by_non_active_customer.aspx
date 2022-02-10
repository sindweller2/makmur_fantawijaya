<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_sales_detail_by_non_active_customer.aspx.vb" Inherits="rep_sales_detail_by_non_active_customer" title="Untitled Page" %>

<%@ Register Src="Forms/Report/lap_sales_detail_by_non_active_customer.ascx" TagName="lap_sales_detail_by_non_active_customer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_sales_detail_by_non_active_customer ID="Lap_sales_detail_by_non_active_customer" runat="server" />
</asp:Content>

