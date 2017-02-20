<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="AssociateActv.aspx.cs" Inherits="AdWeb.AssociateActv"  EnableEventValidation="true"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <style type="text/css">

  input:required:invalid, input:focus:invalid {
    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAeVJREFUeNqkU01oE1EQ/mazSTdRmqSxLVSJVKU9RYoHD8WfHr16kh5EFA8eSy6hXrwUPBSKZ6E9V1CU4tGf0DZWDEQrGkhprRDbCvlpavan3ezu+LLSUnADLZnHwHvzmJlvvpkhZkY7IqFNaTuAfPhhP/8Uo87SGSaDsP27hgYM/lUpy6lHdqsAtM+BPfvqKp3ufYKwcgmWCug6oKmrrG3PoaqngWjdd/922hOBs5C/jJA6x7AiUt8VYVUAVQXXShfIqCYRMZO8/N1N+B8H1sOUwivpSUSVCJ2MAjtVwBAIdv+AQkHQqbOgc+fBvorjyQENDcch16/BtkQdAlC4E6jrYHGgGU18Io3gmhzJuwub6/fQJYNi/YBpCifhbDaAPXFvCBVxXbvfbNGFeN8DkjogWAd8DljV3KRutcEAeHMN/HXZ4p9bhncJHCyhNx52R0Kv/XNuQvYBnM+CP7xddXL5KaJw0TMAF8qjnMvegeK/SLHubhpKDKIrJDlvXoMX3y9xcSMZyBQ+tpyk5hzsa2Ns7LGdfWdbL6fZvHn92d7dgROH/730YBLtiZmEdGPkFnhX4kxmjVe2xgPfCtrRd6GHRtEh9zsL8xVe+pwSzj+OtwvletZZ/wLeKD71L+ZeHHWZ/gowABkp7AwwnEjFAAAAAElFTkSuQmCC);
    background-position: right center;
    background-repeat: no-repeat;
    -moz-box-shadow: none;
  }
  input:required:valid {
    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAepJREFUeNrEk79PFEEUx9/uDDd7v/AAQQnEQokmJCRGwc7/QeM/YGVxsZJQYI/EhCChICYmUJigNBSGzobQaI5SaYRw6imne0d2D/bYmZ3dGd+YQKEHYiyc5GUyb3Y+77vfeWNpreFfhvXfAWAAJtbKi7dff1rWK9vPHx3mThP2Iaipk5EzTg8Qmru38H7izmkFHAF4WH1R52654PR0Oamzj2dKxYt/Bbg1OPZuY3d9aU82VGem/5LtnJscLxWzfzRxaWNqWJP0XUadIbSzu5DuvUJpzq7sfYBKsP1GJeLB+PWpt8cCXm4+2+zLXx4guKiLXWA2Nc5ChOuacMEPv20FkT+dIawyenVi5VcAbcigWzXLeNiDRCdwId0LFm5IUMBIBgrp8wOEsFlfeCGm23/zoBZWn9a4C314A1nCoM1OAVccuGyCkPs/P+pIdVIOkG9pIh6YlyqCrwhRKD3GygK9PUBImIQQxRi4b2O+JcCLg8+e8NZiLVEygwCrWpYF0jQJziYU/ho2TUuCPTn8hHcQNuZy1/94sAMOzQHDeqaij7Cd8Dt8CatGhX3iWxgtFW/m29pnUjR7TSQcRCIAVW1FSr6KAVYdi+5Pj8yunviYHq7f72po3Y9dbi7CxzDO1+duzCXH9cEPAQYAhJELY/AqBtwAAAAASUVORK5CYII=);
    background-position: right center;
    background-repeat: no-repeat;
  }

</style>
    <div class="media" style="max-width: 992px; margin-left: auto; margin-right: auto;">

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title" style="align-self: center;">Edit Activities</h3>
            </div>
           <%-- <form data-toggle="validator" role="form">--%>
                  <div class="form-group">
            <div class="container">
                <div class="row">
                    <div class="col-sm-10">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Scan your Tag Number</span>
                            <asp:TextBox ID="txtTagNumber" type="text" runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1" AutoPostBack="True" OnTextChanged="txtTagNumber_TextChanged"></asp:TextBox>
                        </div>
                        <br />
                    </div>
                     <div class="col-sm-2">
                        <br />
                        <div class="input-group" style="width: 100%;">
                          
                            <asp:Button ID="btnScan" runat="server" Width="100%" Text="Scan" type="button" class="btn btn-danger" OnClick="btnScan_Click"  />
                        </div>
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Rope Course (Min)</span>
                            <asp:TextBox ID="txtRopeCourseOrg" disabled type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtRopeCourseMod" type="number" step="60"  runat="server" min="0" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                        <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Zip Line</span>
                            <asp:TextBox ID="txtZipLineOrg" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtZipLineMod" type="number" runat="server" Width="100%" min="0" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Jump Zone</span>
                            <asp:TextBox ID="txtJumpZoneOrg" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtJumpZoneMod" type="number" runat="server" Width="100%" min="0" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Tubing</span>
                            <asp:TextBox ID="txtTubingOrg" disabled type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtTubingMord" type="number" runat="server" Text="0" min="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                        <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Lacrosse</span>
                            <asp:TextBox ID="txtLacrosseOrg" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtLacrosseMod" type="number" runat="server" Width="100%" min="0" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Maze</span>
                            <asp:TextBox ID="txtMazeOrg" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtMazeMod" type="number" runat="server" Width="100%" min="0" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Soft Ball</span>
                            <asp:TextBox ID="txtSoftBallOrg" disabled type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtSoftBallMod"  min="0" type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                        <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Soccer Dart</span>
                            <asp:TextBox ID="txtSoccerDartOrg" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtSoccerDartMod"   min="0" type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Kids Zone</span>
                            <asp:TextBox ID="txtKidsZoneOrg" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtKidsZoneMod"  min="0" type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="Panel1" runat="server" Visible="True">
                     <div class="row" >
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Heavy Ball</span>
                            <asp:TextBox ID="txtHeavyBallOrg" disabled type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtHeavyBallMod"  min="0" type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                        <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Bull Ride</span>
                            <asp:TextBox ID="txtBullRideOrg" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtBullRideMod"   min="0" type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Extra Act 1</span>
                            <asp:TextBox ID="txtExtraAct1Org" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtExtraAct1Mod"  min="0" type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Extra Act 2</span>
                            <asp:TextBox ID="txtExtraAct2Org" disabled type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtExtraAct2Mod"  min="0" type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                        <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Extra Act 3</span>
                            <asp:TextBox ID="txtExtraAct3Org" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtExtraAct3Mod"  min="0" type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Extra Act 4</span>
                            <asp:TextBox ID="txtExtraAct4Org"  min="0" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtExtraAct4Mod" type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Extra Act 5</span>
                            <asp:TextBox ID="txtExtraAct5Org" disabled type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtExtraAct5Mod"  min="0" type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                        <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Extra Time 1</span>
                            <asp:TextBox ID="txtExtraTime1Org" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtExtraTime1Mod"  min="0" step="60" type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Extra Time 2</span>
                            <asp:TextBox ID="txtExtraTime2Org" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtExtraTime2Mod" min="0" step="60" type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Extra Time 3</span>
                            <asp:TextBox ID="txtExtraTime3Org" disabled type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtExtraTime3Mod"  min="0" step="60"  type="number" runat="server" Text="0" Width="100%" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                        <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Extra Time 4</span>
                            <asp:TextBox ID="txtExtraTime4Org" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtExtraTime4Mod"  min="0" step="60" type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-sm-4">
                        <br />
                        <div class="input-group" style="width: 100%;">
                            
                            <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Extra Time 5</span>
                            <asp:TextBox ID="txtExtraTime5Org" disabled type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                              <asp:TextBox ID="txtExtraTime5Mod"  min="0" step="60" type="number" runat="server" Width="100%" Text="0" class="form-control" aria-describedby="basic-addon1" required></asp:TextBox>
                        </div>
                    </div>
                </div>

                </asp:Panel>
               
                  <div class="row">
                    <div class="col-sm-6">
                         <br />   <br />
                        <div>
                            <asp:Button ID="btnSave" runat="server" Text="Update"  Width="100%" type="button" class="btn btn-success" OnClick="btnSave_Click"/>
                        </div>
                        </div>
                      
                      <div class="col-sm-6">
                           <br />   <br />
                        <div>
                            <asp:Button ID="btnClear" runat="server" Text="Clear"  Width="100%" type="button" class="btn btn-success" OnClick="btnClear_Click"/>
                        </div>
                        </div>

                  </div>
            </div>
                        
</div>
             <%--   </form>--%>
            
        </div>
    </div>
    
        
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" >
            <ContentTemplate>
                <div class="modal-content"  >
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                      <h1>  <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label></h1>
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
