<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LotControlWeb.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Home</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/>
    <asp:Button ID="Button1" runat="server" Text="Print Tags for PO"   type="button" class="btn btn-lg btn-success center-block"  Width="90%"/>
</asp:Content>
