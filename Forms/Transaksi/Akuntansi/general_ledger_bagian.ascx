<%@ Control Language="VB" AutoEventWireup="false" CodeFile="general_ledger_bagian.ascx.vb" Inherits="Forms_Transaksi_Akuntansi_general_ledger_bagian" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
    
<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\general_ledger_per_bagian.rpt"></Report>
</CR:CrystalReportSource>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
    
    function popup_coa(tcid1, tcid2) { 
                window.open('popup_coa.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>


<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="General Ledger Per Bagian" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="Tanggal transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tanggal" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender11" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal" />
            <ajax:CalendarExtender ID="ce_tanggal" TargetControlID="tb_tanggal" runat="server" Format="dd/MM/yyyy" />
            <asp:Label runat="server" ID="Label12" Text=" s.d " Font-Names="Tahoma" Font-Size="8"/>
            <asp:TextBox runat="server" ID="tb_tanggal_akhir" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender113" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal_akhir" />
            <ajax:CalendarExtender ID="ce_tgl_akhir" TargetControlID="tb_tanggal_akhir" runat="server" Format="dd/MM/yyyy" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8" />
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

<table width="900" align="center" runat="server" id="tbl_search">
    <tr>
        <td colspan="3">
            <table>
                <tr>
                    <td width="30%"><asp:Label runat="server" ID="Label5" Text="Total debet" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/>
                        <asp:Label runat="server" ID="Label6" Text=":" Font-Names="Tahoma" Font-Size="8"/>
                        <asp:Label runat="server" ID="lbl_total_debet" Font-Names="Tahoma" Font-Size="8"/></td>
                    <td width="30%"><asp:Label runat="server" ID="Label7" Text="Total kredit" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/>
                        <asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8"/>
                        <asp:Label runat="server" ID="lbl_total_kredit" Font-Names="Tahoma" Font-Size="8"/></td>
                    <td width="30%"><asp:Label runat="server" ID="Label9" Text="Saldo" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/>
                        <asp:Label runat="server" ID="Label10" Text=":" Font-Names="Tahoma" Font-Size="8"/>
                        <asp:Label runat="server" ID="lbl_saldo" Font-Names="Tahoma" Font-Size="8"/></td>
                </tr>
            </table>
        </td>        
    </tr>

    <tr>
        <td colspan="3">
            <table>
                <tr>
                    <td><asp:Label runat="server" ID="Label52" Text="Nilai" Font-Names="Tahoma" Font-Size="8"/>
                        <asp:Label runat="server" ID="Label62" Text=":" Font-Names="Tahoma" Font-Size="8"/>
                        <asp:TextBox runat="server" ID="tb_nilai" width="100" Font-Names="Tahoma" Font-Size="8"/>
			<asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8"/>
                    </td>
                </tr>
            </table>
        </td>        
    </tr>
</table>
            
<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_coa_code" Text="Nama akun" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_coa_code" Text='<%#Databinder.Eval(Container, "Dataitem.coa_code") %>' Visible="false"/>
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>' Visible="false"/>
                            <asp:Label runat="server" ID="lbl_nama_akun" Text='<%#Databinder.Eval(Container, "Dataitem.nama_akun") %>' Width="150" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_coa_code_lawan" Text="Nama akun lawan" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_coa_code_lawan" Text='<%#Databinder.Eval(Container, "Dataitem.coa_code_lawan") %>' Visible="false"/>
                            <asp:Label runat="server" ID="lbl_nama_akun_lawan" Text='<%#Databinder.Eval(Container, "Dataitem.nama_akun_lawan") %>' Width="150" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_id_transaksi" Text="No. transaksi" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id_transaksi" Text='<%#Databinder.Eval(Container, "Dataitem.id_transaksi") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kode_transaksi" Text="Kode transaksi" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_kode_transaksi" Text='<%#Databinder.Eval(Container, "Dataitem.kode_transaksi") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_transaksi" Text="Tgl. transaksi" Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tgl_transaksi" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_transaksi") %>' Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_debet" Text="Nilai debet" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nilai_debet" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_debet"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_kredit" Text="Nilai kredit" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nilai_kredit" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_kredit"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kurs" Text="Kurs" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_kurs" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.kurs"),2) %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_keterangan" Text="Keterangan" Width="175" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_keterangan" Text='<%#Databinder.Eval(Container, "Dataitem.keterangan") %>' Width="175" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" Visible="false" />
        </td>
    </tr>
</table>

