using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void C1GridView1_ColumnGrouping(object sender, C1.Web.Wijmo.Controls.C1GridView.C1GridViewColumnGroupEventArgs e)
    {
        e.Drag.Visible = false;
    }
    protected void C1GridView1_ColumnUngrouping(object sender, C1.Web.Wijmo.Controls.C1GridView.C1GridViewColumnUngroupEventArgs e)
    {
        e.Column.Visible = true;
    }
}