<%@ Control Language="VB" AutoEventWireup="false" CodeFile="coa_new.ascx.vb" Inherits="Forms_Masterfile_accounting_coa_new" %>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="COA" Font-Names="Tahoma" Font-Size="14"
                Font-Bold="true" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td align="center" colspan="3">
            <asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label2" Text="COA" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_jenis_coa" Font-Names="Tahoma" Font-Size="8"
                AutoPostBack="true">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>Piutang Karyawan</asp:ListItem>
                <asp:ListItem>Hutang Lain - Lain</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="label7" Text="Name" Font-Names="Tahoma" Font-Size="8" />
        </td>
        <td>
            <asp:TextBox runat="server" ID="tb_InaName" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_save" runat="server" Font-Names="Tahoma" Font-Size="8" Text="Save" />
            <asp:Button ID="btn_close" runat="server" Font-Names="Tahoma" Font-Size="8" Text="Close" /></td>
    </tr>
</table>
