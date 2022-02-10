<%@ Control Language="VB" AutoEventWireup="false" CodeFile="popup_dokumen_impor_bdp_produk.ascx.vb" Inherits="Forms_Popup_popup_dokumen_impor_bdp_produk" %>

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
        <td><asp:Label runat="server" ID="Label2" Text="No. dokumen impor" Font-Names="Tahoma" Font-Size="8pt" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8pt" /></td>
        <td><asp:Label runat="server" ID="lbl_no_dokumen_impor" Font-Names="Tahoma" Font-Size="8pt" /></td>
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
                            <asp:Label runat="server" ID="lb_nama" Text="Nama produk" Font-Names="Tahoma" Font-Size="8pt" Width="250" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id_product") %>' Visible="false" />
                            <asp:LinkButton runat="server" ID="lbl_nama" Text='<%#Databinder.Eval(Container, "Dataitem.nama_product") %>' Font-Names="Tahoma" Font-Size="8pt" CommandName="LinkItem"  />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_qty" Text="Qty" Font-Names="Tahoma" Font-Size="8pt" Width="75" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_qty" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.qty"),2) %>' Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_harga_satuan" Text="Harga satuan" Font-Names="Tahoma" Font-Size="8pt" Width="100" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_harga_satuan" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.unit_price"),2) %>' Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_discount" Text="Discount (%)" Font-Names="Tahoma" Font-Size="8pt" Width="100" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_discount" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.discount"),2) %>' Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>