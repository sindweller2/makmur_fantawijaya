<%@ Control Language="VB" AutoEventWireup="false" CodeFile="purchase_order_stock.ascx.vb" Inherits="Forms_Transaksi_Pembelian_purchase_order_stock" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Pembelian Barang" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td><asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/>
            <asp:Button runat="server" ID="btn_new" Text="Pembelian baru" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>
            <table runat="server" id="tbl_search" width="700">
                <tr>
                    <td><asp:DropDownList runat="server" ID="dd_pilihan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                            <asp:ListItem Text="No. pembelian" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Status submit" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                        <asp:DropDownList runat="server" ID="dd_submit" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                            <asp:ListItem Text="Sudah disubmit" Value="S"></asp:ListItem>
                            <asp:ListItem Text="Belum disubmit" Value="B"></asp:ListItem>
                        </asp:DropDownList>
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
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                            <asp:Label runat="server" ID="lbl_no" Text='<%#Databinder.Eval(Container, "Dataitem.no") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_so" Text="No. pembelian" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbl_no_so" Text='<%#Databinder.Eval(Container, "Dataitem.po_no_text") %>' Width="100" Font-Names="Tahoma" Font-Size="8" CommandName="LinkItem" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tgl. pembelian" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jenis_pembelian" Text="Jenis pembelian" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jenis_pembelian" Text='<%#Databinder.Eval(Container, "Dataitem.jenis_penjualan") %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_estimasi_tgl_terima" Text="Estimasi tgl. terima" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_estimasi_tgl_terima" Text='<%#Databinder.Eval(Container, "Dataitem.received_date_request") %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_supplier" Text="Nama supplier" Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_supplier" Text='<%#Databinder.Eval(Container, "Dataitem.nama_supplier") %>' Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_submit" Text="Sts. submit" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                             <asp:Label runat="server" ID="lbl_is_submit" Text='<%#Databinder.Eval(Container, "Dataitem.is_submit") %>' Visible="false" />
                            <asp:Label runat="server" ID="lbl_submit" Text='<%#Databinder.Eval(Container, "Dataitem.status_submit") %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
