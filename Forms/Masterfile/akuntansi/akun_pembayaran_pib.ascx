<%@ Control Language="VB" AutoEventWireup="false" CodeFile="akun_pembayaran_pib.ascx.vb" Inherits="Forms_Masterfile_akun_pembayaran_pib" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
    
    function popup_coa(tcid1, tcid2) { 
                window.open('popup_coa.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Akun Pembayaran PIB" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label7" Text="Biaya impor" Font-Names="Tahoma" Font-Size="10" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="Bea masuk" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label1" Text="Posisi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label20" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="lbl323" Text="Debet" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Akun biaya impor" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="350"><asp:Label runat="server" ID="lbl_akun_bea_masuk" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_akun_bea_masuk" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_akun_bea_masuk" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_akun_bea_masuk" Text="Daftar COA" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Posisi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label21" Text="Kredit" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Akun Kas/Bank" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label9" Text="PPN Impor" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Posisi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label12" Text="Debet" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label13" Text="Akun PPN Masukan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label14" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="350"><asp:Label runat="server" ID="lbl_akun_ppn_masukan" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_akun_ppn_masukan" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_akun_ppn_masukan" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_akun_ppn_masukan" Text="Daftar COA" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label16" Text="Posisi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label18" Text="Kredit" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label19" Text="Akun Kas/Bank" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label22" Text="PPh pasal 22" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label23" Text="Posisi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label24" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label25" Text="Debet" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label26" Text="Akun PPh pasal 22" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label27" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="350"><asp:Label runat="server" ID="lbl_akun_pph22" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_akun_pph22" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_akun_pph22" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_akun_pph22" Text="Daftar COA" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label29" Text="Posisi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label30" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label31" Text="Kredit" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label32" Text="Akun Kas/Bank" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label33" Text="Biaya lain-lain" Font-Names="Tahoma" Font-Size="10"/></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label34" Text="Posisi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label35" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label36" Text="Debet" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label37" Text="Akun biaya impor" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label38" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="350"><asp:Label runat="server" ID="lbl_akun_biaya_lain" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_akun_biaya_lain" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_akun_biaya_lain" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_akun_biaya_lain" Text="Daftar COA" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label40" Text="Posisi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label41" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label42" Text="Kredit" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label43" Text="Akun Kas/Bank" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
