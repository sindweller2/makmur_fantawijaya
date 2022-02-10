<%@ Control Language="VB" AutoEventWireup="false" CodeFile="daftar_termin_pembayaran_import.ascx.vb" Inherits="Forms_Masterfile_Purchasing_daftar_termin_pembayaran_import" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Termin Pembayaran Import" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl100" Text="Termin pembayaran import" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_name" Width="150" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Pembayaran L/C ?" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_ls_lc" Font-Names="Tahoma" Font-Size="8" >
                <asp:ListItem Text="Bukan" Value="False"></asp:ListItem>
                <asp:ListItem Text="Ya" Value="True"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
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
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Visible="False" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_name" Text="Termin pembayaran import" Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_name" Text='<%#Databinder.Eval(Container, "Dataitem.name") %>' Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_is_lc" Text="Pembayaran L/C ?" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_is_lc" Text='<%#Databinder.Eval(Container, "Dataitem.is_lc") %>' Visible="False"/>
                            <asp:DropDownList runat="server" ID="dd_is_lc" Font-Names="Tahoma" Font-Size="8" >
                                <asp:ListItem Text="Bukan" Value="False"></asp:ListItem>
                                <asp:ListItem Text="Ya" Value="True"></asp:ListItem>
                            </asp:DropDownList>
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




