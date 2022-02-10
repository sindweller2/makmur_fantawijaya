<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_supplier_produk.ascx.vb" Inherits="Forms_Masterfile_detil_supplier_produk" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
    
    function popup_grup_customer(tcid1, tcid2) { 
                window.open('popup_grup_customer.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Data Supplier Produk" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl100" Text="Nama supplier" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_name" Width="200" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Alamat" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_alamat" Width="350" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="No. telp" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_telp" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label14" Text="No. fax" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_fax" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Discount" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_discount" Width="50" MaxLength="3" Font-Names="Tahoma" Font-Size="8" />
            <asp:Label runat="server" ID="lbl2001" Text="%" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender11" FilterType="Custom, Numbers" TargetControlID="tb_discount" />
        </td>
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
        <td><asp:Label runat="server" ID="Label3" Text="Plus/Minus" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label10" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_plus_minus" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <asp:Label runat="server" ID="Label11" Text="%" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2" FilterType="Custom, Numbers" TargetControlID="tb_plus_minus" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label22" Text="Jenis pembelian" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label23" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_jenis_pembelian" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="L/C" Value="Y"></asp:ListItem>
                <asp:ListItem Text="Non L/C" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </td>
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
    <tr>
        <td><asp:Label runat="server" ID="Label18" Text="Bank supplier" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_nama_bank" Width="500" Height="100" TextMode="MultiLine" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Akun hutang dagang" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="350"><asp:Label runat="server" ID="lbl_nama_akun_hutang_dagang" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_akun_hutang_dagang" Font-Names="Tahoma" Font-Size="8pt" Visible="true" Enabled="False"/>
        </td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_kontak_person" Text="Kontak person" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
