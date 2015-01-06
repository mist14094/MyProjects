using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Index : System.Web.UI.Page
{

    public static string _TrackerRetail = ConfigurationManager.ConnectionStrings["TrackerConnectionString"].ConnectionString;
    public static string _TrackerRetailDownTown = ConfigurationManager.ConnectionStrings["TrackerConnectionStringDownTown"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Login"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {

            GetServiceProviderName();
            GetStoreIDs();
            GetDirectory();
            triggerdropdown();
        }
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {


        userAdministration(ddlDirectoryList.SelectedValue == "--- Add New ---" ? "" : ddlDirectoryList.SelectedValue, txtName.Text, txtMobile.Text, txtEmail.Text, ddlServiceProvider.SelectedValue, txtNotes.Text, string.Join(",", ckbListStoreID.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList().ToArray()), cbIsActive.Checked == true ? "True" : "False");
      //  userAdministrationDownTown(ddlDirectoryList.SelectedValue == "--- Add New ---" ? "" : ddlDirectoryList.SelectedValue, txtName.Text, txtMobile.Text, txtEmail.Text, ddlServiceProvider.SelectedValue, txtNotes.Text, string.Join(",", ckbListStoreID.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList().ToArray()), cbIsActive.Checked == true ? "True" : "False");

        Response.Redirect("Index.aspx");

        //string type = string.Join(",", ckbListStoreID.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList().ToArray());

        //  type = "";

        //  List<string> TagIds = type.Split(',').ToList();

        //try
        //{
        //    for (int count = 0; count < ckbListStoreID.Items.Count; count++)
        //    {
        //        if (TagIds.Contains(ckbListStoreID.Items[count].Value))
        //        {
        //            ckbListStoreID.Items[count].Selected = true;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
    }

    public string GetDirectory()
    {

        DataSet resultset = new DataSet();
        string query = "SELECT [SNO] ,[Name]  FROM [TrackerRetail].[dbo].[RFIDAlertDirectory]";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "ServiceProviderName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    ddlDirectoryList.DataSource = resultset.Tables[0];
                    ddlDirectoryList.DataTextField = "NAME";
                    ddlDirectoryList.DataValueField = "SNO";
                    ddlDirectoryList.DataBind();
                    ddlDirectoryList.Items.Add("--- Add New ---");
                }
            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }
    public string GetStoreIDs()
    {
        DataSet resultset = new DataSet();
        string query = "SELECT [StoreID] ,[KT_StoreName] FROM [TrackerRetail].[dbo].[Stores]";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "ServiceProviderName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    ckbListStoreID.DataSource = resultset.Tables[0];
                    ckbListStoreID.DataTextField = "KT_StoreName";
                    ckbListStoreID.DataValueField = "StoreID";
                    ckbListStoreID.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }

    public string GetServiceProviderName()
    {
        DataSet resultset = new DataSet();
        string query = "SELECT [ServiceProviderName] ,[EquivalentName] FROM [TrackerRetail].[dbo].[ServiceProvider]";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "ServiceProviderName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {

                    ddlServiceProvider.DataSource = resultset.Tables[0];
                    ddlServiceProvider.DataTextField = "ServiceProviderName";
                    ddlServiceProvider.DataValueField = "EquivalentName";
                    ddlServiceProvider.DataBind();

                }
            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }
    protected void ddlDirectoryList_SelectedIndexChanged(object sender, EventArgs e)
    {
        triggerdropdown();
    }


    public void triggerdropdown()
    {
        if (ddlDirectoryList.SelectedValue == "--- Add New ---")
        {
            resetValues();

        }
        else
        {
            setValues(ddlDirectoryList.SelectedValue);
        }
    }


    public void resetValues()
    {
        txtName.Text = "";
        txtMobile.Text = "";
        txtEmail.Text = "";
        txtNotes.Text = "";

        for (int count = 0; count < ckbListStoreID.Items.Count; count++)
        {
            ckbListStoreID.Items[count].Selected = false;
        }
        cbIsActive.Checked = false;
    }
    public void CheckValues(string type)
    {


        List<string> TagIds = type.Split(',').ToList();

        try
        {
            for (int count = 0; count < ckbListStoreID.Items.Count; count++)
            {
                ckbListStoreID.Items[count].Selected = false;
                if (TagIds.Contains(ckbListStoreID.Items[count].Value))
                {
                    ckbListStoreID.Items[count].Selected = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    public void userAdministration(string strsno, string strName, string strValidNo, string strEmailID, string strService, string strNotes, string strStoreID, string strIsActive)
    {
        try
        {
            SqlConnection connection = new SqlConnection(_TrackerRetail);
            SqlCommand command = new SqlCommand("pr_RFIDAlertUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar)).Value = strName;
            command.Parameters.Add(new SqlParameter("@ValidNo", SqlDbType.VarChar)).Value = strValidNo;
            command.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.VarChar)).Value = strEmailID;
            command.Parameters.Add(new SqlParameter("@ServiceProvider", SqlDbType.VarChar)).Value = strService;
            command.Parameters.Add(new SqlParameter("@Notes", SqlDbType.VarChar)).Value = strNotes;
            command.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.VarChar)).Value = strStoreID;
            command.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit)).Value = bool.Parse(strIsActive);
            command.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime)).Value = DateTime.Now.ToShortTimeString();
            command.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now.ToShortTimeString();
            command.Parameters.Add(new SqlParameter("@SNO", SqlDbType.VarChar)).Value = strsno;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }
        catch (Exception ex)
        {
        }
        
    }


    public void userAdministrationDownTown(string strsno, string strName, string strValidNo, string strEmailID, string strService, string strNotes, string strStoreID, string strIsActive)
    {
        try
        {
            SqlConnection connection = new SqlConnection(_TrackerRetailDownTown);
            SqlCommand command = new SqlCommand("pr_RFIDAlertUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar)).Value = strName;
            command.Parameters.Add(new SqlParameter("@ValidNo", SqlDbType.VarChar)).Value = strValidNo;
            command.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.VarChar)).Value = strEmailID;
            command.Parameters.Add(new SqlParameter("@ServiceProvider", SqlDbType.VarChar)).Value = strService;
            command.Parameters.Add(new SqlParameter("@Notes", SqlDbType.VarChar)).Value = strNotes;
            command.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.VarChar)).Value = strStoreID;
            command.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit)).Value = bool.Parse(strIsActive);
            command.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime)).Value = DateTime.Now.ToShortTimeString();
            command.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now.ToShortTimeString();
            command.Parameters.Add(new SqlParameter("@SNO", SqlDbType.VarChar)).Value = strsno;

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }
        catch (Exception ex)
        {
        }
    }

    public void setValues(string SNO)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT * FROM [TrackerRetail].[dbo].[RFIDAlertDirectory] where sno=" + SNO;
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "ServiceProviderName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    txtName.Text = resultset.Tables[0].Rows[0]["Name"].ToString();
                    txtEmail.Text = resultset.Tables[0].Rows[0]["EmailID"].ToString();
                    txtMobile.Text = resultset.Tables[0].Rows[0]["ValidNo"].ToString();
                    txtNotes.Text = resultset.Tables[0].Rows[0]["Notes"].ToString();
                    ddlServiceProvider.SelectedValue = resultset.Tables[0].Rows[0]["ServiceProvider"].ToString();
                    CheckValues(resultset.Tables[0].Rows[0]["StoreID"].ToString());
                    if (resultset.Tables[0].Rows[0]["IsActive"].ToString() == "True")
                    {
                        cbIsActive.Checked = true;
                    }
                    else
                    {
                        cbIsActive.Checked = false;
                    }

                }
            }
        }
        catch (Exception ex)
        {

        }

    }

    public string DeleteUser(string sno)
    {
        DataSet resultset = new DataSet();
        string query = "DELETE FROM [TrackerRetail].[dbo].[RFIDAlertDirectory] WHERE SNO = '" + sno + "'";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "ServiceProviderName");
            }

            if (resultset != null)
            {
               
            }
        }
        catch (Exception ex)
        {

        }



        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "ServiceProviderName");
            }

            if (resultset != null)
            {

            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }



    public string DeleteUserDownTown(string sno)
    {
        DataSet resultset = new DataSet();
        string query = "DELETE FROM [TrackerRetail].[dbo].[RFIDAlertDirectory] WHERE SNO = '" + sno + "'";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetailDownTown))
            {
                dataAdapter.Fill(resultset, "ServiceProviderName");
            }

            if (resultset != null)
            {

            }
        }
        catch (Exception ex)
        {

        }



        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "ServiceProviderName");
            }

            if (resultset != null)
            {

            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteUser(ddlDirectoryList.SelectedValue);
    //    DeleteUserDownTown(ddlDirectoryList.SelectedValue);
        Response.Redirect("Index.aspx");

    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        Session["Login"] = null;
        Response.Redirect("Login.aspx");
    }
}