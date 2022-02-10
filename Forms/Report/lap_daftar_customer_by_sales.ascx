<%@ Control Language="VB" AutoEventWireup="false" CodeFile="lap_daftar_customer_by_sales.ascx.vb"
    Inherits="Forms_Report_lap_daftar_customer_by_sales" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
        
    function popup_sales(tcid1, tcid2) { 
                window.open('popup_sales.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                                
</script>

<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Laporan Daftar Customer by Sales" Font-Names="Tahoma"
                Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl210" Text="Pilihan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="lbl344" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_pilihan" Font-Names="Tahoma" Font-Size="8"
                AutoPostBack="True">
                <asp:ListItem runat="server" Text="Semua sales" Value="A" />
                <asp:ListItem runat="server" Text="Per sales" Value="C" />
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label6" Text="Nama sales" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td colspan="3">
            <asp:Label runat="server" ID="lbl_nama_sales" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="code" Font-Names="Tahoma" Font-Size="8pt" />
            <asp:LinkButton runat="server" ID="link_refresh_sales" Text="Refresh" />
            <asp:LinkButton runat="server" ID="link_popup_sales" Text="Daftar Sales" Font-Names="Tahoma"
                Font-Size="8pt" />
        </td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\lap_daftar_customer_by_all_sales.rpt">
    </Report>
    <Report FileName="reports\lap_daftar_customer_by_sales.rpt">
    </Report>
</CR:CrystalReportSource>
