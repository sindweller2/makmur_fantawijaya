<%@ Control Language="VB" AutoEventWireup="false" CodeFile="periode_transaksi.ascx.vb"
    Inherits="Forms_Masterfile_accounting_periode_transaksi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Daftar Periode Transaksi" Font-Names="Tahoma"
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
            <asp:Label runat="server" ID="lbl199" Text="Tahun" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label2" Text="Bulan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label4" Text="Nama periode" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_name" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label6" Text="Tanggal periode" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tgl_awal" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <act:CalendarExtender ID="ce_tgl_awal" TargetControlID="tb_tgl_awal" runat="server"
                Format="dd/MM/yyyy">
            </act:CalendarExtender>
            <asp:Label runat="server" ID="Label8" Text=" s.d " Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_tgl_akhir" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <act:CalendarExtender ID="ce_tgl_akhir" TargetControlID="tb_tgl_akhir" runat="server"
                Animated="true" Format="dd/MM/yyyy">
            </act:CalendarExtender>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999"
                MaskType="Date" TargetControlID="tb_tgl_awal" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999"
                MaskType="Date" TargetControlID="tb_tgl_akhir" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label11" runat="server" Text="Mata Uang" Font-Names="Tahoma" Font-Size="8"></asp:Label></td>
        <td>
            <asp:Label ID="Label12" runat="server" Text=":" Font-Names="Tahoma" Font-Size="8"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlMataUang" runat="server" Font-Names="Tahoma" Font-Size="8">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label9" Text="Periode awal ?" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label10" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_is_awal" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Bukan" Value="T"></asp:ListItem>
                <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table align="center">
    <tr>
        <td style="height: 22px">
            <asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td style="height: 22px">
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
                            <asp:Label runat="server" ID="lb_bulan" Text="Bulan" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_bulan" Text='<%#Databinder.Eval(Container, "Dataitem.bulan") %>'
                                Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_name" Text="Nama periode" Width="150" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_name" Text='<%#Databinder.Eval(Container, "Dataitem.name") %>'
                                Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl" Text="Tanggal periode" Width="150" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_awal" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_awal") %>'
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <asp:Label runat="server" ID="lb_tgl" Text=" s.d " Font-Names="Tahoma" Font-Size="8" />
                            <asp:TextBox runat="server" ID="tb_tgl_akhir" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_akhir") %>'
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1534" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="tb_tgl_awal" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender243" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="tb_tgl_akhir" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
               
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_is_awal" Text="Periode awal ?" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_is_awal" Text='<%#Databinder.Eval(Container, "Dataitem.is_awal") %>'
                                Visible="False" />
                            <asp:DropDownList runat="server" ID="dd_is_awal" Font-Names="Tahoma" Font-Size="8">
                                <asp:ListItem Text="Bukan" Value="T"></asp:ListItem>
                                <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_closing" Text="Closing" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_closing" Text='<%#Databinder.Eval(Container, "Dataitem.closing") %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
