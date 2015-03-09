<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CTRLCatgSearch.aspx.cs" Inherits="PriceChangerWeb.CTRLCatgSearch" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView" TagPrefix="wijmo" %>
<%@ Register TagPrefix="wijmo" Namespace="C1.Web.Wijmo.Controls.C1GridView" Assembly="C1.Web.Wijmo.Controls.4, Version=4.0.20131.101, Culture=neutral, PublicKeyToken=9b75583953471eea" %>


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
        <div style="margin-top: 30px;"><center><h3>Product Editor - Search for products | <asp:Label ID="lblWelcome" runat="server" Text=""></asp:Label> <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Logout</asp:LinkButton> </h3>
            </center>
          </div>
    <div>
       <div style="margin-top: 30px;" >
           
           <table class="auto-style1">
               <tr>
                   <td>&nbsp;</td>
                   <td  style= "margin: 0px auto 0px auto">
           <center>
        <asp:DropDownList ID="ddlSearchCriteria" runat="server"></asp:DropDownList>
        
        &nbsp;
        
        <asp:TextBox ID="srchTextBox" runat="server" Width="500px"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        <asp:Button ID="btnAddCatagory" runat="server" Text="Search" OnClick="btnAddCatagory_Click"  />
               </center>    </td>
                   <td>&nbsp;</td>
               </tr>
               <tr>
                   <td>&nbsp;</td>
                   <td style= "margin: 0px auto 0px auto"> &nbsp;</td>
                   <td>&nbsp;</td>
               </tr>
               <tr>
                   <td>&nbsp;</td>
                   <td style= "margin: 0px auto 0px auto"> <center>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label></center>
                   </td>
                   <td>&nbsp;</td>
               </tr>
           </table>
       </div>
     <div style="margin: 50px;">
         
       <wijmo:C1GridView ID="C1GridView1" runat="server" AutoGenerateColumns="false" ShowFilter="True" >
  
            <Columns>

              <wijmo:C1TemplateField HeaderText="Link">
                  <HeaderTemplate>
                        <asp:CheckBox ID="chkboxSelectAll" runat="server" OnCheckedChanged="chkboxSelectAll_CheckedChanged" AutoPostBack="True"  />
                  </HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox runat="server" ID="chk"/>
						</ItemTemplate>
					</wijmo:C1TemplateField>
                <wijmo:C1BoundField  DataField="UPC" HeaderText="UPC" />
                  <wijmo:C1BoundField DataField="SKU" HeaderText="SKU" />
                 <wijmo:C1BoundField DataField="desc" HeaderText="Description" FilterOperator="Contains"  />
                  <wijmo:C1BoundField DataField="price" HeaderText="Price" />
                 <wijmo:C1BoundField DataField="Cost" HeaderText="Cost" />
                  <wijmo:C1BoundField DataField="VendorName" HeaderText="Vendor" />
                  <wijmo:C1BoundField DataField="StyleCode" HeaderText="Style Code" />
                  <wijmo:C1BoundField DataField="StyleDesc" HeaderText="Style Description" />
                  <wijmo:C1BoundField DataField="SizeCode" HeaderText="Size Code" />
                  <wijmo:C1BoundField DataField="SizeDesc" HeaderText="Size Description" />
                 <wijmo:C1BoundField DataField="ColorCode" HeaderText="Color Code" />
                 <wijmo:C1BoundField DataField="ColorDesc" HeaderText="Color Description" />
               
               
            
            </Columns>
             </wijmo:C1GridView>
    
           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"   CssClass="mGrid"
            PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt">
                 <pagersettings Position="TopAndBottom" />
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            <Columns>
                
                 <asp:TemplateField>
                      <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" OnCheckedChanged="chkboxSelectAll_CheckedChanged" AutoPostBack="True" />
                        </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
                <asp:BoundField DataField="UPC" HeaderText="UPC" />
                  <asp:BoundField DataField="SKU" HeaderText="SKU" />
                  <asp:BoundField DataField="desc" HeaderText="Description" />
                 <asp:BoundField DataField="price" HeaderText="Price" />
                 <asp:BoundField DataField="Cost" HeaderText="Cost" />
                 <asp:BoundField DataField="VendorName" HeaderText="Vendor" />
                 <asp:BoundField DataField="StyleCode" HeaderText="Style Code" />
                 <asp:BoundField DataField="StyleDesc" HeaderText="Style Description" />
                 <asp:BoundField DataField="SizeCode" HeaderText="Size Code" />
                 <asp:BoundField DataField="SizeDesc" HeaderText="Size Description" />
                 <asp:BoundField DataField="ColorCode" HeaderText="Color Code" />
                 <asp:BoundField DataField="ColorDesc" HeaderText="Color Description" />
               
               
              <asp:TemplateField>
    <ItemTemplate>
        
    <a href="#" onclick="javascript:w= window.open('<%# "PriceChangeWOCA.aspx?UPC=" +Eval("UPC")+"&SKU=" +Eval("SKU") %>','DownloadFile'+'<%# Eval("UPC") %>','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');return false;" >Select</a>
        
    </ItemTemplate>
</asp:TemplateField>
               
            </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
