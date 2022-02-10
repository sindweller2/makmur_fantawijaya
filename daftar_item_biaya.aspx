<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="daftar_item_biaya.aspx.vb" Inherits="daftar_item_biaya" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/keuangan/item_biaya.ascx" TagName="item_biaya"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:item_biaya ID="Item_biaya1" runat="server" />
</asp:Content>

