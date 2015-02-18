<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="True" CodeBehind="SetupDriver.aspx.cs" Inherits="GasBlenderWeb.SetupDriver" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
<script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
<script type = "text/javascript">
    function BlockUI(elementID) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            $("#" + elementID).block({
                message: '<table align = "center"><tr><td>' +
         '<img src="images/loadingAnim.gif"/></td></tr></table>',
                css: {},
                overlayCSS: {
                    backgroundColor: '#000000', opacity: 0.6
                }
            });
        });
        prm.add_endRequest(function () {
            $("#" + elementID).unblock();
        });
    }
    $(document).ready(function () {

        BlockUI("<%=pnlRemove.ClientID %>");
        BlockUI("<%=pnlAdd.ClientID %>");
        BlockUI("<%=pnlEdit.ClientID %>");
        $.blockUI.defaults.css = {};
    });
      
</script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            font-size: large;
        }


        .auto-style2 {
            margin-left: 50px;
            width: 100%;
            padding: 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnAdd" runat="server" Text="Add New Driver" OnClick="Add" CssClass="buttons" Style="margin-left: 30px; margin-top: 10px;" />
            <asp:GridView ID="GridView1" runat="server" Width="90%" Style="margin: 30px;" Height="500px" Font-Size="12px"
                AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#C2D69B" CellSpacing="100"
                HeaderStyle-BackColor="green" CellPadding="4" ForeColor="#333333">
                <Columns>
                    <asp:BoundField DataField="driverID" HeaderText="Driver ID" HtmlEncode="False" />
                    <asp:BoundField DataField="Name" HeaderText="Name" HtmlEncode="False" />
                    <asp:TemplateField ItemStyle-Width="120px" HeaderText="Edit" ItemStyle-Height="30px">
                        <ItemTemplate>
                          <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" OnClick="Remove" ForeColor="Red"></asp:LinkButton>
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

            <asp:Panel ID="pnlEdit" runat="server" CssClass="modalPopup" Style="display: none">

                <br />
                <div>

                    <div class="header">
                        Driver Details
                    </div>
                    <div class="body">
                        <center> <asp:Label runat="server" ID="lblEditError" Text=""></asp:Label>
                        <div style="text-align: left; vertical-align: central;">
                            <table class="auto-style2">
                                <tr>
                                    <td> Driver ID 
                                       
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDriverID" runat="server" Text="Driver ID"></asp:Label>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>

                                    </td>
                                </tr>
                               

                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSaveEdit" runat="server" Text="Save" OnClick="SaveEdit" CssClass="buttons" />
                                        &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttons" OnClick="Cancel" />
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                    <div class="footer" align="right">
                    </div>




                </div>
            </asp:Panel>
            <asp:Panel ID="pnlAdd" runat="server" CssClass="modalPopup" Style="display: none">

                <br />
                <div>

                    <div class="header">
                        Driver Details - New
                    </div>

                    <div class="body">
                        <div style="text-align: left; vertical-align: central;">
                            <center> <asp:Label runat="server" ID="lblAddDriverID" ></asp:Label></center>
                            <table class="auto-style2">
      
                                <tr>
                                    <td>
                                        <asp:Label ID="lbladdName" runat="server" Text="Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtaddName" runat="server"></asp:TextBox>

                                    </td>
                                </tr>
                              
                             

                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSaveAdd" runat="server" Text="Save" OnClick="SaveAdd" CssClass="buttons" />
                                        &nbsp;&nbsp;
                            <asp:Button ID="btnAddCancel" runat="server" Text="Cancel" CssClass="buttons"  OnClick="Cancel"  />
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                    <div class="footer" align="right">
                    </div>




                </div>
            </asp:Panel>
            <asp:Panel ID="pnlRemove" runat="server" CssClass="modalPopup" Style="display: none">

                <br />
                <div>

                    <div class="header">
                        Do you want to delete the Driver
                        <asp:Label runat="server" ID="lblRemove" Text=""></asp:Label>
                    </div>
                    <div class="body">
                        <div style="text-align: left; vertical-align: central; margin:60px;">
                            Delete Driver ? &nbsp;&nbsp;  &nbsp;&nbsp;
                          <asp:Button ID="btnRemove" runat="server" Text="Yes" OnClick="SaveRemove" CssClass="buttons" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnRemoveCancel" runat="server" Text="Cancel" CssClass="buttons"  OnClick="Cancel"  />

                        </div>
                    </div>
                    <div class="footer" align="right">
                    </div>




                </div>
            </asp:Panel>


            <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lnkFake1" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lnkFake2" runat="server"></asp:LinkButton>

            <cc1:ModalPopupExtender ID="popupAdd" runat="server" DropShadow="false"
                PopupControlID="pnlAdd" TargetControlID="lnkFake2"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <cc1:ModalPopupExtender ID="popupEdit" runat="server" DropShadow="false"
                PopupControlID="pnlEdit" TargetControlID="lnkFake"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <cc1:ModalPopupExtender ID="popupDelete" runat="server" DropShadow="false"
                PopupControlID="pnlRemove" TargetControlID="lnkFake1"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridView1" />
            <asp:AsyncPostBackTrigger ControlID="btnRemove" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveAdd" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveEdit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
