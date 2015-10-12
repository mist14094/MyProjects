<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POItems.aspx.cs" Inherits="SimplifiedPOWeb.POItems" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <telerik:RadGrid ID="RadGrid1" runat="server"></telerik:RadGrid>
    </div>
    </form>
</body>
</html>
