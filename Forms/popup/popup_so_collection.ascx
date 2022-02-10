<%@ Control Language="VB" AutoEventWireup="false" CodeFile="popup_so_collection.ascx.vb" Inherits="Forms_Popup_popup_so_collection" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl1" Text="Daftar Sales Order" Font-Names="Tahoma" Font-Size="12" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8pt" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt" />
        </td>
    </tr>
    <tr>
        <td>
            <table runat="server" id="tbl_search" width="450">
                <tr>
                    <td><asp:Label runat="server" ID="lb012" Text="No. Sales order" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8pt" />
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
                            <asp:Label runat="server" ID="lb_alamat" Text="No. Sales Order" Font-Names="Tahoma" Font-Size="8pt" Width="100" />
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.no") %>' Visible="false" />
                        <asp:LinkButton runat="server" ID="lbl_no_so" Text='<%#Databinder.Eval(Container, "Dataitem.so_no_text") %>' Font-Names="Tahoma" Font-Size="8pt" CommandName="LinkItem"  />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tanggal" Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>' Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_customer" Text="Nama customer" Width="200" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_customer" Text='<%#Databinder.Eval(Container, "Dataitem.nama_customer") %>' Width="200" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Mata uang" Width="75" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_mata_uang" Text='<%#Databinder.Eval(Container, "Dataitem.mata_uang") %>' Width="75" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jumlah_nilai" Text="Jumlah nilai" Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jumlah_nilai" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah_nilai"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>