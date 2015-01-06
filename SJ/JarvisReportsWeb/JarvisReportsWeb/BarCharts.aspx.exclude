<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BarCharts.aspx.cs" Inherits="BarCharts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <div id="page-inner">
        <div>
            <div class="row">
                <div class="col-md-12">
                    <h2>
                        Create Your Reports
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
                                Select Chart Type :
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddViewsList" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddViewsList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                X-Axis :</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddXValue" runat="server" Width="100%"  >
                                </asp:DropDownList>
                               <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Y-Axis :</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddYValue" runat="server" Width="100%"  >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" /> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                        <br />
                                
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto"
                        >
                       <asp:GridView ID="gvSampleData" runat="server" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                        </asp:Panel>
                </div>
    </div>
    </div>
    <!-- /. ROW  -->
    <hr />
    </div>
</asp:Content>

