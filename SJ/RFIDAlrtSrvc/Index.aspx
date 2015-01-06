<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>RFID Alert configuration</title>
  <link rel="stylesheet" href="style/style.css" type="text/css" media="screen" />
  <script type="text/javascript" src="style/accordian.pack.js"></script>

    <style type="text/css">
        .style1
        {
            width: 235px;
        }
    </style>

</head>

<body onload="new Accordian('basic-accordian',5,'header_highlight');">
    <form id="form1" runat="server">
 <div id="logo"><h1>RFID Alert</h1></div>
  <div id="basic-accordian" >
    <div id="test-header" class="accordion_headings header_highlight">Configuration</div>
    <div id="test-content">
      <div class="accordion_child">
<asp:button runat="server" text="Logout" onclick="Unnamed1_Click" />
      <table bgcolor="Silver">
            <tr>
                <td style="margin-left: 30px" >
                <div style="width:400px;">  Select</div>  </td>
                <td class="style1"><div style="width:430px;"> 
                    <asp:DropDownList ID="ddlDirectoryList" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlDirectoryList_SelectedIndexChanged" 
                        Font-Size="14px">
                    </asp:DropDownList> </div> 

                    </td>
            </tr>
            <tr>
                <td style="margin-right: 30px"  >
                    Name</td>
                <td class="style1">
                    <asp:TextBox ID="txtName" runat="server" Font-Size="14px" Height="29px" 
                        Width="401px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 30px"  >
                    Mobile No</td>
                <td class="style1">
                    <asp:TextBox ID="txtMobile" runat="server" Font-Size="14px" Height="29px" 
                        Width="401px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 30px"  >
                    Email ID</td>
                <td class="style1">
                    <asp:TextBox ID="txtEmail" runat="server" Font-Size="14px" Height="29px" 
                        Width="401px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 30px"  >
                    Service Provider</td>
                <td class="style1">
                    <asp:DropDownList ID="ddlServiceProvider" runat="server" Font-Size="14px" Height="29px" 
                        Width="401px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 30px"  >
                    Notes</td>
                <td class="style1">
                    <asp:TextBox ID="txtNotes" runat="server" Font-Size="14px" Height="50px" 
                         Width="401px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 30px"  >
                    Alert for checked Stores</td>
                <td >
                    <asp:CheckBoxList ID="ckbListStoreID" runat="server" Font-Size="14px" Height="29px" 
                        Width="401px">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 30px"  >
                    Active User ?</td>
                <td class="style1">
                    <asp:CheckBox ID="cbIsActive" runat="server" Text="Yes" />
                </td>
            </tr>
            <tr>
                <td style="margin-left: 30px"  >
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="margin-left: 30px"  >
                    &nbsp;</td>
                <td class="style1">
             &nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                        Height="35px" Width="144px" />
                &nbsp; &nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Height="35px" 
                        Width="144px" onclick="btnDelete_Click" />
                </td>
            </tr>
        </table>
      </div>
    </div>

  </div>
  <div id="footer">
    <p>© Smokin Joes® | 2014 | RFID Alert Configuration </p>
  </div>
    </form>
</body>
</html>
