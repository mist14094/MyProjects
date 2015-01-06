<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultBar.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="cc1" %>
<%@ Register TagPrefix="shield" Namespace="Shield.Web.UI" Assembly="Shield.Web.UI, Version=1.7.2.0, Culture=neutral, PublicKeyToken=d849307612f07c09" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" type="text/css" href="//www.shieldui.com/shared/components/latest/css/shieldui-all.min.css" />
    <link rel="stylesheet" type="text/css" href="//www.shieldui.com/shared/components/latest/css/light-mint/all.min.css" />
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/shieldui-all.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ShieldChart ID="ShieldChart1" runat="server"></cc1:ShieldChart>
    </div>
    </form>
</body>
</html>
