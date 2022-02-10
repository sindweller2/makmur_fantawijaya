<%@ Control Language="VB" AutoEventWireup="false" CodeFile="lap_sales_detail_by_customer.ascx.vb" Inherits="Forms_Report_lap_sales_detail_by_customer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
        
    function popup_customer(tcid1, tcid2) { 
                window.open('popup_customer.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                                
</script>

    
<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Laporan Sales Detail by Customer" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl2100" Text="Period" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl34" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_awal" Width="65" Font-Names="Tahoma" Font-Size="8" />
        <act:CalendarExtender ID="ce_tgl_awal" TargetControlID="tb_tgl_awal" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
        <td><asp:Label runat="server" ID="Label1" Text=" to " Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_akhir" Width="65" Font-Names="Tahoma" Font-Size="8" />
        <act:CalendarExtender ID="ce_tgl_akhir" TargetControlID="tb_tgl_akhir" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
        <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_awal" />
        <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_akhir" />
    </tr>
    <tr>
	<td><asp:Label runat="server" ID="lbl210" Text="Pilihan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl344" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" id="dd_pilihan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="True">
                <asp:listitem runat="server" Text="Semua customer" Value="A"/>
                <asp:listitem runat="server" Text="Per customer" Value="C"/>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama customer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="3"><asp:Label runat="server" ID="lbl_nama_customer" Font-Names="Tahoma" Font-Size="8" />
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
    <Report FileName="reports\lap_sales_order_listing_by_date.rpt"></Report>
</CR:CrystalReportSource>