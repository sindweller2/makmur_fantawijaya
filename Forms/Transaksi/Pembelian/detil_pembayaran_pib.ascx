<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_pembayaran_pib.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_pembayaran_lc" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Pembayaran PIB" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td><asp:Label runat="server" ID="lbl_periode" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label15" Text="No. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label16" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_no_pembelian" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
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
        <td><asp:Label runat="server" ID="lbl_no_lc" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label3" Text="Tgl. L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label4" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_lc" Width="65" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label23" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label24" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_mata_uang" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label26" Text="Total nilai pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label27" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_total_nilai_pembelian" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="Dokumen Impor" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label21" Text="No. dokumen" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label22" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_no_dokumen" Width="65" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label39" Text="No. B/L / AWB" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label40" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_no_bl" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label42" Text="No. Packing List" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label43" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_no_packing_list" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label31" Text="No. invoce" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label38" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_no_invoice" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label44" Text="Tgl. invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label45" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_invoice" Font-Names="Tahoma" Font-Size="8" /></td> 
        <td><asp:Label runat="server" ID="Label41" Text="Nilai invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label46" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_nilai_invoice" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    
    <tr>
        <td><asp:Label runat="server" ID="Label5" Text="Pembayaran PIB" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label53" Text="Tanggal pembayaran" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label54" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_tgl_pembayaran" Width="65" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Kas/Bank" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_bank" Font-Names="Tahoma" Font-Size="8" Enabled="false" /></td>
    </tr>     
    <tr>
        <td><asp:Label runat="server" ID="Label55" Text="BIAYA IMPOR" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>        
    </tr>
    <tr>
        <td><asp:Button runat="server" ID="btn_hitung" Text="HITUNG BIAYA IMPOR" Font-Names="Tahoma" Font-Size="8"/></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>        
    </tr>
    <tr>    
        <td><asp:Label runat="server" ID="lbl323" Text="Bea Masuk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label30" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_bea_masuk" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_bea_masuk" />
        </td>
        <td><asp:Label runat="server" ID="Label6" Text="PPN Impor" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_ppn_impor" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_ppn_impor" />
        </td>
        <td><asp:Label runat="server" ID="Label19" Text="PPH Ps22" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label20" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_pph_ps22" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender23" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_pph_ps22" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label56" Text="BIAYA LAIN-LAIN" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="Biaya Adm. PIB" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_biaya_adm_pib" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender123" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_biaya_adm_pib" />
        </td> 
        <td><asp:Label runat="server" ID="Label47" Text="PNBP" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label48" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_pnbp" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender3" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_pnbp" />
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label49" Text="Biaya Dokumen" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label50" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_biaya_dokumen" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender4" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_biaya_dokumen" />
        </td> 
        <td><asp:Label runat="server" ID="Label51" Text="Shipping Guarantee" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label52" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_shipping_guarantee" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender5" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_shipping_guarantee" />
        </td> 
    </tr>
    <tr>
        <td><asp:Button runat="server" ID="btn_produk" Text="View Produk Item" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>    
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
</table>

<table align="center" runat="server" id="tbl_produk">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                            <asp:Label runat="server" ID="lbl_id_produk" Text='<%#Databinder.Eval(Container, "Dataitem.id_product") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_name" Text="Nama produk" width="200" Font-Names="Tahoma" Font-Size="8pt"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_name" Text='<%#Databinder.Eval(Container, "Dataitem.nama_product") %>' Width="200" Font-Names="Tahoma" Font-Size="8pt"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_packaging" Text="Packaging" width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_packaging" Text='<%#Databinder.Eval(Container, "Dataitem.packaging") %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_qty" Text="Qty" width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_qty" Text='<%#Databinder.Eval(Container, "Dataitem.qty") %>' Width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                            <asp:Label runat="server" ID="lbl_satuan" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Font-Names="Tahoma" Font-Size="8pt"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_harga_jual" Text="Harga" width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_harga_jual" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.unit_price"),3) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1334" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_harga_jual" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_discount" Text="Discount(%)" width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_discount" Text='<%#Databinder.Eval(Container, "Dataitem.discount") %>' Width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender13343" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_harga_jual" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_sub_total" Text="Sub total" width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_sub_total" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.sub_total"),3) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>
