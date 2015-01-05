<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PODetails.aspx.cs" Inherits="BEPWeb.PODetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <ajaxToolkit:ToolkitScriptManager ID="SCManager" runat="server" ScriptMode="Release">
        </ajaxToolkit:ToolkitScriptManager>
        <table width='100%'>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td>
                    <input onclick="window.history.back().back()" type="button" value="Back">
                </td>
                <td colspan="3">
                </td>
                <td align="right">
                    <input onclick="window.location.reload()" type="button" value="Reset"></input><input
                        type="button" onclick="printDiv('printableArea')" value="Print!" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
                <td colspan="5">
                </td>
            </tr>
        </table>
    </div>
    <div id="printableArea">
        <table class="tableStyle" width="100%">
            <tr>
                <td colspan="5" class="cellStyle" style='font-size: large; width: 30'>
                    &nbsp; &nbsp; PO Details
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="5" class="sectionHeaderStyle" style='width: 30'>
                    &nbsp; &nbsp; Product Detail
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    UPC
                </td>
                <td>
                    <asp:Label ID="lblUPC" runat="server" Text=""></asp:Label>
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    SKU
                </td>
                <td>
                    <asp:Label ID="lblSKU" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Stock Code
                </td>
                <td>
                    <asp:Label ID="lblStkCode" runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Vendor
                </td>
                <td>
                    <asp:Label ID="lblVendor" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Description
                </td>
                <td colspan="4">
                    <asp:Label ID="lblDesc" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Total COGS
                </td>
                <td>
                    <asp:Label ID="lblTotCOGS" runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Quantity Received
                </td>
                <td>
                    <asp:Label ID="lblQtyRcvd" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Total Sales
                </td>
                <td>
                    <asp:Label ID="lblTotSale" runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Quantity Sold
                </td>
                <td>
                    <asp:Label ID="lblQtySold" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Current Status
                </td>
                <td id="tdStatus" runat="server">
                    <asp:Label ID="lblStatus" CssClass="fieldCaption" runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Profit Margin
                </td>
                <td>
                    <asp:Label ID="lblMargin" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td class="fieldCaption">
                    Avg COGS
                </td>
                <td>
                    <asp:Label ID="lblAvgCOGS" runat="server" Text="" /><asp:Label ID="lblAvgCOGSSym"
                        runat="server" Text="" />
                </td>
                <td>
                </td>
                <td class="fieldCaption">
                    Avg Sell Price
                </td>
                <td>
                    <asp:Label ID="lblAvgSellPrice" runat="server" Text="" /><asp:Label ID="lblAvgSellPriceSym"
                        runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td colspan="5" class="sectionHeaderStyle" style='width: 30'>
                    &nbsp; &nbsp; Sales Details
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <asp:GridView ID="gv_PODetail" runat="server" AutoGenerateColumns="False" Width="100%"
                    DataKeyNames="PC_ID" AllowPaging="true" CssClass="GridViewStyle" BorderStyle="Solid"
                    PageSize="50" CellPadding="0" CellSpacing="0" OnPageIndexChanging="GridViewHierachical_PageIndexChanging"
                    EnableViewState="false" AllowSorting="True" OnSorting="gridView_Sorting">
                    <Columns>
                        <asp:BoundField DataField="RefNumber" HeaderText="PO Number" SortExpression="RefNumber">
                            <ItemStyle Width="10%" CssClass="GridViewItemStyle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RefSystem" HeaderText="System" SortExpression="RefSystem">
                            <ItemStyle Width="10%" CssClass="GridViewItemStyle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Supplier" HeaderText="Vendor" SortExpression="Supplier">
                            <ItemStyle Width="15%" CssClass="GridViewItemStyle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QTY_RCV" HeaderText="Qty Rcvd">
                            <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FINAL_COST" HeaderText="Unit Cost" DataFormatString="{0:F2}"
                            SortExpression="FINAL_COST">
                            <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" DataFormatString="{0:F2}">
                            <ItemStyle Width="5%" CssClass="GridViewItemStyle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DateCreated" HeaderText="Date" DataFormatString="{0:d}"
                            SortExpression="DateCreated">
                            <ItemStyle Width="10%" CssClass="GridViewItemStyle" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                    <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                    <HeaderStyle CssClass="GridViewHeader" Height="22px"></HeaderStyle>
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                    <AlternatingRowStyle CssClass="GridViewAlternativeRow"></AlternatingRowStyle>
                </asp:GridView>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td colspan="5" class="sectionHeaderStyle" style='width: 30'>
                    &nbsp; &nbsp; Other Links
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="lkBEPDetails" Text="BreakEven Details" runat="server"></asp:LinkButton>
                    <br />
                    <asp:LinkButton ID="lkSalesTrend" Text="Sales Trend" runat="server"></asp:LinkButton>
                    <br />
                    <asp:LinkButton ID="lkSalesDetail" Text="Sales Detail" runat="server"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
