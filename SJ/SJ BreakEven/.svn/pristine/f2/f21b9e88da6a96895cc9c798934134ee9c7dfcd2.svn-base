using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BreakEvenBAL;

public partial class BEGrid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGrid();
        }
    }
    private void BindGrid()
    {
        BreakEvenSnapShot objBEP = new BreakEvenSnapShot();
        List<BEP_Detail> lstDetails =  objBEP.GetBreakEvenSnapShot_ALL();

        GridViewHierachical.DataSource = lstDetails;
        GridViewHierachical.DataBind();
       //GridViewHierachical

    }

    protected void GridViewHierachical_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridViewHierachical.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}