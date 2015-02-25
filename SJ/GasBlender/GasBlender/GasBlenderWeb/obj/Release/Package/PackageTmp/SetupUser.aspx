<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SetupUser.aspx.cs" Inherits="GasBlenderWeb.SetupUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnInsert" runat="server" Text="Insert User" OnClick="btnInsert_Click" CssClass="buttons" Style="margin-left: 30px; margin-top: 10px;" />
 <asp:GridView ID="GridView1" runat="server" Width="90%" Style="margin: 30px;" Font-Size="12px"
                AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#C2D69B" CellSpacing="100"
                HeaderStyle-BackColor="green" CellPadding="4" ForeColor="#333333">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" HtmlEncode="False" />
                    <asp:BoundField DataField="UserName" HeaderText="Username" HtmlEncode="False" />
                    <asp:BoundField DataField="Name" HeaderText="Name" HtmlEncode="False" />
                    <asp:BoundField DataField="Email" HeaderText="E-Mail" HtmlEncode="False" />
                    <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" HtmlEncode="False" />
                    <asp:TemplateField ItemStyle-Width="120px" HeaderText="Edit" ItemStyle-Height="30px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="Edit" ForeColor="Red"></asp:LinkButton> | 
                          <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" OnClick="Remove" ForeColor="Red" OnClientClick="return confirm('Are you sure want to DELETE ? ')"></asp:LinkButton>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="40px" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
</asp:Content>
