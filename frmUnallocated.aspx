<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUnallocated.aspx.cs" Inherits="frmUnallocated"
    EnableEventValidation="false" %>

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
                                                <asp:SqlDataSource ID="dsBuilding" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                                                    SelectCommand="SELECT Code FROM tblBuildings ORDER BY Code"></asp:SqlDataSource>
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
                    <asp:UpdatePanel runat="server" ID="upd1">
                        <ContentTemplate>
                            <asp:Panel ID="pnlUnallocated" runat="server">
                                <table style="width: 286px">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Text="Unallocated" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource6"
                                                DataKeyNames="id" OnRowCommand="GridView1_RowCommand" EmptyDataText="No unallocated transactions">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Button" CommandName="Remove" Text="Delete" />
                                                    <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="id" />
                                                    <asp:BoundField DataField="lid" HeaderText="Statement ID" SortExpression="lid" />
                                                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="trnDate" DataFormatString="{0:yyyy/MM/dd}" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="description" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="amount">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                    <asp:TemplateField HeaderText="Building" SortExpression="allocatedCode">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbBuilding" runat="server" DataSourceID="dsBuilding" DataTextField="Code"
                                                                DataValueField="Code" SelectedValue='<%# Bind("Code") %>'>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reference" SortExpression="reference">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRef" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Period" SortExpression="period">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbPeriod" runat="server">
                                                                <asp:ListItem>0</asp:ListItem>
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
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:ButtonField ButtonType="Button" CommandName="allocate" Text="Allocate" />
                                                </Columns>
                                                <HeaderStyle BackColor="#C2C5C9" Font-Bold="True" Font-Size="Medium" ForeColor="Black" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label12" runat="server" Text="Unallocated Account" Font-Bold="True"
                                                Font-Size="Medium"></asp:Label>
                                            &nbsp;(<asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                            )
                                        <asp:Button ID="btnExportUnall" runat="server" OnClick="btnExportUnall_Click" Text="Export" />
                                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                                                Text="Add To Unallocated" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5"
                                                DataKeyNames="id" OnRowCommand="GridView5_RowCommand" OnRowDataBound="GridView5_RowDataBound"
                                                EmptyDataText="No unallocated transactions">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Button" CommandName="Remove" Text="Delete" />
                                                    <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="id" />
                                                    <asp:BoundField DataField="lid" HeaderText="Statement ID" SortExpression="lid" />
                                                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="trnDate" DataFormatString="{0:yyyy/MM/dd}" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="description" />
                                                    <asp:TemplateField HeaderText="Amount" SortExpression="amount">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Building" SortExpression="allocatedCode">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbBuilding" runat="server" DataSourceID="dsBuilding" DataTextField="Code"
                                                                DataValueField="Code" SelectedValue='<%# Bind("Code") %>'>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reference" SortExpression="reference">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRef" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Period" SortExpression="period">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbPeriod" runat="server">
                                                                <asp:ListItem>0</asp:ListItem>
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
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:ButtonField ButtonType="Button" CommandName="allocate" Text="Allocate" />
                                                </Columns>
                                                <HeaderStyle BackColor="#C2C5C9" Font-Bold="True" Font-Size="Medium" ForeColor="Black" />
                                            </asp:GridView>
                                            <br />
                                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="Medium" Text="Allocated"></asp:Label>
                                            &nbsp;<asp:Button ID="btnExportUnall0" runat="server" OnClick="btnExportUnall0_Click"
                                                Text="Export" />
                                            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                                DataSourceID="SqlDataSource1" OnRowCommand="GridView5_RowCommand" EmptyDataText="No allocated transactions">
                                                <Columns>
                                                    <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="id" />
                                                    <asp:BoundField DataField="trnDate" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Transaction Date"
                                                        SortExpression="trnDate" />
                                                    <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Building" HeaderText="Building" SortExpression="Building" />
                                                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description"></asp:BoundField>
                                                    <asp:BoundField DataField="reference" HeaderText="Reference" SortExpression="reference" />
                                                    <asp:BoundField DataField="period" HeaderText="Period" SortExpression="period" />
                                                    <asp:BoundField DataField="allocatedDate" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Allocated Date"
                                                        SortExpression="allocatedDate" />
                                                </Columns>
                                                <HeaderStyle BackColor="#C2C5C9" Font-Bold="True" Font-Size="Medium" ForeColor="Black" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnExportUnall" />
                            <asp:PostBackTrigger ControlID="btnExportUnall0" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                        SelectCommand="SELECT tblUnallocated.id, tblUnallocated.trnDate, tblUnallocated.amount, tblBuildings.Building, tblUnallocated.description,
											tblUnallocated.reference, tblUnallocated.period, tblUnallocated.allocatedDate FROM tblBuildings INNER JOIN tblUnallocated ON tblBuildings.Code =
											tblUnallocated.allocatedCode WHERE tblUnallocated.building = 'UNA' ORDER BY tblUnallocated.allocatedDate "></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                        SelectCommand="SELECT     tblUnallocated.id, tblLedgerTransactions.id AS lid,
CONVERT(datetime,tblLedgerTransactions.Date, 103) as Date, tblLedgerTransactions.Description, tblUnallocated.Amount,
                      convert(bit, tblLedgerTransactions.Allocate) as Allocate, 'UNA' as Code
FROM         tblLedgerTransactions LEFT OUTER JOIN
                      tblUnallocated ON tblLedgerTransactions.id = tblUnallocated.lid
WHERE     (convert(bit, tblLedgerTransactions.Allocate) = 1 AND tblUnallocated.allocatedCode = '')
ORDER BY CONVERT(datetime,tblLedgerTransactions.Date, 103) DESC"
                        InsertCommand="INSERT INTO [tblLedgerTransactions] ([Date], [Description], [Amount]) VALUES (@Date, @Description, @Amount)"
                        UpdateCommand="UPDATE tblUnallocated SET allocatedCode = @allocatedCode, reference = @reference, period = @period WHERE (lid = @lid)">
                        <InsertParameters>
                            <asp:Parameter Name="Date" Type="String" />
                            <asp:Parameter Name="Description" Type="String" />
                            <asp:Parameter Name="Amount" Type="Decimal" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="allocatedCode" />
                            <asp:Parameter Name="reference" />
                            <asp:Parameter Name="period" />
                            <asp:Parameter Name="lid" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                        InsertCommand="INSERT INTO [tblLedgerTransactions] ([Date], [Description], [Amount]) VALUES (@Date, @Description, @Amount)"
                        SelectCommand="SELECT     tblUnallocated.id, tblLedgerTransactions.id AS lid, CONVERT(datetime,tblLedgerTransactions.Date, 103) as Date,
                      tblLedgerTransactions.Description, tblLedgerTransactions.Amount, 'UNA' AS Code
FROM         tblLedgerTransactions LEFT OUTER JOIN
                      tblUnallocated ON tblLedgerTransactions.id = tblUnallocated.lid
WHERE     (CONVERT(bit, tblLedgerTransactions.Allocate) = 0)
ORDER BY CONVERT(datetime,tblLedgerTransactions.Date, 103) DESC"
                        UpdateCommand="UPDATE tblUnallocated SET allocatedCode = @allocatedCode, reference = @reference, period = @period WHERE (lid = @lid)">
                        <InsertParameters>
                            <asp:Parameter Name="Date" Type="String" />
                            <asp:Parameter Name="Description" Type="String" />
                            <asp:Parameter Name="Amount" Type="Decimal" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="allocatedCode" />
                            <asp:Parameter Name="reference" />
                            <asp:Parameter Name="period" />
                            <asp:Parameter Name="lid" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="center">
                    <span style="font-family: Garamond; font-size: small">COMPANY REGISTRATION NUMBER
                2004-003502/07 INSTITUTE OF REALTORS NUMBER 747 FIDELITY CERTIFICATE 20051026782678/span>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>