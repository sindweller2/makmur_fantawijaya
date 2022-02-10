<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="customer_group_list.aspx.vb" Inherits="customer_group_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/customer/kelompok_customer.ascx" TagName="kelompok_customer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:kelompok_customer ID="Kelompok_customer1" runat="server" />
</asp:Content>

