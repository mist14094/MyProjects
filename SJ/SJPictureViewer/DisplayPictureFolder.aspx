<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayPictureFolder.aspx.cs" Inherits="DisplayPictureFolder" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>body	{width: 700px; margin: 20px auto; /* center */ padding: 20px;font-family: Roboto,"Helvetica Neue",Helvetica,Arial,sans-serif;
		 }</style>
</head>
<body >
    <form id="form1" runat="server">
         <p style="text-align:right"><a href="DisplayStore.aspx"> Go Back </a></p>
        <h1 style="text-align:center">Please select date </h1>
        
       
        <asp:ListView ID="ListView1" GroupItemCount="6" runat="server" DataKeyNames="Date"   >
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
           ImageUrl="~/Folder.png" PostBackUrl='<%# string.Concat( "/Picture.aspx?StoreID=" , Eval("StoreID"),"&Date=",Eval("Date", "{0:MMddyyyy}"))%>'
           /><br /><div style="font-weight:bold">
      <asp:HyperLink ID="ProductLink" runat="server"
           Target="_blank" Text='<% #Eval("Date", "{0:ddd,MMM-dd}")%>'
           /></div>
        <br /><br />
    </td>
  </ItemTemplate>
            <EmptyDataTemplate><div style="text-align:center;">
           <h1>    No picture found !!!</h1> </div>
            </EmptyDataTemplate>
        </asp:ListView>

    </form>
</body>
</html>
