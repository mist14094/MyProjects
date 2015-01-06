<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="LineChartXAxisProperties.aspx.cs" Inherits="LineChartXAxisProperties" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>X-Axis Properties</title>
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

     <!-- FONTAWESOME STYLES-->
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
        <!-- CUSTOM STYLES-->
    <link href="assets/css/custom.css" rel="stylesheet" />
     <!-- GOOGLE FONTS-->
   <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
        <link rel="stylesheet" type="text/css" href="http://localhost:14111/www.shieldui.com/shared/components/latest/css/shieldui-all.min.css" />
    <link rel="stylesheet" type="text/css" href="http://localhost:14111/www.shieldui.com/shared/components/latest/css/light-mint/all.min.css" />
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/shieldui-all.min.js"></script>
    <script>
        window.onunload = refreshParent;
        function refreshParent() {
            window.opener.location.reload();
        }
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 7%;margin-right: 7%; min-width: 500px;">
     <div id="page-inner">
        <div>
            <div class="row">
                <div class="col-md-12">
                   <center> <h2>
                        Edit Your Reports - Axis
                    </h2>
                    <h5>
                        This page will help you to create your own report with easy configurations.
                    </h5></center>
                    <table class="nav-justified" style="width: 100%">
                        <tr>
                            <td style="width: 35%">
                                &nbsp;
                            </td>
                            <td style="width: 5%">
                                &nbsp;</td>
                            <td style="width: 35%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Column Name - X Axis</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Swap Location</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                                <asp:DropDownList ID="ddViewsList" runat="server" Width="100%" AutoPostBack="True" >
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:CheckBox ID="chkSwapLocation"  runat="server" Checked="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td>
                                Tick Repeat</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Axis Text Angle</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtTicksRepeat" runat="server"  Width="100%" Text="0"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtTextAngle" runat="server"  Width="100%" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Axis Text Angle - X</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Axis Text Angle - Y</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                <asp:TextBox ID="txtAxisTextAngleX"  Width="100%" runat="server" Text="0"></asp:TextBox>
                        <br />
                                
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtAxisTextAngleY"  Width="100%" runat="server" Text="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Axis Text Angle - Step</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Title Text</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAxisTextAngleStep"  Width="100%" runat="server" Text="0"></asp:TextBox>
                                
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtTitle"   Width="100%" runat="server" Text="X Axis"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddNew" runat="server" Text="Update" OnClick="btnAddNew_Click"  /> 
                                
                            &nbsp;
                                <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label>
                                
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
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
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
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
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </div>
    </div>
</div>
          </form>
</body>
</html>

