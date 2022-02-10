<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="rep_sales_summary_by_type_of_goods.aspx.vb" Inherits="rep_sales_summary_by_type_of_goods" title="Untitled Page" %>

<%@ Register Src="Forms/Report/lap_sales_summary_by_type_of_goods.ascx" TagName="lap_sales_summary_by_type_of_goods"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:lap_sales_summary_by_type_of_goods ID="Lap_sales_summary_by_type_of_goods1"
        runat="server" />
</asp:Content>

