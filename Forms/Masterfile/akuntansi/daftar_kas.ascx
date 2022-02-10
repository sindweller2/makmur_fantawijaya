<%@ Control Language="VB" AutoEventWireup="false" CodeFile="daftar_kas.ascx.vb" Inherits="Forms_Masterfile_akuntansi_daftar_kas" %>

<script language="javascript" type="text/javascript">
    var disp_setting="toolbar=no,location=no,directories=no,menubar=no,"; 
        disp_setting+="scrollbars=yes,width=500, height=400, left=100, top=25"; 
    
    function popup_coa(tcid1, tcid2) { 
                window.open('popup_coa.aspx?tcid1=' + tcid1 + '&tcid2=' + tcid2,"",disp_setting); }
                
</script>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl11" Text="Daftar Kas" Font-Names="Tahoma" Font-Size="14" Font-Bold="true" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl_msg" Font-Names="Tahoma" Font-Size="8" ForeColor="red" /></td>
    </tr>
</table>

<table align="center">
    <tr>
        <td><asp:Label runat="server" ID="lbl100" Text="Nama kas" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label1" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:TextBox runat="server" ID="tb_name" Width="200" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label4" Text="Petty cash ?" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label5" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_petty_cash" Font-Names="Tahoma" Font-Size="8">
                <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
                <asp:ListItem Text="Bukan" Value="B"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label6" Text="Mata uang" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label7" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="Label2" Text="Nama akun" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Label runat="server" ID="Label3" Text=":" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:DropDownList runat="server" ID="dd_akun" Font-Names="Tahoma" Font-Size="8" /></td>
    </tr>
</table>


<table align="center">
    <tr>
        <td><asp:Button runat="server" ID="btn_save" Text="Save" Font-Names="Tahoma" Font-Size="8" /></td>
        <td><asp:Button runat="server" ID="btn_close" Text="Close" Font-Names="Tahoma" Font-Size="8" /></td>
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
                            <asp:Label runat="server" ID="lbl_id" Text='<%#Databinder.Eval(Container, "Dataitem.id") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_kas" Text="Nama kas" Width="150" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>                            
                            <asp:TextBox runat="server" ID="tb_nama_kas" Text='<%#Databinder.Eval(Container, "Dataitem.name") %>' Width="150" Font-Names="Tahoma" Font-Size="8" CommandName="LinkItem" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_petty_cash" Text="Petty cash ?" Width="75" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>                            
                            <asp:Label runat="server" ID="lbl_petty_cash" Text='<%#Databinder.Eval(Container, "Dataitem.is_petty_cash") %>' Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_petty_cash" Font-Names="Tahoma" Font-Size="8">
                                <asp:ListItem Text="Ya" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="Bukan" Value="B"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_mata_uang" Text="Mata uang" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>                            
                            <asp:Label runat="server" ID="lbl_mata_uang" Text='<%#Databinder.Eval(Container, "Dataitem.id_currency") %>' Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_mata_uang" Font-Names="Tahoma" Font-Size="8" /> 
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_saldo_akhir" Text="Saldo akhir" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>                            
                            <asp:TextBox runat="server" ID="tb_saldo_akhir" Text='<%#FormatNumber(Databinder.Eval(Container, "Dataitem.saldo_akhir"),2) %>' Font-Names="Tahoma" Font-Size="8" />
                            <ajax:FilteredTextBoxExtender runat="Server" ID="FilteredTextBoxExtender2" FilterType="custom, numbers" ValidChars="." TargetControlID="tb_saldo_akhir" />
                            
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lb_nama_akun" Text="Nama akun" Width="100" Font-Names="Tahoma" Font-Size="8" />
                        </HeaderTemplate>
                        <ItemTemplate>                            
                            <asp:Label runat="server" ID="lbl_nama_akun" Text='<%#Databinder.Eval(Container, "Dataitem.account_code") %>' Visible="false" />
                            <asp:DropDownList runat="server" ID="dd_akun" Font-Names="Tahoma" Font-Size="8" /> 
                        </ItemTemplate>
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