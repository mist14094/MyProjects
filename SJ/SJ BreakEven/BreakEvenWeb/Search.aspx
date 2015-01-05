<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="BEPProduct" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1ComboBox"
    TagPrefix="wijmo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Chart"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1AutoComplete"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView"
    TagPrefix="wijmo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <style type="text/css">
        .WindowsStyle .ajax__combobox_inputcontainer
        {
        }
        .WindowsStyle .ajax__combobox_textboxcontainer
        {
            width: 100%;
        }
        .WindowsStyle .ajax__combobox_textboxcontainer input
        {
            width: 100%;
        }
        .WindowsStyle .ajax__combobox_buttoncontainer
        {
        }
        .WindowsStyle .ajax__combobox_buttoncontainer button
        {
            background-position: center;
            background-repeat: no-repeat;
            border-color: ButtonFace;
            height: 15px;
            width: 15px;
        }
        
        
        .WindowsStyle .ajax__combobox_itemlist
        {
            margin: 0px;
            padding: 0px;
            cursor: default;
            list-style-type: none;
            text-align: left;
            border: solid 1px ButtonShadow;
            background-color: Window;
            color: WindowText;
            width: 300px;
        }
        .WindowsStyle .ajax__combobox_itemlist li
        {
            white-space: nowrap;
            width: 300px;
            padding: 0 3px 0 2px;
        }
        .style1
        {
            width: 100%;
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
                    <li class="item first" id="one"><a href="#" class="main"><span class="outer"><span
                        class="inner dashboard">Dashboard</span></span></a></li>
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
                        <li><a href="#" class="current"><span>Product </span></a></li>
                        <li><a href="#"><span>
                            <div style="color: White;">
                                Purchase Order
                            </div>
                        </span></a></li>
                        <li><a href="#"><span>
                            <div style="color: White;">
                                Vendor
                            </div>
                            l</span></a></li>
                        <li><a href="#"><span>
                            <div style="color: White;">
                                Store
                            </div>
                        </span></a></li>
                        <li><a href="#"><span>
                            <div style="color: White;">
                                Enterprise
                            </div>
                        </span></a></li>
                        <li><a href="#"><span>
                            <div style="color: White;">
                                Custom</div>
                        </span></a></li>
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
                <h1 class="dashboard">
                    Break-Even : Product Select</h1>
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
                    <asp:ScriptManager ID="ScriptManager" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="MainPanel" runat="server">
                        <ContentTemplate>
                            <table class="style1">
                                <tr>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                    </td>
                                    <td style="width: 100px; vertical-align: middle;">
                                        Description :&nbsp;
                                    </td>
                                    <td style="width: 600px; vertical-align: middle;">
                                        <wijmo:C1AutoComplete ID="acDescription" runat="server" DataLabelField="ItemDesc"
                                            MinLength="2" LoadOnDemand="true" MaxCount="15" AutoPostBack="true" DataMember="DefaultView"
                                            DataValueField="ItemDesc" Width="600px" OnTextChanged="acDescription_TextChanged">
                                        </wijmo:C1AutoComplete>
                                    </td>
                                    <td style="width: 200px; vertical-align: middle;">
                                        &nbsp;&nbsp;
                                        <asp:Button Visible="false" ID="btnSearchDesc" runat="server" Text="Search by desc"
                                            Height="28px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 600px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 200px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 100px; vertical-align: middle;">
                                        UPC :&nbsp;
                                    </td>
                                    <td style="width: 600px; vertical-align: middle; z-index: 0;">
                                        <wijmo:C1ComboBox ID="cbUPC" runat="server" DropdownHeight="500" Width="600" AutoPostBack="True"
                                            DropdownWidth="900" OnSelectedIndexChanged="cbUPC_SelectedIndexChanged1" TriggerPosition="Left">
                                            <Columns>
                                                <wijmo:C1ComboBoxColumn Name="UPC" Width="120" />
                                                <wijmo:C1ComboBoxColumn Name="SKU" Width="120" />
                                                <wijmo:C1ComboBoxColumn Name="DESC" Width="400" />
                                                <wijmo:C1ComboBoxColumn Name="VENDOR" Width="260" />
                                            </Columns>
                                        </wijmo:C1ComboBox>
                                    </td>
                                    <td style="width: 200px; vertical-align: middle;">
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 600px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 200px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 100px; vertical-align: middle;">
                                        SKU :&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 600px; vertical-align: middle;">
                                        <wijmo:C1AutoComplete ID="acSKU" runat="server" DataLabelField="SKU" MinLength="9"
                                            LoadOnDemand="true" MaxCount="10" DataMember="DefaultView" DataValueField="SKU"
                                            Width="600px">
                                        </wijmo:C1AutoComplete>
                                    </td>
                                    <td style="width: 200px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 600px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 200px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 100px; vertical-align: middle;">
                                        Vendor :
                                    </td>
                                    <td style="width: 600px; vertical-align: middle;">
                                        <wijmo:C1ComboBox ID="cbVendor" runat="server" DropdownHeight="500" Width="600px"
                                            AutoPostBack="True" DropdownWidth="900" DataTextField="Vendor" DataValueField="Vendor"
                                            TriggerPosition="Left">
                                        </wijmo:C1ComboBox>
                                    </td>
                                    <td style="width: 200px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table class="style1">
                                <tr>
                                    <td style="width: 100px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 70px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 600px; vertical-align: middle;">
                                        <asp:Button ID="btnSearch" runat="server" Height="28px" Text="Search" OnClick="btnSearch_Click" />
                                        &nbsp;&nbsp;
                                        <input onclick="window.location.reload()" type="button" style="height: 28px;" value="Reset">
                                    </td>
                                    <td style="width: 200px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table class="style1">
                                <tr>
                                    <td style="width: 50px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="z-index: 1;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblRecordCount" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="width: 50px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="z-index: 1;">
                                        <wijmo:C1GridView ID="gvSearchResult" runat="server" AllowPaging="true" AllowSorting="true"
                                            AutogenerateColumns="false" CallbackSettings-Action="All" OnPageIndexChanging="gvSearchResult_PageIndexChanging"
                                            OnSorting="gvSearchResult_Sorting" PagerSettings-Mode="NumericFirstLast" PageSize="10"
                                            ShowRowHeader="true">
                                            <CallbackSettings Action="Sorting" />
                                            <Columns>
                                                <wijmo:C1HyperLinkField SortExpression="UPC" HeaderText="UPC" DataTextField="UPC"
                                                    DataNavigateUrlFields="PC_ID" DataNavigateUrlFormatString="~/BEPproduct.aspx?pid={0}"
                                                    Target="_parent">
                                                    <ItemStyle ForeColor="Blue" />
                                                </wijmo:C1HyperLinkField>
                                                <wijmo:C1HyperLinkField SortExpression="SKU" HeaderText="SKU" DataTextField="SKU"
                                                    DataNavigateUrlFields="PC_ID" DataNavigateUrlFormatString="~/BEPproduct.aspx?pid={0}"
                                                    Target="_parent">
                                                    <ItemStyle ForeColor="Blue" />
                                                </wijmo:C1HyperLinkField>
                                                <wijmo:C1HyperLinkField SortExpression="ItemDesc" HeaderText="ItemDesc" DataTextField="ItemDesc"
                                                    DataNavigateUrlFields="PC_ID" DataNavigateUrlFormatString="~/BEPproduct.aspx?pid={0}"
                                                    Target="_parent">
                                                    <ItemStyle ForeColor="Blue" />
                                                </wijmo:C1HyperLinkField>
                                                <wijmo:C1HyperLinkField SortExpression="Vendor" HeaderText="Vendor" DataTextField="Vendor"
                                                    DataNavigateUrlFields="PC_ID" DataNavigateUrlFormatString="~/BEPproduct.aspx?pid={0}"
                                                    Target="_parent">
                                                    <ItemStyle ForeColor="Blue" />
                                                </wijmo:C1HyperLinkField>
                                            </Columns>
                                        </wijmo:C1GridView>
                                    </td>
                                    <td style="width: 50px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="z-index: 1;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                    <td style="z-index: 1;">
                                        <asp:Label ID="lblGenerated" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                    <td style="width: 50px; vertical-align: middle;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <br />
                    &nbsp;<br />
                    <br />
                    <br />
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="container_16" id="footer">
        ©2013 KeyTone Technologies - All rights reserved</div>
    </form>
</body>
</html>
