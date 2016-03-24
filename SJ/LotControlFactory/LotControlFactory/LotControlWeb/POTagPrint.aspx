<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="POTagPrint.aspx.cs" Inherits="LotControlWeb.POTagPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

    <div class="input-group">
        <span class="input-group-addon" id="basic-addon1">Type a PO Number</span>
        <asp:TextBox ID="txtPONumber" runat="server" type="text" Text="016656" class="form-control" placeholder="PO Number" aria-describedby="basic-addon1"></asp:TextBox>
    </div>
    <asp:RequiredFieldValidator ID="rfvPONumber" runat="server" ErrorMessage="* Need a PO Number" class="center-block" ControlToValidate="txtPONumber"></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="btnSearch" runat="server" Text="Search" type="button" class="btn btn btn-success center-block" Width="50%" OnClick="btnSearch_Click" />

    <br />
    <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label>
    <br />
    <asp:GridView ID="GridView1" runat="server" CssClass="table  table-hover" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GridView1_RowDataBound">

        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkboxSelectAll" Checked="True" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                    <asp:Label runat="server" ID="lblhdr" Text="Tags#"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkRow" runat="server" Checked="True" />
                    <asp:TextBox runat="server" ID="NoOfTags" Text='<%# Bind("TagsNeededForThisLine") %>' Width="40px" Enabled="False"> </asp:TextBox>
                    <asp:TextBox runat="server" ID="AltUOM" Text='<%# Bind("AltUOM") %>' Width="40px"> </asp:TextBox>
                    <asp:Button runat="server" ID="UpdateTxt" Text="Update" OnClick="UpdateClick"/><br />
                      <asp:Label ID="TagDenominations" Text='<%# Bind("TagDenominations") %>' runat="server"/>
                </ItemTemplate>
            </asp:TemplateField>
          
            <asp:BoundField DataField="LineItemId" HeaderText="Barcode" />
            <asp:BoundField DataField="StockCode" HeaderText="Stock Code" />
            <asp:BoundField DataField="Description" HeaderText="Description" />

            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:Label ID="Quantity" Text='<%# Bind("Quantity") %> ' runat="server"> </asp:Label>
                    <asp:Label ID="UOM" Text='<%# Bind("UOM") %> ' runat="server"> </asp:Label>
                      <asp:Label ID="lblAltUOM" Text='<%# Bind("AltUOM") %> ' runat="server" Visible="False"> </asp:Label>
 <asp:Label ID="lblOddNumberofTags" Text='<%# Bind("OddNumberofTags") %> ' runat="server" Visible="False"> </asp:Label>

                </ItemTemplate>
            </asp:TemplateField>


            <asp:BoundField DataField="Warehouse" HeaderText="WH" />
            <asp:BoundField DataField="Lotnumber" HeaderText="Lot Number" />
            <asp:BoundField DataField="GRNNumber" HeaderText="GRN#" />
            <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
            <asp:BoundField DataField="DateTime" HeaderText="Created Date" DataFormatString="{0:MM/dd/yyyy}" />
       

        </Columns>
        <EmptyDataTemplate>No Lots Found</EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="lblUOM" runat="server" BackColor="PaleGoldenrod" Text=" * Wrong Alternate UOM" /> | 
    <asp:Label ID="lblNoLotNumber" runat="server" BackColor="Orange" Text=" * No Lot Number" /> 
    <asp:Label ID="lblFlagOddNumberofTags" runat="server" BackColor="lightblue" Text=" * Odd number of Quanity" />
    <asp:Button ID="btnFinalize" runat="server" Visible="False" Text="Finalize Tags" type="button" class="btn btn btn-success center-block" Width="50%" OnClick="btnFinalize_Click" />

    
    <br/><br/>
</asp:Content>
