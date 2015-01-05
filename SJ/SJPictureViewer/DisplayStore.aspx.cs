using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisplayStore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Directory.Exists(System.IO.Path.Combine(Server.MapPath("\\"), "Picture")))
        {
            ListView1.Items.Clear();
            string[] files = Directory.GetDirectories(System.IO.Path.Combine(Server.MapPath("\\"), "Picture", ""));
            
            DataTable table = new DataTable();
            table.Columns.Add("pic_id", typeof(string));
            table.Rows.Add( "2");
            table.Rows.Add( "3");
            table.Rows.Add( "4");
            table.Rows.Add( "6");
            table.Rows.Add( "7");
            table.Rows.Add( "9");
            table.Rows.Add( "10");
            table.Rows.Add( "12");
            
            ListView1.DataSource = table;
            ListView1.DataBind();
        
        } 


       

    }
}