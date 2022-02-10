<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_dokumen_impor.ascx.vb" Inherits="Forms_Transaksi_Pembelian_detil_dokumen_impor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=700, height=400, left=100, top=25"; 
    
    function popup_po(tcid1, tcid2) { 
                window.open('popup_po.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
    function popup_lc(tcid1, tcid2) { 
                window.open('popup_lc.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>


<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Entry Dokumen Impor" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td width="150"><asp:Label runat="server" ID="Label13" Text="Periode transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label14" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_periode" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>    
    <tr>
        <td width="150"><asp:Label runat="server" ID="Label1" Text="No. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label2" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300""><asp:Label runat="server" ID="lbl_no_po" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_no_po" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_no_po" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_no_po" Text="Daftar PO" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
        <td width="200"><asp:Label runat="server" ID="Label3" Text="Tgl. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label4" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_pembelian" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label15" Text="Nama supplier" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label16" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="3"><asp:Label runat="server" ID="lbl_nama_supplier" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label9" Text="Pembelian L/C ?" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label10" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_is_lc" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text="No. L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label12" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_nomor_lc" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_no_lc" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_no_lc" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_no_lc" Text="Daftar L/C" Font-Names="Tahoma" Font-Size="8pt"/>
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label17" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label18" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="3"><asp:Label runat="server" ID="lbl_mata_uang" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label54" Text="Nomor penerimaan" Font-Names="Tahoma" Font-Size="8" font-bold="True"/></td>
        <td><asp:Label runat="server" ID="Label64" Text=":" Font-Names="Tahoma" Font-Size="8" font-bold="True"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_penerimaan" Font-Names="Tahoma" Font-Size="8" font-bold="True" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label5" Text="Tgl. terima" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label6" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_terima" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_terima" />
            <ajax:CalendarExtender ID="ce_tgl_terima" TargetControlID="tb_tgl_terima" runat="server" Format="dd/MM/yyyy" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label32" Text="No. B/L / AWB" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label33" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_bl" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label38" Text="No. invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label39" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_invoice" Width="150"  Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label40" Text="Tgl. invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label41" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:TextBox runat="server" ID="tb_tgl_invoice" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_invoice" />
            <ajax:CalendarExtender ID="ce_tgl_invoice" TargetControlID="tb_tgl_invoice" runat="server" Format="dd/MM/yyyy" />
        </td> 
        <td><asp:Label runat="server" ID="Label7" Text="Tgl. jatuh tempo invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:TextBox runat="server" ID="tb_tgl_jatuh_tempo" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_jatuh_tempo" />
            <ajax:CalendarExtender ID="ce_tgl_jatuh_tempo" TargetControlID="tb_tgl_jatuh_tempo" runat="server" Format="dd/MM/yyyy" />
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label30" Text="Nilai invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label31" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="00"><asp:TextBox runat="server" ID="tb_nilai_invoice" Width="150" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender3" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_nilai_invoice" />
        </td>
        <td><asp:Label runat="server" ID="Label19" Text="Nilai kurs bulanan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label20" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="00"><asp:TextBox runat="server" ID="tb_kurs" Width="75" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
            <asp:Button ID="btn_kurs_idr" runat="server" Text="IDR" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button ID="btn_kurs_usd" runat="server" Text="USD" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label42" Text="No. Packing List" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label43" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_packing_list" Width="150"  Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>       
</table>

<table align="center">
<tr>
        <td colspan="5"></td>
    </tr>
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_produk" Text="Item Produk" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_unsubmit" Text="Unsubmit" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
    <tr>
        <td colspan="5"></td>
    </tr>
</table>

<table align="center" runat="server" id="tbl_produk">
    <tr>
        <td>
            <table align="center">
                <tr>
                    <td>
                        <table runat="server" id="tbl_total_harga">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lbl1020" Text="Total nilai" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>                        
                                    <asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>
                                    <asp:Label runat="server" ID="lbl_total_nilai" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
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
                                        <asp:Label runat="server" ID="lb_qty" Text="Qty" width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="tb_qty" Text='<%#Databinder.Eval(Container, "Dataitem.qty") %>' Width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                                        <asp:Label runat="server" ID="lbl_satuan" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Font-Names="Tahoma" Font-Size="8pt"/>
                                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_qty_stock" Text="Qty stock" width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="tb_qty_stock" Text='<%#Databinder.Eval(Container, "Dataitem.qty_stock") %>' Width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender125" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty_stock" />
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