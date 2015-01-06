<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouponSales.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Coupon Sales Report</title>
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
       
    <shield:ShieldChart ID="ShieldChart1" Width="100%" Height="900px" runat="server"
    CssClass="chart" >
   
</shield:ShieldChart>
       <div style="font-family:Segoe UI, Tahoma, Verdana, sans-serif;font-size:12px;cursor:pointer;color:#3E576F;fill:#3E576F;">
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

                <asp:Button ID="Button1" runat="server" Text="Apply" onclick="Button1_Click" />
               
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" >
                </asp:CheckBoxList>
               

               
            </p>
        </asp:Panel></div>
        
           <table class="style1">
               <tr>
                   <td>
                       &nbsp;</td>
                   <td>
                       <asp:HyperLink ID="HyperLink1" runat="server" 
                           NavigateUrl="~/CouponSales.aspx?selection=Stacked15Area&GraphType=Stack" 
                           ForeColor="Red" >$15 [Stacked area]</asp:HyperLink>
                   </td>
                   <td>
                       &nbsp;</td>
                   <td>
                       <asp:HyperLink ID="HyperLink2" runat="server"  ForeColor="Red"
                       NavigateUrl="~/CouponSales.aspx?selection=Stacked15Area&GraphType=Line" >$15 Current</asp:HyperLink>
                   </td>
                   <td>
                       &nbsp;</td>
                   <td>
                       <asp:HyperLink ID="HyperLink3" runat="server"   ForeColor="Red"
                       NavigateUrl="~/CouponSales.aspx?selection=CanStackedArea&GraphType=Stack" >CAN [Stacked]</asp:HyperLink>
                   </td>
                   <td>
                       &nbsp;</td>
                   <td>
                       <asp:HyperLink ID="HyperLink4" runat="server"   ForeColor="Red"
                       NavigateUrl="~/CouponSales.aspx?selection=CanStackedArea&GraphType=Line">Can [Line]</asp:HyperLink>
                   </td>
                   <td>
                       &nbsp;</td>
                   <td>
                       <asp:HyperLink ID="HyperLink5" runat="server"     ForeColor="Red"
                       NavigateUrl="~/CouponSales.aspx?selection=MentholSPPacked&GraphType=Stack">Menthol SP [Stacked]</asp:HyperLink>
                   </td>
                   <td>
                       &nbsp;</td>
               </tr>
           </table>
        
        </div>
    <ajaxToolkit:CollapsiblePanelExtender ID="PanelCollapse" runat="Server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
     
        
        Collapsed="True"
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
