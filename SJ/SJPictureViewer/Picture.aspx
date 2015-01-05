<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Picture.aspx.cs" Inherits="Picture" EnableEventValidation="false" %>


<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title></title>
     <style>body	{ margin: 20px auto; /* center */ padding: 20px;font-family: Roboto,"Helvetica Neue",Helvetica,Arial,sans-serif;
		 }</style>
   <style>

 

            /* This rule is read by Galleria to define the gallery height: */
            #galleria{height:600px;}

        </style>

        <!-- load jQuery -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.js"></script>

        <!-- load Galleria -->
        <script src="galleria-1.4.2.min.js"></script>

        <!-- load the History plugin, no need for further scripting -->
        <script src="galleria.history.min.js"></script>
</head>
<body >
    <form id="form1" runat="server">
         <p style="text-align:right">
             <asp:HyperLink ID="HyperLink1" runat="server">Back to Store</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </p>
        <h1 style="text-align:center"> <asp:Label ID="lblStoreID" runat="server" Text=""></asp:Label> </h1>
       <h2 style="text-align:center"> <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></h2>

         <br />
         <div class="content">
     
        <!-- Adding gallery images. We use resized thumbnails here for better performance, but it’s not necessary -->
<div id="Div1" runat="server">
          
        </div>
        <div id="galleria" runat="server">
          
        </div>
 

    </div>

    <script>

        // Load the classic theme
        Galleria.loadTheme('../../themes/classic/galleria.classic.min.js');

        // Initialize Galleria
        Galleria.run('#galleria');
        Galleria.configure({
            imageCrop: false,
            transition: 'slide',
            _toggleInfo: false,
            showImagenav:true
        });

    </script>
       
        

    </form>
</body>
</html>
