<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesDetails.aspx.cs" Inherits="QuickSales.SalesDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />

    <style type="text/css">
        .auto-style2 {
            text-align: center;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top: 30px;"></div>
        <div style="margin-top: 30px;">
            <center><h3><asp:Label runat="server" ID="lblVendorname"></asp:Label> </h3>
            <p>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl ="javascript:history.go(-1);">Go back</asp:HyperLink> | <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl ="Startup.aspx">Home</asp:HyperLink>
            </p>
            <p>
                 </p>
            </center>
        </div>
        <div>
            <div style="margin: 30px;">

                <table class="mGrid">
                    <tr>
                        <th scope="col" style="color:White;">Store Number</th>
                        <th scope="col" style="color:White;">
                            <center>
        
        &nbsp;
        
        &nbsp;
                                Ordered Instances</center>
                        </th>
                        <th scope="col" style="color:White;">Sold out Instances [In Finance Period]</th>
                    </tr>
                    <tr>
                        <td style="padding-left: 50px"> Store 2</td>
                        <td style="margin: 0px auto 0px auto" class="auto-style2">
                            <center>   
                                <asp:Label ID="lblStore2Ordered" runat="server" Text=""></asp:Label>
                            </center>
                        </td>
                        <td class="auto-style2">
                            <asp:Label ID="lblStore2Sold" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 50px">Store 3</td>
                        <td style="margin: 0px auto 0px auto" class="auto-style2">
                            <center>
                                <asp:Label ID="lblStore3Ordered" runat="server" Text=""></asp:Label>
                       </center>
                        </td>
                        <td class="auto-style2">
                            <asp:Label ID="lblStore3Sold" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 50px">Store 4</td>
                        <td style="margin: 0px auto 0px auto" class="auto-style2">
                            <asp:Label ID="lblStore4Ordered" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:Label ID="lblStore4Sold" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 50px">Store 6</td>
                        <td style="margin: 0px auto 0px auto" class="auto-style2">
                            <asp:Label ID="lblStore6Ordered" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:Label ID="lblStore6Sold" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 50px">Store 7</td>
                        <td style="margin: 0px auto 0px auto" class="auto-style2">
                            <asp:Label ID="lblStore7Ordered" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:Label ID="lblStore7Sold" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 50px">Store 9</td>
                        <td style="margin: 0px auto 0px auto" class="auto-style2">
                            <asp:Label ID="lblStore9Ordered" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:Label ID="lblStore9Sold" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 50px">Store 10</td>
                        <td style="margin: 0px auto 0px auto" class="auto-style2">
                            <asp:Label ID="lblStore10Ordered" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:Label ID="lblStore10Sold" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 50px">Store 12</td>
                        <td style="margin: 0px auto 0px auto" class="auto-style2">
                            <asp:Label ID="lblStore12Ordered" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:Label ID="lblStore12Sold" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 50px"> <b>Total</b></td>
                        <td style="margin: 0px auto 0px auto" class="auto-style2"><b>
                            <asp:Label ID="lblStoreTotalOrdered" runat="server" Text=""></asp:Label></b>
                        </td>
                        <td class="auto-style2"><b>
                            <asp:Label ID="lblStoreTotalSold" runat="server" Text=""></asp:Label></b>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin: 50px;">
                &nbsp;
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" AllowSorting="True" 
                    PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt" OnSorting="GridView1_Sorting">
                    <PagerSettings Position="TopAndBottom" />
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="RecordDate" HeaderText="Date"  SortExpression="RecordDate" DataFormatString="{0:MM-dd-yyyy}"  >
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Invoice" HeaderText="Invoice" SortExpression="Invoice" >
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Itemstr" HeaderText="UPC"  SortExpression="Itemstr">
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QTY" HeaderText="Quantity"  SortExpression="QTY">
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Unit" HeaderText="Total Units"  SortExpression="Unit">
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost"  SortExpression="UnitCost">
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UnitRetail" HeaderText="Unit Retail"  SortExpression="UnitRetail">
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StoreNumber" HeaderText="Store Number"  SortExpression="StoreNumber">
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
