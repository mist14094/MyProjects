<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SJFactoryInventory.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>Home</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <br/>
    <asp:Button ID="btnHome" runat="server" Text="Print Tags for PO"   type="button" class="btn btn-lg btn-success center-block"  Width="90%" /><br />
    <asp:Button ID="btnLogout" runat="server" Text="Logout"   type="button" class="btn btn-lg btn-success center-block"  Width="90%" OnClick="btnLogout_Click"/>
    <br/>
</asp:Content>
