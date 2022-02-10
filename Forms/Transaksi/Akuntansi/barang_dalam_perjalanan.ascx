<%@ Control Language="VB" AutoEventWireup="false" CodeFile="barang_dalam_perjalanan.ascx.vb" Inherits="Forms_Transaksi_Akuntansi_barang_dalam_perjalanan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=800, height=400, left=100, top=25"; 
        
    var disp_setting2="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting2+="scrollbars=yes,width=1000, height=400, left=100, top=25"; 
        
    function popup_dokumen_impor_bdp(id_periode, tcid1, tcid2) { 
                window.open('popup_dokumen_impor_bdp.aspx?vid_periode=' + id_periode + '&tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
    function popup_dokumen_impor_bdp_produk(no_dokumen, tcid1, tcid2) { 
                window.open('popup_dokumen_impor_bdp_produk.aspx?vno_dokumen=' + no_dokumen + '&tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting2); }
                                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Barang Dalam Perjalanan" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="Tahun transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
        <ajax:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Bulan transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Dokumen impor" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:Label runat="server" ID="lbl_dokumen_impor" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_no_dokumen_impor" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_dokumen_impor" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_dokumen_impor" Text="Daftar Dokumen impor" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
    <tr>
    <td><asp:Label runat="server" ID="Label6" Text="Tgl. Masuk Gudang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:TextBox runat="server" ID="tb_tgl_masuk_gudang" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <act:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_masuk_gudang" />
            <act:CalendarExtender ID="ce_tgl_masuk_gudang" TargetControlID="tb_tgl_masuk_gudang" runat="server" Format="dd/MM/yyyy" />
        </td>
    </tr>
    <%--<tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama produk" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:Label runat="server" ID="lbl_nama_produk" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_produk" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_produk" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_produk" Text="Daftar Produk" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="Label7" Text="Qty" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td style="width: 350px"><asp:Label runat="server" ID="lbl_qty" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label10" Text="Harga satuan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:Label runat="server" ID="lbl_harga_per_unit" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label12" Text="Discount (%)" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:Label runat="server" ID="lbl_discount" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>--%>
</table>

<table align="center">
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
    <tr>
        <td colspan="2"></td>
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
                            <asp:Label runat="server" ID="lbl_is_submit" Text='<%#Databinder.Eval(Container, "Dataitem.is_submit") %>' Visible="False" />
                            <%--<asp:Label runat="server" ID="lbl_id_produk" Text='<%#Databinder.Eval(Container, "Dataitem.id_product") %>' Visible="false"/>--%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no" Text="No. dokumen impor" Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_no_dokumen_impor" Text='<%#Databinder.Eval(Container, "Dataitem.no_dokumen_impor") %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_invoice_no" Text="Invoice no." Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_invoice_no" Text='<%#Databinder.Eval(Container, "Dataitem.invoice_no") %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_invoice" Text="Nilai Invoice" Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nilai_invoice" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_invoice"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_masuk_gudang" Text="Tgl. Masuk Gudang" Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tgl_masuk_gudang" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_masuk_gudang") %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <%--<asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_harga_satuan" Text="Harga satuan" Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_harga_satuan" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.unit_price"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_discount" Text="Discount (%)" Width="50" Font-Names="Tahoma" Font-Size="8"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_discount" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.discount"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>--%>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
        <asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8"/>
        </td>
    </tr>
</table>
