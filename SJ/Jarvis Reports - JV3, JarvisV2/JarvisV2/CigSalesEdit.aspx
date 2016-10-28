<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CigSalesEdit.aspx.cs" Inherits="CigSales" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cig. Sales Report</title>
    <link rel="stylesheet" type="text/css" href="//www.shieldui.com/shared/components/latest/css/shieldui-all.min.css" />
    <link rel="stylesheet" type="text/css" href="//www.shieldui.com/shared/components/latest/css/light-mint/all.min.css" />
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/shieldui-all.min.js"></script>
    <link rel="apple-touch-icon" href="Icon/57.png" />    
    <link rel="apple-touch-icon" sizes="72x72" href="Icon/72.png" />
    <link rel="apple-touch-icon" sizes="114x114"  href="Icon/114.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <style type="text/css">
        .style1
        {
            width: 100%;
            text-align:center;
        }
    </style>

    
<script>
    var page = document.getElementById('page'),
    ua = navigator.userAgent,
    iphone = ~ua.indexOf('iPhone') || ~ua.indexOf('iPod'),
    ipad = ~ua.indexOf('iPad'),
    ios = iphone || ipad,
    // Detect if this is running as a fullscreen app from the homescreen
    fullscreen = window.navigator.standalone,
    android = ~ua.indexOf('Android'),
    lastWidth = 0;

    if (android) {
        // Android's browser adds the scroll position to the innerHeight, just to
        // make this really fucking difficult. Thus, once we are scrolled, the
        // page height value needs to be corrected in case the page is loaded
        // when already scrolled down. The pageYOffset is of no use, since it always
        // returns 0 while the address bar is displayed.
        window.onscroll = function () {
            page.style.height = window.innerHeight + 'px'
        }
    }
    var setupScroll = window.onload = function () {
        // Start out by adding the height of the location bar to the width, so that
        // we can scroll past it
        if (ios) {
            // iOS reliably returns the innerWindow size for documentElement.clientHeight
            // but window.innerHeight is sometimes the wrong value after rotating
            // the orientation
            var height = document.documentElement.clientHeight;
            // Only add extra padding to the height on iphone / ipod, since the ipad
            // browser doesn't scroll off the location bar.
            if (iphone && !fullscreen) height += 60;
            page.style.height = height + 'px';
        } else if (android) {
            // The stock Android browser has a location bar height of 56 pixels, but
            // this very likely could be broken in other Android browsers.
            page.style.height = (window.innerHeight + 56) + 'px'
        }
        // Scroll after a timeout, since iOS will scroll to the top of the page
        // after it fires the onload event
        setTimeout(scrollTo, 0, 0, 1);
    };
    (window.onresize = function () {
        var pageWidth = page.offsetWidth;
        // Android doesn't support orientation change, so check for when the width
        // changes to figure out when the orientation changes
        if (lastWidth == pageWidth) return;
        lastWidth = pageWidth;
        setupScroll();
    })();
</script>
</head>
<body>
    <form id="form1" runat="server" style="background-color:White">
    <div>
        
 <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
        Select Store

        <asp:DropDownList ID="ddlStoreID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStoreID_SelectedIndexChanged"></asp:DropDownList>
   &nbsp; &nbsp;From Date
        <asp:TextBox ID="StartDate" runat="server" AutoPostBack="True"></asp:TextBox>
          
            <ajaxToolkit:CalendarExtender ID="StartDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="StartDate">
            </ajaxToolkit:CalendarExtender>
      &nbsp;   &nbsp;End Date
           <asp:TextBox ID="EndDate" runat="server" AutoPostBack="True"></asp:TextBox>

            <ajaxToolkit:CalendarExtender ID="EndDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="EndDate">
            </ajaxToolkit:CalendarExtender>

        &nbsp;&nbsp; <asp:ImageButton ID="imbDownloadExcel" Height="24px" runat="server" ImageUrl="~/images/Excel.gif" OnClick="imbDownloadExcel_Click"  />

        </center>
        
        <div id="Chart" runat="server" Visible="True">
&nbsp;<shield:ShieldChart ID="ShieldChart1" Width="100%" Height="900px" runat="server"
    CssClass="chart" >
   
</shield:ShieldChart><br/><b>
       <div style="font-family:Segoe UI, Tahoma, Verdana, sans-serif;font-size:12px;cursor:pointer;color:#3E576F;fill:#3E576F;">
            Brand&nbsp;  <asp:DropDownList ID="ddlBrand" runat="server" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"></asp:DropDownList>
            Style&nbsp;   <asp:DropDownList ID="ddlStyle" runat="server" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged"></asp:DropDownList>
            Box/Soft&nbsp;   <asp:DropDownList ID="ddlBS" runat="server" OnSelectedIndexChanged="ddlBS_SelectedIndexChanged"></asp:DropDownList>
            King/100&nbsp;   <asp:DropDownList ID="ddlK100" runat="server" OnSelectedIndexChanged="ddlK100_SelectedIndexChanged"></asp:DropDownList>
            Menthol&nbsp;   <asp:DropDownList ID="ddlMenthol" runat="server" OnSelectedIndexChanged="ddlMenthol_SelectedIndexChanged"></asp:DropDownList>
            Canadian&nbsp;   <asp:DropDownList ID="ddlCanadian" runat="server" OnSelectedIndexChanged="ddlCanadian_SelectedIndexChanged"></asp:DropDownList>
            Non-Filter&nbsp;   <asp:DropDownList ID="ddlNonFilter" runat="server" OnSelectedIndexChanged="ddlNonFilter_SelectedIndexChanged"></asp:DropDownList>
      </div> </b>
            <div style="font-family:Segoe UI, Tahoma, Verdana, sans-serif;font-size:12px;cursor:pointer;color:#3E576F;fill:#3E576F;">
                
<b>
              Trend  <asp:CheckBox ID="chkTrend" runat="server" AutoPostBack="True" OnCheckedChanged="chkTrend_CheckedChanged" Text="Yes" />
                
    &nbsp;|&nbsp;Days  <asp:DropDownList ID="ddlTotalDays" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTotalDays_SelectedIndexChanged">
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                    <asp:ListItem>60</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>90</asp:ListItem>
                </asp:DropDownList>
                
    </b>
            </div>
            
            <br />
            <asp:Panel ID="Panel2" runat="server" CssClass="collapsePanelHeader" Height="30px">
            

            <div style="padding:5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;">Expand</div>
                <div style="float: left; margin-left: 20px;">
                    <asp:Label ID="Label1" runat="server">(Show Details...)</asp:Label>
                </div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </div>
            </div>
        </asp:Panel>
        <div >
        <asp:Panel ID="Panel1" runat="server" CssClass="collapsePanel"  >
            <br />
            <p>

                <asp:Button ID="Button1" runat="server" Text="Apply" onclick="Button1_Click" Width="149px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text="Reset"  Width="149px" OnClick="Button2_Click" />

                <asp:CheckBoxList ID="CheckBoxList1" runat="server" >
                </asp:CheckBoxList>
               

               
            </p>
        </asp:Panel></div>
        
          
        
        </div>
    <ajaxToolkit:CollapsiblePanelExtender ID="PanelCollapse" runat="Server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
     
        
        Collapsed="False"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
        SkinID="CollapsiblePanelDemo" />
</div>


    


    </form>
</body>
</html>
