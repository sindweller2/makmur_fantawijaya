<%@ Page Language="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Public tradingClass As tradingClass = New tradingClass
    
    Private Function CreateJS() As String
        Dim js As String = ""
        js += "<script type=""text/javascript"" language=""javascript"">"
        js += "parent.parent.window.location = ""default.aspx"""
        js += "<" & "/" & "script" & ">"
        Return js
    End Function
    
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.tradingClass.SessionEnd()
        
        If User.Identity.IsAuthenticated = True Then
           
            
            common.Current.ClearAllSession()
            
            'Me.ltl_js.Text = Me.CreateJS
        End If
        FormsAuthentication.SignOut()
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <asp:Literal runat="server" ID="ltl_js" />
    <script language="javascript" type="text/jscript">
       parent.window.location = "default.aspx";
       //window.close();
    </script>
</body>
</html>
