<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_pembayaran_lc.ascx.vb" Inherits="Forms_Transaksi_Keuangan_detil_pembayaran_lc" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Detil Pembayaran L/C" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="Label13" Text="Periode transaksi" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label14" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_periode" Font-Names="Tahoma" Font-Size="8" /></td>        
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label15" Text="No. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label16" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_pembelian" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label17" Text="Tgl. pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label18" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_pembelian" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label25" Text="Nama supplier" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label28" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="6"><asp:Label runat="server" ID="lbl_nama_supplier" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Nama bank" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td colspan="6"><asp:Label runat="server" ID="lbl_nama_bank" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label1" Text="No. L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label2" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_no_lc" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label3" Text="Tgl. L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label4" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_tgl_lc" Width="65" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label23" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label24" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_mata_uang" Width="250" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label26" Text="Total nilai pembelian" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label27" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_total_nilai_pembelian" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>    
    <tr>
        <td><asp:Label runat="server" ID="Label32" Text="Berlaku sampai dengan tanggal" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label33" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_tgl_berlaku_lc" Width="65" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label34" Text="Jenis L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label35" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:DropDownList runat="server" ID="dd_jenis_lc" Font-Names="Tahoma" Font-Size="8" Enabled="false" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label36" Text="Tanggal jatuh tempo L/C" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label37" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td width="300"><asp:Label runat="server" ID="lbl_tgl_jatuh_tempo_lc" Width="65" Font-Names="Tahoma" Font-Size="8" /></td>        
        <td><asp:Label runat="server" ID="Label344" Text="Status lunas" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="Label355" Text=":" Font-Names="Tahoma" Font-Size="8"/></td>
        <td><asp:Label runat="server" ID="lbl_status_lunas" Font-Names="Tahoma" Font-Size="8" /></td> 
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label5" Text="Pembayaran L/C" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
        <td>&nbsp;</td>
        <td width="300">&nbsp;</td>        
    </tr>  
</table>

<%--Daniel--%>
<table width="100%">
<tr>
 <td align="right" width="50%"><asp:Label runat="server" ID="Label8" Text="Account:" Font-Names="Tahoma" Font-Size="8" Font-Bold="true"/></td>
          <td align="left" width="50%">
              <asp:DropDownList ID="DropDownListAccount" runat="server" Font-Names="Tahoma" Font-Size="8" AutoPostBack="true">
              <asp:ListItem Value="41">PEMBELIAN IMPORT</asp:ListItem>
              <asp:ListItem Value="11.08">UANG MUKA LC/PEMBEL. IMP/B. PEMBEL. IMP</asp:ListItem>
              </asp:DropDownList></td>
</tr>
</table>
<%--Daniel--%>

<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8pt"/></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:checkbox runat="server" ID="cb_data" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_pembayaran_ke" Text="Pembayaran ke" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.pembayaran_ke") %>' Visible="false"/> 
                            <asp:Label runat="server" ID="lbl_is_submit" Text='<%#Databinder.Eval(Container, "Dataitem.is_submit") %>' Visible="false"/> 
                            <asp:Label runat="server" ID="lbl_pembayaran_ke" Text='<%#Databinder.Eval(Container, "Dataitem.pembayaran_ke_nama") %>' Width="75" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jenis_bayar" Text="Jenis pembayaran" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jenis_bayar" Text='<%#Databinder.Eval(Container, "Dataitem.jenis_bayar") %>' visible="False"/> 
                            <asp:Label runat="server" ID="lbl_jenis_bayar_text" Text='<%#Databinder.Eval(Container, "Dataitem.jenis_bayar_text") %>' Font-Names="Tahoma" Font-Size="8"/> 
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kurs" Text="Kurs" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>                        
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_kurs" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.kurs"),2) %>' Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_prosentase" Text="Jml. % pembayaran" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_prosentase" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.prosentase"),2) %>' Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jumlah" Text="Jml. nilai pembayaran USD" Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jumlah" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah_nilai"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jumlah_idr" Text="Jml. nilai pembayaran IDR" Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jumlah_idr" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.jumlah_nilai_idr"),2) %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_bayar" Text="Tgl. bayar" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_bayar" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal_bayar") %>' Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_tgl_bayar" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_bank" Text="Kas/Bank" Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_bank" Text='<%#Databinder.Eval(Container, "Dataitem.id_bank") %>' Visible="false"/> 
                            <asp:DropDownList runat="server" ID="dd_bank" Font-Names="Tahoma" Font-Size="8pt"/>                            
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_no_giro" Text="No. Giro/Cek" Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_no_giro" Text='<%#Databinder.Eval(Container, "Dataitem.no_giro") %>' Width="100" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_giro" Text="Tgl. Giro/Cek" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_giro" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_giro") %>' Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_jth_tempo" Text="Tgl.Jth tempo Giro/Cek" Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_jth_tempo" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_jatuh_tempo") %>' Width="65" Font-Names="Tahoma" Font-Size="8pt"/> 
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="link_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8pt" CommandName="LinkSubmit"/> 
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8pt"/>
        </td>
    </tr>
</table>