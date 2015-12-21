using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FactoryInventory
{
    public partial class CheckIn : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox2.Text = DropDownList1.SelectedValue;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Finalize.aspx");
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {

                var val = DropDownList1.Items.FindByText(TextBox1.Text);
                if (val != null)
                {
                    DropDownList1.Items.FindByText(TextBox1.Text).Value =(int.Parse(DropDownList1.Items.FindByText(TextBox1.Text).Value)+1).ToString() ;

                }
                else
                {
                   DropDownList1.Items.Add(new ListItem(TextBox1.Text,"1"));
                }
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(TextBox1.Text));
                TextBox1.Text = "";
                TextBox2.Text = DropDownList1.SelectedValue;
            }
           
        }

    }

}