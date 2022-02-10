<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_received_expedition_invoice.ascx.vb" Inherits="Forms_Transaksi_Pembelian_detil_received_expedition_invoice" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
        
    function popup_penugasan(tcid1, tcid2) { 
                window.open('popup_penugasan.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>


<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Penerimaan Invoice Ekspedisi" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>


<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label14" Text="Periode transaksi" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label15" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_periode_transaksi" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="No. penerimaan" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no" Font-Names="Tahoma" Font-Size="8"/></td> 
        <td><asp:Label runat="server" ID="Label24" Text="Tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label25" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tanggal" Width="65" Font-Names="Tahoma" Font-Size="8"/>
               <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tanggal" />
        <ajax:CalendarExtender ID="ce_tanggal" TargetControlID="tb_tanggal" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td> 
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="No. aju ekspedisi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_aju" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_no_aju" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:LinkButton runat="server" ID="link_refresh_no_aju" Text="Refresh"/>
            <asp:LinkButton runat="server" ID="link_popup_no_aju" Text="Daftar No. Aju Ekspedisi" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
        <td><asp:Label runat="server" ID="Label4" Text="No. penugasan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_no_penugasan" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Nama ekspedisi" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300"><asp:Label runat="server" ID="lbl_nama_ekspedisi" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label8" Text="No. invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_no_invoice" Width="150" Font-Names="Tahoma" Font-Size="8"/></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Tgl. invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_invoice" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_invoice" />
        <ajax:CalendarExtender ID="ce_tgl_invoice" TargetControlID="tb_tgl_invoice" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td> 
        <td><asp:Label runat="server" ID="Label22" Text="Tgl. jatuh tempo invoice" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label23" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_tgl_jatuh_tempo" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_jatuh_tempo" />
        <ajax:CalendarExtender ID="ce_tgl_jatuh_tempo" TargetControlID="tb_tgl_jatuh_tempo" runat="server" Animated="true" Format="dd/MM/yyyy" />
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label16" Text="Status submit" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_status_submit" Font-Names="Tahoma" Font-Size="8"/></td> 
<%--        <td><asp:Label runat="server" ID="Label18" Text="Status bayar" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_status_bayar" Font-Names="Tahoma" Font-Size="8"/></td> --%>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label12" Text="Total nilai invoice" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td><asp:Label runat="server" ID="Label13" Text=":" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_total_nilai" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td> 
    </tr>
</table>    

<table align="center">
<tr>
        <td colspan="4"></td>
    </tr>
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Button runat="server" ID="btn_unsubmit" Text="Unsubmit" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td colspan="4"></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td width="200"><asp:Label runat="server" ID="Label10" Text="Item pembayaran" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td width="85"><asp:Label runat="server" ID="Label26" Text="Mata uang" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td width="75"><asp:Label runat="server" ID="Label27" Text="Nilai invoice" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td><asp:Label runat="server" ID="Label28" Text="Kurs" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td width="50"><asp:Label runat="server" ID="Label102" Text="Item HPP ?" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td width="100"><asp:Label runat="server" ID="Label1012" Text="Item PPN ?" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td><asp:TextBox runat="server" ID="tb_nama_item" Width="200" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true" /></td>
        <td><asp:TextBox runat="server" ID="tb_nilai_invoice" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_nilai_invoice" />
        </td>
        <td><asp:TextBox runat="server" ID="tb_kurs" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender3" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
                   <asp:Button ID="btn_kurs_idr" runat="server" Text="IDR" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button ID="btn_kurs_usd" runat="server" Text="USD" Font-Names="Tahoma" Font-Size="8" />
        </td>
        <td><asp:DropDownList runat="server" ID="dd_item_hpp" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
                <asp:ListItem Text="Bukan" Value="T"></asp:ListItem>                
            </asp:DropDownList>
        </td>
        <td><asp:DropDownList runat="server" ID="dd_item_ppn" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Bukan" Value="T"></asp:ListItem>                
                <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button runat="server" ID="btn_add" Text="Add" Font-Names="Tahoma" Font-Size="8" />
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
                            <asp:Label runat="server" ID="lb_item" Text="Item pembayaran" Width="250" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_item" Text='<%#Databinder.Eval(Container, "Dataitem.description") %>' Width="250" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_id_currency" Text="Mata uang" Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id_currency" Text='<%#Databinder.Eval(Container, "Dataitem.id_currency") %>' Visible="false"/>
                            <asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_invoice" Text="Nilai invoice" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_nilai_invoice" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_invoice"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender13" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_nilai_invoice" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kurs" Text="Kurs" Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_kurs" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.kurs"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender133" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_kurs" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_item_hpp" Text="Item HPP ?" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_item_hpp" Text='<%#Databinder.Eval(Container, "Dataitem.item_hpp") %>' visible="False" />
                            <asp:DropDownList runat="server" ID="dd_item_hpp" Font-Names="Tahoma" Font-Size="8">
                                 <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
                                 <asp:ListItem Text="Bukan" Value="T"></asp:ListItem>                
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_item_ppn" Text="Item PPN ?" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_item_ppn" Text='<%#Databinder.Eval(Container, "Dataitem.item_ppn") %>' visible="False" />
                            <asp:DropDownList runat="server" ID="dd_item_ppn" Font-Names="Tahoma" Font-Size="8">
                                 <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
                                 <asp:ListItem Text="Bukan" Value="T"></asp:ListItem>                
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jumlah" Text="Jumlah nilai" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nilai" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah"),2) %>' Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
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