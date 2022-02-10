<%@ Control Language="VB" AutoEventWireup="false" CodeFile="general_ledger.ascx.vb"
    Inherits="Forms_Transaksi_Akuntansi_general_ledger" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--Daniel--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<%--Daniel--%>
<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\general_ledger_all.rpt">
    </Report>
</CR:CrystalReportSource>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
    
    function popup_coa(tcid1, tcid2) { 
                window.open('popup_coa.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>

<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="General Ledger" Font-Names="Tahoma" Font-Size="14"
                Font-Bold="true" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>
<%--Daniel--%>
<table align="center">
<tr>
        <td>
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
</table>
<table align="center">
<tr>
        <td>
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8"
                Visible="false" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
<%--<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl111" Text="Tahun transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td style="width: 170px">
            <asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label2" Text="Bulan transaksi" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td style="width: 170px">
            <asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8"
                AutoPostBack="true" /></td>
    </tr>
</table>--%>
<%--Daniel--%>
<table align="center" runat="server" id="tbl_search">
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:RadioButton runat="server" ID="rd_semua" Text="Semua akun" Checked="true" GroupName="pilihan"
                            Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" /></td>
                    <td>
                        <asp:RadioButton runat="server" ID="rd_akun" Text="Per akun" GroupName="pilihan"
                            Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" />
                        <asp:Label runat="server" ID="lbl_akun" Font-Names="Tahoma" Font-Size="8" />
                        <asp:TextBox runat="server" ID="tb_id_akun" Font-Names="Tahoma" Font-Size="8pt" />
                        <asp:LinkButton runat="server" ID="link_refresh_akun" Text="Refresh" />
                        <asp:LinkButton runat="server" ID="link_popup_akun" Text="Daftar COA" Font-Names="Tahoma"
                            Font-Size="8pt" />
                    </td>
                </tr>
                <tr>
                <td><asp:Label runat="server" ID="LabelSearch" Text="Search :" Font-Names="Tahoma" Font-Size="8" /></td>
                <td><asp:TextBox runat="server" ID="tb_search" Font-Names="Tahoma" Font-Size="8pt" />
                    <asp:Button ID="buttonSearch" runat="server" Text="Search" Font-Names="Tahoma" Font-Size="8pt"/></td>
                </tr>
            </table>
            </td>
    </tr>
    <tr>
        <td colspan="3">
            <table align="center">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label5" Text="Total debet" Font-Names="Tahoma" Font-Size="8"
                            Font-Bold="true" />
                        <asp:Label runat="server" ID="Label6" Text=":" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Label runat="server" ID="lbl_total_debet" Font-Names="Tahoma" Font-Size="8" /></td>
                    <td>
                        <asp:Label runat="server" ID="Label7" Text="Total kredit" Font-Names="Tahoma" Font-Size="8"
                            Font-Bold="true" />
                        <asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Label runat="server" ID="lbl_total_kredit" Font-Names="Tahoma" Font-Size="8" /></td>
                    <td style="width: 100px">
                        <asp:Label runat="server" ID="Label9" Text="Selisih" Font-Names="Tahoma" Font-Size="8"
                            Font-Bold="true" />
                        <asp:Label runat="server" ID="Label10" Text=":" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Label runat="server" ID="lbl_saldo" Font-Names="Tahoma" Font-Size="8" /></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false"
                HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_seq" Text="No." Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>'
                                Visible="true" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_voucher" Text="No Voucher" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_no_voucher" Text='<%#Databinder.Eval(Container, "Dataitem.no_voucher") %>'
                                Visible="true" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_transaksi" Text="Tgl. transaksi" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tgl_transaksi" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_transaksi") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_id_transaksi" Text="No. transaksi" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id_transaksi" Text='<%#Databinder.Eval(Container, "Dataitem.id_transaksi") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <%--                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kode_transaksi" Text="Kode transaksi" 
                                Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_kode_transaksi" Text='<%#Databinder.Eval(Container, "Dataitem.kode_transaksi") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>--%>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_coa_code" Text="No. akun" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_coa_code" Text='<%#Databinder.Eval(Container, "Dataitem.coa_code") %>'
                                Visible="true" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_coa_name" Text="Nama akun" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_akun" Text='<%#Databinder.Eval(Container, "Dataitem.nama_akun") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_debet" Text="Nilai debet" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--Daniel--%>
                            <asp:Label runat="server" ID="lbl_nilai_debet" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_debet"),2) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                            <%--Daniel--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_kredit" Text="Nilai kredit" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--Daniel--%>
                            <asp:Label runat="server" ID="lbl_nilai_kredit" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_kredit"),2) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                            <%--Daniel--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kurs" Text="Kurs" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--Daniel--%>
                            <asp:Label runat="server" ID="lbl_kurs" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.kurs"),2) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                            <%--Daniel--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_keterangan" Text="Keterangan" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="tb_keterangan" Text='<%#Databinder.Eval(Container, "Dataitem.keterangan") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button ID="btn_delete" runat="server" Text="Delete" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
