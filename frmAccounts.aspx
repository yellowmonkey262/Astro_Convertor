<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAccounts.aspx.cs" Inherits="frmAccounts" %>

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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlImports" runat="server">
                                <table>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label2" runat="server" Text="Bank Statements" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" DataSourceID="dsStatements"
                                                DataTextField="StatementNr" DataValueField="StatementNr" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="dsStatements" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                                                SelectCommand="SELECT DISTINCT [StatementNr] FROM [tblLedgerTransactions] ORDER BY [StatementNr]"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="dsTransactions">
                                                <Columns>
                                                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                                    <asp:BoundField DataField="StatementNr" HeaderText="StatementNr" SortExpression="StatementNr" />
                                                    <asp:BoundField DataField="Allocate" HeaderText="Allocate" SortExpression="Allocate" />
                                                </Columns>
                                                <HeaderStyle BackColor="#C2C5C9" Font-Bold="True" Font-Size="Medium" ForeColor="Black" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="dsTransactions" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                                                SelectCommand="SELECT Date, Description, Amount, StatementNr, Allocate FROM tblLedgerTransactions WHERE (StatementNr = @stmt) ORDER BY StatementNr">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="DropDownList2" DefaultValue="0" Name="stmt" PropertyName="SelectedValue" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlRental" runat="server">
                                <table>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label3" runat="server" Text="Rental Accounts" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                        </td>
                                        <td align="center">Date:
										<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" DataSourceID="dsRentDates"
                                            DataTextField="trnDate" DataValueField="trnDate">
                                        </asp:DropDownList>
                                            <asp:SqlDataSource ID="dsRentDates" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                                                SelectCommand="SELECT DISTINCT [trnDate] FROM [tblRentals] ORDER BY [trnDate]"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                                                DataKeyNames="id">
                                                <Columns>
                                                    <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" InsertVisible="False"
                                                        ReadOnly="True" />
                                                    <asp:BoundField DataField="trnDate" HeaderText="Date" SortExpression="trnDate" DataFormatString="{0:yyyy/MM/dd}" />
                                                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                                                    <asp:BoundField DataField="drValue" HeaderText="Dr Value" SortExpression="drValue" />
                                                    <asp:BoundField DataField="crValue" HeaderText="Cr Value" SortExpression="crValue" />
                                                </Columns>
                                                <HeaderStyle BackColor="#C2C5C9" Font-Bold="True" Font-Size="Medium" ForeColor="Black" />
                                            </asp:GridView>
                                            <br />
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                                                SelectCommand="SELECT id, trnDate, description, drValue, crValue FROM tblRentals WHERE (trnDate = @date) ORDER BY id">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="DropDownList3" DefaultValue="0" Name="date" PropertyName="SelectedValue" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlAllocated" runat="server">
                                <table>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label4" runat="server" Text="Allocated Statements" Font-Bold="True"
                                                Font-Size="Medium"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="grdAllocated" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2"
                                                DataKeyNames="id" OnRowUpdated="grdAllocated_RowUpdated">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                    <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="id" />
                                                    <asp:BoundField DataField="lid" HeaderText="Stmt ID" SortExpression="lid" />
                                                    <asp:TemplateField HeaderText="Date" SortExpression="trnDate">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("trnDate") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("trnDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" SortExpression="description">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value" SortExpression="amount">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Building" SortExpression="building">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("building") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("building") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" SortExpression="code">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="dsBuilding" DataTextField="Code"
                                                                DataValueField="Code" SelectedValue='<%# Bind("code") %>'>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reference" SortExpression="reference">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("reference") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("reference") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Trust Acc" SortExpression="accnumber">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("accnumber") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("accnumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank" SortExpression="contra">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("contra") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("contra") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Data Path" SortExpression="datapath">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("datapath") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("datapath") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                                                </Columns>
                                                <HeaderStyle BackColor="#C2C5C9" Font-Bold="True" Font-Size="Medium" ForeColor="Black" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                                                SelectCommand="SELECT [id], [lid], [trnDate], [amount], [building], [code], [description], [reference], [accnumber], [contra], [datapath] FROM [tblExport] ORDER BY [lid]"
                                                DeleteCommand="DELETE FROM [tblExport] WHERE [id] = @id" InsertCommand="INSERT INTO [tblExport] ([trnDate], [amount], [building], [code], [description], [reference], [accnumber], [contra], [datapath]) VALUES (@trnDate, @amount, @building, @code, @description, @reference, @accnumber, @contra, @datapath)"
                                                UpdateCommand="UPDATE [tblExport] SET [trnDate] = @trnDate, [amount] = @amount, [building] = @building, [code] = @code, [description] = @description, [reference] = @reference, [accnumber] = @accnumber, [contra] = @contra, [datapath] = @datapath WHERE [id] = @id"
                                                OnSelecting="SqlDataSource2_Selecting">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="id" Type="Int32" />
                                                </DeleteParameters>
                                                <InsertParameters>
                                                    <asp:Parameter Name="trnDate" Type="String" />
                                                    <asp:Parameter Name="amount" Type="Decimal" />
                                                    <asp:Parameter Name="building" Type="String" />
                                                    <asp:Parameter Name="code" Type="String" />
                                                    <asp:Parameter Name="description" Type="String" />
                                                    <asp:Parameter Name="reference" Type="String" />
                                                    <asp:Parameter Name="accnumber" Type="String" />
                                                    <asp:Parameter Name="contra" Type="String" />
                                                    <asp:Parameter Name="datapath" Type="String" />
                                                </InsertParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="trnDate" Type="String" />
                                                    <asp:Parameter Name="amount" Type="Decimal" />
                                                    <asp:Parameter Name="building" Type="String" />
                                                    <asp:Parameter Name="code" Type="String" />
                                                    <asp:Parameter Name="description" Type="String" />
                                                    <asp:Parameter Name="reference" Type="String" />
                                                    <asp:Parameter Name="accnumber" Type="String" />
                                                    <asp:Parameter Name="contra" Type="String" />
                                                    <asp:Parameter Name="datapath" Type="String" />
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
                    <span style="font-family: Garamond; font-size: small">COMPANY REGISTRATION NUMBER
				2004-003502/07 INSTITUTE OF REALTORS NUMBER 747 FIDELITY CERTIFICATE 2005102678/span>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>