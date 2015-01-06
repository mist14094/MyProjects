<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MainChart.aspx.cs" Inherits="MainChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div id="page-inner">
        <div>
            <div class="row">
                <div class="col-md-12">
                    <h2>
                        Create/Edit Your Reports
                    </h2>
                    <h5>
                        This page will help you to create your own report with easy configurations.
                    </h5>
                    <table class="nav-justified">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Select Chart Name :
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddViewsList" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddViewsList_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Or </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddNew" runat="server" Text="Add New Chart" OnClick="btnNext_Click" /> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                        <br />
                                
                            </td>
                        </tr>
                    </table>
                </div>
    </div>
    </div>
    <!-- /. ROW  -->
    <hr />
    </div>
</asp:Content>

