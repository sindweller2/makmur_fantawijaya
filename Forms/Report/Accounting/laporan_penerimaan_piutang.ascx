<%@ Control Language="VB" AutoEventWireup="false" CodeFile="laporan_penerimaan_piutang.ascx.vb"
    Inherits="Forms_Report_Accounting_laporan_penerimaan_piutang" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Laporan Penerimaan Piutang"
                Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>
<table align="center">
    <tr>
<td>
            <asp:Label runat="server" ID="lbl111" Text="Periode" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label12" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tgl" Width="60" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999"
                MaskType="Date" TargetControlID="tb_tgl" />
            <ajax:CalendarExtender ID="ce_tgl" TargetControlID="tb_tgl" runat="server"
                Format="dd/MM/yyyy" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label4" Text="Format" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:RadioButton runat="server" ID="RadioButtonPDF" Text="PDF" GroupName="format"
                Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
            <asp:RadioButton runat="server" ID="RadioButtonExcel" Text="Excel" GroupName="format"
                Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
        </td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\lap_penerimaan_piutang.rpt">
    </Report>
</CR:CrystalReportSource>
