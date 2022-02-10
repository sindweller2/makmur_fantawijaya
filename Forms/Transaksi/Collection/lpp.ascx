<%@ Control Language="VB" AutoEventWireup="false" CodeFile="lpp.ascx.vb" Inherits="Forms_Transaksi_Collection_lpp" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=700, height=400, left=100, top=25"; 
        
    function popup_so_collection(tcid1, tcid2) { 
                window.open('popup_so_collection.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="LPP" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label24" Text="Periode transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label25" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_periode" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="lbl111" Text="Tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tanggal" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal" />
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label22" Text="Invoice/NN ?" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label23" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_faktur_nn" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
                <asp:ListItem Text="Invoice" Value="F"></asp:ListItem>
                <asp:ListItem Text="N/N" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="No. invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_invoice" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_no_invoice" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_no_invoicer" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_no_invoice" Text="Daftar Invoice" Font-Names="Tahoma" Font-Size="8pt"/></td>
        <td><asp:Label runat="server" ID="Label4" Text="Tgl. invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="100"><asp:Label runat="server" ID="lbl_tgl_penjualan" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama customer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_nama_customer" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label26" Text="Nilai invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label27" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="100"><asp:Label runat="server" ID="lbl_nilai_invoice" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="Jenis pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_jenis_pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label14" Text="Nama bank" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_nama_bank" Width="250" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label16" Text="No. Cek/Giro" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="150"><asp:TextBox runat="server" ID="tb_no_giro" Width="150" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label18" Text="Tgl. Cek/Giro" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_cek_giro" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_cek_giro" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="Jumlah nilai" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_jumlah" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_jumlah" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Keterangan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_keterangan" Width="350" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /> </td>        
        <td><asp:Button runat="server" ID="btn_print" Text="Print" Font-Names="Tahoma" Font-Size="8" /> </td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /> </td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" Font-Names="Tahoma" Font-Size="8pt"/>
                            <asp:Label runat="server" ID="lbl_is_submit" Text='<%#Databinder.Eval(Container, "Dataitem.is_submit") %>' Visible="false" />
                            <asp:Label runat="server" ID="lbl_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_seq" Text="No." Font-Names="Tahoma" Font-Size="8pt" Width="50" />
                        </HeaderTemplate>
                        <ItemTemplate>                            
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>' Font-Names="Tahoma" Font-Size="8pt"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_invoice" Text="Nama customer" Font-Names="Tahoma" Font-Size="8pt" Width="100" />
                        </HeaderTemplate>
                        <ItemTemplate>                            
                            <asp:Label runat="server" ID="lbl_nama_customer" Text='<%#Databinder.Eval(Container, "Dataitem.nama_customer") %>' Font-Names="Tahoma" Font-Size="8pt"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_invoice" Text="No. invoice" Width="65" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_invoice" Text='<%#Databinder.Eval(Container, "Dataitem.no_invoice") %>' Width="65" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal_invoice" Text="Tgl. invoice" Width="65" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_tanggal_invoice" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>' Width="65" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jenis_pembayaran" Text="Jenis pembayaran" Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jenis_pembayaran" Text='<%#Databinder.Eval(Container, "Dataitem.id_jenis_pembayaran") %>' Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_jenis_pembayaran" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Mata uang" Width="50" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_mata_uang" Text='<%#Databinder.Eval(Container, "Dataitem.id_mata_uang") %>' Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_bank" Text="Nama bank" Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_nama_bank" Text='<%#Databinder.Eval(Container, "Dataitem.nama_bank") %>' Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_cek_giro" Text="No. cek/giro" Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_no_cek_giro" Text='<%#Databinder.Eval(Container, "Dataitem.no_cek_giro") %>' Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_cek_giro" Text="Tgl. cek/giro" Width="75" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_cek_giro" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_cek_giro") %>' Width="65" Font-Names="Tahoma" Font-Size="8pt" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender123" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_cek_giro" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jumlah_nilai" Text="Jumlah nilai" Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_jumlah_nilai" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah_nilai"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender123" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_jumlah" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_keterangan" Text="Keterangan" Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_keterangan" Text='<%#Databinder.Eval(Container, "Dataitem.keterangan") %>' Width="100" Font-Names="Tahoma" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8pt" />
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8pt" />            
        </td>
    </tr>
</table>


<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    <Report FileName="reports\lpp.rpt"></Report>
</CR:CrystalReportSource>