<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="CountExpire.aspx.cs" Inherits="AdWeb.CountExpire" EnableEventValidation="true" %>

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
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <%--<script language="javascript">
  

        $(document).ready(function () {
            $('<%= txtTagNumber.ClientID %>').keydown(function (e) {
              if (e.keyCode == 13) $('<%= btnScan.ClientID %>'.click());
            });
        });
    </script>--%>
    <style>
        td { 
    padding: 10px;
}
    </style>


    <div class="media" style="max-width: 992px; margin-left: auto; margin-right: auto;">

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title" style="align-self: center;">
                    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h3>
            </div>
            <%-- <form data-toggle="validator" role="form">--%>
            <div class="form-group">
                <div class="container">
                                                <br />
                    <div class="row">
                        <div class="col-sm-12">
                       <center>   <div><label class="control-label">Select the Mode</label>
                                    <asp:RadioButtonList ID="rblInandOut" runat="server" RepeatDirection="Horizontal" CellPadding="10"  >
                                        <asp:ListItem class="radio-inline">Get In </asp:ListItem>
                                        <asp:ListItem class="radio-inline">Get Out </asp:ListItem>
                                    </asp:RadioButtonList>
                             <asp:RequiredFieldValidator ID="rfvInandOut" runat="server" ControlToValidate="rblInandOut" ForeColor="#ff0000" ErrorMessage="*Select In time or Out time"></asp:RequiredFieldValidator>
                           
                                          <asp:Label ID="lblDeviceCounter" runat="server" Text="" Visible="False"></asp:Label>
                            &nbsp;<asp:Label ID="lblDeviceSpeed" runat="server" Text="" Visible="False" ></asp:Label>
                                </div>   </center>   
                        </div>
                          
                    </div>
                    <div class="row">
                        <div class="col-sm-10">

  <br />

                            <div class="input-group" style="width: 100%;">
                                <span class="input-group-addon" id="basic-addon3" style="width: 150px;">Scan your Tag Number</span>
                                             
                                <%--   <asp:TextBox ID="txtTagNumber" EnableViewState="true" type="text" runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1" AutoPostBack="True" ></asp:TextBox>--%>
                                <asp:TextBox ID="txtTagNumber" runat="server" type="text" Text="" class="form-control" placeholder="TagNumber" aria-describedby="basic-addon1"></asp:TextBox>




                            </div>
                           
                        </div>
                        <div class="col-sm-2">
     
                            <div class="input-group" style="width: 100%;">
               <br />
                                <asp:Button ID="btnScan" runat="server" Width="100%" Text="Scan" type="button" class="btn btn-danger" OnClick="btnScan_Click" />
                            </div>
             
                        </div>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvTagNumber" ForeColor="#ff0000"  runat="server" ControlToValidate="txtTagNumber" ErrorMessage="*Tag Number Required"></asp:RequiredFieldValidator>
                            <br />
                        <div class="col-sm-10">
                            <br />
                            <div class="input-group" style="width: 100%;">
                             
                                <asp:Label ID="lblResult" runat="server" Text="" Visible="False"></asp:Label>
                            </div>
                            <br />
                        </div>
                        <div>
                            <div class="col-sm-10" style="width: 100%;">
                                <asp:GridView class="table table-striped" ID="GridView1" runat="server" EmptyDataText="No Records Found"></asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <%--  </form>--%>
            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <h1>
                                        <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label></h1>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
