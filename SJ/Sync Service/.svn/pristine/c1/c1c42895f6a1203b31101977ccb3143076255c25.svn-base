using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for SyncService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SyncService : System.Web.Services.WebService
{

    public static string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDSystem"].ConnectionString;
    public static string _SysproSystem = ConfigurationManager.ConnectionStrings["SysproSystem"].ConnectionString;
    public static string _PinnacleSystem = ConfigurationManager.ConnectionStrings["PinnacleSystem"].ConnectionString;
    public static string _QualityComcash = ConfigurationManager.ConnectionStrings["QualityComcash"].ConnectionString;
    public static string _RodewayComcash = ConfigurationManager.ConnectionStrings["RodeWayComcash"].ConnectionString;

    public SyncService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 

        //     _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDSystem"].ConnectionString;
        //  _SysproSystem = ConfigurationManager.ConnectionStrings["SysproSystem"].ConnectionString;
        //  _PinnacleSystem = ConfigurationManager.ConnectionStrings["PinnacleSystem"].ConnectionString;
        //  _QualityComcash = ConfigurationManager.ConnectionStrings["QualityComcash"].ConnectionString;
        //_RodewayComcash = ConfigurationManager.ConnectionStrings["RodeWayComcash"].ConnectionString;

    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public string CheckInSync(string ID)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetCheckinDetails(ID, _RFIDSystem);
            int count = 0;

            do
            {
                if (count > 3)
                {
                    break;
                }
                if (dt.Rows.Count >= 1)
                {
                    break;
                }
                dt = GetCheckinDetails(ID, _RFIDSystem);
                count++;
            } while (true);


            if (strCheckFlag(ID, "KT_CheckIN_Log", _RFIDSystem) != "True")
            {
                if (dt != null)
                {
                    if (strCheckSystem(dt.Rows[0]["ShippedStoreID"].ToString(), "Check_In_System", _RFIDSystem) == "Pinnacle")
                    {
                        PinnacleCheckin(ID, Convert.ToInt16(dt.Rows[0]["ShippedStoreID"].ToString()), dt.Rows[0]["PackageSlip"].ToString(), Convert.ToInt16(dt.Rows[0]["CreateUserID"].ToString()), dt.Rows[0]["CheInTime"].ToString(), dt.Rows[0]["RFIDs"].ToString(), bool.Parse(dt.Rows[0]["isAdhoc"].ToString()), dt.Rows[0]["UserName"].ToString(), _SysproSystem, _PinnacleSystem, _RFIDSystem);
                    }

                    if (strCheckSystem(dt.Rows[0]["ShippedStoreID"].ToString(), "Check_In_System", _RFIDSystem) == "ComcashQ")
                    {
                        ComCashCheckin(ID, Convert.ToInt16(dt.Rows[0]["ShippedStoreID"].ToString()), dt.Rows[0]["PackageSlip"].ToString(), Convert.ToInt16(dt.Rows[0]["CreateUserID"].ToString()), dt.Rows[0]["CheInTime"].ToString(), dt.Rows[0]["RFIDs"].ToString(), bool.Parse(dt.Rows[0]["isAdhoc"].ToString()), dt.Rows[0]["UserName"].ToString(), _QualityComcash, _RFIDSystem);
                    }

                    if (strCheckSystem(dt.Rows[0]["ShippedStoreID"].ToString(), "Check_In_System", _RFIDSystem) == "ComcashR")
                    {
                        ComCashCheckin(ID, Convert.ToInt16(dt.Rows[0]["ShippedStoreID"].ToString()), dt.Rows[0]["PackageSlip"].ToString(), Convert.ToInt16(dt.Rows[0]["CreateUserID"].ToString()), dt.Rows[0]["CheInTime"].ToString(), dt.Rows[0]["RFIDs"].ToString(), bool.Parse(dt.Rows[0]["isAdhoc"].ToString()), dt.Rows[0]["UserName"].ToString(), _RodewayComcash, _RFIDSystem);
                    }

                    if (strCheckSystem(dt.Rows[0]["ShippedStoreID"].ToString(), "Check_In_System", _RFIDSystem) == "Syspro")
                    {

                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
        return ID;
    }


    [WebMethod]
    public string CheckOutSync(string ID)
    {
        try
        {
            DataTable dt = new DataTable();

            int count = 0;

            dt = GetCheckOutDetails(ID, _RFIDSystem);

            do
            {
                if (count > 3)
                {
                    break;
                }
                if (dt.Rows.Count >= 1)
                {
                    break;
                }
                dt = GetCheckOutDetails(ID, _RFIDSystem);
                count++;
            } while (true);

            if (strCheckFlag(ID, "KT_CheckOut_Log", _RFIDSystem) != "True")
            {
                if (dt != null)
                {
                    if (strCheckSystem(dt.Rows[0]["SourceStoreID"].ToString(), "Check_Out_System", _RFIDSystem) == "Pinnacle")
                    {
                        PinnacleCheckout(ID, Convert.ToInt16(dt.Rows[0]["SourceStoreID"].ToString()), dt.Rows[0]["PackageSlip"].ToString(), Convert.ToInt16(dt.Rows[0]["CreateUserID"].ToString()), dt.Rows[0]["CheoutTime"].ToString(), dt.Rows[0]["RFIDs"].ToString(), bool.Parse(dt.Rows[0]["isAdhoc"].ToString()), dt.Rows[0]["UserName"].ToString(), _SysproSystem, _PinnacleSystem, _RFIDSystem);
                    }

                    if (strCheckSystem(dt.Rows[0]["SourceStoreID"].ToString(), "Check_Out_System", _RFIDSystem) == "ComcashQ")
                    {
                        ComCashCheckOut(ID, Convert.ToInt16(dt.Rows[0]["SourceStoreID"].ToString()), dt.Rows[0]["PackageSlip"].ToString(), Convert.ToInt16(dt.Rows[0]["CreateUserID"].ToString()), dt.Rows[0]["CheoutTime"].ToString(), dt.Rows[0]["RFIDs"].ToString(), bool.Parse(dt.Rows[0]["isAdhoc"].ToString()), dt.Rows[0]["UserName"].ToString(), _QualityComcash, _RFIDSystem);
                    }

                    if (strCheckSystem(dt.Rows[0]["SourceStoreID"].ToString(), "Check_Out_System", _RFIDSystem) == "ComcashR")
                    {
                        ComCashCheckOut(ID, Convert.ToInt16(dt.Rows[0]["SourceStoreID"].ToString()), dt.Rows[0]["PackageSlip"].ToString(), Convert.ToInt16(dt.Rows[0]["CreateUserID"].ToString()), dt.Rows[0]["CheoutTime"].ToString(), dt.Rows[0]["RFIDs"].ToString(), bool.Parse(dt.Rows[0]["isAdhoc"].ToString()), dt.Rows[0]["UserName"].ToString(), _RodewayComcash, _RFIDSystem);
                    }

                    if (strCheckSystem(dt.Rows[0]["SourceStoreID"].ToString(), "Check_Out_System", _RFIDSystem) == "Syspro")
                    {

                    }

                }
            }
        }
        catch (Exception ex)
        {

        }
        return ID;

    }





    public string strCheckFlag(string ID, string TableName, string ConString)
    {
        DataSet resultset = new DataSet();
        string query = "select flag from " + TableName + " where sno='" + ID + "'";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }


    public DataTable GetCheckinDetails(string id, string ConString)
    {
        DataSet resultset = new DataSet();
        DataTable dt = new DataTable();
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("select * FROM [TrackerRetail].[dbo].[KT_Checkin_Log] where SNO='" + id + "'", ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    dt = resultset.Tables[0];
                }
            }
        }
        catch (Exception ex)
        {

        }


        return dt;
    }



    public string strCheckSystem(string location, string columnname, string ConString)
    {
        DataSet resultset = new DataSet();
        string query = "select [" + columnname + "] FROM KT_CheckInOutExchange where StoreID='" + location + "'";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }


    public DataTable GetCheckOutDetails(string id, string ConString)
    {
        DataSet resultset = new DataSet();
        DataTable dt = new DataTable();
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("select * FROM [TrackerRetail].[dbo].[KT_Checkout_Log] where SNO='" + id + "'", ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    dt = resultset.Tables[0];
                }
            }
        }
        catch (Exception ex)
        {

        }


        return dt;
    }


    public string PinnacleCheckout(string ID, int ShippedStoreID, string PackageSlip, int CreateUserID, string CheInTime, string RFIDs, bool isAdhoc, string UserName, string SysproString, string PinnacleString, string RFIDString)
    {

        string fromlocation = "", tolocation = "", pinnaclefrom = "", pinnacleto = "";
        try
        {
            tolocation = ShippedStoreID.ToString();
            pinnacleto = GetVendorID(tolocation, "pinnacle_storeid", _RFIDSystem);
            if (isAdhoc)
            {
                fromlocation = "";
                pinnaclefrom = "9";
            }
            else
            {
                fromlocation = getFromLocationPackageSlip(_RFIDSystem, PackageSlip);
                if (fromlocation == "")
                {
                    fromlocation = "";
                    pinnaclefrom = "9";
                }
                else
                {
                    if (getTransferPossible(_SysproSystem, pinnaclefrom, pinnacleto) == "0")
                    {
                        fromlocation = "";
                        pinnaclefrom = "9";
                    }
                    else
                    {
                        //   fromlocation = fromlocation;
                        pinnaclefrom = GetVendorID(fromlocation, "pinnacle_storeid", _RFIDSystem);
                    }
                }
            }

            int flag = 0;
            pinnaclefrom = "100000";
            ///From Location Finished
            PackageSlip = GenInvoice(_SysproSystem, pinnacleto, pinnaclefrom, PackageSlip);
            //Invoice Finalised
            CreateInvoice(_PinnacleSystem, pinnacleto, pinnaclefrom, PackageSlip);
            //invoice Created
            string invoiceref = GetInvoiceReference(_PinnacleSystem, pinnacleto, pinnaclefrom, PackageSlip);
            DataTable dtCheckIn = new DataTable();
            dtCheckIn = GetRFIDDetails(RFIDs, ShippedStoreID.ToString(), RFIDString);
            if (dtCheckIn.Rows.Count > 0)
            {
                dtCheckIn.Columns.Add("Flag");
                for (int i = 0; i < dtCheckIn.Rows.Count; i++)
                {
                    if (CheckPLUVendorMatch(_SysproSystem, dtCheckIn.Rows[i]["SKU"].ToString(), pinnaclefrom) == "0")
                    {
                        dtCheckIn.Rows[i]["Flag"] = "n";
                        flag++;
                    }
                    else
                    {
                        InsertItemsInvoice(_PinnacleSystem, invoiceref, pinnacleto, pinnaclefrom, PackageSlip, dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["ITEMDESCRIPTION"].ToString(), "-" + dtCheckIn.Rows[i]["CNT"].ToString(), "", strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(), ShippedStoreID.ToString(), "Custom1", RFIDString));
                    }

                }

            }
            FinalizeInvoice(_PinnacleSystem, invoiceref);

            UpdateTable(ID, RFIDString, "KT_Checkout_Log", "Flag", "SNO", "1");
            if (flag == 0)
            {
                UpdateTable(ID, RFIDString, "KT_Checkout_Log", "Status", "SNO", "Completed");
            }
            else
            {
                UpdateTable(ID, RFIDString, "KT_Checkout_Log", "Status", "SNO", "Partial");
            }

            UpdateTable(ID, RFIDString, "KT_Checkout_Log", "SystemInvoice", "SNO", PackageSlip);

        }
        catch (Exception ex)
        {

        }
        return "";

    }



    public string PinnacleCheckin(string ID, int ShippedStoreID, string PackageSlip, int CreateUserID, string CheInTime, string RFIDs, bool isAdhoc, string UserName, string SysproString, string PinnacleString, string RFIDString)
    {

        string fromlocation = "", tolocation = "", pinnaclefrom = "", pinnacleto = "";
        try
        {
            tolocation = ShippedStoreID.ToString();
            pinnacleto = GetVendorID(tolocation, "pinnacle_storeid", _RFIDSystem);
            if (isAdhoc)
            {
                fromlocation = "";
                pinnaclefrom = "9";
            }
            else
            {
                fromlocation = getFromLocationPackageSlip(_RFIDSystem, PackageSlip);
                if (fromlocation == "")
                {
                    fromlocation = "";
                    pinnaclefrom = "9";
                }
                else
                {
                    if (getTransferPossible(_SysproSystem, pinnaclefrom, pinnacleto) == "0")
                    {
                        fromlocation = "";
                        pinnaclefrom = "9";
                    }
                    else
                    {
                        //   fromlocation = fromlocation;
                        pinnaclefrom = GetVendorID(fromlocation, "pinnacle_storeid", _RFIDSystem);
                    }
                }
            }

            int flag = 0;
            pinnaclefrom = "100000";
            ///From Location Finished
            PackageSlip = GenInvoice(_SysproSystem, pinnacleto, pinnaclefrom, PackageSlip);
            //Invoice Finalised
            CreateInvoice(_PinnacleSystem, pinnacleto, pinnaclefrom, PackageSlip);
            //invoice Created
            string invoiceref = GetInvoiceReference(_PinnacleSystem, pinnacleto, pinnaclefrom, PackageSlip);
            DataTable dtCheckIn = new DataTable();
            dtCheckIn = GetRFIDDetails(RFIDs, ShippedStoreID.ToString(), RFIDString);
            if (dtCheckIn.Rows.Count > 0)
            {
                dtCheckIn.Columns.Add("Flag");
                for (int i = 0; i < dtCheckIn.Rows.Count; i++)
                {
                    if (CheckPLUVendorMatch(_SysproSystem, dtCheckIn.Rows[i]["SKU"].ToString(), pinnaclefrom) == "0")
                    {
                        dtCheckIn.Rows[i]["Flag"] = "n";
                        flag++;
                    }
                    else
                    {
                        InsertItemsInvoice(_PinnacleSystem, invoiceref, pinnacleto, pinnaclefrom, PackageSlip, dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["ITEMDESCRIPTION"].ToString(), dtCheckIn.Rows[i]["CNT"].ToString(), "", strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(), ShippedStoreID.ToString(), "Custom1", RFIDString));
                    }

                }

            }
            //     FinalizeInvoice(_PinnacleSystem, invoiceref);

            UpdateTable(ID, RFIDString, "KT_CheckIN_Log", "Flag", "SNO", "1");
            if (flag == 0)
            {
                UpdateTable(ID, RFIDString, "KT_CheckIN_Log", "Status", "SNO", "Completed");
            }
            else
            {
                UpdateTable(ID, RFIDString, "KT_CheckIN_Log", "Status", "SNO", "Partial");
            }

            UpdateTable(ID, RFIDString, "KT_CheckIN_Log", "SystemInvoice", "SNO", PackageSlip);

        }
        catch (Exception ex)
        {

        }
        return "";

    }


    public string UpdateTable(string ID, string ConString, string TableName, string Columnname, string wherecolumn, string Value)
    {
        DataSet resultset = new DataSet();
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("UPDATE " + TableName + " SET [" + Columnname + "]='" + Value + "' WHERE " + wherecolumn + "='" + ID + "' ", ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }


    public string GetVendorID(string storeid, string columnname, string ConString)
    {
        DataSet resultset = new DataSet();
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("select " + columnname + " from kt_checkinoutexchange where StoreID='" + storeid + "'", ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string GetSourceID(string packingslip, string ConString)
    {
        DataSet resultset = new DataSet();
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("select SourceStoreID from KT_CICOMaster where PackageSlip='" + packingslip + "'", ConString))
            {
                dataAdapter.Fill(resultset, "SourceStoreID");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }


    public string strGetUPC(string PLU, string ConString)
    {
        DataSet resultset = new DataSet();
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("select plu  from menuitem where plu like '%" + PLU + "'", ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string FinalizeInvoice(string ConString, string Invoice)
    {
        DataSet resultset = new DataSet();
        string query = "UPDATE SJDW.dbo.HandheldInvoices SET Finalized = 'False' WHERE ID = " + Invoice + "";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }

    public string InsertItemsInvoice(string ConString, string ID, string Store, string Vendor, string Invoice, string UPC, string Description, string Qty, string ScanTime, string Cost)
    {
        DataSet resultset = new DataSet();
        //string query = "INSERT INTO SJDW.dbo.HandheldItems (ID, Store, Vendor, Invoice, UPC, Description, Qty, ScanTime, Cost) VALUES (" + ID + ",'" + Store + "','" + Vendor + "','" + Invoice + "','" + UPC + "','" + Description + "'," + Qty + ",getdate(),'" + Cost + "')";
        string query = "INSERT INTO SJDW.dbo.HandheldItems (ID, Store, Vendor, Invoice, UPC, Description, Qty, ScanTime) VALUES (" + ID + ",'" + Store + "','" + Vendor + "','" + Invoice + "','" + UPC + "','" + Description + "'," + Qty + ",getdate())";

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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }

    public string CheckPLUVendorMatch(string ConString, string SKU, string VendorID)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT COUNT(sku) AS Count FROM EPM_DW.dbo.vendor_items WHERE sku='" + SKU + "' AND vendor_id=" + VendorID;
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }

    public string GetInvoiceReference(string ConString, string StoreID, string VendorID, string PackageSlip)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT ID FROM SJDW.dbo.HandheldInvoices WHERE Store = '" + StoreID + "' AND Vendor = '" + VendorID + "' AND Invoice = '" + PackageSlip + "'";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
        return "";
    }

    public string CreateInvoice(string ConString, string StoreID, string VendorID, string PackageSlip)
    {
        DataSet resultset = new DataSet();
        string query = "INSERT INTO SJDW.dbo.HandheldInvoices (Store,Vendor,Invoice,Finalized) VALUES ('" + StoreID + "','" + VendorID + "','" + PackageSlip + "','False')";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string GenInvoice(string ConString, string StoreID, string VendorID, string PackageSlip)
    {
        if (PackageSlip == "")
        {
            PackageSlip = "1";
        }

        if (PackageSlip.Length < 10)
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);
           // PackageSlip = "D" + PackageSlip.Substring(PackageSlip.Length - 9, 9).ToString();
            PackageSlip = "D" + randomNumber.ToString();
        }
        else
        {
            PackageSlip = PackageSlip.Substring(PackageSlip.Length - 10, 10);
        }

        int cntloop = 0;
        do
        {
            cntloop++;
            string count = ChckInvoice(ConString, StoreID, VendorID, PackageSlip);
            string count1 = ChckInvoiceHH(ConString, StoreID, VendorID, PackageSlip);
            if (count == "0" && count1 == "0")
            {
                return PackageSlip;
            }
            else
            {
                Random random = new Random();
                int randomNumber = random.Next(100000000, 222222222);
              //  PackageSlip = "D" + randomNumber.ToString();//+ PackageSlip.Substring(PackageSlip.Length - 9, 9).ToString();
                if (cntloop == 1)
                {
                    PackageSlip = "D" + PackageSlip.Substring(PackageSlip.Length - 9, 9).ToString();
                }
                if (cntloop == 2)
                {
                    PackageSlip = "F" + PackageSlip.Substring(PackageSlip.Length - 9, 9).ToString();
                }
                if (cntloop == 4)
                {
                    PackageSlip = "I" + PackageSlip.Substring(PackageSlip.Length - 9, 9).ToString();
                }
                if (cntloop == 5)
                {
                    PackageSlip = "R" + PackageSlip.Substring(PackageSlip.Length - 9, 9).ToString();
                }
                if (cntloop > 5)
                {
                    PackageSlip = "D" + randomNumber.ToString();//+ PackageSlip.Substring(PackageSlip.Length - 9, 9).ToString();
                }

            }
        } while (true);


    }





    public string ChckInvoiceHH(string ConString, string StoreID, string VendorID, string PackageSlip)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT COUNT(vendor) as Count FROM [SJDW].dbo.[HandheldItems] WHERE store = '" + StoreID + "' AND vendor = '" + VendorID + "' AND invoice = '" + PackageSlip + "'";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }



    public string ChckInvoice(string ConString, string StoreID, string VendorID, string PackageSlip)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT COUNT(vendor) as Count FROM EPM_DW.dbo.invoice_number_history WHERE store = '" + StoreID + "' AND vendor = '" + VendorID + "' AND invoice = '" + PackageSlip + "'";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string getTransferPossible(string ConString, string StoreID, string VendorID)
    {
        DataSet resultset = new DataSet();
        string query = " select * from EPM_DW.dbo.vendor_store_xref WHERE str_nbr = '" + StoreID + "' and LTRIM(RTRIM(" + VendorID + ")) ='9'";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string getFromLocationPackageSlip(string ConString, string PackageSlip)
    {
        DataSet resultset = new DataSet();
        string query = " select SourceStoreID from KT_CICOMaster where PackageSlip='" + PackageSlip + "' ";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string CheckVendor(string ConString, string storeID)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT     vendids.vendor, vendors.vendor_name 		FROM         (SELECT DISTINCT vendor FROM          EPM_DW.dbo.invoice_number_history WHERE      (store = " + storeID + ")) AS vendids LEFT OUTER JOIN EPM_DW.dbo.vendors ON vendids.vendor = vendors.vendor_id ";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    //---------Pinnacle Check in end ------------------

    //---------Check Out Start ------------------
    //                          (int ShippedStoreID, string PackageSlip, int CreateUserID, string CheInTime, string RFIDs, bool isAdhoc, string UserName, string SysproString, string PinnacleString, string RFIDString)
    public string ComCashCheckOut(string ID, int ShippedStoreID, string PackageSlip, int CreateUserID, string CheTime, string RFIDs, bool isAdhoc, string UserName, string QualityStoreCon, string RFIDString)
    {
        string location = "";
        DataTable dtCheckIn = new DataTable();

        try
        {
            location = GetLocation(QualityStoreCon);
            string Transfer_List_Max = Get_Transfer_List(QualityStoreCon, location);
            if (location != "")
            {

                dtCheckIn = GetRFIDDetails(RFIDs, ShippedStoreID.ToString(), RFIDString);
                if (dtCheckIn.Rows.Count > 0)
                {
                    decimal cost = 0;
                    for (int i = 0; i < dtCheckIn.Rows.Count; i++)
                    {
                        string PLU = strGetPLU(dtCheckIn.Rows[i]["UPC"].ToString(), QualityStoreCon);
                        if (PLU == "")
                        {
                            string res = InserNewItem(dtCheckIn.Rows[i]["UPC"].ToString(), location, dtCheckIn.Rows[i]["SKU"].ToString(), "0", dtCheckIn.Rows[i]["ITEMDESCRIPTION"].ToString(), dtCheckIn.Rows[i]["SIZECODE"].ToString(), strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(), ShippedStoreID.ToString(), "Price", RFIDString), "10", "0", strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(), ShippedStoreID.ToString(), "Custom1", RFIDString), QualityStoreCon);
                        }

                        cost = cost + Convert.ToDecimal(strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(), ShippedStoreID.ToString(), "Custom1", RFIDString));


                        Insert_transfer(Transfer_List_Max, location, dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(), dtCheckIn.Rows[i]["ITEMDESCRIPTION"].ToString(), dtCheckIn.Rows[i]["SIZECODE"].ToString(), "-" + dtCheckIn.Rows[i]["CNT"].ToString(), strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(), ShippedStoreID.ToString(), "price", RFIDString), strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(), ShippedStoreID.ToString(), "Custom1", RFIDString), "-" + Convert.ToString((Convert.ToDecimal(strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(), ShippedStoreID.ToString(), "Custom1", RFIDString)) * Convert.ToDecimal(dtCheckIn.Rows[i]["CNT"].ToString()))), "Out", "0", QualityStoreCon);
                        string FIFOIndex = GetFIFOIndex(QualityStoreCon, location);
                        Insert_rcv_fifo(dtCheckIn.Rows[i]["UPC"].ToString(), "", "-" + dtCheckIn.Rows[i]["CNT"].ToString(), "10", "0", strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(), ShippedStoreID.ToString(), "Custom1", RFIDString), location, "", "", FIFOIndex, QualityStoreCon);
                        strUpdateMenuItems(QualityStoreCon, dtCheckIn.Rows[i]["CNT"].ToString(), dtCheckIn.Rows[i]["UPC"].ToString(), location);
                    }
                    Insert_TransferList(QualityStoreCon, Transfer_List_Max, location, "", "", "1", cost.ToString(), "3", "9999", "Automated Transfer", GetStoreID(QualityStoreCon, "1"), "");

                    UpdateTable(ID, RFIDString, "KT_CheckOut_Log", "Flag", "SNO", "1");
                    UpdateTable(ID, RFIDString, "KT_CheckOut_Log", "Status", "SNO", "Completed");
                    UpdateTable(ID, RFIDString, "KT_CheckOut_Log", "SystemInvoice", "SNO", Transfer_List_Max);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return "";
    }

    public string GetStoreID(string ConString, string StoreID)
    {
        DataSet resultset = new DataSet();
        string query = "select Store_name from Ship_To where storeID='" + StoreID + "'";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string Insert_TransferList(string ConString, string Transfer_No, string LocationNo, string Dates, string Times, string Transfer_Status, string COST, string EMPLOYEE_NO, string Customer_No, string Notes, string StoreName, string Modified)
    {
        DataSet resultset = new DataSet();
        string query = "INSERT INTO Transfer_List(Transfer_No,LocationNo,Dates,Times,Transfer_Status,COST,EMPLOYEE_NO,Customer_No,Notes,StoreName,Modified) VALUES (" + Transfer_No + "," + LocationNo + ",getdate(),getdate()," + Transfer_Status + "," + COST + "," + EMPLOYEE_NO + "," + Customer_No + ",'" + Notes + "','" + StoreName + "',1)";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string strUpdateMenuItems(string ConString, string Sold, string PLU, string Location)
    {
        DataSet resultset = new DataSet();
        string query = "UPDATE MenuItem  SET Sold=sold + '" + Sold + "',Modified='1' WHERE PLU='" + PLU + "' AND LocationNo='" + Location + "'";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string Insert_rcv_fifo(string PLU, string Date_Rcv, string Qty, string Vendor_no, string PO_no, string Cost_Ea, string LocationNo, string Modified, string TransType, string fifo_index, string ConString)
    {
        DataSet resultset = new DataSet();
        string query = "INSERT INTO rcv_fifo (PLU, Date_Rcv, Qty, Vendor_no, PO_no, Cost_Ea, LocationNo, Modified, TransType, fifo_index)  VALUES('" + PLU + "',getdate()," + Qty + ", " + Vendor_no + " ," + PO_no + "," + Cost_Ea + "," + LocationNo + ", 1,'TRANSFER OUT'," + fifo_index + ")";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string GetFIFOIndex(string ConString, string location)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT IsNull(MAX(fifo_index),0)+1 as MAXID FROM rcv_fifo  where locationno = " + location + " and po_no = 0 ";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string Insert_transfer(string Transfer_No, string LocationNo, string Plu, string Item_No, string Description, string Sizes, string Qty, string Price, string Cost, string Total_Cost, string Transfer_Type, string Modified, string ConString)
    {
        DataSet resultset = new DataSet();
        string query = "INSERT INTO [Transfers](Transfer_No,LocationNo,Plu,Item_No,[Description],Sizes,Qty,Price,Cost,Total_Cost,Transfer_Type,Modified) VALUES (" + Transfer_No + "," + LocationNo + ",'" + Plu + "','" + Item_No + "','" + Description + "','" + Sizes + "'," + Qty + "," + Price + "," + Cost + "," + Total_Cost + ",'" + Transfer_Type + "',1)";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string Get_Transfer_List(string ConString, string location)
    {
        DataSet resultset = new DataSet();
        string query = "Select Max( Transfer_No )+1 AS MAXREC From Transfer_List  where locationno = " + location;
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }
    //---------Check Out End ------------------

    //--------Check in - Start--------

    public string ComCashCheckin(string ID, int ShippedStoreID, string PackageSlip, int CreateUserID, string CheInTime, string RFIDs, bool isAdhoc, string UserName, string ConString, string RFIDString)
    {
        string location = "";
        DataTable dtCheckIn = new DataTable();

        try
        {
            location = GetLocation(ConString);
            if (location != "")
            {

                dtCheckIn = GetRFIDDetails(RFIDs, ShippedStoreID.ToString(), RFIDString);

                string colName = "";
                if (ShippedStoreID == 22)
                    colName = "R_Comcash_StoreID";
                else if (ShippedStoreID == 21)
                    colName = "Q_Comcash_StoreID";

                string vendorId = GetVendorID(GetSourceID(PackageSlip, RFIDString), colName, RFIDString);

                if (string.IsNullOrEmpty(vendorId))
                {
                    if (ShippedStoreID == 22)
                        vendorId = "10";
                    else if (ShippedStoreID == 21)
                        vendorId = "17";
                }

                if (dtCheckIn.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCheckIn.Rows.Count; i++)
                    {
                        string PLU = strGetPLU(dtCheckIn.Rows[i]["UPC"].ToString(), ConString);
                        if (PLU == "")
                        {
                            string res = InserNewItem(dtCheckIn.Rows[i]["UPC"].ToString(), location, dtCheckIn.Rows[i]["SKU"].ToString(), "0",
                                dtCheckIn.Rows[i]["ITEMDESCRIPTION"].ToString(), dtCheckIn.Rows[i]["SIZECODE"].ToString(),
                                strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(),
                                ShippedStoreID.ToString(), "Price", RFIDString), vendorId, "0",
                                strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(),
                                ShippedStoreID.ToString(), "Custom1", RFIDString), ConString);
                        }

                        string maxindex = GetMaxfifo_index(location, ConString);
                        UpdateQuantity(location, ConString, dtCheckIn.Rows[i]["UPC"].ToString(), 
                            dtCheckIn.Rows[i]["CNT"].ToString());

                        insertfifo(PackageSlip, dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["CNT"].ToString(),
                            vendorId, "0", strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(),
                            ShippedStoreID.ToString(), "Custom1", RFIDString), location, (i + 1).ToString(), "RECEIVED WOPO",
                            maxindex, ConString, strGetValue(dtCheckIn.Rows[i]["UPC"].ToString(),
                            dtCheckIn.Rows[i]["SKU"].ToString(), ShippedStoreID.ToString(), "price", RFIDString));

                        insertBC_Select(dtCheckIn.Rows[i]["UPC"].ToString(), dtCheckIn.Rows[i]["SKU"].ToString(),
                            dtCheckIn.Rows[i]["ITEMDESCRIPTION"].ToString(), "RFID_INSERT", dtCheckIn.Rows[i]["SIZECODE"].ToString(),
                            dtCheckIn.Rows[i]["CNT"].ToString(), "1", ConString);
                    }
                    UpdateTable(ID, RFIDString, "KT_CheckIN_Log", "Flag", "SNO", "1");
                    UpdateTable(ID, RFIDString, "KT_CheckIN_Log", "Status", "SNO", "Completed");
                    UpdateTable(ID, RFIDString, "KT_CheckIN_Log", "SystemInvoice", "SNO", PackageSlip);
                }
            }
        }
        catch (Exception ex)
        {

        }

        return "";

    }

    public string insertBC_Select(string PLU, string Item_no, string Description, string Ext_Desc, string Sizes,
        string Receive, string Modified, string ConString)
    {
        DataSet resultset = new DataSet();
        string query = "INSERT INTO BC_Select(PLU,Item_no,[Description],Ext_Desc,Sizes,[Receive],Modified) VALUES('" + PLU + "','" + Item_no + "','" + Description + "','" + Ext_Desc + "','" + Sizes + "','" + Receive + "','1')";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string insertfifo(string Invoice_No, string PLU, string Qty, string Vendor_no,
        string PO_no, string Cost_Ea, string LocationNo, string SerialNumberID, string TransType,
        string fifo_index, string ConString, string price)
    {
        DataSet resultset = new DataSet();
        string query = "INSERT INTO rcv_fifo (Invoice_No,PLU, Date_Rcv, Qty, Vendor_no, PO_no, Cost_Ea, LocationNo, Modified,SerialNumberID,TransType,fifo_index,price ) VALUES('" + Invoice_No + "','" + PLU + "',getdate() ," + Qty + "," + Vendor_no + "," + PO_no + "," + Cost_Ea + "," + LocationNo + ", 1, '" + SerialNumberID + "','RECEIVED WOPO'," + fifo_index + "," + price + ")";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string UpdateQuantity(string location, string ConString, string PLU, string Count)
    {
        DataSet resultset = new DataSet();
        string query = "UPDATE MenuItem SET On_Order=(select isnull(sum(isnull(on_order,0)),0)  from onorder WHERE PLU='"
            + PLU + "' AND LocationNo=" + location + ") ,Recvd=Recvd+" + Count + " , Modified=1 , LastModified = GETDATE() WHERE PLU='" + PLU + "' AND LocationNo=" + location + " ";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string GetMaxfifo_index(string location, string ConString)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT IsNull(MAX(fifo_index),0)+1 as MAXID FROM rcv_fifo where locationno =" + location + " and po_no = 0 ";
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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string InserNewItem(string PLU, string Location, string sku, string dept, string Desc,
        string size, string price, string vendor, string start, string cost, string ConString)
    {
        DataSet resultset = new DataSet();
        string query = "INSERT INTO [comcash].[dbo].[menuitem] ([PLU] , [LocationNo] , [Item_no] , [Dept] , " +
            " [Description] , [Ext_Desc], [Sizes] , [Price] , [Price_Fixed] , [Price_Fixed2] , [Price_Fixed3] , [Price_Fixed4] , " +
         " [Price_Fixed5] , [Price_Fixed6] , [Price_Fixed7] , [Price_Fixed8] , [Price_Fixed9] , [Price_Fixed10] ," +
         "  [Primary_Vendor] , [Start] , [Sold] , [On_Order] , [CommitQty] , [Recvd] , [Cost] , [UseTax1] , [UseTax2] , " +
         " [UseTax3] , [UseTax4] , [UseTax5] , [UseRecurringPromo] , [ZeroPriceOK] , [TrackInv] , [ItemType] , [BarCode] , " +
         " [Foodstamp] , [WIC] , [PriceBasedonSize] , [Weight] , [Weighted] , [ExcludeReceipt] , [TareEnabled] , [TareType] ," +
         "  [Rental_Increments] , [Billing_Increments] , [Rentable] , [WebAccessible] , [WebCartFrontPage] , [FireDelay] , " +
         " [Modified] , [LastModified] , [Active] , [excluderecipt] , [CaseCost] , [CaseQty] , [CaseOrdered] , [AgeCheck] ," +
         "  [NonSales] , [Tax1BreakDown] , [Tax6BreakDown] , [Tax7BreakDown] , [UseTax6] , [UseTax7] , [WATotalPrice]) " +
         " values ('" + PLU + "' , '" + Location + "' , '" + sku + "' , '" + dept + "' , '" + Desc + "' , 'RFID_INSERT' , '" + size + "' , '"
         + price + "' , '1' , '1' , '1' , '1' , '1' , '1' , '1' , '1' , '1' , '1' , '" + vendor + "' , '" + start +
         "' , '0' , '0' , '0' , '0' , '" + cost + "' , '1' , '1' , '0' , '0' , '0' , '0' , '0' , '0' , 'Standard' , " +
         " '0' , '1' , '1' , '0' , '0' , '0' , '0' , '0' , '0' , '24' , '1' , '0' , '1' , '0' , '0' , '1' , GetDate() , " +
         " '1' , '0' , '0' , '0' , '0' , '0' , '0' , '0' , '0' , '0' , '0' , '0' , '0')";

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
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public string strGetValue(string UPC, string SKU, string storeid, string Param, string TrackerRetailString)
    {
        #region [ OLD CODE ]
        //DataSet resultset = new DataSet();
        //try
        //{

        //    using (SqlDataAdapter dataAdapter = new SqlDataAdapter("select isnull([" + Param + "],0) from Products where UPC='" + UPC + "' and sku='" + SKU + "' and StoreID='" + storeid + "'", TrackerRetailString))
        //    {
        //        dataAdapter.Fill(resultset, "VendorName");
        //    }

        //    if (resultset != null)
        //    {
        //        if (resultset.Tables.Count > 0)
        //        {
        //            try
        //            {
        //                decimal cost = Convert.ToDecimal(resultset.Tables[0].Rows[0][0].ToString());
        //            }
        //            catch (Exception ex)
        //            {
        //                return "0";
        //            }
        //            return resultset.Tables[0].Rows[0][0].ToString();
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{

        //}


        //return "0";
        #endregion [ OLD CODE ]
        DataSet resultset = new DataSet();
        try
        {


            if (Param.ToUpper() == "PRICE")
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter("select isnull([" + Param + "],0) from Products where UPC='" + UPC + "' and sku='" + SKU + "' and StoreID='" + storeid + "'", TrackerRetailString))
                {
                    dataAdapter.Fill(resultset, "VendorName");
                }

                if (resultset != null)
                {
                    if (resultset.Tables.Count > 0)
                    {
                        try
                        {
                            decimal cost = Convert.ToDecimal(resultset.Tables[0].Rows[0][0].ToString());
                        }
                        catch (Exception ex)
                        {
                            return "0";
                        }
                        return resultset.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            else
            {
                if (Param.ToUpper() == "CUSTOM1")
                {
                    Param = "COST";
                }
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter("select isnull([" + Param + "],0) from sku_cost where UPC='" + UPC + "' and sku='" + SKU + "' order by effective_date desc ", _SysproSystem))
                {
                    dataAdapter.Fill(resultset, "VendorName");
                }

                if (resultset != null)
                {
                    if (resultset.Tables.Count > 0)
                    {
                        try
                        {
                            decimal cost = Convert.ToDecimal(resultset.Tables[0].Rows[0][0].ToString());
                        }
                        catch (Exception ex)
                        {
                            return "0";
                        }
                        return resultset.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "0";
    }

    public string strGetPLU(string PLU, string ConString)
    {
        DataSet resultset = new DataSet();
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("select plu  from menuitem where plu like '%" + PLU + "'", ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }

    public DataTable GetRFIDDetails(string RFID, string StoreID, string RFIDString)
    {

        DataTable dtRRDetails = new DataTable();

        SqlCommand scmCmdToExecute = new SqlCommand();
        scmCmdToExecute.CommandText = "dbo.[pr_SelectProducts_OnPID]";
        scmCmdToExecute.CommandType = CommandType.StoredProcedure;
        DataTable dtToReturn = new DataTable("RFIDProducts");
        SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);
        SqlConnection sql = new SqlConnection(RFIDString);
        scmCmdToExecute.Connection = sql; ;

        try
        {
            scmCmdToExecute.Parameters.Add(new SqlParameter("@STOREID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
            scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDS", SqlDbType.VarChar, -1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFID));
            scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

            // Open connection.

            // Open connection.
            sql.Open();


            // Execute query.
            sdaAdapter.Fill(dtRRDetails);
            int _errorCode = 0;
            if (!string.IsNullOrEmpty(Convert.ToString(scmCmdToExecute.Parameters["@Error"].Value)))
                _errorCode = Convert.ToInt32(scmCmdToExecute.Parameters["@Error"].Value);

            if (_errorCode != 0)
            {
                // Throw error.
                //throw new Exception("Stored Procedure 'pr_SelectProductsForCheckOut_BeforeScan' reported the ErrorCode: " + _errorCode);
            }

        }
        catch (Exception ex)
        {
            // some error occured. Bubble it to caller and encapsulate Exception object
            // throw new Exception("Products::GetCategoryReplenishment_BeforeScan::Error occured.", ex);
        }
        finally
        {
            // Close connection.
            sql.Close();
            scmCmdToExecute.Dispose();
            sdaAdapter.Dispose();
        }


        return dtRRDetails;
    }

    public string GetLocation(string ConString)
    {
        DataSet resultset = new DataSet();
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT LocationNo FROM Configuration order by LocationNo asc", ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    return resultset.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }


        return "";
    }




    //--------Check in - End--------



}
