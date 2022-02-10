<%@ Control Language="VB" AutoEventWireup="false" CodeFile="print_invoice_faktur_pajak.ascx.vb" Inherits="Forms_Transaksi_Penjualan_print_invoice_faktur_pajak" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Print Invoice dan Faktur Pajak" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl112" Text="Tahun" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tahun" Width="35" Font-Names="Tahoma" Font-Size="8" />
        <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="No. invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_nomor_awal" Width="75" Font-Names="Tahoma" Font-Size="8" />
            <asp:Label runat="server" ID="Label4" Text=" s.d " Font-Names="Tahoma" Font-Size="8"/>
            <asp:TextBox runat="server" ID="tb_nomor_akhir" Width="75" Font-Names="Tahoma" Font-Size="8" />
        </td>        
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_print_invoice" Text="Print invoice (LittlePaper1)" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_print_faktur" Text="Print faktur pajak (Letter)" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>

<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\faktur_pajak_print.rpt"></Report>
</CR:CrystalReportSource>
