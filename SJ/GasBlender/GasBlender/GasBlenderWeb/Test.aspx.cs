using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GasBlenderWeb
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strPopup = "<script language='javascript' ID='script1'>"+ "document.forms[0].target = \"_blank\";"

            ////// Passing intId to popup window.
            //+ "window.open('BOLReport.aspx?ID=" + "123"

            //+ "','Report')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

            Response.Redirect("'BOLReport.aspx?ID=" + "123");

        }
    }
}