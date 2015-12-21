<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FactoryInventory.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <script>
   $.mobile.ajaxEnabled = false;
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        Welcome <br/>
        <asp:Button runat="server" ID="btnCheckIn"  Text="Check In" OnClick="btnCheckIn_Click"/><br/>
        <asp:Button runat="server" ID="btnCheckOut" Text="Check Out" OnClick="btnCheckOut_Click" /><br/>
        <asp:Button runat="server" ID="ChangePass"  Text="Change Password" OnClick="ChangePass_Click" /><br/>
</asp:Content>
