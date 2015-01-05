using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class BEPUPC : System.Web.UI.Page
{
    public static string _BreakEvenDB = ConfigurationManager.ConnectionStrings["BreakEven"].ConnectionString;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

          //C1AutoComplete1.Items.Add(GetDataItem
            
            
            C1.Web.Wijmo.Controls.C1AutoComplete.C1AutoCompleteDataItem as1 = new C1.Web.Wijmo.Controls.C1AutoComplete.C1AutoCompleteDataItem();
            as1.Label="Newoption";
            as1.Value="Newoption";
           // C1AutoComplete1.Items.Add(as1);
        }
    }

    public DataSet drpName()
    {
        SqlConnection Conn = new  SqlConnection(_BreakEvenDB);
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string Select = "SELECT  distinct(upc) from [KT_BreakEven].[dbo].[ProductCatalog]";
        da = new SqlDataAdapter(Select, Conn);
        da.Fill(ds);
        return ds;
    }
}