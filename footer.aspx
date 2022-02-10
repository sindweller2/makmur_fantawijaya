<%@ Page Language="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            Me.SetTime()
        End If
    End Sub
    
    Private Sub SetTime()
        Dim dt As Date = Now
        Dim reader As Data.SqlClient.SqlDataReader
        reader = connection.koneksi.SelectRecord("select convert(char, getdate(), 103) as tgl")
        reader.Read()
        If reader.HasRows Then
            Me.lbl_time.Text = reader.Item("tgl")
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
   
</head>

<body style="margin: 0px 0px 0px 0px; background-color:#99cc00;">
    <form id="form1" runat="server">
    <div style="margin-left:10px; margin-right:10px;">
        <table width="100%">
            <tr>
                <td>
                    <asp:Label runat="server" ID="Label1" Text="&copy; 2011 Makmur Fantawijaya Chemical Industries" Font-Names="Futura Lt BT" Font-Size="8pt" ForeColor="black" />
                </td>
                 <td align="right">
                    <asp:Label runat="server" ID="lbl_update" Text="Date : " Font-Size="8pt" Font-Names="Futura Lt BT" ForeColor="black"/>
                    <asp:Label EnableViewState="false" runat="server" ID="lbl_time" Font-Size="8pt" Font-Names="Futura Lt BT" ForeColor="black"/>
                </td>
           </tr>
        </table>
     </div>   
    </form>
</body>
</html>