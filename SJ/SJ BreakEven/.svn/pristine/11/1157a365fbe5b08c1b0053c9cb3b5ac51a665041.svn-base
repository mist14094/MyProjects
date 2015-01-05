using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BreakEvenBAL;

namespace BEPWeb
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        static string dtUpdatedTime;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateTimeStamp obj = new UpdateTimeStamp();
                dtUpdatedTime = obj.LastUpdatetime();

                lblLastUpdatedTime.Text = dtUpdatedTime;
            }
        }
    }
}
