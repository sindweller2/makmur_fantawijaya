<%@ Control Language="VB" AutoEventWireup="false" CodeFile="group_menu.ascx.vb" Inherits="form_setting_group_menu" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl1" Text="Hak akses grup user" Font-Names="Tahoma" Font-Size="12" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Grup user" Font-Names="Tahoma" Font-Size="8" />
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" />
            <asp:DropDownList runat="server" ID="dd_user_group" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
            
<table width="100%">    
    <tr>
        <td width="50%" valign="top">
            <table align="center">
                <tr>
                    <td>
                        <table align="center" width="250" runat="server" id="tbl_assign">
                            <tr>
                                <td align="right"><asp:Button runat="server" ID="btn_assign" Text="Assign" Font-Names="Tahoma" Font-Size="8" /></td>
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
                                        <asp:CheckBox runat="server" ID="cb_data" />
                                        <asp:Label runat="server" ID="lbl_code" Text='<%#Databinder.Eval(Container, "Dataitem.code") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_userlogin" Text="Menu" Width="250" Font-Names="Tahoma" Font-Size="8"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_userlogin" Text='<%#Databinder.Eval(Container, "Dataitem.name") %>' Font-Names="Tahoma" Font-Size="8"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">            
            <table align="center">
                <tr>
                    <td>
                        <table align="center" width="250" runat="server" id="tbl_group_menu">
                            <tr>
                                <td align="right"><asp:Button runat="server" ID="btn_cancel" Text="Cancel" Font-Names="Tahoma" Font-Size="8" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_group_menu" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="cb_data" />
                                        <asp:Label runat="server" ID="lbl_code" Text='<%#Databinder.Eval(Container, "Dataitem.code") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_userlogin" Text="Menu" Width="250" Font-Names="Tahoma" Font-Size="8"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_userlogin" Text='<%#Databinder.Eval(Container, "Dataitem.name") %>' Font-Names="Tahoma" Font-Size="8"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

