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
            <table runat="server" id="tbl_search" width="800">
                <tr>
                    <td><asp:DropDownList runat="server" ID="dd_pilihan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                            <asp:ListItem Text="No. pembelian" Value="0"></asp:ListItem>
                            <asp:ListItem Text="No. L/C" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
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
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_lc" Text="No. L/C" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_no" Text='<%#Databinder.Eval(Container, "Dataitem.seq_lc") %>' Visible="false" />
                            <asp:Label runat="server" ID="lbl_no_lc" Text='<%#Databinder.Eval(Container, "Dataitem.no_lc") %>' Width="100" Font-Names="Tahoma" Font-Size="8"  />
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
                            <asp:Label runat="server" ID="lb_no_so" Text="No. pembelian" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>                            
                            <asp:Label runat="server" ID="lbl_no_so" Text='<%#Databinder.Eval(Container, "Dataitem.po_no_text") %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
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
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Mata uang" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_mata_uang" Text='<%#Databinder.Eval(Container, "Dataitem.id_currency") %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_total_nilai" Text="Total nilai" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_total_nilai" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.total_nilai"),2) %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_komisi_bank" Text="Komisi Bank" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_komisi_bank" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.biaya_komisi_bank"),2) %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1234" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_komisi_bank" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_ongkos_kawat" Text="Ongkos Kawat" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_ongkos_kawat" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.biaya_ongkos_kawat"),2) %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender14" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_ongkos_kawat" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_porto_materai" Text="Porto Materai" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_porto_materai" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.biaya_porto_materai"),2) %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_porto_materai" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_biaya_courier" Text="Biaya Courier" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_biaya_courier" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.biaya_courier"),2) %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender18" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_biaya_courier" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_biaya_courier" Text="Biaya L/C Amend" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_biaya_lc_amendment" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.biaya_lc_amendment"),2) %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender148" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_biaya_lc_amendment" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_bayar" Text="Tgl. bayar" Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tgl_bayar" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_bayar_biaya_lc") %>' Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_bank" Text="Bank/Kas" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_bank" Text='<%#Databinder.Eval(Container, "Dataitem.id_bank_biaya_lc") %>' Visible="False"/>
                            <asp:DropDownList runat="server" ID="dd_bank" Font-Names="Tahoma" Font-Size="8" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
