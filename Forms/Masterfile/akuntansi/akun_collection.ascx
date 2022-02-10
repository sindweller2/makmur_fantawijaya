<%@ Control Language="VB" AutoEventWireup="false" CodeFile="akun_collection.ascx.vb" Inherits="Forms_Masterfile_akuntansi_akun_collection" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
    
    function popup_coa(tcid1, tcid2) { 
                window.open('popup_coa.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Akun Collection" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label1" Text="Posisi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label20" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="lbl323" Text="Debet" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Akun piutang giro mundur" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="350"><asp:Label runat="server" ID="lbl_akun_piutang_giro_mundur" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_akun_piutang_giro_mundur" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_akun_piutang_giro_mundur" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_akun_piutang_giro_mundur" Text="Daftar COA" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Posisi" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label21" Text="Kredit" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Akun piutang dagang" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="350"><asp:Label runat="server" ID="lbl_akun_piutang_dagang" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_akun_piutang_dagang" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_akun_piutang_dagang" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_akun_piutang_dagang" Text="Daftar COA" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
