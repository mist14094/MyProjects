<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Speak to Jarvis !!!</title>

    <style>
html, body { height: 100%; width: 100%; margin: 0; background-color:#00172a; }
div { height: 100%; width: 100%;  }
</style>

</head>
<body>
    <form id="form1" runat="server">
    <div >
    <br />

   <center>   <img src="LogoWEB.png"   style=" position:absolute; top: 10px; margin-left:-210px; z-index: 1;"/>
      &nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtHelloWorld" runat="server" Height="40px" 
           Width="532px" Font-Size="Large"></asp:TextBox>
                 &nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnHelloWorld" runat="server" Text="Speak to me" 
                    onclick="btnHelloWorld_Click" Height="46px" />
       <br />
       <asp:Label ID="Label1" runat="server" ForeColor="White" Font-Size="Large"></asp:Label>
        </center>
                          <iframe runat="server" id="IFrame" frameborder="0" width="100%" height="95%"  style="height: 600px;">
  <p>Your browser does not support iframes.</p>
</iframe>
    </div>
    </form>
</body>
</html>
