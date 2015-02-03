<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Startup.aspx.cs" Inherits="QuickSales.Startup" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
          <div style="margin-top: 30px;"></div>
        <div style="margin-top: 30px;"><center><h3>Sales Report for 2014 - 2015</h3>
            </center>
          </div>
    <div>
       <div style="margin-top: 30px;" >
           
           <table class="auto-style1">
               <tr>
                   <td>&nbsp;</td>
                   <td  style= "margin: 0px auto 0px auto">
           <center>
               Select Vendor&nbsp;
        <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="500px">
            <asp:ListItem>--Select---</asp:ListItem>
            <asp:ListItem>Tripi Foods</asp:ListItem>
            <asp:ListItem>Frito Lay</asp:ListItem>
               <asp:ListItem>Pepsi</asp:ListItem>
               </asp:DropDownList>
        
        &nbsp;
        
        
        &nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
               </center>    </td>
                   <td>&nbsp;</td>
               </tr>
               <tr>
                   <td>&nbsp;</td>
                   <td style= "margin: 0px auto 0px auto"> &nbsp;</td>
                   <td>&nbsp;</td>
               </tr>
               <tr>
                   <td>&nbsp;</td>
                   <td style= "margin: 0px auto 0px auto"> <center>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label></center>
                   </td>
                   <td>&nbsp;</td>
               </tr>
           </table>
       </div>
     <div style="margin: 50px;">
           
        </div>
    </div>
    </form>
</body>
</html>