<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JustCountMonitor.aspx.cs" Inherits="AdWeb.JustCountMonitor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/bootstrapNew.css" rel="stylesheet" />
    <link href="css/bootstrap-theme.css" rel="stylesheet" />
    
    <link href='https://fonts.googleapis.com/css?family=Lobster|Open+Sans:400,400italic,300italic,300|Raleway:300,400,600' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css">
    <link href="css/bootstrap-theme.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/animate.css">
    <link rel="stylesheet" type="text/css" href="css/style.css">

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        
    <div class="content">
      <div class="container wow fadeInUp delay-03s"  >
        <div class="row">
          <div class="logo text-center">
            <img src="img/logo.png" alt="logo" width="20%">
            
      <div class="container" >
          <div class="row">

               
              
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                <ContentTemplate>
        <div>
         <b> <h1 style="color: red; font-style: normal; text-decoration: underline; font-weight: bolder;">
             <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h1></b>  
          

                    <asp:Timer ID="Timer1" runat="server" Interval="3000" OnTick="Timer1_Tick"></asp:Timer>
 <div class="container">
     <br/><br/>
                    <asp:GridView     class="table" style="color: black; font-size: 120%" ID="grdMonitor" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
     </div>
                </ContentTemplate>

            </asp:UpdatePanel>
        </div>
              
              
              
              
              

          </div>
        </div>
      
    </div>
            </div>
          </div>
        </div>
    
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.countdown.min.js"></script>
    <script src="js/wow.js"></script>
    <script src="js/custom.js"></script>
    <script src="contactform/contactform.js"></script>

        
        
        

    </form>
</body>
</html>
