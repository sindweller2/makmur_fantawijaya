<%@ Control Language="VB" AutoEventWireup="false" CodeFile="produk_item_detil.ascx.vb" Inherits="Forms_Masterfile_produk_produk_item_detil" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Produk Item" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl100" Text="Nama kategori produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_category" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Nama sub kategori produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_sub_category" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Nama produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_name" Width="300" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Merek produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_merek" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>   
    <tr>
        <td><asp:Label runat="server" ID="Label14" Text="Packaging" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_packaging_qty" Width="50" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_packaging_qty" />
            <asp:DropDownList runat="server" ID="dd_satuan_packaging" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Label runat="server" ID="Label12" Text="/" Font-Names="Tahoma" Font-Size="8" />
            <asp:DropDownList runat="server" ID="dd_satuan" Font-Names="Tahoma" Font-Size="8"/>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Satuan jual" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_is_packaging" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Per satuan packaging" Value="P"></asp:ListItem>
                <asp:ListItem Text="Per satuan qty packaging" Value="Q"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>        
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="Min. qty" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_min_qty" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender13" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_min_qty" />
        </td>
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="lbl34" Text="Status" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label16" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_status" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Aktif" Value="True"></asp:ListItem>
                <asp:ListItem Text="Tidak aktif" Value="False"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>    
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
</table>