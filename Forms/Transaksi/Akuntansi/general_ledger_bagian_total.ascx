<%@ Control Language="VB" AutoEventWireup="false" CodeFile="general_ledger_bagian_total.ascx.vb" Inherits="Forms_Transaksi_Akuntansi_general_ledger_bagian_total" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\general_ledger_all.rpt"></Report>
</CR:CrystalReportSource>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
    
    function popup_coa(tcid1, tcid2) { 
                window.open('popup_coa.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>


<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="General Ledger Total Per Bagian" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Tahun transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
        <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_tahun" Text="Refresh bulan" Font-Names="Tahoma" Font-Size="8" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="Bulan transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/>
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8" Visible="false" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Bagian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_bagian" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                <asp:ListItem Text="Sales Admin" Value="S"></asp:ListItem>
                <asp:ListItem Text="Collection" Value="C"></asp:ListItem>
                <asp:ListItem Text="Import" Value="I"></asp:ListItem>
                <asp:ListItem Text="Finance" Value="F"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label23" Text="Transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label33" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_transaksi" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
    </tr>
</table>
            
<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_coa_code" Text="Nama akun" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_akun" Text='<%#Databinder.Eval(Container, "Dataitem.coa_name") %>' Width="150" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_coa_code_lawan" Text="Nama akun lawan" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_akun_lawan" Text='<%#Databinder.Eval(Container, "Dataitem.coa_lawan_name") %>' Width="150" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_debet" Text="Nilai debet" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nilai_debet" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.total_nilai_debet"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_kredit" Text="Nilai kredit" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nilai_kredit" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.total_nilai_kredit"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>                                        
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>

