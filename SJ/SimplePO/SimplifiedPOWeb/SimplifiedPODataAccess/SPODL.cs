using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SimplifiedPOConstants;
namespace SimplifiedPODataAccess
{
    public class SPODL
    {
        internal Logger Nlog = LogManager.GetCurrentClassLogger();
        private SimplifiedPOConstants.SPOConst _constants = new SPOConst();
        private string _connectionString;


        public SPODL()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _connectionString = _constants.DefaultConnectionString;
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }


        public DataTable Test(DateTime startdate, DateTime enddate, int strNbr)
        {
            throw new NotImplementedException();
        }



        //public DataTable SalesTransationsGroupedByItem(DateTime startdate, DateTime enddate, int strNbr)
        //{
        //    Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
        //    var dataTable = new DataTable();
        //    var selectCommand = new SqlCommand
        //    {
        //        CommandText = string.Format(_constants.SalesTransationsGroupedByItem)
        //        ,
        //        CommandTimeout = 180,
        //        CommandType = CommandType.StoredProcedure
        //    };
        //    selectCommand.Parameters.AddWithValue("@startdate", startdate);
        //    selectCommand.Parameters.AddWithValue("@enddate", enddate);
        //    selectCommand.Parameters.AddWithValue("@str_nbr", strNbr);

        //    var adapter = new SqlDataAdapter(selectCommand);
        //    var connection = new SqlConnection(_constants.DefaultConnectionString);
        //    selectCommand.Connection = connection;
        //    try
        //    {
        //        connection.Open();
        //        adapter.Fill(dataTable);
        //        return dataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        Nlog.Trace(
        //            this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
        //            System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //        connection.Dispose();
        //        Nlog.Trace(message:
        //            this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
        //            System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
        //    }
        //}


        public string Test(string testString)
        {
            return DateTime.Now.ToString(CultureInfo.InvariantCulture) + " " + testString;
        }

        public DataTable Login(string Username, string PasswordHash)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.LoginCheck,Username,PasswordHash)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
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


        public DataTable GetAllUsers()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllUser)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
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

        public DataTable GetAllEntities()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllEntities)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
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

        public DataTable GetSuppliers(string EntitesName)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetSuppliers, EntitesName)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
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


        public DataTable GetSupplierAddress(string EntitesName, string SupplierName)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetSupplierAddress, EntitesName,SupplierName)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
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

        public string CreateTempPO(int loginUserId, string loginUserName, string postFor, string buyerName, string buyerAddress, string buyerContactNumber, 
            int priority, string supplierEntity, string supplierId, string supplierName, string supplierAddress, string supplierContactNumber, string  notes)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertTempPo),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.AddWithValue("@loginUserId", loginUserId);
            selectCommand.Parameters.AddWithValue("@LoginUserName", loginUserName);
            selectCommand.Parameters.AddWithValue("@POPostForID", postFor);
            selectCommand.Parameters.AddWithValue("@BuyerName", buyerName);
            selectCommand.Parameters.AddWithValue("@BuyerAddress", buyerAddress);
            selectCommand.Parameters.AddWithValue("@BuyerContactNumber", buyerContactNumber);
            selectCommand.Parameters.AddWithValue("@Priority", priority);
            selectCommand.Parameters.AddWithValue("@SupplierEntity", supplierEntity);
            selectCommand.Parameters.AddWithValue("@SupplierID", supplierId);
            selectCommand.Parameters.AddWithValue("@SupplierName", supplierName);
            selectCommand.Parameters.AddWithValue("@SupplierAddress", supplierAddress);
            selectCommand.Parameters.AddWithValue("@SupplierContactNumber", supplierContactNumber);
            selectCommand.Parameters.AddWithValue("@Notes", notes);


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
               var ID=    selectCommand.ExecuteScalar();
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
            return "";
        }


        public string UpdateTempPO(int loginUserId, string loginUserName, string postFor, string buyerName, string buyerAddress, string buyerContactNumber, 
            int priority, string supplierEntity, string supplierId, string supplierName, string supplierAddress, string supplierContactNumber, string  notes,string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.UpdateTempPO),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.AddWithValue("@loginUserId", loginUserId);
            selectCommand.Parameters.AddWithValue("@LoginUserName", loginUserName);
            selectCommand.Parameters.AddWithValue("@POPostForID", postFor);
            selectCommand.Parameters.AddWithValue("@BuyerName", buyerName);
            selectCommand.Parameters.AddWithValue("@BuyerAddress", buyerAddress);
            selectCommand.Parameters.AddWithValue("@BuyerContactNumber", buyerContactNumber);
            selectCommand.Parameters.AddWithValue("@Priority", priority);
            selectCommand.Parameters.AddWithValue("@SupplierEntity", supplierEntity);
            selectCommand.Parameters.AddWithValue("@SupplierID", supplierId);
            selectCommand.Parameters.AddWithValue("@SupplierName", supplierName);
            selectCommand.Parameters.AddWithValue("@SupplierAddress", supplierAddress);
            selectCommand.Parameters.AddWithValue("@SupplierContactNumber", supplierContactNumber);
            selectCommand.Parameters.AddWithValue("@Notes", notes);
            selectCommand.Parameters.AddWithValue("@Sno", sno);


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                var ID=    selectCommand.ExecuteScalar();
                return "Success";
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
            return "";
        }

        public DataTable GetUnSubmittedPo(string userId)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetUnSubmittedPo, userId)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
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


        public DataTable DeleteTempPo(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.DeleteTempPo, sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
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


        public DataTable GetTempPoDetails(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetTempPoDetails, sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
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

        public DataTable GetCouponSalesDetail(string query)
        {
            DataTable allData = new DataTable();
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);

                connection.Close();
            }
            catch
            {
                connection.Close();
            }
            return allData;
        }




        public DataTable SearchProducts(string searchString, string companyAlphabet)
        {
            DataTable table = new DataTable();
            try
            {

                table.Columns.Add("Sno#", typeof(int));
                table.Columns.Add("Stockcode", typeof(string));
                table.Columns.Add("Description", typeof(string));
                table.Columns.Add("Long Description", typeof(string));
                table.Columns.Add("UPC", typeof(string));
                table.Columns.Add("Unit Cost", typeof(string));
                table.Columns.Add("Selling Price", typeof(string));
                table.Columns.Add("Supplier", typeof(string));
                string query = string.Format("SELECT a.StockCode, a.Description, a.LongDesc, a.AlternateKey1, a.AlternateKey2, a.StockUom, a.AlternateUom, a.OtherUom, a.Mass, a.Volume, a.Supplier, a.ProductClass, a.KitType, a.Buyer, a.Planner, a.LeadTime, a.PartCategory, a.WarehouseToUse, a.BuyingRule, a.Decimals, a.Ebq, a.PanSize, a.UserField1, a.UserField2, a.UserField3, a.UserField4, a.UserField5, a.DrawOfficeNum, b.QtyOnHand, b.QtyAllocated, b.QtyOnOrder, b.QtyOnBackOrder, b.QtyInTransit, b.QtyAllocatedWip, b.QtyInInspection, b.MinimumQty, b.MaximumQty, b.UnitCost, b.DefaultBin, b.UserField1, b.UserField2, b.UserField3, c.SellingPrice, c.PriceBasis, a.EccFlag, a.Version, a.Release, a.EccUser, a.ClearingFlag FROM syspro.SysproCompany{0}.dbo.InvMaster a WITH (NOLOCK) LEFT JOIN syspro.SysproCompany{0}.dbo.InvWarehouse b WITH (NOLOCK) ON (a.StockCode = b.StockCode AND a.WarehouseToUse = b.Warehouse) LEFT JOIN syspro.SysproCompany{0}.dbo.InvPrice c WITH (NOLOCK) ON (a.StockCode = c.StockCode AND a.ListPriceCode = c.PriceCode) WHERE  ( a.StockCode LIKE samplequery12345 ) OR ( a.Description LIKE samplequery12345 ) OR ( a.LongDesc LIKE samplequery12345 ) OR ( a.AlternateKey1 LIKE samplequery12345 ) OR ( a.AlternateKey2 LIKE samplequery12345 ) OR ( a.Supplier LIKE samplequery12345 ) ",companyAlphabet);
                string str = searchString;
                str = str.Replace("\r\n", ",");
                string[] words = str.Split(',');
                for (int i = 0; i < words.Count(); i++)
                {
                    try
                    {
                        string newquery = query.Replace("samplequery12345", "'%[" + words[i].ToString().Substring(0, 1) + "]" + words[i].ToString().Substring(1, words[i].ToString().Length - 1) + "%'");
                        DataTable dt = GetCouponSalesDetail(newquery);
                        return dt;
                        //if (dt.Rows.Count > 0)
                        //{
                        //    for (int j = 0; j < dt.Rows.Count; j++)
                        //    {
                        //        table.Rows.Add(i, dt.Rows[j]["StockCode"], dt.Rows[j]["Description"], dt.Rows[j]["LongDesc"], dt.Rows[j]["AlternateKey1"], dt.Rows[j]["UnitCost"], dt.Rows[j]["SellingPrice"], dt.Rows[j]["Supplier"]);
                        //    }
                        //}
                        //else
                        //{
                        //    table.Rows.Add(i, "", "", "", words[i], "", "", "");
                        //}
                    }

                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        return null;
        }

        public string AddItemsTempPo(string StockCode, string POMasterNo, string TotalQuantity)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.AddItemsTempPo),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.AddWithValue("@StockCode", StockCode);
            selectCommand.Parameters.AddWithValue("@POMasterNo", POMasterNo);
            selectCommand.Parameters.AddWithValue("@TotalQuantity", TotalQuantity);

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                var ID = selectCommand.ExecuteReader();
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
            return "";
        }

        public DataTable UpdateAttributesPo(string sno,float PoCost, float SubTotal, float Shipping, float Discount, float Total, bool CheckRequired, bool RFIDTagsPrint, 
            string OrderType, string PReason )
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.UpdateAttributesPo),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.AddWithValue("@Sno", sno);
            selectCommand.Parameters.AddWithValue("@POCost", PoCost);
            selectCommand.Parameters.AddWithValue("@SubTotal", SubTotal);
            selectCommand.Parameters.AddWithValue("@Shipping", Shipping);
            selectCommand.Parameters.AddWithValue("@Discount", Discount);
            selectCommand.Parameters.AddWithValue("@Total", Total);
            selectCommand.Parameters.AddWithValue("@CheckRequired", CheckRequired);
            selectCommand.Parameters.AddWithValue("@RFIDTags", RFIDTagsPrint);
            selectCommand.Parameters.AddWithValue("@OrderType", OrderType);
            selectCommand.Parameters.AddWithValue("@PReason", PReason);


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
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
            return null;
        }
        public DataTable SubmitPOForApproval(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SubmitPOForApproval),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.AddWithValue("@Sno", sno);


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
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
            return null;
        }


        public DataTable InsertItemsIntoPO(string sno,string PONumber)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertItemsIntoPO),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.AddWithValue("@Sno", sno);
            selectCommand.Parameters.AddWithValue("@PONumber", PONumber);


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
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
            return null;
        }
        public DataTable GetPOMasterDetail(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetPOMasterDetail, sno),
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
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
            return null;
        }

        public DataTable GetPOItemsDetail(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetPOItemsDetail, sno),
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
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
            return null;
        }

        public string GetOnlinePO(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetOnlinePO, sno),
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                return "Error";
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
            return null;
        }


        public DataTable GetAccessCodeForPO(string PONumber)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAccessCodeForPO, PONumber),
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
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
            return null;
        }

        public DataTable GetEmailID(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetEmailID),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@sno", sno);
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
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
            return null;
        }


        public DataTable PostPoSubmissionActivity(string sno,string poNumber)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.PostPoSubmissionActivity),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@POID", sno);
            selectCommand.Parameters.AddWithValue("@PONUMBER", poNumber);
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
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
            return null;
        }
    } 
    
    
}
