<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_pembayaran_invoice_supplier.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_pembayaran_invoice_supplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Pembayaran Invoice Supplier Impor T/T" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="Periode transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_periode" Font-Names="Tahoma" Font-Size="8"/></td>   
        <td><asp:Label runat="server" ID="Label7" Text="Tgl. terima" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:Label runat="server" ID="lbl_tgl_terima" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label26" Text="No. dokumen" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label27" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_no_dokumen" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="No. invoice" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_invoice" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td><asp:Label runat="server" ID="Label4" Text="Tgl. invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl34" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:Label runat="server" ID="lbl_tgl_invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label10" Text="Tgl. jatuh tempo invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_jatuh_tempo" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label5" Text="Nama supplier" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label12" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_nama_supplier" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label13" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label14" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_mata_uang" Font-Names="Tahoma" Font-Size="8"/></td>   
        <td><asp:Label runat="server" ID="Label16" Text="Nilai invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:Label runat="server" ID="lbl_nilai_invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text="Kurs" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label23" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_kurs" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label15" Text="Status bayar" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td><asp:Label runat="server" ID="Label18" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_status_bayar" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>   
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl203" Text="Pembayaran" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_pembayaran_ke" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="I" Value="1"></asp:ListItem>
                <asp:ListItem Text="II" Value="2"></asp:ListItem>
                <asp:ListItem Text="III" Value="3"></asp:ListItem>
                <asp:ListItem Text="IV" Value="4"></asp:ListItem>
                <asp:ListItem Text="V" Value="5"></asp:ListItem>
            </asp:DropDownList>
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label22" Text="Tgl. pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label29" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_bayar" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_bayar" />
        <act:CalendarExtender ID="ce_tgl_bayar" TargetControlID="tb_tgl_bayar" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>                
        <td><asp:Label runat="server" ID="Label24" Text="Nilai pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label25" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_nilai" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender3" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_nilai" />
        </td>                
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Jenis pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_jenis_pembayaran" Font-Names="Tahoma" Font-Size="8" /></td> 
        <td><asp:Label runat="server" ID="Label31" Text="Kas/Bank" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label32" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_bank" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="Label30" Text="No. Giro/Cek" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label33" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_giro" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>                
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label34" Text="Tgl. Giro/Cek" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label35" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_giro" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_giro" />
        <act:CalendarExtender ID="ce_tgl_giro" TargetControlID="tb_tgl_giro" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>    
        <td><asp:Label runat="server" ID="Label36" Text="Tgl.jatuh tempo Giro/Cek" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label37" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_jatuh_tempo" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_jatuh_tempo" />
            <act:CalendarExtender ID="ce_jatuh_tempo" TargetControlID="tb_jatuh_tempo" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>                            
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>    
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
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
                            <asp:Label runat="Server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq_pembayaran") %>' Visible="false" />
                            <asp:Label runat="Server" ID="lbl_is_submit_bayar" Text='<%#Databinder.Eval(Container, "Dataitem.is_submit_bayar") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_pembayaran_ke" Text="Pembayaran ke" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="Server" ID="lbl_pembayaran_ke" Text='<%#Databinder.Eval(Container, "Dataitem.pembayaran_ke") %>' Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_bayar" Text="Tgl. pembayaran" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="Server" ID="tb_tgl_bayar" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_bayar") %>' Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender221" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_bayar" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_bayar" Text="Nilai pembayaran" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="Server" ID="tb_nilai_bayar" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_bayar"),2) %>' Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender33" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_nilai_bayar" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jenis_bayar" Text="Jenis pembayaran" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="Server" ID="lbl_jenis_bayar" Text='<%#Databinder.Eval(Container, "Dataitem.id_jenis_pembayaran_invoice") %>' Visible="False" />
                            <asp:DropDownList runat="Server" ID="dd_jenis_bayar" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_bank" Text="Kas/Bank" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="Server" ID="lbl_bank" Text='<%#Databinder.Eval(Container, "Dataitem.id_bank_invoice") %>' Visible="False" />
                            <asp:DropDownList runat="Server" ID="dd_bank" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_giro" Text="No. Giro/Cek" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="Server" ID="tb_no_giro" Text='<%#Databinder.Eval(Container, "Dataitem.no_giro_invoice") %>' Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_giro" Text="Tgl. Giro/Cek" Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="Server" ID="tb_tgl_giro" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_giro_invoice") %>' Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender23" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_giro" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_jatuh_tempo_giro" Text="Tgl. Jth. tempo Giro/Cek" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="Server" ID="tb_tgl_jatuh_tempo_giro" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_jatuh_tempo_invoice") %>' Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender233" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_jatuh_tempo_giro" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:LinkButton runat="Server" ID="lbl_submit" Text="Submit" Width="65" Font-Names="Tahoma" Font-Size="8" CommandName="LinkSubmit" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>


