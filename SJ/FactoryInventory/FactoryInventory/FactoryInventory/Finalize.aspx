<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Finalize.aspx.cs" Inherits="FactoryInventory.Finalize" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   <script>
   $.mobile.ajaxEnabled = false;
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Finalize" />   
</asp:Content>
