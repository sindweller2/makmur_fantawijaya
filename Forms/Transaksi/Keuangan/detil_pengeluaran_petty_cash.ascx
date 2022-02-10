<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_pengeluaran_petty_cash.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_pengeluaran_petty_cash" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Pengeluaran Petty Cash" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="Periode transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_periode" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="No. permintaan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="150"><asp:Label runat="server" ID="lbl_no_permintaan" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label4" Text="Tanggal permintaan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_permintaan" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Tunai/Non tunai" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="150"><asp:Label runat="server" ID="lbl_tunai_non_tunai" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label8" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="150"><asp:Label runat="server" ID="lbl_mata_uang" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label10" Text="Jumlah" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_jumlah" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="Keterangan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_keterangan" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label14" Text="Kas tujuan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_kas_untuk" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label16" Text="Kas/Bank asal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_kas_dari" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="No. Cek/Giro" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="150"><asp:TextBox runat="server" ID="tb_no_cek_giro" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label22" Text="Tgl. Cek/Giro" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label23" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_cek_giro" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_cek_giro" />
        <act:CalendarExtender ID="ce_tgl_cek_giro" TargetControlID="tb_tgl_cek_giro" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label24" Text="Jumlah dikeluarkan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label25" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_jumlah_dikeluarkan" width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_jumlah_dikeluarkan" />
        </td>        
    </tr>
    <tr>    
        <td><asp:Label runat="server" ID="Label18" Text="Tgl. dikeluarkan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_dikeluarkan" width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_dikeluarkan" />
        <act:CalendarExtender ID="ce_tgl_dikeluarkan" TargetControlID="tb_tgl_dikeluarkan" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>        
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>