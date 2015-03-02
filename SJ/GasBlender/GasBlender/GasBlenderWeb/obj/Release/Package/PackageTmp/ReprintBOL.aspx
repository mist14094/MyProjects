<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReprintBOL.aspx.cs" Inherits="GasBlenderWeb.ReprintBOL" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.7.1213, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table class="auto-style1">
        <tr>
            <td>&nbsp;</td>
            <td>Search BOL :
                <asp:TextBox ID="txtBolID" runat="server" Width="265px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"  CssClass="buttons"/>
            </td>
            <td>&nbsp;</td>
        </tr>
       
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:GridView ID="GridView1" runat="server" Width="90%" Style="margin: 30px;" Height="50px"  Font-Size="12px"
                AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#C2D69B" CellSpacing="100"
                HeaderStyle-BackColor="green" CellPadding="4" ForeColor="#333333" AllowPaging="True" PageSize="50"  OnPageIndexChanging="GridView1_PageIndexChanging">
                     <Columns>
                         <asp:BoundField DataField="loadID" HeaderText="Load ID" HtmlEncode="False" /> 
                         <asp:BoundField DataField="refNum" HeaderText="Reference ID" HtmlEncode="False" />
                         <asp:BoundField DataField="stamp" HeaderText="Date"  DataFormatString="{0:d}" HtmlEncode="False"  />
                         
                         
                           <asp:TemplateField ItemStyle-Width="120px" HeaderText="Edit" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <asp:ImageButton ToolTip="Edit" ID="lnkEdit" runat="server" Text="Edit"  OnClick="Edit" ForeColor="Blue" ImageUrl="Images/Edit.png" ></asp:ImageButton> 
                        &nbsp;     <asp:ImageButton ToolTip="Re-Print" ID="lnkReprint" runat="server" Text="Reprint"  OnClick="RePrint" ForeColor="Blue" ImageUrl="Images/print.png"></asp:ImageButton> 
                        &nbsp;     <asp:ImageButton ToolTip ="Delete" ID="lnkDelete" runat="server" Text="Delete"  OnClick="Delete" ForeColor="Blue" ImageUrl="Images/delete.gif"></asp:ImageButton>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                     </Columns>   <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="20px" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
