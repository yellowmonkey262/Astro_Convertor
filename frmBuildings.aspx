<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmBuildings.aspx.cs" Inherits="frmBuildings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Astrodon</title>
    <link rel="Stylesheet" href="Styles/styles.css" />
    <style type="text/css">
        .style1 {
            width: 100%;
        }
    </style>
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
                                        <asp:ImageButton ID="imgLogout" runat="server" ImageUrl="images/Logout1.jpg" PostBackUrl="~/frmHome.aspx?lg=true" />
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
                <td align="center">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnTrustCodes" runat="server" Text="Trust Codes" OnClick="btnTrustCodes_Click" />
                            <asp:Button ID="btnRentalCodes" runat="server" Text="Rental Codes" OnClick="btnRentalCodes_Click" />
                            <asp:Button ID="btnBankFees" runat="server" Text="Bank Fees" OnClick="btnBankFees_Click" />
                            <asp:Button ID="btnJournal" runat="server" OnClick="btnJournal_Click" Text="Monthly Service Fees" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlBuildingCodes" runat="server">
                                <table width="100%" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label2" runat="server" Text="Trust Codes" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                                DataSourceID="dsBuildings" OnRowCommand="GridView1_RowCommand" ShowFooter="True">
                                                <Columns>
                                                    <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="id" />
                                                    <asp:TemplateField HeaderText="Building" SortExpression="Building">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Building") %>' Width="150px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewBuild" runat="server" Text='' Width="150px"></asp:TextBox>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Building") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" SortExpression="Code">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Code") %>' Width="50px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewCode" runat="server" Text='' Width="50px"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Trust Account" SortExpression="AccNumber">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("AccNumber") %>' Width="75px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("AccNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewAcc" runat="server" Text='' Width="75px"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Account" SortExpression="Contra">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Contra") %>' Width="75px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Contra") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewBank" runat="server" Text='' Width="75px"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Period" SortExpression="Period">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Period") %>' Width="25px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Period") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewPeriod" runat="server" Text='' Width="25px"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pmt Type" SortExpression="payments">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("payments") %>' Width="25px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewPayment" runat="server" Text='' Width="25px"></asp:TextBox>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("payments") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rec Type" SortExpression="receipts">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("receipts") %>' Width="25px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("receipts") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewReceipt" runat="server" Text='' Width="25px"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jnl Type" SortExpression="journals">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("journals") %>' Width="25px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("journals") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Data Path" SortExpression="DataPath">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("DataPath") %>' Width="100px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("DataPath") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewPath" runat="server" Text='' Width="100px"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PM" SortExpression="pm">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("pm") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewPM" runat="server" Text=''></asp:TextBox>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("pm") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Name" SortExpression="bankName">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("bankName") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("bankName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewBankName" runat="server" Text=''></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Account Name" SortExpression="accName">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("accName") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("accName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewAccName" runat="server" Text=''></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Account Number" SortExpression="bankAccNumber">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("bankAccNumber") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("bankAccNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewAccNumber" runat="server" Text=''></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch Code" SortExpression="branch">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("branch") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label14" runat="server" Text='<%# Bind("branch") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNewBranch" runat="server" Text=''></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <EditItemTemplate>
                                                            <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update"
                                                                Text="Update" />
                                                            &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                Text="Cancel" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit"
                                                                Text="Edit" />
                                                            &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Delete"
                                                                Text="Delete" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Button ID="insertBuild" runat="server" CausesValidation="False" CommandName="InsertNew"
                                                                Text="Insert" />
                                                            &nbsp;<asp:Button ID="cancelBuild" runat="server" CausesValidation="False" CommandName="CancelNew"
                                                                Text="Cancel" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    Building Name:<asp:TextBox ID="noBuild" runat="server" Width="300px"></asp:TextBox>
                                                    Building Code:<asp:TextBox ID="noCode" runat="server" Width="75px"></asp:TextBox>
                                                    <asp:Button ID="noInsert" runat="server" CausesValidation="False" CommandName="NoInsert"
                                                        Text="Insert" />
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#C1C1C1" Font-Bold="True" Font-Size="Medium" ForeColor="Black" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="dsBuildings" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                                                DeleteCommand="DELETE FROM [tblBuildings] WHERE [id] = @id" InsertCommand="INSERT INTO [tblBuildings] ([Building], [Code], [AccNumber], [Contra], [Period], [DataPath], [payments], [receipts], pm, bankName, accName, bankAccNumber, branch) VALUES (@Building, @Code, @AccNumber, @Contra, @Period, @DataPath, @payments, @receipts, @pm, @bankName, @accName, @bankAccNumber, @branch)"
                                                SelectCommand="SELECT id, Building, Code, AccNumber, Contra, Period, DataPath, payments, receipts, journals, pm, bankName, accName, bankAccNumber, branch FROM tblBuildings ORDER BY Building"
                                                UpdateCommand="UPDATE [tblBuildings] SET [Building] = @Building, [Code] = @Code, [AccNumber] = @AccNumber, [Contra] = @Contra, [Period] = @Period, [DataPath] = @DataPath, [payments] = @payments, [receipts] = @receipts, [journals] = @journals,pm = @pm, bankName = @bankName, accName = @accName, bankAccNumber = @bankAccNumber, branch = @branch WHERE [id] = @id"
                                                OnInserting="dsBuildings_Inserting">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="id" Type="Int32" />
                                                </DeleteParameters>
                                                <InsertParameters>
                                                    <asp:Parameter Name="Building" Type="String" />
                                                    <asp:Parameter Name="Code" Type="String" />
                                                    <asp:Parameter Name="AccNumber" Type="String" />
                                                    <asp:Parameter Name="Contra" Type="String" />
                                                    <asp:Parameter Name="Period" Type="String" />
                                                    <asp:Parameter Name="DataPath" Type="String" />
                                                    <asp:Parameter Name="payments" Type="Int32" />
                                                    <asp:Parameter Name="receipts" Type="Int32" />
                                                    <asp:Parameter Name="pm" Type="String" />
                                                    <asp:Parameter Name="bankName" Type="String" />
                                                    <asp:Parameter Name="accName" Type="String" />
                                                    <asp:Parameter Name="bankAccNumber" Type="String" />
                                                    <asp:Parameter Name="branch" Type="String" />
                                                </InsertParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="Building" Type="String" />
                                                    <asp:Parameter Name="Code" Type="String" />
                                                    <asp:Parameter Name="AccNumber" Type="String" />
                                                    <asp:Parameter Name="Contra" Type="String" />
                                                    <asp:Parameter Name="Period" Type="String" />
                                                    <asp:Parameter Name="DataPath" Type="String" />
                                                    <asp:Parameter Name="payments" Type="Int32" />
                                                    <asp:Parameter Name="receipts" Type="Int32" />
                                                    <asp:Parameter Name="journals" Type="Int32" />
                                                    <asp:Parameter Name="pm" Type="String" />
                                                    <asp:Parameter Name="bankName" Type="String" />
                                                    <asp:Parameter Name="accName" Type="String" />
                                                    <asp:Parameter Name="bankAccNumber" Type="String" />
                                                    <asp:Parameter Name="branch" Type="String" />
                                                    <asp:Parameter Name="id" Type="Int32" />
                                                </UpdateParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlRentals" runat="server">
                                <table width="100%" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label8" runat="server" Text="Rental Accounts" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                                DataSourceID="SqlDataSource1" ShowFooter="True">
                                                <Columns>
                                                    <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="id" />
                                                    <asp:TemplateField HeaderText="Description" SortExpression="description">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("description") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtDescrip" runat="server" Text=''></asp:TextBox>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Account" SortExpression="crAccount">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("crAccount") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("crAccount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtAcc" runat="server" Text=''></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contra" SortExpression="crContra">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("crContra") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("crContra") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtContr" runat="server" Text=''></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <EditItemTemplate>
                                                            <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update"
                                                                Text="Update" />
                                                            &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                Text="Cancel" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit"
                                                                Text="Edit" />
                                                            &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Delete"
                                                                Text="Delete" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Button ID="insertRent" runat="server" CausesValidation="False" CommandName="InsertRent"
                                                                Text="Insert" OnClick="insertRent_Click" />
                                                            &nbsp;<asp:Button ID="cancelRent" runat="server" CausesValidation="False" CommandName="CancelRent"
                                                                Text="Cancel" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#C1C1C1" Font-Bold="True" Font-Size="Medium" ForeColor="Black" />
                                            </asp:GridView>
                                            <br />
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                                                SelectCommand="SELECT [id], [description], [crAccount], [crContra] FROM [tblRentalAccounts] ORDER BY [description]"
                                                DeleteCommand="DELETE FROM [tblRentalAccounts] WHERE [id] = @id" InsertCommand="INSERT INTO [tblRentalAccounts] ([description], [drAccount], [drContra], [crAccount], [crContra]) VALUES (@description,@crAccount, @crContra, @crAccount, @crContra)"
                                                UpdateCommand="UPDATE [tblRentalAccounts] SET [description] = @description, [crAccount] = @crAccount, [crContra] = @crContra WHERE [id] = @id"
                                                OnInserting="SqlDataSource1_Inserting">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="id" Type="Int32" />
                                                </DeleteParameters>
                                                <InsertParameters>
                                                </InsertParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="description" Type="String" />
                                                    <asp:Parameter Name="crAccount" Type="String" />
                                                    <asp:Parameter Name="crContra" Type="String" />
                                                    <asp:Parameter Name="id" Type="Int32" />
                                                </UpdateParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlBankCharges" runat="server">
                                <table width="100%" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label7" runat="server" Text="Bank Charges" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="BankChargesId"
                                                DataSourceID="dsBank" OnRowUpdated="GridView2_RowUpdated">
                                                <Columns>
                                                    <asp:BoundField DataField="BankChargesId" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="BankChargesId" />
                                                    <asp:BoundField DataField="CashDeposit" HeaderText="Service Fees" SortExpression="CashDeposit" />
                                                    <asp:BoundField DataField="DebitOrder" HeaderText="EFT" SortExpression="DebitOrder" />
                                                    <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                                                </Columns>
                                                <HeaderStyle BackColor="#C1C1C1" Font-Bold="True" Font-Size="Medium" ForeColor="Black" />
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                                DataSourceID="dsCashDep" OnRowCommand="GridView5_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="id" />
                                                    <asp:BoundField DataField="min" HeaderText="Min" SortExpression="min" />
                                                    <asp:BoundField DataField="max" HeaderText="Max" SortExpression="max" />
                                                    <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                                                    <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                                                </Columns>
                                                <HeaderStyle BackColor="#C1C1C1" Font-Bold="True" Font-Size="Medium" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="dsCashDep" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                                                SelectCommand="SELECT [id], [min], [max], [amount] FROM [tblCashDeposits] ORDER BY [min]"
                                                DeleteCommand="DELETE FROM [tblCashDeposits] WHERE [id] = @id" InsertCommand="INSERT INTO [tblCashDeposits] ([min], [max], [amount]) VALUES (@min, @max, @amount)"
                                                UpdateCommand="UPDATE [tblCashDeposits] SET [min] = @min, [max] = @max, [amount] = @amount WHERE [id] = @id">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="id" Type="Int32" />
                                                </DeleteParameters>
                                                <InsertParameters>
                                                    <asp:Parameter Name="min" Type="Decimal" />
                                                    <asp:Parameter Name="max" Type="Decimal" />
                                                    <asp:Parameter Name="amount" Type="Decimal" />
                                                </InsertParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="min" Type="Decimal" />
                                                    <asp:Parameter Name="max" Type="Decimal" />
                                                    <asp:Parameter Name="amount" Type="Decimal" />
                                                    <asp:Parameter Name="id" Type="Int32" />
                                                </UpdateParameters>
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="dsBank" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>"
                                                SelectCommand="SELECT BankChargesId, CashDeposit, DebitOrder FROM tblBankCharges"
                                                DeleteCommand="DELETE FROM [tblBankCharges] WHERE [BankChargesId] = @BankChargesId"
                                                InsertCommand="INSERT INTO [tblBankCharges] ([CashDeposit], [DebitOrder]) VALUES (@CashDeposit, @DebitOrder)"
                                                UpdateCommand="UPDATE [tblBankCharges] SET [CashDeposit] = @CashDeposit, [DebitOrder] = @DebitOrder WHERE [BankChargesId] = @BankChargesId">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="BankChargesId" Type="Int32" />
                                                </DeleteParameters>
                                                <InsertParameters>
                                                    <asp:Parameter Name="CashDeposit" />
                                                    <asp:Parameter Name="DebitOrder" />
                                                </InsertParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="CashDeposit" />
                                                    <asp:Parameter Name="DebitOrder" />
                                                    <asp:Parameter Name="BankChargesId" Type="Int32" />
                                                </UpdateParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlJournals" runat="server">
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Monthly Service Fees"></asp:Label>
                                <br />
                                <table class="style1">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="Description: "></asp:Label>
                                            <asp:TextBox ID="txtDescription" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="Reference: "></asp:Label>
                                            <asp:TextBox ID="txtReference" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Text="Amount: "></asp:Label>
                                            <asp:TextBox ID="txtAmount" runat="server" Width="75px" Text="79.00"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="Trust Period: "></asp:Label>
                                            <asp:DropDownList ID="cmbPeriod" runat="server">
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
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged"
                                                Text="Select / Deselect All" />
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnApplyJournal" runat="server" Text="Apply Journal" OnClick="btnApplyJournal_Click" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Label ID="lblJnlStatus" runat="server"></asp:Label>
                                <br />
                                <asp:GridView ID="grdJournal" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                    DataSourceID="dsBuildings0">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="id" />
                                        <asp:TemplateField HeaderText="Building" SortExpression="Building">
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("Building") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" SortExpression="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                &nbsp;
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkJournal" runat="server" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                &nbsp;
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#C1C1C1" Font-Bold="True" Font-Size="Medium" ForeColor="Black" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="dsBuildings0" runat="server" ConnectionString="<%$ ConnectionStrings:AstrodonConnectionString %>" DeleteCommand="DELETE FROM [tblBuildings] WHERE [id] = @id" InsertCommand="INSERT INTO [tblBuildings] ([Building], [Code], [AccNumber], [Contra], [Period], [DataPath], [payments], [receipts], pm, bankName, accName, bankAccNumber, branch) VALUES (@Building, @Code, @AccNumber, @Contra, @Period, @DataPath, @payments, @receipts, @pm, @bankName, @accName, @bankAccNumber, @branch)" OnInserting="dsBuildings_Inserting" SelectCommand="SELECT id, Building, Code, AccNumber, Contra, Period, DataPath, payments, receipts, journals, pm, bankName, accName, bankAccNumber, branch FROM tblBuildings
where bank = 'TRUST'
 ORDER BY Building"
                                    UpdateCommand="UPDATE [tblBuildings] SET [Building] = @Building, [Code] = @Code, [AccNumber] = @AccNumber, [Contra] = @Contra, [Period] = @Period, [DataPath] = @DataPath, [payments] = @payments, [receipts] = @receipts, [journals] = @journals,pm = @pm, bankName = @bankName, accName = @accName, bankAccNumber = @bankAccNumber, branch = @branch WHERE [id] = @id">
                                    <DeleteParameters>
                                        <asp:Parameter Name="id" Type="Int32" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="Building" Type="String" />
                                        <asp:Parameter Name="Code" Type="String" />
                                        <asp:Parameter Name="AccNumber" Type="String" />
                                        <asp:Parameter Name="Contra" Type="String" />
                                        <asp:Parameter Name="Period" Type="String" />
                                        <asp:Parameter Name="DataPath" Type="String" />
                                        <asp:Parameter Name="payments" Type="Int32" />
                                        <asp:Parameter Name="receipts" Type="Int32" />
                                        <asp:Parameter Name="pm" Type="String" />
                                        <asp:Parameter Name="bankName" Type="String" />
                                        <asp:Parameter Name="accName" Type="String" />
                                        <asp:Parameter Name="bankAccNumber" Type="String" />
                                        <asp:Parameter Name="branch" Type="String" />
                                    </InsertParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="Building" Type="String" />
                                        <asp:Parameter Name="Code" Type="String" />
                                        <asp:Parameter Name="AccNumber" Type="String" />
                                        <asp:Parameter Name="Contra" Type="String" />
                                        <asp:Parameter Name="Period" Type="String" />
                                        <asp:Parameter Name="DataPath" Type="String" />
                                        <asp:Parameter Name="payments" Type="Int32" />
                                        <asp:Parameter Name="receipts" Type="Int32" />
                                        <asp:Parameter Name="journals" Type="Int32" />
                                        <asp:Parameter Name="pm" Type="String" />
                                        <asp:Parameter Name="bankName" Type="String" />
                                        <asp:Parameter Name="accName" Type="String" />
                                        <asp:Parameter Name="bankAccNumber" Type="String" />
                                        <asp:Parameter Name="branch" Type="String" />
                                        <asp:Parameter Name="id" Type="Int32" />
                                    </UpdateParameters>
                                </asp:SqlDataSource>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span style="font-family: Garamond; font-size: small">COMPANY REGISTRATION NUMBER 2004-003502/07
					INSTITUTE OF REALTORS NUMBER 747 FIDELITY CERTIFICATE 2005102678</span>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>