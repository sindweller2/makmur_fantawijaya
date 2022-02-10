<%@ Control Language="VB" AutoEventWireup="false" CodeFile="kurs_bulanan.ascx.vb"
    Inherits="Forms_Masterfile_akuntansi_kurs_bulanan" %>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Kurs Bulanan" Font-Names="Tahoma" Font-Size="14"
                Font-Bold="true" /></td>
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
        <td style="width: 55px; text-align: left">
            <asp:Label runat="server" ID="lbl199" Text="Tahun" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false"
                HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>'
                                Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_name" Text="Nama periode" Width="150" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_name" Text='<%#Databinder.Eval(Container, "Dataitem.name") %>'
                                Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl" Text="Tanggal periode" Width="170" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tgl_awal" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_awal") %>'
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <asp:Label runat="server" ID="lb_tgl" Text=" s.d " Font-Names="Tahoma" Font-Size="8" />
                            <asp:Label runat="server" ID="lbl_tgl_akhir" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_akhir") %>'
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kurs_bulanan" Text="USD" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_kurs_bulanan" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.kurs_bulanan"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_kurs_bulanan" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--Daniel--%>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_EUR" Text="EUR" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_EUR" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.EUR"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender13" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_EUR" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_JPY" Text="JPY" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_JPY" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.JPY"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender14" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_JPY" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_CNY" Text="CNY" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_CNY" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.CNY"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender15" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_CNY" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_GBP" Text="GBP" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_GBP" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.GBP"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender16" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_GBP" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_AUD" Text="AUD" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_AUD" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.AUD"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender17" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_AUD" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_SGD" Text="SGD" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_SGD" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.SGD"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender18" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_SGD" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_CHF" Text="CHF" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_CHF" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.CHF"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender19" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_CHF" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_KRW" Text="KRW" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_KRW" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.KRW"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender20" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_KRW" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_MYR" Text="MYR" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_MYR" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.MYR"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender21" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_MYR" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_HKD" Text="HKD" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_HKD" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.HKD"),2) %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender22" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_HKD" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--Daniel--%>
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
