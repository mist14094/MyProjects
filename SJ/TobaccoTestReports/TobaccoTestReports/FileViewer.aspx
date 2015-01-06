<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileViewer.aspx.cs" Inherits="FileViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2" CellPadding="4" ForeColor="#333333" GridLines="None">
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                 <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" />
                 <asp:BoundField DataField="FileName" HeaderText="FileName" SortExpression="FileName" />
                 <asp:BoundField DataField="SheetName" HeaderText="SheetName" SortExpression="SheetName" />
                 <asp:BoundField DataField="ReportName" HeaderText="ReportName" SortExpression="ReportName" />
                 <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                 <asp:BoundField DataField="BaseConfiguration" HeaderText="BaseConfiguration" SortExpression="BaseConfiguration" />
                 <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                 <asp:BoundField DataField="FromLocation" HeaderText="FromLocation" SortExpression="FromLocation" />
                 <asp:BoundField DataField="ImportedTime" HeaderText="ImportedTime" SortExpression="ImportedTime" />
                 <asp:BoundField DataField="ToLocation" HeaderText="ToLocation" SortExpression="ToLocation" />
                 <asp:CheckBoxField DataField="IsValid" HeaderText="IsValid" SortExpression="IsValid" />
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
        <br />
        <br />
       
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                
                
                <asp:BoundField DataField="ProductClass" HeaderText="ProductClass" SortExpression="ProductClass" />
                <asp:CheckBoxField DataField="ValueTobeProcessed" HeaderText="ValueTobeProcessed" SortExpression="ValueTobeProcessed" />
                <asp:BoundField DataField="RSugarPercentage" HeaderText="RSugarPercentage" SortExpression="RSugarPercentage" />
                <asp:BoundField DataField="TSugarPercentage" HeaderText="TSugarPercentage" SortExpression="TSugarPercentage" />
                <asp:BoundField DataField="NicotinePercentage" HeaderText="NicotinePercentage" SortExpression="NicotinePercentage" />
                <asp:BoundField DataField="Wgt" HeaderText="Wgt" SortExpression="Wgt" />
                <asp:BoundField DataField="Dil" HeaderText="Dil" SortExpression="Dil" />
                <asp:BoundField DataField="Identifier" HeaderText="Identifier" SortExpression="Identifier" />
                <asp:BoundField DataField="Sam" HeaderText="Sam" SortExpression="Sam" />
                <asp:BoundField DataField="LineNumber" HeaderText="LineNumber" SortExpression="LineNumber" />
                
                
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        
         <br />
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetFileDetails" TypeName="BusinessLogic.TbcExcelTemplate">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="sno" QueryStringField="SNO" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br/>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="TbcExcelInternals" TypeName="BusinessLogic.TbcExcelValues">
            <SelectParameters>
                <asp:QueryStringParameter Name="sno" QueryStringField="SNO" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
