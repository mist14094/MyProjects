<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewCatgrySearch.aspx.cs" Inherits="PriceChangerWeb.NewCatgrySearch" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />

    </head>
<body>
    <form id="form1" runat="server">
          <div style="margin-top: 30px;"></div>
        <div style="margin-top: 30px;"><center><h3>Product Editor - Search for products | <asp:Label ID="lblWelcome" runat="server" Text=""></asp:Label> &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Logout</asp:LinkButton>   | <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="LandingMenu.aspx">Home</asp:LinkButton> </h3>
            <p>&nbsp;</p>
            </center>
          </div>
    <div>
        
        
        
        
        

        <table style=" margin:0 auto; min-width: 900px; width: 900px; vertical-align: central;">
            <tr>
                <td>&nbsp;</td>
                <td>
        
        <asp:TextBox ID="srchTextBox" runat="server" Width="150px" Height="20px" ></asp:TextBox>
                </td>
                <td>
        
                    &nbsp;</td>
                <td>
        
                    &nbsp;</td>
                <td>
        <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="500px" Height="23px"></asp:DropDownList>
        
                </td>
                <td>
        <asp:Button ID="btnSearch" runat="server" Text="Product Search" OnClick="btnSearch_Click" Width="150px" Height="23px" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr style="height: 10px;">
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
       
        
        <asp:TextBox ID="txtCatagories" runat="server" Width="150px" Height="20px" ></asp:TextBox>
                 </td>
                <td>
       
        
                    <asp:ImageButton ID="imbSearchCatg" runat="server" Height="16px" ImageUrl="~/css/icon_search.png" Width="16px" OnClick="imbSearchCatg_Click"  />
                 </td>
                <td>
                    &nbsp;</td>
                <td> 
        <asp:DropDownList ID="ddlCatagories" runat="server" Width="500px" Height="23px"></asp:DropDownList>
        
                </td>
                <td><asp:Button ID="btnAddCatagory" runat="server" Text="Categorize" OnClick="btnAddCatagory_Click"  Height="25px" Width="150px"/></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td> &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td style="text-align: center"> Total Products :
                    <asp:Label ID="lblProduct" runat="server" Text="0"></asp:Label>
&nbsp;| Total Catagories Found :
                    <asp:Label ID="lblCatg" runat="server" Text="0"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        
        
        
        
        

       
     <div style="margin: 50px; height: 600px; ">
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
        
    <a href="#" onclick="javascript:w= window.open('<%# "CatagoriesExpl.aspx?UPC=" +Eval("UPC")+"&SKU=" +Eval("SKU") %>','DownloadFile'+'<%# Eval("UPC") %>','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');return false;" ><%#Eval("Price") %></a>
        
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
