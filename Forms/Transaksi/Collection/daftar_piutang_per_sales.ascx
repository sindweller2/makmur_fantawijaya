<%@ Control Language="VB" AutoEventWireup="false" CodeFile="daftar_piutang_per_sales.ascx.vb" Inherits="Forms_Transaksi_Collection_daftar_piutang_per_sales" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
   
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
   
<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Piutang per Sales" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl384" Text="Periode" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_jatuh_tempo" Width="65" Font-Names="Tahoma" Font-Size="8" />
        <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_jatuh_tempo" /> 
            <act:CalendarExtender ID="ce_tgl_jatuh_tempo" TargetControlID="tb_tgl_jatuh_tempo" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
            <asp:Label runat="server" ID="Label2" Text=" s.d " Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_tgl_jatuh_tempo_akhir" Width="65" Font-Names="Tahoma" Font-Size="8" />     
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_jatuh_tempo_akhir" />    
            <act:CalendarExtender ID="ce_tgl_jatuh_tempo_akhir" TargetControlID="tb_tgl_jatuh_tempo_akhir" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>      
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>

<table align="center">
<tr>
        <td>
            <table runat="server" id="tbl_search">
                <tr>
                    <td><asp:Label runat="server" ID="Label4" Text="Total IDR" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" />
                        <asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Label runat="server" ID="lbl_total_idr" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Label runat="server" ID="Label8" Text="    " Font-Names="Tahoma" Font-Size="8" />
                        <asp:Label runat="server" ID="Label6" Text="Total USD" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" />
                        <asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Label runat="server" ID="lbl_total_usd" Font-Names="Tahoma" Font-Size="8" />
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
                            <asp:Label runat="server" ID="lb_sales" Text="Nama sales" Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_sales" Text='<%#Databinder.Eval(Container, "Dataitem.nama_sales") %>' Width="200" Font-Names="Tahoma" Font-Size="8" />
                            <asp:Label runat="server" ID="lbl_id_sales" Text='<%#Databinder.Eval(Container, "Dataitem.id_sales") %>' Visible="false"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_total_piutang_idr" Text="Total piutang IDR" Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_total_piutang_idr" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_idr"),3) %>' Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>             
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_total_piutang_usd" Text="Total piutang USD" Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_total_piutang_usd" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_usd"),3) %>' Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_print" Text="Print" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbl_print" Text="Print" Width="50" Font-Names="Tahoma" Font-Size="8" CommandName="LinkPrint" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn> 
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
</table>

<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\lap_piutang_per_sales.rpt"></Report>
</CR:CrystalReportSource>