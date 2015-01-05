<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SalesTrend.aspx.cs" Inherits="BEPWeb.SalesTrend" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script type="text/javascript">
     function printDiv(divName) {
         var printContents = document.getElementById(divName).innerHTML;
         var originalContents = document.body.innerHTML;

         document.body.innerHTML = printContents;

         window.print();

         document.body.innerHTML = originalContents;
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <ajaxToolkit:ToolkitScriptManager ID="SCManager" runat="server" ScriptMode="Release">
        </ajaxToolkit:ToolkitScriptManager>
        <table width='100%'>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td>
                    <input onclick="window.history.back().back()" type="button" value="Back">
                </td>
                <td colspan="3">
                </td>
                <td align="right">
                    <input onclick="window.location.reload()" type="button" value="Reset"></input><input
                        type="button" onclick="printDiv('printableArea')" value="Print!" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
                <td colspan="5">
                </td>
            </tr>
        </table>
    </div>
    <div id="printableArea">
        <table class="tableStyle" width="100%">
            <tr>
                <td colspan="5" class="cellStyle" style='font-size: large; width: 30'>
                    &nbsp; &nbsp; Sales Trend
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="5" class="sectionHeaderStyle" style='width: 30'>
                    &nbsp; &nbsp; Product Detail
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    UPC
                </td>
                <td>
                    <asp:Label ID="lblUPC" runat="server" Text=""></asp:Label>
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    SKU
                </td>
                <td>
                    <asp:Label ID="lblSKU" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Stock Code
                </td>
                <td>
                    <asp:Label ID="lblStkCode" runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Vendor
                </td>
                <td>
                    <asp:Label ID="lblVendor" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Description
                </td>
                <td colspan="4">
                    <asp:Label ID="lblDesc" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td colspan="5"><hr />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Total COGS
                </td>
                <td>
                    <asp:Label ID="lblTotCOGS" runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Quantity Received
                </td>
                <td>
                    <asp:Label ID="lblQtyRcvd" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Total Sales
                </td>
                <td>
                    <asp:Label ID="lblTotSale" runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Quantity Sold
                </td>
                <td>
                    <asp:Label ID="lblQtySold" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Current Status
                </td>
                <td id="tdStatus" runat="server">
                    <asp:Label ID="lblStatus" CssClass="fieldCaption" runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Profit Margin
                </td>
                <td>
                    <asp:Label ID="lblMargin" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Avg COGS
                </td>
                <td>
                    <asp:Label ID="lblAvgCOGS" runat="server" Text="" /><asp:Label ID="lblAvgCOGSSym"
                        runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Avg Sell Price
                </td>
                <td>
                    <asp:Label ID="lblAvgSellPrice" runat="server" Text="" /><asp:Label ID="lblAvgSellPriceSym"
                        runat="server" Text="" />
                </td>
            </tr>
           
               
                
            
            
            <tr>
                <td colspan="5" class="sectionHeaderStyle" style='width: 30'>
                    &nbsp; &nbsp; Sales Summary
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Last Sale Date
                </td>
                <td>
                    <asp:Label ID="lblLastSaleDate" runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Most Selling Store
                </td>
                <td>
                    <asp:Label ID="lblStoreName" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Min Price
                </td>
                <td>
                    <asp:Label ID="lblMinPrice" runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Max Price
                </td>
                <td>
                    <asp:Label ID="lblMaxPrice" runat="server" Text="" />
                </td>
            </tr>
             
            <tr>
                <td colspan="5" class="sectionHeaderStyle" style='width: 30'>
                    &nbsp; &nbsp; Chart
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" 
                        ActiveTabIndex="1">
                        <ajaxToolkit:TabPanel runat="server" ID="tabPnlSaleTrend" HeaderText="Sales by Date"
                            Enabled="true" ScrollBars="Auto" OnDemandMode="Once">
                            <ContentTemplate>
                                <asp:Chart ID="chSaleTrends" runat="server" Width="900px" Height="600px" 
                                   >
                                    <ChartAreas>
                                        <asp:ChartArea Name="beChartArea" BackColor="WhiteSmoke">
                                            <AxisY Title="No of Items Sold">
                                            </AxisY>
                                            <AxisX Title="Date">
                                                <MajorGrid Enabled="False" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel runat="server" ID="tabPnlSaleStore" HeaderText="Sales by Store"
                            Enabled="true" ScrollBars="Auto" OnDemandMode="Once">
                            <ContentTemplate>
                                <asp:Chart ID="chSalesStorePie" runat="server" Width="900px" Height="600px" >
                                    <Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BackColor="WhiteSmoke" Area3DStyle-Enable3D="true"
                                            Area3DStyle-Inclination="60">
                                            <Area3DStyle Enable3D="True" Inclination="60"></Area3DStyle>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Legends>
                                        <asp:Legend Alignment="Center" Docking="Right" IsTextAutoFit="true" Name="Default"
                                            LegendStyle="Column" Font="Arial Narrow, 6pt" />
                                    </Legends>
                                </asp:Chart>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
             
                    <tr>
                        <td colspan="5" class="sectionHeaderStyle" style='width: 30'>
                            &nbsp; &nbsp; Other Links
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <asp:LinkButton id="lkBEPDetails" Text= "BreakEven Details"  runat ="server"></asp:LinkButton>
                        <br />
                           <asp:LinkButton id="lkPODetails" Text= "PO Details" runat ="server"></asp:LinkButton>
                        <br />
                           <asp:LinkButton id="lkSalesDetail" Text= "Sales Detail"  runat ="server"></asp:LinkButton>
                        </td>
                    </tr>
        </table>
    </div>
</asp:Content>
