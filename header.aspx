<%@ Page Language="VB" AutoEventWireup="false" CodeFile="header.aspx.vb" Inherits="header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

    <title>Integrated Accounting System</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    
</head>
<body style="margin-top:5px; margin-bottom:0px; width:100%;">

    <form id="form1" runat="server">
        <div style="float: left; margin-top:0px;">
            <div style="position: absolute; top: 20px; color:Black">
                    <table>
                        <tr>
                            <td><asp:Image runat="server" ID="Image2" ImageUrl="~/images/logo.jpg" ImageAlign="Left" BorderWidth="0" Width="200" Height="50" /></td>
                            <td>&nbsp;</td>
                            <td valign="top">
                                <asp:Label runat="server" ID="Label2" Text="Integrated Accounting System" Font-Size="24px" Font-Bold="true" /><br />
                                <asp:Label runat="server" ID="Label3" Text="PT. Makmur Fantawijaya Chemical Industries" Font-Size="12px" Font-Bold="true"/>
                            </td>
                        </tr>
                    </table>
                </div> 
            
            <div style="margin-top:45px">
                <table align="right">
                    <tr>
                        <td align="right">
                            <asp:Label runat="server" ID="lbl_username" Font-Names="Tahoma" Font-Size="8pt" ForeColor="blue" />
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbl_msg" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>
           
        </div>
    </form>
</body>
</html>

