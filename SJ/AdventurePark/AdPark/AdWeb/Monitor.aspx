<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="Monitor.aspx.cs" Inherits="AdWeb.Monitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/><br/>
    <div class="media" style="max-width: 992px; margin-left: auto; margin-right: auto;">
        <div class="panel panel-default">
     
                <table class="table">
                    <tr>
                        <td>
                            <asp:Button ID="btnLacrosseThrow" runat="server" Text="Lacrosse Throw" class="btn btn-success btn-lg" style="width: 100%" OnClick="btnLacrosseThrow_Click"  />
                            
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Button ID="btnSoftballThrow" runat="server" Text="Softball Throw" class="btn btn-success btn-lg" style="width: 100%" /></td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Button ID="btnHeaveyBallThrow" runat="server" Text="Heavyball Throw" class="btn btn-success btn-lg" style="width: 100%" /></td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Button ID="btnMaze" runat="server" Text="Maze" class="btn btn-success btn-lg" style="width: 100%" OnClick="btnMaze_Click"  /></td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Button ID="btnBullRide" runat="server" Text="Bull Ride" class="btn btn-success btn-lg" style="width: 100%" OnClick="btnBullRide_Click"  />
                            
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Button ID="btnKidZone" runat="server" Text="Kids Zone" class="btn btn-success btn-lg" style="width: 100%" OnClick="btnKidZone_Click" /></td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Button ID="btnSoccerDarts" runat="server" Text="Soccer Darts" class="btn btn-success btn-lg" style="width: 100%" /></td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Button ID="btnRopeCourse" runat="server" Text="Rope Course" class="btn btn-success btn-lg" style="width: 100%" OnClick="btnRopeCourse_Click"  /></td>
                    </tr>
                       <tr>
                        <td>
                            <asp:Button ID="btnZipLine" runat="server" Text="Zip Line" class="btn btn-success btn-lg" style="width: 100%" OnClick="btnZipLine_Click"  /></td>
                    </tr>
                       <tr>
                        <td>
                            <asp:Button ID="btnTubing" runat="server" Text="Tubing" class="btn btn-success btn-lg" style="width: 100%" OnClick="btnTubing_Click"  /></td>
                    </tr>
                       <tr>
                        <td>
                            <asp:Button ID="btnJumpZone" runat="server" Text="Jump Zone" class="btn btn-success btn-lg" style="width: 100%" OnClick="btnJumpZone_Click"  /></td>
                    </tr>
                       <tr>
                        <td>
                            <asp:Button ID="btnExtraAct1InCount" runat="server" Text="Extra Act 1" class="btn btn-success btn-lg" style="width: 100%" OnClick="btnExtraAct1InCount_Click"  /></td>
                    </tr>   
                    <tr>
                        <td>
                            <asp:Button ID="btnExtracAct1InTime" runat="server" Text="Extra Time 1" class="btn btn-success btn-lg" style="width: 100%"  /></td>
                    </tr>
                   
                </table>


        </div>
    </div>
</asp:Content>
