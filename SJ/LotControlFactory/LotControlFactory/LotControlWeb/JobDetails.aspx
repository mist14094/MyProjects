<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="JobDetails.aspx.cs" Inherits="LotControlWeb.JobDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

    <div class="input-group">
        <span class="input-group-addon" id="basic-addon1">Type a Job Number</span>
        <asp:TextBox ID="txtJobNumber" runat="server" type="text" Text="SJP00128" class="form-control" placeholder="Job Number" aria-describedby="basic-addon1"></asp:TextBox>
    </div>
    <a href="JobDetails.aspx">JobDetails.aspx</a>
    <asp:RequiredFieldValidator ID="rfvJobNumber" runat="server" ErrorMessage="* Need a Job Number" class="center-block" ControlToValidate="txtJobNumber"></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="btnSearch" runat="server" Text="Search" type="button" class="btn btn btn-success center-block" Width="50%" OnClick="btnSearch_Click" />
    <br/>
    <div style="text-align: center; font-weight: bold">
        <h3>
    <asp:Label ID="lblJob" runat="server" Text=""></asp:Label>
&nbsp;|
    <asp:Label ID="lblJobDescr" runat="server" Text=""></asp:Label>
&nbsp;|
    <asp:Label ID="lblStockCode" runat="server" Text=""></asp:Label>
&nbsp;|
    <asp:Label ID="lblQtyToMake" runat="server" Text=""></asp:Label>
<asp:Label ID="lblSUom" runat="server" Text=""></asp:Label>
&nbsp;|
    <asp:Label ID="lblJobStartDate" runat="server" Text=""></asp:Label></h3>
</div>
    <br />
    <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label>
    <br />
    <asp:GridView ID="grdViewJob" runat="server" CssClass="table  table-hover" AutoGenerateColumns="False" Width="100%" OnDataBound="grdViewJob_OnDataBound"  OnRowDataBound="GridView1_RowDataBound">
          <Columns>
                <asp:TemplateField HeaderText="Desc"  ItemStyle-Width="65%" >
                    <ItemTemplate>
                    <asp:Label ID="Desc" runat="server" Text='<%# string.Concat( "<b>",Eval("StockCode"),"</b><i>", "&emsp;", Eval("StockCodeDesc") ,"</i><br/>",
                    "Required : ",Eval("CalculatedRequired")," ",Eval("Uom") ," <br/> Issued to Date : " ,Eval("QtyIssued")," ",Eval("Uom")          ) %>'></asp:Label>
                    </ItemTemplate> 
                       </asp:TemplateField>
                <asp:TemplateField HeaderText="Bin" ItemStyle-Width="85%" >
                  <ItemTemplate>
                       <asp:Label ID="Bin"  runat="server" Text='<%# string.Concat( "<b>",Eval("Bin"), " - ", (Eval("BinQtyOnHand").ToString()!="0"? Eval("BinQtyOnHand").ToString():"No Quantity Available") ," ",(Eval("BinQtyOnHand").ToString()!="0"? Eval("Uom"):"") , "</b>"  ) %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>

              </Columns>
     
        <EmptyDataTemplate>No Lots Found</EmptyDataTemplate>
    </asp:GridView>
   

    
    <br/><br/>
</asp:Content>
