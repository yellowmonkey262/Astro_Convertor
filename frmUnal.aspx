<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUnal.aspx.cs" Inherits="frmUnal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click"
                Text="Back to unallocated" />
            <br />
            <br />
            Search For Statement Entry:<br />
            Search by date:<asp:TextBox ID="txtSDate" runat="server"></asp:TextBox>
            <br />
            or search by amount:<asp:TextBox ID="txtSAmt" runat="server"></asp:TextBox>
            <br />
            or search by description:<asp:TextBox ID="txtSDesc" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblAdd1" runat="server"></asp:Label>
            <br />
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                Text="Search statement" />
            <br />
            <asp:GridView ID="GridView1" runat="server" EmptyDataText="No records">
            </asp:GridView>
            <br />
            Record ID:<asp:TextBox ID="txtRec" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                Text="Add to unallocated" />
            <br />
            <br />
            Or enter new item<br />
            Date:<asp:TextBox ID="txtADate" runat="server"></asp:TextBox>
            <br />
            Amount:<asp:TextBox ID="txtAAmt" runat="server"></asp:TextBox>
            <br />
            Description:<asp:TextBox ID="txtADesc" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblAdd2" runat="server"></asp:Label>
            <br />
            <asp:Button ID="btnAdd2" runat="server" OnClick="btnAdd2_Click"
                Text="Add to unallocated" />
        </div>
    </form>
</body>
</html>