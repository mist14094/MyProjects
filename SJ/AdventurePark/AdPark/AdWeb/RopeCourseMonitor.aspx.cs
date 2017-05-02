using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdBsnsLayer;
namespace AdWeb
{
    public partial class RopeCourseMonitor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          

        }

        protected void Timer1_Tick(object sender, EventArgs e)

        {
            AdBsnsLayer.Monitor monitor = new AdBsnsLayer.Monitor();
            grdMonitor.DataSource = monitor.RopeCourseMonitor();
            grdMonitor.DataBind();
        }

        protected void grdMonitor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text == "Times Up, Get Down!")
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
                else if (e.Row.Cells[6].Text == "Ready to Get Down")
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Green;
                }
            }
        }
    }
}