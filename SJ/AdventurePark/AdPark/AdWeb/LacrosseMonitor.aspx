<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LacrosseMonitor.aspx.cs" Inherits="AdWeb.LacrosseMonitor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/bootstrapNew.css" rel="stylesheet" />
    <link href="css/bootstrap-theme.css" rel="stylesheet" />

    <link href='https://fonts.googleapis.com/css?family=Lobster|Open+Sans:400,400italic,300italic,300|Raleway:300,400,600' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css">
    <link href="css/bootstrap-theme.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/animate.css">
    <link rel="stylesheet" type="text/css" href="css/styleMonitor.css">
    <script src="http://malsup.github.io/jquery.blockUI.js"></script>

    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
    <link rel="stylesheet" href="https://www.w3schools.com/lib/w3-theme-teal.css">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style>
        .w3-bar-block .w3-bar-item {
            padding: 16px;
            font-weight: bold;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


        <div class="content">
            <div class="container wow fadeInUp delay-03s">
                <div class="row">
                    <div class="logo text-center">
                        <img src="img/logo_black.png" alt="logo" width="20%">

                        <div class="container">
                            <div class="row">




                                <div>
                                    <b>
                                        <h1 style="color: red; font-style: normal; text-decoration: underline; font-weight: bolder;">Laccrosse</h1>
                                    </b>

                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                        <ContentTemplate>


                                            <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick"></asp:Timer>




                                            <%--      <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"></ajaxToolkit:ModalPopupExtender>--%>
                                            <asp:Label ID="lblHidden" runat="server" Text=""></asp:Label>


                                            <div runat="server" id="divPopU1p" class="w3-modal">
                                                <div class="w3-modal-content w3-card-4">
                                                    <header class="w3-container w3-teal">
                                                        <span onclick="document.getElementById('id01').style.display='none'"
                                                            class="w3-button w3-display-topright">&times;</span>
                                                        <span style="font-size: 2.5vw; color: black; font-style: normal; font-weight: bolder;">
                                                            <br/>
                                                       <asp:Label ID="lblMessage" runat="server" Text=""   class="blink_text"></asp:Label>
                                                            <br/><br/>
                                                        </span>
                                                    </header>
                                                    
                                                   
                                                </div>
                                            </div>







                                            <asp:Panel ID="pnlTimer" runat="server" Visible="False">

                                                <span style="font-size: 3.0vw; color: black; font-style: normal; font-weight: bolder;">
                                                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label></span>
                                                <br />
                                                <div style="text-align: center;">
                                                    <span style="font-size: 20vw; text-align: center; color: red; font-style: normal; font-weight: bolder;">
                                                        <asp:Label ID="lblTimer" runat="server" Text=""></asp:Label></span>
                                                </div>
                                                <span style="font-size: 2.5vw; color: black; font-style: normal; font-weight: bolder;">seconds to finish</span>
                                                <br />

                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="pnlPersonRecord" Visible="False">

                                                <table class="nav-justified">
                                                    <tr>
                                                        <td><span style="font-size: 3.0vw; color: black; font-style: normal; font-weight: bolder;">
                                                            <asp:Label ID="lblPersonRecordSpeed" runat="server" Text=""></asp:Label></span>
                                                            <br />
                                                            <div style="text-align: center;">
                                                                <span style="font-size: 20vw; text-align: center; color: red; font-style: normal; font-weight: bolder;">
                                                                    <asp:Label ID="lblSpeed" runat="server" Text=""></asp:Label></span>
                                                            </div>
                                                            <span style="font-size: 2.5vw; color: black; font-style: normal; font-weight: bolder;">
                                                                <asp:Label ID="lblSwipeAgain" runat="server" Text=""></asp:Label></span>
                                                            <br />
                                                        </td>
                                                        <td>
                                                            <span style="font-size: 1.5vw; text-align: center; color: black; font-style: normal; font-weight: bolder; width: 80%; padding-right: 15%;">
                                                                <asp:Label ID="Label1" runat="server" Text="Your Personal Record"></asp:Label></span>
                                                            <br />
                                                            <div style="margin: 10%; font-size: 1.5vw;">

                                                                <asp:GridView CssClass="table-responsive" ID="grdPersonalHistory" runat="server" CellPadding="4" ForeColor="Black" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="4" Width="80%">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="CreatedDate" HeaderText="Date &amp; Time" />
                                                                        <asp:BoundField DataField="Value" HeaderText="Speed" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#CCCCCC" />
                                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                                                    <RowStyle BackColor="White" />
                                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>



                                            </asp:Panel>



                                            <asp:Panel runat="server" ID="pnlRecord" Visible="False">

                                                <table class="nav-justified">
                                                    <tr>
                                                        <td>


                                                            <div style="margin: 10%; font-size: 1.5vw;">
                                                                <span style="font-size: 1.5vw; text-align: center; color: black; font-style: normal; font-weight: bolder;">
                                                                    <asp:Label ID="Label2" runat="server" Text="Top 10 - Overall Records"></asp:Label><br />
                                                                    <br />
                                                                </span>
                                                                <asp:GridView CssClass="table-responsive" ID="grdTopAllRecord" runat="server" CellPadding="4" ForeColor="Black" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="4" Width="100%">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="CreatedDate" HeaderText="Date &amp; Time" />
                                                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                        <asp:BoundField DataField="Value" HeaderText="Speed" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#CCCCCC" />
                                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                                                    <RowStyle BackColor="White" />
                                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                                                </asp:GridView>
                                                            </div>

                                                        </td>

                                                        <td>

                                                            <div style="margin: 10%; font-size: 1.5vw;">
                                                                <span style="font-size: 1.5vw; text-align: center; color: black; font-style: normal; font-weight: bolder;">
                                                                    <asp:Label ID="Label3" runat="server" Text="Top 10 - Today Records"></asp:Label><br />
                                                                    <br />
                                                                </span>
                                                                <asp:GridView CssClass="table-responsive" ID="grdTopTodayRecord" runat="server" CellPadding="4" ForeColor="Black" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="4" Width="90%">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="CreatedDate" HeaderText="Date &amp; Time" />
                                                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                        <asp:BoundField DataField="Value" HeaderText="Speed" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#CCCCCC" />
                                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                                                    <RowStyle BackColor="White" />
                                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </asp:Panel>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                                </div>





                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <script src="js/jquery.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
        <script src="js/jquery.countdown.min.js"></script>
        <script src="js/wow.js"></script>
        <script src="js/custom.js"></script>
        <script src="contactform/contactform.js"></script>





    </form>
</body>
</html>

