<%@ Control Language="VB" AutoEventWireup="false" CodeFile="pembayaran_invoice_supplier.ascx.vb" Inherits="Forms_Transaksi_Keuangan_pembayaran_invoice_supplier" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Pembayaran Invoice Supplier Impor TT" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>
            <table runat="server" id="tbl_search" width="950">
                <tr>
                    <td><asp:DropDownList runat="server" ID="dd_pilihan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                            <asp:ListItem Text="No. invoice" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Nama supplier" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No. dokumen" Value="2"></asp:ListItem>
                            <asp:ListItem Text="No. pembelian" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Status bayar" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                        <asp:DropDownList runat="server" ID="dd_status_bayar" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                            <asp:ListItem Text="Belum lunas" Value="B"></asp:ListItem>
                            <asp:ListItem Text="Sudah lunas" Value="S"></asp:ListItem>
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
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_so" Text="No. invoice" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.no_dokumen") %>' Visible="false" />
                            <asp:Label runat="server" ID="lbl_nilai_invoice" Text='<%#Databinder.Eval(Container, "Dataitem.nilai_invoice") %>' Visible="false" />
                            <asp:LinkButton runat="server" ID="lbl_no_invoice" Text='<%#Databinder.Eval(Container, "Dataitem.invoice_no") %>' Width="100" Font-Names="Tahoma" Font-Size="8" CommandName="LinkItem" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal_terima" Text="Tgl. terima" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tanggal_terima" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_terima") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tgl. invoice" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_invoice") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_dokumen" Text="No. dokumen" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_no_dokumen" Text='<%#Databinder.Eval(Container, "Dataitem.no_dokumen") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_jatuh_tempo_invoice_supplier" Text="Tgl. jatuh tempo invoice" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tgl_jatuh_tempo_invoice_supplier" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_jatuh_tempo_invoice_supplier") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_supplier" Text="Nama supplier" Width="250" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_supplier" Text='<%#Databinder.Eval(Container, "Dataitem.nama_supplier") %>' Width="250" Font-Names="Tahoma" Font-Size="8" CommandName="LinkLC" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_pembelian" Text="No. pembelian" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_no_pembelian" Text='<%#Databinder.Eval(Container, "Dataitem.no_pembelian") %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_status_bayar" Text="Status bayar" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_status_bayar" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>
