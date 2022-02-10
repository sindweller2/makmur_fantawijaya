<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="customer_list.aspx.vb" Inherits="customer_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/customer/daftar_customer.ascx" TagName="daftar_customer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:daftar_customer ID="Daftar_customer1" runat="server" />
</asp:Content>

