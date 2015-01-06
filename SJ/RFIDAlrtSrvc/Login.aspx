<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  <link rel="stylesheet" href="css/style.css">
</head>
<body>
    <form class="sign-up" id="form1" runat="server">
    <div>
    
    <h1 class="sign-up-title">RFID Alert System</h1>
       <div   align="middle"> <img src="logonew.png" /></div> <br />
       <asp:TextBox ID="txtUserID" class="sign-up-input" placeholder="What's your username?" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtPassword"  class="sign-up-input" placeholder="Your password goes here !" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="BtnLogin" runat="server" Text="Log me in!" 
            class="sign-up-button" onclick="BtnLogin_Click"  />
<br />
        <br />
     <div   align="middle">   <asp:Label ID="lblPassword" runat="server" Text="" ></asp:Label></div>
 

    </div>
    </form>
</body>
</html>
