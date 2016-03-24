using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using NLog;
namespace SJDealStore
{

    public class SalesResult
    {
        public string ErrorCode { get; set; }
        public string Decommissioned { get; set; }
        public string Rejected { get; set; }
    }
    public class DALayer
    {
        private Constants _constants = new Constants();
        internal Logger Nlog = LogManager.GetCurrentClassLogger();

        public SalesResult MakeSales(string RFIDs)
        {
         SalesResult result= new SalesResult();
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.pr_UpdateDecommissionedItems)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.Add("@StoreID", _constants.StoreID);
            selectCommand.Parameters.Add("@DeviceID",  _constants.DeviceID);
            selectCommand.Parameters.Add("@IsDamaged", _constants.SalesIsDamaged);
            selectCommand.Parameters.Add("@RFIDs", RFIDs);
            int ErrorCode=0;
            selectCommand.Parameters.Add("@ERRORCODE", ErrorCode);
            selectCommand.Parameters["@ERRORCODE"].Direction = ParameterDirection.Output;

            int Decommissioned = 0;
            selectCommand.Parameters.Add("@Decommissioned", Decommissioned);
            selectCommand.Parameters["@Decommissioned"].Direction = ParameterDirection.Output;

            int Rejected = 0;
            selectCommand.Parameters.Add("@Rejected", Rejected);
            selectCommand.Parameters["@Rejected"].Direction = ParameterDirection.Output;



            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                selectCommand.ExecuteNonQuery();
                result.Rejected = selectCommand.Parameters["@Rejected"].Value.ToString();
                result.ErrorCode = selectCommand.Parameters["@ErrorCode"].Value.ToString();
                result.Decommissioned = selectCommand.Parameters["@Decommissioned"].Value.ToString();
                return result;

            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
        public DataTable SelectProducts(string RFIDs)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.pr_SelectProducts_OnRFID)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@RFIDS", RFIDs);
            selectCommand.Parameters.Add("@STOREID", _constants.StoreID);
            int ErrorCode = 0;
            selectCommand.Parameters.Add("@ERROR", ErrorCode);
            selectCommand.Parameters["@ERROR"].Direction = ParameterDirection.Output;

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        } 
        public DataTable GetProductDetails(string UPC)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.pr_GetProductDetailsForUPC)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@UPC", UPC);
            selectCommand.Parameters.Add("@STOREID", _constants.StoreID);
            int ErrorCode = 0;
            selectCommand.Parameters.Add("@ERRORCODE", ErrorCode);
            selectCommand.Parameters["@ERRORCODE"].Direction = ParameterDirection.Output;

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }   
        public string ReturnProduct(string UPC,string SKU, string RFID)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.pr_InsertProducts_OnSingleAssociation)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@UPC", UPC);
            selectCommand.Parameters.Add("@SKU", SKU);
            selectCommand.Parameters.Add("@STOREID", _constants.StoreID);
            selectCommand.Parameters.Add("@RFIDTagID", RFID);
            selectCommand.Parameters.Add("@DeviceID", _constants.DeviceID);
            selectCommand.Parameters.Add("@IsReturned", "1");
           
            int ErrorCode = 0;
            selectCommand.Parameters.Add("@ERRORCODE", ErrorCode);
            selectCommand.Parameters["@ERRORCODE"].Direction = ParameterDirection.Output;

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                selectCommand.ExecuteNonQuery();
                return selectCommand.Parameters["@ErrorCode"].Value.ToString();
               
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }



        public string InsertMasterFile(string FileName, string CategoryName)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertMasterFile),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.AddWithValue("@FileName ", FileName);
            selectCommand.Parameters.AddWithValue("@CategoryName ", CategoryName);
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                var ID = selectCommand.ExecuteScalar();
                return ID.ToString();

            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        private string InsertMasterFileDetails(int MasterID, string CL1,
        string CL2,string CL3,string CL4,string CL5,string CL6,string CL7,string CL8,string CL9,string CL10,string CL11,string CL12,string CL13,
        string CL14,string CL15,string CL16,string CL17,string CL18,string CL19,string CL20,string CL21,string CL22,string CL23,string CL24,string CL25,
        string CL26,string CL27,string CL28,string CL29,string CL30,string CategoryName)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertMasterFileDetails),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@MasterID ", MasterID);
            selectCommand.Parameters.AddWithValue("@CL1", CL1);
            selectCommand.Parameters.AddWithValue("@CL2", CL2);
            selectCommand.Parameters.AddWithValue("@CL3", CL3);
            selectCommand.Parameters.AddWithValue("@CL4", CL4);
            selectCommand.Parameters.AddWithValue("@CL5", CL5);
            selectCommand.Parameters.AddWithValue("@CL6", CL6);
            selectCommand.Parameters.AddWithValue("@CL7", CL7);
            selectCommand.Parameters.AddWithValue("@CL8", CL8);
            selectCommand.Parameters.AddWithValue("@CL9", CL9);
            selectCommand.Parameters.AddWithValue("@CL10", CL10);
            selectCommand.Parameters.AddWithValue("@CL11", CL11);
            selectCommand.Parameters.AddWithValue("@CL12", CL12);
            selectCommand.Parameters.AddWithValue("@CL13", CL13);
            selectCommand.Parameters.AddWithValue("@CL14", CL14);
            selectCommand.Parameters.AddWithValue("@CL15", CL15);
            selectCommand.Parameters.AddWithValue("@CL16", CL16);
            selectCommand.Parameters.AddWithValue("@CL17", CL17);
            selectCommand.Parameters.AddWithValue("@CL18", CL18);
            selectCommand.Parameters.AddWithValue("@CL19", CL19);
            selectCommand.Parameters.AddWithValue("@CL20", CL20);
            selectCommand.Parameters.AddWithValue("@CL21", CL21);
            selectCommand.Parameters.AddWithValue("@CL22", CL22);
            selectCommand.Parameters.AddWithValue("@CL23", CL23);
            selectCommand.Parameters.AddWithValue("@CL24", CL24);
            selectCommand.Parameters.AddWithValue("@CL25", CL25);
            selectCommand.Parameters.AddWithValue("@CL26", CL26);
            selectCommand.Parameters.AddWithValue("@CL27", CL27);
            selectCommand.Parameters.AddWithValue("@CL28", CL28);
            selectCommand.Parameters.AddWithValue("@CL29", CL29);
            selectCommand.Parameters.AddWithValue("@CL30", CL30);
            selectCommand.Parameters.AddWithValue("@CategoryName", CategoryName);
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                var ID = selectCommand.ExecuteScalar();
                return ID.ToString();

            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public string InsertMasterFileDetails(DataTable dt, int MasterID,string CategoryName)
        {
            try
            {
                if (dt != null)
                {
                    foreach (DataRow drDataRow in dt.Rows)
                    {
                        
                        string CL1 = "";
                        string CL2 = "";
                        string CL3 = "";
                        string CL4 = "";
                        string CL5 = "";
                        string CL6 = "";
                        string CL7 = "";
                        string CL8 = "";
                        string CL9 = "";
                        string CL10 = "";
                        string CL11 = "";
                        string CL12 = "";
                        string CL13 = "";
                        string CL14 = "";
                        string CL15 = "";
                        string CL16 = "";
                        string CL17 = "";
                        string CL18 = "";
                        string CL19 = "";
                        string CL20 = "";
                        string CL21 = "";
                        string CL22 = "";
                        string CL23 = "";
                        string CL24 = "";
                        string CL25 = "";
                        string CL26 = "";
                        string CL27 = "";
                        string CL28 = "";
                        string CL29 = "";
                        string CL30 = "";
                        int flag = 1;
                        foreach (object item in drDataRow.ItemArray)
                        {
                            switch (flag)
                            {
                                case 1:
                                    CL1 = item.ToString();
                                    break;
                                case 2:
                                    CL2 = item.ToString();
                                    break;
                                case 3:
                                    CL3 = item.ToString();
                                    break;
                                case 4:
                                    CL4 = item.ToString();
                                    break;
                                case 5:
                                    CL5 = item.ToString();
                                    break;
                                case 6:
                                    CL6 = item.ToString();
                                    break;
                                case 7:
                                    CL7 = item.ToString();
                                    break;
                                case 8:
                                    CL8 = item.ToString();
                                    break;
                                case 9:
                                    CL9 = item.ToString();
                                    break;
                                case 10:
                                    CL10 = item.ToString();
                                    break;
                                case 11:
                                    CL11 = item.ToString();
                                    break;
                                case 12:
                                    CL12 = item.ToString();
                                    break;
                                case 13:
                                    CL13 = item.ToString();
                                    break;
                                case 14:
                                    CL14 = item.ToString();
                                    break;
                                case 15:
                                    CL15 = item.ToString();
                                    break;
                                case 16:
                                    CL16 = item.ToString();
                                    break;
                                case 17:
                                    CL17 = item.ToString();
                                    break;
                                case 18:
                                    CL18 = item.ToString();
                                    break;
                                case 19:
                                    CL19 = item.ToString();
                                    break;
                                case 20:
                                    CL20 = item.ToString();
                                    break;
                                case 21:
                                    CL21 = item.ToString();
                                    break;
                                case 22:
                                    CL22 = item.ToString();
                                    break;
                                case 23:
                                    CL23 = item.ToString();
                                    break;
                                case 24:
                                    CL24 = item.ToString();
                                    break;
                                case 25:
                                    CL25 = item.ToString();
                                    break;
                                case 26:
                                    CL26 = item.ToString();
                                    break;
                                case 27:
                                    CL27 = item.ToString();
                                    break;
                                case 28:
                                    CL28 = item.ToString();
                                    break;
                                case 29:
                                    CL29 = item.ToString();
                                    break;
                                case 30:
                                    CL30 = item.ToString();
                                    break;

                            }
                            flag++;
                         
                        }


                        var result = InsertMasterFileDetails(MasterID, CL1, CL2, CL3, CL4, CL5, CL6, CL7, CL8, CL9, CL10, CL11, CL12,
                        CL13, CL14, CL15, CL16, CL17, CL18, CL19, CL20, CL21, CL22, CL23, CL24, CL25,
                        CL26, CL27, CL28, CL29, CL30,CategoryName);

                    
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }


        public DataTable SelectImportMaster()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SelectImportMaster)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable SearchString(string SearchString, int MasterID)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SearchString)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@string", SearchString);
            selectCommand.Parameters.AddWithValue("@MasterID", MasterID);
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable IncrementReceived(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.IncrementReceived,sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable IncrementDamaged(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.IncrementDamaged, sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable DecrementReceived(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.DecrementReceived, sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable DecrementDamaged(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.DecrementDamaged, sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable SjDealsSaveSettings(int SJDealMasterSno, bool TestTags, string PrefixPrice, string PrefixOriginalPrice,
            string Barcode, string OriginalSKUNumber, string DESC, string Price, string OrgPrice, string LotNumber)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SjDealsSaveSettings)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@SJDealMasterSno", SJDealMasterSno);
            selectCommand.Parameters.Add("@TestTags", TestTags);
            selectCommand.Parameters.Add("@PrefixPrice", PrefixPrice);
            selectCommand.Parameters.Add("@PrefixOriginalPrice", PrefixOriginalPrice);
            selectCommand.Parameters.Add("@Barcode", Barcode);
            selectCommand.Parameters.Add("@OriginalSKUNumber", OriginalSKUNumber);
            selectCommand.Parameters.Add("@DESC", DESC);
            selectCommand.Parameters.Add("@Price", Price);
            selectCommand.Parameters.Add("@OrgPrice", OrgPrice);
            selectCommand.Parameters.Add("@LotNumber", LotNumber);

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
        public DataTable SJDeals_SquareLogger(string categoryName, string skuNumber, string name, string description,
            string price, string QOH, string ItemID, string Result)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SJDeals_SquareLogger)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@categoryName", categoryName);
            selectCommand.Parameters.Add("@skuNumber", skuNumber);
            selectCommand.Parameters.Add("@name", name);
            selectCommand.Parameters.Add("@description", description);
            selectCommand.Parameters.Add("@price", price);
            selectCommand.Parameters.Add("@QOH", QOH);
            selectCommand.Parameters.Add("@ItemID", ItemID);
            selectCommand.Parameters.Add("@Result", Result);

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
        public DataTable GetSettings(string SJDealMasterSno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetSettings, SJDealMasterSno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable TagLogger(string UPCNumber, string Descr, string MSRP, string Price,
            string MasterID, string LotNumber,string MSRPrice, string sjPrice, string stickerType)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.TagLogger)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@UPCNumber", UPCNumber);
            selectCommand.Parameters.Add("@Descr", Descr);
            selectCommand.Parameters.Add("@MSRP", MSRP);
            selectCommand.Parameters.Add("@Price", Price);
            selectCommand.Parameters.Add("@MasterID", MasterID);
            selectCommand.Parameters.Add("@LotNumber", LotNumber);
            selectCommand.Parameters.Add("@MSRPPrice", MSRPrice);
            selectCommand.Parameters.Add("@SJPrice", sjPrice);
            selectCommand.Parameters.Add("@StickerType", stickerType);

          
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable SJDeals_GetReceivedReport(string masterID)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SJDeals_GetReceivedReport)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@MasterID", masterID);

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable SJDeals_GetDamagedReport(string masterID)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SJDeals_GetDamagedReport)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@MasterID", masterID);

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable SJDeals_CreateManifest(string Name, string Description)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SJDeals_CreateManifest)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@Name", Name);
            selectCommand.Parameters.Add("@Description", Description);

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable GetAllManifest()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllManifest)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
        public DataTable GetManifestMaster(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetManifestMaster,sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable GetManifestDetail(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetManifestDetails, sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }



        public DataTable InsertItemInManifest(string Barcode, string ManifestID)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertItemInManifest)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.Add("@Barcode", Barcode);
            selectCommand.Parameters.Add("@ManifestID", ManifestID);

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable IncManifestQty(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.IncManifestQty, sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable DecManifestQty(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.DecManifestQty, sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
        public DataTable DeleteManifestItem(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.DeleteManifestItem, sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable AddNewItems(string skuNumber, string descr, string msrp, string sjPrice,string masterId)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.AddNewItems)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@SKUNumber", skuNumber);
            selectCommand.Parameters.Add("@Descr", descr);
            selectCommand.Parameters.Add("@MSRP", msrp);
            selectCommand.Parameters.Add("@SJPrice", sjPrice);
            selectCommand.Parameters.Add("@MasterID", masterId);
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                return null;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable AddItemsWithReceiving(string skuNumber, string descr, string msrp, string sjPrice, string masterId, int NoOfItems)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.AddItemsWithReceiving)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@SKUNumber", skuNumber);
            selectCommand.Parameters.Add("@Descr", descr);
            selectCommand.Parameters.Add("@MSRP", msrp);
            selectCommand.Parameters.Add("@SJPrice", sjPrice);
            selectCommand.Parameters.Add("@MasterID", masterId);
            selectCommand.Parameters.Add("@TotalUnits", NoOfItems);
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                return null;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
        public DataTable GetItemsToBeImportedToSquare()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetItemsToBeImportedToSquare)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                return null;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

    }
}