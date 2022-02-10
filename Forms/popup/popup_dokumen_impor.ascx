<%@ Control Language="VB" AutoEventWireup="false" CodeFile="popup_dokumen_impor.ascx.vb" Inherits="Forms_Popup_popup_dokumen_impor" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl1" Text="Daftar Dokumen Impor" Font-Names="Tahoma" Font-Size="12" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8pt" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>
            <table width="600">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl2" Text="No. dokumen impor" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:TextBox runat="server" ID="tb_search" Width="150" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt" />
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
                            <asp:Label runat="server" ID="lb_nama" Text="No. dokumen impor" Font-Names="Tahoma" Font-Size="8pt" Width="150" />
                        </HeaderTemplate>
                        <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>' Font-Names="Tahoma" Font-Size="8pt" CommandName="LinkItem"  />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_po" Text="No. P.O" Font-Names="Tahoma" Font-Size="8pt" Width="100" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_no_po" Text='<%#Databinder.Eval(Container, "Dataitem.po_no_text") %>' Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_bl_no" Text="No. B/L / AWB" Font-Names="Tahoma" Font-Size="8pt" Width="100" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_bl_no" Text='<%#Databinder.Eval(Container, "Dataitem.bl_no") %>' Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_packing_list_no" Text="No. Packing list" Font-Names="Tahoma" Font-Size="8pt" Width="100" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_packing_list_no" Text='<%#Databinder.Eval(Container, "Dataitem.packing_list_no") %>' Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_invoice_no" Text="No. Invoice" Font-Names="Tahoma" Font-Size="8pt" Width="100" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_invoice_no" Text='<%#Databinder.Eval(Container, "Dataitem.invoice_no") %>' Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>