<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="customer_industry.aspx.vb" Inherits="customer_industry" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/customer/bidang_usaha_customer.ascx" TagName="bidang_usaha_customer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:bidang_usaha_customer ID="Bidang_usaha_customer1" runat="server" />
</asp:Content>

