<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LoadTruck.aspx.cs" Inherits="GasBlenderWeb.LoadTruck" %>

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

        $.blockUI.defaults.css = {};
    });
      
</script>
    <style type="text/css">
        .auto-style3 {
            width: 100%;
            font-weight: bold;
            
            
        }
        .auto-style4 {
            height: 17px;
        }
        .auto-style5 {
            height: 17px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table class="auto-style3">
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp; &nbsp;</td>
            <td>Loadtype</td>
            <td>
                <asp:DropDownList ID="ddlLoadType" runat="server" Width="200px" >
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>Carrier</td>
            <td>
                <asp:DropDownList ID="ddlCarrier" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>Ref.no</td>
            <td>
               <asp:TextBox runat="server" ID="txtRefNo" Width="197px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Trailer Number</td>
            <td>
                <asp:DropDownList ID="ddlTrailerNumber" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlTrailerNumber_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>Tractor</td>
            <td>
                <asp:DropDownList ID="ddlTractor" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>Driver</td>
            <td>
                <asp:DropDownList ID="ddlDriver" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    
    <div style="margin-left:50px;">
    <table class="auto-style3"  cellspacing="10">
        <tr>
            <td colspan="10" style="text-align: center">Compartments</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td >&nbsp;</td>
            <td class="ac">1</td>
            <td class="ac">2</td>
            <td class="ac">3</td>
            <td class="ac">4</td>
            <td class="ac">5</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width:250px;">Gas Type:(R-Regular,<br />
                S-Super,SP SuperPremix)</td>
            <td>&nbsp;</td>
            <td class="ac">
                <asp:DropDownList ID="ddlGasType1" runat="server" Width="54px" AutoPostBack="True" OnSelectedIndexChanged="ddlGasType1_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="ac">
                <asp:DropDownList ID="ddlGasType2" runat="server" Width="54px" AutoPostBack="True" OnSelectedIndexChanged="ddlGasType2_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="ac">
                <asp:DropDownList ID="ddlGasType3" runat="server" Width="54px" AutoPostBack="True" OnSelectedIndexChanged="ddlGasType3_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="ac">
                <asp:DropDownList ID="ddlGasType4" runat="server" Width="54px" AutoPostBack="True" OnSelectedIndexChanged="ddlGasType4_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="ac">
                <asp:DropDownList ID="ddlGasType5" runat="server" Width="54px" AutoPostBack="True" OnSelectedIndexChanged="ddlGasType5_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Size<br />
&nbsp;</td>
            <td>&nbsp;</td>
            <td class="ac">
                <asp:TextBox ID="txtSize1" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtSize1_TextChanged"></asp:TextBox>
                <cc1:FilteredTextBoxExtender runat="server" ID="FTLcmp1" Enabled="True" TargetControlID="txtSize1" FilterType="Numbers,Custom" ValidChars="."   />
            </td>
            <td class="ac">
                <asp:TextBox ID="txtSize2" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtSize2_TextChanged"></asp:TextBox>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" Enabled="True" TargetControlID="txtSize2" FilterType="Numbers,Custom" ValidChars="."   />
            </td>
            <td class="ac">
                <asp:TextBox ID="txtSize3" runat="server" Width="50px"  AutoPostBack="True" OnTextChanged="txtSize3_TextChanged"></asp:TextBox>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2" Enabled="True" TargetControlID="txtsize3" FilterType="Numbers,Custom" ValidChars="."   />
            </td>
            <td class="ac">
                <asp:TextBox ID="txtSize4" runat="server" Width="50px"  AutoPostBack="True" OnTextChanged="txtSize4_TextChanged"></asp:TextBox>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" Enabled="True" TargetControlID="txtsize4" FilterType="Numbers,Custom" ValidChars="."   />
            </td>
            <td class="ac">
                <asp:TextBox ID="txtSize5" runat="server" Width="50px"  AutoPostBack="True" OnTextChanged="txtSize5_TextChanged"></asp:TextBox>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender4" Enabled="True" TargetControlID="txtSize5" FilterType="Numbers,Custom" ValidChars="."   />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">Actual on board<br />
&nbsp;</td>
            <td class="auto-style4"></td>
            <td class="auto-style5">
                <asp:TextBox ID="txtOnBoard1" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtOnBoard1_TextChanged"></asp:TextBox>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender5" Enabled="True" TargetControlID="txtOnBoard1" FilterType="Numbers,Custom" ValidChars="."  />
            </td>
            <td class="auto-style5">
                <asp:TextBox ID="txtOnBoard2" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtOnBoard2_TextChanged"></asp:TextBox>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender6" Enabled="True" TargetControlID="txtOnBoard2" FilterType="Numbers,Custom" ValidChars="."   />
            </td>
            <td class="auto-style5">
                <asp:TextBox ID="txtOnBoard3" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtOnBoard3_TextChanged"></asp:TextBox>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender7" Enabled="True" TargetControlID="txtOnBoard3" FilterType="Numbers,Custom" ValidChars="."   />
            </td>
            <td class="auto-style5">
                <asp:TextBox ID="txtOnBoard4" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtOnBoard4_TextChanged"></asp:TextBox>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender8" Enabled="True" TargetControlID="txtOnBoard4" FilterType="Numbers,Custom" ValidChars="."   />
            </td>
            <td class="auto-style5">
                <asp:TextBox ID="txtOnBoard5" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtOnBoard5_TextChanged"></asp:TextBox>
                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender9" Enabled="True" TargetControlID="txtOnBoard5" FilterType="Numbers,Custom" ValidChars="."   />
            </td>
            <td class="auto-style4"></td>
            <td class="auto-style4"></td>
            <td class="auto-style4"></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtPercentage" runat="server" Width="20px" AutoPostBack="True" OnTextChanged="txtPercentage_TextChanged"></asp:TextBox>
                % of Size<br />
&nbsp;</td>
            <td>&nbsp;</td>
            <td class="ac">
                <asp:TextBox ID="txt90P1" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txt90P2" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txt90P3" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txt90P4" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txt90P5" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>Totals </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td >Regular to add:<br />
                (- to Take Off)</td>
            <td>&nbsp;</td>
            <td class="ac">
                <asp:TextBox ID="txtReg1" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtReg1_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtReg2" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtReg2_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtReg3" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtReg3_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtReg4" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtReg4_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtReg5" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtReg5_TextChanged"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>= <asp:Label runat="server" ID ="lblTotalRight1"/> From Regualr Stock</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width:250px; height: 20px">Super&nbsp; to add:<br />
                (- to Take Off)</td>
            <td>&nbsp;</td>
            <td class="ac">
                <asp:TextBox ID="txtSuper1" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtSuper1_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtSuper2" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtSuper2_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtSuper3" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtSuper3_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtSuper4" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtSuper4_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtSuper5" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtSuper5_TextChanged"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>= <asp:Label runat="server" ID ="lblTotalRight2"/> From Super Stock</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width:250px; height: 20px">Ethanol to add:<br />
                (- to Take Off)</td>
            <td>&nbsp;</td>
            <td class="ac">
                <asp:TextBox ID="txtEthanol1" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtEthanol1_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtEthanol2" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtEthanol2_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtEthanol3" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtEthanol3_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtEthanol4" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtEthanol4_TextChanged"></asp:TextBox>
            </td>
            <td class="ac">
                <asp:TextBox ID="txtEthanol5" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtEthanol5_TextChanged"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>= <asp:Label runat="server" ID ="lblTotalRight3"/> from Ethanol Stock</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Total Load<br />
&nbsp;</td>
            <td>&nbsp;</td>
            <td class="ac">
                <asp:Label  runat="server" ID ="lblTotalLoad1" /></td>
            <td class="ac">
                <asp:Label  runat="server" ID ="lblTotalLoad2" /></td>
            <td class="ac">
                <asp:Label  runat="server" ID ="lblTotalLoad3" /></td>
            <td class="ac">
                <asp:Label  runat="server" ID ="lblTotalLoad4" /></td>
            <td class="ac">
                <asp:Label  runat="server" ID ="lblTotalLoad5" /></td>
            <td>&nbsp;</td>
            <td> <asp:Label  runat="server" ID ="lblFinalTotal" /></td>
            <td>&nbsp;</td>
        </tr>
       
        <tr>
            <td>Deliver to </td>
            <td>&nbsp;</td>
            <td class="ac">
                <asp:DropDownList ID="ddlDeliver1" runat="server" Width="54px">
                </asp:DropDownList>
            </td>
            <td class="ac">
                <asp:DropDownList ID="ddlDeliver2" runat="server" Width="54px">
                </asp:DropDownList>
            </td>
            <td class="ac">
                <asp:DropDownList ID="ddlDeliver3" runat="server" Width="54px">
                </asp:DropDownList>
            </td>
            <td class="ac">
                <asp:DropDownList ID="ddlDeliver4" runat="server" Width="54px">
                </asp:DropDownList>
            </td>
            <td class="ac">
                <asp:DropDownList ID="ddlDeliver5" runat="server" Width="54px">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
       
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
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
            <td>&nbsp;</td>
            <td class="ac">&nbsp;</td>
            <td class="ac"><asp:Button ID="btnPrint" runat="server" Text="Print"  Height="25px" Width="60px"/></td>
            <td class="ac"><asp:Button ID="btnSave" runat="server" Text="Save"  Height="25px" Width="60px" OnClick="btnSave_Click" Enabled="False"/></td>
            <td class="ac"><asp:Button ID="btnCancel" runat="server" Text="Cancel"  Height="25px" Width="60px"/></td>
            <td class="ac">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
        

        <table class="auto-style3">
            <tr>
                <td><asp:Label runat="server" ID="lblMessg" Text="" /></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                 <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        

</div>
              </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>
