<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="BEPGrid.aspx.cs" Inherits="BEPWeb.BEPGrid" %>--%>

<%@ Page Title="Break Even Grid" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="BEPGrid.aspx.cs" Inherits="BEPWeb.BEPGrid" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript">



        function changeTab(tabIndex) {
            var tabBehavior = $get('<%=TabContainer1.ClientID%>').control;
            tabBehavior.set_activeTabIndex(tabIndex);
        }



    </script>
    <style type="text/css">
        .customerRow
        {
        }
        #custom-menu
        {
            z-index: 1000;
            position: absolute;
            border: solid 2px black;
            background-color: white;
            padding: 5px 0;
            display: none;
        }
        #custom-menu ol
        {
            padding: 0;
            margin: 0;
            list-style-type: none;
            min-width: 130px;
            width: auto;
            max-width: 200px;
            font-family: Verdana;
            font-size: 12px;
        }
        #custom-menu ol li
        {
            margin: 0;
            display: block;
            list-style: none;
            padding: 5px 5px;
        }
        #custom-menu ol li:hover
        {
            background-color: #efefef;
        }
        
        #custom-menu ol li:active
        {
            color: White;
            background-color: #000;
        }
        
        #custom-menu ol .list-devider
        {
            padding: 0px;
            margin: 0px;
        }
        
        #custom-menu ol .list-devider hr
        {
            margin: 2px 0px;
        }
        
        #custom-menu ol li a
        {
            color: Black;
            text-decoration: none;
            display: block;
            padding: 0px 5px;
        }
        #custom-menu ol li a:active
        {
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <ajaxToolkit:ToolkitScriptManager ID="SCManager" runat="server" ScriptMode="Release">
        </ajaxToolkit:ToolkitScriptManager>
        <div>
            <div>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="pnlFilterControls"
                    CollapsedSize="0" Collapsed="true" ExpandControlID="pnlFilter" CollapseControlID="pnlFilter"
                    AutoCollapse="False" AutoExpand="False" TextLabelID="lblDetails" CollapsedText="Filter"
                    ExpandedText="Filter" ImageControlID="Image1" ExpandedImage="~/Images/toggle_collapse.png"
                    CollapsedImage="~/Images/toggle_expand.png" ExpandDirection="Vertical" ScrollContents="false" SuppressPostBack="true" />
                <asp:Panel ID="pnlFilter" runat="server" CssClass="collapsePanelHeader" Width="100%"
                    BackColor="LightGray">
                    <div style="cursor: pointer; vertical-align: middle; width: 100%;">
                        <div style="float: left; margin-left: 20px;">
                            <asp:Label ID="lblDetails" runat="server" Text="Filter" Font-Bold="true" Height="100%"></asp:Label>
                        </div>
                        <div style="float: right; vertical-align: middle;">
                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Images/toggle_expand.png"
                                AlternateText="(Show/Hide Filter...)" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlFilterControls" runat="server" Width="100%" CssClass="collapsePanel"
                    ScrollBars="None" Height="100%">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblUPC" Text="UPC" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUPC" Text="" runat="server" />
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtUPC"
                                    MinimumPrefixLength="3" EnableCaching="true" CompletionSetCount="12" CompletionInterval="1000"
                                    ServiceMethod="GetUPCData" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                    CompletionListCssClass="autocomplete_completionListElement">
                                </ajaxToolkit:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:Label ID="lblVendor" Text="Vendor" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVendor" Text="" runat="server" />
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtVendor"
                                    MinimumPrefixLength="3" EnableCaching="true" CompletionSetCount="12" CompletionInterval="1000"
                                    ServiceMethod="GetVendorData" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                    CompletionListCssClass="autocomplete_completionListElement">
                                </ajaxToolkit:AutoCompleteExtender>
                            </td>
                            <td >
                                <asp:Button ID="btnApplyFilter" Text="Apply" CssClass="button" Width="90px" 
                                    runat="server" onclick="btnApplyFilter_Click" />
                                    </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDesc" Text="Description" runat="server"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDesc" Text="" runat="server" Width="90%" />
                            </td>
                            <td>
                                <asp:Button ID="btnClear" Text="Clear" CssClass="button" Width="90px" 
                                    runat="server" onclick="btnClear_Click" />
                            </td>
                        </tr>
                          <tr>
                            <td>
                                <asp:Label ID="lblPercentage" Text="Profit Percentage" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="dLstPercentage" runat="server" >
                                    <asp:ListItem Value="NONE" >SELECT</asp:ListItem>
                                    <asp:ListItem Value="MINUS50" >Below -50%</asp:ListItem>
                                    <asp:ListItem Value="MINUS2550">-50% =< -25%</asp:ListItem>
                                    <asp:ListItem Value="MINUS025">-25% =< 0%</asp:ListItem>
                                    <asp:ListItem Value="POSITIVE010">0% =< 10%</asp:ListItem>
                                    <asp:ListItem Value="POSITIVE1025">10% =< 25%</asp:ListItem>
                                    <asp:ListItem Value="POSITIVE2550">25% =< 50%</asp:ListItem>
                                    <asp:ListItem Value="POSITIVE50">50% Above</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblQtyToBeBrokeEven" Text="Qty on Hand" runat="server"></asp:Label>
                            </td>
                            <td>
                                
                                 <asp:TextBox ID="txtQtyToBeBrokeEven" Text="0" runat="server" />   
                                  <asp:RangeValidator ID="RangeValidator1" runat="server"
                                ErrorMessage="Range to be 0-100000" ControlToValidate="txtQtyToBeBrokeEven"
                                MaximumValue="100000" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                                              
                            </td>
                             <td></td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div>
                <table class="tableStyle" width="100%">
                    <tr>
                        <td colspan="6" class="clickableCellStyle" style='font-size: large; width: 30'>
                            &nbsp; &nbsp; Summary
                        </td>
                    </tr>
                    <tr>
                        <td class="clickableCellStyle" onclick="changeTab(0);return false;">
                            <center>
                                Total Items
                                <br />
                                <asp:Label ID="lblTotalItems" runat="server" Text=""></asp:Label></center>
                        </td>
                        <td class="clickableCellStyle" onclick="changeTab(1);return false;">
                            <center>
                                Broke Even Items
                                <br />
                                <asp:Label ID="lblProfitItemCnt" runat="server" Text=""></asp:Label></center>
                        </td>
                        <td class="clickableCellStyle" onclick="changeTab(2);return false;">
                            <center>
                                Yet to Break Even Items
                                <br />
                                <asp:Label ID="lblLossItemCnt" runat="server" Text=""></asp:Label></center>
                        </td>
                        <td class="clickableCellStyle" onclick="changeTab(3);return false;">
                            <center>
                                RCVD QTY = SOLD QTY
                                <br />
                                <asp:Label ID="lblEvenItems" runat="server" Text=""></asp:Label></center>
                        </td>
                        <td class="clickableCellStyle" onclick="changeTab(4);return false;">
                            <center>
                               RCVD QTY > SOLD QTY
                                <br />
                                <asp:Label ID="lblNotEvenItems" runat="server" Text=""></asp:Label></center>
                        </td>
                        <td class="cellStyle" id="tdProfitLoss" runat="server">
                            <center>
                                Profit Margin<br />
                                <asp:Label ID="lblProfitMargin" runat="server" Text=""></asp:Label>
                            </center>
                        </td>
                    </tr>
                </table>
                <table class="tableStyle" width="100%">
                    <%-- <tr>
                    <td colspan="4" class="sectionHeaderStyle" style='font-size: large; width: 30'>
                        &nbsp; &nbsp; Filter
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUPC" runat="server" Text="UPC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVendor" runat="server" Text="Vendor"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnFilter" runat="server" Text="Filter" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>--%>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="sectionHeaderStyle" style='font-size: large; width: 30'>
                            &nbsp; &nbsp; Grid
                        </td>
                    </tr>
                </table>
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="100%">
                    <ajaxToolkit:TabPanel runat="server" ID="tabPnlAll" HeaderText="All Items" Enabled="true"
                        ScrollBars="Auto" OnDemandMode="Once">
                        <ContentTemplate>
                            <asp:GridView ID="GridViewHierachical" runat="server" AutoGenerateColumns="False"
                                Width="100%" DataKeyNames="PC_ID" AllowPaging="true" CssClass="GridViewStyle"
                                BorderStyle="Solid" PageSize="50" CellPadding="0" CellSpacing="0" OnPageIndexChanging="GridViewHierachical_PageIndexChanging"
                                EnableViewState="false" AllowSorting="True" OnSorting="gridView_Sorting" OnRowCommand="GridViewHierachical_RowCommand"
                                OnRowDataBound="GridViewHierachical_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="UPC" HeaderText="UPC" SortExpression="UPC">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                        <ItemStyle Width="30%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor">
                                        <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalRCVD" HeaderText="Rcvd Qty">
                                        <ItemStyle Width="4%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalSold" HeaderText="Sold Qty">
                                        <ItemStyle Width="4%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastSaleDate" HeaderText="Last Sale" SortExpression="LastSaleDate"
                                        DataFormatString="{0:d}">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProfitMargin" HeaderText="Total Profit ($)" SortExpression="ProfitMargin"
                                        DataFormatString="{0:F2}">
                                        <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="ProfitPercentage" HeaderText="Profit (%)" SortExpression="ProfitPercentage"
                                        DataFormatString="{0:F2}">
                                        <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <%--<asp:TemplateField ItemStyle-Width="15%" ItemStyle-BorderColor="Black" HeaderStyle-HorizontalAlign="Left"
                                HeaderStyle-BorderWidth="1px" ItemStyle-HorizontalAlign="Left" HeaderText="Detail"
                                HeaderStyle-BorderColor="Black">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="<%# Bind('PC_ID') %>" NavigateUrl='<%# String.Format("BEPDetails.aspx?PID={0}", Eval("PC_ID")) %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Reports" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSelect" runat="server" Text="Select" Width="100px" />
                                            <asp:Panel ID="DropPanel" runat="server" CssClass="ContextMenuPanel" Style="display: none;
                                                visibility: hidden;">
                                                <asp:LinkButton runat="server" ID="LinkButton1" Text="Break Even Details" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="BEPDETAIL" /><br />
                                                <asp:LinkButton runat="server" ID="LinkButton2" Text="Sales Trend" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="SALETREND" /><br />
                                                <asp:LinkButton runat="server" ID="LinkButton3" Text="PO Details" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="PODETAIL" /><br />
                                                <asp:LinkButton runat="server" ID="Option1" Text="Sales Detail" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="SALEDETAIL" />
                                            </asp:Panel>
                                            <ajaxToolkit:DropDownExtender runat="server" ID="DDE" TargetControlID="lblSelect"
                                                DropDownControlID="DropPanel" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                                <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                                <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                                <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                            </asp:GridView>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tbPnlProfit" HeaderText="Broke Even Items"
                        Enabled="true" ScrollBars="Auto" OnDemandMode="Once">
                        <ContentTemplate>
                            <asp:GridView ID="GV_Profit" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="PC_ID" AllowPaging="true" CssClass="GridViewStyle" BorderStyle="Solid"
                                PageSize="50" CellPadding="0" CellSpacing="0" OnPageIndexChanging="GridProfit_PageIndexChanging"
                                EnableViewState="false" AllowSorting="True" OnSorting="gridView_Sorting" OnRowCommand="GridViewHierachical_RowCommand"
                                OnRowDataBound="GV_Profit_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="UPC" HeaderText="UPC" SortExpression="UPC">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                        <ItemStyle Width="30%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor">
                                        <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalRCVD" HeaderText="Rcvd Qty">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalSold" HeaderText="Sold Qty">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastSaleDate" HeaderText="Last Sale" SortExpression="LastSaleDate"
                                        DataFormatString="{0:d}">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProfitMargin" HeaderText="Total Profit ($)" SortExpression="ProfitMargin"
                                        DataFormatString="{0:F2}">
                                        <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="ProfitPercentage" HeaderText="Profit (%)" SortExpression="ProfitPercentage"
                                        DataFormatString="{0:F2}">
                                        <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Reports" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSelect" runat="server" Text="Select" Width="100px" />
                                            <asp:Panel ID="DropPanel" runat="server" CssClass="ContextMenuPanel" Style="display: none;
                                                visibility: hidden;">
                                                <asp:LinkButton runat="server" ID="LinkButton1" Text="Break Even Details" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="BEPDETAIL" /><br />
                                                <asp:LinkButton runat="server" ID="LinkButton2" Text="Sales Trend" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="SALETREND" /><br />
                                                <asp:LinkButton runat="server" ID="LinkButton3" Text="PO Details" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="PODETAIL" /><br />
                                                <asp:LinkButton runat="server" ID="Option1" Text="Sales Detail" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="SALEDETAIL" />
                                            </asp:Panel>
                                            <ajaxToolkit:DropDownExtender runat="server" ID="DDE" TargetControlID="lblSelect"
                                                DropDownControlID="DropPanel" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                                <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                                <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                                <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                            </asp:GridView>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tbPnlLoss" HeaderText="Yet to Break Even Items"
                        Enabled="true" ScrollBars="Auto" OnDemandMode="Once">
                        <ContentTemplate>
                            <asp:GridView ID="GV_Loss" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="PC_ID" AllowPaging="true" CssClass="GridViewStyle" BorderStyle="Solid"
                                PageSize="50" CellPadding="0" CellSpacing="0" OnPageIndexChanging="GridLoss_PageIndexChanging"
                                EnableViewState="false" AllowSorting="True" OnSorting="gridView_Sorting" OnRowCommand="GridViewHierachical_RowCommand"
                                OnRowDataBound="GV_Loss_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="UPC" HeaderText="UPC" SortExpression="UPC">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                        <ItemStyle Width="30%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor">
                                        <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalRCVD" HeaderText="Rcvd Qty">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalSold" HeaderText="Sold Qty">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastSaleDate" HeaderText="Last Sale" SortExpression="LastSaleDate"
                                        DataFormatString="{0:d}">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProfitMargin" HeaderText="Total Profit ($)" SortExpression="ProfitMargin"
                                        DataFormatString="{0:F2}">
                                        <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProfitPercentage" HeaderText="Profit (%)" SortExpression="ProfitPercentage"
                                        DataFormatString="{0:F2}">
                                        <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="Reports" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSelect" runat="server" Text="Select" Width="100px" />
                                            <asp:Panel ID="DropPanel" runat="server" CssClass="ContextMenuPanel" Style="display: none;
                                                visibility: hidden;">
                                                <asp:LinkButton runat="server" ID="LinkButton1" Text="Break Even Details" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="BEPDETAIL" /><br />
                                                <asp:LinkButton runat="server" ID="LinkButton2" Text="Sales Trend" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="SALETREND" /><br />
                                                <asp:LinkButton runat="server" ID="LinkButton3" Text="PO Details" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="PODETAIL" /><br />
                                                <asp:LinkButton runat="server" ID="Option1" Text="Sales Detail" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="SALEDETAIL" />
                                            </asp:Panel>
                                            <ajaxToolkit:DropDownExtender runat="server" ID="DDE" TargetControlID="lblSelect"
                                                DropDownControlID="DropPanel" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                                <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                                <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                                <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                            </asp:GridView>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tabPnlEven" HeaderText="Even Items"
                        Enabled="true" ScrollBars="Auto" OnDemandMode="Once">
                        <ContentTemplate>
                            <asp:GridView ID="gdViewEvenItems" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="PC_ID" AllowPaging="true" CssClass="GridViewStyle" BorderStyle="Solid"
                                PageSize="50" CellPadding="0" CellSpacing="0" OnPageIndexChanging="GridEven_PageIndexChanging"
                                EnableViewState="false" AllowSorting="True" OnSorting="gridView_Sorting" OnRowCommand="GridViewHierachical_RowCommand"
                                OnRowDataBound="GV_Even_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="UPC" HeaderText="UPC" SortExpression="UPC">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                        <ItemStyle Width="30%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor">
                                        <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalRCVD" HeaderText="Rcvd Qty">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalSold" HeaderText="Sold Qty">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastSaleDate" HeaderText="Last Sale" SortExpression="LastSaleDate"
                                        DataFormatString="{0:d}">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProfitMargin" HeaderText="Total Profit ($)" SortExpression="ProfitMargin"
                                        DataFormatString="{0:F2}">
                                        <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProfitPercentage" HeaderText="Profit (%)" SortExpression="ProfitPercentage"
                                        DataFormatString="{0:F2}">
                                        <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="Reports" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSelect" runat="server" Text="Select" Width="100px" />
                                            <asp:Panel ID="DropPanel" runat="server" CssClass="ContextMenuPanel" Style="display: none;
                                                visibility: hidden;">
                                                <asp:LinkButton runat="server" ID="LinkButton1" Text="Break Even Details" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="BEPDETAIL" /><br />
                                                <asp:LinkButton runat="server" ID="LinkButton2" Text="Sales Trend" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="SALETREND" /><br />
                                                <asp:LinkButton runat="server" ID="LinkButton3" Text="PO Details" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="PODETAIL" /><br />
                                                <asp:LinkButton runat="server" ID="Option1" Text="Sales Detail" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="SALEDETAIL" />
                                            </asp:Panel>
                                            <ajaxToolkit:DropDownExtender runat="server" ID="DDE" TargetControlID="lblSelect"
                                                DropDownControlID="DropPanel" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                                <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                                <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                                <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                            </asp:GridView>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tbPnlQtyOnHandh" HeaderText="Qty on hand more than 0"
                        Enabled="true" ScrollBars="Auto" OnDemandMode="Once">
                        <ContentTemplate>
                            <asp:GridView ID="gdViewQOH" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="PC_ID" AllowPaging="true" CssClass="GridViewStyle" BorderStyle="Solid"
                                PageSize="50" CellPadding="0" CellSpacing="0" OnPageIndexChanging="GridQOH_PageIndexChanging"
                                EnableViewState="false" AllowSorting="True" OnSorting="gridView_Sorting" OnRowCommand="GridViewHierachical_RowCommand"
                                OnRowDataBound="GV_QOH_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="UPC" HeaderText="UPC" SortExpression="UPC">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                        <ItemStyle Width="30%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor">
                                        <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalRCVD" HeaderText="Rcvd Qty">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalSold" HeaderText="Sold Qty">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastSaleDate" HeaderText="Last Sale" SortExpression="LastSaleDate"
                                        DataFormatString="{0:d}">
                                        <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProfitMargin" HeaderText="Total Profit ($)" SortExpression="ProfitMargin"
                                        DataFormatString="{0:F2}">
                                        <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProfitPercentage" HeaderText="Profit (%)" SortExpression="ProfitPercentage"
                                        DataFormatString="{0:F2}">
                                        <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="Reports" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSelect" runat="server" Text="Select" Width="100px" />
                                            <asp:Panel ID="DropPanel" runat="server" CssClass="ContextMenuPanel" Style="display: none;
                                                visibility: hidden;">
                                                <asp:LinkButton runat="server" ID="LinkButton1" Text="Break Even Details" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="BEPDETAIL" /><br />
                                                <asp:LinkButton runat="server" ID="LinkButton2" Text="Sales Trend" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="SALETREND" /><br />
                                                <asp:LinkButton runat="server" ID="LinkButton3" Text="PO Details" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="PODETAIL" /><br />
                                                <asp:LinkButton runat="server" ID="Option1" Text="Sales Detail" CssClass="button-link"
                                                    CommandArgument='<%#Bind("PC_ID")%>' CommandName="SALEDETAIL" />
                                            </asp:Panel>
                                            <ajaxToolkit:DropDownExtender runat="server" ID="DDE" TargetControlID="lblSelect"
                                                DropDownControlID="DropPanel" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                                <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                                <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                                <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                            </asp:GridView>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
                <asp:HiddenField ID="hndExpandedChild" runat="server" Value="" />
            </div>
        </div>
    </div>
</asp:Content>
