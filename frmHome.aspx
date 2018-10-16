<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmHome.aspx.cs" Inherits="frmHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Astrodon</title>
    <link rel="Stylesheet" href="Styles/styles.css" />
    <script type="text/javascript" language="javascript">
		function listbox_click() {
			document.getElementById("<%=btnImportFile.ClientID %>").click();
		}
    </script>
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
                <td>
                    <table width="90%" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top" width="75%">
                                <table width="100%" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="center">
                                            <div id="divContent">
                                                <asp:Label ID="Label1" runat="server" Text="Pastel Integrator Status: "></asp:Label>
                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlImportFiles" runat="server">
                                <table width="100%" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <input id="Button1" type="button" value="Upload new docs" onclick="window.open('upload.aspx', 'location=2, width=350, height=200');" />
                                            <br />
                                            &nbsp;<asp:ListBox ID="ListBox1" runat="server" Width="500px" Rows="15" Font-Names="Garamond"
                                                Font-Size="medium" ForeColor="Black" BackColor="#CCCCCC" Style="border: medium inset #FFFFFF"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblImportStatus" runat="server" Text=""></asp:Label>
                                            <asp:Button ID="btnImportFile" runat="server" Text="Button" OnClick="btnImportFile_Click"
                                                Style="visibility: hidden" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlAllocate" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Trust Period"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbTrustPeriod" runat="server">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAllocate" runat="server" Text="Allocate" OnClick="btnAllocate_Click" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblAllocateStatus" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="Label3" runat="server" Text="Allocation References"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="dsMatch">
                                                <Columns>
                                                    <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                                    <asp:BoundField DataField="statementRef" HeaderText="Statement Reference" SortExpression="statementRef" />
                                                    <asp:BoundField DataField="astroRef" HeaderText="Allocation Reference" SortExpression="astroRef" />
                                                    <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                                                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="dsMatch" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>" DeleteCommand="DELETE FROM [tblMatch] WHERE [id] = @id" InsertCommand="INSERT INTO [tblMatch] ([statementRef], [astroRef]) VALUES (@statementRef, @astroRef)" SelectCommand="SELECT [id], [statementRef], [astroRef] FROM [tblMatch] ORDER BY [statementRef], [astroRef]" UpdateCommand="UPDATE [tblMatch] SET [statementRef] = @statementRef, [astroRef] = @astroRef WHERE [id] = @id">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="id" Type="Int32" />
                                                </DeleteParameters>
                                                <InsertParameters>
                                                    <asp:Parameter Name="statementRef" Type="String" />
                                                    <asp:Parameter Name="astroRef" Type="String" />
                                                </InsertParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="statementRef" Type="String" />
                                                    <asp:Parameter Name="astroRef" Type="String" />
                                                    <asp:Parameter Name="id" Type="Int32" />
                                                </UpdateParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span style="font-family: Garamond; font-size: small">COMPANY REGISTRATION NUMBER 2004-003502/07
					INSTITUTE OF REALTORS NUMBER 747 FIDELITY CERTIFICATE 2005102678</span><%--<img alt="" src="images/banner_01.gif" width="640px" height="27px" />--%>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>