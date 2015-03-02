<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="TransactionLogHome.aspx.cs" Inherits="GasBlenderWeb.TransactionLogHome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 84px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 50px;">
        <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
        <table class="auto-style1">
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">Start Date : </td>
                <td>
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

                    <asp:ImageButton runat="Server" ID="ibStartDate" ImageUrl="Images/calendar.png" AlternateText="Click here to display calendar" />
                    <asp:TextBox ID="txtStartDate" runat="server" Enabled="False"></asp:TextBox>
                    <cc1:CalendarExtender runat="server" ID="clExtenderStart" TargetControlID="txtStartDate" PopupButtonID="ibStartDate" />
                    &nbsp;
                    <asp:DropDownList ID="ddlHourStart" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlMinuteStart" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSecondStart" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAMStart" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">End Date :
                </td>
                <td>
                    <asp:ImageButton runat="Server" ID="ibEndDate" ImageUrl="Images/calendar.png" AlternateText="Click here to display calendar" />
                    <asp:TextBox ID="txtEndDate" runat="server"  Enabled="False"></asp:TextBox>
                    <cc1:CalendarExtender runat="server" ID="clExtenderEnd" TargetControlID="txtEndDate" PopupButtonID="ibEndDate" />
                    &nbsp;
                    <asp:DropDownList ID="ddlHourEnd" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlMinuteEnd" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSecondEnd" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAMEnd" runat="server">
                    </asp:DropDownList>

                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>
                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>
                    <asp:Button ID="btnGetData" runat="server" Text="Get Report" OnClick="btnGetData_Click" CssClass="buttons" />
                </td>
            </tr>
        </table>                
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
