<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddItemsPrint.aspx.cs" Inherits="AddItemsPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Print Tags</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
    
       <script src="http://labelwriter.com/software/dls/sdk/js/DYMO.Label.Framework.latest.js" type="text/javascript" charset="UTF-8"></script>
  
    
    <script type="text/javascript">
        function confirmDelete( TobaccoType, MfgDate, Weight, Moisture,Barcode) {
          try {

              //     var x = window.location.protocol + "//" + window.location.host + "/dealstore/OverStock_1.label";

              var label;
              try {
                  label = dymo.label.framework.openLabelFile(window.location.protocol + "//" + window.location.host + "/" + "ShippingLabel.label");
              }
              catch (err) {
                  label = dymo.label.framework.openLabelFile(window.location.protocol + "//" + window.location.host + "/jarvisv2/" + "ShippingLabel.label");
              }
              //dymo.label.framework.openLabelXml(labelXml);

              // set label text

              label.setObjectText("TobaccoType", TobaccoType.toString());
              label.setObjectText("Date", MfgDate.toString());
              label.setObjectText("Weight", Weight.toString());
              label.setObjectText("Moisture", Moisture.toString());
              label.setObjectText("BARCODE", Barcode.toString());
              
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
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="auto-style1">
            <tr>
                <td>Select Tobacco Type<br />
        <asp:DropDownList ID="ddlTobaccoType" runat="server" Width="100%"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Mfg Date<br />
                    <asp:TextBox ID="txtMfgDate" type="date" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Total Weight in LBs<br />
                    <asp:TextBox ID="txtTotalWeight" runat="server" step="any" type="number" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Final Moisture<br />
                    <asp:TextBox ID="txtMoisture" runat="server" step="any" type="number" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>New Tote#<br />
                    <asp:TextBox ID="txtNewTote" runat="server" step="any" type="number"  Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>RFID Tag#<br />
                    <asp:TextBox ID="txtRFID" runat="server" Width="100%"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="100%" OnClick="btnSave_Click" />
<br/>
                    <br />
<asp:Button ID="Button2" runat="server" Text="Clear" Width="100%" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
