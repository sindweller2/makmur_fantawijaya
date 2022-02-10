<%@ Control Language="VB" AutoEventWireup="false" CodeFile="history_kas.ascx.vb"
    Inherits="Forms_Transaksi_Keuangan_history_kas" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\buku_bank.rpt">
    </Report>
</CR:CrystalReportSource>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="History Kas/Bank" Font-Names="Tahoma"
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
            <asp:Label runat="server" ID="Label4" Text="Kas/Bank" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_kas_bank" Font-Names="Tahoma" Font-Size="8"
                AutoPostBack="true" /></td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
</table>
<table align="center">
    <tr>

                    <td align="center">
                        <asp:Label runat="server" ID="lbl_2323" Text="Keterangan" Font-Names="Tahoma" Font-Size="8" />
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8" /></td>
                </tr>


    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false"
                HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <%--Daniel--%>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                             <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_seq" Text="No." Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>'
                                Visible="true"  Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--Daniel--%>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tanggal" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>'
                                Width="100" Font-Names="Tahoma" Font-Size="8" CommandName="LinkItem" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <%--Daniel--%>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="LabelNoVoucher" Text="No Voucher" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="TextboxNoVoucher" Text='<%#Databinder.Eval(Container, "Dataitem.no_voucher") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--Daniel--%>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_keterangan" Text="Keterangan" Width="200" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_keterangan" Text='<%#Databinder.Eval(Container, "Dataitem.keterangan") %>'
                                Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_customer" Text="Nama customer" Width="150"
                                Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_customer" Text='<%#Databinder.Eval(Container, "Dataitem.nama_customer") %>'
                                Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_debet" Text="Nilai debet" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nilai_debet" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_debet"),2) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_kredit" Text="Nilai kredit" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nilai_kredit" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_kredit"),2) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
    <td align="right"><asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
