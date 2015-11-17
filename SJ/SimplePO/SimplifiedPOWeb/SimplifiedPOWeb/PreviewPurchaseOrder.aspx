<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewPurchaseOrder.aspx.cs" Inherits="SimplifiedPOWeb.PreviewPurchaseOrder" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Simplified Purchase Order System">
    <title>Simplified PO</title>
    <link rel="stylesheet" href="http://yui.yahooapis.com/pure/0.6.0/pure-min.css">
    <link rel="stylesheet" href="css/layouts/side-menu.css">
    <link href="css/pure-min.css" rel="stylesheet" />
    <link rel='shortcut icon' type='image/x-icon' href='favicon.ico' />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="content" id="Content" runat="server" Visible="True" >
                <div>
                  <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <style type="text/css">
        .auto-style4 { width: 100px; }
   
        .auto-style9 { width: 35%;}
    </style>
    
    <div style=" min-width:950px;  width: 100%">
    <center>
    <h2>Preview Purchase Order</h2></center>
        <h3><center><asp:Label ID="PONumber" runat="server" Text=""></asp:Label></center></h3>
        <h5><center><asp:Label ID="Date" runat="server" Text=""></asp:Label></center></h5>
        <div style="font-size: small;">
       
   </div> <table class="pure-table pure-table-Noline" style="width: 100%;Font-Size:Small;">
        <thead>
            <td colspan="2"><h3>Buyer Information</h3></td>
            <td class="auto-style3">&nbsp;</td>
            <td colspan="2"><h3>Supplier Information</h3></td>
        </thead>
        <tr>
            <td class="auto-style4">UserName</td>
            <td class="auto-style5">
                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
            </td>
            <td class="auto-style6"></td>
            <td class="auto-style7">Select Entity</td>
            <td class="auto-style5">
                  <asp:Label ID="lblEntity" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style8" >Post for</td>
            <td class="auto-style9">
                  <asp:Label ID="lblBuyerPostFor" runat="server" Text=""></asp:Label>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">Select Supplier</td>
            <td class="auto-style9">
                  <asp:Label ID="lblSupplier" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Buyer&#39;s Name</td>
            <td class="auto-style9">
                  <asp:Label ID="lblBuyerName" runat="server" Text=""></asp:Label>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">Supplier Name</td>
            <td class="auto-style9">
                  <asp:Label ID="lblSupplierName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Buyer Address</td>
            <td class="auto-style9">
                  <asp:Label ID="lblBuyerAddress" runat="server" Text=""></asp:Label>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">Supplier Address</td>
            <td class="auto-style9">
                  <asp:Label ID="lblSupplierAddress" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Contact Number</td>
            <td class="auto-style9">
                  <asp:Label ID="lblBuyerContact" runat="server" Text=""></asp:Label>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">Contact Number</td>
            <td class="auto-style9">
                  <asp:Label ID="lblSupplierContact" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Priority</td>
            <td class="auto-style9">
                  <asp:Label ID="lblPriority" runat="server" Text=""></asp:Label>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">Notes</td>
            <td class="auto-style9">
                  <asp:Label ID="lblNotes" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        
        </table>
    </div>
 
    <br/><div style="font-size: small;">
            
</div>
      <telerik:RadGrid ID="RadGrid1" runat="server" CssClass="RadGrid" ShowFooter="True"
            AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
            ShowStatusBar="True"   
            AllowAutomaticUpdates="True"  GroupPanelPosition="Top" >
            <MasterTableView  CommandItemDisplay="Bottom" 
                DataKeyNames="sno">
             
                <CommandItemSettings ShowRefreshButton="False"  ShowAddNewRecordButton="False"></CommandItemSettings>
                <Columns>
                    <telerik:GridBoundColumn UniqueName="sno" HeaderText="sno" DataField="sno" DataType="System.Int32" FilterControlAltText="Filter sno column" ReadOnly="True" SortExpression="sno" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="POMasterNo" HeaderText="POMasterNo" DataField="POMasterNo" DataType="System.Int32" FilterControlAltText="Filter POMasterNo column" SortExpression="POMasterNo" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="VendorCode" HeaderText="VendorCode" DataField="VendorCode" FilterControlAltText="Filter VendorCode column" SortExpression="VendorCode">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="StockCode" HeaderText="StockCode" DataField="StockCode" FilterControlAltText="Filter StockCode column" SortExpression="StockCode">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Description" HeaderText="Description" DataField="Description" FilterControlAltText="Filter Description column" SortExpression="Description">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="UPC" FilterControlAltText="Filter UPC column" HeaderText="UPC" SortExpression="UPC" UniqueName="UPC">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SKU" FilterControlAltText="Filter SKU column" HeaderText="SKU" SortExpression="SKU" UniqueName="SKU">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Manufacturer" FilterControlAltText="Filter Manufacturer column" HeaderText="Manufacturer" SortExpression="Manufacturer" UniqueName="Manufacturer">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Model" FilterControlAltText="Filter Model column" HeaderText="Model" SortExpression="Model" UniqueName="Model">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Color" FilterControlAltText="Filter Color column" HeaderText="Color" SortExpression="Color" UniqueName="Color">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Size" FilterControlAltText="Filter Size column" HeaderText="Size" SortExpression="Size" UniqueName="Size">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Style" FilterControlAltText="Filter Style column" HeaderText="Style" SortExpression="Style" UniqueName="Style">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Gender" FilterControlAltText="Filter Gender column" HeaderText="Gender" SortExpression="Gender" UniqueName="Gender">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="UnitOfMeasure" FilterControlAltText="Filter UnitOfMeasure column" HeaderText="UnitOfMeasure" SortExpression="UnitOfMeasure" UniqueName="UnitOfMeasure">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter Quantity column" HeaderText="Quantity" SortExpression="Quantity" UniqueName="Quantity" Aggregate="Sum" FooterText=" ">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Cost" FilterControlAltText="Filter Cost column" HeaderText="Cost" SortExpression="Cost" UniqueName="Cost" DataType="System.Decimal" DataFormatString="{0:#.##}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Retail" FilterControlAltText="Filter Retail column" HeaderText="Retail" SortExpression="Retail" UniqueName="Retail" DataType="System.Decimal" DataFormatString="{0:#.##}">
                    </telerik:GridBoundColumn>
                    <telerik:GridCalculatedColumn HeaderText="Total Price" UniqueName="TotalPrice" DataType="System.Decimal" DataFormatString="{0:#.##}" DataFields="Quantity, Cost" Expression="{0}*{1}" FooterText="" Aggregate="Sum">
                    </telerik:GridCalculatedColumn>

                
                </Columns>
                  </MasterTableView>
              </telerik:RadGrid>  
    <br/>
    <center>
    <telerik:RadGrid ID="RadGrid2" runat="server"></telerik:RadGrid></center>  
    <br/>
<%--    Subtotal <asp:Label ID="lblSubtotal" runat="server" Text=""/><br/>
    Shipping <asp:Label ID="lblShipping" runat="server" Text=""/><br/>
    Discount <asp:Label ID="lblDiscount" runat="server" Text=""/><br/>
    Total <asp:Label ID="lblTotal" runat="server" Text=""/><br/>
    Check Required <asp:Label ID="lblCheckRequired" runat="server" Text=""/><br/>
    RFID Tags <asp:Label ID="lblRFIDTags" runat="server" Text=""/><br/>
    Order Type <asp:Label ID="lblOrderType" runat="server" Text=""/><br/>--%>
   <div style="font-size: small;"> Purchase Reason : <asp:Label ID="lblPurchaseReason" runat="server" Text=""/><br/>
       
        <div style="font-size: small;"> Sumbitted on : <asp:Label ID="submissionDate" runat="server" Text=""/><br/>
   </div> <br/>
   
                </div>
            </div>
         
        </div>
               <div id ="NotVisible" runat="server" Visible="True">
             <h1><center> Access Code Not Valid / PO doesn't Exist. Contact IT</center></h1>  
            </div>
        </div>
    </form>
</body>
</html>
