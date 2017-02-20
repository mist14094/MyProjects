using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdBsnsLayer;

namespace AdWeb
{
    public partial class Engine : System.Web.UI.Page
    {
        AdBsnsLayer.Engine _Engine = new AdBsnsLayer.Engine();
        protected void Page_Load(object sender, EventArgs e)
        {
            // var asd = menu.GetAllTagActivities();
          

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           var mess=  _Engine.Process(int.Parse(TextBox1.Text),TextBox2.Text, TextBox3.Text, TextBox4.Text);
            //2 rr 12 1
            lblMessage.Text = mess;
        }
    }
}