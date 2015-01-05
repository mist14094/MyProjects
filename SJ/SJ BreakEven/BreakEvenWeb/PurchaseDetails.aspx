<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseDetails.aspx.cs" Inherits="SalesDetails" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView"
    TagPrefix="wijmo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wijmo:C1GridView ID="C1GridView1" runat="server" AutogenerateColumns="False" 
            ShowGroupArea="True" AllowColMoving="True" AllowSorting="True"
             DataSourceID="SqlDataSource1"  
            oncolumngrouping="C1GridView1_ColumnGrouping" 
            oncolumnungrouping="C1GridView1_ColumnUngrouping">
            <Columns>
               
                <wijmo:C1BoundField DataField="PO Number" HeaderText="PO Number" 
                    SortExpression="PO Number">
                </wijmo:C1BoundField>

                <wijmo:C1BoundField DataField="Quantity Received" HeaderText="Quantity"
                    SortExpression="Quantity Received">
                </wijmo:C1BoundField>
                <wijmo:C1BoundField DataField="Final Cost" HeaderText="COGS" 
                    SortExpression="Final Cost">
                </wijmo:C1BoundField>
                <wijmo:C1BoundField DataField="Total Cost" HeaderText="Total Cost"
                    SortExpression="Total Cost" ReadOnly="True">
                </wijmo:C1BoundField>
                <wijmo:C1BoundField DataField="Sugg. MSRP" HeaderText="Sugg. MSRP" 
                    SortExpression="Sugg. MSRP">
                </wijmo:C1BoundField>

              
                <wijmo:C1BoundField DataField="Supplier" HeaderText="Supplier" 
                    SortExpression="Supplier">
                </wijmo:C1BoundField>
                <wijmo:C1BoundField DataField="System" HeaderText="System" 
                    SortExpression="System">
                </wijmo:C1BoundField>

              
                <wijmo:C1BoundField DataField="Received Date" HeaderText="Received Date" 
                    SortExpression="Received Date">
                </wijmo:C1BoundField>

              
            </Columns>
        </wijmo:C1GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:KT_BreakEvenConnectionString %>" 
            
            SelectCommand="SELECT b.[RefNumber] as 'PO Number',a.[QTY_RCV] as 'Quantity Received', a.[FINAL_COST] as 'Final Cost',([QTY_RCV]*[FINAL_COST]) as 'Total Cost', a.[SUGGESTED_RETAIL_PRICE] as 'Sugg. MSRP', b.[Supplier] as 'Supplier' , b.[RefSystem] as 'System' ,b.[DateCreated] as 'Received Date'  FROM ReceivingDetails a LEFT OUTER JOIN ReceivingMaster b ON a.RCVM_ID = b.RM_ID where(a.[PC_ID] = @PC_ID)">
            <SelectParameters>
                <asp:QueryStringParameter Name="PC_ID" QueryStringField="PID" Type="Int64" />
            </SelectParameters>
        </asp:SqlDataSource>
       
    </div>
    </form>
</body>
</html>
