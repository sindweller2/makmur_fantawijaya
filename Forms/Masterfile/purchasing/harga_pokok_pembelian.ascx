<%@ Control Language="VB" AutoEventWireup="false" CodeFile="harga_pokok_pembelian.ascx.vb" Inherits="Forms_Masterfile_purchasing_harga_pokok_pembelian" %>

<script language="javascript" type="text/javascript">
    var disp_setting2="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting2+="scrollbars=yes,width=700, height=400, left=100, top=25"; 
    
    function popup_produk_item(tcid1, tcid2) { 
                window.open('popup_produk_item.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting2); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Harga Pokok Pembelian Produk" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl100" Text="Nama produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_nama_produk" Font-Names="Tahoma" Font-Size="8" />
                  <asp:TextBox runat="server" ID="tb_id_produk" Font-Names="Tahoma" Font-Size="8pt"/>
                  <asp:LinkButton runat="server" ID="link_refresh_produk" Text="Refresh"/>
                  <asp:LinkButton runat="server" ID="link_popup_produk" Text="Daftar Produk" Font-Names="Tahoma" Font-Size="8pt"/>
                  <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Nama kategori produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_kategori_produk" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Nama sub kategori produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_sub_category" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Satuan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_satuan" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr> 
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="Packaging" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_packaging" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>   
</table>

<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_periode" Text="Periode transaksi" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_periode" Text='<%#Databinder.Eval(Container, "Dataitem.nama_periode") %>' Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_harga" Text="HPP" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_harga" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.hpp"),2) %>' Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>