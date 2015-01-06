<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewClassDetails.aspx.cs" Inherits="ViewClassDetails" %>
<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" type="text/css" href="//www.shieldui.com/shared/components/latest/css/shieldui-all.min.css" />
    <link rel="stylesheet" type="text/css" href="//www.shieldui.com/shared/components/latest/css/light-mint/all.min.css" />
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/shieldui-all.min.js"></script>
    <link rel="stylesheet" href="http://yui.yahooapis.com/pure/0.5.0/pure-min.css">
    <link href="1.17.16.css" rel="stylesheet" />
      
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <shield:ShieldChart ID="ShieldChart1" Width="100%" Height="400px" runat="server" ></shield:ShieldChart>
        
        <table class="pure-table" width="100%">
             <thead>
        <tr>
            <td>&nbsp;</td>
            <td>Overall Average (%)</td>
            <td>Latest Value (%)</td>
            <td>Higest Record (%)</td>
            <td>Lowest Record (%)</td>
        </tr>
                 </thead>
        <tr>
            <td>Nicotine(%)</td>
            <td><asp:Label ID="lblAvgNicotine" runat="server" Text="Label"></asp:Label></td>
            <td><asp:Label ID="lblLatestValueNIC" runat="server" Text="Label"></asp:Label></td>
            <td><asp:Label ID="lblHighestValueNIC" runat="server" Text="Label"></asp:Label></td>
            <td><asp:Label ID="lblLowestValueNIC" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr  class="pure-table-odd">
            <td>T Sugar(%)</td>
            <td><asp:Label ID="lblAvgTSugar" runat="server" Text="Label"></asp:Label></td>
            <td><asp:Label ID="lblLatestValueTSugar" runat="server" Text="Label"></asp:Label></td>
            <td><asp:Label ID="lblHighestValueTSugar" runat="server" Text="Label"></asp:Label></td>
            <td><asp:Label ID="lblLowestValueTSugar" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td>R Sugar(%)</td>
            <td><asp:Label ID="lblAvgRSugar" runat="server" Text="Label"></asp:Label></td>
            <td><asp:Label ID="lblLatestValueRSugar" runat="server" Text="Label"></asp:Label></td>
            <td><asp:Label ID="lblHighestValueRSugar" runat="server" Text="Label"></asp:Label></td>
            <td><asp:Label ID="lblLowestValueRSugar" runat="server" Text="Label"></asp:Label></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
