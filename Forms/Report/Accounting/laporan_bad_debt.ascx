<%@ Control Language="VB" AutoEventWireup="false" CodeFile="laporan_bad_debt.ascx.vb" Inherits="Forms_Report_Accounting_laporan_bad_debt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Laporan Bad Debt" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl23" Text="Pilihan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lb45" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_pilihan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                <asp:ListItem Text="Keseluruhan" Value="A"></asp:ListItem>
                <asp:ListItem Text="Jumlah hari" Value="J"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="tb_hari" Width="50" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Label runat="server" ID="lbl_sd" Text=" s.d " Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_hari_ke" Width="50" Font-Names="Tahoma" Font-Size="8"/>
        </td>
    </tr>
    <tr>
        
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>


<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\lap_bad_debt.rpt"></Report>
</CR:CrystalReportSource>