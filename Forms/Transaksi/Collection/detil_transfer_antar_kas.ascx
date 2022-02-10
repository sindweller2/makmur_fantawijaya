<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_transfer_antar_kas.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_transfer_antar_kas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Transfer Antar Kas/Bank" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td><asp:Label runat="server" ID="Label2" Text="Tgl. transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_transaksi" width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender11" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_transaksi" />
        <act:CalendarExtender ID="ce_tgl_transaksi" TargetControlID="tb_tgl_transaksi" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label23" Text="Tgl. transfer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label33" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_transfer" width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender113" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_transfer" />
        <act:CalendarExtender ID="ce_tgl_transfer" TargetControlID="tb_tgl_transfer" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
        <td><asp:Label runat="server" ID="Label233" Text="Tgl. masuk" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label333" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_masuk" width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1133" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_masuk" />
        <act:CalendarExtender ID="ce_tgl_masuk" TargetControlID="tb_tgl_masuk" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Nama kas/bank asal" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_kas_bank_asal" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
        <td><asp:Label runat="server" ID="Label12" Text="Mata uang asal" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_mata_uang_asal" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama kas/bank tujuan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_kas_bank_tujuan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
        <td><asp:Label runat="server" ID="Label16" Text="Mata uang tujuan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_mata_uang_tujuan" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="Nilai kas/bank awal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_nilai_awal" Width="150" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_nilai_awal" />
        </td>
        <td><asp:Label runat="server" ID="Label162" Text="Kurs kas/bank awal" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label172" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_kurs_awal" width="100" Font-Names="Tahoma" Font-Size="8"/>
<ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs_awal" />    
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label82" Text="Nilai kas/bank akhir" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label92" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_nilai_akhir" Width="150" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender13" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_nilai_akhir" />
        </td>
        <td><asp:Label runat="server" ID="Label18" Text="Kurs kas/bank akhir" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_kurs_akhir" width="100" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs_akhir" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label812" Text="Nilai biaya provisi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label912" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_nilai_biaya_provisi" Width="75" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender213" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_nilai_biaya_provisi" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Keterangan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_keterangan" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Status submit" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_status_submit" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>
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