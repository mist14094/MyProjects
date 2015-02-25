<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LoadBOLHome.aspx.cs" Inherits="GasBlenderWeb.LoadBOLHome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

              
    <table class="auto-style1">
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Enter the BOL Number&nbsp;
                <asp:TextBox ID="TextBox1" runat="server" Width="231px"></asp:TextBox>
                 <cc1:FilteredTextBoxExtender runat="server" ID="txtBoxExtender" Enabled="True" TargetControlID="TextBox1" FilterType="Numbers" />
&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"  />
                
                
                                   
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:GridView ID="GridView1" runat="server" Width="90%" Style="margin: 30px;" Height="50px"  Font-Size="12px"
                AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#C2D69B" CellSpacing="100"
                HeaderStyle-BackColor="green" CellPadding="4" ForeColor="#333333">
                     <Columns>
                         <asp:BoundField DataField="loadID" HeaderText="Load ID" HtmlEncode="False" /> 
                         <asp:BoundField DataField="refNum" HeaderText="Reference ID" HtmlEncode="False" />
                         <asp:BoundField DataField="stamp" HeaderText="Date"  DataFormatString="{0:d}" HtmlEncode="False"  />
                         <asp:BoundField DataField="loadType" HeaderText="Load Type" HtmlEncode="False" />
                         <asp:BoundField DataField="driver" HeaderText="Driver" HtmlEncode="False" />
                         
                           <asp:TemplateField ItemStyle-Width="120px" HeaderText="Edit" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"  OnClick="Edit" ForeColor="Blue"></asp:LinkButton>
                           
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

