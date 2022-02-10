<%@ Control Language="VB" AutoEventWireup="false" CodeFile="nomor_depan_faktur_pajak.ascx.vb" Inherits="Forms_Masterfile_akuntansi_nomor_depan_faktur_pajak" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Nomor Depan Faktur Pajak" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl1" Text="Tahun" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tahun" Width="50" MaxLength="4" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label24" Text="Kawasan berikat ?" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label25" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_is_kawasan_berikat" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
                <asp:ListItem Text="Tidak" Value="T"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Nomor depan faktur pajak" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_nomor_depan" Width="100" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
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
                            <asp:Label runat="server" ID="lb_tahun" Text="Tahun" Width="75" Font-Names="Tahoma" Font-Size="8pt"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tahun" Text='<%#Databinder.Eval(Container, "Dataitem.tahun") %>' Width="50" Font-Names="Tahoma" Font-Size="8pt"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kawasan_berikat" Text="Kawasan berikat ?" Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_is_kawasan_berikat" Text='<%#Databinder.Eval(Container, "Dataitem.is_kawasan_berikat") %>' Visible="false"/>
                            <asp:Label runat="server" ID="lbl_kawasan_berikat" Text='<%#Databinder.Eval(Container, "Dataitem.kawasan_berikat") %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nomor_depan" Text="Nomor depan faktur pajak" Width="200" Font-Names="Tahoma" Font-Size="8pt"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_nomor_depan" Text='<%#Databinder.Eval(Container, "Dataitem.no_depan") %>' Width="200" Font-Names="Tahoma" Font-Size="8pt"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8pt" />
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8pt" />
        </td>
    </tr>
</table>