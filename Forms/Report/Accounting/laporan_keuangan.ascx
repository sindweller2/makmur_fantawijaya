<%@ Control Language="VB" AutoEventWireup="false" CodeFile="laporan_keuangan.ascx.vb" Inherits="Forms_Report_Accounting_laporan_keuangan" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Laporan Keuangan" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
<tr>
        <td>
            <asp:Label runat="server" ID="lbl111" Text="Tahun transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_refresh" Text="Refresh" Font-Names="Tahoma"
                Font-Size="8" />
        </td>
    </tr>
    <tr>
    <td>
            <asp:Label runat="server" ID="Label4" Text="Laporan" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
        <asp:RadioButton runat="server" ID="RadioButtonNeraca" Text="Neraca" GroupName="laporan"
                Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
                <asp:RadioButton runat="server" ID="RadioButtonRugiLaba" Text="Rugi Laba" GroupName="laporan"
                Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
                <asp:RadioButton runat="server" ID="RadioButtonPerincianBiayaUmum" Text="Perincian Biaya Umum" GroupName="laporan"
                Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
    </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label2" Text="Bulan transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
        <asp:RadioButton runat="server" ID="RadioButton1bulan" Text="1 bulan" GroupName="bulan"
                Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
            <asp:DropDownList runat="server" ID="dd_1_bulan" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
    <td colspan="2"></td>
        <td>
        <asp:RadioButton runat="server" ID="RadioButton3bulan" Text="3 bulan" GroupName="bulan"
                Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
            <asp:DropDownList runat="server" ID="dd_3_bulan" Font-Names="Tahoma" Font-Size="8" >
                <asp:ListItem>Januari - Maret</asp:ListItem>
                <asp:ListItem>April -Juni</asp:ListItem>
                <asp:ListItem>Juli - September</asp:ListItem>
                <asp:ListItem>Oktober - Desember</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
    <td colspan="2"></td>
        <td>
        <asp:RadioButton runat="server" ID="RadioButton6bulan" Text="6 bulan" GroupName="bulan"
                Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
            <asp:DropDownList runat="server" ID="dd_6_bulan" Font-Names="Tahoma" Font-Size="8" >
                <asp:ListItem>Januari - Juni</asp:ListItem>
                <asp:ListItem>Juli - Desember</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
    <td colspan="2"></td>
        <td>
        <asp:RadioButton runat="server" ID="RadioButton12bulan" Text="12 bulan" GroupName="bulan"
                Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
    <td colspan="3">
    </td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
        <asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8" />&nbsp;
        <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>


<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\lap_neraca.rpt"></Report>
</CR:CrystalReportSource>
&nbsp;