<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FactoryInventory.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Welcome
      <asp:Button runat="server" ID="btnLogin" class="ui-input-text ui-body-a ui-corner-all ui-shadow-inset" Text="Check In" /><br/>
     <asp:Button runat="server" ID="Button1" class="ui-input-text ui-body-a ui-corner-all ui-shadow-inset" Text="Check Out" /><br/>
    <asp:Button runat="server" ID="Button2" class="ui-input-text ui-body-a ui-corner-all ui-shadow-inset" Text="Change Password" /><br/>
</asp:Content>
