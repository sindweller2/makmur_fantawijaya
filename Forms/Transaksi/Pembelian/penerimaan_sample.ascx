<%@ Control Language="VB" AutoEventWireup="false" CodeFile="penerimaan_sample.ascx.vb" Inherits="Forms_Transaksi_Pembelian_penerimaan_sample" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=700, height=400, left=100, top=25"; 
        
    function popup_supplier(tcid1, tcid2) { 
                window.open('popup_supplier.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
    function popup_sample(tcid1, tcid2) { 
                window.open('popup_sample.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
    
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Penerimaan Sample" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <ajax:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Bulan transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="Tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tanggal" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal" />
            <ajax:CalendarExtender ID="ce_tanggal" TargetControlID="tb_tanggal" runat="server" Format="dd/MM/yyyy" />

        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama supplier" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="350"><asp:Label runat="server" ID="lbl_nama_supplier" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_supplier" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_supplier" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_supplier" Text="Daftar supplier" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>            
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Nama sample" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="350"><asp:Label runat="server" ID="lbl_nama_sample" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_sample" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_sample" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_sample" Text="Daftar sample" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>            
        <td><asp:Label runat="server" ID="Label10" Text="Satuan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_satuan" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label7" Text="Qty" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_qty" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender3" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label16" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="350"><asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label18" Text="Kurs" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_kurs" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
        <asp:Button ID="btn_kurs_idr" runat="server" Text="IDR" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button ID="btn_kurs_usd" runat="server" Text="USD" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label14" Text="Harga" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_harga" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_harga" />
        </td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>   
</table>

<table align="center">
    <tr>
        <td>
            <table runat="server" id="tbl_search" width="500">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl2001" Text="Nama sample" Font-Names="Tahoma" Font-Size="8" />
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderTemplate>                        
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tanggal" Font-Names="Tahoma" Font-Size="8" Width="75" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>' width="65" Font-Names="Tahoma" Font-Size="8pt"/>                            
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_supplier" Text="Nama supplier" Font-Names="Tahoma" Font-Size="8" Width="250" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id_supplier" Text='<%#Databinder.Eval(Container, "Dataitem.nama_supplier") %>' Width="250" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_name" Text="Nama sample" Font-Names="Tahoma" Font-Size="8" Width="250" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_name" Text='<%#Databinder.Eval(Container, "Dataitem.nama_sample") %>' width="250" Font-Names="Tahoma" Font-Size="8pt"/>                            
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_measurement" Text="Satuan" Font-Names="Tahoma" Font-Size="8" Width="100" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_satuan" Text='<%#Databinder.Eval(Container, "Dataitem.nama_satuan") %>' Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_qty" Text="Qty" Font-Names="Tahoma" Font-Size="8" Width="75" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_qty" Text='<%#Databinder.Eval(Container, "Dataitem.qty") %>' Width="75" Font-Names="Tahoma" Font-Size="8"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender123" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty" />
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Mata uang" Font-Names="Tahoma" Font-Size="8" Width="75" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_mata_uang" Text='<%#Databinder.Eval(Container, "Dataitem.id_currency") %>' Visible="False"/>
                            <asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kurs" Text="Kurs" Font-Names="Tahoma" Font-Size="8" Width="75" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_kurs" Text='<%#Databinder.Eval(Container, "Dataitem.kurs") %>' Width="75" Font-Names="Tahoma" Font-Size="8"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1357" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
                        </ItemTemplate>
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_harga" Text="Harga" Font-Names="Tahoma" Font-Size="8" Width="75" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_harga" Text='<%#Databinder.Eval(Container, "Dataitem.harga") %>' Width="75" Font-Names="Tahoma" Font-Size="8"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender157" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_harga" />
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