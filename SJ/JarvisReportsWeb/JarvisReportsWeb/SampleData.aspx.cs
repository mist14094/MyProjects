using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JarvisBusinessAccess;
using Shield.Web.UI;

public partial class SampleData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        JarvisBusinessAccess.DefaultLineChart chart = new DefaultLineChart();
        DefaultBusinessAccess access = new DefaultBusinessAccess();
        if (Request.QueryString["sno"] != null)
        {
            try
            {
                string sno = Request.QueryString["sno"];
                string Databaselement = access.SelectChartNameDesc().AsEnumerable()
                    .Where(row => row["sno"].ToString().Equals(sno))
                    .Select(row => row["DataBaseElementSno"].ToString())
                    .FirstOrDefault();
                GridView1.DataSource = access.GetRealData(Databaselement);
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Value cannot be null."))
                {
                    Response.Write("<h2><center>Chart not Found!!!</h2></center>");
                }
                else
                {
                    Response.Write("<h2><center>Oops error occured .... Check Chart Configuration</h2></center>");
                }

            }
        }
    }
}