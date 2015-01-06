<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            
    <shield:ShieldChart ID="ShieldChart1" Width="100%" Height="900px" runat="server"
    CssClass="chart" ></shield:ShieldChart>
    </div>
    </form>
</body>
</html>
