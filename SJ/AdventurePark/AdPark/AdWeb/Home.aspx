<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="AdWeb.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div class="media" style="max-width: 992px; margin-left: auto; margin-right: auto;">
        <div class="panel panel-default">

            <table class="table">
                <tr>
                    <td>
                        <asp:Button ID="btnSearchWaiver" runat="server" Text="Search Waiver and Add User" class="btn btn-success btn-lg" Style="width: 100%" OnClick="btnSearchWaiver_Click" /></td>
                </tr>
                 <tr>
                    <td>
                        <asp:Button ID="btnSearchTag" runat="server" Text="Search by Tag or Lastname" class="btn btn-success btn-lg" Style="width: 100%" OnClick="btnSearchTag_Click"  /></td>
                </tr>
                  <tr>
                    <td>
                        <asp:Button ID="btnViewTagRemainingActivities" runat="server" Text="View Tag - Activities Remaining" class="btn btn-success btn-lg" Style="width: 100%" OnClick="btnViewTagRemainingActivities_Click"  /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAddUser" runat="server" Text="Add User" class="btn btn-success btn-lg" Style="width: 100%" OnClick="btnAddUser_Click" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btTagUpdate" runat="server" Text="Update User Activities" class="btn btn-success btn-lg" Style="width: 100%" OnClick="btTagUpdate_Click" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnTagLog" runat="server" Text="View Tag Log" class="btn btn-success btn-lg" Style="width: 100%" OnClick="btnTagLog_Click" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnDevices" runat="server" Text="Devices" class="btn btn-success btn-lg" Style="width: 100%" OnClick="btnDevices_Click" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnMonitor" runat="server" Text="Monitor" class="btn btn-success btn-lg" Style="width: 100%" OnClick="btnMonitor_Click" /></td>
                </tr>
                <tr>
                    <td>
                        <button type="button" class="btn btn-success btn-lg" style="width: 100%">Settings</button></td>
                </tr>
            </table>


        </div>
    </div>
</asp:Content>
