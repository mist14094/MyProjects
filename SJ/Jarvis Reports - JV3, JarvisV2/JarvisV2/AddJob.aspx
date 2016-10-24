<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddJob.aspx.cs" Inherits="AddJob" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Add Job</title>
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
                <td>Scan RFID Number<asp:TextBox ID="txtRFID" runat="server" Width="100%" AutoPostBack="True" OnTextChanged="txtRFID_TextChanged" ></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td>Scan Job Number<br />
                    <asp:TextBox ID="txtJobNumber"  runat="server" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Quantity<br />
                    <asp:TextBox ID="txtTotalWeight" runat="server" step="any" type="number" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rdlType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Take out</asp:ListItem>
                        <asp:ListItem>Put In</asp:ListItem>
                    </asp:RadioButtonList>
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
