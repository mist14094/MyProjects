<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="EditItemsPrint.aspx.cs" Inherits="SJFactoryInventory.EditItemsPrint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Create New Label</title>
     <script src="http://labelwriter.com/software/dls/sdk/js/DYMO.Label.Framework.latest.js" type="text/javascript" charset="UTF-8"></script>
  <script type="text/javascript">
        function confirmDelete( TobaccoType, MfgDate, Weight, Moisture,Barcode,NewTote,RFID) {
          try {

              //     var x = window.location.protocol + "//" + window.location.host + "/dealstore/OverStock_1.label";

              var label;
              try {
                  label = dymo.label.framework.openLabelFile(window.location.protocol + "//" + window.location.host + "/Labels/" + "64Label.label");
              }
              catch (err) {
                  label = dymo.label.framework.openLabelFile(window.location.protocol + "//" + window.location.host + "/jarvisv2/Labels/" + "64Label.label");
              }
              //dymo.label.framework.openLabelXml(labelXml);

              // set label text

              label.setObjectText("TobaccoType", TobaccoType.toString());
              label.setObjectText("Date", MfgDate.toString());
              label.setObjectText("Weight", Weight.toString());
              label.setObjectText("Moisture", Moisture.toString());
              label.setObjectText("BARCODE", Barcode.toString());
              label.setObjectText("NewTote", NewTote.toString());
              label.setObjectText("RFID", RFID.toString());
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="media" style="max-width: 992px; margin-left: auto; margin-right: auto;">
   
      <div class="panel panel-default" > 
        <div class="panel-heading">
    <h3 class="panel-title">Create New Label</h3>
  </div>
    
        <div class="panel-body">
               <br/>
    
       <br/>
           
       <%--///////////////////////////////////////////////////////////////////////////--%>
             <div class="input-group" style="width:100%;">
                <span class="input-group-addon" id="basic-addon10" style="width:175px;"  >Tag Number</span>
                <asp:TextBox  ID="txtSno" runat="server" Width="100%"  class="form-control" aria-describedby="basic-addon1" Enabled="False"  ></asp:TextBox>  
            </div>
            <br/>
            
            <div class="input-group" style="width:100%;">
                <span class="input-group-addon" id="basic-addon1" style="width:175px;"  >Select Tobacco Type</span>
                 <asp:DropDownList ID="ddlTobaccoType" runat="server" Width="100%"  class="form-control" aria-describedby="basic-addon1"  ></asp:DropDownList>      <br/>
            </div>
            <br/>
            
            <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon2" style="width:175px;" >Select Location</span>
                 <asp:DropDownList ID="ddlLocation" runat="server" Width="100%"  class="form-control" aria-describedby="basic-addon1"  ></asp:DropDownList>      <br/>
            </div>
            <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">Mfg Date</span>
                 <asp:TextBox ID="txtMfgDate" type="date"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
             <asp:RequiredFieldValidator ID="rfvMfgDate" runat="server"  ControlToValidate="txtMfgDate" ErrorMessage="*Mfg Date Required"></asp:RequiredFieldValidator>
            <br/>    
              <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon4"   style="width:175px;">Total Weight in LBs</span>
                 <asp:TextBox ID="txtTotalWeight" runat="server" Width="100%"  class="form-control" aria-describedby="basic-addon1" step="any" type="number"  ></asp:TextBox>   
            </div>
              <asp:RequiredFieldValidator ID="rfvTotalWeight" runat="server" ControlToValidate="txtTotalWeight" ErrorMessage="*Total Weight Required"></asp:RequiredFieldValidator>  
            <br/>   
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon5"   style="width:175px;">Final Moisture</span>
                 <asp:TextBox  ID="txtMoisture" runat="server" Width="100%"  class="form-control" aria-describedby="basic-addon1" step="any" type="number"  ></asp:TextBox>    
                  
            </div>
            <asp:RequiredFieldValidator ID="rfvMoisture" runat="server" ControlToValidate="txtMoisture" ErrorMessage="*Moisture Required"></asp:RequiredFieldValidator>
            <br/>   
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon6"   style="width:175px;">New Tote#</span>
                 <asp:TextBox  ID="txtNewTote" runat="server" Width="100%"  class="form-control" aria-describedby="basic-addon1"   ></asp:TextBox>     
                
            </div>
             <asp:RequiredFieldValidator ID="rfvNewTote" runat="server" ControlToValidate="txtNewTote" ErrorMessage="*Tote# Required"></asp:RequiredFieldValidator>
            <br/>   
               <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon7"   style="width:175px;">RFID Tag#</span>
                 <asp:TextBox  ID="txtRFID" runat="server" Width="100%"  class="form-control" aria-describedby="basic-addon1"   ></asp:TextBox>    
                  
            </div>
             <asp:RequiredFieldValidator ID="rfvRfid" runat="server" ControlToValidate="txtRFID" ErrorMessage="*RFID Tag Required"></asp:RequiredFieldValidator>
            <br/>   
            
                    <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
                    <br />
            <br/>
            <table class="nav-justified">
                <tr>
                    <td>  <asp:Button  ID="btnSave" runat="server" Text="Save"  OnClick="btnSave_Click" type="button" class="btn btn btn-success center-block" Width="80%"/></td>
                    <td>&nbsp;</td>
                    <td><asp:Button ID="btnClear" runat="server"  Text="Clear" type="button" class="btn btn btn-success center-block" Width="80%" /></td>
                </tr>
            </table>
            

           
            </div>
        </div>

    </div>
    
    
</asp:Content>
