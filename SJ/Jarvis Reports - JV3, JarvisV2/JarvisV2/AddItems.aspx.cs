using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class AddItems : System.Web.UI.Page
{
    public static string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)

        {
            ddlTobaccoType.DataSource = GetProductName();
            ddlTobaccoType.DataTextField = "Desc";
            ddlTobaccoType.DataValueField = "UPC";
            ddlTobaccoType.DataBind();
            lblMessage.Text = "";
        }

    }


    public DataTable InsertRFIDValues(string TobaccoType, string MfgDate, int TotalWeight, float FinalMoisture, int NewTote, string RFID)
    {
        string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand("SJ_WAREHOUSE_INSERT", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TobaccoType", TobaccoType));
            cmd.Parameters.Add(new SqlParameter("@MfgDate", MfgDate));
            cmd.Parameters.Add(new SqlParameter("@FinalMoisture", FinalMoisture));
            cmd.Parameters.Add(new SqlParameter("@NewTote", NewTote));
            cmd.Parameters.Add(new SqlParameter("@RFID", RFID));
            cmd.Parameters.Add(new SqlParameter("@TotalWeightLb", TotalWeight));

            
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);
            return allData;
        }
        catch (Exception ex)
        { }

        return null;
    }
    public DataTable GetProductName()
    {
        string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT UPC, [Desc] FROM TrackerRetail.dbo.Products WHERE upc LIKE '2000000000%'", connection);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);
            return allData;
        }
        catch (Exception ex)
        { }

        return null;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {


            var identity = InsertRFIDValues(ddlTobaccoType.SelectedValue, txtMfgDate.Text, int.Parse(txtTotalWeight.Text),
                    float.Parse(txtMoisture.Text), int.Parse(txtNewTote.Text), txtRFID.Text);
            if (identity != null)
            {
                lblMessage.Text = "Added Successfully!";
                txtMfgDate.Text ="";
                txtTotalWeight.Text = "";
                txtMoisture.Text="";
                txtNewTote.Text="";
                txtRFID.Text = "";
            }
            else
            {
                lblMessage.Text = "Tag Already Exists";
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error Occured";

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        txtMfgDate.Text = "";
        txtTotalWeight.Text = "";
        txtMoisture.Text = "";
        txtNewTote.Text = "";
        txtRFID.Text = "";
        lblMessage.Text = "";
    }
}