using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SJDealStore.Pages.Items
{
    public partial class Sales : System.Web.UI.Page
    {
        DALayer _layer= new DALayer();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
            }

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if(ListBox1.Items.FindByText(TextBox1.Text)==null)
            ListBox1.Items.Add(TextBox1.Text);

            lblCount.Text = ListBox1.Items.Count.ToString();
            var selectProducts = _layer.SelectProducts(GetCSVFromListBox());
            GridView1.DataSource = selectProducts;
            GridView1.DataBind();
            TextBox1.Text = "";
            if (selectProducts.Rows.Count > 0)
            {
                lblWarning.Text = "* Item Found";
            }
            else
            {
                lblWarning.Text = "* Item Not Found";
            }

            TextBox1.Focus();
        }

        public string GetCSVFromListBox()
        {
            string result = "";
            int i = 0;
            foreach (ListItem listItem in ListBox1.Items)
            {
                if (i == 0)
                {
                    result = listItem.Value;

                }
                else
                {
                    result = result + "," + listItem.Value;
                }
                i++;
            }
            return result;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Sales.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SalesResult result= new SalesResult();
            result = _layer.MakeSales(GetCSVFromListBox());
            if (int.Parse( result.Decommissioned) > 0)
            {
                lblWarning.Text = result.Decommissioned + " Item(s) decommissioned successfully";
            }
            else
            {
                lblWarning.Text = "No Valid Item(s)";
            }

            ListBox1.Items.Clear();
            lblCount.Text = ListBox1.Items.Count.ToString();
            GridView1.DataSource = null;
            GridView1.DataBind();
            TextBox1.Text = "";
            TextBox1.Focus();
        }
    }
}