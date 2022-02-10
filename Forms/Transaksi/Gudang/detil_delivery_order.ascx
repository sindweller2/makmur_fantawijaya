<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_delivery_order.ascx.vb" Inherits="Forms_Transaksi_Gudang_detil_delivery_order" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Pengiriman Barang" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td><asp:Label runat="server" ID="lbl111" Text="No. pengiriman" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_pengiriman" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label2" Text="Tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_pengiriman" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Jenis pengiriman" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_jenis_pengiriman" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Gudang" Value="G"></asp:ListItem>
                <asp:ListItem Text="Kantor" Value="K"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama customer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="4"><asp:Label runat="server" ID="lbl_nama_customer" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_customer" Font-Names="Tahoma" Font-Size="8pt" />
        </td>               
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label7" Text="Alamat customer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="4"><asp:Label runat="server" ID="lbl_alamat_customer" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_alamat_customer" Font-Names="Tahoma" Font-Size="8pt" />
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="No. sales order" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="350"><asp:Label runat="server" ID="lbl_no_so" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_no_so" Font-Names="Tahoma" Font-Size="8pt"  />
        </td>
        <td><asp:Label runat="server" ID="Label16" Text="Tgl. sales order" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="100"><asp:Label runat="server" ID="lbl_tgl_sales_order" Font-Names="Tahoma" Font-Size="8"/></td>
     </tr>
     <tr>
        <td><asp:Label runat="server" ID="Label12" Text="Estimasi tgl. kirim" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_kirim" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_kirim" />
            <ajax:CalendarExtender ID="ce_tgl_kirim" TargetControlID="tb_tgl_kirim" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Tgl. terima gudang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_terima_gudang" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_terima_gudang" />
            <ajax:CalendarExtender ID="ce_tgl_terima_gudang" TargetControlID="tb_tgl_terima_gudang" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>
        <td><asp:Label runat="server" ID="Label23" Text="Tgl. kirim gudang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label24" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="100"><asp:TextBox runat="server" ID="tb_tgl_kirim_gudang" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_kirim_gudang" />
            <ajax:CalendarExtender ID="ce_tgl_kirim_gudang" TargetControlID="tb_tgl_kirim_gudang" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>
        <td><asp:Label runat="server" ID="Label27" Text="Tgl. terima customer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label28" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="100"><asp:TextBox runat="server" ID="tb_tgl_terima_customer" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_terima_customer" />
            <ajax:CalendarExtender ID="ce_tgl_terima_customer" TargetControlID="tb_tgl_terima_customer" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>
    </tr>
</table>    

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8pt" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
    <tr>
        <td colspan="4"></td>
    </tr>
</table>

<table align="center" runat="server" id="tbl_produk">
    <tr>
        <td>
            <table align="center">
                <tr>
                    <td>
                        <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_name" Text="Nama produk" width="250" Font-Names="Tahoma" Font-Size="8pt"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_id_produk" Text='<%#Databinder.Eval(Container, "Dataitem.id_product") %>' Visible="false" />
                                        <asp:Label runat="server" ID="lbl_name" Text='<%#Databinder.Eval(Container, "Dataitem.nama_product") %>' Width="250" Font-Names="Tahoma" Font-Size="8pt"/>
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
                                        <asp:Label runat="server" ID="lbl_qty" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.qty"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8pt"/>
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
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>


<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\delivery_order.rpt"></Report>
</CR:CrystalReportSource>
