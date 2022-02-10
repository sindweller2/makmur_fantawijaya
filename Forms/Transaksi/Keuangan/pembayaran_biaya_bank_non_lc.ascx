<%@ Control Language="VB" AutoEventWireup="false" CodeFile="pembayaran_biaya_bank_non_lc.ascx.vb"
    Inherits="Forms_Transaksi_Keuangan_pembayaran_biaya_bank_non_lc" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Pembayaran Biaya Bank Non L/C" Font-Names="Tahoma"
                Font-Size="14" Font-Bold="true" /></td>
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
            <asp:Label runat="server" ID="lbl111" Text="Tahun transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label2" Text="Bulan transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8"
                AutoPostBack="true" /></td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <table runat="server" id="tbl_search" width="500">
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="dd_pilihan" Font-Names="Tahoma" Font-Size="8">
                            <asp:ListItem Text="No. pembelian" Value="0"></asp:ListItem>
                            <asp:ListItem Text="No. dokumen" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No. invoice" Value="2"></asp:ListItem>
                            <asp:ListItem Text="No. L/C" Value="3"></asp:ListItem>
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
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false"
                HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                            <asp:Label runat="server" ID="lbl_is_submit_non_lc" Text='<%#Databinder.Eval(Container, "Dataitem.is_submit_non_lc") %>'
                                Visible="False" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_seq" Text="No. dokumen" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.no_dokumen") %>'
                                Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_so" Text="No. pembelian" Width="150" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_no_so" Text='<%#Databinder.Eval(Container, "Dataitem.po_no_text") %>'
                                Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_invoice" Text="No. Invoice" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_no_invoice" Text='<%#Databinder.Eval(Container, "Dataitem.invoice_no") %>'
                                Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jumlah" Text="Jumlah nilai" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_jumlah" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.biaya_bank_non_lc"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" Enabled="false" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_jumlah" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_bayar" Text="Tgl. bayar" Width="65" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_bayar" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_byr_biayabank_nonlc") %>'
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender11" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="tb_tgl_bayar" />
                                <act:CalendarExtender ID="ce_tgl_bayar" TargetControlID="tb_tgl_bayar" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kas_bank" Text="Kas/Bank" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_kas_bank" Text='<%#Databinder.Eval(Container, "Dataitem.id_bank_biayabank_nonlc") %>'
                                Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_kas_bank" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_giro" Text="No. Cek/Giro" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_giro" Text='<%#Databinder.Eval(Container, "Dataitem.no_giro_biayabank_nonlc") %>'
                                Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_giro" Text="Tgl. Cek/Giro" Width="65" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_giro" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_giro_biayabank_nonlc") %>'
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender13" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="tb_tgl_giro" />
                                <act:CalendarExtender ID="ce_tgl_giro" TargetControlID="tb_tgl_giro" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_jatuh_tempo" Text="Tgl. jatuh tempo" Width="65"
                                Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_jatuh_tempo" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_jatuh_tempo_biayabank_nonlc") %>'
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender133" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="tb_tgl_jatuh_tempo" />
                                <act:CalendarExtender ID="ce_tgl_jatuh_tempo" TargetControlID="tb_tgl_jatuh_tempo" runat="server" Animated="true" Format="dd/MM/yyyy" ></act:CalendarExtender>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <%--Daniel--%>
    <tr>
        <td align="right">
            <asp:Label runat="server" ID="Label34" Text="Account:" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" />
            <asp:DropDownList ID="DropDownListAccount" runat="server" Font-Names="Tahoma" Font-Size="8"
                AutoPostBack="true">
                <asp:ListItem Value="61.04">BIAYA PEMBELIAN IMPORT</asp:ListItem>
                <asp:ListItem Value="11.08">UANG MUKA LC/PEMBEL. IMP/B. PEMBEL. IMP</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <%--Daniel--%>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
