<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RopeCourseMonitor.aspx.cs" Inherits="AdWeb.RopeCourseMonitor" %>

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

               
              
              
        <div>
         <b> <h1 style="color: red; font-style: normal; text-decoration: underline; font-weight: bolder;">Rope Course</h1></b>  
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                <ContentTemplate>

                    <asp:Timer ID="Timer1" runat="server" Interval="3000" OnTick="Timer1_Tick"></asp:Timer>
 <div class="container">
     <br/><br/>
                    <asp:GridView   class="table table-striped table-hover" style="color: black; font-size: 120%" ID="grdMonitor" runat="server" OnRowDataBound="grdMonitor_RowDataBound" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Tagnumber" HeaderText="Tag Number" />
                            <asp:BoundField DataField="InTime" HeaderText="In Time" />
                            <asp:BoundField DataField="MinuteDiff" HeaderText="Time Spent" />
                            <asp:BoundField DataField="RopeCourseInMinutes" HeaderText="Purchased Time" />
                            <asp:BoundField DataField="RemainingTime" HeaderText="Remaining Time" />
                            <asp:BoundField DataField="Status"  HeaderText="Status"  />
                            <asp:BoundField DataField="Color" Visible="False" />
                        </Columns>
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
