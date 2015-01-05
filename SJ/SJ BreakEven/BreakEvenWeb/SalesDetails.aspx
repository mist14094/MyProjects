<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesDetails.aspx.cs" Inherits="SalesDetails" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView"
    TagPrefix="wijmo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style type="text/css">  
        .accordion {  
            width: 400px;  
        }  
          
        .accordionHeader {  
            border: 1px solid #2F4F4F;  
            color: white;  
            background-color: #2E4d7B;  
            font-family: Arial, Sans-Serif;  
            font-size: 12px;  
            font-weight: bold;  
            padding: 5px;  
            margin-top: 5px;  
            cursor: pointer;  
        }  
          
        .accordionHeaderSelected {  
            border: 1px solid #2F4F4F;  
            color: white;  
            background-color: #5078B3;  
            font-family: Arial, Sans-Serif;  
            font-size: 12px;  
            font-weight: bold;  
            padding: 5px;  
            margin-top: 5px;  
            cursor: pointer;  
        }  
          
        .accordionContent {  
            background-color: #D3DEEF;  
            border: 1px dashed #2F4F4F;  
            border-top: none;  
            padding: 5px;  
            padding-top: 10px;  
        }  
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wijmo:C1GridView ID="C1GridView1" runat="server" AutogenerateColumns="False" 
            ShowGroupArea="true" AllowColMoving="true"  AllowAutoSort="true" AllowSorting="true"
             DataSourceID="SqlDataSource1"  
            oncolumngrouping="C1GridView1_ColumnGrouping" 
            oncolumnungrouping="C1GridView1_ColumnUngrouping">
            <Columns>
               
                <wijmo:C1BoundField DataField="StoreID" HeaderText="StoreID"    SortDirection="Ascending" 
                    SortExpression="StoreID">
                </wijmo:C1BoundField>

                <wijmo:C1BoundField DataField="RetailPrice" HeaderText="RetailPrice"  Aggregate="Sum"  SortDirection="Ascending"
                    SortExpression="RetailPrice">
                </wijmo:C1BoundField>
                <wijmo:C1BoundField DataField="QtySold" HeaderText="QtySold"  SortDirection="Ascending" Aggregate="Sum" 
                    SortExpression="QtySold">
                </wijmo:C1BoundField>
                <wijmo:C1BoundField DataField="TotalPrice" HeaderText="TotalPrice"  SortDirection="Ascending" Aggregate="Sum"
                    SortExpression="TotalPrice">
                </wijmo:C1BoundField>
                <wijmo:C1BoundField DataField="SaleTime" HeaderText="Sale Date"  SortDirection="Ascending" 
                    SortExpression="SaleTime">
                </wijmo:C1BoundField>

              
            </Columns>
        </wijmo:C1GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:KT_BreakEvenConnectionString %>" 
            SelectCommand="SELECT cast( [StoreID] as int) as [StoreID], [RetailPrice], [QtySold], [TotalPrice], [SaleTime] FROM [SalesDetail] WHERE ([PC_ID] = @PC_ID)">
            <SelectParameters>
                <asp:QueryStringParameter Name="PC_ID" QueryStringField="PID" Type="Int64" />
            </SelectParameters>
        </asp:SqlDataSource>
       
    </div>
    </form>
</body>
</html>
