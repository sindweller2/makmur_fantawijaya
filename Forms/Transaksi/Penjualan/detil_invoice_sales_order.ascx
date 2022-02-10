<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_invoice_sales_order.ascx.vb" Inherits="Forms_Transaksi_Penjualan_detil_invoice_sales_order" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
        
    function popup_alamat_customer(id_customer, tcid1, tcid2) { 
                window.open('popup_alamat_customer.aspx?vid_customer=' + id_customer + '&tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Invoice dan Faktur Pajak Penjualan Barang" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td style="width: 350px"><asp:Label runat="server" ID="lbl_periode_transaksi" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="No. penjualan" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td style="width: 350px"><asp:Label runat="server" ID="lbl_no_penjualan" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label2" Text="Tgl. penjualan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_penjualan" width="65%" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender12" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_penjualan" />
       <ajax:CalendarExtender ID="ce_tgl_penjualan" TargetControlID="tb_tgl_penjualan" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl1115" Text="No. faktur pajak" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label158" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td style="width: 350px"><asp:Label runat="server" ID="lbl_no_faktur_pajak" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Nama sales" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td style="width: 350px"><asp:DropDownList runat="server" ID="dd_sales" Font-Names="Tahoma" Font-Size="8" Enabled="false" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="No. SP" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:TextBox runat="server" ID="tb_no_sp" Width="75%" Font-Names="Tahoma" Font-Size="8"  Enabled="false"/></td>
        <td><asp:Label runat="server" ID="Label7" Text="Tgl. SP" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_sp" Width="65%" Font-Names="Tahoma" Font-Size="8"  Enabled="false"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_sp" />
        <ajax:CalendarExtender ID="ce_tgl_sp" TargetControlID="tb_tgl_sp" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Jenis penjualan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:DropDownList runat="server" ID="dd_jenis_penjualan" Font-Names="Tahoma" Font-Size="8"  Enabled="false">
                <asp:ListItem Text="Tunai" Value="T"></asp:ListItem>
                <asp:ListItem Text="Kredit" Value="K"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td><asp:Label runat="server" ID="Label12" Text="PPN" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_ppn" Font-Names="Tahoma" Font-Size="8"  Enabled="false">
                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                <asp:ListItem Text="10" Value="10"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama customer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:Label runat="server" ID="lbl_nama_customer" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_customer" Font-Names="Tahoma" Font-Size="8pt"/></td>        
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label16" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8"  Enabled="false"/></td>
        <td><asp:Label runat="server" ID="Label18" Text="Nilai kurs" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_kurs" Width="50%" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_kurs" />
        </td>
        <td>
        <asp:Button ID="btn_kurs_idr" runat="server" Text="IDR" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button ID="btn_kurs_usd" runat="server" Text="USD" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label29" Text="Alamat invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label30" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="4"><asp:Label runat="server" ID="lbl_alamat_customer" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_alamat_customer" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_alamat_customer" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_alamat_customer" Text="Daftar Alamat Customer" Font-Names="Tahoma" Font-Size="8pt"/>
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label32" Text="Alamat faktur pajak" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label33" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="4"><asp:Label runat="server" ID="lbl_alamat_faktur_pajak" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_alamat_faktur_pajak" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_alamat_faktur_pajak" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_alamat_faktur_pajak" Text="Daftar Alamat Customer" Font-Names="Tahoma" Font-Size="8pt"/>
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label22" Text="Tanggal invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label23" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:TextBox runat="server" ID="tb_tgl_invoice" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_invoice" />
            <ajax:CalendarExtender ID="ce_tgl_invoice" TargetControlID="tb_tgl_invoice" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>
        <td><asp:Label runat="server" ID="Label24" Text="Lama pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label25" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_lama_pembayaran" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_lama_pembayaran" />
            <asp:Label runat="server" ID="Label26" Text="hari" Width="50" Font-Names="Tahoma" Font-Size="8"/>
        </td>
        <td><asp:Label runat="server" ID="Label27" Text="Tanggal jatuh tempo" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label28" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td ><asp:TextBox runat="server" ID="tb_jatuh_tempo" Width="65" Font-Names="Tahoma" Font-Size="8" />
             <asp:Button runat="server" ID="btn_hitung" Text="Jatuh tempo" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_jatuh_tempo" />
            <ajax:CalendarExtender ID="ce_jatuh_tempo" TargetControlID="tb_jatuh_tempo" runat="server" Animated="true" Format="dd/MM/yyyy" />            
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label34" Text="Nama penandatangan faktur pajak" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label35" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:DropDownList runat="server" ID="dd_ttd_faktur_pajak" Font-Names="Tahoma" Font-Size="8" /></td>
        <%--Daniel--%>
        <td><asp:Label runat="server" ID="Label36" Text="Uang Muka" Font-Names="Tahoma" Font-Size="8"/></td>
    <td><asp:Label runat="server" ID="Label37" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
    <td><asp:DropDownList runat="server" ID="DropDownListUangMuka" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" Enabled="false">
        <asp:ListItem Selected="True" Value="Tidak">Tidak</asp:ListItem>
        <asp:ListItem Value="Ya">Ya</asp:ListItem>
    </asp:DropDownList>
    </td>
    <td>
    <asp:Label runat="server" ID="Label38" Text="Nominal" Font-Names="Tahoma" Font-Size="8"/>
    </td>
    <td><asp:Label runat="server" ID="Label39" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
    <td>
    <asp:TextBox ID="TextBoxNominal" runat="server" Font-Names="Tahoma" Font-Size="8" Enabled="false"></asp:TextBox>
    <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender4" FilterType="custom, numbers" ValidChars=",." TargetControlID="TextBoxNominal" />
    </td>
    </tr>     
    <tr>
    <td></td>
     <td></td>
      <td></td>
      <td><asp:Label runat="server" ID="Label40" Text="Keterangan" Font-Names="Tahoma" Font-Size="8"/></td>
     <td><asp:Label runat="server" ID="Label41" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
      <td colspan="4"><asp:Label runat="server" ID="LabelKeterangan" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <%--Daniel--%>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_print_invoice" Text="Print invoice (Letter)" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_print_faktur_pajak" Text="Print faktur pajak (A4)" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_unsubmit" Text="Unsubmit" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center" runat="server" id="tbl_produk">    
    <tr>
        <td>
            <table runat="server" id="tbl_total_harga">
                <tr>
                    <td width="50%">
                        <asp:Label runat="server" ID="lbl1020" Text="Total nilai" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>                        
                        <asp:Label runat="server" ID="Label31" Text=":" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>
                        <asp:Label runat="server" ID="lbl_total_nilai" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>
                    </td>
                    <td width="50%" align="right">
                        <asp:Label runat="server" ID="lbl10202" Text="Total nilai IDR" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>                        
                        <asp:Label runat="server" ID="Label313" Text=":" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>
                        <asp:Label runat="server" ID="lbl_total_nilai_idr" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="true"/>
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
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_name" Text="Nama produk" width="200" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_id_produk" Text='<%#Databinder.Eval(Container, "Dataitem.id_product") %>' Visible="false" />
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
                                        <asp:Label runat="server" ID="lbl_qty" Text='<%#Formatnumber(Databinder.Eval(Container, "Dataitem.qty"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_satuan" Text="Satuan" width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_satuan" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_harga_jual" Text="Harga" width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_harga_jual" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.harga_jual"),3) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_discount" Text="Discount(%)" width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="tb_discount" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.discount"),2) %>' Width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_sub_total" Text="Sub total" width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_sub_total" Text='<%#formatnumber(Databinder.Eval(Container, "Dataitem.sub_total"),3) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label runat="server" id="lbl5645" text="Grand sub total" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:Label runat="server" id="lbl34" text=":" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:Label runat="server" id="lbl_subtotal" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
</table>


<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\invoice.rpt"></Report>
</CR:CrystalReportSource>

