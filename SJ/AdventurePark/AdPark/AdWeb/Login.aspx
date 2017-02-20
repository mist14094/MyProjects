<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AdWeb.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Login</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">      <br/>
  <div class="media" style="max-width: 992px; margin-left: auto; margin-right: auto;">
   
      <div class="panel panel-default" > 
        <div class="panel-heading">
    <h3 class="panel-title">Login Here</h3>
  </div>
    
        <div class="panel-body">
               <br/>
            <div class="input-group">   
                <span class="input-group-addon" id="basic-addon1">Username</span>
                <asp:TextBox ID="txtUserName" runat="server" type="text" Text="" class="form-control" placeholder="Username" aria-describedby="basic-addon1"></asp:TextBox>
                   
            </div>
             <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="*Username Required"></asp:RequiredFieldValidator>
            <br /><br/>
            <div class="input-group">
                <span class="input-group-addon" id="basic-addon1">Password</span>
                <asp:TextBox ID="txtPassWord" runat="server" type="text" Text="" class="form-control" TextMode="Password" placeholder="Password" aria-describedby="basic-addon1"></asp:TextBox>
                
                     
          
            </div>
             <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassWord" ErrorMessage="*Password Required"></asp:RequiredFieldValidator>
            <br/><br/>
            

            <table class="nav-justified">
                <tr>
                    <td>  <asp:Button ID="btnLogin" runat="server"  Text="Login" type="button" class="btn btn btn-success center-block" Width="80%" OnClick="btnLogin_Click"/></td>
                    <td>&nbsp;</td>
                    <td><asp:Button ID="btnClear" runat="server"  Text="Clear" type="button" class="btn btn btn-success center-block" Width="80%" OnClick="btnClear_Click"/></td>
                </tr>
              
            </table><br/><br/>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label><br /><br/>

           
            </div>
        </div>

    </div>
</asp:Content>
