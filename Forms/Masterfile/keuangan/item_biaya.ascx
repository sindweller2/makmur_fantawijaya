<%@ Control Language="VB" AutoEventWireup="false" CodeFile="item_biaya.ascx.vb" Inherits="Forms_Masterfile_keuangan_item_biaya" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Item Biaya" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td align="right"><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_name" Text="Nama item biaya" Width="250" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Visible="False" />
                            <asp:Label runat="server" ID="lbl_name" Text='<%#Databinder.Eval(Container, "Dataitem.name") %>' Width="250" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>



