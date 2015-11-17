<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="POLineItems.aspx.cs" Inherits="SimplifiedPOWeb.POLineItems" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
        </script>

    </telerik:RadCodeBlock>
    <style type="text/css">
        .auto-style1 {
            font: 12px "segoe ui", arial, sans-serif;
            width: 100%;
            margin: 10px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <p id="divMsgs" runat="server">
        <asp:Label ID="Label1" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="#FF8080">
        </asp:Label>
        <asp:Label ID="Label2" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="#00C000">
        </asp:Label>
    </p>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="divMsgs"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecorationZoneID="demo" DecoratedControls="All" EnableRoundedCorners="false" />
    <div id="demo" class="demo-container no-bg">
        <telerik:RadGrid ID="RadGrid1" runat="server" CssClass="RadGrid" ShowFooter="True"
            AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
            ShowStatusBar="True" AllowAutomaticDeletes="True" AllowAutomaticInserts="True"
            AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1" OnItemDeleted="RadGrid1_ItemDeleted"
            OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated" OnItemCommand="RadGrid1_ItemCommand"
            OnPreRender="RadGrid1_PreRender" GroupPanelPosition="Top" OnItemDataBound="RadGrid1_ItemDataBound">
            <MasterTableView CommandItemDisplay="Bottom" DataSourceID="SqlDataSource1"
                DataKeyNames="sno">
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn UniqueName="sno" HeaderText="sno" DataField="sno" DataType="System.Int32" FilterControlAltText="Filter sno column" ReadOnly="True" SortExpression="sno" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="POMasterNo" HeaderText="POMasterNo" DataField="POMasterNo" DataType="System.Int32" FilterControlAltText="Filter POMasterNo column" SortExpression="POMasterNo" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="VendorCode" HeaderText="VendorCode" DataField="VendorCode" FilterControlAltText="Filter VendorCode column" SortExpression="VendorCode">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="StockCode" HeaderText="StockCode" DataField="StockCode" FilterControlAltText="Filter StockCode column" SortExpression="StockCode">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Description" HeaderText="Description" DataField="Description" FilterControlAltText="Filter Description column" SortExpression="Description">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="UPC" FilterControlAltText="Filter UPC column" HeaderText="UPC" SortExpression="UPC" UniqueName="UPC">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SKU" FilterControlAltText="Filter SKU column" HeaderText="SKU" SortExpression="SKU" UniqueName="SKU">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Manufacturer" FilterControlAltText="Filter Manufacturer column" HeaderText="Manufacturer" SortExpression="Manufacturer" UniqueName="Manufacturer">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Model" FilterControlAltText="Filter Model column" HeaderText="Model" SortExpression="Model" UniqueName="Model">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Color" FilterControlAltText="Filter Color column" HeaderText="Color" SortExpression="Color" UniqueName="Color">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Size" FilterControlAltText="Filter Size column" HeaderText="Size" SortExpression="Size" UniqueName="Size">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Style" FilterControlAltText="Filter Style column" HeaderText="Style" SortExpression="Style" UniqueName="Style">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Gender" FilterControlAltText="Filter Gender column" HeaderText="Gender" SortExpression="Gender" UniqueName="Gender">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="UnitOfMeasure" FilterControlAltText="Filter UnitOfMeasure column" HeaderText="UnitOfMeasure" SortExpression="UnitOfMeasure" UniqueName="UnitOfMeasure">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter Quantity column" HeaderText="Quantity" SortExpression="Quantity" UniqueName="Quantity" Aggregate="Sum" FooterText=" ">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Cost" FilterControlAltText="Filter Cost column" HeaderText="Cost" SortExpression="Cost" UniqueName="Cost" DataType="System.Decimal" DataFormatString="{0:#.##}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Retail" FilterControlAltText="Filter Retail column" HeaderText="Retail" SortExpression="Retail" UniqueName="Retail" DataType="System.Decimal" DataFormatString="{0:#.##}">
                    </telerik:GridBoundColumn>
                    <telerik:GridCalculatedColumn HeaderText="Total Price" UniqueName="TotalPrice" DataType="System.Decimal" DataFormatString="{0:#.##}" DataFields="Quantity, Cost" Expression="{0}*{1}" FooterText="" Aggregate="Sum">
                    </telerik:GridCalculatedColumn>

                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />

                </Columns>
                <EditFormSettings EditFormType="Template">
                    <EditColumn UniqueName="EditCommandColumn1" FilterControlAltText="Filter EditCommandColumn1 column"></EditColumn>
                    <FormTemplate>      <div style="margin-left: 20px;">
                        <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none" 
                            style="border-collapse: collapse; width: 100%;">
                            <tr class="EditFormHeader">
                                <td>
                                    <b>Item Details</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                              
                                    <table id="Table3" width="450px" border="0" class="Mod1" style="width: 100%; border-bottom: 5px solid transparent;" >
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">Stock Code
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox16" runat="server" TabIndex="8" Text='<%# Bind( "StockCode") %>'>
                                                </asp:TextBox>

                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">Description
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;" >
                                                <asp:TextBox ID="TextBox2" runat="server" TabIndex="8" Text='<%# Bind( "Description") %>'>
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">UPC </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox3" runat="server" TabIndex="9" Text='<%# Bind( "UPC") %>'>
                                                </asp:TextBox>
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">SKU </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox17" runat="server" TabIndex="9" Text='<%# Bind( "SKU") %>'>
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">Vendor Code:
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox9" runat="server" TabIndex="2" Text='<%# Bind("VendorCode") %>'>
                                                </asp:TextBox>
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">Quantity
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox13" runat="server" TabIndex="11" Text='<%# Bind( "Quantity") %>'>
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">Manufacturer
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox18" runat="server" TabIndex="9" Text='<%# Bind( "Manufacturer") %>'>
                                                </asp:TextBox>
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">Model
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox4" runat="server" TabIndex="11" Text='<%# Bind( "Model") %>'>
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">Cost
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox14" runat="server" TabIndex="11" Text='<%# Bind( "Cost") %>'>
                                                </asp:TextBox>
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">Retail
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox15" runat="server" TabIndex="11" Text='<%# Bind( "Retail") %>'>
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">Gender </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox11" runat="server" TabIndex="11" Text='<%# Bind( "Gender") %>'>
                                                </asp:TextBox>
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">Unit Of Measure </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox12" runat="server" TabIndex="11" Text='<%# Bind( "UnitOfMeasure") %>'>
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">Style
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox10" runat="server" TabIndex="11" Text='<%# Bind( "Style") %>'>
                                                </asp:TextBox>
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">Size
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox5" runat="server" TabIndex="11" Text='<%# Bind( "Size") %>'>
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">Color
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox1" runat="server" TabIndex="11" Text='<%# Bind( "Color") %>'>
                                                </asp:TextBox>
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">Supplier</td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:TextBox ID="TextBox19" runat="server" TabIndex="11" Text='<%# Bind( "Color") %>'>
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">&nbsp;</td>
                                            <td style=" border-bottom: 5px solid transparent;">&nbsp;</td>
                                            <td style=" border-bottom: 5px solid transparent;">&nbsp;</td>
                                            <td style=" border-bottom: 5px solid transparent;">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">&nbsp;</td>
                                            <td style=" border-bottom: 5px solid transparent;">&nbsp;</td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:Button ID="btnUpdate" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' />
                                            </td>
                                            <td style=" border-bottom: 5px solid transparent;">
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" border-bottom: 5px solid transparent;">&nbsp;</td>
                                            <td style=" border-bottom: 5px solid transparent;">&nbsp;</td>
                                            <td style=" border-bottom: 5px solid transparent;">&nbsp;</td>
                                            <td style=" border-bottom: 5px solid transparent;">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                        </table></div>
                    
                    </FormTemplate>
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnRowDblClick="RowDblClick"></ClientEvents>
            </ClientSettings>
        </telerik:RadGrid>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:SJPurchaseOrder %>"
        DeleteCommand="DELETE FROM [PODetails] WHERE[sno] = @sno"
        InsertCommand="INSERT INTO [PODetails] (POMasterNo,VendorCode,StockCode,Description,UPC,SKU,Manufacturer,Model,Color,Size,Style,Gender,UnitOfMeasure,Quantity,Cost,Retail) VALUES ( @sno,@VendorCode,@StockCode,@Description,@UPC,@SKU,@Manufacturer,@Model,@Color,@Size,@Style,@Gender,@UnitOfMeasure,@Quantity,@Cost,@Retail)"
        SelectCommand="SELECT * FROM [PODetails] where POMasterNo=@sno"
        UpdateCommand="UPDATE [PODetails] SET [VendorCode] = @VendorCode, [StockCode] = @StockCode, [Description] = @Description, [UPC] = @UPC, [SKU] = @SKU, [Manufacturer] = @Manufacturer, [Model] = @Model, [Color] = @Color, [Size] = @Size, [Style] = @Style,[Gender] = @Gender, [UnitOfMeasure] = @UnitOfMeasure, [Quantity] = @Quantity, [Cost] = @Cost, [Retail] = @Retail WHERE [sno] = @sno">
        <DeleteParameters>
            <asp:Parameter Name="sno"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:QueryStringParameter Name="sno" QueryStringField="sno" Type="string" />
            <asp:Parameter Name="POMasterNo"></asp:Parameter>

            <asp:Parameter Name="VendorCode"></asp:Parameter>
            <asp:Parameter Name="StockCode"></asp:Parameter>
            <asp:Parameter Name="Description"></asp:Parameter>
            <asp:Parameter Name="UPC"></asp:Parameter>
            <asp:Parameter Name="SKU"></asp:Parameter>
            <asp:Parameter Name="Manufacturer"></asp:Parameter>
            <asp:Parameter Name="Model"></asp:Parameter>
            <asp:Parameter Name="Color"></asp:Parameter>
            <asp:Parameter Name="Size"></asp:Parameter>
            <asp:Parameter Name="Style"></asp:Parameter>
            <asp:Parameter Name="Gender"></asp:Parameter>
            <asp:Parameter Name="UnitOfMeasure" />
            <asp:Parameter Name="Quantity" />
            <asp:Parameter Name="Cost" />
            <asp:Parameter Name="Retail" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="POMasterNo"></asp:Parameter>

            <asp:Parameter Name="StockCode"></asp:Parameter>
            <asp:Parameter Name="Description"></asp:Parameter>
            <asp:Parameter Name="UPC"></asp:Parameter>
            <asp:Parameter Name="SKU"></asp:Parameter>
            <asp:Parameter Name="Manufacturer"></asp:Parameter>
            <asp:Parameter Name="Model"></asp:Parameter>
            <asp:Parameter Name="Color"></asp:Parameter>
            <asp:Parameter Name="Size"></asp:Parameter>
            <asp:Parameter Name="Style"></asp:Parameter>
            <asp:Parameter Name="Gender"></asp:Parameter>
            <asp:Parameter Name="UnitOfMeasure"></asp:Parameter>
            <asp:Parameter Name="Quantity" />
            <asp:Parameter Name="Cost" />
            <asp:Parameter Name="Retail" />
            <asp:Parameter Name="sno" />
        </UpdateParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="sno" QueryStringField="sno" Type="string" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />









  <asp:HyperLink ID="hpLink" runat="server" Target="_blank">Search Items</asp:HyperLink>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
        Loading
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" EnableAJAX="True">



        <table class="auto-style1">
            <tr>
                <td>SubTotal:</td>
                <td>Shipping:</td>
                <td>Discount:</td>
                <td>Total:</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;<telerik:RadNumericTextBox  ID="txtSubTotal" runat="server" AutoPostBack="True" Type="Currency" MinValue="0" Value="0"  OnTextChanged="txtSubTotal_TextChanged" Width="90%" Enabled="False">
                    </telerik:RadNumericTextBox>
                </td>
                <td>&nbsp;<telerik:RadNumericTextBox ID="txtShiping" runat="server" AutoPostBack="True" Type="Currency" MinValue="0" Value="0" OnTextChanged="txtShiping_TextChanged" Width="90%">
                    </telerik:RadNumericTextBox>
                </td>
                <td>&nbsp;<telerik:RadNumericTextBox ID="txtDiscount" runat="server" AutoPostBack="True" Type="Currency" MinValue="0" Value="0"   OnTextChanged="txtDiscount_TextChanged" Width="90%">
                    </telerik:RadNumericTextBox>
                </td>
                <td>&nbsp;<telerik:RadNumericTextBox ID="txtTotal" runat="server" AutoPostBack="True" Type="Currency" MinValue="0" Value="0"  OnTextChanged="txtTotal_TextChanged" Width="90%" Enabled="False">
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Check Required:&nbsp;
                    <asp:CheckBox ID="chkCheckRequired" CssClass="pure-checkbox" runat="server" />
                </td>
                <td>RFID Tags:&nbsp;
                    <asp:CheckBox ID="chkRFIDTags" runat="server" />
                </td>
                <td>Order Type:
                    <asp:RadioButtonList ID="rdblPurchaseype" runat="server">
                        <asp:ListItem Value="1">One-Time Order</asp:ListItem>
                        <asp:ListItem Value="2">Re-Order</asp:ListItem>
                        <asp:ListItem Value="3">Future Reorder</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td>
                    Purchase Reason</td>
                <td colspan="3">
                    <telerik:RadTextBox ID="txtPReason" runat="server" AutoPostBack="True" Height="51px" OnTextChanged="txtTotal_TextChanged" Width="90%" TextMode="MultiLine">
                    </telerik:RadTextBox>
                 </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan="3"> &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan="3">
                    <asp:Button ID="btnNext" runat="server" class="pure-button pure-button-primary" OnClick="btnNext_Click" Text="Next &gt;&gt;" Width="244px" />
                </td>
            </tr>
        </table>

    </telerik:RadAjaxPanel>

   






</asp:Content>
