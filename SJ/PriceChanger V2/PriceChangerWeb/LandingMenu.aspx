<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingMenu.aspx.cs" Inherits="PriceChangerWeb.LandingMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Product Price Changer and Categorization.</title>
    <meta http-equiv="Content-Type" content="application/xhtml+xml; charset=utf-8" />
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            line-height: 1.5em;
            background: #EAEAEA;
        }

        b {
            font-size: 110%;
        }

        em {
            color: red;
        }

        #topsection {
            background: #EAEAEA;
            height: 90px; /*Height of top section*/
        }

            #topsection h1 {
                margin: 0;
                padding-top: 15px;
            text-align: center;
        }

        #contentwrapper {
            float: left;
            width: 100%;
        }

        #contentcolumn {
           
        }

        #leftcolumn {
            float: left;
            width: 25%; /*Width of left column in percentage*/
            margin-left: -100%;
            background: #EAEAEA;
        }

        #rightcolumn {
            float: left;
            width: 200px; /*Width of right column in pixels*/
            margin-left: -200px; /*Set margin to -(RightColumnWidth)*/
            background: #EAEAEA;
        }

        #footer {

           width:100%;
	height:50px;
	position:absolute;
	bottom:0;
	left:0;
	background:#553479;
   
            text-align: center;
        }

            #footer a {
                color: #EAEAEA;
            }

        .innertube {
            margin: 10px; /*Margins for inner DIV inside each column (to provide padding)*/
            margin-top: 0;
            
        }

        /* ####### responsive layout CSS ####### */

        @media (max-width: 840px) { /* 1st level responsive layout break point- drop right column down*/

            #leftcolumn {
                margin-left: -100%;
            }

            #rightcolumn {
                float: none;
                width: 100%;
                margin-left: 0;
                clear: both;
            }

            #contentcolumn {
                margin-right: 0;
            }
        }

        @media (max-width: 600px) { /* 2nd level responsive layout break point- drop left column down */
            #leftcolumn {
                float: none;
                width: 100%;
                clear: both;
                margin-left: 0;
            }

            #contentcolumn {
                margin-left: 0;
            }
        }
        .auto-style1 {
            font-size: large;
            color: white;
        }
    </style>

   

</head>
<body>
    <form id="form1" runat="server">
        
<div id="maincontainer">

<div id="topsection"><div class="innertube"><h2 style="text-align: center">Product Retail & Categorization</h2></div></div>

<div id="contentwrapper">
<div id="contentcolumn">
<div class="innertube">
    
    <script type="text/javascript">filltext(45)</script><center>
    <asp:Button ID="btnPriceChanger" runat="server" Text="Price Changer" Height="75px" Width="350px" Font-Size="X-Large" OnClick="btnPriceChanger_Click" /><br/>
    <br/>
        <asp:Button ID="btnProductCatg" runat="server" Text="Product Catagory" Height="75px" Width="350px" Font-Size="X-Large" OnClick="btnProductCatg_Click" /></center>

    <br />
    <br />
</div>
</div>
</div>

<div id="leftcolumn">
<div class="innertube"><b></b> <script type="text/javascript">filltext(20)</script></div>
</div>

<div id="rightcolumn">
<div class="innertube"><b></b> <script type="text/javascript">filltext(15)</script></div>
</div>
    
    <div id="footer" class="auto-style1"><strong><br/>© Smokin Joes</strong></div>

</div>

    </form>
</body>
</html>
