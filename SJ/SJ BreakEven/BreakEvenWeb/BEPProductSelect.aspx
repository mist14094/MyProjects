<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BEPProductSelect.aspx.cs" Inherits="BEPProduct" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Chart"
    TagPrefix="wijmo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Break Even | Dashboard</title>
    <link rel="stylesheet" type="text/css"         href="css/960.css" />
    <link rel="stylesheet" type="text/css"         href="css/reset.css" />
    <link rel="stylesheet" type="text/css"         href="css/text.css" />
    <link rel="stylesheet" type="text/css"         href="css/blue.css" />
    <link type="text/css"         href="css/smoothness/ui.css"         rel="stylesheet" />
    <script type="text/javascript"         src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript"         src="js/blend/jquery.blend.js"></script>
    <script type="text/javascript"         src="js/ui.core.js"></script>
    <script type="text/javascript"         src="js/ui.sortable.js"></script>
    <script type="text/javascript"         src="js/ui.dialog.js"></script>
    <script type="text/javascript"         src="js/ui.datepicker.js"></script>
    <script type="text/javascript"         src="js/effects.js"></script>
    <script type="text/javascript"         src="js/flot/jquery.flot.pack.js"></script>
    <script id="source" language="javascript" type="text/javascript"         src="js/graphs.js"></script>
    
     <style type="text/css">
        .WindowsStyle .ajax__combobox_inputcontainer
        {
            

        }
        .WindowsStyle .ajax__combobox_textboxcontainer
        {
            width:100%;
        }
        .WindowsStyle .ajax__combobox_textboxcontainer input
        {
            width:100%;
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
            width:300px;
        }
        .WindowsStyle .ajax__combobox_itemlist li
        {
            white-space: nowrap;
            width: 300px;
            padding: 0 3px 0 2px;
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
                        <span class="inner content">Break Even</span></span></a></li>
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
                        <li><a href="#" ><span><div style="color:White;">Purchase Order </div></span></a></li>
                        <li><a href="#" ><span><div style="color:White;">Vendor </div>l</span></a></li>
                        <li><a href="#" ><span><div style="color:White;">Store </div></span></a></li>
                        <li><a href="#" ><span><div style="color:White;">Enterprise </div></span></a></li>
                        <li><a href="#" ><span><div style="color:White;">Custom</div></span></a></li>
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
                    Break Even - 
                    Product Select</h1>
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
                         <asp:UpdatePanel runat="server" ID="asd">
                        <ContentTemplate>
                            
                 <table >
            <tr>
                <td >

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    &nbsp;</td>
                <td >

                    
                    Enter the UPC&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td >
 
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"  
                        Width="300px" onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
                    </asp:DropDownList>    
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;<asp:Button ID="Button1" runat="server" Text="Select" 
                        onclick="Button1_Click1" />
                    &nbsp;</td>
            </tr>
                     <tr>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td>
                             &nbsp;</td>
                         <td>
                             SKU</td>
                         <td>
                             <asp:Label ID="lblSKU" runat="server"></asp:Label>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td>
                             &nbsp;</td>
                         <td>
                             Description</td>
                         <td>
                             <asp:Label ID="lblDesc" runat="server"></asp:Label>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td>
                             &nbsp;</td>
                         <td style=" vertical-align:top;">
                            </td>
                         <td><br />
                             <asp:Image ID="Image1" runat="server"  Height="250px" Width="250px" />
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        <br />
                    <br /><br />
                    <br />
                </div>
            </div>
            <div class="clear">
            </div>
            <!-- END CONTENT-->
        </div>
        <div class="clear">
        </div>
        <!-- This contains the hidden content for modal box calls -->
        <div class='hidden'>
            <div id="inline_example1" title="This is a modal box" style='padding: 10px; background: #fff;'>
                <p>
                    <strong>This content comes from a hidden element on this page.</strong></p>
                <p>
                    <strong>Try testing yourself!</strong></p>
                <p>
                    You can call as many dialogs you want with jQuery UI.</p>
            </div>
        </div>
    </div>
    <div class="container_16" id="footer">
        ©2013 KeyTone Technologies - All rights reserved</div>
    </form>
    
</body>
</html>
