<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPrepareDB.aspx.cs" Inherits="frmPrepareDB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<body>
    <html>
    <head>
        <title>Astrodon</title>
        <link rel="Stylesheet" href="Styles/styles.css" />
        <script type="text/javascript" language="javascript" src="Scripts/JScript.js"></script>
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
                    <td align="center" height="40"><span style="font-family: Garamond; font-size: medium">Clear Database</span></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" height="200" valign="top">
                        <asp:Button ID="btnYes" runat="server" Text=" Yes " OnClick="btnYes_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNo" runat="server" Text="  No  " Style="height: 26px"
                        OnClick="btnNo_Click" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center"><span style="font-family: Garamond; font-size: small">COMPANY REGISTRATION NUMBER 2004-003502/07 INSTITUTE OF REALTORS NUMBER 747 FIDELITY CERTIFICATE 2005102678</span><%--<img alt="" src="images/banner_01.gif" width="640px" height="27px" />--%></td>
                </tr>
            </table>
        </form>
    </body>
    </html>
</body>