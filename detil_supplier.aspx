<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_supplier.aspx.vb" Inherits="detil_supplier" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/supplier/detil_supplier_produk.ascx" TagName="detil_supplier_produk"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_supplier_produk id="Detil_supplier_produk1" runat="server">
    </uc1:detil_supplier_produk>
</asp:Content>

