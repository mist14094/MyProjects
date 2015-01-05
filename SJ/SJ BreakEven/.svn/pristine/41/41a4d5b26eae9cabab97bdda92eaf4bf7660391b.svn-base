<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BEPUPC.aspx.cs" Inherits="BEPUPC" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1AutoComplete"
    TagPrefix="wijmo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wijmo:C1AutoComplete ID="C1AutoComplete1" runat="server" DataLabelField="UPC"  MaxCount="5"
            DataMember="DefaultView" DataSourceID="SqlDataSource1" DataValueField="UPC">
           
        </wijmo:C1AutoComplete>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:KT_BreakEvenConnectionString %>" 
            SelectCommand="SELECT DISTINCT [UPC] FROM [ProductCatalog]">
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
