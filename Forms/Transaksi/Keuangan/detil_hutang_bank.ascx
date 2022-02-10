<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_hutang_bank.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_hutang_bank" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Penerimaan Hutang Bank" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label14" Text="Periode transaksi" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_periode_transaksi" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="No. transaksi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_transaksi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label2" Text="Tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tanggal" width="65" Font-Names="Tahoma" Font-Size="8"/>
	    <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal" />
        <act:CalendarExtender ID="ce_tanggal" TargetControlID="tb_tanggal" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label63" Text="Tgl. terima" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label93" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="6"><asp:TextBox runat="server" ID="tb_tgl_terima" width="65" Font-Names="Tahoma" Font-Size="8" />
	    <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender12" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_terima" />
        <act:CalendarExtender ID="ce_tgl_terima" TargetControlID="tb_tgl_terima" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>            
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Kas/Bank" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="6"><asp:DropDownList runat="server" ID="dd_no_rekening" Font-Names="Tahoma" Font-Size="8" AutoPostBack="True" /></td>            
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Nama bank" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_nama_bank" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
     <tr>
        <td><asp:Label runat="server" ID="Label30" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label31" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="350" colspan="3"><asp:Label runat="server" ID="lbl_mata_uang" Font-Names="Tahoma" Font-Size="8" /></td>            
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label16" Text="Jumlah" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_jumlah" width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender4" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_jumlah" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label21" Text="Keterangan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label26" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_keterangan" width="350" Font-Names="Tahoma" Font-Size="8" />
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label18" Text="Status submit" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_status_submit" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>


