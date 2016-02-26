using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;

namespace KTone.DAL.KTDAGlobaApplLib
{
    public class Client : DBInteractionBase
    {
        #region Constant Declarations
        public const string COMPANY_NAME = "CompanyName";
        public const string ADDRESS1 = "Address1";
        public const string ADDRESS2 = "Address2";
        public const string CONTACT_NO = "ContactNo";
        public const string WEBSITE = "WebSite";
        public const string LOGO = "Logo";
        #endregion

        #region Class Member Declarations
        private String _companyName, _address1, _address2, _contactNo, _website;
        private Int32 _dataOwnerID;
        private byte[] _imageArr;
        #endregion

        /// <summary>
        /// Purpose: Class constructor.
        /// </summary>
        public Client()
        {
            // Nothing for now.
        }

        #region Class Methods Declarations

        /// <summary>
        /// Purpose: SelectAll method. This method will Select all rows from the table.
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Client_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ClientInfo");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
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
                    throw new Exception("Stored Procedure 'pr_Client_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Client::SelectAll::Error occured.", ex);
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

        /// <summary>
        /// Purpose: Insert method. This method will insert one new row into the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>CompanyName</LI>
        ///		 <LI>Address1</LI>
        ///		 <LI>Address2 May be SqlString.Null</LI>
        ///		 <LI>ContactNo May be SqlString.Null</LI>
        ///		 <LI>Website. May be SqlString.Null</LI>
        ///		 <LI>Logo</LI>        
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>        
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:ClientInfo ; CompanyName:{0}," +
            "Address1 :{1}, Address2:{2},ContactNo:{3},Website:{4}," +
            "Logo:{5}", _companyName, _address1, _address2, _contactNo, _website, _imageArr.Length);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Client_Save]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@companyName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _companyName));
                cmdToExecute.Parameters.Add(new SqlParameter("@address1", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _address1));
                cmdToExecute.Parameters.Add(new SqlParameter("@address2", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _address2));
                cmdToExecute.Parameters.Add(new SqlParameter("@contactNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _contactNo));
                cmdToExecute.Parameters.Add(new SqlParameter("@webSite", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _website));
                cmdToExecute.Parameters.Add(new SqlParameter("@logo", SqlDbType.Image, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _imageArr));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
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

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Client_Save' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Client::Insert::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Insert");
            }
        }
        #endregion

        #region [Class Property Declarations]
        public String CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                String companyNameTmp = (String)value;
                if (companyNameTmp == null)
                {
                    throw new ArgumentOutOfRangeException("Company Name", "Company Name can't be NULL");
                }
                _companyName = value;
            }
        }
        public String Address1
        {
            get
            {
                return _address1;
            }
            set
            {
                String address1Tmp = (String)value;
                if (address1Tmp == null)
                {
                    throw new ArgumentOutOfRangeException("Address1", "Address1 can't be NULL");
                }
                _address1 = value;
            }
        }
        public String Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
            }
        }
        public String ContactNo
        {
            get
            {
                return _contactNo;
            }
            set
            {
                _contactNo = value;
            }
        }
        public String Website
        {
            get
            {
                return _website;
            }
            set
            {
                _website = value;
            }
        }
        public byte[] ImageByteArr
        {
            get
            {
                return _imageArr;
            }
            set
            {
                byte[] imageArrTmp = value;
                if (imageArrTmp.Length == 0)
                {
                    throw new ArgumentOutOfRangeException("Logo", "Log can't be NULL");
                }
                _imageArr = value;
            }
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
        #endregion
    }
}
