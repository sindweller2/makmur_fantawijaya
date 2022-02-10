<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_lc.ascx.vb" Inherits="Forms_Transaksi_Pembelian_detil_lc" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=700, height=400, left=100, top=25"; 
        
    function popup_po(tcid1, tcid2) { 
                window.open('popup_po.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>
    
<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Data L/C" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td width="300"><asp:Label runat="server" ID="lbl_no_pembelian" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_no_pembelian" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_pembelian" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_pembelian" Text="Daftar PO" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
        <td><asp:Label runat="server" ID="Label17" Text="Tgl. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label18" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_pembelian" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label23" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label24" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_mata_uang" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text="Nilai pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_nilai_pembelian" Font-Names="Tahoma" Font-Size="8" /></td>         
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label25" Text="Nama supplier" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label28" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="6"><asp:Label runat="server" ID="lbl_nama_supplier" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label9" Text="Nama bank" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label10" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="6"><asp:DropDownList runat="server" ID="dd_bank" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label1" Text="No. L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label2" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_lc" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label3" Text="Tgl. L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label4" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_lc" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_lc" />
            <ajax:CalendarExtender ID="ce_tgl_lc" TargetControlID="tb_tgl_lc" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label363" Text="Nilai L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label373" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_nilai_lc" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender23" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_nilai_lc" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label32" Text="Berlaku sampai dengan tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label33" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_berlaku_lc" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_berlaku_lc" />
            <ajax:CalendarExtender ID="ce_tgl_berlaku_lc" TargetControlID="tb_tgl_berlaku_lc" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>        
        <td><asp:Label runat="server" ID="Label34" Text="Jenis L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label35" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_jenis_lc" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label36" Text="Tanggal jatuh tempo L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label37" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_jatuh_tempo_lc" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender5" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_jatuh_tempo_lc" />
            <ajax:CalendarExtender ID="ce_tgl_jatuh_tempo_lc" TargetControlID="tb_tgl_jatuh_tempo_lc" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label38" Text="Negara koresponden" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label39" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_negara_koresponden" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label40" Text="Dikapalkan dari" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label41" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_dikapalkan_dari" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label42" Text="Pelabuhan tujuan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label43" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_pelabuhan_tujuan" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label44" Text="Negara asal barang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label45" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_negara_asal" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label5" Text="Remarks" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label6" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_remarks" Width="350" Height="75" TextMode="MultiLine" Font-Names="Tahoma" Font-Size="8"/></td>        
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_print" Text="Print Pengajuan L/C (Legal)" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_lampiran" Text="Print Lampiran (A4)" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
</table>


<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\lc.rpt"></Report>
</CR:CrystalReportSource>