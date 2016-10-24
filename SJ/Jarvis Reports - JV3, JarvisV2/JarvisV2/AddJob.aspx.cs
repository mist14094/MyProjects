using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class AddJob : System.Web.UI.Page
{
    public static string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)

        {
            lblMessage.Text = "";
        }

    }


    public DataTable InsertRFIDLogs(string RFID, string JobNumber, int Quantity)
    {
        string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand("SJ_WAREHOUSE_AUDIT", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RFID", RFID));
            cmd.Parameters.Add(new SqlParameter("@JobNumber", JobNumber));
            cmd.Parameters.Add(new SqlParameter("@Quantity", Quantity));
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

            int selectedQuanity = 0;
            if (rdlType.SelectedValue == "Take out")
            {
                selectedQuanity= Int16.Parse(txtTotalWeight.Text) *-1;
            }

            else
            {
                selectedQuanity = Int16.Parse(txtTotalWeight.Text);
            }

            var identity = InsertRFIDLogs(txtRFID.Text, txtJobNumber.Text,selectedQuanity);

            if (identity != null)
            {
                if (identity.Rows[0][0].ToString() != "")
                {
                    lblMessage.Text = "Added Successfully!";
                    txtRFID.Text = "";
                    txtTotalWeight.Text = "";
                    txtJobNumber.Text = "";
                }
                else
                {
                    lblMessage.Text = "RFID Tag scanned not exist - Check with IT";
                }
            }
            else
            {
                lblMessage.Text = "Something Went wrong!";
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error Occured";

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        txtRFID.Text = "";
        txtTotalWeight.Text = "";
        txtJobNumber.Text = "";
        lblMessage.Text = "";
    }

    protected void txtRFID_TextChanged(object sender, EventArgs e)
    {

    }
}