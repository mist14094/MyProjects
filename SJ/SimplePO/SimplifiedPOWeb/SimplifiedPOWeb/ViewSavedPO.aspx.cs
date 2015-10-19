using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SimplifiedPOBusiness;
using SimplifiedPOConstants;
using Telerik.Web.UI;

namespace SimplifiedPOWeb
{
    public partial class ViewSavedPO : System.Web.UI.Page
    {
        SPOBL _spobl = new SPOBL();
        private static User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                user = (User)Session["Login"];
                GetValue();
            }
        }

        private void GetValue()
        {
            RadGrid1.DataSource = _spobl.GetUnSubmittedPo(user.UserID.ToString());
            RadGrid1.DataBind();
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Sno"].ToString();
            _spobl.DeleteTempPo(sno: ID);
            GetValue();
            // using DataKey
            // string str2 = item.GetDataKeyValue("ID").ToString();
        }

        protected void RadGrid1_DeleteCommand1(object sender, GridCommandEventArgs e)
        {

        }
    }


}