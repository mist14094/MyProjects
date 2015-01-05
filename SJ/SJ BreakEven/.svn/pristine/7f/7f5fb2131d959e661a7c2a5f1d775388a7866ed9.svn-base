using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C1.Web.Wijmo.Controls.C1Chart;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using C1.Web.Wijmo.Controls.C1ComboBox;
using BreakEvenBAL;
public partial class BEPProduct : System.Web.UI.Page
{
    BreakEvenBAL.BEBAL BEBALLayer = new BEBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvSearchResult.Visible = false;
        }
        else
        {
            InitialSetup();
        }
    }

   
    public DataSet GetUPCandSKU(string descr)
    {
        DataSet ds = new DataSet();
        if (descr.Trim() != "")
        {
            ds = BEBALLayer.GetUPCSKUbyDesc(descr);
        }
        return ds;
    }

    public void fillUPC()
    {
        cbUPC.Items.Clear();
        DataSet ds = new DataSet();
        ds = GetUPCandSKU(acDescription.Text);
        if (ds != null && ds.Tables.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                C1ComboBoxItem C1CBI = new C1ComboBoxItem();
                string[] separators = { "||" };
                string value = ds.Tables[0].Rows[i]["Combined"].ToString();
                string[] Values = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                C1CBI.Cells = Values;
                C1CBI.Value = ds.Tables[0].Rows[i]["SKU"].ToString();
                C1CBI.Text = ds.Tables[0].Rows[i]["UPC"].ToString();
                cbUPC.Items.Add(C1CBI);
            }
        }
    }
    protected void acDescription_TextChanged(object sender, EventArgs e)
    {
        fillUPC();
        GridBind();
        gvSearchResult.DataSource = Session["TaskTable"] as DataSet;
        gvSearchResult.DataBind();
    }
    protected void cbUPC_SelectedIndexChanged(object sender, C1ComboBoxEventArgs args)
    {
        GridBind();
        gvSearchResult.DataSource = Session["TaskTable"] as DataSet;
        gvSearchResult.DataBind();
    }
    protected void cbUPC_SelectedIndexChanged1(object sender, C1ComboBoxEventArgs args)
    {
        try
        {
            acSKU.Text = cbUPC.Items[args.NewSelectedIndex].Cells[1].ToString();
            if (acSKU.Text == "-")
            {
                acSKU.Text = "";
            }
        }
        catch (Exception ex)
        {
            acSKU.Text = "";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridBind();
        gvSearchResult.DataSource = Session["TaskTable"] as DataSet;
        gvSearchResult.DataBind();
    }
    protected void gvSearchResult_PageIndexChanging(object sender, C1.Web.Wijmo.Controls.C1GridView.C1GridViewPageEventArgs e)
    {
        gvSearchResult.DataSource = Session["TaskTable"] as DataSet;
        gvSearchResult.PageIndex = e.NewPageIndex;
        gvSearchResult.DataBind();
    }

    public void GridBind()
    {
        DataSet ds = new DataSet();
        if (!(cbUPC.Text == "" && acSKU.Text == "" && acDescription.Text == "" && cbVendor.Text == ""))
        {
            Session["TaskTable"] = BEBALLayer.getBreakEvenProductSearch(cbUPC.Text, acSKU.Text, acDescription.Text, cbVendor.Text);
            gvSearchResult.Visible = true; ;
            try
            {
                lblRecordCount.Text ="Total Record(s) found : " + (Session["TaskTable"] as DataSet).Tables[0].Rows.Count.ToString();
                lblGenerated.Visible = true;
                lblGenerated.Text = "Generated on " + DateTime.Now.ToString("F");
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void gvSearchResult_Sorting(object sender, C1.Web.Wijmo.Controls.C1GridView.C1GridViewSortEventArgs e)
    {
        DataSet sa =  Session["TaskTable"] as DataSet;
        DataTable dataTable = sa.Tables[0];

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (e.SortDirection == C1.Web.Wijmo.Controls.C1GridView.C1SortDirection.Ascending)
            {
                dataView.Sort = e.SortExpression + " " + "DESC";
            }

            else
            {
                dataView.Sort = e.SortExpression + " " + "ASC";
            }
            gvSearchResult.DataSource = dataView;
            gvSearchResult.DataBind();
        }
    }
    public void InitialSetup()
    {

        SqlDataSource ItemDesc = new SqlDataSource();
        ItemDesc.ID = "ItemDesc";
        this.Page.Controls.Add(ItemDesc);
        ItemDesc.ConnectionString = BEBALLayer.GetConnKTBEValue();
        ItemDesc.SelectCommand = BEBALLayer.GetDistinctDescription();
        ItemDesc.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        acDescription.DataSourceID = ItemDesc.ID;

        SqlDataSource SKUString = new SqlDataSource();
        SKUString.ID = "SKU";
        this.Page.Controls.Add(SKUString);
        SKUString.ConnectionString = BEBALLayer.GetConnKTBEValue();
        SKUString.SelectCommand = BEBALLayer.GetDistinctSKU();
        SKUString.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        acSKU.DataSourceID = SKUString.ID;

        SqlDataSource VendorName = new SqlDataSource();
        VendorName.ID = "VendorName";
        this.Page.Controls.Add(VendorName);
        VendorName.ConnectionString = BEBALLayer.GetConnKTBEValue();
        VendorName.SelectCommand = BEBALLayer.GetDistinctVendorName();
        VendorName.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        cbVendor.DataSourceID = VendorName.ID;


    }

}