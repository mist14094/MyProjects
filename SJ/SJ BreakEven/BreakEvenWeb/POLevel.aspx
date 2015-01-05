<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POLevel.aspx.cs" Inherits="POLevel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />PO Number : <asp:Label ID="lblPoNumber" runat="server" Text="Label"></asp:Label>
    <br />Supplier  : <asp:Label ID="lblSupplierName" runat="server" Text="Label"></asp:Label>
    <br />CoGS Total Cost : <asp:Label ID="lblTotalCost" runat="server" Text="Label"></asp:Label>
    <br />Retail Total : <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />Sales Realised : <asp:Label ID="lblSalesRealised" runat="server" Text="Label"></asp:Label>
    <br />Total Products : <asp:Label ID="lblTotalProducts" runat="server" Text="Label"></asp:Label>
    <br />Total Sold Products : <asp:Label ID="lblTotalSoldProducts" runat="server" Text="Label"></asp:Label>
    <br />Date Received : <asp:Label ID="lblDateReceived" runat="server" Text="Label"></asp:Label>

    <br />Graph Pie Chart for Total Products

    <br />Grid with Total Items and Sales Details
    <br />
    </div>
    </form>
</body>
</html>
