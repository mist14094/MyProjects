<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="GasBlenderWeb.Default" %>
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
                <td class="ar"> <asp:Label runat="server" ID="lblRGasTotal" Text=""/> </td>
                <td class="ac"> /</td>
                <td class="al">  <asp:Label runat="server" ID="lblRTankSize" Text=""/> </td>
                <td> Gallons</td>
                <td>&nbsp;&nbsp; = </td>
                <td class="ar"> <asp:Label runat="server" ID="lblRTotal" Text=""/> </td>
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
                <td class="ar"> <asp:Label runat="server" ID="lblSGasTotal" Text=""/> </td>
                <td class="ac"> /</td>
                <td class="al">  <asp:Label runat="server" ID="lblSTankSize" Text=""/>  </td>
                <td> Gallons</td>
                <td>&nbsp;&nbsp; = </td>
                <td class="ar"><asp:Label runat="server" ID="lblSTotal" Text=""/></td>
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
                <td class="ar"> <asp:Label runat="server" ID="lblEGasTotal" Text=""/></td>
                <td class="ac"> /</td>
                <td class="al"> <asp:Label runat="server" ID="lblETankSize" Text=""/>   </td>
                <td> Gallons</td>
                <td>&nbsp;&nbsp; = </td>
                <td class="ar"><asp:Label runat="server" ID="lblETotal" Text=""/></td>
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
                <td>&nbsp;</td> <asp:Label runat="server" ID="lblError" ForeColor="red"/>

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
                <asp:TextBox ID="txtRGasAdj" runat="server" Font-Size="large" ></asp:TextBox>
                <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" Enabled="True" TargetControlID="txtRGasAdj" FilterType="Numbers, Custom" ValidChars="." />
            </td>
        </tr>
        <tr>
            <td>Super Gas</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtSGasAdj" runat="server" Font-Size="large" ></asp:TextBox>
                <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2" Enabled="True" TargetControlID="txtSGasAdj" FilterType="Numbers, Custom" ValidChars="." />

            </td>
        </tr>
        <tr>
            <td>Ethanol</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtEAdj" runat="server" Font-Size="large"></asp:TextBox>
                <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" Enabled="True" TargetControlID="txtEAdj" FilterType="Numbers, Custom" ValidChars="." />
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
                <asp:TextBox ID="txtRGasSetup" runat="server" Font-Size="large" ></asp:TextBox>
                <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender4" Enabled="True" TargetControlID="txtRGasSetup" FilterType="Numbers, Custom" ValidChars="." />
            </td>
        </tr>
        <tr>
            <td>Super Gas</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtSGasSetup" runat="server" Font-Size="large" ></asp:TextBox>
                <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender5" Enabled="True" TargetControlID="txtSGasSetup" FilterType="Numbers, Custom" ValidChars="." />
            </td>
        </tr>
        <tr>
            <td>Ethanol</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtEthSetup" runat="server" Font-Size="large"></asp:TextBox>
                <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender6" Enabled="True" TargetControlID="txtEthSetup" FilterType="Numbers, Custom" ValidChars="." />
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
                                      <asp:Button ID="btnEditHolding" runat="server" CssClass="buttons" Text="Save" OnClick="btnEditHolding_Click"  />
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
    </div>
    </div>
</asp:Content>
