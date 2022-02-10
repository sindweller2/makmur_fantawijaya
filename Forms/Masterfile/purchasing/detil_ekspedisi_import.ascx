<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_ekspedisi_import.ascx.vb" Inherits="Forms_Masterfile_Purchasing_detil_ekspedisi_import" %>


<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Data Ekspedisi Import" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl100" Text="Nama ekspedisi" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_name" Width="200" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Alamat" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_alamat" Width="350" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Jenis layanan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_jenis_layanan" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Laut" Value="1"></asp:ListItem>
                <asp:ListItem Text="Udara" Value="2"></asp:ListItem>                
                <asp:ListItem Text="Laut/Udara" Value="3"></asp:ListItem>        
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="No. telepon" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_telp" Width="200" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label14" Text="No. fax" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_fax" Width="200" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label18" Text="Nama kontak person" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_kontak_person" Width="200" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="Lama pembayaran" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_lama_pembayaran" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <asp:Label runat="server" ID="Label2" Text="hari" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" FilterType="Custom, Numbers" TargetControlID="tb_lama_pembayaran" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label87" Text="Akung hutang lain-lain" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label97" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_akun_hutang_lain_lain" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label16" Text="Status" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_status" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Aktif" Value="1"></asp:ListItem>
                <asp:ListItem Text="Tidak aktif" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_biaya_jasa" Text="Daftar biaya jasa" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>


