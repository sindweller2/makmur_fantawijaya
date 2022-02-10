<%@ Control Language="VB" AutoEventWireup="false" CodeFile="history_pembelian_customer.ascx.vb" Inherits="Forms_Transaksi_Keuangan_history_pembelian_customer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
        
    function popup_customer(tcid1, tcid2) { 
                window.open('popup_customer.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>
    
<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Histori Pembelian Customer" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl33" Text="Tahun" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
        <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama customer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="350"><asp:Label runat="server" ID="lbl_nama_customer" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_customer" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_customer" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_customer" Text="Daftar Customer" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>

<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\history_pembelian_customer.rpt"></Report>
</CR:CrystalReportSource>