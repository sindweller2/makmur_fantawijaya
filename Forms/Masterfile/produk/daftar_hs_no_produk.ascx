<%@ Control Language="VB" AutoEventWireup="false" CodeFile="daftar_hs_no_produk.ascx.vb" Inherits="Forms_Masterfile_Produk_daftar_hs_no_produk" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar No. HS Produk" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td><asp:DropDownList runat="server" ID="dd_category" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Nama sub kategori produk" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_sub_category" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Size="8" />
        </td>
    </tr> 
    <tr>
        <td>&nbsp;</td>
    </tr>   
</table>

<table align="center">
    <tr>
        <td>
            <table runat="server" id="tbl_search" width="600">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl2001" Text="Nama produk" Font-Names="Tahoma" Font-Size="8" />
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
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
                            <asp:Label runat="server" ID="lb_name_beli" Text="Nama produk" Font-Names="Tahoma" Font-Size="8" Width="250" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Visible="false" />
                            <asp:Label runat="server" ID="lbl_name" Text='<%#Databinder.Eval(Container, "Dataitem.nama_beli") %>' Font-Names="Tahoma" Font-Size="8pt"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_measurement" Text="Packaging" Font-Names="Tahoma" Font-Size="8" Width="150" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_measurement" Text='<%#Databinder.Eval(Container, "Dataitem.packaging") %>' Width="150" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_hs_no" Text="H.S no." Font-Names="Tahoma" Font-Size="8" Width="150" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_hs_no" Text='<%#Databinder.Eval(Container, "Dataitem.hs_no") %>' Width="100" Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_ppn" Text="PPN (%)" Font-Names="Tahoma" Font-Size="8" Width="50" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_ppn" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.ppn_tax"),2) %>' Width="50" MaxLength="3" Font-Names="Tahoma" Font-Size="8"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender11453" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_ppn" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_bbm" Text="BBM (%)" Font-Names="Tahoma" Font-Size="8" Width="50" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_bbm" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.bbm_tax"),2) %>' Width="50" MaxLength="3" Font-Names="Tahoma" Font-Size="8"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1154" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_bbm" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_ppnbm" Text="PPNBM (%)" Font-Names="Tahoma" Font-Size="8" Width="50" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_ppnbm" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.ppnbm_tax"),2) %>' Width="50" MaxLength="3" Font-Names="Tahoma" Font-Size="8"/>
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1132" FilterType="custom, numbers" ValidChars=",." TargetControlID="tb_ppnbm" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn> 
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>
