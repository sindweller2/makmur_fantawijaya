<%@ Control Language="VB" AutoEventWireup="false" CodeFile="popup_sales.ascx.vb"
    Inherits="Forms_Popup_popup_sales" %>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl1" Text="Daftar Sales" Font-Names="Tahoma" Font-Size="12"
                Font-Bold="true" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8pt" ForeColor="red" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl2" Text="Nama Sales" Font-Names="Tahoma" Font-Size="8pt" />
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8pt" />
            <asp:TextBox runat="server" ID="tb_search" Width="150" Font-Names="Tahoma" Font-Size="8pt" />
            <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8pt" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt" />
        </td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false"
                HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_pegawai" Text="Nama sales" Font-Names="Tahoma"
                                Font-Size="8pt" Width="250" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_code" Text='<%#Databinder.Eval(Container, "Dataitem.code") %>'
                                Visible="false" />
                            <asp:LinkButton runat="server" ID="lbl_nama_pegawai" Text='<%#Databinder.Eval(Container, "Dataitem.nama_pegawai") %>'
                                Font-Names="Tahoma" Font-Size="8pt" CommandName="LinkItem" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_status" Text="Status" Font-Names="Tahoma" Font-Size="8pt"
                                Width="150" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_status" Text='<%#Databinder.Eval(Container, "Dataitem.status") %>'
                                Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>
