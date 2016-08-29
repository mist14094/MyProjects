<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GasBlenderWeb.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
    <script src="http://viralpatel.net/blogs/demo/jquery-1.3.2.min.js"></script>
    <script>
			$(document).ready(function(){

			    $("input[name*='Col1']").each(function () {

					$(this).keyup(function(){
						calculateSum();
					});
			    });
			    $("input[name*='Col2']").each(function () {

			        $(this).keyup(function () {
			            calculateSum2();
			        });
			    });

			});

			function calculateSum() {

			 

				var sum = 0;
				$("input[name*='Col1']").each(function () {

				    if (!isNaN(this.value) && this.value.length != 0) {
				        sum += parseFloat(this.value);
				    }

				});
				$("#Res11").val(sum);

				
				var Row1 = 0;
				$("input[name*='Row1']").each(function () {

				    if (!isNaN(this.value) && this.value.length != 0) {
				        Row1 += parseFloat(this.value);
				    }

				});
				$("#Res31").val(Row1);

				var Row2 = 0;
				$("input[name*='Row2']").each(function () {

				    if (!isNaN(this.value) && this.value.length != 0) {
				        Row2 += parseFloat(this.value);
				    }

				});
				$("#Res32").val(Row2);


				var Row3 = 0;
				$("input[name*='Row3']").each(function () {

				    if (!isNaN(this.value) && this.value.length != 0) {
				        Row3 += parseFloat(this.value);
				    }

				});
				$("#Res33").val(Row3);

				var Row4 = 0;
				$("input[name*='Row4']").each(function () {

				    if (!isNaN(this.value) && this.value.length != 0) {
				        Row4 += parseFloat(this.value);
				    }

				});
				$("#Res34").val(Row4);




			}
			function calculateSum2() {



			    var sum = 0;
			    $("input[name*='Col2']").each(function () {

			        if (!isNaN(this.value) && this.value.length != 0) {
			            sum += parseFloat(this.value);
			        }

			    });
			    $("#Res22").val(sum);


			    var Row1 = 0;
			    $("input[name*='Row1']").each(function () {

			        if (!isNaN(this.value) && this.value.length != 0) {
			            Row1 += parseFloat(this.value);
			        }

			    });
			    $("#Res31").val(Row1);

			    var Row2 = 0;
			    $("input[name*='Row2']").each(function () {

			        if (!isNaN(this.value) && this.value.length != 0) {
			            Row2 += parseFloat(this.value);
			        }

			    });
			    $("#Res32").val(Row2);


			    var Row3 = 0;
			    $("input[name*='Row3']").each(function () {

			        if (!isNaN(this.value) && this.value.length != 0) {
			            Row3 += parseFloat(this.value);
			        }

			    });
			    $("#Res33").val(Row3);

			    var Row4 = 0;
			    $("input[name*='Row4']").each(function () {

			        if (!isNaN(this.value) && this.value.length != 0) {
			            Row4 += parseFloat(this.value);
			        }

			    });
			    $("#Res34").val(Row4);


			}
		</script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td>Col 1</td>
                <td>
                    <asp:TextBox ID="Col1Row1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="Col1Row2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="Col1Row3" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="Col1Row4" runat="server"></asp:TextBox>
                </td>
                <td> <asp:TextBox ID="Res11" runat="server" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Col 2</td>
                <td>
                    <asp:TextBox ID="Col2Row1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="Col2Row2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="Col2Row3" runat="server"></asp:TextBox>
                </td>
                <td style="margin-left: 80px">
                    <asp:TextBox ID="Col2Row4" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="Res22" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Result</td>
                <td>
                    <asp:TextBox ID="Res31" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="Res32" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="Res33" runat="server"></asp:TextBox>
                </td>
                <td style="margin-left: 80px">
                    <asp:TextBox ID="Res34" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="Res35" runat="server"></asp:TextBox>
                </td>
            </tr>
            </table>
    <div>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    
    </div>
    </form>
</body>
</html>
