<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODetails.aspx.cs" Inherits="_PODetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:Segoe WP" >
        Search PO by Purchase Order Number, Supplier Name, Web PO Order Number or by Customer<br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="128px" 
            TextMode="MultiLine" Width="795px" ></asp:TextBox>
            <br />
        <br />
            <asp:Button ID="Button1" runat="server"
                Text="Search Information" onclick="Button1_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server"
                Text="Top 30 PO" onclick="Button2_Click" />
        <br />
        <br />
    </div>
    <div style="   margin-left:5%; margin-right:5%;">
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" BackColor="White"  Font-Names="Segoe WP"  Width="100%"
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="White" ForeColor="#003399" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
    </asp:GridView></div>
    </form>
</body>
</html>
