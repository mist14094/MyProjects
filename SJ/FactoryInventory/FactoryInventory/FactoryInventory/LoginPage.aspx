<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="FactoryInventory.LoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
   $.mobile.ajaxEnabled = false;
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <asp:TextBox ID="txtUserName" runat="server" class="input" placeholder="Username"></asp:TextBox><br/>
                 <asp:TextBox ID="txtPassword" runat="server" class="input" placeholder="Password" type="password"></asp:TextBox><br/>
                <asp:Button runat="server" ID="btnLogin" class="ui-input-text ui-body-a ui-corner-all ui-shadow-inset" Text="Login" OnClick="btnLogin_Click"/><br/>
</asp:Content>
