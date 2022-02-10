<%@ Control Language="VB" AutoEventWireup="false" CodeFile="jurnal_list.ascx.vb"
    Inherits="Forms_Transaksi_Akuntansi_jurnal_list" %>
     <%--Daniel--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
 <%--Daniel--%>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Jurnal" Font-Names="Tahoma" Font-Size="14"
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
        <td style="width: 79px">
            <asp:Label runat="server" ID="lbl111" Text="Tahun transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_refresh" Text="Refresh" Font-Names="Tahoma"
                Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label4" Text="Bulan transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
    <td align="center" colspan="3">
    <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
    <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
    </td>
    </tr>
    <%--<tr>
        <td>
            <asp:Label runat="server" ID="lbl111" Text="Tahun transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label2" Text="Bulan transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8"
                AutoPostBack="true" /></td>
    </tr>--%>
    <%--Daniel--%>
    <%--<tr>
        <td><asp:Label runat="server" ID="Label4" Text="Jenis jurnal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_jenis_jurnal" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                <asp:ListItem Text="Jurnal umum" Value="U"></asp:ListItem>
                <asp:ListItem Text="Jurnal penyesuaian" Value="P"></asp:ListItem>
            </asp:DropDownList>
            
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>--%>
    <%--Daniel--%>
</table>
<table align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="lbl_search" runat="server" Text="Search" Font-Names="Tahoma" Font-Size="8"></asp:Label>
                        <asp:TextBox runat="server" ID="tb_search" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
                    </td>
                </tr>
                <tr><td></td></tr>
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false"
                HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" Visible="true" />
                            <%--Daniel--%>
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>'
                                Visible="false" />
                            <%--Daniel--%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_jurnal" Text="No. jurnal" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--Daniel--%>
                            <asp:LinkButton runat="server" ID="lbl_no_jurnal" Text='<%#Databinder.Eval(Container, "Dataitem.id_jurnal") %>'
                                Width="100" Font-Names="Tahoma" Font-Size="8" CommandName="LinkItem" />
                            <%--Daniel--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tgl. jurnal" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_jurnal") %>'
                                Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <%--Daniel--%>
                    <%--<asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Mata uang" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_mata_uang" Text='<%#Databinder.Eval(Container, "Dataitem.id_currency") %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>--%>
                    <%--Daniel--%>
                    <%--<asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_jurnal" Text="Nama Jurnal" Width="125" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_jurnal" Text='<%#Databinder.Eval(Container, "Dataitem.nama_jurnal") %>'
                                Width="125" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>--%>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <%--Daniel--%>
                            <asp:Label runat="server" ID="lb_keterangan" Text="Keterangan" Width="300" Font-Names="Tahoma"
                                Font-Size="8" />
                            <%--Daniel--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--Daniel--%>
                            <asp:Label runat="server" ID="lbl_keterangan" Text='<%#Databinder.Eval(Container, "Dataitem.keterangan") %>'
                                Width="300" Font-Names="Tahoma" Font-Size="8" />
                            <%--<asp:Label runat="server" ID="lbl_is_submit" Text='<%#Databinder.Eval(Container, "Dataitem.is_submit") %>' Visible="false" />
                            <asp:Label runat="server" ID="lbl_submit" Text='<%#Databinder.Eval(Container, "Dataitem.status_submit") %>' Width="50" Font-Names="Tahoma" Font-Size="8" />--%>
                            <%--Daniel--%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_new" Text="Jurnal baru" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8"
                Visible="false" />
        </td>
    </tr>
</table>
