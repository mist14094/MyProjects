<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login1.aspx.cs" Inherits="PriceChangerWeb.Login" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
        <asp:DropDownList ID="ddlSearchCriteria" runat="server"></asp:DropDownList>
        
        <asp:TextBox ID="srchTextBox" runat="server" Width="500px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Button" OnClick="btnSearch_Click" />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
     <asp:Panel runat="server" ID="pnlGridView"></asp:Panel>
           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="UPC" HeaderText="UPC" />
                  <asp:BoundField DataField="SKU" HeaderText="SKU" />
                  <asp:BoundField DataField="desc" HeaderText="Description" />
                 <asp:BoundField DataField="price" HeaderText="Price" />
                 <asp:BoundField DataField="Custom1" HeaderText="Cost" />
            </Columns>
        </asp:GridView>
        
    </div>
    </form>
</body>
</html>
