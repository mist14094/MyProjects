using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomeScreen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        JarvisBusinessAccess.DefaultBusinessAccess jbAccess = new JarvisBusinessAccess.DefaultBusinessAccess();
        DataTable dt= new DataTable();
        dt = jbAccess.SelectChartNameDesc();
        ListView1.DataSource = dt.AsEnumerable().Where(row => row["ChartTypeRefNo"].ToString().Equals("8"))
            .Where(row => row["IsValid"].ToString().Equals("True")).CopyToDataTable();
        ListView1.DataBind();
    }
}