<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_sales_order_list.aspx.vb" Inherits="rep_sales_order_list" title="Untitled Page" %>

<%@ Register Src="Forms/Report/lap_sales_order_list.ascx" TagName="lap_sales_order_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_sales_order_list ID="Lap_sales_order_list1" runat="server" />
</asp:Content>

