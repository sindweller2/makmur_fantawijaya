<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_purchase_order.ascx.vb" Inherits="Forms_Transaksi_Pembelian_detil_purchase_order" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=700, height=400, left=100, top=25"; 
        
    function popup_supplier(tcid1, tcid2) { 
                window.open('popup_supplier.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
    function popup_produk_item(tcid1, tcid2) { 
                window.open('popup_produk_item.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
    
    function popup_kontak_person_supplier(id_supplier, tcid1, tcid2) { 
                window.open('popup_kontak_person_supplier.aspx?vid_supplier=' + id_supplier + '&tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Pembelian Barang" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td><asp:Label runat="server" ID="lbl111" Text="No. pembelian" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_pembelian" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label2" Text="Tgl. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Jenis pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_jenis_pembelian" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Kredit" Value="K"></asp:ListItem>
                <asp:ListItem Text="Tunai" Value="T"></asp:ListItem>                
            </asp:DropDownList>
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama supplier" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="6"><asp:Label runat="server" ID="lbl_nama_supplier" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_supplier" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_supplier" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_supplier" Text="Daftar supplier" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>            
    </tr> 
     <tr>
        <td><asp:Label runat="server" ID="Label30" Text="Nama kontak person" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label31" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="350" colspan="3"><asp:Label runat="server" ID="lbl_nama_kontak_person" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_kontak_person" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_kontak_person" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_kontak_person" Text="Daftar kontak person" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>            
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label16" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label38" Text="PI No." Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label39" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_pi_no" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>
         <td><asp:Label runat="server" ID="Label12" Text="PPN" Font-Names="Tahoma" Font-Size="8" Visible="false"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8" Visible="false"/></td>
        <td><asp:DropDownList runat="server" ID="dd_ppn" Font-Names="Tahoma" Font-Size="8" Visible="false" >
                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                <asp:ListItem Text="10" Value="10"></asp:ListItem>
            </asp:DropDownList>
        </td>       
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label21" Text="Pembelian dengan L/C ?" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label26" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_is_lc" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                    <asp:ListItem Text="Ya" Value="True"></asp:ListItem>
                    <asp:ListItem Text="Tidak" Value="False"></asp:ListItem>                                        
                </asp:DropDownList>
        </td>
        <td><asp:Label runat="server" ID="Label28" Text="Termin pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label29" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_term_pembayaran" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label18" Text="Jenis pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_payment_type" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label4" Text="Lama pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_lama_pembayaran" Width="50" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Label runat="server" ID="Label7" Text="hari" Font-Names="Tahoma" Font-Size="8"/>
               <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender4" FilterType="custom, numbers" TargetControlID="tb_lama_pembayaran" />
        </td>          
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label32" Text="Termijn pengapalan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label33" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_term_of_shipment" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label34" Text="Pelabuhan tujuan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label35" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_port_of_destination" Font-Names="Tahoma" Font-Size="8"/></td>          
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label36" Text="Remarks" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label37" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_remarks" Width="350" Height="50" TextMode="MultiLine" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label8" Text="Estimasi tgl. terima barang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label20" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_estimasi_tgl_terima" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_estimasi_tgl_terima" />
            <ajax:CalendarExtender ID="ce_estimasi_tgl_terima" TargetControlID="tb_estimasi_tgl_terima" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label145" Text="Dibuat oleh" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label155" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_dibuat_oleh" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8pt" Visible="False"/></td>
        <td><asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center" runat="server" id="tbl_produk">
    <tr>
        <td>
            <table align="center">
                <tr>
                    <td><asp:Label runat="server" ID="lbl83" Text="Nama produk" Width="300" Font-Names="Tahoma" Font-Size="8pt"/></td>
                    <td><asp:Label runat="server" ID="Label25" Text="Packaging" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>
                    <td><asp:Label runat="server" ID="Label22" Text="Qty" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>                    
                    <td><asp:Label runat="server" ID="Label23" Text="Harga" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>
                    <td><asp:Label runat="server" ID="Label24" Text="Discount (%)" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>
                </tr>
                <tr>
                    <td width="350"><asp:TextBox runat="server" ID="tb_nama_produk" Width="250" Font-Names="Tahoma" Font-Size="8" />
                        <asp:TextBox runat="server" ID="tb_id_produk" Font-Names="Tahoma" Font-Size="8pt"/>
                        <asp:LinkButton runat="server" ID="link_refresh_produk" Text="Refresh"/>
                        <asp:LinkButton runat="server" ID="link_popup_produk" Text="Daftar Produk" Font-Names="Tahoma" Font-Size="8pt"/>
                    </td>
                    <td><asp:Label runat="server" ID="lbl_packaging" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>
                    <td><asp:TextBox runat="server" ID="tb_qty" Width="50" Font-Names="Tahoma" Font-Size="8pt"/>                        
                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_qty" />
                        <asp:Label runat="server" ID="lbl_satuan" Width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                    </td>                    
                    <td><asp:TextBox runat="server" ID="tb_harga" Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_harga" />
                    </td>
                    <td><asp:TextBox runat="server" ID="tb_discount" Width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender3" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_discount" />
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
                                        <asp:Label runat="server" ID="lbl_id_produk" Text='<%#Databinder.Eval(Container, "Dataitem.id_product") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_name" Text="Nama produk" width="200" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="tb_name" Text='<%#Databinder.Eval(Container, "Dataitem.nama_product") %>' Width="200" Font-Names="Tahoma" Font-Size="8pt"/>
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
                                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender134" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_qty" />
                                        <asp:Label runat="server" ID="lbl_satuan" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_qty_pending" Text="Qty pending" width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_qty_pending" Text='<%#Databinder.Eval(Container, "Dataitem.qty_pending") %>' Width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
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


<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\purchase_order.rpt"></Report>
</CR:CrystalReportSource>




