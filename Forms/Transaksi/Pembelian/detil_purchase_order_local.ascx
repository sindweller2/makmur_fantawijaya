<%-- Daniel 27/3/2017 =========================================================================== --%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_purchase_order_local.ascx.vb"
    Inherits="Forms_Transaksi_Pembelian_detil_purchase_order_local" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

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
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Pembelian Barang Lokal" Font-Names="Tahoma"
                Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="Label14" Text="Periode transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="lbl_periode_transaksi" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl111" Text="No. pembelian" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="lbl_no_pembelian" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label4" Text="Inv. pembelian" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox ID="tb_inv_pembelian" runat="server" Font-Names="Tahoma" Font-Size="8"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label2" Text="Tgl. pembelian" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tgl_pembelian" Width="65" Font-Names="Tahoma"
                Font-Size="8" />
            <act:CalendarExtender ID="ce_tgl_pembelian" TargetControlID="tb_tgl_pembelian" runat="server"
                Format="dd/MM/yyyy">
            </act:CalendarExtender>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999"
                MaskType="Date" TargetControlID="tb_tgl_pembelian" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label36" Text="Note" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label37" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_note" Width="350" Height="50" TextMode="MultiLine"
                Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label145" Text="Dibuat oleh" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label155" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_dibuat_oleh" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt" /></td>
        <td>
            <asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8pt"
                Visible="False" /></td>
        <td>
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt" /></td>
    </tr>
    <tr>
        <td colspan="3">
        </td>
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
                    <td><asp:Button runat="server" ID="btn_add" Text="Add" Font-Names="Tahoma" Font-Size="8pt"/>
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
</table>
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
                            <asp:Label runat="server" ID="lb_qty" Text="Qty" Width="75" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_qty" Text='<%#Databinder.Eval(Container, "Dataitem.qty") %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8pt" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender134" FilterType="custom, numbers"
                                ValidChars=",." TargetControlID="tb_qty" />
                            <asp:Label runat="server" ID="lbl_satuan" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>'
                                Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_harga" Text="Harga" Width="75" Font-Names="Tahoma"
                                Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_harga" Text='<%#Databinder.Eval(Container, "Dataitem.unit_price") %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_sub_total" Text="Sub total" Width="100" Font-Names="Tahoma"
                                Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_sub_total" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.sub_total"),3) %>'
                                Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right" style="height: 22px">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8pt" />
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8pt" />
        </td>
    </tr>
</table>
<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\purchase_order_local.rpt">
    </Report>
</CR:CrystalReportSource>
<%-- =========================================================================== Daniel 27/3/2017 --%>
