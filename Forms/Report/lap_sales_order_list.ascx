<%@ Control Language="VB" AutoEventWireup="false" CodeFile="lap_sales_order_list.ascx.vb" Inherits="Forms_Report_lap_sales_order_list" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
    
<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Laporan Sales Order Listing (by date)" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl2100" Text="Period" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl34" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_awal" Width="65" Font-Names="Tahoma" Font-Size="8" />
        <act:CalendarExtender ID="ce_tgl_awal" TargetControlID="tb_tgl_awal" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
        <td><asp:Label runat="server" ID="Label1" Text=" to " Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_akhir" Width="65" Font-Names="Tahoma" Font-Size="8" />
        <act:CalendarExtender ID="ce_tgl_akhir" TargetControlID="tb_tgl_akhir" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
        <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_awal" />
        <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_akhir" />
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label4" Text="Format" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td colspan="3">
            <asp:RadioButton runat="server" ID="RadioButtonPDF" Text="PDF" GroupName="pilihan"
                Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
            <asp:RadioButton runat="server" ID="RadioButtonExcel" Text="Excel" GroupName="pilihan"
                Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
        </td>
    </tr>
</table>
<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_print13" Text="Print Jan-Mar 2012 (A3 Rotated - Portrait)" Font-Names="Tahoma" Font-Size="8" Visible="False"/>
            <asp:Button runat="server" ID="btn_print" Text="Print (A3 Rotated - Portrait)" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>

<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\lap_sales_order_listing_by_date.rpt"></Report>
</CR:CrystalReportSource>