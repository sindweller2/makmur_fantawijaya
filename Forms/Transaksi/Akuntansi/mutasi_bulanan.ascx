<%@ Control Language="VB" AutoEventWireup="false" CodeFile="mutasi_bulanan.ascx.vb"
    Inherits="Forms_Transaksi_Akuntansi_mutasi_bulanan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Mutasi Bulanan" Font-Names="Tahoma" Font-Size="14"
                Font-Bold="true" /></td>
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
            <asp:Label runat="server" ID="lbl111" Text="Tahun asal" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tahun_from" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun_from" />
            <asp:Button runat="server" ID="btn_view_from" Text="View" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label2" Text="Bulan asal" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_bulan_from" Font-Names="Tahoma" Font-Size="8"
                AutoPostBack="true" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label4" Text="Tahun tujuan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tahun_to" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun_to" />
            <asp:Button runat="server" ID="btn_view_to" Text="View" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label6" Text="Bulan tujuan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_bulan_to" Font-Names="Tahoma" Font-Size="8"
                AutoPostBack="true" /></td>
    </tr>
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="3" align="center">
            <asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
