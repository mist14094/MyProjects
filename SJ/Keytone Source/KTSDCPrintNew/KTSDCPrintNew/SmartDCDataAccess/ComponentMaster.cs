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
    public class ComponentMaster : DBInteractionBase
    {
        private Int32 _id, _refComponentCategory, _refInstanceSequenceId;
        private String _instanceName, _componentName, _componentDescription, _omponentAssembly, _componentClass, _componentDefaultConfig, _xsdRelativePath;
        private Boolean _createDefaultInstance;

        #region ClassProperties

        public Int32 Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public String InstanceName
        {
            get
            {
                return _instanceName;
            }
            set
            {
                _instanceName = value;
            }
        }

        public String ComponentName
        {
            get
            {
                return _componentName;
            }
            set
            {
                _componentName = value;
            }
        }

        public String ComponentDescription
        {
            get
            {
                return _componentDescription;
            }
            set
            {
                _componentDescription = value;
            }
        }

        public Int32 RefComponentCategory
        {
            get
            {
                return _refComponentCategory;
            }
            set
            {
                _refComponentCategory = value;
            }
        }

        public String ComponentAssembly
        {
            get
            {
                return _omponentAssembly;
            }
            set
            {
                _omponentAssembly = value;
            }
        }

        public String ComponentClass
        {
            get
            {
                return _componentClass;
            }
            set
            {
                _componentClass = value;
            }
        }

        public String ComponentDefaultConfig
        {
            get
            {
                return _componentDefaultConfig;
            }
            set
            {
                _componentDefaultConfig = value;
            }
        }

        public Boolean CreateDefaultInstance
        {
            get
            {
                return _createDefaultInstance;
            }
            set
            {
                _createDefaultInstance = value;
            }
        }

        public String XsdRelativePath
        {
            get
            {
                return _xsdRelativePath;
            }
            set
            {
                _xsdRelativePath = value;
            }
        }

        public Int32 RefInstanceSequenceId
        {
            get
            {
                return _refInstanceSequenceId;
            }
            set
            {
                _refInstanceSequenceId = value;
            }
        }

        #endregion

        public ComponentMaster()
        {

        }

        #region Select

        /// <summary>
        /// Purpose: Select method. This method will Select one existing row from the database, based on the Primary Key.
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>UserID</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        ///		 <LI>UserID</LI>
        ///		 <LI>Name</LI>
        ///		  <LI>EmailID</LI>
        ///		 <LI>UserName</LI>
        ///		 <LI>Password</LI>
        ///		 <LI>RoleID</LI>
        ///		 <LI>Active</LI>
        ///		 <LI>ModifiedDate</LI>
        ///		 <LI>CreatedDate</LI>
        /// </UL>
        /// Will fill all properties corresponding with a field in the table with the value of the row selected.
        /// </remarks>
        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ComponentMaster_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ComponentMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RefComponentCategory", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefComponentCategory));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure '[pr_ComponentMaster_SelectOne]' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("[ComponentMaster]::SelectOne::Error occured.", ex);
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

        #endregion

    }
}
