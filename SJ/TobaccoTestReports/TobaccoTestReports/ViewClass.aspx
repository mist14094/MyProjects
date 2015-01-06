<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewClass.aspx.cs" Inherits="ViewClass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>body	{width: 900px; margin: 20px auto; /* center */ padding: 20px;font-family: Roboto,"Helvetica Neue",Helvetica,Arial,sans-serif;
		 }</style>
</head>
<body >
    <form id="form1" runat="server">
       
        <h1 style="text-align:center">Select the Tobacco Product Class</h1>
         <div>
          <center>  <asp:RadioButtonList ID="rblChooseCatagory" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblChooseCatagory_SelectedIndexChanged">
                <asp:ListItem>By Latest</asp:ListItem>
                <asp:ListItem>By Popularity</asp:ListItem>
            </asp:RadioButtonList>
</center>
        </div>
        <p style="text-align:center">&nbsp;</p>
        <asp:ListView ID="ListView1" GroupItemCount="9" runat="server" DataKeyNames="ProductClass">
            <LayoutTemplate>
    <table cellpadding="2" runat="server"
           id="tblProducts" style="height:320px">
      <tr runat="server" id="groupPlaceholder">
      </tr>
    </table>
    
  </LayoutTemplate>
  <GroupTemplate>
    <tr runat="server" id="productRow"
        style="height:80px">
      <td runat="server" id="itemPlaceholder">
      </td>
    </tr>
  </GroupTemplate>
           <ItemTemplate>
    <td id="Td1" valign="top" align="center" style="width:300px;" runat="server">
      <asp:ImageButton ID="ProductImage" runat="server"  style="margin-bottom:10px" Height="10%"
           ImageUrl="~/tobacco_leaf.jpg" PostBackUrl='<%# string.Concat( "/ViewClassDetails.aspx?Class=" , Eval("ProductClass"))%>' />
           <br /><div style="font-weight:bold">
      <asp:HyperLink ID="ProductLink" runat="server"
           Target="_blank" Text='<% #Eval("ProductClass")%>'
           /></div>
        <br /><br />
    </td>
  </ItemTemplate>
            <EmptyItemTemplate>
            </EmptyItemTemplate>
        </asp:ListView>

    </form>
</body>
</html>
