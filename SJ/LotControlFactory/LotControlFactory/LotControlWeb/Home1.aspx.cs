using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LotControlBusiness;

namespace LotControlWeb
{
    public partial class Home1 : System.Web.UI.Page
    {

        LotControlBusiness.LcBusiness Business= new LcBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(TextBox1.Text!="")
            {
                GridView1.DataSource = Business.ImportItemsinPO(TextBox1.Text);
                GridView1.DataBind();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
            }
        }
    }
}