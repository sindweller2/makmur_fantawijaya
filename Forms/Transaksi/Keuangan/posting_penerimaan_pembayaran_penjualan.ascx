<%@ Control Language="VB" AutoEventWireup="false" CodeFile="posting_penerimaan_pembayaran_penjualan.ascx.vb"
    Inherits="Forms_Transaksi_Keuangan_posting_penerimaan_pembayaran_penjualan" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Posting Penerimaan Pembayaran Penjualan Barang"
                Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl111" Text="Periode" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label12" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tgl_awal" Width="60" Font-Names="Tahoma" Font-Size="8" />
            <asp:Label runat="server" ID="Label1" Text="s.d" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_tgl_akhir" Width="60" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999"
                MaskType="Date" TargetControlID="tb_tgl_awal" />
            <ajax:CalendarExtender ID="ce_tgl_awal" TargetControlID="tb_tgl_awal" runat="server"
                Format="dd/MM/yyyy" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender12" Mask="99/99/9999"
                MaskType="Date" TargetControlID="tb_tgl_akhir" />
            <ajax:CalendarExtender ID="ce_tgl_akhir" TargetControlID="tb_tgl_akhir" runat="server"
                Format="dd/MM/yyyy" />
        </td>
    </tr>
    <tr>
    <td colspan="3" align="center">
    <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
    <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
    </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <table width="650" runat="server" id="tbl_search">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label122" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false"
                HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>'
                                Visible="False" />
                            <asp:Label runat="server" ID="lbl_no_so" Text='<%#Databinder.Eval(Container, "Dataitem.no_so") %>'
                                Visible="false" />
                            <asp:Label runat="server" ID="lbl_is_posting" Text='<%#Databinder.Eval(Container, "Dataitem.is_posting") %>'
                                Visible="false" />
                            <asp:Label runat="server" ID="lbl_kurs" Text='<%#Databinder.Eval(Container, "Dataitem.kurs") %>'
                                Visible="False" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_invoic" Text="No. invoice" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_no_invoice" Text='<%#Databinder.Eval(Container, "Dataitem.no_invoice") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_voucher" Text="No. Voucher" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_no_voucher" Text='<%#Databinder.Eval(Container, "Dataitem.no_voucher") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_periode" Text="Periode pembayaran" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_periode" Text='<%#Databinder.Eval(Container, "Dataitem.id_periode_pembayaran") %>'
                                Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_periode" Font-Names="Tahoma" Font-Size="8"
                                Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tanggal pembayaran" Width="65" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="tb_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>'
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jenis" Text="Jenis pembayaran" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jenis" Text='<%#Databinder.Eval(Container, "Dataitem.id_jenis_pembayaran") %>'
                                Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_jenis_pembayaran" Font-Names="Tahoma" Font-Size="8"
                                Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_bank" Text="Nama bank" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_bank" Text='<%#Databinder.Eval(Container, "Dataitem.id_bank_account") %>'
                                Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_bank_account" Font-Names="Tahoma" Font-Size="8"
                                Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Mata uang" Width="50" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_mata_uang" Text='<%#Databinder.Eval(Container, "Dataitem.id_currency") %>'
                                Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8"
                                Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_pembayaran" Text="Nilai pembayaran" Width="75"
                                Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="tb_nilai_pembayaran" Width="75" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_pembayaran"),2) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_potongan" Text="Potongan" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="tb_potongan" Width="75" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.potongan"),2) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kelebihan" Text="Kelebihan" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="tb_kelebihan" Width="75" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.kelebihan"),2) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_terima_uang" Text="Tanggal terima uang" Width="65"
                                Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_terima" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_terima_uang") %>'
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="tb_tgl_terima" />
                                <act:CalendarExtender ID="ce_tgl_terima" TargetControlID="tb_tgl_terima" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                           
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btn_unsubmit" Text="Unsubmit" Font-Names="Tahoma" Font-Size="8pt" OnClick="btn_unsubmit_Click"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_posting" Text="Submit" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
