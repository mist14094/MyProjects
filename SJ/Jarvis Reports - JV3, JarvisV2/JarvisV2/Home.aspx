<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
#customers {
    font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
    width: 100%;
    border-collapse: collapse;
}

#customers td, #customers th {
    font-size: 1em;
    border: 1px solid #98bf21;
    padding: 3px 7px 2px 7px;
            text-align: center;
        }

#customers th {
    font-size: 1.1em;
    text-align: center;
    padding-top: 5px;
    padding-bottom: 4px;
    background-color: #A7C942;
    color: #ffffff;
}

#customers tr.alt td {
    color: #000000;
    background-color: #EAF2D3;
}
</style>
    <title></title>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="font-family:Segoe WP">
      <h2>  <center>Report Center</center></h2>
     
        <div style="margin-left: 10%; margin-right: 10%;">
            
     
<table id="customers">
  <tr>
    <th>Apple</th>
  </tr>
  <tr>
    <td><a href="tShirt299.aspx">$2.99 T-Shirts - Adult</a></td>
  </tr>
  <tr class="alt">
    <td><a href="tShirt499.aspx">$4.99 T-Shirts - Adult (Color)</a></td>
  </tr>
  <tr>
    <td><a href="tShirt599.aspx">$5.99 T-Shirts - Youth</a></td>
  </tr>
  <tr class="alt">
    <td><a href="tShirt699.aspx">$6.99 T-Shirts - Baby Doll</a></td>
  </tr>
  <tr>
    <td><a href="Hoodies.aspx">Hoodies - Adult</a></td>
  </tr>
 
 
</table>
<br/>
<br/>
<table  id="customers">
  <tr>
    <th>American T-Shirt</th>
  </tr>
  <tr>
    <td><a href="AmericanTShirt.aspx"> T-Shirt & Hoodies [Beta Test]</a> </td>
  </tr>

 
</table>
<br/>
<br/>
<table  id="customers">
  <tr>
    <th>General</th>
  </tr>
  
  <tr>
    <td><a href="UPCDetail.aspx">Item Search</a> </td>
  </tr>
  <tr class="alt">
    <td><a href="CatalogItems.aspx">Item Catalog</a></td>
  </tr>
 
  <tr class="alt">
    <td><a href="POMaster.aspx">Syspro/Web PO Search</a></td>
  </tr>
 
</table>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
