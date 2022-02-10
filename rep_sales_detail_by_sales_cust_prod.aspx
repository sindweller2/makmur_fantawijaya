<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_sales_detail_by_sales_cust_prod.aspx.vb" Inherits="rep_sales_detail_by_sales_cust_prod" title="Untitled Page" %>

<%@ Register Src="Forms/Report/lap_sales_detail_by_sales_cust_prod.ascx" TagName="lap_sales_detail_by_sales_cust_prod"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_sales_detail_by_sales_cust_prod ID="Lap_sales_detail_by_sales_cust_prod1"
        runat="server" />
</asp:Content>

