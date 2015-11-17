<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="UpdatePO.aspx.cs" Inherits="SimplifiedPOWeb.UpdatePO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style=" min-width:950px;">
    
   <center> <h2>Update New Purchase Order</h2></center>
    <table class="pure-table pure-table-Noline"  style="width: 100%">
        <thead>
            <td colspan="2"><h3>Buyer Information</h3></td>
            <td class="auto-style3">&nbsp;</td>
            <td colspan="2"><h3>Supplier Information</h3></td>
        </thead>
        <tr>
            <td class="auto-style4">UserName</td>
            <td class="auto-style5">
                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
            </td>
            <td class="auto-style6"></td>
            <td class="auto-style7">Select Entity</td>
            <td class="auto-style5">
                <asp:DropDownList ID="ddlEntity" runat="server" Width="244px" AutoPostBack="True" Font-Size="Small" OnSelectedIndexChanged="ddlEntity_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style8" >Post for</td>
            <td class="auto-style9">
                <asp:DropDownList ID="ddlBuyerPostFor" runat="server" Width="244px" AutoPostBack="True"  Font-Size="Small" OnSelectedIndexChanged="ddlBuyerPostFor_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">Select Supplier</td>
            <td class="auto-style9">
                <asp:DropDownList ID="ddlSupplier" runat="server" Width="244px" Font-Size="Small" class="pure-menu pure-menu-horizontal" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Buyer&#39;s Name</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtBuyerName" runat="server" Font-Size="Small" Height="19px" Width="244px"></asp:TextBox>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">Supplier Name</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtSupplierName" runat="server" Height="19px" Width="244px" Font-Size="Small" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Buyer Address</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtBuyerAddress" runat="server" Height="76px" Width="244px" TextMode="MultiLine" Font-Size="Small"></asp:TextBox>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">Supplier Address</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtSupplierAddress" runat="server" Height="76px" Width="244px" TextMode="MultiLine" Font-Size="Small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Contact Number</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtBuyerContact" runat="server" Height="19px" Width="244px" Font-Size="Small" ></asp:TextBox>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">Contact Number</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtSupplierContact" runat="server" Height="19px" Width="244px" Font-Size="Small" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Priority</td>
            <td class="auto-style9">
                <asp:DropDownList ID="ddlPriority" runat="server" Width="244px" AutoPostBack="True"  Font-Size="Small" OnSelectedIndexChanged="ddlBuyerPostFor_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">Notes</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtNotes" runat="server" Height="76px" Width="244px" TextMode="MultiLine" Font-Size="Small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">&nbsp;</td>
            <td class="auto-style9">
          
            </td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style10">&nbsp;</td>
            <td class="auto-style9">
                   <asp:Button ID="btnNext" runat="server" Text="Next >>" class="pure-button pure-button-primary" Width="244px" OnClick="btnNext_Click" /></td>
        </tr>
        </table>
    </div>
</asp:Content>
