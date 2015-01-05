using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisplayPictureFolder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListView1.DataBind();
            string StoreID = Request.QueryString["StoreID"];
            if (StoreID != null)
            {
                if (Directory.Exists(System.IO.Path.Combine(Server.MapPath("\\"), "Picture", StoreID)))
                {

                    ListView1.Items.Clear();
                    string[] files = Directory.GetDirectories(System.IO.Path.Combine(Server.MapPath("\\"), "Picture", StoreID));
                    DataTable table = new DataTable();
                    table.Columns.Add("Date", typeof(DateTime));
                    table.Columns.Add("StoreID", typeof(string));
                    foreach(string file in files)
                    {
                        try
                        {
                            table.Rows.Add(DateTime.ParseExact(file.Substring(file.LastIndexOf("\\") + 1, 8), "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture),StoreID);
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    table.DefaultView.Sort = "Date DESC";


                    ListView1.DataSource = table;
                    ListView1.DataBind();

                }
            }

        }


    }
}