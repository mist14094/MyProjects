<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="HomeSetup.aspx.cs" Inherits="GasBlenderWeb.HomeSetup" %>
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
                    <asp:Button ID="btnTrailer" runat="server" Text="Setup Trailer" Width="150px" CssClass="buttons" style="text-align: center;" OnClick="btnTrailer_Click" /></center>
                </td>
                <td>&nbsp;</td>
                <td><center>
                    <asp:Button ID="btnLocation" runat="server" Text="Setup Location"  Width="150px" CssClass="buttons" style="text-align: center;" OnClick="btnLocation_Click" /></center>
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
            <tr>
                <td>&nbsp;</td>
                <td><center>
                    <asp:Button ID="btnTractor" runat="server" Text="Setup Tractor" Width="150px" CssClass="buttons" style="text-align: center;" OnClick="btnTractor_Click" /></center></td>
                <td>&nbsp;</td>
                <td><center>
                    <asp:Button ID="btnDriver" runat="server" Text="Setup Driver" Width="150px" CssClass="buttons" style="text-align: center;" OnClick="btnDriver_Click" /></center></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <center>
                        <asp:Button ID="btnCarrier" runat="server" CssClass="buttons" style="text-align: center;" Text="Setup Carriers" Width="150px" OnClick="btnCarrier_Click" />
                    </center>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
