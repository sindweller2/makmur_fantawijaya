<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_sales_detail_by_salesname.aspx.vb" Inherits="rep_sales_detail_by_salesname" title="Untitled Page" %>

<%@ Register Src="Forms/Report/lap_sales_detail_by_salesname.ascx" TagName="lap_sales_detail_by_salesname"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_sales_detail_by_salesname ID="Lap_sales_detail_by_salesname1" runat="server" />
</asp:Content>

