<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_pembayaran_lc.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_pembayaran_lc" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Pembayaran L/C" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label13" Text="Periode transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label14" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_periode" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label15" Text="No. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label16" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_pembelian" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label17" Text="Tgl. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label18" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_pembelian" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label25" Text="Nama supplier" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label28" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="6"><asp:Label runat="server" ID="lbl_nama_supplier" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label1" Text="No. L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label2" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_lc" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label3" Text="Tgl. L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label4" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_lc" Width="65" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label32" Text="Berlaku sampai dengan tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label33" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_tgl_berlaku_lc" Width="65" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label34" Text="Jenis L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label35" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_jenis_lc" Font-Names="Tahoma" Font-Size="8" Enabled="false" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label36" Text="Tanggal jatuh tempo L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label37" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_tgl_jatuh_tempo_lc" Width="65" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label23" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label24" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_mata_uang" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label232" Text="Total nilai L/C" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label242" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_total_nilai_pembelian" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label231" Text="Total nilai invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label241" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_total_nilai_invoice" Font-Names="Tahoma" Font-Size="8"/></td>        
        <td><asp:Label runat="server" ID="Label344" Text="Status lunas" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label355" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_status_lunas" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>                
    <tr>
        <td><asp:Label runat="server" ID="Label5" Text="Pembayaran L/C" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td>&nbsp;</td>
        <td width="300">&nbsp;</td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Pembayaran L/C ke" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_pembayaran_ke" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="I" Value="1"></asp:ListItem>
                <asp:ListItem Text="II" Value="2"></asp:ListItem>
                <asp:ListItem Text="III" Value="3"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td><asp:Label runat="server" ID="Label82" Text="Jenis pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label93" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_jenis_bayar" Font-Names="Tahoma" Font-Size="8" >
              <asp:ListItem Text="L/C" Value="L"></asp:ListItem>
                <asp:ListItem Text="T/R" Value="T"></asp:ListItem>
            </asp:DropDownList>
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="Kurs" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_kurs" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label19" Text="Jumlah % pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label20" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_prosentase" Width="50" MaxLength="3" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_hitung" Text="Hitung nilai" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender23" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_prosentase" />
        </td>
        <td><asp:Label runat="server" ID="Label8" Text="Jumlah nilai pembayaran USD" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_jumlah_pembayaran" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender123" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_jumlah_pembayaran" />
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Jumlah nilai pembayaran IDR" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_nilai_idr" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>     
        <tr>
        <td><asp:Button runat="server" ID="btn_hitung_idr" Text="Hitung jumlah IDR" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>     
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:checkbox runat="server" ID="cb_data" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_pembayaran_ke" Text="Pembayaran ke" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.pembayaran_ke") %>' Visible="false"/> 
                            <asp:Label runat="server" ID="lbl_pembayaran_ke" Text='<%#Databinder.Eval(Container, "Dataitem.pembayaran_ke_nama") %>' Width="75" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jenis_bayar" Text="Jenis pembayaran" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jenis_bayar" Text='<%#Databinder.Eval(Container, "Dataitem.jenis_bayar") %>' Visible="false"/> 
                            <asp:DropDownList runat="server" ID="dd_jenis_bayar" Font-Names="Tahoma" Font-Size="8" >
                                 <asp:ListItem Text="L/C" Value="L"></asp:ListItem>
                                 <asp:ListItem Text="T/R" Value="T"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kurs" Text="Kurs" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_kurs" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.kurs"),2) %>' Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender13" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_prosentase" Text="Jml. % pembayaran" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_prosentase" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.prosentase"),2) %>' Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12343" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_prosentase" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jumlah" Text="Jml. nilai pembayaran USD" Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_jumlah" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah_nilai"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1234" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_jumlah" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jumlah" Text="Jml. nilai pembayaran IDR" Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jumlah_idr" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah_nilai_idr"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_bayar" Text="Tgl. bayar" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tgl_bayar" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal_bayar") %>' Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_bank" Text="Kas/Bank" Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_bank" Text='<%#Databinder.Eval(Container, "Dataitem.id_bank") %>' Visible="false"/> 
                            <asp:DropDownList runat="server" ID="dd_bank" Font-Names="Tahoma" Font-Size="8pt" Enabled="false"/>                            
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
</table>