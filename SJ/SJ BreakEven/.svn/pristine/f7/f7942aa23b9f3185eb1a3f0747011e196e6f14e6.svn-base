<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesTrend.aspx.cs" Inherits="BEPProduct" %>

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
    
   <script type="text/javascript">
       function hintContent() {
           return this.y;
       }

       function printDiv(divName) {
           var printContents = document.getElementById(divName).innerHTML;
           var originalContents = document.body.innerHTML;

           document.body.innerHTML = printContents;

           window.print();

           document.body.innerHTML = originalContents;
       }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
<form id="Form1" runat ="server" >
        <asp:ScriptManager ID="sd" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="up1" runat="server">
                        <ContentTemplate>
                           
                            
                            
               <center>            
                   <table class="style1">
                       <tr>
                           <td>
                               <wijmo:C1LineChart ID="C1LineChart1" runat="server" Height="470" 
                                   ShowChartLabels="False" Width="1050">
                                   <Header Text="Sales Trend">
                                   </Header>
                                   <Footer Compass="South" Visible="False">
                                   </Footer>
                                   <Legend Visible="true"></Legend>
                                   <Hint OffsetY="-10">
                                       <Content Function="hintContent" />
                                   </Hint>
                               </wijmo:C1LineChart>
                           </td>
                           <td>
                           <br /><br /><br /><br />
                         <br /><br /><br /><br /><br /><br /><center>        <asp:Label ID="lblNoSales" runat="server" Font-Size="55px"   style=" vertical-align:middle;"
                                   Text="No Sales Happenend Yet"></asp:Label></center>  
                           </td>
                       </tr>
                   </table>
                              </center>   
                            
                           
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </form>
</body>
</html>
