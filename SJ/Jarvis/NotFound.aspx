<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotFound.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
	
	<title>Not configured Yet</title>
	<meta name="viewport" content="width=device-width, user-scalable=no">
	<style>
		* { margin: 0; padding: 0; }
	
		#bg { position: fixed; top: 0; left: 0;  background-color:#00172a;  }
		.bgwidth { width: 100%; }
		.bgheight { height: 100%; }
		
		#page-wrap { position: relative; width: 400px; margin: 50px auto; padding: 20px; background-color:#00172a;  -moz-box-shadow: 0 0 20px black; -webkit-box-shadow: 0 0 20px black; box-shadow: 0 0 20px black; }
		p { font: 15px/2 Georgia, Serif; margin: 0 0 30px 0; text-indent: 40px; }
	</style>
	
	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
	<script>
	    $(function () {

	        var theWindow = $(window),
			    $bg = $("#bg"),
			    aspectRatio = $bg.width() / $bg.height();

	        function resizeBg() {

	            if ((theWindow.width() / theWindow.height()) < aspectRatio) {
	                $bg
				    	.removeClass()
				    	.addClass('bgheight');
	            } else {
	                $bg
				    	.removeClass()
				    	.addClass('bgwidth');
	            }

	        }

	        theWindow.resize(function () {
	            resizeBg();
	        }).trigger("resize");

	    });
	</script>

</head>

<body style="background-color:#00172a; ">
    <form id="form1" runat="server">
    <div>
        <img src="jarvis.jpg" id="bg" alt="">
    </div>
    </form>
</body>
</html>
