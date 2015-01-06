<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewMenthol.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menthol Sales Report</title>
    <link rel="stylesheet" type="text/css" href="//www.shieldui.com/shared/components/latest/css/shieldui-all.min.css" />
    <link rel="stylesheet" type="text/css" href="//www.shieldui.com/shared/components/latest/css/light-mint/all.min.css" />
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/shieldui-all.min.js"></script>
    <link rel="apple-touch-icon" href="Icon/57.png" />    
    <link rel="apple-touch-icon" sizes="72x72" href="Icon/72.png" />
    <link rel="apple-touch-icon" sizes="114x114"  href="Icon/114.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>

    
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
    <form id="form1" runat="server">
    <div>
    
    <shield:ShieldChart ID="ShieldChart1" Width="100%" Height="900px" runat="server"
    CssClass="chart" >
   
</shield:ShieldChart>


    </div>
    </form>
</body>
</html>
