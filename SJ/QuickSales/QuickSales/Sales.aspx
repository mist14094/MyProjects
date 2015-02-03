<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="QuickSales.Sales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top: 30px;"></div>
        <div style="margin-top: 30px;">
            <center><h3><asp:Label runat="server" ID="lblVendorname"></asp:Label> </h3>
            <p>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Startup.aspx">Go back to vendor</asp:HyperLink>
            </p>
            <p>
                 </p>
            </center>
        </div>
        <div>
            <div style="margin-top: 30px;">

                <table class="auto-style1">
                    <tr>
                        <td>&nbsp;</td>
                        <td style="margin: 0px auto 0px auto">
                            <center>
        <asp:DropDownList ID="ddlSearchCriteria" runat="server">
            <asp:ListItem Value="0">--Select---</asp:ListItem>
            <asp:ListItem Value="UPC">UPC</asp:ListItem>
            <asp:ListItem>Description</asp:ListItem>
               </asp:DropDownList>
        
        &nbsp;
        
        <asp:TextBox ID="srchTextBox" runat="server" Width="500px"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
               <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
               </center>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="margin: 0px auto 0px auto">
                            <center>   <asp:Label ID="lblcount" runat="server" Text=""></asp:Label></center>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="margin: 0px auto 0px auto">
                            <center>
                       </center>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div style="margin: 50px;">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" AllowSorting="True" 
                    PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt" OnSorting="GridView1_Sorting">
                    <PagerSettings Position="TopAndBottom" />
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                UPC
                            </HeaderTemplate>
                            <ItemTemplate> <asp:HyperLink runat="server" ID="hypLink"  DataField="UPC" HeaderText="UPC"  SortExpression="UPC" Text='<%#Eval("UPC")%>' NavigateUrl='<%#"SalesDetails.aspx?UPC=" + Eval("UPC") + "&ID=" + Request.QueryString["ID"]%>' ></asp:HyperLink></ItemTemplate>
                                      
                        </asp:TemplateField>
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" >   <HeaderStyle ForeColor="White" /></asp:BoundField>
                        <asp:BoundField DataField="OrderedQty" HeaderText="Ordered Quantity"  SortExpression="OrderedQty">   <HeaderStyle ForeColor="White" /></asp:BoundField>
                        <asp:BoundField DataField="TotalInstanceOrdered" HeaderText="Ordered Instances" SortExpression="TotalInstanceOrdered">   <HeaderStyle ForeColor="White" /></asp:BoundField>
                        <asp:BoundField DataField="SoldoutinFinancePeriod" HeaderText="Sold Out Instances"  SortExpression="SoldoutinFinancePeriod">   <HeaderStyle ForeColor="White" /></asp:BoundField>


                    </Columns>

                    <PagerStyle CssClass="pgr"></PagerStyle>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
