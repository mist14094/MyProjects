using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Picture : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                
                string StoreID = Request.QueryString["StoreID"];
                HyperLink1.NavigateUrl = "displayPictureFolder.aspx?StoreID=" + StoreID;
                string Date = Request.QueryString["Date"];

                lblDate.Text = DateTime.ParseExact(Date, "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture).ToLongDateString();
                lblStoreID.Text = "Store Number = " + StoreID ;
             
                string strTotal = "";
                if (StoreID != null)
                {
                    if (Directory.Exists(System.IO.Path.Combine(Server.MapPath("\\"), "Picture", StoreID, Date)))
                    {
                        string[] files =
                            Directory.GetFiles(System.IO.Path.Combine(Server.MapPath("\\"), "Picture", StoreID, Date))
                                .OrderByDescending(s => s.ToLower())
                                .ToArray();
                        foreach (string file in files)
                        {
                            strTotal += "<a href=\"" + HttpContext.Current.Request.Url.ToString().Substring(0, HttpContext.Current.Request.Url.ToString().LastIndexOf("/") + 1) + "/picture/" + StoreID + "/" + Date + "/" + Path.GetFileName(file) + "\"> <img src=\"" + HttpContext.Current.Request.Url.ToString().Substring(0, HttpContext.Current.Request.Url.ToString().LastIndexOf("/") + 1) + "/picture/" + StoreID + "/" + Date + "/" + Path.GetFileName(file) + "\", data-title=\"" + File.GetLastWriteTime(file) + "\" > </a>";
                            

                            //    C1LightBoxItem item = new C1LightBoxItem();
                            //    item.ImageUrl = HttpContext.Current.Request.Url.ToString().Substring(0, HttpContext.Current.Request.Url.ToString().LastIndexOf("/") + 1) + "/picture/" + StoreID + "/" + Date + "/" + Path.GetFileName(file);
                            //    item.LinkUrl = HttpContext.Current.Request.Url.ToString().Substring(0, HttpContext.Current.Request.Url.ToString().LastIndexOf("/") + 1) + "/picture/" + StoreID + "/" + Date + "/" + Path.GetFileName(file);
                            //    item.Height=1;
                            //   C1LightBox1.Items.Add(item);
                        }
                        //  C1LightBox1.DataBind();
                        galleria.InnerHtml = strTotal;

                    }
                    else
                    {
                        Div1.InnerHtml = "<h1 style=\"text-align:center\"> No pictures found for this date and store</h1>";
                    }
                }
            }
            catch (Exception ex)
            {
                 lblStoreID.Text= "Error !!!";
            }

        }


    }
}