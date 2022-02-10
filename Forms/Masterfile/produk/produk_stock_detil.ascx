<%@ Control Language="VB" AutoEventWireup="false" CodeFile="produk_stock_detil.ascx.vb" Inherits="Forms_Masterfile_produk_produk_stock_detil" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl1" Text="Detil Stock Produk" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl2" Text="Nama kategori produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="LanguageLabel1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_category_name" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="LanguageLabel21" Text="Nama sub kategori produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="LanguageLabel22" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_subcategory_name" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="LanguageLabel2" Text="Nama produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="LanguageLabel3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_name" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="LanguageLabel6" Text="Satuan produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="LanguageLabel7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_measurement" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="LanguageLabel13" Text="Total stock" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="LanguageLabel14" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_total_stock" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="LanguageLabel15" Text="Satuan packaging produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="LanguageLabel16" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_measurement_conversion" Font-Names="Tahoma" Font-Size="8"/></td>
        <td>&nbsp;</td>
        <td><asp:Label runat="server" ID="LanguageLabel17" Text="Qty packaging" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="LanguageLabel20" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_conversion_qty" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="LanguageLabel18" Text="Total stock packaging" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="LanguageLabel19" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_total_conversion_stock" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><b><asp:Label runat="server" ID="LanguageLabel10" Text="Stock produk" Font-Names="Tahoma" Font-Size="8" /></b></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="LanguageLabel8" Text="Lot no." Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="LanguageLabel9" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_lot_no" Width="150" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="LanguageLabel11" Text="Qty" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="LanguageLabel12" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_qty" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender11" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_qty" />
            <asp:Button runat="server" ID="btn_add" Text="Add" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8"/>
        </td>
    </tr>
</table>    

<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderTemplate>                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_lot_no" Text="Lot no." Font-Names="Tahoma" Font-Size="8" Width="150" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_lot_no" Text='<%#Databinder.Eval(Container, "Dataitem.lot_no") %>' Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_qty" Text="Qty stock" Font-Names="Tahoma" Font-Size="8" Width="100" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_qty" Text='<%#Formatnumber(Databinder.Eval(Container, "Dataitem.qty"),2) %>' Font-Names="Tahoma" Font-Size="8" Width="100"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_qty" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_conversion_qty" Text="Packaging qty stock" Font-Names="Tahoma" Font-Size="8" Width="150" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_conversion_qty_text" Font-Names="Tahoma" Font-Size="8" Width="100"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
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