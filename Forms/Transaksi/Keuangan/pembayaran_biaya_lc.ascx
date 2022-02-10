<%@ Control Language="VB" AutoEventWireup="false" CodeFile="pembayaran_biaya_lc.ascx.vb" Inherits="Forms_Transaksi_Keuangan_pembayaran_biaya_lc" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Pembayaran Biaya L/C" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
            <table runat="server" id="tbl_search" width="350">
                <tr>
                    <td><asp:DropDownList runat="server" ID="dd_pilihan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                            <asp:ListItem Text="No. pembelian" Value="0"></asp:ListItem>
                            <asp:ListItem Text="No. L/C" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Status bayar" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Status submit" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                        
                        <asp:DropDownList runat="server" ID="dd_sts_bayar" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                            <asp:ListItem Text="Belum bayar" Value="B"></asp:ListItem>
                            <asp:ListItem Text="Sudah bayar" Value="S"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="dd_sts_submit" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                            <asp:ListItem Text="Belum disubmit" Value="B"></asp:ListItem>
                            <asp:ListItem Text="Sudah disubmit" Value="S"></asp:ListItem>
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
                            <asp:Label runat="server" ID="lb_no_lc" Text="No. L/C" Width="125" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_seq_lc" Text='<%#Databinder.Eval(Container, "Dataitem.seq_lc") %>' Visible="false" />
                            <asp:LinkButton runat="server" ID="lbl_no_lc" Text='<%#Databinder.Eval(Container, "Dataitem.no_lc") %>' Width="125" Font-Names="Tahoma" Font-Size="8" CommandName="LinkLC" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_lc" Text="Tgl. L/C" Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tgl_lc" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal_lc") %>' Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>     
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_so" Text="No. pembelian" Width="125" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_no" Text='<%#Databinder.Eval(Container, "Dataitem.no") %>' Visible="false" />
                            <asp:LinkButton runat="server" ID="lbl_no_so" Text='<%#Databinder.Eval(Container, "Dataitem.po_no_text") %>' Width="125" Font-Names="Tahoma" Font-Size="8" CommandName="LinkItem" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tgl. pembelian" Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>' Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_bayar" Text="Tgl. bayar" Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tgl_bayar" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_bayar_biaya_lc") %>' Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_status_submit" Text="Status submit" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_is_status_submit" Text='<%#Databinder.Eval(Container, "Dataitem.is_submit_bayar") %>' Visible="false" />
                            <asp:Label runat="server" ID="lbl_status_submit" Text='<%#Databinder.Eval(Container, "Dataitem.status_submit") %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>                                 
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>

