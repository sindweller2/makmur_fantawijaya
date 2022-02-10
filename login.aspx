<%@ Page Language="VB" %>

<%@ Register Src="Forms/login/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc1" %>

<%@ Register Assembly="Microsoft.Web.DynamicDataControls" Namespace="Microsoft.Web.DynamicDataControls" TagPrefix="cc1" %>
    
<!DOCTYPE html> 

<script language="javascript" type="text/javascript">
    window.moveTo(0,0);    
    window.resizeTo(screen.width, screen.height);
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title>Integrated Accounting System</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <%--<div>
        <asp:Image runat="Server" ID="imlogo" ImageUrl="~/images/logo.jpg" />
    </div>--%>
    <div style="position: absolute; top: 100px; left: 80px; color: Silver;">
            <b style=" Font-size: 48px; color:#576d2c;">Integrated Accounting System</b><br />
            <b style=" Font-size: 36px; color:#576d2c;">PT. Makmur Fantawijaya Chemical Industries</b>
    </div>
    
    <div style="position: absolute; right: 80px; bottom: 80px;">
        <table>
           <tr>
             <td><asp:Label runat="server" ID="lbl_msg" Font-Names="tahoma" Font-Size="8pt" ForeColor="red" /></td>
           </tr>
        </table>
        <uc1:LoginControl id="LoginControl1" runat="server" />
    </div>
    </form>
</body>
</html>
