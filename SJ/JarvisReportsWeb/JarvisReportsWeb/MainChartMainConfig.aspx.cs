using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JarvisBusinessAccess;
using System.Linq;
public partial class MainChartMainConfig : System.Web.UI.Page
{
    JarvisBusinessAccess.DefaultBusinessAccess access = new DefaultBusinessAccess();
   
    private static string postback = "";
    private static string AxisBack = "";
    private static string SampleData = "";
    private static string PreviewChart = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("Login.aspx");
        }
       
         string sno = Request.QueryString["Sno"];
        if (!IsPostBack)
        {



            if (sno != null)
            {
                btnSave.Text = "Update";
                DataTable ChartValues = access.SelectChartNameDesc();
                var dataResult = ChartValues.AsEnumerable().Where(row =>row["sno"].ToString().Equals(sno));
                foreach (var dataRow in dataResult)
                {
                    txtChartName.Text = dataRow["ChartName"].ToString();
                    txtChartDesc.Text = dataRow["ChartDesc"].ToString();

                    ddlChartTypes.DataSource = access.SelectChartTypes();
                    ddlChartTypes.DataTextField = "ChartType";
                    ddlChartTypes.DataValueField = "sno";
                    ddlChartTypes.DataBind();

                    ddlChartTypes.SelectedValue = dataRow["ChartTypeRefNo"].ToString();

                    ddlSelectDataBase.DataSource = access.GetAllDataBaseConnection();
                    ddlSelectDataBase.DataValueField = "Sno";
                    ddlSelectDataBase.DataTextField = "EquivalentString";
                    ddlSelectDataBase.DataBind();
                    ddlSelectDataBase.Items.Insert(0, "");

                    ddlSelectDataBase.SelectedValue = access.GetDatabaseSnofromElementReference(dataRow["DataBaseElementSno"].ToString());


                    if (ddlSelectDataBase.SelectedValue != "")
                    {
                        ddlDataBaseElements.DataSource = access.GetDataElements(ddlSelectDataBase.SelectedValue, true,
                            "'V','C'");
                        ddlDataBaseElements.DataTextField = "ElementAliasName";
                        ddlDataBaseElements.DataValueField = "sno";
                        ddlDataBaseElements.DataBind();
                        ddlDataBaseElements.Items.Insert(0, "");
                    }
                    ddlDataBaseElements.SelectedValue = dataRow["DataBaseElementSno"].ToString();

                    postback = "<script>window.open('MainChartProperties.aspx?Sno=" + dataRow["MainChartConfigurationRefNo"].ToString() +
                               "','PostBack','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');</script>";

                    AxisBack = "<script>window.open('LineChartAxisProperties.aspx?Sno=" + dataRow["MainChartConfigurationRefNo"].ToString() +
                               "','AxisBack','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');</script>";

                    PreviewChart = "<script>window.open('GenerateChart.aspx?sno=" + dataRow["sno"].ToString() +
                              "','PreviewChart','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');</script>";

                    SampleData = "<script>window.open('SampleData.aspx?sno=" + dataRow["sno"].ToString() +
                              "','SampleData','left=20,top=20,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');</script>";


                }
            }
            else
            {
                btnSave.Text="Save & Next";

                ddlChartTypes.DataSource = access.SelectChartTypes();
                ddlChartTypes.DataTextField = "ChartType";
                ddlChartTypes.DataValueField = "sno";
                ddlChartTypes.DataBind();


                ddlSelectDataBase.DataSource = access.GetAllDataBaseConnection();
                ddlSelectDataBase.DataValueField = "Sno";
                ddlSelectDataBase.DataTextField = "EquivalentString";
                ddlSelectDataBase.DataBind();
                ddlSelectDataBase.Items.Insert(0, "");


            }
            
        }

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {

    }
    protected void ddViewsList_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddlSelectDataBase_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelectDataBase.SelectedValue != "")
        {
            ddlDataBaseElements.DataSource = access.GetDataElements(ddlSelectDataBase.SelectedValue, true, "'V','C'");
            ddlDataBaseElements.DataTextField = "ElementAliasName";
            ddlDataBaseElements.DataValueField = "sno";
            ddlDataBaseElements.DataBind();
            ddlDataBaseElements.Items.Insert(0, "");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sno = Request.QueryString["Sno"];
        if (sno == null)
        {
            if (txtChartName.Text == "" || txtChartDesc.Text == "" || ddlChartTypes.SelectedValue == "" ||
                ddlDataBaseElements.SelectedValue == "" || ddlSelectDataBase.SelectedValue == "")
            {
                lblResult.Text = "* All Fields are mandatory ";
            }
            else
            {
                string UniqueID = access.InsertMainConfigurationComp(txtChartName.Text, txtChartDesc.Text,
                Convert.ToInt16(ddlChartTypes.SelectedValue), Convert.ToInt16(ddlDataBaseElements.SelectedValue)
                , true, DateTime.Now, "", "", "", 0);
                if (UniqueID != "")
                {
                    Response.Redirect("MainChartMainConfig.aspx?sno="+UniqueID);
                    lblResult.Text = "* Save Successful" + UniqueID;

                }
            }
            
        }

        else
        {
            if (txtChartName.Text == "" || txtChartDesc.Text == "" || ddlChartTypes.SelectedValue == "" ||
                ddlDataBaseElements.SelectedValue == "" || ddlSelectDataBase.SelectedValue == "")
            {
                lblResult.Text = "*All Fields are mandatory ";
            }
            else
            {
                string UniqueID = access.UpdateMainConfigurationComp(txtChartName.Text, txtChartDesc.Text,
                Convert.ToInt16(ddlChartTypes.SelectedValue), Convert.ToInt16(ddlDataBaseElements.SelectedValue)
                , true, DateTime.Now, "", "", "", 0,sno);
                if (UniqueID != "")
                {
                   // Response.Redirect("MainChartMainConfig.aspx?sno=" + UniqueID);
                    lblResult.Text = "*Update Successful!" ;

                }
            }
        }
    }

    protected void btnProperties_Click(object sender, EventArgs e)
    {
        if (postback != "")
        {
            ClientScript.RegisterStartupScript(this.GetType(),
   "newWindow", postback);
        }
    }
    protected void btnAxis_Click(object sender, EventArgs e)
    {
        if (AxisBack != "")
        {
            ClientScript.RegisterStartupScript(this.GetType(),
   "newWindow", AxisBack);
        }

    }
    protected void btbBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MainChart.aspx");
    }
    protected void btnSampleData_Click(object sender, EventArgs e)
    {
        if (SampleData != "")
        {
            ClientScript.RegisterStartupScript(this.GetType(),
                 "newWindow", SampleData);
        }

    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        if (PreviewChart != "")
            {
                ClientScript.RegisterStartupScript(this.GetType(),
                     "newWindow", PreviewChart);
            }
    }
}