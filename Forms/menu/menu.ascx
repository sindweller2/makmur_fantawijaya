<%@ Control Language="VB" AutoEventWireup="false" CodeFile="menu.ascx.vb" Inherits="form_Menu_menu" %>

<table align="center">
    <tr>
        <td style="width:200; height: 167px;" valign="top">
             <asp:TreeView ID="TreeView1" ExpandDepth="0" PopulateNodesFromClient="true" ShowLines="true" 
                ShowExpandCollapse="true" runat="server" Width="200" 
                Font-Names="Tahoma" Font-Size="8pt"/>
     </td>
    </tr>
</table>