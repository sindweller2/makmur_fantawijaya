<%@ Control Language="VB" AutoEventWireup="false" CodeFile="penugasan_ekspedisi_impor.ascx.vb" Inherits="Forms_Transaksi_Pembelian_penugasan_ekspedisi_impor" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=700, height=400, left=100, top=25"; 
        
    function popup_ekspedisi_impor(tcid1, tcid2) { 
                window.open('popup_ekspedisi_impor.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
    
    function popup_dokumen_impor(tcid1, tcid2) { 
                window.open('popup_dokumen_impor.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Penugasan Ekspedisi Impor" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="Tahun transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tahun" Width="50" Font-Names="Tahoma" Font-Size="8" />
        <ajax:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun" />
            <asp:Button runat="server" ID="btn_view" Text="View" Font-Names="Tahoma" Font-Size="8" />            
        </td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Bulan transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_bulan" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true"/></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama ekspedisi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="350"><asp:Label runat="server" ID="lbl_nama_ekspedisi" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_id_ekspedisi" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_ekspedisi" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_ekspedisi" Text="Daftar Ekspedisi" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_tanggal" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal" />     
            <ajax:CalendarExtender ID="ce_tanggal" TargetControlID="tb_tanggal" runat="server" Animated="true" Format="dd/MM/yyyy" />       
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label11" Text="No. Aju Ekspedisi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label12" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:TextBox runat="server" ID="tb_no_aju" Width="150" Font-Names="Tahoma" Font-Size="8"/>
            <asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>


<table align="center">
    <tr>
        <td width="50%" valign="top">
            <table align="center">
                <tr>
                    <td>
                        <table runat="server" id="tbl_search" width="450">
                            <tr>
                                <td><asp:Label runat="server" ID="lbl1034" Text="No. AJU" Font-Names="Tahoma" Font-Size="8" />
                                    <asp:TextBox runat="server" ID="tb_search" Width="100" Font-Names="Tahoma" Font-Size="8" />
                                    <asp:Button runat="server" ID="btn_search" Text="Search" Font-Names="Tahoma" Font-Size="8" />
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
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="cb_data" />
                                        <asp:Label runat="server" ID="lbl_id_expedition" Text='<%#Databinder.Eval(Container, "Dataitem.id_expedition") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_seq" Text="No. penugasan" Width="75" Font-Names="Tahoma" Font-Size="8" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>' Width="75" Font-Names="Tahoma" Font-Size="8"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_tanggal" Text="Tanggal" Width="75" Font-Names="Tahoma" Font-Size="8" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>' Width="75" Font-Names="Tahoma" Font-Size="8" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_nama" Text="Nama ekspedisi" Width="175" Font-Names="Tahoma" Font-Size="8" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_nama" Text='<%#Databinder.Eval(Container, "Dataitem.nama_ekspedisi") %>' Width="175" Font-Names="Tahoma" Font-Size="8"/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_no_aju" Text="No. AJU" Width="100" Font-Names="Tahoma" Font-Size="8" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="tb_no_aju" Text='<%#Databinder.Eval(Container, "Dataitem.no_aju") %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lb_detil" Text="Dokumen impor" Width="75" Font-Names="Tahoma" Font-Size="8" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbl_detil" Text="Detil" Width="75" Font-Names="Tahoma" Font-Size="8" CommandName="LinkItem" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
                        <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
            <table runat="server" id="tbl_dokumen">
                <tr>
                    <td>
                        <table align="center">
                            <tr>
                                <td><asp:Label runat="server" ID="lbl12" Text="Nama ekspedisi : " Font-Names="Tahoma" Font-Size="8" />
                                    <asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" />
                                    <asp:Label runat="server" ID="lbl_nama_ekspedisi_dokumen" Font-Names="Tahoma" Font-Size="8" />
                                </td>                                
                            </tr>
                             <tr>
                                <td><asp:Label runat="server" ID="Label8" Text="No. dokumen impor" Font-Names="Tahoma" Font-Size="8"/>
                                    <asp:Label runat="server" ID="Label10" Text=":" Font-Names="Tahoma" Font-Size="8"/>
                                    <asp:Label runat="server" ID="lbl_no_dokumen_impor" Font-Names="Tahoma" Font-Size="8" />
                                    <asp:TextBox runat="server" ID="tb_id_dokumen_impor" Font-Names="Tahoma" Font-Size="8pt"/>
                                    <asp:LinkButton runat="server" ID="link_refresh_dokumen_impor" Text="Refresh"/>
                                    <asp:LinkButton runat="server" ID="link_popup_dokumen_impor" Text="Daftar Dok. Impor" Font-Names="Tahoma" Font-Size="8pt"/>
                                    <asp:Button runat="server" ID="btn_add" Text="Add" Font-Names="Tahoma" Font-Size="8pt"/>
                                    <asp:Button runat="server" ID="btn_close_dokumen" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/>
                                </td>
                            </tr>
                        </table>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data_dokumen" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="cb_data" />                                                    
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" ID="lb_bl_no" Text="No. dokumen" Width="75" Font-Names="Tahoma" Font-Size="8" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbl_seq_dokumen" Text='<%#Databinder.Eval(Container, "Dataitem.seq_entry_dokumen_impor") %>' Font-Names="Tahoma" Font-Size="8" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" ID="lb_bl_no" Text="No. B/L / AWB" Width="100" Font-Names="Tahoma" Font-Size="8" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbl_bl_no" Text='<%#Databinder.Eval(Container, "Dataitem.bl_no") %>' Font-Names="Tahoma" Font-Size="8" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" ID="lb_packing_list_no" Text="No. Packing list" Width="100" Font-Names="Tahoma" Font-Size="8" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbl_packing_list_no" Text='<%#Databinder.Eval(Container, "Dataitem.packing_list_no") %>' Font-Names="Tahoma" Font-Size="8" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" ID="lb_invoice_no" Text="No. invoice" Width="100" Font-Names="Tahoma" Font-Size="8" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbl_invoice_no" Text='<%#Databinder.Eval(Container, "Dataitem.invoice_no") %>' Font-Names="Tahoma" Font-Size="8" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Button runat="server" ID="btn_delete_dokumen" Text="Delete" Font-Names="Tahoma" Font-Size="8" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>                        
        </td>
    </tr>
</table>

