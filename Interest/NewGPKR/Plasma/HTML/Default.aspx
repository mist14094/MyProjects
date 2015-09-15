<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8">
    <title>GPKR - Handmade Decoratives</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"> 

    <!-- Loading Bootstrap -->
    <link href="css/style.css" rel="stylesheet">
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/responsive.css" rel="stylesheet">

	<link rel="stylesheet" type="text/css" href="js/rs-plugin/css/settings.css" media="screen" />
	<link rel="stylesheet" type="text/css" href="js/fancybox/jquery.fancybox.css" media="screen" />

    <link rel="stylesheet" href="css/skins/default.css" data-name="skins">
    <link rel="stylesheet" type="text/css" href="css/switcher.css" media="screen" />


    <!-- Loading Flat UI -->
    <link href="css/flat-ui.css" rel="stylesheet">

    <link rel="shortcut icon" href="images/favicon.ico">
	
	<link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
	
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements. All other JS at the end of file. -->
    <!--[if lt IE 9]>
      <script src="js/html5shiv.js"></script>
    <![endif]-->


      <script>
          (function(i, s, o, g, r, a, m) {
              i['GoogleAnalyticsObject'] = r;
              i[r] = i[r] || function() {
                  (i[r].q = i[r].q || []).push(arguments)
              }, i[r].l = 1 * new Date();
              a = s.createElement(o),
                  m = s.getElementsByTagName(o)[0];
              a.async = 1;
              a.src = g;
              m.parentNode.insertBefore(a, m)
          })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

          ga('create', 'UA-67545998-1', 'auto');
          ga('send', 'pageview');

      </script>

      <script language="javascript">
    document.onmousedown = disableclick;
    status = "Welcome to GPKR - Handmade Decoratives";
    function disableclick(event) {
        if (event.button == 2) {
            alert(status);
            return false;
        }
    }
      </script>
</head>
<body runat="server">
    <form id="form1" runat="server">

         
     <!-- Preloader -->
    <div id="preloader">
        <div id="status"></div>
    </div>
    <header id="top" class="mTop">
        <div class="topHead">
            <div class="container">
                <div class="row">
                    <div class="topMenu">
                        <ul class="span12 topContact">
                            <li class="addresTop"><span class="icon-map-marker"></span>112, Mariyamman Koil Street, Kavarapattu, Chidambaram - 608102</li>
                            <li class="mailTop"><span class="icon-envelope"></span>gpkrdecoratives@gmail.com</li>
                            <li class="phoneTop"><span class="icon-phone"></span>(+91) 7708410945</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="headContent">
            <div class="container">
                <div class="row">
                    <div class="span4">
                        <div class="brand">
                            <a href=""><img src="images/logo.png" alt="Logo"></a>
                        </div>
                    </div>
                    <div class="span8">
                        <div class="menu" id="steak">
                            <nav>
                                <ul class="navMenu inline-list" id="nav">
                                    <li class="current"><a href="#top">Home</a></li>
                                    <li><a href="#about">About</a></li>
                                    <li><a href="#service">Service</a></li>
                                    <li><a href="#portfolio">Portfolio</a></li>
                                    <li><a href="#contact">Contact</a></li>
                                </ul>
                                <div class="clearfix"></div>
                            </nav>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <!--Slider-->
    <section class="revSlider">
        <!-- START REVOLUTION SLIDER 2.3.91 -->
        <div class="fullwidthbanner-container">
            <div class="fullwidthbanner">
                <ul>
                    <!-- SLIDEUP -->
                    <li data-transition="slide-Up" data-slotamount="7" data-thumb="img/revslider/transparent.png">
                        <img src="img/revslider/13.jpg" alt="sa">

                        <div class="tp-caption plasma_def lft" data-x="300" data-y="130" data-speed="700" data-start="1200" data-easing="easeOutExpo">
                            Can bring designs or characters to life
                        </div>
                        <div class="tp-caption plasma_inverse lfl" data-x="300" data-y="192" data-speed="600" data-start="1900" data-easing="easeOutBack">
                            unique and creative Three dimensional work of art
                        </div>

                        <div class="tp-caption plasma_white lfb" data-x="300" data-y="254" data-speed="500" data-start="2700" data-easing="easeOutBack">
                            100% client satisfaction
                        </div>

                        <div data-easing="easeOutExpo" data-start="3000" data-speed="2500" data-y="320" data-x="480" class="caption lfr vivera_text_def tp-caption start">
                            <a href="#" class="btn btn-huge btn-block btn-primary btn-embossed">Contact us!</a>
                        </div>
                        <div data-easing="easeOutExpo" data-start="3300" data-speed="2500" data-y="320" data-x="300" class="caption lfl vivera_text_def tp-caption start">
                            <a href="#" class="btn btn-huge btn-block btn-inverse btn-embossed">Best of all!</a>
                        </div>
                    </li>

                    <!-- BOX FADE -->
                    <li data-transition="boxfade" data-slotamount="7" data-thumb="img/revslider/transparent.png">
                        <img src="img/revslider/14.jpg" alt="sa">
                        <div class="tp-caption lfb" data-x="0" data-y="60" data-speed="1000" data-start="900" data-easing="easeOutExpo">
                            <img src="img/revslider/tablet1.png" alt="Poeple Slider">
                        </div>
                        <div class="tp-caption plasma_inverse lfr" data-x="600" data-y="150" data-speed="600" data-start="1100" data-easing="easeOutBack">
                            specializes in creating custom sculpture
                        </div>
                        <div class="tp-caption plasma_white lft" data-x="600" data-y="215" data-speed="500" data-start="1500" data-easing="easeOutBack">
                            restoration and display of historical characters and props
                        </div>
                        <div class="tp-caption plasma_inverse lfb" data-x="600" data-y="280" data-speed="700" data-start="1900" data-easing="easeOutExpo">
                            classical and realistic sculptures
                        </div>
                    </li>
                    <!-- RANDOM ROTATION -->
                    <!--<li data-transition="randomrotate" data-slotamount="6" data-thumb="img/revslider/transparent.png">
                        <img class="tp-caption lfl" src="img/revslider/2.jpg" alt="tiga">
                        <div class="caption large_black_text sft tp-caption start"  data-x="0" data-y="80" data-speed="700" data-start="1200" data-easing="easeOutBack">
                            VIDEO PRESENT YOUR
                        </div>

                        <div class="caption very_large_black_text sft tp-caption start"  data-x="0" data-y="120" data-speed="700" data-start="1800" data-easing="easeOutBack">PRODUCT & SERVICES
                        </div>

                        <div class="caption medium_black_text text sft tp-caption start"  data-x="0" data-y="170" data-speed="700" data-start="2300" data-easing="easeOutBack">	Play Youtube & Vimeo Videos
                        </div>
                        <div data-easing="easeOutExpo" data-start="2800" data-speed="900" data-y="220" data-x="0" class="caption lfl small_black_text tp-caption start" >
                            Lorem Ipsum which looks reasonable non charact eristic words,<br>making this the first true generato on the Internet uses a dictionary.<br/> Ipsum generators on the Internet tend to repeat predefined chunks<br>generators on the Internet tend to repeat predefined chunks as necessary,<br> humour randomise words which don't look even slightly believable!
                        </div>
                        <div data-easing="easeOutExpo" data-start="3300" data-speed="1000" data-y="360" data-x="0" class="caption lfb vivera_text_def tp-caption start" >
                            <a href="#" class="btn btn-large btn-block btn-primary btn-embossed">Try it now!</a>
                        </div>

                        <div class="caption lft boxshadow" data-x="600" data-y="90" data-speed="900" data-start="1300" data-easing="easeOutExpo">
                            <iframe src="http://player.vimeo.com/video/31240369?title=0&amp;byline=0&amp;portrait=0" width="500" height="320"></iframe>
                        </div>
                    </li>-->
                </ul>

            </div>
        </div>
        <!-- END REVOLUTION SLIDER -->
    </section>

    <!--Intro-->
    <section class="intro">
        <div class="container">
            <div class="row">
                <div class="span12">
                    <h4>Welcome to <span class="bold">GPKR</span>. <span class="colDefault">Eco Friendly</span> Handmade Decoratives</h4>
                    <p>GPKR HandMade Decoratives, a pioneer in the field of eco-friendly handmade decorative and expert for production, design and installations, incorporate party planners, event management, props and prop hire. GPKR was launched in 1990 as a creative showroom and marriage decorative company operating pan India. Our objective is to give our clients a unique, personal and professional service of the highest standard from concept to completion, delivering creative solutions to ensure absolute satisfaction. GPKR offer full in-house services with complete logistical support & back-up contingencies with designers, technicians & workshops on-site incorporating production, design, consultancy & installation - in full or in part.</p>
                    <!--DIVIDER-->
                    <div class="divider">
                        <div class="divLine"></div>
                        <div class="divImg">
                            <i class="icon fui-heart"></i>
                        </div>
                        <div class="divLine"></div>
                    </div>
                </div>
                <div class="span4">
                    <div class="tile">
                        <img class="tile-image" alt="Infinity-Loop@2x" src="images/icons/eco1.png">
                        <h3 class="tile-title">Eco friendly</h3>
                        <p>Say NO to Plastic, Plaster of Paris and Thermocol decorations. Plaster of paris and chemical-laced colors pollutes atmosphere.</p>
                        <!--a class="btn btn-small btn-primary btn-embossed" href="#fakelink">View More</a-->

                    </div>
                </div>

                <div class="span4">
                    <div class="tile">
                        <img class="tile-image" alt="Retina-Ready" src="images/icons/Bulb@2x.png">
                        <h3 class="tile-title">Creativity</h3>
                        <p>Creative reuse and recycle ideas turn ordinary objects into interesting and functional decor. </p>
                        <!--a class="btn btn-small btn-primary btn-embossed" href="#fakelink">View More</a-->

                    </div>
                </div>

                <div class="span4">
                    <div class="tile tile-hot">
                        <img class="tile-image" alt="Chat@2x" src="images/icons/exp.png">
                        <h3 class="tile-title">Experienced</h3>
                        <p>Our sculptors have decades of experience creating Custom sculpture from photos such as Statues Lifesized Characters, Environments Decor, Theme Props and Large Foam Sculptures Holiday Decor, even theming entire ROOMS</p>

                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--About-->
    <section id="about" class="about grey">
        <div class="container">
            <div class="row">
                <div class="span12">
                    <div class="titleHead">
                        <h3>About Us</h3>
                        <p>
                            We are creative and experienced to create highly unique sculptures, statues, life-sized characters and art for showrooms, shopping mall, theaters, businesses, trade shows, exhibition and more.
                            Based on your needs and interests, we can design and create busts, characters, props, signs and creatures, however large, for marketing, for your hotel, shops, business, collection, store, office, themed or haunted attraction for theme park.
                        </p>
                    </div>
                    <!--DIVIDER-->
                    <div class="divider">
                        <div class="divLine"></div>
                        <div class="divImg">
                            <i class="icon fui-user"></i>
                        </div>
                        <div class="divLine"></div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="span12">
                    <div class="flex-container">
                        <!-- flex slider starts-->
                        <div class="flexslider">
                            <ul class="slides round">
                                <li><img src="images/about/about-1.jpg" alt=" " /></li>
                                <li><img src="images/about/about-2.jpg" alt=" " /></li>
                                <li><img src="images/about/about-3.jpg" alt=" " /></li>
                                <li><img src="images/about/about-4.jpg" alt=" " /></li>
                            </ul>
                        </div>
                    </div><!-- flex slider ends-->
                    <p>Our carved clay and fabricated props, statues, oversized statues, stage sets, trade show booths, environments and more are realistic!  Our artists can create characters and props of all sizes!</p>
                    <div class="mrg-30"></div>
                    <div class="clearfix"></div>
                </div>
            </div> <!--./row-->
        </div>
    </section>
    <section id="service" class="about grey">
        <div class="container">
            <div class="row">
               
                    <div class="titleHead">
                        <h3>Our Services</h3></div>
                        <div class="span6 skill">
                            <div class="titleContent">
                                <h6><span>We undertake</span></h6>
                            </div>
                            <ul class="progress-skill-bar">
                                <li>
                                    <span class="lable">100%</span>
                                    <div class="progress">
                                        <div class="bar" data-value="100" role="progressbar">
                                            Show Room Decoration
                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <span class="lable">100%</span>
                                    <div class="progress">
                                        <div class="bar" data-value="100" role="progressbar">
                                            Indoor/Outdoor Decorations
                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <span class="lable">100%</span>
                                    <div class="progress">
                                        <div class="bar" data-value="100" role="progressbar">
                                            Shopping Mall Decorations
                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <span class="lable">100%</span>
                                    <div class="progress">
                                        <div class="bar" data-value="100" role="progressbar">
                                            Party Decorations
                                        </div>
                                    </div>
                                </li>

                            </ul>

                        </div>

                        <div class="span6 skill">
                            <div class="titleContent">
                                <h6><span>Our Speciality</span></h6>
                            </div>
                            <br />
                            <div class="description">
                                <p><span class="dropcap colDefault">GPKR</span> artists have a wide range of talents and can offer everything from portrait sculpture, custom fabricated statues, add signs, decor and theme props of all sizes or even entire themed showrooms and shopping mall.</p>

                                <p>
                                    <span class="colDefault">Hiring and Installation services...!!!</span>
                                </p>
                                <ul class="progress-skill-bar">

                                    <li>
                                        <span class="lable">100%</span>
                                        <div class="progress">
                                            <div class="bar" data-value="100" role="progressbar">
                                                Marriage Decorations
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <span class="lable">100%</span>
                                        <div class="progress">
                                            <div class="bar" data-value="100" role="progressbar">
                                                Sculpturing Art works and More...
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                   
                    <!--DIVIDER-->
                    <div class="divider">
                        <div class="divLine"></div>
                        <div class="divImg">
                            <i class="icon fui-user"></i>
                        </div>
                        <div class="divLine"></div>
                    </div>
             
            </div>
        </div>

    </section>
    
        <!--Portfolio-->
        <section id="portfolio" class="portfolio">
            <div class="container">
                <div class="row">
                    <div class="span12">
                        <div class="titleHead">
                            <h3>Our Portfolio</h3>
                            <p>A selected collection of our work for your view...</p>
                        </div>

                        <!--DIVIDER-->
                        <div class="divider">
                            <div class="divLine"></div>
                            <div class="divImg">
                                <i class="icon fui-image"></i>
                            </div>
                            <div class="divLine"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <!--begin isotope -->
                    <div class="isotope">
                        <!--begin portfolio filter -->
                        <ul id="filter" class="option-set clearfix">
                            <li data-filter="*" class="selected"><a href="#">All</a></li>
                            <li data-filter=".responsive"><a href="#">Statues</a></li>
                            <li data-filter=".mobile"><a href="#">Props</a></li>
                            <li data-filter=".branding"><a href="#">Sets</a></li>
                        </ul>
                        <!--end portfolio filter -->
                        <!--begin portfolio_list -->
                        <ul id="list" class="portfolio_list">
                            <!--begin span4 >
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/team-1.png" class="fancybox" data-rel="gallery1" title="Tolpis barbata (mariluzpicado)">
                                        <img src="images/photos/team-1.png" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Kathakali</h2>
                                    <span>Statue / Kerela </span>
                                </div>

                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image2.jpg" class="fancybox" data-rel="gallery1" title="Wedding Set">
                                        <img src="images/photos/image2.jpg" alt="Ancient Tamilans">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Wedding Set</h2>
                                    <span>Ancient Tamilan</span>
                                </div>
                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image3.jpg" class="fancybox" data-rel="gallery1" title="Christmas Decorations">
                                        <img src="images/photos/image3.jpg" alt="Christmas Collection">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Christmas Decorations</h2>
                                    <span></span>
                                </div>
                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4">
                                <div class="view view-first">
                                    <a href="images/photos/image4.jpg" class="fancybox" data-rel="gallery1" title="Avatars of Vishnu">
                                        <img src="images/photos/image4.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Avatars of Vishnu</h2>
                                    <span>Statues</span>
                                </div>
                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 branding">
                                <div class="view view-first">
                                    <a href="images/photos/image5.jpg" class="fancybox" data-rel="gallery1" title="Showroom Entrance Set">
                                        <img src="images/photos/image5.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Showroom</h2>
                                    <span>Set</span>
                                </div>
                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/image6.jpg" class="fancybox" data-rel="gallery1" title="Statue">
                                        <img src="images/photos/image6.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Statue</h2>
                                    <span>Ancient</span>
                                </div>
                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/image7.jpg" class="fancybox" data-rel="gallery1" title="Avatars of Vishnu">
                                        <img src="images/photos/image7.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Avatar of Vishnu</h2>
                                    <span>Statues</span>
                                </div>
                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/image8.jpg" class="fancybox" data-rel="gallery1" title="Avatars of Vishnu">
                                        <img src="images/photos/image8.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Avatars of Vishnu</h2>
                                    <span>Statues</span>
                                </div>
                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 Props">
                                <div class="view view-first">
                                    <a href="images/photos/image9.jpg" class="fancybox" data-rel="gallery1" title="Onam Decoration">
                                        <img src="images/photos/image9.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Onam Decorations</h2>
                                    <span>Kerela Elephants</span>
                                </div>
                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 branding">
                                <div class="view view-first">
                                    <a href="images/photos/image10.jpg" class="fancybox" data-rel="gallery1" title="Vintage Mandapam">
                                        <img src="images/photos/image10.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Vintage Mandapam</h2>
                                    <span>Entrance Set</span>
                                </div>
                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image11.jpg" class="fancybox" data-rel="gallery1" title="Avatars of Vishnu">
                                        <img src="images/photos/image11.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Avatars of Vishnu</h2>
                                    <span>Statues</span>
                                </div>
                            </li>
                            <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 branding">
                                <div class="view view-first">
                                    <a href="images/photos/image12.jpg" class="fancybox" data-rel="gallery1" title="Ancient Wedding Set">
                                        <img src="images/photos/image12.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Bride in Pallakku</h2>
                                    <span>Entrance Set</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 branding">
                                <div class="view view-first">
                                    <a href="images/photos/image13.jpg" class="fancybox" data-rel="gallery1" title="Christmas Set">
                                        <img src="images/photos/image13.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Christmas Set</h2>
                                    <span>Entrance Set</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image14.jpg" class="fancybox" data-rel="gallery1" title="Christmas Props">
                                        <img src="images/photos/image14.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Santa</h2>
                                    <span>Props</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image15.jpg" class="fancybox" data-rel="gallery1" title="Ancient Tamil">
                                        <img src="images/photos/image15.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Pongal Decoration</h2>
                                    <span>Props</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/image17.jpg" class="fancybox" data-rel="gallery1" title="Avatars of Vishnu">
                                        <img src="images/photos/image17.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Avatars of Vishnu</h2>
                                    <span>Statues</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/image16.jpg" class="fancybox" data-rel="gallery1" title="Amman Statue">
                                        <img src="images/photos/image16.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Amman Statue</h2>
                                    <span>Navaratri Decoration</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image22.jpg" class="fancybox" data-rel="gallery1" title="Laughing Buddha">
                                        <img src="images/photos/image22.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Laughing Buddha</h2>
                                    <span>Props</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image26.jpg" class="fancybox" data-rel="gallery1" title="Laughing Buddha">
                                        <img src="images/photos/image26.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Laughing Buddha</h2>
                                    <span>Statues</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image18.jpg" class="fancybox" data-rel="gallery1" title="Pongal Decorations">
                                        <img src="images/photos/image18.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Tamil PFestival</h2>
                                    <span>Pongal Decorations</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image19.jpg" class="fancybox" data-rel="gallery1" title="Festival Decorations">
                                        <img src="images/photos/image19.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Festival Decorations</h2>
                                    <span>Entrance Set</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image20.jpg" class="fancybox" data-rel="gallery1" title="Save Nature - Theme Decoration ">
                                        <img src="images/photos/image20.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Save Nature</h2>
                                    <span>Outdoor Decoration</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 branding">
                                <div class="view view-first">
                                    <a href="images/photos/image21.jpg" class="fancybox" data-rel="gallery1" title="Save Nature - Theme Decoration">
                                        <img src="images/photos/image21.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Save Nature</h2>
                                    <span>Entrance Set</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image23.jpg" class="fancybox" data-rel="gallery1" title="Laughing Buddha">
                                        <img src="images/photos/image23.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Laughing Buddha</h2>
                                    <span>Props</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/image25.jpg" class="fancybox" data-rel="gallery1" title="Ancient Dance">
                                        <img src="images/photos/image24.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Ancient Dance</h2>
                                    <span>Props</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/image27.jpg" class="fancybox" data-rel="gallery1" title="Title Goes Here">
                                        <img src="images/photos/image27.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Laughing Buddha</h2>
                                    <span>outdoor Set</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/image28.jpg" class="fancybox" data-rel="gallery1" title="Entrance Set">
                                        <img src="images/photos/image28.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Entrance Set</h2>
                                    <span>Set</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 branding">
                                <div class="view view-first">
                                    <a href="images/photos/image29.jpg" class="fancybox" data-rel="gallery1" title="Onam Decorations">
                                        <img src="images/photos/image29.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Onam Decoration</h2>
                                    <span>Entrance Set</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/image30.jpg" class="fancybox" data-rel="gallery1" title="Kathakali Staues">
                                        <img src="images/photos/image30.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Kerela Statue</h2>
                                    <span>Onam Decorations</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/image31.jpg" class="fancybox" data-rel="gallery1" title="Avatars of Vishnu">
                                        <img src="images/photos/image31.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Avatars of Vishnu</h2>
                                    <span>Outdoor set</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/image32.jpg" class="fancybox" data-rel="gallery1" title="Avatars of Vishnu">
                                        <img src="images/photos/image32.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Avatars of Vishnu</h2>
                                    <span>Outdoor Set</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 branding">
                                <div class="view view-first">
                                    <a href="images/photos/image33.jpg" class="fancybox" data-rel="gallery1" title="Onam Decoration">
                                        <img src="images/photos/image33.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Onam Decorations</h2>
                                    <span>Entrance Set</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/d1.jpg" class="fancybox" data-rel="gallery1" title="Kurma Avatar">
                                        <img src="images/photos/d1.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Kurma Avatar</h2>
                                    <span>Dasavatharam</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/d2.jpg" class="fancybox" data-rel="gallery1" title="Narasimha">
                                        <img src="images/photos/d2.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Narasimha Avatar</h2>
                                    <span>Dasavatharam</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 mobile">
                                <div class="view view-first">
                                    <a href="images/photos/d3.jpg" class="fancybox" data-rel="gallery1" title="Vaamana Avatar">
                                        <img src="images/photos/d3.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Vaamana Avatar</h2>
                                    <span>Dasavatharam</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/d4.jpg" class="fancybox" data-rel="gallery1" title="Parasurama Avatar">
                                        <img src="images/photos/d4.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Parasurama Avatar</h2>
                                    <span>Dasavatharam</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/d5.jpg" class="fancybox" data-rel="gallery1" title="Varaha Avatar">
                                        <img src="images/photos/d5.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Varaha Avatar</h2>
                                    <span>Dasavatharam</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/d6.jpg" class="fancybox" data-rel="gallery1" title="Raama Avatar">
                                        <img src="images/photos/d6.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Raama Avatar</h2>
                                    <span>Dasavatharam</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/d7.jpg" class="fancybox" data-rel="gallery1" title="Krishna Avatar">
                                        <img src="images/photos/d7.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Krishna Avatar</h2>
                                    <span>Dasavatharam</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/d8.jpg" class="fancybox" data-rel="gallery1" title="Balaraman Avatar">
                                        <img src="images/photos/d8.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Balaraman Avatar</h2>
                                    <span>Dasavatharam</span>
                                </div>
                            </li> <!--end span4 -->
                            <!--begin span4 -->
                            <li class="list_item span4 responsive">
                                <div class="view view-first">
                                    <a href="images/photos/d9.jpg" class="fancybox" data-rel="gallery1" title="Varaha Avatar">
                                        <img src="images/photos/d9.jpg" alt="Title Goes Here">
                                        <span class="mask">
                                            <span class="fui-search zoom"></span>
                                        </span>
                                    </a>
                                </div>

                                <div class="portfolio_details">
                                    <h2>Varaha Avatar</h2>
                                    <span>Dasavatharam</span>
                                </div>
                            </li> <!--end span4 -->


                        </ul> <!--end portfolio_list -->

                    </div>
                    <!--end isotope -->
                </div>
            </div>
        </section>
        <!--Blog-->
        <!--SEPARATOR-->
        <section class="separator default">
            <div class="container">
                <div class="row">
                    <div class="span7">
                        <div class="descSubscribe">
                            <i class="fui-mail"></i>

                            <h5 style="margin-left: 40px;">Please sign-up for our newsletter to get notified regarding our recent works and offers...!</h5>

                        </div>
                    </div>
                    <div class="span5">
                        <div class="subscribe">
                            <form class="control-group">
                                <input type="text" class="flat" placeholder="Your E-mail" id="EmailSubs">
                                <button class="btn btn-embossed btn-large btn-inverse"> Subscribe</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--Contact-->
        <section id="contact" class="contact">
            <div class="container">
                <div class="row">
                    <div class="span12">
                        <div class="titleHead">
                            <h3>Contact Us</h3>
                            <p>In GPKR, We are comprised of Art Directors, Graphic Artists, Illustrators, Matte Artists, Model Makers, Scenic Artists, Set Designers and Title Artists. We make your dream comes true, in vision.</p>
                        </div>
                        <!--DIVIDER-->
                        <div class="divider">
                            <div class="divLine"></div>
                            <div class="divImg">
                                <i class="icon fui-mail"></i>
                            </div>
                            <div class="divLine"></div>
                        </div>
                    </div>
                </div>

                <!-- Contact Form -->
                <div class="row">

                    <div class="span12">
                        <div id="map"></div>
                    </div>

                    <div class="contact_info span12">
                        <h6><span>Info details</span></h6>
                        
                        <p>
                            When you call GPKR, you won’t get a hard sell. We understand that choosing a Art Direction is a very personal decision. Therefore, our primary goal is to help you determine if we’re a good fit for you. During our initial call, we’ll ask about your unique situation and needs and we’ll explain our process. Then, you can decide if it’s right for you.
                            <br /><br />
                            We look forward to the opportunity to work with you.
                        </p>

                        <div class="row">
                            <address class="adr span6">
                                <span class="road"><em class="icon-map-marker"></em>112, Mariyamman Koil Street, Kavarapattu, Chidambaram - 608102</span>
                                <br />
                                <span class="phone"><em class="icon-phone"></em> +91 770 841 0945</span>
                                <br />
                                <span class="web">
                                    <em class="icon-globe"></em>
                                    <a href="http://www.gpkrdecoratives.com/">www.gpkrdecoratives.com</a>
                                </span>
                              
                                <br/>
                                  <span class="mail">
                                    <em class="icon-envelope"></em>
                                    <a href="mailto:info@gpkrdecoratives.com"> info@gpkrdecoratives.com</a>
                                </span>
                                 <br/>
                                  <span class="mail">
                                    <em class="icon-envelope"></em>
                                    <a href="mailto:Raja@gpkrdecoratives.com"> Raja@gpkrdecoratives.com</a>
                                </span>
                                <br />
                                  <span class="mail">
                                    <em class="icon-envelope"></em>
                                    <a href="mailto:gpkrdecoratives@gmail.com"> gpkrdecoratives@gmail.com</a>
                                </span>
                                <br/>
                            </address>
                        </div>
                        <!-- End Contact Form -->
                    </div>


                    
                </div>
            </div>
        </section>
        <!--Footer-->
        <footer class="footer grey">
            <div class="container">
                <div class="row">
                    <div class="span6">
                        <div class="copyright">
                            <p>&copy; 2015. All right Reserved. Powered By <a href="http://www.aaruthra.com/">Aaruthra Technologies</a></p>
                        </div>
                    </div>
                    <div class="span6">
                        <ul class="bottom-icons">
                            <li><a class="fui-pinterest" href="#"></a></li>
                            <li><a class="fui-facebook" href="#"></a></li>
                            <li><a class="fui-googleplus" href="#"></a></li>
                            <li><a class="fui-vimeo" href="#"></a></li>
                            <li><a class="fui-skype" href="#"></a></li>
                            <li><a class="fui-path" href="#"></a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </footer>
        <!-- Load JS here for greater good =============================-->
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
        <script type="text/javascript" src="js/jquery.js"></script>
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
        <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&language=en"></script>
        <script type="text/javascript" src="js/jquery.cookie.js"></script>
        <script type="text/javascript" src="js/styleswitch.js"></script>
        <script type="text/javascript" src="js/jquery.nav.js"></script>
        <script type="text/javascript" src="js/jquery.sticky.js"></script>
        <script type="text/javascript" src="js/jquery.parallax-1.1.3.js"></script>
        <script type="text/javascript" src="js/jquery.lavalamp-1.4.js"></script>
        <script type="text/javascript" src="js/jquery.scrollTo.js"></script>
        <script type="text/javascript" src="js/rs-plugin/js/jquery.themepunch.plugins.min.js"></script>
        <script type="text/javascript" src="js/rs-plugin/js/jquery.themepunch.revolution.min.js"></script>
        <script type="text/javascript" src="js/jquery.carouFredSel-6.1.0-packed.js"></script>
        <script type="text/javascript" src="js/jquery.bxslider.min.js"></script>
        <script type="text/javascript" src="js/jquery.colorbox.js"></script>
        <script type="text/javascript" src="js/jquery.isotope.min.js"></script>
        <script type="text/javascript" src="js/fancybox/jquery.fancybox.pack.js"></script>
        <script type="text/javascript" src="js/jquery.gmap.js"></script>
        <script type="text/javascript" src="js/flex-slider.min.js"></script>
        <script type="text/javascript" src="js/jquery.placeholder.js"></script>
        <script type="text/javascript" src="js/jquery.validate.min.js"></script>
        <script type="text/javascript" src="js/jquery.tweet.js"></script>
        <script type="text/javascript" src="js/main.js" charset="utf-8"></script>
  
    </form>
</body>
</html>
