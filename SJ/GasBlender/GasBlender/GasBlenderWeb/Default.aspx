<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="false" CodeBehind="Default.aspx.cs" Inherits="GasBlenderWeb.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.7.1213, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            font-size: large;
        }

      
        .auto-style2 {
            width: 100%;
            padding: 50px;
        }

      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="margin-left: 100px;margin-right
    : 100px;">
    <div style="margin: 50px; width: 600px; ">
        <table class="auto-style1">
            <tr>
                <td>Regular Gas</td>
                <td> : </td>
                <td class="ar"> 33373</td>
                <td class="ac"> /</td>
                <td class="al"> 12000.1 </td>
                <td> Gallons</td>
                <td>&nbsp;&nbsp; = </td>
                <td class="ar">2278.1%</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;<br />
                    <br />
                </td>
                <td>&nbsp;</td>
                <td class="ar">&nbsp;</td>
                <td class="ac">&nbsp;</td>
                <td class="al">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="ar">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>

                <td>Super Gas</td>
                <td> : </td>
                <td class="ar"> 3410</td>
                <td class="ac"> /</td>
                <td class="al"> 12000 </td>
                <td> Gallons</td>
                <td>&nbsp;&nbsp; = </td>
                <td class="ar">28.4%</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;<br />
                    <br />
                </td>
                <td>&nbsp;</td>
                <td class="ar">&nbsp;</td>
                <td class="ac">&nbsp;</td>
                <td class="al">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="ar">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Ethanol</td>
                <td> : </td>
                <td class="ar"> -1192769</td>
                <td class="ac"> /</td>
                <td class="al"> 28079 </td>
                <td> Gallons</td>
                <td>&nbsp;&nbsp; = </td>
                <td class="ar">-4247.9%</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                </td>
                <td>&nbsp;</td>
                <td class="ar">&nbsp;</td>
                <td class="ac">&nbsp;</td>
                <td class="al">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="ar">&nbsp;</td>
                <td>&nbsp;</td>xcept 
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="ar">&nbsp;</td>
                <td class="ac">&nbsp;</td>
                <td class="al">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="ar">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>
                    
                    <asp:Button ID="btnAdjustment" runat="server" Text="Make Adjustment" CssClass="buttons"  />
                    <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopupAdjust" TargetControlID="btnAdjustment"  BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnTankSize"   runat="server" Text="Setup Tank Sizes" CssClass="buttons" />
                    
                <cc1:ModalPopupExtender ID="mpeTank" runat="server" PopupControlID="pnlPopupTank" TargetControlID="btnTankSize"  BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
         
      <asp:Panel ID="pnlPopupAdjust" runat="server" CssClass="modalPopup">
                    <div class="header">
                        Adjustment
                    </div>
                    <div class="body">
                      <div>
                          <table class="auto-style2">
        <tr>
            <td>Regular Gas</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Font-Size="large" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Super Gas</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Font-Size="large" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Ethanol</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Font-Size="large"></asp:TextBox>
            </td>
        </tr>
                              <tr>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                              </tr>
                              <tr>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                                  <td>
                                      <div>
                                      <asp:Button ID="btnYes" runat="server" CssClass="buttons" OnClick="btnYes_Click" Text="Save"  />
                                      &nbsp;&nbsp;&nbsp;
                                      <asp:Button ID="btnNo" runat="server" CssClass="buttons" Text="Cancel" /></div>
                                     </td>
                              </tr>
    </table>

                      </div>
                    </div>
                    <div class="footer" align="right">
                    </div>
                </asp:Panel>
      
      <asp:Panel ID="pnlPopupTank" runat="server" CssClass="modalPopup">
                    <div class="header">
                        Edit Holding Tank Sizes
                    </div>
                    <div class="body">
                      <div>
                          <table class="auto-style2">
        <tr>
            <td>Regular Gas</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" Font-Size="large" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Super Gas</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server" Font-Size="large" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Ethanol</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server" Font-Size="large"></asp:TextBox>
            </td>
        </tr>
                              <tr>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                              </tr>
                              <tr>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                                  <td>
                                      <div>
                                      <asp:Button ID="Button1" runat="server" CssClass="buttons" OnClick="btnYes_Click" Text="Save"  />
                                      &nbsp;&nbsp;&nbsp;
                                      <asp:Button ID="Button2" runat="server" CssClass="buttons" Text="Cancel" /></div>
                                     </td>
                              </tr>
    </table>

                      </div>
                    </div>
                    <div class="footer" align="right">
                    </div>
                </asp:Panel>
    </div></div>
</asp:Content>
