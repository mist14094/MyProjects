<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PriceChangeWOCA.aspx.cs" Inherits="PriceChangerWeb.PriceChangeWOCA" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1ComboBox" TagPrefix="wijmo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            margin-left: 20px;
        }
    </style>
        
</head>
<body style="background-color: #f7f7e9; font-family:Arial,sans-serif ">
    <form id="form1" runat="server" style="background-color: #f7f7e9">
    <div style="min-width: 300px;">
        <center><h3>
        <asp:Label ID="lblUPCTitle" runat="server" Text="UPC : "></asp:Label>
  <asp:Label ID="lblUPC" runat="server" Text=""></asp:Label>
             <asp:Label ID="lblSKUTitle" runat="server" Text="| SKU : "></asp:Label>
        <asp:Label ID="lblSKU" runat="server" Text=""></asp:Label></h3></center>
        <table class="auto-style1">
            <tr>
                <td>Price</td>
                <td>
        <asp:TextBox ID="txtGPrice" runat="server" Width="100%"></asp:TextBox>

                </td>
                <td>&nbsp;</td>
                <td>
        <asp:Button ID="btnPriceChangeAll" runat="server" Text="Change All Price" OnClick="btnPriceChangeAll_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Cost</td>
                <td>
        <asp:TextBox ID="txtGCost" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>
        <asp:Button ID="btnCostChangeAll" runat="server" Text="Change All Cost " OnClick="btnCostChangeAll_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Description</td>
                <td>          

        
        <asp:TextBox ID="txtGDesc" runat="server" Width="100%"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>
        <asp:Button ID="btnDescChangeAll" runat="server" Text="Change All Desc" OnClick="btnDescChangeAll_Click"  />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>          

        
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>          

        
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>          

        
        <asp:Button ID="btnChangeAll" runat="server" Text="Change All" OnClick="btnChangeAll_Click" Width="200px"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSaveValue" runat="server" Text="Save All" OnClick="btnSaveValue_Click"  Width="200px"  />
                &nbsp;
  <h5><asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label></h5>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
     
        <asp:Panel ID="pnlAllStores" runat="server"></asp:Panel>
        
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>          

        
        <br />
        
        <br />

    </div>
    </form>
</body>
</html>
