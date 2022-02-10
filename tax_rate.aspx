<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="tax_rate.aspx.vb" Inherits="tax_rate" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/kurs_pajak.ascx" TagName="kurs_pajak"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:kurs_pajak id="Kurs_pajak1" runat="server">
    </uc1:kurs_pajak>
</asp:Content>

