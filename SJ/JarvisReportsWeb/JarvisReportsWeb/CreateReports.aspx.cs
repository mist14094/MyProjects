using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JarvisBusinessAccess;
using NLog;

public partial class CreateReports : System.Web.UI.Page
{
    JarvisBl JarvisBA = new JarvisBl();
    internal Logger nlog = LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        nlog.Trace("JarvisWeb:CreateReports:Page_Load::Entering");
        try
        {
            if (!IsPostBack)
            {
                var ds = JarvisBA.GetViewandProcedureName();
                ddViewsList.DataSource = ds;
                ddViewsList.DataValueField = "name";
                ddViewsList.DataTextField = "name";
                ddViewsList.DataBind();
            }
        }

        catch (Exception ex)
        {
            nlog.Error("JarvisWeb:CreateReports:Page_Load::Error", ex);
            throw;
        }
        finally
        {
            nlog.Trace("JarvisWeb:CreateReports:Page_Load::Leaving");

        }
    }
    protected void ddViewsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        nlog.Trace("JarvisWeb:CreateReports:ddViewsList_SelectedIndexChanged::Entering");
        try
        {

            var strobjectname = ddViewsList.SelectedValue.ToString(CultureInfo.InvariantCulture).Substring(0, ddViewsList.SelectedValue.ToString(CultureInfo.InvariantCulture).IndexOf(" - [", System.StringComparison.Ordinal));
            var strobjecttype = ddViewsList.SelectedValue.ToString(CultureInfo.InvariantCulture).Substring(ddViewsList.SelectedValue.ToString(CultureInfo.InvariantCulture).IndexOf(" - [", System.StringComparison.Ordinal) + 3).Replace("[", "").Replace("]", "");
            gvSampleData.DataSource = JarvisBA.GetSampleData(strobjectname,strobjecttype);
            gvSampleData.DataBind();
          
        }

        catch (Exception ex)
        {
            nlog.Error("JarvisWeb:CreateReports:ddViewsList_SelectedIndexChanged::Error", ex);
            throw;
        }
        finally
        {
            nlog.Trace("JarvisWeb:CreateReports:ddViewsList_SelectedIndexChanged::Leaving");

        }

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {


    }
}