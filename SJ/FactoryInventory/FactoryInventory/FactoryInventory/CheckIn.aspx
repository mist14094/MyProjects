<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CheckIn.aspx.cs" Inherits="FactoryInventory.CheckIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script>
   $.mobile.ajaxEnabled = false;
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:TextBox placeholder="Scan the Shelf" ID="TextBox1" runat="server" TextMode="SingleLine" TabIndex="1" Height="112px" Width="100%" AutoPostBack="True" autofocus="autofocus" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <br />
        <br />
        <br />
        <div class="ui-field-contain">
            <asp:DropDownList ID="DropDownList1" runat="server" data-native-menu="false" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" >
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList>
              <asp:TextBox ID="TextBox2" type="number" runat="server" TextMode="SingleLine" Width="100%" pattern="[0-9]*"></asp:TextBox><br/><br/>
                <asp:Button runat="server" ID="Button2" Text="Next"  OnClick="Button2_Click" />
        </div>
    </div>
</asp:Content>
