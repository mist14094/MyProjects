using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
public partial class ViewClass : System.Web.UI.Page
{
    BusinessLogic.Reports Reports = new Reports();

    protected void Page_Load(object sender, EventArgs e)
    {

        var qString = Request.QueryString["Sort"];
        if (!IsPostBack)
        {
            if (qString == "Popular")
            {
                rblChooseCatagory.SelectedIndex = 1;
            }
            else
            {
                rblChooseCatagory.SelectedIndex = 0;
            }
        }

       

        if (qString == "Popular")
        {
            using (var dtDataTable = Reports.GetGetTobbaccoClass())
            {
                ListView1.DataSource = dtDataTable;
                ListView1.DataBind();
            }
        }
        else
        {
            using (var dtDataTable = Reports.GetTobbaccoClassByLatest())
            {
                ListView1.DataSource = dtDataTable;
                ListView1.DataBind();
            }

        }
        }
    protected void rblChooseCatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(rblChooseCatagory.SelectedIndex == 0
            ? "ViewClass.aspx?Sort=Latest"
            : "ViewClass.aspx?Sort=Popular");
    }
}