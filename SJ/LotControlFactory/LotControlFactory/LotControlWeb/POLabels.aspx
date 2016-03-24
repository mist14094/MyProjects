<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="POLabels.aspx.cs" Inherits="LotControlWeb.POLabels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         function PrintTags(filename, barcode, supplier, lotnumber, stockcode, description, ponumber, grn, quantity,masterLineId, noOfCopies) {
             try {

                 //     var x = window.location.protocol + "//" + window.location.host + "/dealstore/OverStock_1.label";

                 var label = dymo.label.framework.openLabelFile(filename);
                 //dymo.label.framework.openLabelXml(labelXml);

                 // set label text

                 label.setObjectText("BARCODE", barcode.toString());
                 label.setObjectText("Supplier", supplier.toString());
                 label.setObjectText("lotnumber", lotnumber.toString());
                 label.setObjectText("stockcode", stockcode.toString());
                 label.setObjectText("description", description);
                 label.setObjectText("ponumber", ponumber);
                 label.setObjectText("grn", grn);
                 label.setObjectText("Quantity", quantity);
                 label.setObjectText("MasterLineID", masterLineId);
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

                 var printParams = {};
                 printParams.copies = noOfCopies;
                 if (printerName == "")
                     throw "No LabelWriter printers found. Install LabelWriter printer";

                 // finally print the label
                 label.print(printerName, dymo.label.framework.createLabelWriterPrintParamsXml(printParams));
             }
             catch (e) {
                 alert(e.message || e);
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

 
    <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label>
    <br />
    
        <asp:Button ID="btnPrint" runat="server" Visible="False" Text="Print Tags" type="button" class="btn btn btn-success center-block" Width="50%" OnClick="btnPrint_Click" />
    <br/>
    <asp:GridView ID="GridView1" runat="server" CssClass="table  table-hover" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GridView1_RowDataBound">

        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox Checked="True" ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                    <asp:Label runat="server" ID="lblhdr" Text="Tags#"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkRow" runat="server" Checked="True" />
                
                </ItemTemplate>
            </asp:TemplateField>
          
            <asp:BoundField DataField="LabelId" HeaderText="Barcode" />
             <asp:BoundField DataField="MasterLineId" HeaderText="Master ID" />
            <asp:BoundField DataField="StockCode" HeaderText="Stock Code" />
            <asp:BoundField DataField="Description" HeaderText="Description" />

            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:Label ID="Quantity" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity","{0:#.##}") %> ' runat="server"> </asp:Label>
                    <asp:Label ID="UOM" Text='<%# Bind("Unitofmeasure") %> ' runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:BoundField DataField="LineNumberInOrder" HeaderText="#" />
            <asp:BoundField DataField="Lotnumber" HeaderText="Lot Number" />
            <asp:BoundField DataField="GRNNumber" HeaderText="GRN#" />
            <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
            <asp:BoundField DataField="PoNumber" HeaderText="PO Number" />
            <asp:BoundField DataField="Warehouse" HeaderText="Warehouse" Visible="False" />
            <asp:BoundField DataField="Date" HeaderText="Created Date" DataFormatString="{0:MM/dd/yyyy}" Visible="False" />
                        <asp:BoundField DataField="Warehouse" HeaderText="Warehouse"  Visible="False"/>
             <asp:BoundField DataField="Notes" HeaderText="Notes"  Visible="False"/>
       

        </Columns>
        <EmptyDataTemplate>No Lots Found</EmptyDataTemplate>
    </asp:GridView>

    <asp:Label ID="lblFlagOddNumberofTags" runat="server" BackColor="lightblue" Text=" * Odd number of Quanity" />


    
    <br/><br/>
</asp:Content>
