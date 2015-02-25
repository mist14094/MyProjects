<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="HomeReports.aspx.cs" Inherits="GasBlenderWeb.HomeReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin:50px;">
        <table class="auto-style1">
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><center>
                    <asp:Button ID="Button1" runat="server" Text="Reprint BOL" Width="150px" CssClass="buttons" style="text-align: center;" OnClick="Button1_Click" /></center>
                </td>
                <td>&nbsp;</td>
                <td><center>
                    <asp:Button ID="Button2" runat="server" Text="Transaction Log"  Width="150px" CssClass="buttons" style="text-align: center;" OnClick="Button2_Click" /></center>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
