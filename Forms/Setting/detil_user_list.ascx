<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_user_list.ascx.vb" Inherits="Forms_Setting_detil_user_list" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil User" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Nama pegawai" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_nama_pegawai" Width="150" Font-Names="Tahoma" Font-Size="8" />  </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl199" Text="Nama user" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_name" Width="150" Font-Names="Tahoma" Font-Size="8" />  </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Password" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_pwd" Width="150" Font-Names="Tahoma" Font-Size="8" />  </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Email" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_email" Width="150" Font-Names="Tahoma" Font-Size="8" />  </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama grup user" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDOwnList runat="server" ID="dd_group" Font-Names="Tahoma" Font-Size="8" />  </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="Status" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDOwnList runat="server" ID="dd_status" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Aktif" Value="1"></asp:ListItem>
                <asp:ListItem Text="Tidak aktif" Value="0"></asp:ListItem>
            </asp:DropDOwnList>
        </td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
