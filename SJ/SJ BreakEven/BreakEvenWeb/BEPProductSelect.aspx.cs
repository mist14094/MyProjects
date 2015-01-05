using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C1.Web.Wijmo.Controls.C1Chart;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using BreakEvenBAL;
public partial class BEPProduct : System.Web.UI.Page
{
    BEBAL ObjBEBal = new BEBAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            drpName();
            lblDesc.Text = ObjBEBal.getDESCByPID(ObjBEBal.getPIDByUPC( DropDownList1.SelectedValue));
            lblSKU.Text = ObjBEBal.getSKUByPID(ObjBEBal.getPIDByUPC( DropDownList1.SelectedValue));
            Image1.ImageUrl = filepath(DropDownList1.SelectedValue);
        }

    }
    public void drpName()
    {
        DropDownList1.DataSource = ObjBEBal.getAllUPC();     // ajax comboBox ID
        DropDownList1.DataTextField = "upc";    // Name DataFieldName
        DropDownList1.DataBind();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("BEPproduct.aspx?pid=" + ObjBEBal.getPIDByUPC( DropDownList1.SelectedValue));
    }
 
    protected void ajaxCmbName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("BEPproduct.aspx?pid=" + ObjBEBal.getPIDByUPC( DropDownList1.SelectedValue));
    }
    protected void ajaxCmbName_TextChanged(object sender, EventArgs e)
    {
        Response.Redirect("BEPproduct.aspx?pid=" + ObjBEBal.getPIDByUPC( DropDownList1.SelectedValue));
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Redirect("BEPproduct.aspx?pid=" + ObjBEBal.getPIDByUPC( DropDownList1.SelectedValue));
    }
    
    public string filepath(string PID)
    {
        try
        {
            if (File.Exists(Server.MapPath("productimages\\" + PID.Trim() + ".jpg")))
            {
                return "~/productimages/" + PID.Trim() + ".jpg";
            }
            else
            {
                return "~/productimages/noimg.jpg";
            }
            
        }
        catch
        {
            return "~/productimages/noimg.jpg";
        }
    }
    
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblDesc.Text = ObjBEBal.getDESCByPID(ObjBEBal.getPIDByUPC( DropDownList1.SelectedValue));
        lblSKU.Text = ObjBEBal.getSKUByPID(ObjBEBal.getPIDByUPC( DropDownList1.SelectedValue));
        Image1.ImageUrl = filepath(DropDownList1.SelectedValue);
  
    }
}