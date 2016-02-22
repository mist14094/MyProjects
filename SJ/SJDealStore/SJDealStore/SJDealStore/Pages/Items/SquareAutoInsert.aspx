<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SquareAutoInsert.aspx.cs" Inherits="SJDealStore.Pages.Items.SquareAutoInsert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <br/><asp:Button ID="btnGetName" runat="server" Text="Search" OnClick="btnGetName_Click" />
    </div>
    </form>
</body>
</html>
