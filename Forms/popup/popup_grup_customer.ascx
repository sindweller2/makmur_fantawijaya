<%@ Control Language="VB" AutoEventWireup="false" CodeFile="popup_grup_customer.ascx.vb" Inherits="Forms_Popup_popup_grup_customer" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl1" Text="Daftar grup customer" Font-Names="Tahoma" Font-Size="12" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8pt" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>
            <table width="600">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl2" Text="Nama grup customer" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:TextBox runat="server" ID="tb_search" Width="150" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt" />
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
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama" Text="Nama grup customer" Font-Names="Tahoma" Font-Size="8pt" Width="450" />
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:Label runat="server" ID="lbl_code" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Visible="false" />
                        <asp:LinkButton runat="server" ID="lbl_nama" Text='<%#Databinder.Eval(Container, "Dataitem.name") %>' Font-Names="Tahoma" Font-Size="8pt" CommandName="LinkItem"  />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>