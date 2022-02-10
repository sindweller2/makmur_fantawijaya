<%@ Control Language="VB" AutoEventWireup="false" CodeFile="popup_produk_retur.ascx.vb" Inherits="Forms_Popup_popup_produk_retur" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl1" Text="Daftar Produk" Font-Names="Tahoma" Font-Size="12" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8pt" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="No. penjualan" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Label runat="server" ID="lbl_no_penjualan" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>
            <table width="600">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl2" Text="Nama produk" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:TextBox runat="server" ID="tb_search" Width="150" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama" Text="Nama produk" Font-Names="Tahoma" Font-Size="8pt" Width="300" />
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Visible="false" />
                        <asp:LinkButton runat="server" ID="lbl_nama" Text='<%#Databinder.Eval(Container, "Dataitem.nama_beli") %>' Font-Names="Tahoma" Font-Size="8pt" CommandName="LinkItem"  />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_hrg_jual" Text="Harga jual" Width="150" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_hrg_jual" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.harga_jual"),2) %>' Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_qty" Text="Qty" Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_qty" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.qty"),2) %>' Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_satuan" Text="Satuan produk" Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_satuan" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>

