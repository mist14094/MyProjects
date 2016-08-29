<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CopyItemWithPrint.aspx.cs" Inherits="SJDealStore.Pages.Items.CopyItemWithPrint" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png" />
    <link rel="apple-touch-icon" sizes="57x57" href="apple-touch-icon-57x57.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="apple-touch-icon-72x72.png" />
    <link rel="apple-touch-icon" sizes="76x76" href="apple-touch-icon-76x76.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="apple-touch-icon-114x114.png" />
    <link rel="apple-touch-icon" sizes="120x120" href="apple-touch-icon-120x120.png" />
    <link rel="apple-touch-icon" sizes="144x144" href="apple-touch-icon-144x144.png" />
    <link rel="apple-touch-icon" sizes="152x152" href="apple-touch-icon-152x152.png" />
    <link rel="apple-touch-icon" sizes="180x180" href="apple-touch-icon-180x180.png" />
      <script src="http://labelwriter.com/software/dls/sdk/js/DYMO.Label.Framework.latest.js" type="text/javascript" charset="UTF-8"></script>
  
    <title>Reports</title>

    <!-- Bootstrap core CSS -->
    <link href="../dist/css/bootstrap.min.css" rel="stylesheet">

    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <link href="../assets/css/ie10-viewport-bug-workaround.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="sticky-footer-navbar.css" rel="stylesheet">

    <!-- Just for debugging purposes. Don't actually copy these 2 lines! -->
    <!--[if lt IE 9]><script src="../../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
    <script src="../assets/js/ie-emulation-modes-warning.js"></script>

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
  <script type="text/javascript">
      function confirmDelete(filename, barcode, skulotNumber, stockDesc, price, otherprice, stockCode, NoOfCopies, TestTag) {
          try {

              //     var x = window.location.protocol + "//" + window.location.host + "/dealstore/OverStock_1.label";

              var label = dymo.label.framework.openLabelFile(window.location.protocol + "//" + window.location.host + "/dealstore/" + "overstock_1.label");
              //dymo.label.framework.openLabelXml(labelXml);

              // set label text

              label.setObjectText("BARCODE", barcode.toString());
              label.setObjectText("UPC", skulotNumber.toString());
              label.setObjectText("Desc", stockDesc.toString());
              label.setObjectText("Price", price);
              label.setObjectText("OrgPrice", otherprice);
              label.setObjectText("ShortLot", stockCode);
              label.setObjectText("TestTag", TestTag);

              // select printer to print on
              // for simplicity sake just use the first LabelWriter printer
              var printers = dymo.label.framework.getPrinters();
              if (printers.length == 0)
                  throw "No DYMO printers are installed. Install DYMO printers.";

              var printerName = "";
              for (var i = 0; i < printers.length; ++i) {
                  var printer = printers[i];
                  if (printer.printerType == "LabelWriterPrinter") {
                      printerName = printer.name;
                      break;
                  }
              }

              if (printerName == "")
                  throw "No LabelWriter printers found. Install LabelWriter printer";

              // finally print the label
              label.print(printerName);
          }
          catch (e) {
              alert(e.message || e);
          }
      }
    </script>
    <script type="text/javascript">
       

        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
    </script>
     
</head>
<body>
    <form id="form1" runat="server">

<div id="dvScroll" >
        <!-- Fixed navbar -->
        <nav class="navbar navbar-default navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">Smokin Joe Deal Store</a>
                </div>
                <div id="navbar" class="collapse navbar-collapse">
                    <ul class="nav navbar-nav">
                        <li class="active"><a href="Home.aspx">Home</a></li>

                        <li class="Operations">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Operations <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li><a href="#"></a></li>
                                <li><a href="Returns.aspx">Returns</a></li>
                                <li><a href="Sales.aspx">Sales</a></li>

                            </ul>
                        </li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </nav>
        <br />
        <!-- Begin page content -->
        <div class="container">
            <div class="page-header">
            </div>

            <div>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Operations - Copy from Old Item</h3>
                    </div>
                    <div class="panel-body">
                  
                        <asp:DropDownList ID="ddlFileSelect" runat="server" Width="50%" >
                        </asp:DropDownList>
                        <br />
                        <br />
                        Unique Number :
                     <b> <asp:Label ID="lblUniqueNumber" runat="server" Text="Label"></asp:Label></b>  
                        <br />
                        UPC<br />
                        <asp:TextBox ID="txtUPC" runat="server" Width="70%"></asp:TextBox>
                        &nbsp;
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUPC" ErrorMessage="UPC Required"></asp:RequiredFieldValidator>
                        <br />
                        Desc<br />
                        <asp:TextBox ID="txtDesc" runat="server" Width="70%"></asp:TextBox>
                        &nbsp;
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Description Required" ControlToValidate="txtDesc"></asp:RequiredFieldValidator>
                        <br />
                        Original Price [MSRP]<br />
                        <asp:TextBox ID="txtMSRP" runat="server" Width="70%" ></asp:TextBox>
                       

                        &nbsp;
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ErrorMessage="MSRP Required" ControlToValidate="txtMSRP"></asp:RequiredFieldValidator>
                       
                        <asp:CompareValidator id="CheckFormat1" runat="server" ControlToValidate="txtMSRP" Operator="DataTypeCheck"
   Type="Currency"  Display="Dynamic" ErrorMessage="Illegal format for currency" />

                        <br />
                        SJ Retail<br />
                        <asp:TextBox ID="txtSJRetail" runat="server" Width="70%"></asp:TextBox>
                        &nbsp;
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Retail Required" ControlToValidate="txtSJRetail"></asp:RequiredFieldValidator>
                           <asp:CompareValidator id="CompareValidator1" runat="server" ControlToValidate="txtSJRetail" Operator="DataTypeCheck"
   Type="Currency"  Display="Dynamic" ErrorMessage="Illegal format for currency" />
                        <br/> 
                        
                          Number of Units <br />
                        <asp:TextBox ID="txtNoOfUnits" runat="server" Width="70%"></asp:TextBox>
                        &nbsp;
                        <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ErrorMessage="No of Units Required" ControlToValidate="txtNoOfUnits"></asp:RequiredFieldValidator>
                     
                        &nbsp;<asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Valid no of Units Req." Type="Integer" ControlToValidate="txtNoOfUnits" Display="Dynamic" MaximumValue="20" MinimumValue="1"></asp:RangeValidator>
                     
                        <br/> 
                        
                          <br/>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                        <br />

                        <asp:Label ID="lblreport" runat="server" Text=""></asp:Label>

                        <br />
                        <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label>
                        <br />
                        <div  style="overflow: auto;" class="container-fluid">
                       <div id="printablediv"  >
                          </div>  
                            <br/>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <footer class="footer">
            <div class="container">
                <p class="text-muted">Smokin Joes - 2015</p>
            </div>
        </footer>

</div>
        <!-- Bootstrap core JavaScript
    ================================================== -->
        <!-- Placed at the end of the document so the pages load faster -->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
        <script>window.jQuery || document.write('<script src="../../assets/js/vendor/jquery.min.js"><\/script>')</script>
        <script src="../dist/js/bootstrap.min.js"></script>
        <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
        <script src="../assets/js/ie10-viewport-bug-workaround.js"></script>

    </form>
</body>
</html>
