using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestSharp;

namespace SJDealStore.Pages.Items
{
    public partial class SquareAutoInsert : System.Web.UI.Page
    {
        SquareSjApi Api = new SquareSjApi();
        protected void Page_Load(object sender, EventArgs e)
        {

         

        }

     
        protected void btnGetName_Click(object sender, EventArgs e)
        {
               // Api.ListInventory();
                Api.GetAllItems();
                Api.InsertProduct("Walmart", "10848", "Sample Name", "Sample Descri", "4999", "1");
            // Response.Write(Api.GetCategoryId(txtName.Text));
        }
    }
}