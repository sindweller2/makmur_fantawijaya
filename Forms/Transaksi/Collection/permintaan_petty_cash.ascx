<%@ Control Language="VB" AutoEventWireup="false" CodeFile="permintaan_petty_cash.ascx.vb" Inherits="Forms_Transaksi_Collection_permintaan_petty_cash" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Permintaan Petty Cash" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <ajax:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />            
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

<table align="center" width="600">
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Permintaan Petty cash" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>
    
<table align="center" width="600">
    <tr>
        <td width="100"><asp:Label runat="server" ID="lbl203" Text="Tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label4" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tanggal" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal" />
        <act:CalendarExtender ID="ce_tanggal" TargetControlID="tb_tanggal" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
        <td width="200"><asp:Label runat="server" ID="Label8" Text="Tunai/Non tunai" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_is_cash" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Tunai" Value="T"></asp:ListItem>
                <asp:ListItem Text="Non tunai" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="100"><asp:Label runat="server" ID="Label10" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="200"><asp:Label runat="server" ID="Label5" Text="Jumlah nilai" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_nilai" Width="100" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_nilai" />
        </td>
    </tr>
    <tr>
        <td width="100"><asp:Label runat="server" ID="Label12" Text="Keterangan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_keterangan" Width="250" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center">
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
                            <asp:Label runat="server" ID="lb_no" Text="No. permintaan" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tanggal" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender134" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_is_cash" Text="Tunai/Non tunai" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_is_cash" Text='<%#Databinder.Eval(Container, "Dataitem.is_cash") %>' Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_is_cash" Font-Names="Tahoma" Font-Size="8" >
                                <asp:ListItem Text="Tunai" Value="T"></asp:ListItem>
                                <asp:ListItem Text="Non tunai" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Mata uang" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_mata_uang" Text='<%#Databinder.Eval(Container, "Dataitem.id_currency") %>' Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Jumlah nilai" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_nilai" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender123" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_nilai" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jumlah_diberikan" Text="Jumlah diberikan" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jumlah_diberikan" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah_dikeluarkan"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_keterangan" Text="Keterangan" Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_keterangan" Text='<%#Databinder.Eval(Container, "Dataitem.keterangan") %>' Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_status" Text="Status" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_status" Text='<%#Databinder.Eval(Container, "Dataitem.status") %>' Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_status" Font-Names="Tahoma" Font-Size="8" Enabled="false">
                                <asp:ListItem Text="Pending" Value="P"></asp:ListItem>
                                <asp:ListItem Text="Ok" Value="O"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_submit" Text="Submit" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_submit" Text='<%#Databinder.Eval(Container, "Dataitem.is_submit") %>' Visible="false" />
                            <asp:LinkButton runat="server" ID="link_submit" Text="Submit" Width="50" Font-Names="Tahoma" Font-Size="8" CommandName="LinkItem" />
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


