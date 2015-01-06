using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JarvisBusinessAccess;

public partial class LineChartYAxisProperties : System.Web.UI.Page
{
    JarvisBusinessAccess.DefaultBusinessAccess jbAccess = new DefaultBusinessAccess();
    private static bool Addnew = false;
    private static string SNO = "";
    private static string MainChartConfigRefNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Username"] == null)
        {
            Response.Redirect("Login.aspx");
        }


        Addnew = false;
        SNO = "";
        MainChartConfigRefNo = "";
        if (Request.QueryString["AddNew"] != null)
        {
            Addnew = true;
        }

        if (Request.QueryString["Sno"] != null)
        {
            SNO = Request.QueryString["Sno"];
        }
        if (Request.QueryString["MainChartConfigRefNo"] != null)
        {
            MainChartConfigRefNo = Request.QueryString["MainChartConfigRefNo"];
        }
        if (!IsPostBack)
        {

            //[MainChartReferenceChartNo]
            if (SNO == "" || SNO == null)
            {
                if (MainChartConfigRefNo != "")
                {
                    btnAddNew.Text = "Add new";
                    string databaseelementsno = jbAccess.SelectChartNameDesc().AsEnumerable()
                        .Where(row => row["MainChartConfigurationRefNo"].ToString().Equals(MainChartConfigRefNo))
                        .FirstOrDefault()["DataBaseElementSno"].ToString();

                    ddViewsList.DataSource = jbAccess.GetSampleData(databaseelementsno)
                              .Columns.Cast<DataColumn>()
                              .Select(x => x.ColumnName)
                              .ToArray();
                    ddViewsList.DataBind();
                    txtCollectionAlias.Text = ddViewsList.SelectedValue;
                }

                //  ddViewsList.DataSource = jbAccess.GetChartList().Where(row => row["sno"].ToString().Equals("1"));
            }

            else
            {

                btnAddNew.Text = "Update";
                DataTable dt = jbAccess.GetYAxisValueFromCLSSno(Request.QueryString["Sno"]);
                foreach (DataRow drRow in dt.Rows)
                {
                    txtCollectionAlias.Text = drRow["CollectionAlias"].ToString();

                    chkSwapLocation.Checked = Convert.ToBoolean(drRow["SwapLocation"]);
                    txtTicksRepeat.Text = drRow["TicksRepeat"].ToString();
                    txtTextAngle.Text = drRow["AxisTextAngle"].ToString();
                    txtAxisTextAngleX.Text = drRow["AxisTextAngleX"].ToString();
                    txtAxisTextAngleY.Text = drRow["AxisTextAngleY"].ToString();
                    txtAxisTextAngleStep.Text = drRow["AxisTextAngleStep"].ToString();
                    txtTitle.Text = drRow["TitleText"].ToString();


                    if (drRow["DataBaseElementSno"].ToString() != "")
                    {
                        //DynamicBusinessAccess businessAccess = new DynamicBusinessAccess(defaultaccess.GetDataBaseConnectionStringRef(int.Parse(ddlDataBaseElements.SelectedValue)));

                        ddViewsList.DataSource = jbAccess.GetSampleData(drRow["DataBaseElementSno"].ToString())
                                                            .Columns.Cast<DataColumn>()
                                                             .Select(x => x.ColumnName)
                                                             .ToArray();
                        ddViewsList.DataBind();
                        ddViewsList.SelectedValue = drRow["DataFieldY"].ToString();
                    }

                    chkEnablePointSelection.Checked = Convert.ToBoolean(drRow["EnablePointSelection"]);
                    ddlStackMode.SelectedValue = drRow["StackMode"].ToString();
                    txtDrawWidth.Text = drRow["DrawWidth"].ToString();
                    txtDrawRadius.Text = drRow["DrawRadius"].ToString();
                    chkSwapLocation.Checked = Convert.ToBoolean(drRow["SwapLocation"]);
                    

                }

            }



        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (ddViewsList.SelectedValue == "" || txtCollectionAlias.Text=="" || txtTicksRepeat.Text == "" || txtTextAngle.Text == "" || txtAxisTextAngleX.Text == ""
               || txtAxisTextAngleY.Text == "" || txtAxisTextAngleStep.Text == "" || txtTitle.Text == "" ||txtDrawRadius.Text=="" || txtDrawWidth.Text=="")
        {
            lblWarning.Text = "* All fields are Neccessary";
        }
        else
        {
            if (MainChartConfigRefNo != "")
            {
                string result = jbAccess.InsertMainChartAxisBase(MainChartConfigRefNo, 1, 0, Convert.ToDecimal(txtTicksRepeat.Text),
                     Convert.ToBoolean(chkSwapLocation.Checked),
                     ddViewsList.SelectedValue, Convert.ToInt16(txtTextAngle.Text),
                     Convert.ToInt16(txtAxisTextAngleX.Text), Convert.ToInt16(txtAxisTextAngleY.Text),
                     Convert.ToInt16(txtAxisTextAngleStep.Text), txtTitle.Text, true, DateTime.Now);

                string Result_LineSeries = jbAccess.InsertChartLineSeries(Convert.ToInt16(result),
                    ddViewsList.SelectedValue, txtCollectionAlias.Text,
                    chkEnablePointSelection.Checked,Convert.ToInt16(txtDrawWidth.Text),
                    Convert.ToInt16(txtDrawRadius.Text), Convert.ToInt16(ddlStackMode.SelectedValue),
                    true, DateTime.Now);
                if (result != "")
                {
                    lblWarning.Text = "* Update Successful";
                    Response.Redirect("LineChartYAxisProperties.aspx?Sno=" + Result_LineSeries);
                }
            }

            if (SNO != "")
            {
                string result = jbAccess.UpdateMainChartAxisBase(1, 0, Convert.ToDecimal(txtTicksRepeat.Text),
                   Convert.ToBoolean(chkSwapLocation.Checked),
                   ddViewsList.SelectedValue, Convert.ToInt16(txtTextAngle.Text),
                   Convert.ToInt16(txtAxisTextAngleX.Text), Convert.ToInt16(txtAxisTextAngleY.Text),
                   Convert.ToInt16(txtAxisTextAngleStep.Text), txtTitle.Text, true, DateTime.Now, Sno:jbAccess.GetMainChartAxisBaseSnofromSno(SNO));

               string Result_LineSeries = jbAccess.UpdateChartLineSeries(ddViewsList.SelectedValue, txtCollectionAlias.Text,
                    chkEnablePointSelection.Checked, Convert.ToInt16(txtDrawWidth.Text),
                    Convert.ToInt16(txtDrawRadius.Text), Convert.ToInt16(ddlStackMode.SelectedValue),
                    true, DateTime.Now, SNO);
               lblWarning.Text = "* Update Successful";
                
            }
        }
    }
    protected void ddViewsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCollectionAlias.Text = ddViewsList.SelectedValue;
    }
}