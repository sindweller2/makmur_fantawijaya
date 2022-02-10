<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_pembayaran_biaya_lc.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_pembayaran_biaya_lc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Pembayaran Biaya L/C" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label13" Text="Periode transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label14" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_periode" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label15" Text="No. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label16" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_pembelian" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label17" Text="Tgl. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label18" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_pembelian" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label25" Text="Nama supplier" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label28" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="6"><asp:Label runat="server" ID="lbl_nama_supplier" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label1" Text="No. L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label2" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_lc" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label3" Text="Tgl. L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label4" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_lc" Width="65" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label23" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label24" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_mata_uang" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label26" Text="Total nilai pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label27" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_total_nilai_pembelian" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr> 
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td width="300">&nbsp;</td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label5" Text="Biaya L/C" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td>&nbsp;</td>
        <td width="300">&nbsp;</td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Komisi bank" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_komisi_bank" Width="100" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label9" Text="Ongkos kawat" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label10" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_ongkos_kawat" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="Porto Materai" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_porto_materai" Width="100" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label19" Text="Biaya courier" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label20" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_biaya_courier" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="Biaya L/C Amendment" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_biaya_lc_amendment" Width="100" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label39" Text="Total biaya L/C" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td><asp:Label runat="server" ID="Label40" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td><asp:Label runat="server" ID="lbl_total_biaya" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr> 
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td width="300">&nbsp;</td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label38" Text="Pembayaran biaya L/C" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td>&nbsp;</td>
        <td width="300">&nbsp;</td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label22" Text="Tgl. pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label29" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_bayar" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_bayar" />
        <act:CalendarExtender ID="ce_tgl_bayar" TargetControlID="tb_tgl_bayar" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>                
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label43" Text="Kurs bulanan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label44" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_kurs" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
            <asp:Button ID="btn_kurs_idr" runat="server" Text="IDR" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button ID="btn_kurs_usd" runat="server" Text="USD" Font-Names="Tahoma" Font-Size="8" />
        </td>               
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label41" Text="Jenis pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label42" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_jenis_pembayaran" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label31" Text="Kas/Bank" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label32" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_bank" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label30" Text="No. Giro/Cek" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label33" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_giro" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>                
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label34" Text="Tgl. Giro/Cek" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label35" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_giro" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_giro" />
            <act:CalendarExtender ID="ce_tgl_giro" TargetControlID="tb_tgl_giro" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>    
        <td><asp:Label runat="server" ID="Label36" Text="Tgl.jatuh tempo Giro/Cek" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label37" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_jatuh_tempo" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_jatuh_tempo" />
        <act:CalendarExtender ID="ce_jatuh_tempo" TargetControlID="tb_jatuh_tempo" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>                            
    </tr>
</table>    

<%--Daniel--%>
<table width="100%">
<tr>
 <td align="right" width="50%"><asp:Label runat="server" ID="Label45" Text="Account:" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
          <td align="left" width="50%">
              <asp:DropDownList ID="DropDownListAccount" runat="server" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
              <asp:ListItem Value="61.04">BIAYA PEMBELIAN IMPORT</asp:ListItem>
              <asp:ListItem Value="11.08">UANG MUKA LC/PEMBEL. IMP/B. PEMBEL. IMP</asp:ListItem>
              </asp:DropDownList></td>
</tr>
</table>
<%--Daniel--%>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
