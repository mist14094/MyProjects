using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GBBusiness;

namespace GasBlenderWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GBBusiness.BusinessAccess sBusinessAccess = new BusinessAccess();
            GridView gdView = new GridView();
            this.Controls.Add(gdView);
            gdView.DataSource = sBusinessAccess.GetAllTrailer();
            gdView.DataBind();
        }
    }
}