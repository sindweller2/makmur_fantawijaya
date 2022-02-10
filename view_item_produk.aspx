<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="view_item_produk.aspx.vb" Inherits="view_item_produk" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/purchasing/view_produk_item_list_import.ascx"
    TagName="view_produk_item_list_import" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:view_produk_item_list_import id="View_produk_item_list_import1" runat="server">
    </uc1:view_produk_item_list_import>
</asp:Content>

