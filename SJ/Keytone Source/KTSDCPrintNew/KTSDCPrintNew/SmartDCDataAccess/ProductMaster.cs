using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;

namespace KTone.DAL.SmartDCDataAccess
{
    public class ProductMaster:DBInteractionBase 
    {
        #region Class Member Declarations
        private Int32 _dataOwnerID, _companyID, _crupUser, _categoryID, _packageID;
        private long _prodID, _productID, _skuID;
        private string _prodName, _prodPref,_message;
        private string _productSKU, _skuDescription;
        #endregion

        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ProductMaster_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ProductMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _companyID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
               

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
              
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ProductMaster_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ProductMaster::SelectAll::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }
        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Productmaster_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ProductMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.BigInt,10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _prodID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Productmaster_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Productmaster::SelectOne::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public bool Insert(string columnsProd, string colunmvaluesProd, string columnsSku, string colunmvaluesSku,int categoryIdSku)
        {
            _log.Trace("Entering Insert -  Table:ProductMaster ; ProductName:{0},CompanyId(1)" +
            "ProductPrefix :{2},DataOwnerId:{3},@ProductSKU(4),@SKUDescription(5),@ProdcutID(6)," +
            "@ColumnsProd(7),@ColumnValuesProd(8),@ColumnsSku(9),@ColumnValuesSku(10)" + _prodName, _companyID,
            _prodPref, _dataOwnerID, _productSKU, _skuDescription, _productID, columnsProd, colunmvaluesProd,
            columnsSku, colunmvaluesSku, _packageID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ProductSkuMaster_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 150, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _prodName));
                cmdToExecute.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _companyID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductPrefix", SqlDbType.VarChar, 5, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _prodPref));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _crupUser));
                //cmdToExecute.Parameters.Add(new SqlParameter("@ProdId", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _prodID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ColumnsProd", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, columnsProd));
                cmdToExecute.Parameters.Add(new SqlParameter("@ColumnValuesProd", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, colunmvaluesProd));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryIdProd", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _categoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductSKU", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _productSKU));
                cmdToExecute.Parameters.Add(new SqlParameter("@SKUDescription", SqlDbType.VarChar, 300, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _skuDescription));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProdcutID", SqlDbType.BigInt, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _productID));
                cmdToExecute.Parameters.Add(new SqlParameter("@SKUId", SqlDbType.BigInt, 8 , ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _skuID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ColumnsSku", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, columnsSku));
                cmdToExecute.Parameters.Add(new SqlParameter("@ColumnValuesSku", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, colunmvaluesSku));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryIdSku", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, categoryIdSku));
                cmdToExecute.Parameters.Add(new SqlParameter("@PackageID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _packageID));
	

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
               // _prodID = (Int32)cmdToExecute.Parameters["@ProdId"].Value;
                _skuID = (Int64)cmdToExecute.Parameters["@SKUId"].Value;

                if (_errorCode == -558)
                {
                    //throw new Exception("Product Name already exists.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ProductSkuMaster_Insert' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ProductMaster::Insert::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting Insert");
            }
        }

        public bool InsertOne()
        {
            _log.Trace("Entering InsertOne -  Table:ProductMaster ; ProductName:{0},CompanyId(1)" +
            "DataOwnerId:{3},@ProductSKU(4),@SKUDescription(5),@ProdcutID(6)," +
            "@ColumnsProd(7),@ColumnValuesProd(8),@ColumnsSku(9),@ColumnValuesSku(10)" + _prodName, _companyID,
             _dataOwnerID);

            SqlCommand cmdToExecute = new SqlCommand();


            cmdToExecute.CommandText = "dbo.[pr_ProductMaster_Insert_Product]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 150, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _prodName));
                cmdToExecute.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _companyID));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _crupUser));
                cmdToExecute.Parameters.Add(new SqlParameter("@Message", SqlDbType.VarChar, 1000, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _message));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                _message = cmdToExecute.Parameters["@Message"].Value.ToString();
                // _prodID = (Int32)cmdToExecute.Parameters["@ProdId"].Value;
               // _skuID = (Int32)cmdToExecute.Parameters["@SKUId"].Value;

                if (_errorCode == -558)
                {
                    //throw new Exception("Product Name already exists.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ProductMaster_Insert_One' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert One : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ProductMaster::Insert One::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting Insert One");
            }
        }

        public  bool Update(string columns, string colunmvalues)
        {
            _log.Trace("Entering Update -  Table:ProductMaster ;" +
             "ProductName :{1},DataOwnerId:{2}" + _prodName , _dataOwnerID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ProductMaster_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 150, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _prodName));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _crupUser));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProdId", SqlDbType.BigInt,10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _prodID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@Columns", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, columns));
                cmdToExecute.Parameters.Add(new SqlParameter("@ColumnValues", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, colunmvalues));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.VarChar, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _categoryID));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;


                if (_errorCode == -558)
                {

                    throw new Exception("Can not Update Product.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ProductMaster_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("ProductMaster::Update::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting Update");
            }
        }

        public override bool Delete()
        {
            _log.Trace("Entering Delete - Table:ProductCustom,ProductMaster ; @ProdID :{0}", _prodID);
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ProductMasterCustom_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            bool isSuccess = true;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ProdID", SqlDbType.BigInt, 10, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _prodID));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataownerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                 cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

               

                if ( _errorCode == -1001)
                {
                   // throw new Exception("Can not Delete Product.");
                    isSuccess = false;
                }
                else if (_errorCode != 0 )
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ProductMasterCustom_Delete' reported the ErrorCode: " + _errorCode);
                }
               
                return isSuccess;
            }
            catch (Exception ex)
            {
                _log.Error("Delete:{0}", ex.Message);
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting Delete");
            }
        }






        
    #region Class Property Declarations

        public string ProdName
        {
            get { return _prodName ; }
            set { _prodName = value; }
        }

        public string ProdPref
        {
            get { return _prodPref ; }
            set { _prodPref = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public Int32 DataOwnerID
        {
            get
            {
                return _dataOwnerID;
            }
            set
            {
                _dataOwnerID = value;
            }
        }

        public Int32 CompanyID
        {
            get
            {
                return _companyID ;
            }
            set
            {
                _companyID = value;
            }
        }

        public long ProdID
        {
            get
            {
                return _prodID;
            }
            set
            {
                _prodID = value;
            }
        }


        public Int32 CrupUser
        {
            get { return _crupUser; }
            set { _crupUser = value; }
        }

        public Int32 CategoryID
        {
            get
            {
                return _categoryID ;
            }
            set
            {
                _categoryID = value;
            }
        }

        public string ProductSKU
        {
            get { return _productSKU; }
            set { _productSKU = value; }
        }

        public string SKUDescription
        {
            get { return _skuDescription; }
            set { _skuDescription = value; }
        }
        public Int64 SkuID
        {
            get { return _skuID; }
            set { _skuID = value; }
        }

        public long ProductID
        {
            get
            {
                return _productID;
            }
            set
            {
                
             _productID = value;
               
               
                
            }
        }

        public Int32 PackageID
        {
            get
            {
                return _packageID;
            }
            set
            {
                _packageID = value;
            }
        }


    

            #endregion


    }
 }

