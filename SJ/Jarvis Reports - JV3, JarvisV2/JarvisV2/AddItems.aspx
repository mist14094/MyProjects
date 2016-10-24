<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddItems.aspx.cs" Inherits="AddItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="auto-style1">
            <tr>
                <td>Select Tobacco Type<br />
        <asp:DropDownList ID="ddlTobaccoType" runat="server" Width="100%"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Mfg Date<br />
                    <asp:TextBox ID="txtMfgDate" type="date" runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Total Weight in LBs<br />
                    <asp:TextBox ID="txtTotalWeight" runat="server" step="any" type="number" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Final Moisture<br />
                    <asp:TextBox ID="txtMoisture" runat="server" step="any" type="number" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>New Tote#<br />
                    <asp:TextBox ID="txtNewTote" runat="server" step="any" type="number"  Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>RFID Tag#<br />
                    <asp:TextBox ID="txtRFID" runat="server" Width="100%"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="100%" OnClick="btnSave_Click" />
<br/>
                    <br />
<asp:Button ID="Button2" runat="server" Text="Clear" Width="100%" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
