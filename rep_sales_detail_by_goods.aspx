<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_sales_detail_by_goods.aspx.vb" Inherits="rep_sales_detail_by_goods" title="Untitled Page" %>

<%@ Register Src="Forms/Report/lap_sales_detail_by_goods.ascx" TagName="lap_sales_detail_by_goods"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_sales_detail_by_goods ID="Lap_sales_detail_by_goods1" runat="server" />
</asp:Content>

