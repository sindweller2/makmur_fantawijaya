<%@ Control Language="VB" AutoEventWireup="false" CodeFile="produk_stock_list.ascx.vb" Inherits="Forms_Masterfile_produk_produk_stock_list" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Stock Produk" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Size="8" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label5" Text="Bulan transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label6" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>   
</table>

<table align="center">
    <tr>
        <td>
            <table runat="server" id="tbl_search" width="600">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl2001" Text="Nama produk" Font-Names="Tahoma" Font-Size="8" />
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
                            <asp:Label runat="server" ID="lb_name_beli" Text="Nama produk" Font-Names="Tahoma" Font-Size="8" Width="250" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_name" Text='<%#Databinder.Eval(Container, "Dataitem.nama_beli") %>' Font-Names="Tahoma" Font-Size="8pt"/>
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_measurement" Text="Packaging" Font-Names="Tahoma" Font-Size="8" Width="100" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_measurement" Text='<%#Databinder.Eval(Container, "Dataitem.packaging") %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_total_stock" Text="Total stock" Font-Names="Tahoma" Font-Size="8" Width="100" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="tb_total_stock" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.qty_stock"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8"/>
                            <asp:Label runat="server" ID="Label2" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_packaging") %>' Width="50" Font-Names="Tahoma" Font-Size="8"/>                         
                            <asp:Label runat="server" ID="lbl20" Text=" / " Font-Names="Tahoma" Font-Size="8" />
                            <asp:Label runat="server" ID="Label1" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.total_stock_conversion"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8"/>
                            <asp:Label runat="server" ID="Label3" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Width="50" Font-Names="Tahoma" Font-Size="8"/>                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn>                    
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_total_masuk" Text="Total Masuk" Font-Names="Tahoma" Font-Size="8" Width="75" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_masuk" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.total_masuk"),2) %>' Font-Names="Tahoma" Font-Size="8"/>
                            <asp:Label runat="server" ID="Label2" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_packaging") %>' Width="50" Font-Names="Tahoma" Font-Size="8"/>                         
                            <asp:Label runat="server" ID="lbl20" Text=" / " Font-Names="Tahoma" Font-Size="8" />
                            <asp:Label runat="server" ID="Label1" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.total_masuk_conversion"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8"/>
                            <asp:Label runat="server" ID="Label3" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Width="50" Font-Names="Tahoma" Font-Size="8"/>                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>                     
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_total_keluar" Text="Total Keluar" Font-Names="Tahoma" Font-Size="8" Width="75" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_keluar" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.total_keluar"),2) %>' Font-Names="Tahoma" Font-Size="8"/>
                            <asp:Label runat="server" ID="Label2" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_packaging") %>' Width="50" Font-Names="Tahoma" Font-Size="8"/>                         
                            <asp:Label runat="server" ID="lbl20" Text=" / " Font-Names="Tahoma" Font-Size="8" />
                            <asp:Label runat="server" ID="Label1" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.total_keluar_conversion"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8"/>
                            <asp:Label runat="server" ID="Label3" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Width="50" Font-Names="Tahoma" Font-Size="8"/>                                                        
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />                        
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_total_saldo" Text="Total Saldo" Font-Names="Tahoma" Font-Size="8" Width="75" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_saldo" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.total_saldo"),2) %>' Font-Names="Tahoma" Font-Size="8"/>
                            <asp:Label runat="server" ID="Label2" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_packaging") %>' Width="50" Font-Names="Tahoma" Font-Size="8"/>                         
                            <asp:Label runat="server" ID="lbl20" Text=" / " Font-Names="Tahoma" Font-Size="8" />
                            <asp:Label runat="server" ID="Label1" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.total_saldo_conversion"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8"/>
                            <asp:Label runat="server" ID="Label3" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Width="50" Font-Names="Tahoma" Font-Size="8"/>                                                        
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn> 
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>
