<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="ViewTagRemaining.aspx.cs" Inherits="AdWeb.ViewTagRemaining" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="media" style="max-width: 992px; margin-left: auto; margin-right: auto;">

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title" style="align-self: center;">Search Tag</h3>
            </div>

            <div class="panel-body">
                <div class="input-group" style="width: 100%;">

                    <asp:TextBox ID="txtSearchString" placeholder="Search by Lastname or Tagnumber - Min 3 Letters" type="text" runat="server" Width="100%" class="form-control" aria-describedby="basic-addon1"></asp:TextBox>
                </div>
                <br />
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" AllowPaging="True" PageSize="20" CellSpacing="-1" GridLines="Both" OnPageIndexChanged="RadGrid1_PageIndexChanged" OnSortCommand="RadGrid1_SortCommand">
                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                    <MasterTableView>
                        <Columns>
                            <telerik:GridHyperLinkColumn AllowSorting="True" DataTextField="TagNumber" FilterControlAltText="Filter column column" HeaderText="Tag Number" UniqueName="TagNumber" SortExpression="TagNumber">
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn DataTextField="LacrosseThrow" FilterControlAltText="Filter column column" HeaderText="Lacrosse" UniqueName="LacrosseThrow" SortExpression="LacrosseThrow" >
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn DataTextField="SoftBallThrow" FilterControlAltText="Filter column column" HeaderText="Soft Ball" UniqueName="SoftBallThrow" SortExpression="SoftBallThrow">
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn DataTextField="KidsZone" FilterControlAltText="Filter column column" HeaderText="Kids Zone" UniqueName="KidsZone" SortExpression="KidsZone">
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn DataTextField="Maze" FilterControlAltText="Filter column column" HeaderText="Maze" UniqueName="Maze" SortExpression="Maze">
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn DataTextField="SoccerDarts" FilterControlAltText="Filter column column" HeaderText="Soccer Darts" UniqueName="SoccerDarts" SortExpression="SoccerDarts">
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn DataTextField="RopeCourseInMinutes" FilterControlAltText="Filter column column" HeaderText="Rope Course" UniqueName="RopeCourseInMinutes" SortExpression="RopeCourseInMinutes" >
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn DataTextField="ZipLine" FilterControlAltText="Filter column column" HeaderText="ZipLine" UniqueName="ZipLine" SortExpression="ZipLine">
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn DataTextField="Tubing" FilterControlAltText="Filter column column" HeaderText="Tubing" UniqueName="Tubing" SortExpression="Tubing">
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn DataTextField="JumpZone" FilterControlAltText="Filter column column" HeaderText="Jump Zone" UniqueName="JumpZone" SortExpression="JumpZone">
                            </telerik:GridHyperLinkColumn>

                            <telerik:GridDateTimeColumn DataField="ExpirationDate" HeaderText="Expiry Date" DataFormatString="{0:dd-MMM-yy}"
                                UniqueName="ExpirationDate" PickerType="DatePicker" />

                        </Columns>
                        <PagerStyle AlwaysVisible="True" />
                    </MasterTableView>
                    <PagerStyle AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <table class="nav-justified">
                    <tr>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" type="button" class="btn btn btn-success center-block" Width="80%" OnClick="btnSearch_Click" /></td>
                        <td>
                            <asp:Button ID="btnTop20Waiver" runat="server" Text="Top 20 Waivers" type="button" class="btn btn btn-success center-block" Width="80%" OnClick="btnTop20Waiver_Click" /></td>
                        <td>
                            <asp:Button ID="btnClear" runat="server" Text="Clear" type="button" class="btn btn btn-success center-block" Width="80%" OnClick="btnClear_Click" /></td>
                    </tr>
                </table>



            </div>
        </div>

    </div>

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
