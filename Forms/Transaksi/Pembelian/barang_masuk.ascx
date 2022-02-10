<%@ Control Language="VB" AutoEventWireup="false" CodeFile="barang_masuk.ascx.vb" Inherits="Forms_Transaksi_Pembelian_barang_masuk" %>

<script language="javascript" type="text/javascript">
    var disp_setting2="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting2+="scrollbars=yes,width=700, height=400, left=100, top=25"; 
    
    function popup_produk_item(tcid1, tcid2) { 
                window.open('popup_produk_item.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting2); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Barang Masuk" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td><asp:Label runat="server" ID="Label4" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
        <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label5" Text="Bulan transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label6" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label1" Text="Nama barang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label2" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_nama_produk" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_produk" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_produk" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_produk" Text="Daftar Produk" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label3" Text="Qty" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_qty" Width="75" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty" />
        </td>
    </tr>  
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8"/></td>
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
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>' Visible="false" />   
                            <asp:Label runat="server" ID="lbl_id_product" Text='<%#Databinder.Eval(Container, "Dataitem.id_product") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_barang" Text="Nama barang" Width="250" Font-Names="Tahoma" Font-Size="8"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_barang" Text='<%#Databinder.Eval(Container, "Dataitem.nama_produk") %>' Width="250" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_qty" Text="Qty" Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_qty" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.qty"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_qty" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8"/>
        </td>
    </tr>
</table>