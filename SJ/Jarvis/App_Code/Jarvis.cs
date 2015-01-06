using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Jarvis
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Jarvis : System.Web.Services.WebService {

    public static string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
    public Jarvis () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string SpeakToJarvis(String speech)
    {
        return CentralProcessor(speech);
    }

    [WebMethod]
    public string SpeakToJarvisWithGrid(String speech, String Option)
    {
        if (Option == "0")
        {
            return CentralProcessor(speech);
        }
        else
        {
            //
            DataTable table = new DataTable();
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            //
            // Here we add five DataRows.
            //
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            return GetJson(table);
        }

    }


    public string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new

        System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows =
          new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName.Trim(), dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    
    public string CentralProcessor(string Speech)
    {
        string Params = "";
        string result = "";
        Speech = Speech.ToLower().Replace(",", " ");
        Speech = Speech.ToLower().Replace("and", "");
        Speech = Speech.ToLower().Replace("  ", " ");


        if (Speech.ToUpper().Contains("COUPON"))
        {
            result = "http://192.168.1.17/JarvisV2/CouponSales.aspx?selection=Stacked15Area&GraphType=Stack";
        }


        else if (Speech.ToUpper().Contains("MENTHOL"))
        {
            string[] words = StringSplitter(Speech);
            Params = QueryBuilder(words);
          
            string storeID="4", NumberofDays="15";
            ///Number oF days
            int dayindex = 0;
            dayindex = Array.IndexOf(words, "days");
            if (dayindex < 0)
            {
                dayindex = Array.IndexOf(words, "day");
            }

            if (dayindex > 0 && words.Count()-1 >= dayindex)
            {
                NumberofDays = words[dayindex-1];
            }

            if (Array.IndexOf(words, "today") > -1)
            {
                NumberofDays = "1";
            }

            if (Array.IndexOf(words, "yesterday") > -1)
            {
                NumberofDays = "2";
            }

            ///////////////////////////////

            /////////////////-Store Number -/////////////

            int StoreIndex= 0;
            StoreIndex = Array.IndexOf(words, "store");
            if (StoreIndex < 0)
            {
                StoreIndex = Array.IndexOf(words, "stores");
            }

            if (StoreIndex > 0)
            {
                storeID="";
                int storeid;
                for (int j = StoreIndex+1; j <= words.Count() - 1; j++)
                {
                    bool ParseResult = Int32.TryParse(words[j], out storeid);
                    if (ParseResult)
                    {
                        storeID = storeID + "," + storeid.ToString();

                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (Array.IndexOf(words, "all") > -1)
            {
                storeID = "2,3,4,5,6,9,10,12";
            }

            ////////////////////////////////////////////

            result = "http://192.168.1.17/jarvisv2/newmenthol.aspx?numberofdays=" + NumberofDays + "&storeid=" + storeID;
        }

        else
        {
            string[] words = StringSplitter(Speech);
            Params = QueryBuilder(words);
            string SystemID = strSystemName(Params, _RFIDSystem);
            string ReportID = strReportName(Params, _RFIDSystem, SystemID);
         
            if (ReportID != "0")
            {
                result = strSystemURL(SystemID, _RFIDSystem) + strReportURL(ReportID, _RFIDSystem) + strAttibuteBuilder(ReportID, Params, _RFIDSystem);
            }
            else
            {
                result = "notfound.aspx";
            }
        }
        return result;
    }



    public string[] StringSplitter(string text)
    {
        return text.Split(' ');

    }
    public string QueryBuilder(string[] text)
    {
        string result = "";
        for (int i = 0; i < text.Count(); i++)
        {
            result = result + "'" + text[i] + "',";
        }
        if (result.Length > 0)
        {
            result = result.Substring(0, result.Length - 1);
        }
        return result;
    }
    public string strSystemName(string Parameters, string ConString)
    {
        DataSet resultset = new DataSet();
        string query = "  SELECT  TOP 1 SystemID FROM [SystemKeywords] WHERE Keyword in  ( " + Parameters + ")";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    if (resultset.Tables[0].Rows.Count > 0)
                    {
                        return resultset.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            else
            {
                return "1";
            }
        }
        catch (Exception ex)
        {

        }
        return "1";
    }
    public string strReportName(string Parameters, string ConString, string SystemID)
    {
        DataSet resultset = new DataSet();
        string query = "  SELECT top 1 REPORTID FROM [ReportKeywords] WHERE ReportID IN (SELECT ID  FROM [ReportName] WHERE systemID =" + SystemID + ") AND Keyword IN (" + Parameters + ")";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    if (resultset.Tables[0].Rows.Count > 0)
                    {
                        return resultset.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            else
            {
                return "0";
            }
        }
        catch (Exception ex)
        {

        }
        return "0";
    }
    public string strSystemURL(string Parameters, string ConString)
    {
        DataSet resultset = new DataSet();
        string query = "  SELECT [PrefixURL]  FROM [Jarvis].[dbo].[Systems] WHERE id = " + Parameters;
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    if (resultset.Tables[0].Rows.Count > 0)
                    {
                        return resultset.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            else
            {
                return "0";
            }
        }
        catch (Exception ex)
        {

        }
        return "0";
    }
    public string strReportURL(string Parameters, string ConString)
    {
        DataSet resultset = new DataSet();
        string query = "  SELECT [ReportLink]  FROM [Jarvis].[dbo].[ReportName] WHERE id = " + Parameters;
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    if (resultset.Tables[0].Rows.Count > 0)
                    {
                        return resultset.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            else
            {
                return "0";
            }
        }
        catch (Exception ex)
        {

        }
        return "0";
    }
    public string strAttibuteBuilder(string strReportID, string Parameters, string ConString)
    {
        string result = "";
        DataSet ds = new DataSet();
        ds = strReportAttribute(_RFIDSystem, strReportID);
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            result = result + "?";
                        }
                        else
                        {
                            result = result + "&";
                        }
                        string temp = strReportAttributeKeywords(Parameters, _RFIDSystem, ds.Tables[0].Rows[i]["ID"].ToString());
                        result = result + ds.Tables[0].Rows[i]["Value"] + "=" + (temp != "0" ? temp : ds.Tables[0].Rows[i]["DefaultValue"].ToString());

                    }
                }
            }
        }

        return result;
    }
    public string strReportAttributeKeywords(string Parameters, string ConString, string ID)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT TOP 1 Value  FROM [ReportAttributeKeywords] WHERE [ReportAttributeID] = '" + ID + "' AND keyword IN (" + Parameters + ")";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    if (resultset.Tables[0].Rows.Count > 0)
                    {
                        return resultset.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            else
            {
                return "0";
            }
        }
        catch (Exception ex)
        {

        }
        return "0";
    }
    public DataSet strReportAttribute(string ConString, string ID)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT * FROM [Jarvis].[dbo].[ReportAttributes] WHERE ReportID=" + ID;

        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, ConString))
        {
            dataAdapter.Fill(resultset, "VendorName");
        }



        return resultset;
    }
}
