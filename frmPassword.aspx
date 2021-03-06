﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPassword.aspx.cs" Inherits="frmPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Astrodon</title>
    <link rel="Stylesheet" href="Styles/styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="50">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <img alt="" src="images/Header2.png" width="600px" height="100px" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <img alt="" src="images/Header.jpg" width="650px" height="50px" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="850px" align="center" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgHome" runat="server" ImageUrl="images/Home.jpg" OnClick="imgHome_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgLogout" runat="server" ImageUrl="images/Logout1.jpg" PostBackUrl="~/frmHome.aspx?lg=true"
                                            OnClick="imgLogout_Click" />
                                    </td>
                                    <td runat="server" id="rowImport">
                                        <asp:ImageButton ID="imgImport" runat="server" ImageUrl="images/Import1.jpg" OnClick="imgImport_Click" />
                                    </td>
                                    <td runat="server" id="rowReconcile">
                                        <asp:ImageButton ID="imgAllocate" runat="server" ImageUrl="images/Reconcile1.jpg"
                                            OnClick="imgAllocate_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgTrust" runat="server" ImageUrl="images/TrustAcc1.jpg" OnClick="imgTrust_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgAllocated" runat="server" ImageUrl="images/Allocated1.jpg"
                                            OnClick="imgAllocated_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgUnAllocated" runat="server" ImageUrl="images/UnAllocated1.jpg"
                                            OnClick="imgUnAllocated_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgBuildings" runat="server" ImageUrl="images/TrustCodes.jpg"
                                            OnClick="imgBuildings_Click" />
                                    </td>
                                    <td runat="server" id="rowExport">
                                        <asp:ImageButton ID="imgExport" runat="server" ImageUrl="images/Export.jpg" OnClick="imgExport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>Please enter password:
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:Button ID="btnContinue" runat="server" Text="Continue"
                        OnClick="btnContinue_Click" />
                </td>
            </tr>
    </form>
</body>
</html>