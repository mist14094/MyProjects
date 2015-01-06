<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="MainChartProperties.aspx.cs" Inherits="MainChartProperties" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Chart Properties</title>
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <link href="assets/css/bootstrap.css" rel="stylesheet" />
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


</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 7%;margin-right: 7%; min-width: 500px;">
    <div id="page-inner">
        <div>
            <div class="row">
                <div class="col-md-12">
                    <h2>
                       <center> Chart Properties</center></h2>
                    <h5>
                        <center>Change attributes of chart.</center>
                        <br/>
                    </h5>
                    <table class="nav-justified">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 15%">
                    <asp:Label ID="lblchartPrimaryHeader" runat="server" Text="Primary Header"></asp:Label>
                            </td>
                            <td style="width: 35%">
                    <asp:TextBox ID="txtPrimaryHeader" runat="server" Text="Title" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;</td>
                            <td   style="width: 15%">
                    <asp:Label ID="lblchartSecondaryHeader" runat="server" Text="Secondary Header "></asp:Label>
                            </td>
                            <td style="width: 35%" colspan="2">
                    <asp:TextBox ID="txtSecondaryHeader" runat="server" Text="-" Width="100%"></asp:TextBox>
                            </td>
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
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                    <asp:Label ID="lblAllowMultipleSelection" runat="server" Text="Multiple Selection"></asp:Label>
                            </td>
                            <td>
                    <asp:CheckBox ID="chkAllowMultipleSelection" Text="Enable" runat="server" Checked="True" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                    <asp:Label ID="lblHeight" runat="server" Text="Height"></asp:Label>
                            </td>
                            <td>
                    <asp:TextBox ID="txtHeight" runat="server" Text="500" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblHeight" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>Percentage</asp:ListItem>
                        <asp:ListItem Selected="True">Pixel</asp:ListItem>
                    </asp:RadioButtonList>
                            </td>
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
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td>
                    <asp:Label ID="lblAllowExportImage" runat="server" Text="Export To Image"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkExportImage" Text="Enable" runat="server"  Checked="True"/></td>
                            <td>
                                &nbsp;</td>
                            <td>
                    <asp:Label ID="lblWidth" runat="server" Text="Width"></asp:Label>
                            </td>
                            <td>
                    <asp:TextBox ID="txtWidth" runat="server" Text="900" Width="100px"></asp:TextBox>
                            </td>
                            <td>
               
                    <asp:RadioButtonList ID="rblWidth" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>Percentage</asp:ListItem>
                        <asp:ListItem Selected="True">Pixel</asp:ListItem>
                    </asp:RadioButtonList>
                            </td>
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
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                    <asp:Label ID="lblAllowPrintImage" runat="server" Text="Print Chart"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkPrint" Text="Enable" runat="server" Checked="True" /></td>
                            <td>
                                &nbsp;</td>
                            <td>
                    <asp:Label ID="lblIsInverted" runat="server" Text="Invert Chart"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="chkInvertChart" Text="Enable" runat="server" /></td>
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
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                    <asp:Label ID="lblZoomMode" runat="server" Text="Zoom Mode"></asp:Label>
                            </td>
                            <td>
                    <asp:DropDownList ID="ddlZoomMode" runat="server">
                        <asp:ListItem Value="0" Selected="True">None</asp:ListItem>
                        <asp:ListItem Value="1">X</asp:ListItem>
                        <asp:ListItem Value="2">Y</asp:ListItem>
                        <asp:ListItem Value="3">XY</asp:ListItem>
                    </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                    <asp:Label ID="lblAxisMarkersEnabled" runat="server" Text="Axis Marker"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="chkAxisMarkerEnabled" Text="Enable" runat="server" Checked="True" /></td>
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
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                    <asp:Label ID="lblAxisMarkersMode" runat="server" Text="Axis Markers"></asp:Label>
                            </td>
                            <td>
                    <asp:DropDownList ID="ddlAxisMarkerMode" runat="server">
                        <asp:ListItem Value="0" >None</asp:ListItem>
                        <asp:ListItem Value="1">X</asp:ListItem>
                        <asp:ListItem Value="2">Y</asp:ListItem>
                        <asp:ListItem Value="3" Selected="True">XY</asp:ListItem>
                    </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                    <asp:Label ID="lblAxisMarkersWidth" runat="server" Text="Axis Marker Width"></asp:Label>
                            </td>
                            <td colspan="2">
                    <asp:TextBox ID="txtAxisWidth" runat="server" Text="1" Width="100%"></asp:TextBox></td>
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
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                    <asp:Label ID="lblToolTipSettings" runat="server" Text="ChartBound"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkChartBound" Text="Enable" runat="server" Checked="True" /></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnUpdate" runat="server"  class="btn btn-danger square-btn-adjust" Text="Update" OnClick="btnUpdate_Click" />
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
    </div>
    </div>
    <!-- /. ROW  -->
    <hr />
    </div>
</div>
          </form>
</body>
</html>

