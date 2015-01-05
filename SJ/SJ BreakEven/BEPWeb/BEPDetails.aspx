<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="BEPGrid.aspx.cs" Inherits="BEPWeb.BEPGrid" %>--%>

<%@ Page Title="Break Even Details" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="BEPDetails.aspx.cs" Inherits="BEPWeb.BEPDetails" %>

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
    <style>
        .tooltip
        {
            outline: none;
        }
        .tooltip strong
        {
            line-height: 30px;
        }
        .tooltip:hover
        {
            text-decoration: none;
        }
        .tooltip span
        {
            z-index: 10;
            display: none;
            padding: 14px 20px;
            margin-top: -30px;
            margin-left: 28px;
            width: 240px;
            line-height: 16px;
        }
        .tooltip:hover span
        {
            display: inline;
            position: absolute;
            color: #111;
            border: 1px solid #DCA;
            background: #fffAF0;
        }
        .callout
        {
            z-index: 20;
            position: absolute;
            top: 30px;
            border: 0;
            left: -12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
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
        <div class="ControlsCenter">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="asd">
                <ProgressTemplate>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="UpdateProgress1"
                PopupControlID="Panel1" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="modalExtender">
                <img alt="Processing" src="~/Images/ajax-loader.gif" />
                <center>
                    <h3>
                        LOADING</h3>
                </center>
            </asp:Panel>
        </div>
        <asp:UpdatePanel runat="server" ID="asd">
            <ContentTemplate>
                <table class="tableStyle" width="100%">
                    <tr>
                        <td colspan="5" class="cellStyle" style='font-size: large; width: 30'>
                            &nbsp; &nbsp; Break Even Details
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
                        <td class="fieldCaption">
                            Last Sale Date
                        </td>
                        <td>
                            <asp:Label ID="lblLastSaleDate" runat="server" Text="" />
                        </td>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" class="sectionHeaderStyle" style='width: 30'>
                            &nbsp; &nbsp; Chart
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Chart ID="chProductDetails" runat="server" Width="900px" Height="600px">
                                <Series>
                                    <asp:Series IsValueShownAsLabel="true" IsVisibleInLegend="false" ChartType="line"
                                        BorderWidth="2" MarkerSize="6" MarkerStyle="Square">
                                        <SmartLabelStyle AllowOutsidePlotArea="Yes" IsMarkerOverlappingAllowed="true" />
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="beChartArea" BackColor="WhiteSmoke" Area3DStyle-Enable3D="false">
                                        <AxisY Title="Sale/Cost" LabelAutoFitMaxFontSize="10">
                                        </AxisY>
                                        <AxisX Title="Items" IsLabelAutoFit="True" LabelAutoFitMaxFontSize="10">
                                            <MajorGrid Enabled="False" />
                                            <LabelStyle Angle="0" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" class="sectionHeaderStyle" style='width: 30'>
                            &nbsp; &nbsp; What - Is Analysis
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldCaption">
                            Unit Product Cost ($)
                        </td>
                        <td>
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtProductCost" runat="server" AutoPostBack="true" OnTextChanged="txtProductCost_TextChanged"></asp:TextBox>
                                            <ajaxToolkit:SliderExtender ID="slExtProductCost" runat="server" TargetControlID="txtProductCost"
                                                Decimals="2" BoundControlID="txtProductCostDisplay" Enabled="True" RailCssClass="slider_rail"
                                                HandleImageUrl="~/Images/button-blue.png">
                                            </ajaxToolkit:SliderExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProductCostDisplay" Width="50px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td>
                        </td>
                        <td class="fieldCaption">
                            Potential Revenue ($)
                        </td>
                        <td>
                            <asp:Label ID="lblPotentialRevenue" runat="server" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldCaption">
                            Variable Cost (%)
                        </td>
                        <td>
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtVariableCost" runat="server" AutoPostBack="true" OnTextChanged="txtVariableCost_TextChanged"></asp:TextBox>
                                            <ajaxToolkit:SliderExtender ID="slVariableCost" runat="server" TargetControlID="txtVariableCost"
                                                Decimals="2" BoundControlID="txtVariableCostDisplay" Enabled="True" RailCssClass="slider_rail"
                                                HandleImageUrl="~/Images/button-blue.png">
                                            </ajaxToolkit:SliderExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtVariableCostDisplay" Width="50px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td>
                        </td>
                        <td class="fieldCaption">
                            Potential Profit ($)
                        </td>
                        <td>
                            <asp:Label ID="lblPotentialProfit" runat="server" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldCaption">
                            Total Products
                        </td>
                        <td>
                            <div style="float: left;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTotalProducts" runat="server" AutoPostBack="true" OnTextChanged="txtTotalProducts_TextChanged"></asp:TextBox>
                                            <ajaxToolkit:SliderExtender ID="slTotalProducts" runat="server" TargetControlID="txtTotalProducts"
                                                BoundControlID="txtTotalProductsDisplay" Enabled="True" RailCssClass="slider_rail"
                                                HandleImageUrl="~/Images/button-blue.png">
                                            </ajaxToolkit:SliderExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalProductsDisplay" Width="50px" runat="server" AutoPostBack="true"
                                                OnTextChanged="txtTotalProducts_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td>
                        </td>
                        <td class="fieldCaption">
                            Break Even Quantity
                        </td>
                        <td>
                            <asp:Label ID="lblBreakEvenQty" runat="server" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldCaption">
                            Suggested Retail ($)
                        </td>
                        <td>
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtSuggestRetail" runat="server" AutoPostBack="true" OnTextChanged="txtSuggestRetail_TextChanged"></asp:TextBox>
                                            <ajaxToolkit:SliderExtender ID="slSuggestRetail" runat="server" TargetControlID="txtSuggestRetail"
                                                BoundControlID="txtSuggestRetailDisplay" Decimals="2" Enabled="True" RailCssClass="slider_rail"
                                                HandleImageUrl="~/Images/button-blue.png">
                                            </ajaxToolkit:SliderExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSuggestRetailDisplay" Width="50px" runat="server" AutoPostBack="true"
                                                OnTextChanged="txtSuggestRetail_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td>
                        </td>
                        <td class="fieldCaption">
                            Break Even Point
                        </td>
                        <td>
                            <asp:Label ID="lblBreakEvenPnt" runat="server" Text="" />
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
                            <asp:LinkButton ID="lkSalesTrend" Text="Sales Trend" runat="server"></asp:LinkButton>
                            <br />
                            <asp:LinkButton ID="lkPODetails" Text="PO Details" runat="server"></asp:LinkButton>
                            <br />
                            <asp:LinkButton ID="lkSalesDetail" Text="Sales Detail" runat="server"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
