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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
      <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
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
            <telerik:RadGrid ID="RadGrid1" runat="server" CssClass="RadGrid"
                AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False" 
                ShowStatusBar="True" AllowAutomaticDeletes="True" AllowAutomaticInserts="True"
                AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1" OnItemDeleted="RadGrid1_ItemDeleted"
                OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated" OnItemCommand="RadGrid1_ItemCommand"
                OnPreRender="RadGrid1_PreRender" GroupPanelPosition="Top">
                <MasterTableView CommandItemDisplay="Bottom" DataSourceID="SqlDataSource1"
                    DataKeyNames="sno">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                    </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn UniqueName="sno" HeaderText="sno" DataField="sno" DataType="System.Int32" FilterControlAltText="Filter sno column" ReadOnly="True" SortExpression="sno">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="POMasterNo" HeaderText="POMasterNo" DataField="POMasterNo" DataType="System.Int32" FilterControlAltText="Filter POMasterNo column" SortExpression="POMasterNo">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="LineNumber" HeaderText="LineNumber" DataField="LineNumber" DataType="System.Int32" FilterControlAltText="Filter LineNumber column" SortExpression="LineNumber">
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
                        <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter Quantity column" HeaderText="Quantity" SortExpression="Quantity" UniqueName="Quantity">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Cost" FilterControlAltText="Filter Cost column" HeaderText="Cost" SortExpression="Cost" UniqueName="Cost">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Retail" FilterControlAltText="Filter Retail column" HeaderText="Retail" SortExpression="Retail" UniqueName="Retail">
                        </telerik:GridBoundColumn>
                           <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton" />
                    </Columns>
                    <EditFormSettings EditFormType="Template">
<EditColumn UniqueName="EditCommandColumn1" FilterControlAltText="Filter EditCommandColumn1 column"></EditColumn>
                        <FormTemplate>
                            <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0" rules="none"
                                style="border-collapse: collapse;">
                                <tr class="EditFormHeader">
                                    <td colspan="2">
                                        <b>Item Details</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Table3" width="450px" border="0" class="module">
                                            <tr>
                                                <td class="title" style="font-weight: bold;" colspan="2">Purchase Order Line Items</td>
                                                <td class="title" style="font-weight: bold;">&nbsp;</td>
                                                <td class="title" style="font-weight: bold;">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>Purchase Order Number:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("POMasterNo") %>'>
                                                    </asp:TextBox> <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                                                </td>
                                                <td>Color
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" runat="server" TabIndex="11" Text='<%# Bind( "Color") %>'>
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Line Number:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("LineNumber") %>' TabIndex="1">
                                                    </asp:TextBox>
                                                </td>
                                                <td>Size
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox5" runat="server" TabIndex="11" Text='<%# Bind( "Size") %>'>
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Vendor Code:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("VendorCode") %>' TabIndex="2">
                                                    </asp:TextBox>
                                                </td>
                                                <td>Style
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox10" runat="server" TabIndex="11" Text='<%# Bind( "Style") %>'>
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <b>Personal Info:</b>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>Stock Code
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox16" Text='<%# Bind( "StockCode") %>' runat="server" TabIndex="8">
                                                    </asp:TextBox>

                                                </td>
                                                <td>Gender
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox11" runat="server" TabIndex="11" Text='<%# Bind( "Gender") %>'>
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Description
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox2" Text='<%# Bind( "Description") %>' runat="server" TabIndex="8">
                                                    </asp:TextBox>
                                                </td>
                                                <td>Unit Of Measure
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox12" runat="server" TabIndex="11" Text='<%# Bind( "UnitOfMeasure") %>'>
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>UPC
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox3" Text='<%# Bind( "UPC") %>' runat="server" TabIndex="9">
                                                    </asp:TextBox>
                                                </td>
                                                <td>Quantity
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox13" runat="server" TabIndex="11" Text='<%# Bind( "Quantity") %>'>
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>SKU
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox17" Text='<%# Bind( "SKU") %>' runat="server" TabIndex="9">
                                                    </asp:TextBox>
                                                </td>
                                                <td>Cost
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox14" runat="server" TabIndex="11" Text='<%# Bind( "Cost") %>'>
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Manufacturer
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox18" runat="server" TabIndex="9" Text='<%# Bind( "Manufacturer") %>'>
                                                    </asp:TextBox>
                                                </td>
                                                <td>Retail
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox15" runat="server" TabIndex="11" Text='<%# Bind( "Retail") %>'>
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Model
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox4" runat="server" TabIndex="11" Text='<%# Bind( "Model") %>'>
                                                    </asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                        CommandName="Cancel"></asp:Button>
                                    </td>
                                </tr>
                            </table>
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
            DeleteCommand="DELETE FROM [PODetailsTemp] WHERE[sno] = @sno"
            InsertCommand="INSERT INTO [PODetailsTemp] (POMasterNo,LineNumber,VendorCode,StockCode,Description,UPC,SKU,Manufacturer,Model,Color,Size,Style,Gender,UnitOfMeasure,Quantity,Cost,Retail) VALUES (@POMasterNo,@LineNumber,@VendorCode,@StockCode,@Description,@UPC,@SKU,@Manufacturer,@Model,@Color,@Size,@Style,@Gender,@UnitOfMeasure,@Quantity,@Cost,@Retail)"
            SelectCommand="SELECT * FROM [PODetailsTemp] where sno=@sno" UpdateCommand="UPDATE [PODetailsTemp] SET POMasterNo= @POMasterNo, [LineNumber] = @LineNumber, [VendorCode] = @VendorCode, [StockCode] = @StockCode, [Description] = @Description, [UPC] = @UPC, [SKU] = @SKU, [Manufacturer] = @Manufacturer, [Model] = @Model, [Color] = @Color, [Size] = @Size, [Style] = @Style,[Gender] = @Gender, [UnitOfMeasure] = @UnitOfMeasure, [Quantity] = @Quantity, [Cost] = @Cost, [Retail] = @Retail WHERE [sno] = @sno">
            <DeleteParameters>
                <asp:Parameter Name="sno"></asp:Parameter>
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="POMasterNo"></asp:Parameter>
                <asp:Parameter Name="LineNumber"></asp:Parameter>
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
                <asp:Parameter Name="LineNumber"></asp:Parameter>
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
    
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://192.168.1.17/jv3/UPCDetail.aspx" Target="_blank">Search Items</asp:HyperLink>
</asp:Content>
