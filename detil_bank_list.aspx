<%@ Page Language="VB" MasterPageFile="~/default.master" AutoEventWireup="false" CodeFile="detil_bank_list.aspx.vb" Inherits="detil_bank_list" title="Untitled Page" %>

<%@ Register Src="Forms/Masterfile/akuntansi/detil_bank.ascx" TagName="detil_bank"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:detil_bank id="Detil_bank1" runat="server">
    </uc1:detil_bank>
</asp:Content>

