<%@ Control Language="VB" AutoEventWireup="false" CodeFile="detil_penerimaan_pembayaran_penjualan.ascx.vb"
    Inherits="Forms_Transaksi_Keuangan_detil_penerimaan_pembayaran_penjualan" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<table align="center">
    <tr>
        <td>
            <asp:Label runat="server" ID="lbl11" Text="Detil Penerimaan Pembayaran Penjualan Barang"
                Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
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
        <td>
            <asp:Label runat="server" ID="lbl2323" Text="No. Sales order" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300">
            <asp:Label runat="server" ID="lbl_no_so" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label7" Text="Tgl. Sales order" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label8" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="150">
            <asp:Label runat="server" ID="lbl_tgl_so" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label13" Text="Jenis penjualan" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label14" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="lbl_jenis_penjualan" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label2" Text="Nama customer" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300">
            <asp:Label runat="server" ID="lbl_nama_customer" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label10" Text="Lama pembayaran" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label11" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="150">
            <asp:Label runat="server" ID="lbl_lama_pembayaran" Font-Names="Tahoma" Font-Size="8" />
            <asp:Label runat="server" ID="Label12" Text="hari" Font-Names="Tahoma" Font-Size="8" />
        </td>
        <td>
            <asp:Label runat="server" ID="Label47" Text="PPN" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label48" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="150">
            <asp:Label runat="server" ID="lbl_ppn" Font-Names="Tahoma" Font-Size="8" />
            <asp:Label runat="server" ID="Label49" Text="(%)" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label4" Text="Tgl. invoice" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="300">
            <asp:Label runat="server" ID="lbl_tgl_invoice" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label6" Text="Tgl. jatuh tempo" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label9" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="150">
            <asp:Label runat="server" ID="lbl_tgl_jatuh_tempo" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label38" Text="Mata uang" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label39" Text=":" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td width="300">
            <asp:Label runat="server" ID="lbl_mata_uang" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label35" Text="Total nilai invoice" Font-Names="Tahoma"
                Font-Size="8" Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label36" Text=":" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td width="150">
            <asp:Label runat="server" ID="lbl_total_nilai" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label45" Text="Kurs" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label46" Text=":" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td width="150">
            <asp:Label runat="server" ID="lbl_kurs" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label171" Text="Nilai retur (IDR)" Font-Names="Tahoma"
                Font-Size="8" Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label201" Text=":" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="lbl_nilai_retur_idr" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label1714" Text="Nilai retur (USD)" Font-Names="Tahoma"
                Font-Size="8" Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label2014" Text=":" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="lbl_nilai_retur_usd" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label18" Text="Sisa nilai invoice (IDR)" Font-Names="Tahoma"
                Font-Size="8" Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label19" Text=":" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td width="300">
            <asp:Label runat="server" ID="lbl_sisa_nilai_invoice" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label17" Text="Status" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="Label20" Text=":" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
        <td>
            <asp:Label runat="server" ID="lbl_status" Font-Names="Tahoma" Font-Size="8" Font-Bold="true" /></td>
    </tr>
</table>
<table align="center" width="750">
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label21" Text="Pembayaran" Font-Names="Tahoma" Font-Size="8"
                Font-Bold="true" /></td>
    </tr>
    <tr>
        <td width="150">
            <asp:Label runat="server" ID="lbl304" Text="Periode pembayaran" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label22" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td colspan="5">
            <asp:TextBox runat="server" ID="tb_tahun_pembayaran" Width="50" Font-Names="Tahoma"
                Font-Size="8" />
            <ajax:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="9999" MaskType="Number" TargetControlID="tb_tahun_pembayaran" />
            <asp:Button runat="server" ID="btn_refresh_bulan" Text="Refresh bulan" Font-Names="Tahoma"
                Font-Size="8" />
                <asp:DropDownList runat="server" ID="dd_periode_pembayaran" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
    <tr>
        <td width="150">
            <asp:Label runat="server" ID="Label23" Text="Tanggal pembayaran" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label24" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tgl_bayar" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99/99/9999"
                MaskType="Date" TargetControlID="tb_tgl_bayar" />
                <act:CalendarExtender ID="ce_tgl_bayar" TargetControlID="tb_tgl_bayar" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td width="150" style="height: 43px">
            <asp:Label runat="server" ID="Label25" Text="Jenis pembayaran" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td style="height: 43px">
            <asp:Label runat="server" ID="Label26" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td style="height: 43px">
            <asp:DropDownList runat="server" ID="dd_jenis_pembayaran" Font-Names="Tahoma" Font-Size="8"
                AutoPostBack="true" /></td>
        <td colspan="6" style="height: 43px">
            <table runat="server" id="tbl_deposit">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label50" Text="Jumlah deposit" Font-Names="Tahoma"
                            Font-Size="8" /></td>
                    <td>
                        <asp:Label runat="server" ID="Label51" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
                    <td>
                        <asp:Label runat="server" ID="Label52" Text="IDR " Font-Names="Tahoma" Font-Size="8" /></td>
                    <td>
                        <asp:Label runat="server" ID="lbl_jumlah_deposit_idr" Font-Names="Tahoma" Font-Size="8" /></td>
                    <td>
                        <asp:Label runat="server" ID="Label53" Text="USD " Font-Names="Tahoma" Font-Size="8" /></td>
                    <td>
                        <asp:Label runat="server" ID="lbl_jumlah_deposit_usd" Font-Names="Tahoma" Font-Size="8" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="150">
            <asp:Label runat="server" ID="Label27" Text="Rekening bank/kas" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label28" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_bank_account" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td width="150">
            <asp:Label runat="server" ID="Label29" Text="No. Cek/Giro" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label30" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_no_cek_giro" Width="100" Font-Names="Tahoma" Font-Size="8" /></td>
        <td width="100">
            <asp:Label runat="server" ID="Label31" Text="Tgl. Cek/Giro" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label32" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tgl_cek_giro" Width="65" Font-Names="Tahoma" Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99/99/9999"
                MaskType="Date" TargetControlID="tb_tgl_cek_giro" />
                <act:CalendarExtender ID="ce_tgl_cek_giro" TargetControlID="tb_tgl_cek_giro" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
        <td width="150">
            <asp:Label runat="server" ID="Label33" Text="Tgl. jatuh tempo Cek/Giro" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label34" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_tgl_jatuh_tempo_cek_giro" Width="65" Font-Names="Tahoma"
                Font-Size="8" />
            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="99/99/9999"
                MaskType="Date" TargetControlID="tb_tgl_jatuh_tempo_cek_giro" />
                <act:CalendarExtender ID="ce_tgl_jatuh_tempo_cek_giro" TargetControlID="tb_tgl_jatuh_tempo_cek_giro" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td width="150">
            <asp:Label runat="server" ID="Label43" Text="Mata uang" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label44" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td width="150">
            <asp:Label runat="server" ID="Label15" Text="Nilai pembayaran" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label16" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_nilai_pembayaran" Width="100" Font-Names="Tahoma"
                Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender12" FilterType="custom, numbers"
                ValidChars="." TargetControlID="tb_nilai_pembayaran" />
        </td>
        <td width="100">
            <asp:Label runat="server" ID="Label37" Text="Nilai potongan" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label40" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_potongan" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1" FilterType="custom, numbers"
                ValidChars="." TargetControlID="tb_potongan" />
                
        </td>
        <td width="150">
            <asp:Label runat="server" ID="Label41" Text="Nilai kelebihan" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label42" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_kelebihan" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers"
                ValidChars="." TargetControlID="tb_kelebihan" />
        </td>
    </tr>
    <tr>
        <td width="150">
            <asp:Label runat="server" ID="Label435" Text="Biaya Bank" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label448" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_biaya_bank" Width="100" Font-Names="Tahoma" Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender1234" FilterType="custom, numbers"
                ValidChars="." TargetControlID="tb_biaya_bank" />
        </td>
    </tr>
    <%--Daniel--%>
    <tr>
        <td width="150">
            <asp:Label runat="server" ID="Label56" Text="Biaya pengiriman barang" Font-Names="Tahoma"
                Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label57" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_biaya_pengiriman_barang" Width="100" Font-Names="Tahoma"
                Font-Size="8" />
            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender3" FilterType="custom, numbers"
                ValidChars="." TargetControlID="tb_biaya_pengiriman_barang" />
        </td>
    </tr>
    <%--Daniel--%>
    <tr>
        <td width="150">
            <asp:Label runat="server" ID="Label54" Text="Keterangan" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Label runat="server" ID="Label55" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:TextBox runat="server" ID="tb_keterangan" Width="350" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td>
            <asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>
<table align="center">
    <tr>
        <td>
            <asp:DataGrid SkinID="DGAPP" runat="server" ID="dg_data" AutoGenerateColumns="False"
                HeaderStyle-HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cb_data" />
                            <asp:Label runat="server" ID="lbl_seq" Text='<%#Databinder.Eval(Container, "Dataitem.seq") %>'
                                Visible="false" />
                            <asp:Label runat="server" ID="lbl_is_submit" Text='<%#Databinder.Eval(Container, "Dataitem.is_submit") %>'
                                Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_periode" Text="Periode pembayaran" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_periode" Text='<%#Databinder.Eval(Container, "Dataitem.id_periode_pembayaran") %>'
                                Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_periode" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tanggal" Text="Tanggal pembayaran" Width="65" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tanggal" Text='<%#Databinder.Eval(Container, "Dataitem.tanggal") %>'
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender33" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="tb_tanggal" />
                                <act:CalendarExtender ID="ce_tanggal" TargetControlID="tb_tanggal" runat="server" Format="dd/MM/yyyy" ></act:CalendarExtender>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_jenis" Text="Jenis pembayaran" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_jenis" Text='<%#Databinder.Eval(Container, "Dataitem.id_jenis_pembayaran") %>'
                                Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_jenis_pembayaran" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_bank" Text="Nama bank" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_bank" Text='<%#Databinder.Eval(Container, "Dataitem.id_bank_account") %>'
                                Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_bank_account" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_cek_giro" Text="No. Cek/Giro" Width="100" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_no_cek_giro" Width="100" Text='<%#Databinder.Eval(Container, "Dataitem.no_cek_giro") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_cek_giro" Text="Tgl. Cek/Giro" Width="65" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_no_cek_giro" Width="65" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_cek_giro") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender333" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="tb_tgl_no_cek_giro" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_tgl_jatuh_tempo_cek_giro" Text="Tgl. jatuh tempo Cek/Giro"
                                Width="65" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_tgl_jatuh_tempo_cek_giro" Width="65" Text='<%#Databinder.Eval(Container, "Dataitem.tgl_jatuh_tempo_cek_giro") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                            <ajax:MaskedEditExtender runat="server" ID="MaskedEditExtender3333" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="tb_tgl_jatuh_tempo_cek_giro" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Mata uang" Width="50" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_mata_uang" Text='<%#Databinder.Eval(Container, "Dataitem.id_currency") %>'
                                Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nilai_pembayaran" Text="Nilai pembayaran" Width="75"
                                Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_nilai_pembayaran" Width="75" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.nilai_pembayaran"),3) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender23" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_nilai_pembayaran" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_potongan" Text="Potongan" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_potongan" Width="75" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.potongan"),3) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender233" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_potongan" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_kelebihan" Text="Kelebihan" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_kelebihan" Width="75" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.kelebihan"),3) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2343" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_kelebihan" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_biaya_bank" Text="Biaya Bank" Width="75" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_biaya_bank" Width="75" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.biaya_bank"),3) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender23243" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_biaya_bank" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_biaya_pengiriman_barang" Text="Biaya pengiriman barang"
                                Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_biaya_pengiriman_barang" Width="75" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.biaya_pengiriman_barang"),3) %>'
                                Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender233245345" FilterType="custom, numbers"
                                ValidChars="." TargetControlID="tb_biaya_pengiriman_barang" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_keterangan" Text="Keterangan" Width="150" Font-Names="Tahoma"
                                Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="tb_keterangan" Width="150" Text='<%#Databinder.Eval(Container, "Dataitem.keterangan") %>'
                                Font-Names="Tahoma" Font-Size="8" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button runat="server" ID="btn_submit" Text="Submit" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_unsubmit" Text="Unsubmit" Font-Names="Tahoma" Font-Size="8pt"/>
            <asp:Button runat="server" ID="btn_update" Text="Update" Font-Names="Tahoma" Font-Size="8" />
            <asp:Button runat="server" ID="btn_delete" Text="Delete" Font-Names="Tahoma" Font-Size="8" />
        </td>
    </tr>
</table>
