<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TShirt299.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <style type="text/css"> 
 
.VertiColumn {
    -webkit-FILTER: flipv fliph;
    -moz-FILTER: flipv fliph;
    -o-FILTER: flipv fliph;
    FILTER: flipv fliph;
    WRITING-MODE: tb-rl;
} 
 
    </style>
    <title></title>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="font-family:Segoe WP">
      <h3>  <center>T-Shirt Split up - $2.99 </center></h3>
       <h4><center>Report generated on <asp:Label runat="server" Text="" ID="lblDate"></asp:Label> </center></h4> </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            ondatabound="GridView1_DataBound" CellPadding="4" ForeColor="Black" 
            Width="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" 
            BorderWidth="3px" CellSpacing="2"  Font-Names="Segoe WP" font-Size="12px" HeaderStyle="VertiColumn" >
            <Columns>
       
             <asp:ImageField DataImageUrlField="Family" AlternateText="Family"  >
                <ControlStyle  Width="100px"/>
      
                
                 <ItemStyle BackColor="White" BorderColor="Black" />
      
                
                </asp:ImageField>
                <asp:BoundField DataField="Description"  HeaderText="Description" />
                   <asp:BoundField  DataField="Stock code" HeaderText="Stock Code"  />
                <asp:BoundField  DataField="Barcode" HeaderText="UPC"  />
                <asp:BoundField  DataField="Store10cnt"  HeaderText="INC-QOH"/>
                <asp:BoundField  DataField="Store21cnt"  HeaderText="Quality-QOH"/>
                <asp:BoundField  DataField="Store22cnt"  HeaderText="Rodeway-QOH"/>
                <asp:BoundField  DataField="Store101cnt"  HeaderText="CWH31-QOH"/>
                <asp:BoundField  DataField="cnt"  HeaderText="QOH"/>
                <asp:BoundField DataField="LastWKSales" HeaderText="7 Day Sales"  />
                <asp:BoundField DataField="PreviousWkSales" HeaderText="Prev. Week Sales"  />
                
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"  CssClass="VertiColumn"/>
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="Gray" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
