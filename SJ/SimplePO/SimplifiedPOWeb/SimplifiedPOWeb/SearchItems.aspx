<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="SearchItems.aspx.cs" Inherits="SimplifiedPOWeb.SearchItems" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="qsf" Namespace="System.Windows.Forms" Assembly="System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="js\script.js" type="text/javascript"></script>
    <div>
          <style type="text/css"> 
        .rgRow td, .rgAltRow td, .rgHeader td, .rgFilterRow td 
        { 
            border-left: solid 1px black !important; 
        } 
    </style> 
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <div style="text-align: center;font-family:Segoe WP">
        Search Item by UPC, Stockcode, Description, Supplier or Customer Code     <asp:Label runat="server" ID="lblCompanyName"></asp:Label><br>
        <br>

               <asp:TextBox ID="txtSearch" runat="server" rows="2" cols="20"  style="height: 68px; width: 795px;" TextMode="MultiLine" Text="APIMA TEE NF"></asp:TextBox> 
        <br/>
            <br>
            <asp:Button ID="btnSearch" runat="server" Text="Search Information" OnClick="btnSearch_Click" />
        <br>
            <br>
              </div>
            <telerik:RadGrid ID="rgSearchItems" runat="server" AllowPaging="True" AllowMultiRowSelection="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" CellSpacing="-1" GridLines="Both"  SelectionMode="Extended" OnNeedDataSource="rgSearchItems_NeedDataSource">
              <ClientSettings > <Selecting AllowRowSelect="True">  </Selecting> <Resizing AllowColumnResize="True" ></Resizing></ClientSettings>
                <MasterTableView TableLayout="Fixed">
                    <Columns>
                        <telerik:GridBoundColumn FilterControlAltText="Filter StockCode column" UniqueName="StockCode" DataField="StockCode" HeaderText="Stock Code">
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn FilterControlAltText="Filter Description column" UniqueName="Description" DataField="Description" HeaderText ="Description">
                              <HeaderStyle Width="50%" />
                        </telerik:GridBoundColumn>
                                                  <telerik:GridBoundColumn FilterControlAltText="Filter LongDesc column" UniqueName="LongDesc" DataField="LongDesc" HeaderText="Long Desc.">
                        </telerik:GridBoundColumn>
                                                  <telerik:GridBoundColumn FilterControlAltText="Filter AlternateKey1 column" UniqueName="AlternateKey1" DataField="AlternateKey1" HeaderText="UPC">
                        </telerik:GridBoundColumn>
                                                  <telerik:GridBoundColumn FilterControlAltText="Filter UnitCost column" UniqueName="UnitCost" DataField="UnitCost" HeaderText="Unit Cost">
                        </telerik:GridBoundColumn>
                                                  <telerik:GridBoundColumn FilterControlAltText="Filter Supplier column" UniqueName="Supplier" DataField="Supplier" HeaderText="Supplier" >
                        </telerik:GridBoundColumn>

                    </Columns>
                </MasterTableView>
                
            </telerik:RadGrid>
       
      <b>  <asp:Label runat="server" ID="lblMessage" Text=""></asp:Label></b><br/> <br/>
        <asp:Label runat="server" ID="lblTotalQuantity" Text="Total Quantity"></asp:Label>
         <telerik:RadNumericTextBox runat="server" ID="txtTotalQuantity" Width="190px" Value="0" EmptyMessage="Enter units count" MinValue="1"  MaxValue="200" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>

      
          &nbsp;&nbsp;
      
          <asp:Button runat="server" ID="btnAddItems" Text="Add Items" OnClick="btnAddItems_Click"/>
        
            </div>


</asp:Content>
