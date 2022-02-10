<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_transaksi_pengeluaran_uang_lain2.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_transaksi_pengeluaran_uang_lain2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
    
    function popup_coa(tcid1, tcid2) { 
                window.open('popup_coa.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Transaksi Pengeluaran Uang Lain-Lain" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender22" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_transaksi" />
        <act:CalendarExtender ID="ce_tgl_transaksi" TargetControlID="tb_tgl_transaksi" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Nama Kas/Bank" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_kas_petty_cash" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
        <td><asp:Label runat="server" ID="Label6" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:Label runat="server" ID="lbl_mata_uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text="Kurs" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_kurs" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
        </td>
    </tr>
     <tr>
        <td><asp:Label runat="server" ID="Label29" Text="No. Giro/Cek" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label57" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_giro" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>         
        <td><asp:Label runat="server" ID="Label9" Text="No. Voucher" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label10" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_voucher" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>             
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label58" Text="Tgl. Giro/Cek" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label59" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_giro" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_giro" />
        <act:CalendarExtender ID="ce_tgl_giro" TargetControlID="tb_tgl_giro" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>    
        <td><asp:Label runat="server" ID="Label60" Text="Tgl.jatuh tempo Giro/Cek" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label61" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_jatuh_tempo" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_jatuh_tempo" />
            <act:CalendarExtender ID="ce_jatuh_tempo" TargetControlID="tb_jatuh_tempo" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>                            
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label291" Text="Keterangan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label571" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_keterangan_header" Width="350" Font-Names="Tahoma" Font-Size="8" /></td>                
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_unsubmit" Text="Unsubmit" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center" runat="server" id="tbl_petty_cash">
    <tr>
        <td>
            <table align="center">
                <tr>
                    <td><asp:Label runat="server" ID="Label4" Text="Keterangan" Font-Names="Tahoma" Font-Size="8"/></td>
                    <td><asp:Label runat="server" ID="lbl83" Text="Nama akun" Font-Names="Tahoma" Font-Size="8pt"/></td>
                    <td><asp:Label runat="server" ID="Label25" Text="Jumlah" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>
                </tr>
                <tr>
                    <td width="300"><asp:TextBox runat="server" ID="tb_keterangan" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>
                    <td width="350"><asp:Label runat="server" ID="lbl_akun" Font-Names="Tahoma" Font-Size="8" />
                        <asp:TextBox runat="server" ID="tb_id_akun" Font-Names="Tahoma" Font-Size="8pt"/>
                        <asp:LinkButton runat="server" ID="link_refresh_akun" Text="Refresh"/>
                        <asp:LinkButton runat="server" ID="link_popup_akun" Text="Daftar COA" Font-Names="Tahoma" Font-Size="8pt"/>
                    </td>
                    <td><asp:TextBox runat="server" ID="tb_jumlah" Width="100" Font-Names="Tahoma" Font-Size="8pt"/>                        
                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="-." TargetControlID="tb_jumlah" />
                        <asp:Button runat="server" ID="btn_add" Text="Add" Font-Names="Tahoma" Font-Size="8pt"/>
                    </td>                    
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table runat="server" id="tbl_total_harga">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl1020" Text="Total nilai" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>                        
                        <asp:Label runat="server" ID="Label27" Text=":" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>
                        <asp:Label runat="server" ID="lbl_total_nilai" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table align="center">
                <tr>
                    <td>
                        <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="cb_data" />                                        
                                        <asp:Label runat="server" ID="lbl_id_biaya" Text='<%#Databinder.Eval(Container, "Dataitem.id_item_biaya") %>' Visible="false" />
                                        <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_keterangan" Text="Keterangan" width="250" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>                                        
                                        <asp:TextBox runat="server" ID="tb_keterangan" Text='<%#Databinder.Eval(Container, "Dataitem.keterangan") %>' width="250" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_name" Text="Nama biaya" width="300" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>                                        
                                        <asp:Label runat="server" ID="lbl_item_biaya" Text='<%#Databinder.Eval(Container, "Dataitem.nama_biaya") %>' width="300" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_jumlah" Text="Jumlah" width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="tb_jumlah" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender13" FilterType="custom, numbers" ValidChars="-." TargetControlID="tb_jumlah" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8pt" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>