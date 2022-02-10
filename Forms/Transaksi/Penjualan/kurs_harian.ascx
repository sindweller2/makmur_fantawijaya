<%@ Control Language="VB" AutoEventWireup="false" CodeFile="kurs_harian.ascx.vb" Inherits="Forms_Transaksi_Penjualan_kurs_harian" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Kurs harian" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label1" Text="Tanggal" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label2" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_hari_ini" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="lbl3030" Text="Kurs harian" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_kurs_harian" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_kurs_harian" />            
        </td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>
            <asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />        
        </td>
    </tr>
</table>