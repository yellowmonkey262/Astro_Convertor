<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Astrodon</title>
    <link rel="Stylesheet" href="Styles/styles.css" />
</head>
<body>
    <form id="frmLogin" runat="server">
        <table width="100%" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="50">&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <img alt="" src="images/Header2.png" width="600px" height="100px" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <img alt="" src="images/Header.jpg" width="650px" height="50px" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <table width="50%" align="center" cellpadding="0" cellspacing="0">
                        <%--<tr>
                            <td colspan="3" class="headers" align="left"><span style="font-family:Garamond"> Login : </span></td>
                        </tr>--%>
                        <tr>
                            <td colspan="3" height="30" align="center">
                                <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Names="Garamond" Font-Size="Small" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="48%" align="right" height="30">Username : </td>
                            <td width="4%">&nbsp;</td>
                            <td width="48%" align="left">
                                <asp:TextBox ID="txtUname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td width="48%" align="right" height="30">Password : </td>
                            <td width="4%">&nbsp;</td>
                            <td width="48%" align="left">
                                <asp:TextBox ID="txtPword" runat="server" TextMode="Password"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" height="50">
                                <asp:Button ID="btnLogin" runat="server"
                                    Text=" Login " OnClick="btnLogin_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="150">&nbsp;</td>
            </tr>
            <tr>
                <td align="center"><span style="font-family: Garamond; font-size: small">COMPANY REGISTRATION NUMBER 2004-003502/07 INSTITUTE OF REALTORS NUMBER 747 FIDELITY CERTIFICATE 2005102678</span><%--<img alt="" src="images/banner_01.gif" width="640px" height="27px" />--%></td>
            </tr>
        </table>
    </form>
</body>
</html>