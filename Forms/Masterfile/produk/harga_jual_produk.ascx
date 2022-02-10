<%@ Control Language="VB" AutoEventWireup="false" CodeFile="harga_jual_produk.ascx.vb" Inherits="Forms_Masterfile_produk_harga_jual_produk" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Harga Jual Produk" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_sinkron" Text="Upload produk" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl100" Text="Nama kategori produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_category" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Nama sub kategori produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_sub_category" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />            
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr> 
    <tr>
        <td>&nbsp;</td>
    </tr>   
</table>

<table align="center">
    <tr>
        <td>
            <table runat="server" id="tbl_search" width="725">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl2001" Text="Nama produk" Font-Names="Tahoma" Font-Size="8" />
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="Label4" Text="Harga" Font-Names="Tahoma" Font-Size="8" />
                        <asp:RadioButton runat="server" ID="rad_all" GroupName="harga" Text="Semua" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
                        <asp:RadioButton runat="server" ID="rad_full" GroupName="harga" Text="Sudah diisi" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
                        <asp:RadioButton runat="server" ID="rad_null" GroupName="harga" Text="Belum diisi" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderTemplate>                        
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_name_beli" Text="Nama produk" Font-Names="Tahoma" Font-Size="8" Width="250" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_name" Text='<%#Databinder.Eval(Container, "Dataitem.nama_beli") %>' Font-Names="Tahoma" Font-Size="8pt"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_packaging" Text="Packaging" Font-Names="Tahoma" Font-Size="8" Width="150" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_packaging" Text='<%#Databinder.Eval(Container, "Dataitem.packaging") %>' Width="150" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_measurement" Text="Satuan produk" Font-Names="Tahoma" Font-Size="8" Width="100" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_measurement" Text='<%#Databinder.Eval(Container, "Dataitem.satuan_produk") %>' Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Mata uang" Font-Names="Tahoma" Font-Size="8" Width="100" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_mata_uang" Text='<%#Databinder.Eval(Container, "Dataitem.id_currency") %>' Font-Names="Tahoma" Font-Size="8" /> 
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_harga_jual" Text="Harga jual" Font-Names="Tahoma" Font-Size="8" Width="100" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_harga_jual" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.harga_jual"),3) %>' Font-Names="Tahoma" Font-Size="8"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_harga_jual" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_status" Text="Status" Font-Names="Tahoma" Font-Size="8" Width="100" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_status" Text='<%#Databinder.Eval(Container, "Dataitem.status") %>' Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>