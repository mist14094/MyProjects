using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Image = System.Web.UI.WebControls.Image;

public partial class _Default : System.Web.UI.Page
{
    public static string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
    public static DataTable dtNew;
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }


 
}