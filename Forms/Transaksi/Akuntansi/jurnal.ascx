<%@ Control Language="VB" AutoEventWireup="false" CodeFile="jurnal.ascx.vb" Inherits="Forms_Transaksi_Akuntansi_jurnal" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\general_ledger_all.rpt"></Report>
</CR:CrystalReportSource>


<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
    
    function popup_coa(tcid1, tcid2) { 
                window.open('popup_coa.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Jurnal" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
<%--Daniel--%>
    <%--<tr>
        <td><asp:Label runat="server" ID="Label6" Text="Periode" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_periode" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl302" Text="Jenis jurnal" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_jenis_jurnal" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>--%>
    <%--Daniel--%>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="No. jurnal" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_jurnal" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label4" Text="Tgl. jurnal" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_jurnal" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_jurnal" />
        </td>
    </tr>
    <%--Daniel--%>
    <%--<tr>
        <td><asp:Label runat="server" ID="Label8" Text="Mata uang" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text="Kurs" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label12" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_kurs" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
        </td>
    </tr> --%>
    <%--Daniel--%>
    <tr>
        <td><asp:Label runat="server" ID="Label13" Text="Keterangan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label14" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
         <%--Daniel--%>
        <td colspan="6"><asp:TextBox runat="server" ID="tb_keterangan" Width="90%" Font-Names="Tahoma" Font-Size="8"/></td>
     <%--Daniel--%>
    </tr>   
</table>

<table align="center">
    <tr>
        <td style="width: 3px"><asp:Button id="btn_save" runat="server" Font-Size="8" Font-Names="Tahoma" Text="Save"></asp:Button></td>
        <%--Daniel--%>
        <%--<td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8"/></td>--%>
        <%--Daniel--%>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
</table>

<table align="center" runat="server" id="general_ledger">
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Item jurnal" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="lbl_akun" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_akun" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_akun" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_akun" Text="Daftar COA" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
        <td><asp:DropDownList runat="server" ID="dd_debet_kredit" Font-Names="Tahoma" Font-Size="8">
               <asp:ListItem Text="Debet" Value="D"></asp:ListItem>
               <asp:ListItem Text="Kredit" Value="K"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td><asp:TextBox runat="server" ID="tb_nilai" Width="75" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_nilai" />
            <asp:Button runat="server" ID="btn_add" Text="Add" Font-Names="Tahoma" Font-Size="8"/>
        </td>
    </tr>
</table>    

<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kode_akun" Text="Kode akun" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                                <asp:Label runat="server" ID="lbl_kode_akun" Text='<%#Databinder.Eval(Container, "Dataitem.coa_code") %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_akun" Text="Nama akun" Width="350" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nama_akun" Text='<%#Databinder.Eval(Container, "Dataitem.nama_akun") %>' Width="350" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_debet" Text="Nilai debet" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_debet" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_debet"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                            <%--Daniel--%>
                            <%--<ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_debet" />--%>
                        <%--Daniel--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kredit" Text="Nilai kredit" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_kredit" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_kredit"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                           <%--Daniel--%>
                            <%--<ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender132" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kredit" />--%>
                        <%--Daniel--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;<asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8" Visible="false" />
        </td>
    </tr>
</table>