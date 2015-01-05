<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BEPProduct.aspx.cs" Inherits="BEPProduct" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Chart"
    TagPrefix="wijmo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Break-Even | Dashboard</title>
    <link rel="stylesheet" type="text/css" href="css/960.css" />
    <link rel="stylesheet" type="text/css" href="css/reset.css" />
    <link rel="stylesheet" type="text/css" href="css/text.css" />
    <link rel="stylesheet" type="text/css" href="css/blue.css" />
    <link type="text/css" href="css/smoothness/ui.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="js/blend/jquery.blend.js"></script>
    <script type="text/javascript" src="js/ui.core.js"></script>
    <script type="text/javascript" src="js/ui.sortable.js"></script>
    <script type="text/javascript" src="js/ui.dialog.js"></script>
    <script type="text/javascript" src="js/ui.datepicker.js"></script>
    <script type="text/javascript" src="js/effects.js"></script>
    <script type="text/javascript" src="js/flot/jquery.flot.pack.js"></script>
    <script id="source" language="javascript" type="text/javascript" src="js/graphs.js"></script>
    <script type="text/javascript">
        function hintContent() {
            return this.y;
        }

        function PopupCenter(pageURL, title, w, h) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        } 
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
    <style type="text/css">
        .new
        {
            position: relative;
            background-color: Gray;
            width: 300px;
            height: 20px;
            border-radius: 15px;
        }
        .handle
        {
            position: relative;
            height: 33px;
            width: 30px;
        }
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .modalPopup
        {
            padding: 5px;
        }
        .modalExtender
        {
        }
        .style1
        {
            width: 100%;
        }
        .style2
        {
            color: White;
            width: 115px;
            height: 100px;
            background-image: url('images/back.png');
            background-repeat: no-repeat;
            vertical-align: middle;
        }
        .style2Red
        {
            color: White;
            width: 115px;
            height: 100px;
            background-image: url('images/backRed.png');
            background-repeat: no-repeat;
            vertical-align: middle;
        }
        .style2Green
        {
            color: White;
            width: 115px;
            height: 100px;
            background-image: url('images/backGreen.png');
            background-repeat: no-repeat;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container_16" id="wrapper">
        <div style="position: relative;">
        </div>
        <div class="grid_8" id="logo">
            Break - Even Analysis</div>
        <div class="grid_8">
            <div id="user_tools">
                <span>Welcome <a href="#">Admin </a>| <a href="#">Logout</a></span></div>
        </div>
        <div class="grid_16" id="header">
            <!-- MENU START -->
            <div id="menu">
                <ul class="group" id="menu_group_main">
                    <li class="item first" id="one"><a href="Search.aspx" class="main"><span
                        class="outer"><span class="inner dashboard">Dashboard</span></span></a></li>
                    <li class="item middle" id="two"><a href="#" class="main current"><span class="outer">
                        <span class="inner content">Break-Even</span></span></a></li>
                    <li class="item middle" id="three"><a href="#"><span class="outer"><span class="inner reports png">
                        Reports</span></span></a></li>
                    <li class="item middle" id="four"><a href="#" class="main"><span class="outer"><span
                        class="inner users">Sales</span></span></a></li>
                    <li class="item middle" id="five"><a href="#" class="main"><span class="outer"><span
                        class="inner media_library">Receiving</span></span></a></li>
                    <li class="item middle" id="six"><a href="#" class="main"><span class="outer"><span
                        class="inner event_manager">Inventory</span></span></a></li>
                    <li class="item middle" id="seven"><a href="#" class="main"><span class="outer"><span
                        class="inner newsletter">Overhead</span></span></a></li>
                    <li class="item last" id="eight"><a href="#" class="main"><span class="outer"><span
                        class="inner settings">Settings</span></span></a></li>
                </ul>
            </div>
            <!-- MENU END -->
        </div>
        <div class="grid_16">
            <!-- TABS START -->
            <div id="tabs">
                <div class="container">
                    <ul>
                        <li><a href="#" class="current"><span>Product Level</span></a></li>
                    </ul>
                </div>
            </div>
            <!-- TABS END -->
        </div>
        <!-- HIDDEN SUBMENU START -->
        <div class="grid_16" id="hidden_submenu">
            <ul class="more_menu">
                <li><a href="#">More link 1</a></li>
                <li><a href="#">More link 2</a></li>
                <li><a href="#">More link 3</a></li>
                <li><a href="#">More link 4</a></li>
            </ul>
            <ul class="more_menu">
                <li><a href="#">More link 5</a></li>
                <li><a href="#">More link 6</a></li>
                <li><a href="#">More link 7</a></li>
                <li><a href="#">More link 8</a></li>
            </ul>
            <ul class="more_menu">
                <li><a href="#">More link 9</a></li>
                <li><a href="#">More link 10</a></li>
                <li><a href="#">More link 11</a></li>
                <li><a href="#">More link 12</a></li>
            </ul>
        </div>
        <!-- HIDDEN SUBMENU END -->
        <!-- CONTENT START -->
        <div class="grid_16" id="content">
            <!--  TITLE START  -->
            <div class="grid_9">
                <table>
                    <tr>
                        <td style="width: 750px;">
                            <h1 class="dashboard" style="width: 750px;">
                                Break-Even : Product Level</h1>
                        </td>
                        <td style="width: 150px;">
                            <input onclick="window.location.reload()" type="button" value="Reset"></input>
                        </td>
                        <td style="width: 150px;">
                            <input type="button" onclick="printDiv('printableArea')" value="Print!" />
                        </td>
                    </tr>
                </table>
            </div>
            <!--RIGHT TEXT/CALENDAR-->
            <!--RIGHT TEXT/CALENDAR END-->
            <div class="clear">
            </div>
            <!--  TITLE END  -->
            <!-- #PORTLETS START -->
            <div id="portlets">
                <!-- FIRST SORTABLE COLUMN START -->
                <!-- FIRST SORTABLE COLUMN END -->
                <!-- SECOND SORTABLE COLUMN START -->
                <!--  SECOND SORTABLE COLUMN END -->
                <div class="clear">
                </div>
                <div>
                    <asp:ScriptManager ID="sd" runat="server">
                    </asp:ScriptManager>
                    <br />
                    <div id="printableArea">
                        <div class="ControlsCenter">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="asd">
                                <ProgressTemplate>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="UpdateProgress1"
                                PopupControlID="Panel1" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="Panel1" runat="server" CssClass="modalExtender">
                                <img alt="Processing" src="loading.gif" />
                                <center>
                                    <h3>
                                        LOADING</h3>
                                </center>
                            </asp:Panel>
                        </div>
                        <asp:UpdatePanel runat="server" ID="asd">
                            <ContentTemplate>
                                <table class="style1">
                                    <tr>
                                        <td class="style2">
                                            <center>
                                                UPC
                                                <br />
                                                <asp:Label ID="lblUPC" runat="server" Text=""></asp:Label></center>
                                        </td>
                                        <td class="style2">
                                            <center>
                                                SKU<br />
                                                <asp:Label ID="lblSKU" runat="server" Text=""></asp:Label></center>
                                        </td>
                                        <td class="style2">
                                            <center>
                                                <br />
                                                Description<br />
                                                <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label></center>
                                            <br />
                                        </td>
                                        <td class="style2">
                                            <center>
                                                &nbsp;Total Products<br />
                                                <asp:Label ID="lblReceivedQuantity" runat="server" Text=""></asp:Label>
                                            </center>
                                        </td>
                                        <td class="style2">
                                            <center>
                                                Quantity sold<br />
                                                <asp:Label ID="lblSold" runat="server" Text=""></asp:Label></center>
                                        </td>
                                        <td class="style2">
                                            <center>
                                                COGS<br />
                                                ($)
                                                <asp:Label ID="lblTotalcst" runat="server" Text=""></asp:Label></center>
                                        </td>
                                        <td class="style2">
                                            <center>
                                                Sales Realised<br />
                                                ($)
                                                <asp:Label ID="lblMoneyRealised" runat="server" Text=""></asp:Label>
                                                <br />
                                            </center>
                                        </td>
                                        <td class="style2" runat='server' id="tdProfit">
                                            <center>
                                                <asp:Label ID="lblZone" runat="server" Text=""></asp:Label></center>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp; <asp:Label ID="lblAvgCost" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblPurchaseSymbol" runat="server" Text=""></asp:Label>
                                &nbsp;|&nbsp;
                                
                                <asp:Label ID="lblAvg" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblSellingSymbol" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;
                                
                                <asp:Label ID="lblBreakEvn" runat="server" Text=""></asp:Label>
                                <br />
                                <br />
                                <wijmo:C1LineChart ID="C1LineChart1" runat="server" Width="900px">
                                    <SeriesTransition Duration="2000" />
                                    <Animation Duration="2000" />
                                    <Header Compass="North">
                                    </Header>
                                    <Footer Compass="South" Visible="False">
                                    </Footer>
                                    <Legend>
                                        <Size Height="3" Width="30" />
                                    </Legend>
                                    <Axis>
                                        <X>
                                            <GridMajor Visible="True">
                                            </GridMajor>
                                            <GridMinor Visible="False">
                                            </GridMinor>
                                        </X>
                                        <Y Compass="West" Visible="False">
                                            <Labels TextAlign="Center">
                                            </Labels>
                                            <GridMajor Visible="True">
                                            </GridMajor>
                                            <GridMinor Visible="False">
                                            </GridMinor>
                                        </Y>
                                    </Axis>
                                    <Hint OffsetY="-10">
                                        <Content Function="hintContent" />
                                    </Hint>
                                </wijmo:C1LineChart>
                                <table class="style1">
                                    <tr>
                                        <td colspan="8">
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px;">
                                            Unit Product Cost ($)
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtProductCost" runat="server" AutoPostBack="true" OnTextChanged="txtProductCost_TextChanged"></asp:TextBox>
                                            <asp:SliderExtender ID="txtProductCostSE" runat="server" Enabled="True" HandleImageUrl="~/Arrow.png"
                                                Maximum="20" Minimum="0" TargetControlID="txtProductCost" RailCssClass="new"
                                                TooltipText="Slider: value {0}. Please slide to change value." HandleCssClass="handle">
                                            </asp:SliderExtender>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 200px;">
                                            <asp:Label ID="lblProductCost" runat="server" Text=""></asp:Label>
                                            $
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 250px;">
                                            Potential Revenue ($)
                                        </td>
                                        <td style="width: 200px;">
                                            :
                                            <asp:Label ID="lblTotalCost" runat="server" Text=""></asp:Label>
                                            $
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Variable Cost
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtVariableCost" runat="server" AutoPostBack="true" OnTextChanged="txtVariableCost_TextChanged"></asp:TextBox>
                                            <asp:SliderExtender ID="txtVariableCostSliderExtender" runat="server" Enabled="True"
                                                HandleImageUrl="~/Arrow.png" Maximum="500" Minimum="0" TargetControlID="txtVariableCost"
                                                RailCssClass="new" HandleCssClass="handle">
                                            </asp:SliderExtender>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblVariableCost" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Potential Profit ($)
                                        </td>
                                        <td>
                                            :
                                            <asp:Label ID="lblProfitMade" runat="server" Text=""></asp:Label>
                                            $
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Total Products
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalProducts" runat="server" AutoPostBack="true" OnTextChanged="txtTotalProducts_TextChanged"></asp:TextBox>
                                            <asp:SliderExtender ID="txtTotalProductsSE" runat="server" Enabled="True" HandleImageUrl="~/Arrow.png"
                                                Maximum="500" Minimum="0" TargetControlID="txtTotalProducts" RailCssClass="new"
                                                HandleCssClass="handle">
                                            </asp:SliderExtender>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotalProducts" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Break-Even (Products)
                                        </td>
                                        <td>
                                            :&nbsp;<asp:Label ID="lblBEPProduct" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Suggested MSRP* ($)
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRetailPrice" runat="server" AutoPostBack="true" OnTextChanged="txtRetailPrice_TextChanged"
                                                Style="height: 22px"></asp:TextBox>
                                            <asp:SliderExtender ID="txtRetailPriceSE" runat="server" Enabled="True" HandleImageUrl="~/Arrow.png"
                                                Maximum="500" Minimum="0" TargetControlID="txtRetailPrice" RailCssClass="new"
                                                HandleCssClass="handle">
                                            </asp:SliderExtender>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRetailPrice" runat="server" Text=""></asp:Label>
                                            $
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Break-Even Point ($)
                                        </td>
                                        <td>
                                            :&nbsp;<asp:Label ID="lblBEPDollar" runat="server" Text=""></asp:Label>
                                            $
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <br />
                                            <br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <asp:Label ID="lblMoneyMade" runat="server" Text="" Visible="false"></asp:Label>
                                            &nbsp;<asp:Label ID="lblGenerated" runat="server" Visible="False"></asp:Label>
                                            <br />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                 &nbsp;<asp:HyperLink ID="SalesTrendHyper" runat="server" Target="_blank">Sales Trend</asp:HyperLink>
 &nbsp; &nbsp; &nbsp;
                                 <asp:HyperLink ID="SaleValues" runat="server" Target="_blank" Visible="false">Sales Details</asp:HyperLink>
                                  <asp:HyperLink ID="SaleValuesNew" runat="server" Target="_blank">Sales Details</asp:HyperLink>
                               &nbsp; &nbsp; &nbsp;
                                   <asp:HyperLink ID="PurchaseDetails" runat="server" Target="_blank">Purchase Details</asp:HyperLink>
                                <br />
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <!-- END CONTENT-->
        </div>
        <div class="clear">
        </div>
        <!-- This contains the hidden content for modal box calls -->
    </div>
    <div class="container_16" id="footer">
        ©2013 KeyTone Technologies - All rights reserved</div>
    </form>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(showPopup);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(hidePopup);

        function showPopup(sender, args) {
            var ModalControl = '<%= modalExtender.ClientID %>';
            $find(ModalControl).show();
        }

        function hidePopup(sender, args) {
            var ModalControl = '<%= modalExtender.ClientID %>';
            $find(ModalControl).hide();
        } 
    </script>
</body>
</html>
