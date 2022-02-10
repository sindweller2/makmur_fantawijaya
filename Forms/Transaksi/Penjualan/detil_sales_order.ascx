<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_sales_order.ascx.vb" Inherits="Forms_Transaksi_Penjualan_detil_sales_order" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
        
    var disp_setting2="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting2+="scrollbars=yes,width=700, height=400, left=100, top=25"; 
    
    function popup_customer(tcid1, tcid2) { 
                window.open('popup_customer.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
    function popup_produk(id_sales, tcid1, tcid2) { 
                window.open('popup_produk.aspx?vid_sales=' + id_sales + '&tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting2); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Penjualan Barang" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td><asp:Label runat="server" ID="Label26" Text="Bonus ?" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label28" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_is_bonus" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                <asp:ListItem Text="Bukan" Value="T"></asp:ListItem>
                <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="No. penjualan" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_penjualan" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" />
            <asp:TextBox runat="server" ID="tb_no_penjualan" Width="75" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" />
        </td>
        <td><asp:Label runat="server" ID="Label2" Text="Tgl. penjualan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 296px"><asp:TextBox runat="server" ID="tb_tgl_penjualan" width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender11" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_penjualan" />
            <ajax:CalendarExtender ID="ce_tgl_penjualan" TargetControlID="tb_tgl_penjualan" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="No. SP" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_sp" Width="75" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text="Tgl. SP" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 296px"><asp:TextBox runat="server" ID="tb_tgl_sp" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_sp" />
            <ajax:CalendarExtender ID="ce_tgl_sp" TargetControlID="tb_tgl_sp" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Jenis penjualan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_jenis_penjualan" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Tunai" Value="T"></asp:ListItem>
                <asp:ListItem Text="Kredit" Value="K"></asp:ListItem>
            </asp:DropDownList>
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama customer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="350"><asp:Label runat="server" ID="lbl_nama_customer" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_customer" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_customer" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_customer" Text="Daftar Customer" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
        <td><asp:Label runat="server" ID="Label12" Text="PPN" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 296px"><asp:DropDownList runat="server" ID="dd_ppn" Font-Names="Tahoma" Font-Size="8" >
                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                <asp:ListItem Text="10" Value="10"></asp:ListItem>
            </asp:DropDownList>
        </td>      
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label29" Text="No. PO customer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label30" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_po_no" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label16" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" /></td>
        <td><asp:Label runat="server" ID="Label18" Text="Nilai kurs" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 296px">
        <asp:TextBox runat="server" ID="tb_kurs" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Button ID="btn_kurs_idr" runat="server" Text="IDR" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button ID="btn_kurs_usd" runat="server" Text="USD" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_kurs" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Nama sales" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_sales" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" /></td>
    <%--Daniel--%>
    <td><asp:Label runat="server" ID="Label31" Text="Uang Muka" Font-Names="Tahoma" Font-Size="8"/></td>
    <td><asp:Label runat="server" ID="Label32" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
    <td style="width: 296px"><asp:DropDownList runat="server" ID="DropDownListUangMuka" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"  >
        <asp:ListItem Selected="True" Value="Tidak">Tidak</asp:ListItem>
        <asp:ListItem Value="Ya">Ya</asp:ListItem>
    </asp:DropDownList>
    
    <asp:Label runat="server" ID="Label35" Text="Nominal" Font-Names="Tahoma" Font-Size="8"/><asp:TextBox ID="TextBoxNominal" runat="server" Font-Names="Tahoma" Font-Size="8"></asp:TextBox>
    <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender4" FilterType="custom, numbers" ValidChars=",." TargetControlID="TextBoxNominal" />
    </td>
    </tr>  
    <tr>
    <td></td>
    <td></td>
    <td></td>
    <td><asp:Label runat="server" ID="Label33" Text="Keterangan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label34" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
<td style="width: 296px">
    <asp:TextBox ID="TextBoxKeterangan" runat="server" Font-Names="Tahoma" Font-Size="8" Width="100%" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
    <%--Daniel--%>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8pt" Visible="False"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center" runat="server" id="tbl_produk">
    <tr>
        <td style="height: 108px">
            <table align="center">
                <tr>
                    <td><asp:Label runat="server" ID="lbl83" Text="Nama produk" Width="300" Font-Names="Tahoma" Font-Size="8pt"/></td>
                    <td><asp:Label runat="server" ID="Label25" Text="Packaging" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>
                    <td><asp:Label runat="server" ID="Label22" Text="Qty" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>                    
                    <td><asp:Label runat="server" ID="Label23" Text="Harga" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>
                    <td><asp:Label runat="server" ID="Label24" Text="Discount (%)" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>
                </tr>
                <tr>
                    <td width="350" style="height: 66px"><asp:TextBox runat="server" ID="tb_nama_produk" Width="250" Font-Names="Tahoma" Font-Size="8" />
                        <asp:TextBox runat="server" ID="tb_id_produk" Font-Names="Tahoma" Font-Size="8pt"/>
                        <asp:LinkButton runat="server" ID="link_refresh_produk" Text="Refresh"/>
                        <asp:LinkButton runat="server" ID="link_popup_produk" Text="Daftar Produk" Font-Names="Tahoma" Font-Size="8pt"/>
                    </td>
                    <td style="height: 66px"><asp:Label runat="server" ID="lbl_packaging" Width="100" Font-Names="Tahoma" Font-Size="8pt"/></td>
                    <td style="height: 66px"><asp:TextBox runat="server" ID="tb_qty" Width="50" Font-Names="Tahoma" Font-Size="8pt"/>                        
                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty" />
                        <asp:Label runat="server" ID="lbl_satuan" Width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                    </td>                    
                    <td style="height: 66px"><asp:TextBox runat="server" ID="tb_harga" Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_harga" />
                    </td>
                    <td style="height: 66px"><asp:TextBox runat="server" ID="tb_discount" Width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender3" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_discount" />
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
                                        <asp:TextBox runat="server" ID="tb_qty" Text='<%#formatnumber(Databinder.Eval(Container, "Dataitem.qty"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender134" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty" />
                                        <asp:Label runat="server" ID="lbl_satuan" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_qty_pending" Text="Qty pending" width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_qty_pending" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.qty_pending"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_harga_jual" Text="Harga" width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="tb_harga_jual" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.harga_jual"),3) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1334" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_harga_jual" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_discount" Text="Discount(%)" width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="tb_discount" Text='<%#Databinder.Eval(Container, "Dataitem.discount") %>' Width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                                        <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender13343" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_harga_jual" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_sub_total" Text="Sub total" width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_sub_total" Text='<%#Databinder.Eval(Container, "Dataitem.sub_total") %>' Visible="false" />
                                        <asp:Label runat="server" ID="lbl_sub_total_display" Font-Names="Tahoma" Font-Size="8pt" />
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





