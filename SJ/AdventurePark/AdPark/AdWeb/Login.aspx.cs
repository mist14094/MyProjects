using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;
using AdBsnsLayer;
namespace AdWeb
{
    public partial class Login : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        AdBsnsLayer.User User = new AdBsnsLayer.User();
        protected void Page_Load(object sender, EventArgs e)
        {

            //      User.InsertUser("Rob", "Lynda", "Greenwood Dr", "Sanborn", "NY", "USA", "14094", "7162553032",
            //          "r.vivekit@gmail.com", DateTime.Now, "12345678");
            GridView1.DataSource  =   User.GetAllUsers();
            GridView1.DataBind();
            logger.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            
        }
    }
}