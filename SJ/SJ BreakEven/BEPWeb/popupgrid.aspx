<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popupgrid.aspx.cs" Inherits="BEPWeb.popupgrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>BreakEven</title>
     <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color:White;">
    <form id="form1" runat="server">
    <div style="margin-left:2%; margin-right:2%;">
                   
<div  >
<br /><br />
 <table class="tableStyle" width="80%">
        <tr>
            <td colspan="4" class="sectionHeaderStyle">
               <center>
    <asp:Label ID="lblSummary" runat="server" Text="Summary"></asp:Label></center>
            </td>
        </tr></table>
        <br /><br />
        <asp:GridView ID="grdPointerHeader" runat="server" AutoGenerateColumns="False" Width="100%"
                       CssClass="GridViewStyle" 
                        CellPadding="0" CellSpacing="0" EnableViewState="false">
                         <HeaderStyle  Height="75px" BackColor="#4b6c9e"  Font-Size="14px"
                             Font-Bold="True" ForeColor="White"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="StoreName" HeaderText="Store Name">
                                <ItemStyle Width="20%" CssClass="GridViewItemStyle"  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalSales" HeaderText="Total Sales">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GrossMargin" HeaderText="Gross Margin">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AverageTransaction" HeaderText="Average Transaction">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HighestTransaction" HeaderText="Highest Transaction">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle"  HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="Profit %" HeaderText="Profit%">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MedianTransaction" HeaderText="Median Transaction">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="topselling" HeaderText="Top selling product by volume">
                                <ItemStyle Width="20%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                                <asp:BoundField DataField="TopRevenue" HeaderText="Top revenue generator">
                                <ItemStyle Width="20%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                        <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                        <AlternatingRowStyle  BackColor="LightGray"></AlternatingRowStyle>
        </asp:GridView>
        </div>
         <br /><br />
          <asp:GridView ID="grdPointerSummary" runat="server" AutoGenerateColumns="False" Width="100%"
                        CssClass="GridViewStyle"
                       CellPadding="0" CellSpacing="0" EnableViewState="false">
                         <HeaderStyle  Height="75px" BackColor="#4b6c9e"  Font-Size="14px"
                             Font-Bold="True" ForeColor="White"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="UPC" HeaderText="UPC">
                                <ItemStyle Width="10%" CssClass="GridViewItemStyle"  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="ITEMDESC" HeaderText="Item Description">
                                <ItemStyle Width="22%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                                <ItemStyle Width="20%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalSales" HeaderText="Total Sales">
                                <ItemStyle Width="10%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AveragePrice" HeaderText="Average Price">
                                <ItemStyle Width="10%" CssClass="GridViewItemStyle"  HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Gross Sales" HeaderText="Gross Sales">
                                <ItemStyle Width="8%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AvgCogs" HeaderText="Average COGS">
                                <ItemStyle Width="10%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                                <asp:BoundField DataField="AVGMargin" HeaderText="Average Margin">
                                <ItemStyle Width="10%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                        <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                        <AlternatingRowStyle BackColor="LightGray"></AlternatingRowStyle>
        </asp:GridView>
   
          <asp:GridView ID="grdLegend" runat="server" AutoGenerateColumns="False" Width="100%"
                        CssClass="GridViewStyle" 
                       CellPadding="0" CellSpacing="0" EnableViewState="false">
                         <HeaderStyle  Height="75px" BackColor="#4b6c9e"  Font-Size="14px"
                             Font-Bold="True" ForeColor="White"></HeaderStyle>
                        <Columns>
                 
                            <asp:BoundField DataField="StoreName" HeaderText="Store Name">
                                <ItemStyle Width="20%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalSales" HeaderText="Total Sales">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GrossMargin" HeaderText="Gross Margin">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AverageTransaction" HeaderText="Average Transaction">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle"  HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HighestTransaction" HeaderText="Highest Transaction">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                                   <asp:BoundField DataField="Profit %" HeaderText="Profit%">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Median" HeaderText="Median Transaction">
                                <ItemStyle Width="5%" CssClass="GridViewItemStyle" HorizontalAlign="Center" />
                            </asp:BoundField>
                                  
                            <asp:BoundField DataField="TopSelling" HeaderText="Top selling product by volume">
                                <ItemStyle Width="25%" CssClass="GridViewItemStyle"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="TopRevenueGen" HeaderText="Top revenue generator">
                                <ItemStyle Width="25%" CssClass="GridViewItemStyle" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>
                        <RowStyle CssClass="GridViewRow" Height="24px"></RowStyle>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
                        <AlternatingRowStyle BackColor="LightGray"></AlternatingRowStyle>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
