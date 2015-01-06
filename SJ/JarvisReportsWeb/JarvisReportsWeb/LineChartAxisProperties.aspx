<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="LineChartAxisProperties.aspx.cs" Inherits="LineChartAxisProperties" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Axis Properties</title>
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

     <!-- FONTAWESOME STYLES-->
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
        <!-- CUSTOM STYLES-->
    <link href="assets/css/custom.css" rel="stylesheet" />
     <!-- GOOGLE FONTS-->
   <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
        <link rel="stylesheet" type="text/css" href="http://localhost:14111/www.shieldui.com/shared/components/latest/css/shieldui-all.min.css" />
    <link rel="stylesheet" type="text/css" href="http://localhost:14111/www.shieldui.com/shared/components/latest/css/light-mint/all.min.css" />
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="//www.shieldui.com/shared/components/latest/js/shieldui-all.min.js"></script>
    

</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 7%;margin-right: 7%; min-width: 500px;">
    <div id="page-inner">
        <div>
            <div class="row">
                <div class="col-md-12">
                    <h2>
                       <center> Axis Properties</center></h2>
                    <h5>
                        <center>Change Axis attributes of chart.</center>
                        <br/>
                    <table >
                        <tr>
                            <td>X Axis Attributes -
                                <asp:HyperLink ID="hplAddXAxis" runat="server"  Target="_blank" >+ Add X-Axis</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td >
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" GridLines="Both" CellSpacing="4">
                                    <AlternatingRowStyle BackColor="White" Width="100%" />
                                    <Columns>
                                        <asp:BoundField DataField="CatagoricalValuesColumnName" HeaderText="Column Name" />
                                        <asp:BoundField DataField="TitleText" HeaderText="Title Text" />
                                         <asp:BoundField DataField="Sno" HeaderText="Sno" />
                                         <asp:TemplateField>
    <ItemTemplate>
        
  <a href="#" onclick="javascript:w= window.open('<%# "LineChartXAxisProperties.aspx?Sno=" +Eval("Sno")%>','LineChart','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');return false;" >Select</a>
        
    </ItemTemplate>
</asp:TemplateField>
               
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Y Axis Attributes -
                                <asp:HyperLink ID="hplAddYAxis" runat="server" Target="_blank">+ Y-Axis</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="10" ForeColor="#333333" GridLines="Both" CellSpacing="4">

                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="DataFieldY" HeaderText="Y Axis Series"  />
                                        <asp:BoundField DataField="CollectionAlias" HeaderText="Series Alias Name" />
                                          <asp:BoundField DataField="Sno1" HeaderText="Sno" />
                                        <asp:TemplateField>
    <ItemTemplate>
        
  <a href="#" onclick="javascript:w= window.open('<%# "LineChartYAxisProperties.aspx?Sno=" +Eval("Sno1")+"&UpdateMainChartConfigRefNo=" + Request.QueryString["Sno"].ToString() %>','LineChart','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');return false;" >Select</a>
        
    </ItemTemplate>
</asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" Width="100px" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table></h5>
                </div>
    </div>
    </div>
    <!-- /. ROW  -->

    </div>
</div>
          </form>
</body>
</html>

