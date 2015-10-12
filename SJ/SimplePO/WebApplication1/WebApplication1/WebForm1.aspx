<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="import" href="../scripts/polymer.html">
    <script>
  // register a new element called proto-element
  Polymer({
    is: "proto-element",
    // add a callback to the element's prototype
    ready: function() {
      this.textContent = "I'm a proto-element. Check out my prototype!"
    }
  });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
