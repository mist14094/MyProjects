<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="AddUserWithWaiver.aspx.cs" Inherits="AdWeb.AddUserWithWaiver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="media" style="max-width: 992px; margin-left: auto; margin-right: auto;">
   
      <div class="panel panel-default" > 
        <div class="panel-heading">
    <h3 class="panel-title" style="align-self: center;">Add New User</h3>
  </div>
    
        <div class="panel-body">
              
       <%--///////////////////////////////////////////////////////////////////////////--%>
            
                <br/>           

           
            <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">First Name</span>
                 <asp:TextBox ID="txtFirstName" type="text"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
             <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"  ControlToValidate="txtFirstName" ErrorMessage="*First Name Required"></asp:RequiredFieldValidator>
            <br/>       <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">Last Name</span>
                 <asp:TextBox ID="txtLastName" type="text"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
             <asp:RequiredFieldValidator ID="rfvLastName" runat="server"  ControlToValidate="txtLastName" ErrorMessage="*Last Name Required"></asp:RequiredFieldValidator>
            <br/> 
            
               <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">Address</span>
                 <asp:TextBox ID="txtAddress" type="text"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
             <%--<asp:RequiredFieldValidator ID="rfvAddress" runat="server"  ControlToValidate="txtAddress" ErrorMessage="*Address Required"></asp:RequiredFieldValidator>--%>
            <br/> 
            
               <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">City</span>
                 <asp:TextBox ID="txtCity" type="text"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
             <%--<asp:RequiredFieldValidator ID="rfvCity" runat="server"  ControlToValidate="txtCity" ErrorMessage="*City Required"></asp:RequiredFieldValidator>--%>
            <br/> 
            
            
               <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">State</span>
                 <asp:TextBox ID="txtState" type="text"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
             <%--<asp:RequiredFieldValidator ID="rfvState" runat="server"  ControlToValidate="txtState" ErrorMessage="*State Required"></asp:RequiredFieldValidator>--%>
            <br/> 
            
               <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">Country</span>
                 <asp:TextBox ID="txtCountry" type="text"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
             <%--<asp:RequiredFieldValidator ID="rfvCountry" runat="server"  ControlToValidate="txtCountry" ErrorMessage="*Country Required"></asp:RequiredFieldValidator>--%>
            <br/> 
            
               <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">ZipCode</span>
                 <asp:TextBox ID="txtZipCode" type="text"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
             <%--<asp:RequiredFieldValidator ID="rfvZipCode" runat="server"  ControlToValidate="txtZipCode" ErrorMessage="*Zipcode Required"></asp:RequiredFieldValidator>--%>
            <br/> 
            
            
               <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">Contact Number</span>
                 <asp:TextBox ID="txtMobile" type="text"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
            <%-- <asp:RequiredFieldValidator ID="rfvMobile" runat="server"  ControlToValidate="txtMobile" ErrorMessage="*Contact Number Required"></asp:RequiredFieldValidator>--%>
            <br/> 
            
               <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">Email ID</span>
                 <asp:TextBox ID="txtEmailId" type="email"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
            <%-- <asp:RequiredFieldValidator ID="rfvEmailId" runat="server"  ControlToValidate="txtEmailId" ErrorMessage="*Email ID Required"></asp:RequiredFieldValidator>--%>
            <br/> 
            
               <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">Date of Birth</span>
                 <asp:TextBox ID="txtDob"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
             <%--<asp:RequiredFieldValidator ID="rfvDob" runat="server"  ControlToValidate="txtDob" ErrorMessage="*Date Of Birth Required"></asp:RequiredFieldValidator>--%>
            <br/> 
            
            
                  <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">Select Plan</span>
                 <asp:RadioButtonList ID="rblMenuType" runat="server"></asp:RadioButtonList>
                    
            </div>
             <asp:RequiredFieldValidator ID="rfvMenuType" runat="server"  ControlToValidate="rblMenuType" ErrorMessage="*Select a Plan"></asp:RequiredFieldValidator>
            <br/> 
            
               <br/>           
             <div class="input-group"  style="width:100%;">
                <span class="input-group-addon" id="basic-addon3"  style="width:175px;">Tag Number</span>
                 <asp:TextBox ID="txtTagNumber" type="text"  runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"      ></asp:TextBox>     
                    
            </div>
             <asp:RequiredFieldValidator ID="rfvTagNumber" runat="server"  ControlToValidate="txtTagNumber" ErrorMessage="*Tag Number Required"></asp:RequiredFieldValidator>
            <br/> 


            
              
            
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    <br />
            <br/>
            <table class="nav-justified">
                <tr>
                    <td>  <asp:Button  ID="btnSave" runat="server" Text="Save"  OnClick="btnSave_Click" type="button" class="btn btn btn-success center-block" Width="80%"/></td>
                    <td></td>
                    <td><asp:Button ID="btnClear" runat="server"  Text="Clear" type="button" class="btn btn btn-success center-block" Width="80%" OnClick="btnClear_Click" /></td>
                </tr>
            </table>
            

           
            </div>
        </div>

    </div>
    
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
    <div class="modal-dialog">
        <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content" >
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
</asp:Content>
