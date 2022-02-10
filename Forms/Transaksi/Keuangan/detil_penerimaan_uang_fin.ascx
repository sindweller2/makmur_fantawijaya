<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_penerimaan_uang_fin.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_penerimaan_uang_fin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Penerimaan Uang" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td width="100"><asp:Label runat="server" ID="Label22" Text="Periode transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label23" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_periode_transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td width="100"><asp:Label runat="server" ID="Label20" Text="No. transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="100"><asp:Label runat="server" ID="lbl203" Text="Tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label4" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:Label runat="server" ID="lbl_tanggal" Width="65" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td width="100"><asp:Label runat="server" ID="Label1" Text="Tgl. transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label2" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_transaksi" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_transaksi" />
            <act:CalendarExtender ID="ce_tgl_transaksi" TargetControlID="tb_tgl_transaksi" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
    </tr>     
    <tr>
        <td width="100"><asp:Label runat="server" ID="Label8" Text="Jenis penerimaan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_jenis_penerimaan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
        <td width="100"><asp:Label runat="server" ID="Label10" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="100"><asp:Label runat="server" ID="Label3" Text="Kurs" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label24" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_kurs" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
        </td>
    </tr>
    <tr>
        <td width="100"><asp:Label runat="server" ID="lb394" Text="Nama kas/bank" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label6" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_kas_bank" Font-Names="Tahoma" Font-Size="8"/></td>        
   <td>
            <asp:Label runat="server" ID="Label26" Text="Jenis Transaksi" Font-Names="Tahoma" Font-Size="8" />
            </td>
        <td>
            :
        </td>
        <td>
            <asp:DropDownList ID="DropDownListJenisTransaksi" runat="server" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="Biaya">Biaya</asp:ListItem>
                <asp:ListItem Value="Pendapatan">Pendapatan</asp:ListItem>
                <asp:ListItem Value="Penerimaan Piutang Karyawan">Penerimaan Piutang Karyawan</asp:ListItem>
                <asp:ListItem Value="Penerimaan Hutang">Penerimaan Hutang</asp:ListItem>
            </asp:DropDownList>
        </td>
   </tr>      
    <tr>
        <td width="100"><asp:Label runat="server" ID="Label14" Text="No. cek/giro" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_cek_giro" Width="150" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="100"><asp:Label runat="server" ID="Label16" Text="Tgl. cek/giro" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:TextBox runat="server" ID="tb_tgl_cek_giro" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_cek_giro" />
        <act:CalendarExtender ID="ce_tgl_cek_giro" TargetControlID="tb_tgl_cek_giro" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
        <td width="100"><asp:Label runat="server" ID="Label18" Text="Tgl. jatuh cek/giro" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_jatuh_tempo" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_jatuh_tempo" />
        <act:CalendarExtender ID="ce_tgl_jatuh_tempo" TargetControlID="tb_tgl_jatuh_tempo" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
    </tr>    
    <tr>       
        <td width="100"><asp:Label runat="server" ID="Label5" Text="Jumlah nilai" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_nilai" Width="100" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_nilai" />
        </td>
    </tr>
    <tr>
        <td width="100"><asp:Label runat="server" ID="Label12" Text="Keterangan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_keterangan" Width="250" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>

<table align="center" runat="server" id="tbl_item_pendapatan">
    <tr>
        <td>
            <table align="center">
                <tr>
                    <td><asp:Label runat="server" ID="lbl83" Text="Nama" Font-Names="Tahoma" Font-Size="8pt"/></td>
                    <td><asp:Label runat="server" ID="Label25" Text="Jumlah" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>
                </tr>
                <tr>
                    <td><asp:DropDownList runat="server" ID="dd_pendapatan" Font-Names="Tahoma" Font-Size="8" /></td>
                    <td><asp:TextBox runat="server" ID="tb_jumlah" Width="100" Font-Names="Tahoma" Font-Size="8pt"/>                        
                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_jumlah" />
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
                                        <asp:Label runat="server" ID="lbl_id_pendapatan" Text='<%#Databinder.Eval(Container, "Dataitem.id_item_pendapatan") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_name" Text="Nama" width="200" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>                                        
                                        <asp:Label runat="server" ID="lbl_item_pendapatan" Text='<%#Databinder.Eval(Container, "Dataitem.nama_item") %>' width="200" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_jumlah" Text="Jumlah" width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="tb_jumlah" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender13" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_jumlah" />
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


