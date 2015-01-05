<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayStore.aspx.cs" Inherits="DisplayStore" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>body	{width: 600px; margin: 20px auto; /* center */ padding: 20px;font-family: Roboto,"Helvetica Neue",Helvetica,Arial,sans-serif;
		 }</style>
</head>
<body >
    <form id="form1" runat="server">
        <h1 style="text-align:center">Select the store</h1>
        <p style="text-align:center">&nbsp;</p>
        <asp:ListView ID="ListView1" GroupItemCount="4" runat="server" DataKeyNames="pic_id">
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
      <asp:ImageButton ID="ProductImage" runat="server"  style="margin-bottom:10px"
           ImageUrl="~/shop.png" PostBackUrl='<%# string.Concat( "/DisplayPictureFolder.aspx?StoreID=" , Eval("pic_id"))%>' />
           <br /><div style="font-weight:bold">
     Store <asp:HyperLink ID="ProductLink" runat="server"
           Target="_blank" Text='<% #Eval("pic_id")%>'
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
