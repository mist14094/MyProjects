<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatagoriesDetail.aspx.cs" Inherits="PriceChangerWeb.CatagoriesDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <style type="text/css">
        .auto-style2 {
            font-size: 12pt;
        }

        .auto-style3 {
            text-align: center;
        }

        .auto-style4 {
            font-size: large;
        }
    </style>
</head>
<body style="min-width: 600px;">
    <form id="form1" runat="server">
        <div>
            <div class="auto-style3">
                <br/>
                <span class="auto-style2"><strong style="text-align: center">Product Details<br />
                </strong></span>
                <asp:Label ID="lblSKU" runat="server" Text="" CssClass="auto-style4"></asp:Label>
                <br />
            </div>

        </div>
        <br />
        <div style="margin: 20px 20px 20px 20px;">
            <div>
                <center>  <span class="auto-style2"><strong style="text-align: center"> <asp:Label ID="lblCatg" runat="server" Text=""></asp:Label>
                    <br />
                    </strong></span></center>
            </div>
            <asp:GridView ID="gvCatag" runat="server" CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" Width="100%" Font-Size="Medium" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="UPC" HeaderText="UPC" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SKU" HeaderText="SKU" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DESC" HeaderText="Description" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
                
            </asp:GridView>
        </div>

    </form>
    <p>
        n</p>
</body>
</html>
