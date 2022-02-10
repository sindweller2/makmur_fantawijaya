<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_customer.ascx.vb" Inherits="Forms_Masterfile_customer_detil_customer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
    
    function popup_grup_customer(tcid1, tcid2) { 
                window.open('popup_grup_customer.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Data Customer" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl100" Text="Nama customer" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_name" Width="200" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Nama grup customer" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="350"><asp:Label runat="server" ID="lbl_nama_grup_customer" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_grup_customer" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_grup_customer" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_grup_customer" Text="Daftar grup customer" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Nama sales" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_sales" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Lama pembayaran" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_lama_pembayaran" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <asp:Label runat="server" ID="lbl2001" Text="hari" Font-Names="Tahoma" Font-Size="8" />
                        <ajax:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender11" FilterType="Custom, Numbers" TargetControlID="tb_lama_pembayaran" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="Limit hutang pembelian (Rp.)" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_limit_pembelian" Width="150" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" FilterType="Custom, Numbers" ValidChars="." TargetControlID="tb_limit_pembelian" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="No. NPWP" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_npwp" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="Tgl. awal" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_awal" Width="65" Font-Names="Tahoma" Font-Size="8" />
        <act:CalendarExtender ID="ce_tgl_awal" TargetControlID="tb_tgl_awal" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_awal" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label14" Text="Tgl. akhir" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_akhir" Width="65" Font-Names="Tahoma" Font-Size="8" />
         <act:CalendarExtender ID="ce_tgl_akhir" TargetControlID="tb_tgl_akhir" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_akhir" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label18" Text="Customer polos" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_is_polos" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Ya" Value="True"></asp:ListItem>
                <asp:ListItem Text="Tidak" Value="False"></asp:ListItem>
            </asp:DropDownList>
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label24" Text="Kawasan berikat ?" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label25" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_is_kawasan_berikat" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Tidak" Value="T"></asp:ListItem>
                <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label130" Text="Keterangan kawasan berikat" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label141" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_keterangan_kawasan_berikat" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl24" Text="Ekspor ?" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbll2345" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_is_ekspor" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
                <asp:ListItem Text="Tidak" Value="T"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Akun piutang giro mundur" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="350"><asp:Label runat="server" ID="lbl_nama_akun_giro_mundur" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_akun_giro_mundur" Font-Names="Tahoma" Font-Size="8pt" Visible="true" Enabled="False"/>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label22" Text="Akun piutang dagang" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label23" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="350"><asp:Label runat="server" ID="lbl_nama_akun_piutang_dagang" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_akun_piutang_dagang" Font-Names="Tahoma" Font-Size="8pt" Visible="false" Enabled="False"/>
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
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_alamat" Text="Alamat" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
