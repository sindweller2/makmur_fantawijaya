<%@ Control Language="VB" AutoEventWireup="false" CodeFile="saldo_awal_customer.ascx.vb" Inherits="Forms_Transaksi_Akuntansi_saldo_awal_customer" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Saldo Awal Customer" Font-Names="Tahoma" Font-Size="14pt" Font-Bold="True" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>
            <table align="center">
                <tr>
                    <td><asp:Label runat="server" ID="lbl234" Text="Nama Customer" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
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
                            <asp:CheckBox runat="server" ID="cb_data" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_id" Text="ID" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_customer" Text="Nama Customer" Width="250" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_customer" Text='<%#Databinder.Eval(Container, "Dataitem.name") %>' Width="250" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_saldo" Text="Saldo" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_saldo" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.saldo"),2) %>' Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
        <asp:Button runat="server" ID="btn_add" Text="Add" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>