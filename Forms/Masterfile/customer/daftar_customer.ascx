<%@ Control Language="VB" AutoEventWireup="false" CodeFile="daftar_customer.ascx.vb"
    Inherits="Forms_Masterfile_customer_daftar_customer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Daftar Customer" Font-Names="Tahoma" Font-Size="14"
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
        <td align="right">
            <asp:Button runat="server" ID="btn_new" Text="Customer baru" Font-Names="Tahoma"
                Font-Size="8" />
            <asp:Button runat="server" ID="btn_generate" Text="Generate akun" Font-Names="Tahoma"
                Font-Size="8" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <table runat="server" id="tbl_search" width="700">
                <tr>
                    <td align="right">
                        <%--<asp:Label runat="server" ID="lbl_search" Text="Nama customer" Font-Names="Tahoma" Font-Size="8" />--%>
                        <ajax:UpdatePanel ID="UpdatePanelFilter" runat="server">
                            <ContentTemplate>
                                <%--<asp:DropDownList ID="DropDownListFilter" AutoPostBack="True" runat="server">
                                    <asp:ListItem>Nama customer</asp:ListItem>
                                    <asp:ListItem>Nama grup</asp:ListItem>
                                    <asp:ListItem>Nama sales</asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:Label runat="server" ID="label">Search:</asp:Label>
                            </ContentTemplate>
                        </ajax:UpdatePanel>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tb_search" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false"
                HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_name" Text="Nama customer" Width="250" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>'
                                Visible="false" />
                            <asp:LinkButton runat="server" ID="lbl_name" Text='<%#Databinder.Eval(Container, "Dataitem.name") %>'
                                Width="250" Font-Names="Tahoma" Font-Size="8" CommandName="LinkItem" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_grup" Text="Nama grup" Width="200" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_grup" Text='<%#Databinder.Eval(Container, "Dataitem.nama_grup") %>'
                                Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_sales" Text="Nama sales" Width="150" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_sales" Text='<%#Databinder.Eval(Container, "Dataitem.nama_sales") %>'
                                Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_status" Text="Status" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_status" Text='<%#Databinder.Eval(Container, "Dataitem.status") %>'
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
