<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MainChartMainConfig.aspx.cs" Inherits="MainChartMainConfig" %>

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
                                Chart Name
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtChartName" runat="server" Width="70%"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Chart Description</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtChartDesc" runat="server" Width="70%"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                        <br />
                                
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Chart Type</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlChartTypes" runat="server" Width="70%">
                                </asp:DropDownList>
                                
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Database Connection Name</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlSelectDataBase" runat="server" Width="70%" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectDataBase_SelectedIndexChanged">
                                </asp:DropDownList>
                                
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                DataBase Element Name</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlDataBaseElements" runat="server" Width="70%" >
                                </asp:DropDownList>
                                
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" class="btn btn-danger square-btn-adjust" Text="Save & Next" OnClick="Button1_Click" />
                                
                            &nbsp;&nbsp;
                                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                                
                                <br />
                                <br />
                                <asp:Button ID="btnProperties" runat="server" class="btn btn-danger square-btn-adjust" Text="Chart Properties" OnClick="btnProperties_Click"  />
                                 &nbsp; &nbsp; <asp:Button ID="btnAxis" runat="server" class="btn btn-danger square-btn-adjust" Text="Axis Properties" OnClick="btnAxis_Click" />
                                &nbsp; &nbsp; <asp:Button ID="btnSampleData" runat="server" class="btn btn-danger square-btn-adjust" Text="Sample Data" OnClick="btnSampleData_Click"  />
                                 &nbsp; &nbsp; <asp:Button ID="btnPreview" runat="server" class="btn btn-danger square-btn-adjust" Text="Preview Chart" OnClick="btnPreview_Click"  />
                               
                                <br />
                                <br />
                                <asp:Button ID="btbBack" runat="server" class="btn btn-danger square-btn-adjust" Text="Go Back" OnClick="btbBack_Click"  />
                               
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </div>
    </div>
    </div>
    <!-- /. ROW  -->
    <hr />
    </div>
</asp:Content>

