<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewSavedPO.aspx.cs" Inherits="SimplifiedPOWeb.ViewSavedPO" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand" >
        <MasterTableView  DataKeyNames="Sno">
                <Columns>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                         Text="Delete" UniqueName="DeleteColumn" ConfirmDialogType="RadWindow"
                        Resizable="false" ConfirmText="Delete record?">
                    </telerik:GridButtonColumn>
                    <telerik:GridHyperLinkColumn DataTextFormatString="View" DataNavigateUrlFields="sno"
  UniqueName="sno" DataNavigateUrlFormatString="POLineItems.aspx?sno={0}" HeaderText="Link"
  DataTextField="sno">
</telerik:GridHyperLinkColumn>
                    </Columns></MasterTableView>
    </telerik:RadGrid>
</asp:Content>
