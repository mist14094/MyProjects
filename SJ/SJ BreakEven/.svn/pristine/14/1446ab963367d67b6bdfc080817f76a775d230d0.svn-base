<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BEPWeb._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1">
        <ControlBundles>
            <ajaxToolkit:ControlBundle Name="Accordion" />
        </ControlBundles>
    </ajaxToolkit:ToolkitScriptManager>
    <table class="tableStyle" width="100%">
        <tr>
            <td colspan="4" class="sectionHeaderStyle">
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
                    Profit Making Items
                    <br />
                    <asp:Label ID="lblProfitItemCnt" runat="server" Text=""></asp:Label></center>
            </td>
            <td class="clickableCellStyle" onclick="changeTab(2);return false;">
                <center>
                    Loss Making Items
                    <br />
                    <asp:Label ID="lblLossItemCnt" runat="server" Text=""></asp:Label></center>
            </td>
            <td class="cellStyle" id="tdProfitLoss" runat="server">
                <center>
                    Profit Margin<br />
                    <asp:Label ID="lblProfitMargin" runat="server" Text=""></asp:Label>
                </center>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="sectionHeaderStyle">
                &nbsp; &nbsp; Week's Sales Snapshot
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Chart ID="chStoreSales" runat="server" Width="900px" Height="500px" Palette="Bright"
                    IsValueShownAsLabel="true" IsVisibleInLegend="false" BackColor="WhiteSmoke">
                    <Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="chStoreSales">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="sectionHeaderStyle">
                &nbsp; &nbsp; Week's Highlight
            </td>
        </tr>
    </table>
    <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="Limit"
        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Height="400px" >
        <Panes>
            <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                <Header>
                    <a href=""> 25 Most Selling Items</a></Header>
                <Content>
                    <asp:GridView ID="gvTMSI" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="PC_ID" AllowPaging="true" CssClass="GridViewStyle" BorderStyle="Solid"
                        PageSize="50" CellPadding="0" CellSpacing="0" EnableViewState="false">
                        <Columns>
                            <asp:BoundField DataField="UPC" HeaderText="UPC">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                <ItemStyle Width="30%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                                <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UOM" HeaderText="UOM">
                                <ItemStyle Width="4%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SoldQty" HeaderText="Sold Qty">
                                <ItemStyle Width="4%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalSales" HeaderText="Total Sales" DataFormatString="{0:C}">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                        <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                        <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                        <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                    </asp:GridView>
                </Content>
            </ajaxToolkit:AccordionPane>
            <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server">
                <Header>
                    <a href=""> 25 Least Selling Items</a></Header>
                <Content>
                       <asp:GridView ID="gvTLSI" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="PC_ID" AllowPaging="true" CssClass="GridViewStyle" BorderStyle="Solid"
                        PageSize="50" CellPadding="0" CellSpacing="0" EnableViewState="false">
                        <Columns>
                            <asp:BoundField DataField="UPC" HeaderText="UPC">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                <ItemStyle Width="30%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                                <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UOM" HeaderText="UOM">
                                <ItemStyle Width="4%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SoldQty" HeaderText="Sold Qty">
                                <ItemStyle Width="4%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalSales" HeaderText="Total Sales" DataFormatString="{0:C}">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                        <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                        <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                        <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                    </asp:GridView>
                </Content>
            </ajaxToolkit:AccordionPane>
            <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server">
                <Header>
                    <a href=""> 25 Revenue Generator</a></Header>
                <Content>
                      <asp:GridView ID="gvTRG" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="PC_ID" AllowPaging="true" CssClass="GridViewStyle" BorderStyle="Solid"
                        PageSize="50" CellPadding="0" CellSpacing="0" EnableViewState="false">
                        <Columns>
                            <asp:BoundField DataField="UPC" HeaderText="UPC">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                <ItemStyle Width="30%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                                <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UOM" HeaderText="UOM">
                                <ItemStyle Width="4%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SoldQty" HeaderText="Sold Qty">
                                <ItemStyle Width="4%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalSales" HeaderText="Total Sales" DataFormatString="{0:C}">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                        <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                        <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                        <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                    </asp:GridView>
              
                </Content>
            </ajaxToolkit:AccordionPane>
            <ajaxToolkit:AccordionPane ID="AccordionPane4" runat="server">
                <Header>
                    <a href=""> 5 Most Selling Vendor</a></Header>
                <Content>
                       <asp:GridView ID="gvTMSV" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true" CssClass="GridViewStyle" BorderStyle="Solid"
                        PageSize="50" CellPadding="0" CellSpacing="0" EnableViewState="false">
                        <Columns>
                           <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                                <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SoldQty" HeaderText="Sold Qty">
                                <ItemStyle Width="4%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalSales" HeaderText="Total Sales" DataFormatString="{0:C}">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                        <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                        <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                        <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                    </asp:GridView>
              
                </Content>
            </ajaxToolkit:AccordionPane>
            <ajaxToolkit:AccordionPane ID="AccordionPane5" runat="server">
                <Header>
                    <a href=""> 5 Least Selling Vendor</a></Header>
                <Content>
                      <asp:GridView ID="gvTLSV" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true" CssClass="GridViewStyle" BorderStyle="Solid"
                        PageSize="50" CellPadding="0" CellSpacing="0" EnableViewState="false">
                        <Columns>
                           <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                                <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SoldQty" HeaderText="Sold Qty">
                                <ItemStyle Width="4%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalSales" HeaderText="Total Sales" DataFormatString="{0:C}">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                        <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                        <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                        <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                    </asp:GridView>
              
                </Content>
            </ajaxToolkit:AccordionPane>
        </Panes>
    </ajaxToolkit:Accordion>
</asp:Content>
