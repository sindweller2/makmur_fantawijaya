<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginControl.ascx.cs" Inherits="Forms_login_LoginControl" %>

<script runat="server">

</script>
<asp:Login runat="server" ID="lgn" BackColor="#576d2c" BorderColor="Orange" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" >
    <LayoutTemplate>
        <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
            <tbody>
                <tr>
                    <td>
                        <table border="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td align="center" colspan="2"><b><asp:label SkinID="LBL" runat="server" ID="login" Text="Log in" ForeColor="white"/></b></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="UserNameLabel" SkinID="LBL" runat="server" AssociatedControlID="UserName" ForeColor="white">User Name:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" CssClass="datatextbox" />
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="ctl00$lgn">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="PasswordLabel" SkinID="LBL" runat="server" AssociatedControlID="Password" ForeColor="white">Password:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="datatextbox" />
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctl00$lgn">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td colspan="2">
                                        <asp:CheckBox ID="RememberMe" runat="server" CssClass="checkbox" Text="Remember me next time." /></td>--%>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color: red">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="ctl00$lgn" ForeColor="black" Font-Names="Verdana" Font-Size="0.8em" OnClick="LoginButton_Click"/>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </LayoutTemplate>
    <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
    <TextBoxStyle Font-Size="0.8em" />
    <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
        Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
</asp:Login>

