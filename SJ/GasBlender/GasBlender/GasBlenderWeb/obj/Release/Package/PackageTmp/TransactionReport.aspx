<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionReport.aspx.cs" Inherits="GasBlenderWeb.TransactionReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="PrntButton" OnClick="PrntButton_Click" Text="Print" Visible="False"/>
        <CR:CrystalReportViewer ID="TransactionLogReport" runat="server" Visible="True" />
    </div>
    </form>
</body>
</html>
