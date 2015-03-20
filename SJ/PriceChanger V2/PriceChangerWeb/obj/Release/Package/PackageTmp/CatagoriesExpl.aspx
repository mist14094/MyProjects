<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatagoriesExpl.aspx.cs" Inherits="PriceChangerWeb.CatagoriesExpl" %>

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
                <span class="auto-style2"><strong style="text-align: center">Product Categorization<br />
                </strong></span><span class="auto-style4">UPC : </span>
                <asp:Label ID="lblUPC" runat="server" Text="" CssClass="auto-style4"></asp:Label>
                <span class="auto-style4">&nbsp;| SKU : </span>
                <asp:Label ID="lblSKU" runat="server" Text="" CssClass="auto-style4"></asp:Label>
                <br />
            </div>

        </div>
        <br />
        <div style="margin: 20px 20px 20px 20px;">
            <div>
                <center>  <span class="auto-style2"><strong style="text-align: center"> <asp:Label ID="lblCatg" runat="server" Text=""></asp:Label></strong></span></p></center>
            </div>
            <asp:GridView ID="gvCatag" runat="server" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Width="100%" AutoGenerateColumns="False" Font-Size="Medium">
                <Columns>
                    
                    <asp:TemplateField>
                        <HeaderStyle Height="30px" />
                        <ItemTemplate>
                            <a href="#" onclick="javascript:w= window.open('<%# "CatagoriesDetail.aspx?CatagID=" +Eval("Sno")%>','DownloadFile','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');return false;"><%#Eval("Sno") %></a>
                            <asp:HiddenField runat="server" ID="Hype" Value='<%# Eval("Sno") %>'/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:BoundField DataField="Result" HeaderText="Catagory" SortExpression="Result">
                        <HeaderStyle Height="30px" />
                        <ItemStyle HorizontalAlign="Center" Height="30px" />
                    </asp:BoundField>

                    <asp:TemplateField ItemStyle-Width="100px" HeaderText="Edit" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" OnClick="Remove" ForeColor="Red" OnClientClick="return confirm('Are you sure want to DELETE ? ')"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Height="30px" Width="100px" HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                </Columns>
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
        </div>

    </form>
</body>
</html>
