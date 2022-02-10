<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_pembayaran_expedition_invoice.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_pembayaran_expedition_invoice" %>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Pembayaran Invoice Ekspedisi" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td width="300"><asp:Label runat="server" ID="lbl_tanggal" Width="65" Font-Names="Tahoma" Font-Size="8"/></td> 
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="lbl111" Text="No. aju ekspedisi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_aju" Font-Names="Tahoma" Font-Size="8" />
            <asp:TextBox runat="server" ID="tb_no_aju" Font-Names="Tahoma" Font-Size="8pt"/></td>
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
        <td width="300"><asp:Label runat="server" ID="lbl_no_invoice" Width="150" Font-Names="Tahoma" Font-Size="8"/></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label20" Text="Tgl. invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label21" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_tgl_invoice" Width="65" Font-Names="Tahoma" Font-Size="8"/></td> 
        <td><asp:Label runat="server" ID="Label22" Text="Tgl. jatuh tempo invoice" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label23" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="lbl_tgl_jatuh_tempo" Width="65" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>        
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td width="300">&nbsp;</td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label10" Text="Pembayaran invoice" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td>&nbsp;</td>
        <td width="300">&nbsp;</td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label11" Text="Tgl. pembayaran" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label26" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:TextBox runat="server" ID="tb_tgl_bayar" Width="65" Font-Names="Tahoma" Font-Size="8"/>
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_bayar" />
        </td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label124" Text="Total nilai invoice" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label134" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_total_nilai" Font-Names="Tahoma" Font-Size="8"/></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label16" Text="Status submit" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label17" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_status_submit" Font-Names="Tahoma" Font-Size="8"/></td> 
        <td><asp:Label runat="server" ID="Label18" Text="Status bayar" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_status_bayar" Font-Names="Tahoma" Font-Size="8"/></td> 
    </tr>
</table>    

<%--Daniel--%>
<table width="100%">
<tr>
 <td align="right" width="50%"><asp:Label runat="server" ID="Label45" Text="Account:" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
          <td align="left" width="50%">
              <asp:DropDownList ID="DropDownListAccount" runat="server" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
              <asp:ListItem Value="61.04">BIAYA PEMBELIAN IMPORT</asp:ListItem>
              <asp:ListItem>Hutang Lain - Lain / Ekspedisi</asp:ListItem>
              </asp:DropDownList></td>
</tr>
</table>
<%--Daniel--%>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8"/></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td width="300">&nbsp;</td> 
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
                            <asp:Label runat="server" ID="lb_item" Text="Item pembayaran" Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>                            
                            <asp:Label runat="server" ID="lbl_item" Text='<%#Databinder.Eval(Container, "Dataitem.description") %>' Width="200" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_id_currency" Text="Mata uang" Width="35" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id_currency" Text='<%#Databinder.Eval(Container, "Dataitem.id_currency") %>' Font-Names="Tahoma" Font-Size="8"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_invoice" Text="Nilai invoice" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nilai_invoice" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_invoice"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kurs" Text="Kurs" Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_kurs" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.kurs"),2) %>' Width="50" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_item_hpp" Text="Item HPP ?" Width="35" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_item_hpp" Text='<%#Databinder.Eval(Container, "Dataitem.item_hpp") %>' Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_item_ppn" Text="Item PPN ?" Width="35" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_item_ppn" Text='<%#Databinder.Eval(Container, "Dataitem.item_ppn") %>' Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jumlah" Text="Jumlah nilai" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_nilai" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_id_bank" Text="Kas/Bank" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_id_bank" Text='<%#Databinder.Eval(Container, "Dataitem.id_bank_account") %>' Visible="False"/>
                            <asp:DropDownList runat="server" ID="dd_bank_account" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />    
        </td>
    </tr>
</table>