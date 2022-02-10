<%@ Control Language="VB" AutoEventWireup="false" CodeFile="mutasi_stock_list.ascx.vb" Inherits="Forms_Transaksi_Accounting_mutasi_stock_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<script language="javascript" type="text/javascript">
    var disp_setting2="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting2+="scrollbars=yes,width=700, height=400, left=100, top=25"; 
                    
    function popup_produk(tcid1, tcid2) { 
                window.open('popup_produk_item.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting2); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Mutasi Stock" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="Tahun transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
        <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Bulan transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label342" Text="Tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label433" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tanggal" width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender11" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal" />
        <act:CalendarExtender ID="ce_tanggal" TargetControlID="tb_tanggal" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2351" Text="Mutasi dari" Font-Names="Tahoma" Font-Size="8" font-bold="True"/></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl83" Text="Nama produk" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Label runat="server" ID="Label39" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="6">
            <asp:Label runat="server" ID="lbl_nama_produk_asal" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_produk_asal" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_produk_asal" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_produk_asal" Text="Daftar Produk" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label24" Text="Satuan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label35" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Lbl_satuan_asal" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2443" Text="Packaging" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3653" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_packaging_asal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label30" Text="Qty" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label32" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_qty_asal" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label23" Text="Mutasi ke" Font-Names="Tahoma" Font-Size="8" font-bold="True"/></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl833" Text="Nama produk" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Label runat="server" ID="Label391" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td>
            <asp:Label runat="server" ID="lbl_nama_produk_tujuan" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_produk_tujuan" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_produk_tujuan" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_produk_tujuan" Text="Daftar Produk" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label243" Text="Satuan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label353" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Lbl_satuan_tujuan" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label239" Text="Packaging" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label339" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_packaging_tujuan" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2390" Text="Qty" Font-Names="Tahoma" Font-Size="8" font-bold="True"/></td>
        <td><asp:Label runat="server" ID="Label3590" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_qty" width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty" />            
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2391" Text="Keterangan" Font-Names="Tahoma" Font-Size="8" font-bold="True"/></td>
        <td><asp:Label runat="server" ID="Label3591" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_keterangan" width="300" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" />
        </td>
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
                            <asp:Label runat="server" ID="lbl_no" Text='<%#Databinder.Eval(Container, "Dataitem.no") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tanggal" Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>' Width="65" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_produk_asal" Text="Nama produk asal" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id_produk_asal" Text='<%#Databinder.Eval(Container, "Dataitem.id_product_from") %>' visible="False"/>	
                            <asp:Label runat="server" ID="lbl_nama_produk_asal" Text='<%#Databinder.Eval(Container, "Dataitem.nama_beli_asal") %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_satuan_asal" Text="Satuan" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_satuan_asal" Text='<%#Databinder.Eval(Container, "Dataitem.nama_satuan_asal") %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_packaging_asal" Text="Packaging" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_packaging_asal" Text='<%#Databinder.Eval(Container, "Dataitem.packaging_asal") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_produk_tujuan" Text="Nama produk tujuan" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id_produk_tujuan" Text='<%#Databinder.Eval(Container, "Dataitem.id_product_to") %>' visible="False"/>	
                            <asp:Label runat="server" ID="lbl_nama_produk_tujuan" Text='<%#Databinder.Eval(Container, "Dataitem.nama_beli_tujuan") %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_satuan_tujuan" Text="Satuan" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_satuan_tujuan" Text='<%#Databinder.Eval(Container, "Dataitem.nama_satuan_tujuan") %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_packaging_tujuan" Text="Packaging" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_packaging_tujuan" Text='<%#Databinder.Eval(Container, "Dataitem.packaging_tujuan") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_qty" Text="Qty" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_temp_qty" Text='<%#Databinder.Eval(Container, "Dataitem.temp_qty") %>' visible="False" />
                            <asp:TextBox runat="server" ID="tb_qty" Text='<%#Databinder.Eval(Container, "Dataitem.qty") %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty" />            
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_keterangan" Text="Keterangan" Width="300" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_keterangan" Text='<%#Databinder.Eval(Container, "Dataitem.keterangan") %>' Width="300" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
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
