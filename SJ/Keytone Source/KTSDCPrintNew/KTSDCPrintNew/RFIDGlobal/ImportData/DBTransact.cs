/********************************************************************************************************
Copyright (c) 2005 KeyTone Technologies.All Right Reserved

KeyTone's source code and documentation is copyrighted and contains proprietary information.
Licensee shall not modify, create any derivative works, make modifications, improvements, 
distribute or reveal the source code ("Improvements") to anyone other than the software 
developers of licensee's organization unless the licensee has entered into a written agreement
("Agreement") to do so with KeyTone Technologies Inc. Licensee hereby assigns to KeyTone all right,
title and interest in and to such Improvements unless otherwise stated in the Agreement. Licensee 
may not resell, rent, lease, or distribute the source code alone, it shall only be distributed in 
compiled component of an application within the licensee'organization. 
The licensee shall not resell, rent, lease, or distribute the products created from the source code
in any way that would compete with KeyTone Technologies Inc. KeyTone' copyright notice may not be 
removed from the source code.
   
Licensee may be held legally responsible for any infringement of intellectual property rights that
is caused or encouraged by licensee's failure to abide by the terms of this Agreement. Licensee may 
make copies of the source code provided the copyright and trademark notices are reproduced in their 
entirety on the copy. KeyTone reserves all rights not specifically granted to Licensee. 
 
Use of this source code constitutes an agreement not to criticize, in any way, the code-writing style
of the author, including any statements regarding the extent of documentation and comments present.

THE SOFTWARE IS PROVIDED "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER  LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. KEYTONE TECHNOLOGIES SHALL NOT BE LIABLE FOR ANY DAMAGES 
SUFFERED BY LICENSEE AS A RESULT OF USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.
********************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using KTone.RFIDGlobal.ImportData;
using KTone.RFIDGlobal;
using KTone.DAL.KTDBBaseLib;


using NLog;
using System.Data.SqlTypes;

namespace KTone.RFIDGlobal.ImportData
{
    /// <summary>
    /// Summary description for DBTransact.
    /// </summary>
    public class DBTransact : DBInteractionBase
    {
        private static readonly Logger log
            = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();

        //private static DBTransact objDBTrans = null;
        private DBManager m_objDBMgr = null;



        #region Table Names

        private const string ORDER_TABLE = "trOrders";
        private const string ORDER_DETAIL_TABLE = "trOrderDetail";
        private const string ORDER_ITEM_DETAIL_TABLE = "trOrderItemDetail";
        private const string ORDER_CONTAINER_ITEMDETAIL_TABLE = "trContainerItemDetail";
        private const string PRODUCT_MASTER_TABLE = "trProductMaster";
        private const string SKU_MASTER_TABLE = "trSKU_Master";
        private const string COMPANY_MASTER_TABLE = "trCompanyMaster";
        private const string SHIP_TO_ADDRESS_TABLE = "trShipToAddress";
        private const string SHIP_FROM_ADDRESS_TABLE = "trShipFromAddress";
        private const string CARRIER_ADDRESS_TABLE = "trCarrierAddress";
        private const string RETAILLINK_EPCDATA_TABLE = "trRetailLinkEPCData";


        #endregion Table Names

        #region ColumnNames
        private const string ORDER_TABLE_ID = "OrderNo";
        private const string ORDER_DETAIL_TABLE_ID = "OrderDetailId";
        private const string ORDER_ITEMDETAIL_TABLE_ID = "OrderItemId";
        private const string ORDER_ITEMDETAIL_TABLE_QUANTITY = "Quantity";
        private const string CONTAINER_ITEM_TABLE_ID = "ContainerItemDetailId";
        private const string CONTAINER_ITEM_TABLE_REFORDERDETAIL = "OrderDetailId";
        private const string CONTAINER_ITEM_TABLE_REFORDERITEMDETAIL = "RefOrderItemDetail";

        #endregion ColumnNames





        public DBTransact(string connStr)
        {
            //
            // TODO: Add constructor logic here
            //
            m_objDBMgr = new DBManager(connStr);
        }

        //		public static DBTransact GetInstance(string connStr)
        //		{
        //			if(objDBTrans == null)
        //				objDBTrans = new DBTransact(connStr);
        //			return objDBTrans;
        //		}

        public void RemoveInstance()
        {
            //objDBTrans = null;
            m_objDBMgr.CloseConnection();
        }

        public void UpdateOrderShipStatus()
        {
            try
            {
                m_objDBMgr.ExecuteSP(null, "pr_ShipStatusUpdater", false);
            }
            catch (Exception ex)
            {
                log.Error("ImportUtil:UpdateOrderShipStatus=>" + ex.Message, ex);
            }
        }

        public void UpdateShipmentURN(string uccStd, Int16 filter, Int16 extDigit)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                SqlParameter sqlParam = null;


                sqlParam = new SqlParameter("@filter", SqlDbType.SmallInt);
                sqlParam.Value = filter;
                sqlParams[0] = sqlParam;

                sqlParam = new SqlParameter("@extDigit", SqlDbType.SmallInt);
                sqlParam.Value = extDigit;
                sqlParams[1] = sqlParam;

                sqlParam = new SqlParameter("@uccStd", SqlDbType.VarChar, 10);
                sqlParam.Value = uccStd;
                sqlParams[2] = sqlParam;


                m_objDBMgr.ExecuteSP(sqlParams, "pr_UpdateUrn", false);
            }
            catch (Exception ex)
            {
                log.Error("ImportUtil:UpdateOrderShipStatus=>" + ex.Message, ex);
            }
        }


        //Updates the SAP data into database
        public bool UpdateFlatFileData(TableInfo[] tableList, Hashtable fieldValueHash, bool orderCompleted)
        {
            string strSQL = string.Empty;
            string strWhrCond = string.Empty;

            try
            {
                m_objDBMgr.BiginTrans();
                for (int tblCnt = 0; tblCnt < tableList.Length; tblCnt++)
                {
                    strWhrCond = GetWhereCond(tableList[tblCnt].fieldDetails, fieldValueHash);
                    bool recExist = IsRecordExist(strWhrCond, tableList[tblCnt].tableName);

                    //					//Lexar Specific
                    //					// TODO::if file is shipOrders then Check whether record is updated with status
                    //					//If yes ignore otherwise update
                    //					if(tableList[tblCnt].tableName.ToUpper() == "ORDERDETAIL")
                    //					{
                    //						
                    //						bool statusUpdated = IsRecordExist(strWhrCond + " AND OrderStatus='C' ", tableList[tblCnt].tableName);
                    //						if(statusUpdated)
                    //						{
                    //							DeleteOrderDetail(fieldValueHash["DeliveryNum"].ToString());
                    //							continue;
                    //						}
                    //						if(orderCompleted)
                    //							fieldValueHash["OrderStatus"] = 'C';
                    //						else
                    //							fieldValueHash["OrderStatus"] = 'P';
                    //					}

                    if (recExist)
                    {

                        //strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);
                        //if (useTransaction)
                        m_objDBMgr.CommitTrans();
                        continue;
                    }
                    else
                        strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);

                    log.Trace("ImportUtil:DBTransact::UpdateFlatFileData:SQL => " + strSQL);
                    int recUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                    if (tableList[tblCnt].tableName.ToUpper() == "ORDERDETAIL" && orderCompleted)// && (!recExist))
                        DeleteOrderDetail(fieldValueHash["DeliveryNum"].ToString());
                }
                m_objDBMgr.CommitTrans();
            }
            catch (Exception ex)
            {
                m_objDBMgr.RollBackTrans();
                log.Error("ImportUtil:DBTransact::UpdateFlatFileData=>" + ex.Message, ex);
                return false;
            }
            return true;
        }

        public void UpdateMasterDetailImport(TableInfo[] tableList, DataTable dtMaster, DataTable dtDetail)
        {
            Hashtable fieldValueHash = null;
            try
            {
                m_objDBMgr.BiginTrans();
                fieldValueHash = new Hashtable();
                foreach (DataColumn masterCol in dtMaster.Columns)
                {
                    fieldValueHash[masterCol.ColumnName] = dtMaster.Rows[0][masterCol.ColumnName].ToString();
                }
                UpdateManualImportData(tableList[0], fieldValueHash, false);

                foreach (DataRow drDetail in dtDetail.Rows)
                {
                    foreach (DataColumn detCol in dtDetail.Columns)
                    {
                        fieldValueHash[detCol.ColumnName] = drDetail[detCol.ColumnName].ToString();
                    }
                    UpdateManualImportData(tableList[1], fieldValueHash, false);
                }
                m_objDBMgr.CommitTrans();

            }
            catch (Exception ex)
            {
                m_objDBMgr.RollBackTrans();
                throw ex;
            }
        }
        //Updates the data into database
        public bool UpdateManualImportData(TableInfo tableInfo, Hashtable fieldValueHash, bool useTransaction)
        {
            string strSQL = string.Empty;
            string strWhrCond = string.Empty;

            try
            {
                if (useTransaction)
                    m_objDBMgr.BiginTrans();
                bool recExist = false;
                //strWhrCond = GetWhereCond(tableInfo.fieldDetails, fieldValueHash);
                if (tableInfo.tableName == "OrderDetail" || tableInfo.tableName == "trOrderDetail")
                    strWhrCond = "[OrderNo]='" + fieldValueHash["OrderNo"].ToString().Trim().Replace("'", "''") + "' AND " +
                        "[UCCNo]='" + fieldValueHash["UCCNo"].ToString().Trim().Replace("'", "''") + "' AND " +
                        "[ItemNo]='" + fieldValueHash["ItemNo"].ToString().Trim().Replace("'", "''") + "'";
                else
                    strWhrCond = GetWhereCond(tableInfo.fieldDetails, fieldValueHash);

                DataTable dtTranRecords = GetInTransRecords(strWhrCond, tableInfo.tableName);
                if (dtTranRecords != null && dtTranRecords.Rows.Count > 0)
                    recExist = true;

                if (recExist)
                {
                    if (tableInfo.tableName == "OrderDetail" || tableInfo.tableName == "trOrderDetail")
                    {
                        fieldValueHash.Remove("IsVerified");
                        if (fieldValueHash.ContainsKey("OrderStatus"))
                        {
                            bool statusUpdated = false;
                            DataTable dtStatusRecords = GetInTransRecords(strWhrCond + " AND OrderStatus='C' ", tableInfo.tableName);
                            if (dtStatusRecords != null && dtStatusRecords.Rows.Count > 0)
                                statusUpdated = true;

                            if (statusUpdated)
                            {
                                DeleteOrderDetail(fieldValueHash["DeliveryNum"].ToString());
                                return false;
                            }
                        }
                    }
                    strSQL = GetUpdateSQL(tableInfo, fieldValueHash, strWhrCond);

                }
                else

                    strSQL = GetInsertSQL(tableInfo, fieldValueHash);

                log.Trace("DBTransact::UpdateImportData:SQL => " + strSQL);
                int recUpd = m_objDBMgr.ExecuteSQL(strSQL, true);

                if (useTransaction)
                    m_objDBMgr.CommitTrans();
                return true;
            }
            catch (Exception ex)
            {
                if (useTransaction)
                    m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::UpdateImportData=>" + ex.Message, ex);
                throw ex;
            }
        }

        //Updates the data into database
        public bool UpdateImportData(TableInfo[] tableList, Hashtable fieldValueHash, bool useTransaction)
        {
            string strSQL = string.Empty;
            string strWhrCond = string.Empty;

            try
            {
                if (useTransaction)
                    m_objDBMgr.BiginTrans();
                for (int tblCnt = 0; tblCnt < tableList.Length; tblCnt++)
                {
                    strWhrCond = GetWhereCond(tableList[tblCnt].fieldDetails, fieldValueHash);
                    bool recExist = IsRecordExist(strWhrCond, tableList[tblCnt].tableName);

                    if (recExist)
                    {
                        //  if (tableList[tblCnt].tableName == RETAILLINK_EPCDATA_TABLE)
                        // {
                        strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);
                        // }
                        // else
                        //  continue;
                    }
                    else
                    {

                        strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                    }

                    log.Trace("DBTransact::UpdateImportData:SQL => " + strSQL);
                    int recUpd = 0;
                    if (strSQL != string.Empty)
                        recUpd = m_objDBMgr.ExecuteSQL(strSQL, true);

                }
                if (useTransaction)
                    m_objDBMgr.CommitTrans();
                return true;
            }
            catch (Exception ex)
            {
                if (useTransaction)
                    m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::UpdateImportData=>" + ex.Message, ex);
                throw ex;


            }
        }

        public bool UpdateImportData(TableInfo[] tableList, Hashtable fieldValueHash, bool useTransaction, out bool recExist, out bool recInserted, out bool recUpdated)
        {
            string strSQL = string.Empty;
            string strWhrCond = string.Empty;
            recExist = false; recInserted = false; recUpdated = false;
            bool customRecExist = false; bool IdetailsRecExist = false;
            string skuID1 = "0"; string productID1 = "0"; string companyID1 = "0"; string ID1 = "0";

            try
            {
                if (useTransaction)
                    m_objDBMgr.BiginTrans();
                for (int tblCnt = 0; tblCnt < tableList.Length; tblCnt++)
                {
                    strWhrCond = GetWhereCond(tableList[tblCnt].fieldDetails, fieldValueHash);
                    recExist = IsRecordExist(strWhrCond, tableList[tblCnt].tableName);

                    if (recExist)
                    {
                        strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);
                        if (tableList[tblCnt].tableName == "SKUMaster" || tableList[tblCnt].tableName == "ItemMaster")
                            recUpdated = true;
                    }
                    else
                    {
                        strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                        if (tableList[tblCnt].tableName == "SKUMaster" || tableList[tblCnt].tableName == "ItemMaster")
                            recInserted = true;
                    }

                    log.Trace("DBTransact::UpdateImportData:SQL => " + strSQL);
                    int recUpd = 0;
                    if (strSQL != string.Empty)
                        recUpd = m_objDBMgr.ExecuteSQL(strSQL, true);

                    if (useTransaction)
                        m_objDBMgr.CommitTrans();


                    if (useTransaction)
                        m_objDBMgr.BiginTrans();

                    if (tableList[tblCnt].tableName == "SKUMaster")
                    {
                        DataSet dsSDC = m_objDBMgr.GetDataSet("Select  SKU_ID  from " + tableList[tblCnt].tableName + " where " + strWhrCond);
                        if (dsSDC != null && dsSDC.Tables.Count > 0 && dsSDC.Tables[0].Rows.Count > 0)
                        {
                            skuID1 = dsSDC.Tables[0].Rows[0]["SKU_ID"].ToString();
                            if (skuID1 != "")
                            {
                                recExist = true;
                            }
                            else
                            {
                                recExist = false;
                                throw new ApplicationException("Cannot find the " + skuID1 + ".");
                            }
                        }
                        else
                            recExist = false;
                    }
                    else if (tableList[tblCnt].tableName == "ProductMaster")
                    {
                        DataSet dsSDC = m_objDBMgr.GetDataSet("Select  ProductID  from " + tableList[tblCnt].tableName + " where " + strWhrCond);
                        if (dsSDC != null && dsSDC.Tables.Count > 0 && dsSDC.Tables[0].Rows.Count > 0)
                        {
                            productID1 = dsSDC.Tables[0].Rows[0]["ProductID"].ToString();
                            if (productID1 != "")
                            {
                                recExist = true;
                            }
                            else
                            {
                                recExist = false;
                                throw new ApplicationException("Cannot find the " + productID1 + ".");
                            }
                        }
                        else
                            recExist = false;
                    }
                    else if (tableList[tblCnt].tableName == "CompanyMaster")
                    {
                        DataSet dsSDC = m_objDBMgr.GetDataSet("Select  CompanyID  from " + tableList[tblCnt].tableName + " where " + strWhrCond);
                        if (dsSDC != null && dsSDC.Tables.Count > 0 && dsSDC.Tables[0].Rows.Count > 0)
                        {
                            companyID1 = dsSDC.Tables[0].Rows[0]["CompanyID"].ToString();
                            if (companyID1 != "")
                            {
                                recExist = true;
                            }
                            else
                            {
                                recExist = false;
                                throw new ApplicationException("Cannot find the " + companyID1 + ".");
                            }
                        }
                        else
                            recExist = false;
                    }
                    else if (tableList[tblCnt].tableName == "ItemMaster")
                    {
                        DataSet dsSDC = m_objDBMgr.GetDataSet("Select  ID  from " + tableList[tblCnt].tableName + " where " + strWhrCond);
                        if (dsSDC != null && dsSDC.Tables.Count > 0 && dsSDC.Tables[0].Rows.Count > 0)
                        {
                            ID1 = dsSDC.Tables[0].Rows[0]["ID"].ToString();
                            if (ID1 != "")
                            {
                                recExist = true;
                            }
                            else
                            {
                                recExist = false;
                                throw new ApplicationException("Cannot find the " + ID1 + ".");
                            }
                        }
                        else
                            recExist = false;
                    }
                    else
                    {
                        recExist = false;
                    }

                    if (recExist)
                    {
                        // if (!tableList[0].updateData)
                        //   continue;
                        if (fieldValueHash.ContainsKey("CustomTableInfo"))
                        {
                            if (tableList[tblCnt].tableName == "SKUMaster")
                            {
                                customRecExist = IsRecordExist(" SKU_ID = " + skuID1, "SKUCustom");
                            }
                            if (tableList[tblCnt].tableName == "ProductMaster")
                            {
                                customRecExist = IsRecordExist(" ProductID = " + productID1, "ProductCustom");
                            }
                            if (tableList[tblCnt].tableName == "CompanyMaster")
                            {
                                customRecExist = IsRecordExist(" CompanyID = " + companyID1, "CompanyCustom");
                            }
                            if (tableList[tblCnt].tableName == "ItemMaster")
                            {
                                customRecExist = IsRecordExist(" ID = " + ID1, "ItemCustom");

                                IdetailsRecExist = IsRecordExist(" ID = " + ID1, "ItemDetails");
                            }
                        }
                        int recCustUpd = 0;
                        //if (IdetailsRecExist && tableList[tblCnt].tableName == "ItemMaster")
                        //{
                        //    strSQL = "Update ItemDetails set ID = " + ID1 + " ";
                        //        //GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);
                        //       recCustUpd= m_objDBMgr.ExecuteSQL(strSQL, true);
                        //}

                        if (!IdetailsRecExist && tableList[tblCnt].tableName == "ItemMaster")
                        {
                            strSQL = "Insert into ItemDetails ( ID ) values (" + ID1 + " ) ";
                            recCustUpd = m_objDBMgr.ExecuteSQL(strSQL, true);

                        }
                        if (fieldValueHash.ContainsKey("CustomTableInfo"))
                        {
                            if (customRecExist)
                            {
                                if (tableList[tblCnt].tableName == "SKUMaster")
                                {
                                    List<string> CustomCols = m_objDBMgr.GetColumns("SKUCustom", true);
                                    string strCols = "";
                                    foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                    {
                                        if (CustomCols.Contains(custom.Key.ToUpper()))
                                        {
                                            strCols += " " + custom.Key + " = '" + custom.Value + "' ,";
                                        }
                                    }
                                    if (strCols.Trim().EndsWith(","))
                                        strCols = strCols.Trim().Substring(0, strCols.Trim().Length - 1);
                                    if (!strCols.Trim().Equals(string.Empty))
                                    {
                                        strSQL = "UPDATE SKUCustom SET  " + strCols + " WHERE SKU_ID = " + skuID1;

                                        recCustUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                                    }
                                }
                                if (tableList[tblCnt].tableName == "ProductMaster")
                                {
                                    List<string> CustomCols = m_objDBMgr.GetColumns("ProductCustom", true);
                                    string strCols = "";
                                    foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                    {
                                        if (CustomCols.Contains(custom.Key.ToUpper()))
                                        {
                                            strCols += " " + custom.Key + " = '" + custom.Value + "' ,";
                                        }
                                    }
                                    if (strCols.Trim().EndsWith(","))
                                        strCols = strCols.Trim().Substring(0, strCols.Trim().Length - 1);
                                    if (!strCols.Trim().Equals(string.Empty))
                                    {
                                        strSQL = "UPDATE ProductCustom SET  " + strCols + " WHERE ProductId = " + productID1;

                                        recCustUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                                    }
                                }
                                if (tableList[tblCnt].tableName == "CompanyMaster")
                                {
                                    List<string> CustomCols = m_objDBMgr.GetColumns("CompanyCustom", true);
                                    string strCols = "";
                                    foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                    {
                                        if (CustomCols.Contains(custom.Key.ToUpper()))
                                        {
                                            strCols += " " + custom.Key + " = '" + custom.Value + "' ,";
                                        }
                                    }
                                    if (strCols.Trim().EndsWith(","))
                                        strCols = strCols.Trim().Substring(0, strCols.Trim().Length - 1);
                                    if (!strCols.Trim().Equals(string.Empty))
                                    {
                                        strSQL = "UPDATE CompanyCustom SET  " + strCols + " WHERE CompanyID = " + companyID1;

                                        recCustUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                                    }
                                }
                                if (tableList[tblCnt].tableName == "ItemMaster")
                                {
                                    List<string> CustomCols = m_objDBMgr.GetColumns("ItemCustom", true);

                                    string strCols = "";
                                    //Import in ItemCustom Table
                                    foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                    {
                                        if (CustomCols.Contains(custom.Key.ToUpper()))
                                        {
                                            strCols += " " + custom.Key + " = '" + custom.Value + "' ,";
                                        }
                                    }
                                    if (strCols.Trim().EndsWith(","))
                                        strCols = strCols.Trim().Substring(0, strCols.Trim().Length - 1);
                                    if (!strCols.Trim().Equals(string.Empty))
                                    {
                                        strSQL = "UPDATE ItemCustom SET  " + strCols + " WHERE ID = " + ID1;

                                        recCustUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                                    }
                                    strCols = "";

                                }
                            }
                            else
                            {
                                if (tableList[tblCnt].tableName == "SKUMaster")
                                {
                                    string strCols = "";
                                    string strVals = "";
                                    List<string> CustomCols = m_objDBMgr.GetColumns("SKUCustom", true);

                                    foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                    {
                                        if (CustomCols.Contains(custom.Key.ToUpper()))
                                        {
                                            strCols += ", " + custom.Key;
                                            strVals += ", '" + custom.Value + "'";
                                        }
                                    }

                                    strSQL = "Insert into SKUCustom ( SKU_ID  " + strCols + " ) values (" + skuID1 + strVals + " ) ";
                                    recCustUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                                }
                                if (tableList[tblCnt].tableName == "ProductMaster")
                                {
                                    string strCols = "";
                                    string strVals = "";
                                    List<string> CustomCols = m_objDBMgr.GetColumns("ProductCustom", true);

                                    foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                    {
                                        if (CustomCols.Contains(custom.Key.ToUpper()))
                                        {
                                            strCols += ", " + custom.Key;
                                            strVals += ", '" + custom.Value + "'";
                                        }
                                    }

                                    strSQL = "Insert into ProductCustom ( ProductId  " + strCols + " ) values (" + productID1 + strVals + " ) ";
                                    recCustUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                                }
                                if (tableList[tblCnt].tableName == "CompanyMaster")
                                {
                                    string strCols = "";
                                    string strVals = "";
                                    List<string> CustomCols = m_objDBMgr.GetColumns("CompanyCustom", true);

                                    foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                    {
                                        if (CustomCols.Contains(custom.Key.ToUpper()))
                                        {
                                            strCols += ", " + custom.Key;
                                            strVals += ", '" + custom.Value + "'";
                                        }
                                    }

                                    strSQL = "Insert into CompanyCustom ( CompanyID  " + strCols + " ) values (" + companyID1 + strVals + " ) ";
                                    recCustUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                                }
                                if (tableList[tblCnt].tableName == "ItemMaster")
                                {
                                    string strCols = "";
                                    string strVals = "";
                                    List<string> CustomCols = m_objDBMgr.GetColumns("ItemCustom", true);

                                    foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                    {
                                        if (CustomCols.Contains(custom.Key.ToUpper()))
                                        {
                                            strCols += ", " + custom.Key;
                                            strVals += ", '" + custom.Value + "'";
                                        }
                                    }

                                    strSQL = "Insert into ItemCustom ( ID  " + strCols + " ) values (" + ID1 + strVals + " ) ";
                                    recCustUpd = m_objDBMgr.ExecuteSQL(strSQL, true);

                                }
                            }
                        }
                    }
                    else
                    {
                        int recCustIns = 0;
                        if (tableList[tblCnt].tableName == "SKUMaster")
                        {
                            strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                            skuID1 = m_objDBMgr.ExecuteInsertStatment(strSQL, true);

                            if (fieldValueHash.ContainsKey("CustomTableInfo"))
                            {
                                string strCols = "";
                                string strVals = "";
                                List<string> CustomCols = m_objDBMgr.GetColumns("SKUCustom", true);

                                foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                {
                                    if (CustomCols.Contains(custom.Key.ToUpper()))
                                    {
                                        strCols += ", " + custom.Key;
                                        strVals += ", '" + custom.Value + "'";
                                    }
                                }

                                strSQL = "Insert into SKUCustom ( SKU_ID  " + strCols + " ) values (" + skuID1 + strVals + " ) ";
                                recCustIns = m_objDBMgr.ExecuteSQL(strSQL, true);
                            }
                        }
                        if (tableList[tblCnt].tableName == "ProductMaster")
                        {
                            strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                            productID1 = m_objDBMgr.ExecuteInsertStatment(strSQL, true);
                            if (fieldValueHash.ContainsKey("CustomTableInfo"))
                            {
                                string strCols = "";
                                string strVals = "";
                                List<string> CustomCols = m_objDBMgr.GetColumns("ProductCustom", true);

                                foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                {
                                    if (CustomCols.Contains(custom.Key.ToUpper()))
                                    {
                                        strCols += ", " + custom.Key;
                                        strVals += ", '" + custom.Value + "'";
                                    }
                                }

                                strSQL = "Insert into ProductCustom ( ProductId  " + strCols + " ) values (" + productID1 + strVals + " ) ";
                                recCustIns = m_objDBMgr.ExecuteSQL(strSQL, true);
                            }
                        }
                        if (tableList[tblCnt].tableName == "CompanyMaster")
                        {
                            strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                            companyID1 = m_objDBMgr.ExecuteInsertStatment(strSQL, true);
                            if (fieldValueHash.ContainsKey("CustomTableInfo"))
                            {
                                string strCols = "";
                                string strVals = "";
                                List<string> CustomCols = m_objDBMgr.GetColumns("CompanyCustom", true);

                                foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                {
                                    if (CustomCols.Contains(custom.Key.ToUpper()))
                                    {
                                        strCols += ", " + custom.Key;
                                        strVals += ", '" + custom.Value + "'";
                                    }
                                }

                                strSQL = "Insert into CompanyCustom ( CompanyID  " + strCols + " ) values (" + companyID1 + strVals + " ) ";
                                recCustIns = m_objDBMgr.ExecuteSQL(strSQL, true);
                            }
                        }
                        if (tableList[tblCnt].tableName == "ItemMaster")
                        {
                            strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                            ID1 = m_objDBMgr.ExecuteInsertStatment(strSQL, true);
                            if (fieldValueHash.ContainsKey("CustomTableInfo"))
                            {
                                string strCols = "";
                                string strVals = "";
                                List<string> CustomCols = m_objDBMgr.GetColumns("ItemCustom", true);

                                foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                {
                                    if (CustomCols.Contains(custom.Key.ToUpper()))
                                    {
                                        strCols += ", " + custom.Key;
                                        strVals += ", '" + custom.Value + "'";
                                    }
                                }

                                strSQL = "Insert into ItemCustom ( ID  " + strCols + " ) values (" + ID1 + strVals + " ) ";
                                recCustIns = m_objDBMgr.ExecuteSQL(strSQL, true);
                            }
                        }

                    }

                    log.Trace("DBTransact::UpdateImportData:SQL => " + strSQL);
                }
                if (useTransaction)
                    UpdateNotifyCacheUpdateTable();
                m_objDBMgr.CommitTrans();
                return true;
            }
            catch (Exception ex)
            {
                if (useTransaction)
                    m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::UpdateImportData=>" + ex.Message, ex);
                throw ex;


            }
        }
        private string GetWhereCondForOrderDetail(FieldInfo[] fieldInfo, Hashtable fieldValueHash, out bool isContainarised)
        {
            string strWhrCond = string.Empty;
            isContainarised = false;
            if (fieldValueHash.ContainsKey("UCCNo"))
            {
                isContainarised = true;
            }
            foreach (FieldInfo fldDetail in fieldInfo)
            {
                if (fldDetail.isKeyCol)
                {
                    if (fldDetail.fieldName != "UCCNo")
                        if (fieldValueHash[fldDetail.fieldName].ToString().Trim() == "")
                        {
                            throw new ApplicationException("KeyColumn = " + fldDetail.fieldName + " cannot be blank");
                        }
                    if (fieldValueHash.ContainsKey(fldDetail.fieldName))
                    {
                        strWhrCond += "[" + fldDetail.fieldName + "]='" + fieldValueHash[fldDetail.fieldName].ToString().Trim().Replace("'", "''") + "' AND ";
                    }
                }
            }
            if (strWhrCond.Length > 0)
                strWhrCond = strWhrCond.Substring(0, strWhrCond.Length - 4);
            else
                throw new ApplicationException("No KeyColumn exists.");
            return strWhrCond;
        }



        public bool UpdateOrderDetailData(TableInfo[] tableList, Hashtable fieldValueHash)
        {
            string strSQL = string.Empty;
            string strWhrCond = string.Empty;

            try
            {
                m_objDBMgr.BiginTrans();
                for (int tblCnt = 0; tblCnt < tableList.Length; tblCnt++)
                {
                    strWhrCond = GetWhereCond(tableList[tblCnt].fieldDetails, fieldValueHash);
                    bool recExist = IsRecordExist(strWhrCond, tableList[tblCnt].tableName);

                    if (recExist)
                    {
                        if (tableList[tblCnt].tableName == "trOrderDetail")
                        {
                            fieldValueHash.Remove("IsVerified");
                            strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);
                        }

                    }
                    else
                        strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);

                    log.Trace("DBTransact::UpdateOrderDetailData:SQL => " + strSQL);
                    int recUpd = m_objDBMgr.ExecuteSQL(strSQL, true);

                }
                m_objDBMgr.CommitTrans();
            }
            catch (Exception ex)
            {
                m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::UpdateOrderDetailData=>" + ex.Message, ex);
                return false;
            }
            return true;
        }

        private void DeleteOrderDetail(string DelNum)
        {
            //==========7Feb06===============
            //If recNotIxist then Delete pending records corr to DelNo 

            string discardedRecSQL = "DELETE FROM trOrderDetail WHERE OrderStatus = 'P' AND DeliveryNum='" + DelNum + "'";
            int discardeCnt = m_objDBMgr.ExecuteSQL(discardedRecSQL, true);
            if (discardeCnt > 0)
                log.Trace("ImportUtil:DBTransact::UpdateFlatFileData: " + discardeCnt.ToString() + " Record Discarded for DeliveryNum = " + DelNum);

            //=========================
        }

        /// <summary>
        /// Based on key column it generates where condition
        /// </summary>
        /// <param name="fieldsDetail"></param>
        /// <returns></returns>
        public string GetWhereCond(FieldInfo[] fieldsDetail, Hashtable fieldValueHash)
        {
            string strWhrCond = string.Empty;
            foreach (FieldInfo fldDetail in fieldsDetail)
            {
                if (fldDetail.isKeyCol)
                {
                    if (fieldValueHash[fldDetail.fieldName].ToString().Trim() == "")
                        throw new ApplicationException("KeyColumn = " + fldDetail.fieldName + " cannot be blank");
                    strWhrCond += "[" + fldDetail.fieldName + "]='" + fieldValueHash[fldDetail.fieldName].ToString().Trim().Replace("'", "''") + "' AND ";
                }
            }
            if (strWhrCond.Length > 0)
                strWhrCond = strWhrCond.Substring(0, strWhrCond.Length - 4);
            else
                throw new ApplicationException("No KeyColumn exists.");
            return strWhrCond;
        }

        public string GetWhereCond(FieldInfo[] fieldsDetail, string[] fieldValues)
        {
            string strWhrCond = string.Empty;
            foreach (FieldInfo fldDetail in fieldsDetail)
            {
                if (fldDetail.isKeyCol)
                {
                    if (fieldValues[fldDetail.indexValue].Trim() == "")
                        throw new ApplicationException("KeyColumn = " + fldDetail.fieldName + " cannot be blank");
                    strWhrCond += "[" + fldDetail.fieldName + "]='" + fieldValues[fldDetail.indexValue].Trim().Replace("'", "''") + "' AND ";
                }
            }
            if (strWhrCond.Length > 0)
                strWhrCond = strWhrCond.Substring(0, strWhrCond.Length - 4);
            else
                throw new ApplicationException("No KeyColumn exists.");
            return strWhrCond;
        }

        private string GetInsertSQL(TableInfo tblInfo, Hashtable fieldValueHash)
        {
            string fieldCol = string.Empty;
            string fieldVal = string.Empty;

            for (int fieldCount = 0; fieldCount < tblInfo.fieldDetails.Length; fieldCount++)
            {
                if (!tblInfo.fieldDetails[fieldCount].DBInsertReq)
                    continue;

                if (fieldValueHash.ContainsKey(tblInfo.fieldDetails[fieldCount].fieldName) == false)
                    continue;

                if (fieldValueHash[tblInfo.fieldDetails[fieldCount].fieldName] == null)
                    continue;

                if (fieldValueHash[tblInfo.fieldDetails[fieldCount].fieldName].ToString().Trim() == "")
                    continue;

                //if (!tblInfo.fieldDetails[fieldCount].DBInsertReq)
                //    continue;

                fieldCol += "[" + tblInfo.fieldDetails[fieldCount].fieldName + "],";
                fieldVal += "'" + fieldValueHash[tblInfo.fieldDetails[fieldCount].fieldName].ToString().Trim().Replace("'", "''") + "',";
            }
            if (fieldCol.Length > 0 && fieldCol.EndsWith(","))
                fieldCol = fieldCol.Substring(0, fieldCol.Length - 1);
            if (fieldVal.Length > 0 && fieldVal.EndsWith(","))
                fieldVal = fieldVal.Substring(0, fieldVal.Length - 1);

            if (fieldCol.Length == 0)
                return string.Empty;

            string strSQL = "INSERT INTO [" + tblInfo.tableName + "] (" + fieldCol + ") VALUES (" +
                fieldVal + ")";
            return strSQL;
        }

        private string GetUpdateSQL(TableInfo tblInfo, Hashtable fieldValueHash, string strWhrCond)
        {
            string fieldColVal = string.Empty;

            for (int fieldCount = 0; fieldCount < tblInfo.fieldDetails.Length; fieldCount++)
            {
                if (!tblInfo.fieldDetails[fieldCount].isKeyCol)
                {
                    //******Commented for Updating the Records****** //
                  if (!tblInfo.fieldDetails[fieldCount].DBUpdateReq)
                       continue;

                    if (fieldValueHash.ContainsKey(tblInfo.fieldDetails[fieldCount].fieldName) == false)
                        continue;

                    if (fieldValueHash[tblInfo.fieldDetails[fieldCount].fieldName] == null)
                        continue;

                    if (fieldValueHash[tblInfo.fieldDetails[fieldCount].fieldName].ToString().Trim() == "")
                        continue;

                    //if (!tblInfo.fieldDetails[fieldCount].DBInsertReq)
                    //    continue;

                    fieldColVal += "[" + tblInfo.fieldDetails[fieldCount].fieldName + "]='" + fieldValueHash[tblInfo.fieldDetails[fieldCount].fieldName].ToString().Trim().Replace("'", "''") + "',";
                }
            }

            if (fieldColVal.Length > 0 && fieldColVal.EndsWith(","))
                fieldColVal = fieldColVal.Substring(0, fieldColVal.Length - 1);

            if (fieldColVal.Length == 0)
                return string.Empty;

            string strSQL = "UPDATE [" + tblInfo.tableName + "] SET " + fieldColVal + " WHERE " + strWhrCond;
            return strSQL;
        }

        private SqlParameter[] GetSQLParams(TableInfo tblInfo, Hashtable fieldValueHash)
        {
            string fieldCol = string.Empty;
            string fieldVal = string.Empty;
            Hashtable paramHash = new Hashtable();
            for (int fieldCount = 0; fieldCount < tblInfo.fieldDetails.Length; fieldCount++)
            {
                if (fieldValueHash.ContainsKey(tblInfo.fieldDetails[fieldCount].fieldName) == false)
                    continue;

                if (fieldValueHash[tblInfo.fieldDetails[fieldCount].fieldName] == null)
                    continue;

                if (fieldValueHash[tblInfo.fieldDetails[fieldCount].fieldName].ToString().Trim() == "")
                    continue;

                if (!tblInfo.fieldDetails[fieldCount].DBInsertReq)
                    continue;

                paramHash[tblInfo.fieldDetails[fieldCount].fieldName] = fieldValueHash[tblInfo.fieldDetails[fieldCount].fieldName];

                //fieldCol += "[" + tblInfo.fieldDetails[fieldCount].fieldName + "],";
                //fieldVal += "'" + fieldValueHash[tblInfo.fieldDetails[fieldCount].fieldName].ToString().Trim().Replace("'", "''") + "',";
            }
            //if (fieldCol.Length > 0 && fieldCol.EndsWith(","))
            //    fieldCol = fieldCol.Substring(0, fieldCol.Length - 1);
            //if (fieldVal.Length > 0 && fieldVal.EndsWith(","))
            //    fieldVal = fieldVal.Substring(0, fieldVal.Length - 1);

            //if (fieldCol.Length == 0)
            //    return string.Empty;

            //string strSQL = "INSERT INTO [" + tblInfo.tableName + "] (" + fieldCol + ") VALUES (" +
            //    fieldVal + ")";
            SqlParameter[] sqlParamArr = new SqlParameter[paramHash.Count];
            return sqlParamArr;
        }

        #region [ Generic Function ]


        public bool IsRecordExist(string strWhrCond, string tableName)
        {
            string strSQL = "SELECT * FROM [" + tableName + "] WHERE " + strWhrCond;
            if (m_objDBMgr.GetDataSet(strSQL).Tables[0].Rows.Count > 0)
                return true;
            return false;
        }




        public bool IsOrderDetailExist(string orderNum, string uccNo, string qtyInCont)
        {

            string strSQL = "select containeritemdetailid ,qtyincontainer,orderdetailid from trContainerItemDetail where OrderDetailId = (select orderdetailid from trOrderDetail where OrderNo='" + orderNum + "'" + "and UCCNo ='" + uccNo + "')" + " and QtyInContainer='" + qtyInCont + "'";

            if (m_objDBMgr.GetInTransDataSet(strSQL).Tables[0].Rows.Count > 0)
                return true;
            return false;
        }



        public bool IsRecordExistInTrans(string strWhrCond, string tableName)
        {
            string strSQL = "SELECT * FROM [" + tableName + "] WHERE " + strWhrCond;
            if (m_objDBMgr.GetInTransDataSet(strSQL).Tables[0].Rows.Count > 0)
                return true;
            return false;
        }
        public DataTable GetRecords(string strWhrCond, string tableName)
        {
            string strSQL = "SELECT * FROM [" + tableName + "] WHERE " + strWhrCond;
            DataTable dt = m_objDBMgr.GetDataSet(strSQL).Tables[0];
            return dt;
        }

        public DataTable GetRecords(string strWhrCond, string tableName, string[] cols)
        {
            string columns = "";
            foreach (string col in cols)
                columns += "[" + col + "] , ";
            columns = columns.Substring(0, columns.LastIndexOf(','));
            string strSQL = "SELECT " + columns + " FROM [" + tableName + "] WHERE " + strWhrCond;
            DataTable dt = m_objDBMgr.GetDataSet(strSQL).Tables[0];
            return dt;
        }

        public object GetRecordId(string strWhrCond, string tableName, string cols)
        {
            string strQuery = "Select [" + cols + "] " +
               "From " + tableName + " " +
               "where " +
               strWhrCond;

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return dsFrom.Tables[0].Rows[0][cols];
            }
            return 0;
        }

        #endregion [ Generic Function ]

        public DataTable GetInTransRecords(string strWhrCond, string tableName)
        {
            string strSQL = "SELECT * FROM [" + tableName + "] WHERE " + strWhrCond;
            DataTable dt = m_objDBMgr.GetInTransDataSet(strSQL).Tables[0];
            return dt;
        }

        public bool IsSSCCRead(string sscc_URN)
        {
            return IsRecordExist("SSCC_URN='" + sscc_URN + "'", "PalletMaster");
        }

        public long GetSKUMAsterID(string strWhere, string productname, string companyname)
        {
            string strQuery = "Select SKU_ID " +
                "From SKUMaster " +
                "where " + strWhere + "and ProdcutID = (Select ProductID from ProductMaster where CompanyID=(Select CompanyID from CompanyMaster where CompanyName='" + companyname + "')" +
                "and ProductName='" + productname + "')";

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt64(dsFrom.Tables[0].Rows[0]["SKU_ID"].ToString());
            }
            return 0;
        }

        public string GetCUSTID(string strWhere)
        {
            string strQuery = "Select CustomerUniqueId " +
                 "From ItemMaster " +
                 "where " + strWhere + "";

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return dsFrom.Tables[0].Rows[0]["CustomerUniqueId"].ToString();
            }
            return null;
        }   

        public int GetStateProvinceID(string stateValue, string countryValue)
        {

            string strQuery = "Select S.stateProvinceID from trStateProvince S " +
                               " INNER JOIN trCountry_Region C " +
                               " ON S.CountryRegionCode = C.CountryCode " +
                               " Where (C.CountryCode = '" + countryValue +
                               "' OR C.CountryName = '" + countryValue +
                               "') AND  (S.stateProvinceCode ='" +
                                stateValue + "' OR [name] = '" + stateValue + "') ";

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsFrom.Tables[0].Rows[0]["stateProvinceId"].ToString());
            }
            return 0;
        }
        public int GetOrderItemID(string strWhere)
        {
            string strQuery = "Select OrderItemId " +
                "From trOrderItemDetail " +
                "where " +
                strWhere;

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsFrom.Tables[0].Rows[0]["OrderItemId"].ToString());
            }
            return 0;
        }

        public int GetOrderItemDetailExctQty(string strWhere)
        {
            string strQuery = "select Quantity from trOrderItemDetail where "
                + strWhere;

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsFrom.Tables[0].Rows[0]["Quantity"].ToString());
            }
            return 0;
        }




        public int GetTotalQtyForOrderNumAndItemIdFromContItemDetl(string orderNum, string orderItemId)
        {
            string strQuery = "SELECT sum(qtyincontainer)Quantity from trcontaineritemdetail c " +
                               " inner join trOrderItemDetail i on c.RefOrderItemDetail = i.OrderItemId " +
                               " inner join trOrderdetail d on c.OrderDetailId = d.OrderDetailId where i.OrderItemId = '" + orderItemId + "'" + " and d.OrderNo = '" + orderNum + "'";


            DataSet dsFrom = m_objDBMgr.GetInTransDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                if (dsFrom.Tables[0].Rows[0]["Quantity"].ToString() != null && dsFrom.Tables[0].Rows[0]["Quantity"].ToString() != string.Empty)
                {
                    return Convert.ToInt32(dsFrom.Tables[0].Rows[0]["Quantity"].ToString());
                }
            }
            return 0;
        }


        public int GetContItemDetailQtyForOrderNumAndItemId(string orderDetailId, string orderItemId)
        {
            string strQuery = "select QtyInContainer from trContainerItemDetail where RefOrderItemDetail='" + orderItemId + "'" + " and OrderDetailId='" + orderDetailId + "'";

            DataSet dsFrom = m_objDBMgr.GetInTransDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                if (dsFrom.Tables[0].Rows[0]["QtyInContainer"].ToString() != null && dsFrom.Tables[0].Rows[0]["QtyInContainer"].ToString() != string.Empty)
                {
                    return Convert.ToInt32(dsFrom.Tables[0].Rows[0]["QtyInContainer"].ToString());
                }
            }
            return 0;
        }


        public void GenerateAlert(int ruleId, string violationComment, DateTime timeStamp, string operatorName,
                              int zoneID, string primaryId, string secondaryIds)
        {

            int errorCode = 0;
            int violationID = 0;

            try
            {
                SqlParameter[] sqlParams = new SqlParameter[13];
                SqlParameter sqlParam = null;


                sqlParam = new SqlParameter("@iRuleID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ruleId);
                sqlParams[0] = sqlParam;

                sqlParam = new SqlParameter("@lViolationID", SqlDbType.BigInt, 8, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, violationID);
                sqlParams[1] = sqlParam;

                sqlParam = new SqlParameter("@sViolationComment", SqlDbType.VarChar, 1000, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, violationComment);
                sqlParams[2] = sqlParam;

                sqlParam = new SqlParameter("@daTimeStamp", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, timeStamp);
                sqlParams[3] = sqlParam;

                sqlParam = new SqlParameter("@sOperator", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, operatorName);
                sqlParams[4] = sqlParam;

                sqlParam = new SqlParameter("@iResourceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, 0);
                sqlParams[5] = sqlParam;


                sqlParam = new SqlParameter("@sPrimaryID", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, primaryId);
                sqlParams[6] = sqlParam;

                sqlParam = new SqlParameter("@sSecondaryID", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, secondaryIds);
                sqlParams[7] = sqlParam;

                sqlParam = new SqlParameter("@bIsAcknowledged", SqlDbType.Bit, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, false);
                sqlParams[8] = sqlParam;

                sqlParam = new SqlParameter("@sAcknowledgedBy", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, string.Empty);
                sqlParams[9] = sqlParam;

                sqlParam = new SqlParameter("@daAcknowledgedTime", SqlDbType.DateTime, 30, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, DateTime.Now);
                sqlParams[10] = sqlParam;

                sqlParam = new SqlParameter("@sAcknowledgeComments", SqlDbType.VarChar, 1000, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, string.Empty);
                sqlParams[11] = sqlParam;

                sqlParam = new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, errorCode);
                sqlParams[12] = sqlParam;

                m_objDBMgr.ExecuteSP(sqlParams, "[pr_Violations_Insert]", false);

            }
            catch (Exception ex)
            {
                log.Error("ImportUtil:GenerateAlert=>" + ex.Message, ex);
            }

        }





        public DataTable GetAllOrderItemDetails(string orderNo)
        {
            string strSQL = "SELECT * FROM trOrderItemDetail WHERE OrderNo='" + orderNo + "'";
            DataTable dt = m_objDBMgr.GetInTransDataSet(strSQL).Tables[0];
            return dt;

        }
        #region OrderDetail Import

        #region Utility Function

        #region [ Import ProductCatalog ]

        public int NextVal(string sSequenceName)
        {
            try
            {
                return m_objDBMgr.GetNextVal(sSequenceName);
            }
            catch (Exception e)
            {

                throw new Exception("Cannot generate the nextval" + e.Message);
            }
        }

        public int GetUnitofMeasureCode(string name)
        {
            string strQuery = "Select UnitofMeasureCode " +
                "From trUnitofMeasure " +
                "Where Name='" + name + "'";

            DataSet dsCarrier = m_objDBMgr.GetDataSet(strQuery);

            if (dsCarrier.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsCarrier.Tables[0].Rows[0]["UnitofMeasureCode"].ToString());
            }
            return -1;
        }

        public long GetproductID(string name)
        {
            string strQuery = "Select ProductID " +
                "From ProductMaster " +
                "Where ProductPrefix='" + name + "'";

            DataSet dsCarrier = m_objDBMgr.GetDataSet(strQuery);

            if (dsCarrier.Tables[0].Rows.Count > 0)
                return Convert.ToInt32(dsCarrier.Tables[0].Rows[0]["ProductID"].ToString());
            else
                return -1;
        }

        public long GetItemID(string name)
        {
            string strQuery = "Select ID " +
                "From ItemMaster " +
                "Where CustomerUniqueId='" + name + "'";

            DataSet dsCarrier = m_objDBMgr.GetDataSet(strQuery);

            if (dsCarrier.Tables[0].Rows.Count > 0)
                return Convert.ToInt32(dsCarrier.Tables[0].Rows[0]["ID"].ToString());
            else
                return -1;
        }

        public bool IsProductSkuUnique(string ProductSku, out int skuId)
        {
            bool exist = false;
            string strQuery = "Select SKUMasterID " +
                "From trsku_Master " +
                "Where ProductSKU='" + ProductSku + "'";

            DataSet dsSKU = m_objDBMgr.GetDataSet(strQuery);

            if (dsSKU.Tables[0].Rows.Count > 0)
            {
                skuId = Convert.ToInt32(dsSKU.Tables[0].Rows[0]["skuMasterID"].ToString());
                exist = false;
            }
            else
            {
                skuId = NextVal("trSKU_Master");
                exist = true;
            }
            return exist;
        }

        public bool GetCompanyMasterId(string companyPrefix, out int companyId)
        {
            companyId = -1;
            string strQuery = "Select CompanyId " +
                "From CompanyMaster " +
                "Where CompanyPrefix='" + companyPrefix + "'";

            DataSet dsCarrier = m_objDBMgr.GetDataSet(strQuery);

            if (dsCarrier.Tables[0].Rows.Count > 0)
            {
                companyId = Convert.ToInt32(dsCarrier.Tables[0].Rows[0]["CompanyId"].ToString());
                return true;
            }
            else
                // companyId = NextVal(@"CompanyMaster");
                //string str = "SELECT IDENT_CURRENT('" + "CompanyMaster" + "')+ IDENT_INCR('" + "CompanyMaster" + "') as CompanyId";
                //DataSet ds = m_objDBMgr.GetDataSet(str);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    companyId = Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyId"].ToString());
                //    return true;
                //}
                //else
                return false;


        }

        public bool GetProductMasterId(Hashtable whereHash, out int productId)
        {
            productId = -1;
            string whereCond = GetWhereString(whereHash);
            string strQuery = @"Select ProductId From ProductMaster " +
                @"Where " + whereCond;
            //CompanyMasterRef='" + companyId + @"' and [ProductPrefix]='" + productPrefix + "'" ;

            DataSet dsProductMaster = m_objDBMgr.GetDataSet(strQuery);

            if (dsProductMaster.Tables[0].Rows.Count > 0)
            {
                productId = Convert.ToInt32(dsProductMaster.Tables[0].Rows[0]["ProductId"].ToString());
                return true;
            }
            else

                //productId = NextVal(@"ProductMaster");
                //string str = "SELECT IDENT_CURRENT('" + "ProductMaster" + "')+ IDENT_INCR('" + "ProductMaster" + "') as ProductID";
                //DataSet ds = m_objDBMgr.GetDataSet(str);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    productId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductID"].ToString());
                //    return true;
                //}
                //else
                return false;
        }

        public bool GetItemMasterId(Hashtable whereHash, out long ID)
        {
            ID = -1;
            string whereCond = GetWhereString(whereHash);
            string strQuery = @"Select ID From ItemDetails " +
                @"Where " + whereCond;
            DataSet dsProductMaster = m_objDBMgr.GetDataSet(strQuery);

            if (dsProductMaster.Tables[0].Rows.Count > 0)
            {
                ID = Convert.ToInt32(dsProductMaster.Tables[0].Rows[0]["ID"].ToString());
                return true;
            }
            else

                return false;
        }


        #endregion [ Import Product Catalog ]

        private string GetWhereString(Hashtable fromHash)
        {
            string strSQL = "";
            foreach (string strKey in fromHash.Keys)
            {
                strSQL += "LTRIM(RTRIM([" + strKey + "])) ='" + fromHash[strKey].ToString().Replace("'", "''").Trim() + "' AND ";
            }
            strSQL = strSQL.Trim();
            if (strSQL.EndsWith("AND"))
                strSQL = strSQL.Substring(0, strSQL.Length - 3);
            return strSQL;
        }


        private string GetInsertSQL(Hashtable fieldValueHash, string tableName)
        {
            string fieldCol = string.Empty;
            string fieldVal = string.Empty;

            foreach (string fieldName in fieldValueHash.Keys)
            {
                if (fieldName.Trim() == "")
                    continue;

                fieldCol += "[" + fieldName + "],";
                fieldVal += "'" + fieldValueHash[fieldName].ToString().Trim().Replace("'", "''") + "',";
            }
            if (fieldCol.Length > 0 && fieldCol.EndsWith(","))
                fieldCol = fieldCol.Substring(0, fieldCol.Length - 1);
            if (fieldVal.Length > 0 && fieldVal.EndsWith(","))
                fieldVal = fieldVal.Substring(0, fieldVal.Length - 1);

            string strSQL = "INSERT INTO [" + tableName + "] (" + fieldCol + ") VALUES (" +
                fieldVal + ")";
            return strSQL;
        }


        private string GetUpdateSQL(Hashtable fieldValueHash, string tableName, string strWhrCond)
        {
            string fieldColVal = string.Empty;

            foreach (string fieldName in fieldValueHash.Keys)
            {
                if (fieldName.Trim() == "")
                    continue;

                fieldColVal += fieldName + " = ";
                fieldColVal += "'" + fieldValueHash[fieldName].ToString().Trim().Replace("'", "''") + "',";
            }

            if (fieldColVal.Length > 0 && fieldColVal.EndsWith(","))
                fieldColVal = fieldColVal.Substring(0, fieldColVal.Length - 1);

            string strSQL = "UPDATE [" + tableName + "] SET " + fieldColVal + " WHERE " + strWhrCond;
            return strSQL;
        }


        #endregion Utility Function


        /// <summary>
        /// Returns shipfrom address ID,  If 0 means no corresponding address found
        /// </summary>
        /// <param name="fromHash"></param>
        /// <returns></returns>
        public int GetShipFromId(Hashtable whereCondHash)
        {
            if (whereCondHash.Count <= 0)
                return 0;
            string strWhere = GetWhereString(whereCondHash);

            string strQuery = "Select ShipFrom.ShipFromAddressId " +
                "From trOrders as ShipFrom " +
                "where " +
                strWhere;

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsFrom.Tables[0].Rows[0]["ShipFromAddressId"].ToString());
            }
            return 0;
        }


        /// <summary>
        /// Returns shipfrom address ID,  If 0 means no corresponding address found
        /// </summary>
        /// <param name="fromHash"></param>
        /// <returns></returns>
        public int GetCarrierId(Hashtable whereCondHash)
        {
            if (whereCondHash.Count <= 0)
                return 0;
            string strWhere = GetWhereString(whereCondHash);

            string strQuery = "Select ShipFrom.CarrierAddressId " +
                "From trOrders as ShipFrom " +
                "where " +
                strWhere;

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsFrom.Tables[0].Rows[0]["CarrierAddressId"].ToString());
            }
            return 0;
        }
        /// <summary>
        /// Returns shipTo address ID,  If 0 means no corresponding address found
        /// </summary>
        /// <param name="fromHash"></param>
        /// <returns></returns>
        public int GetShipToId(Hashtable whereCondHash)
        {
            string strWhere = GetWhereString(whereCondHash);
            string strQuery = "Select ShipTo.ShipToAddressId " +
                "From trOrders as ShipTo Where " +
                strWhere;

            DataSet dsTo = m_objDBMgr.GetDataSet(strQuery);

            if (dsTo.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsTo.Tables[0].Rows[0]["ShipToAddressId"].ToString());
            }

            return 0;
        }

        /// <summary>
        /// Returns shipTo address ID,  If 0 means no corresponding address found
        /// </summary>
        /// <param name="fromHash"></param>
        /// <returns></returns>
        public bool GetShipToId(Hashtable whereCondHash, out int shipToId)
        {
            shipToId = 0;
            bool IsRecExist = false;
            string strWhere = "globallocationno ='" + whereCondHash["StoreNo"].ToString() + "'"; //GetWhereString(whereCondHash);
            string strQuery = "Select ShipTo.ShipToAddressId " +
                "From trShipToAddress as ShipTo Where " +
                strWhere;

            DataSet dsTo = m_objDBMgr.GetDataSet(strQuery);

            if (dsTo.Tables[0].Rows.Count > 0)
            {
                shipToId = Convert.ToInt32(dsTo.Tables[0].Rows[0]["ShipToAddressId"].ToString());
                IsRecExist = true;
            }
            else if (dsTo.Tables[0].Rows.Count == 0)
            {
                strWhere = GetWhereString(whereCondHash);
                strQuery = "Select ShipTo.ShipToAddressId " +
                "From trShipToAddress as ShipTo Where " +
                strWhere;

                dsTo = m_objDBMgr.GetDataSet(strQuery);
                if (dsTo.Tables[0].Rows.Count > 0)
                {
                    shipToId = Convert.ToInt32(dsTo.Tables[0].Rows[0]["ShipToAddressId"].ToString());
                    IsRecExist = true;
                }
                else
                {
                    shipToId = NextVal("trShipToAddress");
                    IsRecExist = false;
                }
            }

            return IsRecExist;
        }

        /// <summary>
        /// Returns shipTo address ID,  If 0 means no corresponding address found
        /// </summary>
        /// <param name="fromHash"></param>
        /// <returns></returns>
        public bool GetShipFromId(Hashtable whereCondHash, out int shipToId)
        {
            string strWhere = GetWhereString(whereCondHash);
            string strQuery = "Select ShipTo.ShipFromAddressId " +
                "From trShipFromAddress as ShipTo Where " +
                strWhere;

            DataSet dsTo = m_objDBMgr.GetDataSet(strQuery);

            if (dsTo.Tables[0].Rows.Count > 0)
            {
                shipToId = Convert.ToInt32(dsTo.Tables[0].Rows[0]["ShipFromAddressId"].ToString());
                return true;
            }
            else
                shipToId = NextVal("trShipFromAddress");
            return false;
        }

        public bool GetContainerShipToID(Hashtable whereCondHash, out int shipToId)
        {
            shipToId = 0;
            bool IsRecExist = false;
            string strWhere = "OrderNo ='" + whereCondHash["OrderNo"].ToString() + "'"; //GetWhereString(whereCondHash);
            string strQuery = "Select Orders.ShipToAddressId " +
                "From trOrders as Orders Where " +
                strWhere;

            DataSet dsTo = m_objDBMgr.GetDataSet(strQuery);

            if (dsTo.Tables[0].Rows.Count > 0)
            {
                shipToId = Convert.ToInt32(dsTo.Tables[0].Rows[0]["ShipToAddressId"].ToString());
                IsRecExist = true;
            }


            return IsRecExist;

        }


        public int GetCompanyMasterId(Hashtable whereCondHash)
        {
            int companyId = -1;

            string strWhere = GetWhereString(whereCondHash);

            string strQuery = "Select CompanyId " +
                "From trCompanyMaster Where " + strWhere;


            DataSet dsCarrier = m_objDBMgr.GetDataSet(strQuery);

            if (dsCarrier.Tables[0].Rows.Count > 0)
            {
                companyId = Convert.ToInt32(dsCarrier.Tables[0].Rows[0]["CompanyId"].ToString());

            }
            return companyId;
        }


        public string GetOrderSalesPONo(Hashtable hashobjt)
        {
            string strWhere = GetWhereString(hashobjt);
            string strQuery = "Select SalesPONo " +
                "From trOrders  Where " +
                strWhere;

            DataSet dsTo = m_objDBMgr.GetDataSet(strQuery);

            if (dsTo.Tables[0].Rows.Count > 0)
            {
                return dsTo.Tables[0].Rows[0]["SalesPONo"].ToString();
            }

            return "";
        }
        public bool AddRecord(string tableName, Hashtable fieldValueHash, Hashtable duplicateCondHash)
        {
            try
            {
                if (duplicateCondHash.Count > 0)
                {
                    string duplicateCheck = GetWhereString(duplicateCondHash);
                    if (IsRecordExist(duplicateCheck, tableName))
                        throw new ApplicationException("This Address already exists.");
                }
                string strSQL = GetInsertSQL(fieldValueHash, tableName);
                m_objDBMgr.ExecuteSQL(strSQL, true);
            }
            catch (Exception ex)
            {
                m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::AddShipToRecord=>" + ex.Message, ex);
                throw ex;
                return false;
            }
            return true;
        }


        public int GetCarriedAddressId(string columnName, string ColumnVal)
        {
            string strQuery = "Select CarrierAddressId " +
                "From trCarrierAddress " +
                "Where LTRIM(RTRIM(" + columnName + ")) ='" + ColumnVal.Trim() + "'";

            DataSet dsCarrier = m_objDBMgr.GetDataSet(strQuery);

            if (dsCarrier.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsCarrier.Tables[0].Rows[0]["CarrierAddressId"].ToString());
            }
            return 0;
        }



        public int GetCarriedAddressId(string ColumnVal)
        {
            string strQuery = "Select CarrierAddressId " +
                "From trCarrierAddress " +
                "Where LTRIM(RTRIM(CustomCode)) ='" + ColumnVal.Trim() + "'";

            DataSet dsCarrier = m_objDBMgr.GetDataSet(strQuery);

            if (dsCarrier.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsCarrier.Tables[0].Rows[0]["CarrierAddressId"].ToString());
            }
            return 0;
        }


        public bool AddOrderRecord(Hashtable fieldValueHash, Hashtable duplicateCondHash)
        {
            try
            {
                if (duplicateCondHash.Count > 0)
                {
                    string duplicateCheck = GetWhereString(duplicateCondHash);
                    if (IsRecordExist(duplicateCheck, "trOrders"))
                        throw new ApplicationException("This Order already exists.");
                }
                string strSQL = GetInsertSQL(fieldValueHash, "trOrders");
                m_objDBMgr.ExecuteSQL(strSQL, true);
            }
            catch (Exception ex)
            {
                m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::UpdateRetailLinkData=>" + ex.Message, ex);
                return false;
            }
            return true;
        }


        public bool AddOrderDetailRecord(Hashtable fieldValueHash, Hashtable duplicateCondHash)
        {
            try
            {
                if (duplicateCondHash.Count > 0)
                {
                    string duplicateCheck = GetWhereString(duplicateCondHash);
                    if (IsRecordExist(duplicateCheck, "trOrderDetail"))
                        throw new ApplicationException("This entry already exists.");
                }
                string strSQL = GetInsertSQL(fieldValueHash, "trOrderDetail");
                m_objDBMgr.ExecuteSQL(strSQL, true);
            }
            catch (Exception ex)
            {
                m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::UpdateRetailLinkData=>" + ex.Message, ex);
                return false;
            }
            return true;
        }


        public bool ModifyOrderRecord(Hashtable fieldValueHash, Hashtable whereCondHash)
        {
            try
            {
                string strWhere = "";
                if (whereCondHash.Count > 0)
                    strWhere = GetWhereString(whereCondHash);
                string strSQL = GetUpdateSQL(fieldValueHash, "trOrders", strWhere);
                m_objDBMgr.ExecuteSQL(strSQL, true);
            }
            catch (Exception ex)
            {
                //m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::ModifyOrderRecord" + ex.Message, ex);
                return false;
            }
            return true;
        }


        public bool ModifyOrderDetailRecord(Hashtable fieldValueHash, Hashtable whereCondHash)
        {
            try
            {
                //IsVerified is not to be modified
                fieldValueHash.Remove("IsVerified");

                string strWhere = "";
                if (whereCondHash.Count > 0)
                    strWhere = GetWhereString(whereCondHash);
                string strSQL = GetUpdateSQL(fieldValueHash, "trOrderDetail", strWhere);
                m_objDBMgr.ExecuteSQL(strSQL, true);
            }
            catch (Exception ex)
            {
                m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::UpdateRetailLinkData=>" + ex.Message, ex);
                return false;
            }
            return true;
        }



        #endregion OrderDetail Import

        #region AssetDetailImport
        public string GetAssetClassCode(string className, string categoryCode)
        {
            string classCode = string.Empty;
            try
            {

                string sql = "select classcode from class where classname = '"
                                 + className + "' and categorycode = '" + categoryCode + "'";
                DataSet dsClass = m_objDBMgr.GetDataSet(sql);
                if (dsClass != null && dsClass.Tables.Count > 0 && dsClass.Tables[0].Rows.Count > 0)
                {
                    classCode = dsClass.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    classCode = NextVal("Class").ToString();
                    Hashtable classHash = new Hashtable();
                    classHash["CategoryCode"] = categoryCode;
                    classHash["ClassName"] = className;
                    classHash["ClassCode"] = classCode;
                    string sqlstr = GetInsertSQL(classHash, "Class");
                    m_objDBMgr.ExecuteSQL(sqlstr, true);

                }

            }
            catch (Exception ex)
            {
                log.Error("DBTransact::GetAssetClassCode=>" + ex.Message, ex);
            }
            return classCode;
        }

        public string GetAssetCategoryCode(string categoryName)
        {
            string catCode = string.Empty;
            try
            {


                string sql = "select categorycode from category where categoryname = '"
                                 + categoryName + "'";
                DataSet dsCategory = m_objDBMgr.GetDataSet(sql);
                if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
                {
                    catCode = dsCategory.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    catCode = NextVal("Category").ToString();
                    Hashtable catHash = new Hashtable();
                    catHash["CategoryCode"] = catCode;
                    catHash["CategoryName"] = categoryName;

                    string sqlstr = GetInsertSQL(catHash, "Category");
                    m_objDBMgr.ExecuteSQL(sqlstr, true);

                }

            }
            catch (Exception ex)
            {
                log.Error("DBTransact::GetAssetCategoryCode=>" + ex.Message, ex);
            }
            return catCode;
        }

        public string GetAssetID(string strWhere)
        {
            string assetId = string.Empty;
            string strQuery = "Select AssetID " +
                "From Asset " +
                "where " +
                strWhere;

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                assetId = dsFrom.Tables[0].Rows[0][0].ToString();
            }
            return assetId;
        }

        public bool IsAssetLiveDeleted(string strWhrCond)
        {
            string strSQL = "SELECT * FROM AssetMaster WHERE " + strWhrCond + " AND ISACTIVE =1 AND ISDELETED = 0";
            if (m_objDBMgr.GetDataSet(strSQL).Tables[0].Rows.Count > 0)
                return true;
            return false;
        }

        public bool IsLocationCodeExist(int locationCode)
        {
            bool isExist = false;

            string strSQL = "SELECT * FROM trLocationType WHERE LocationCode='" + locationCode + "'";
            if (m_objDBMgr.GetDataSet(strSQL).Tables[0].Rows.Count > 0)
            {
                isExist = true;
            }
            else
            {
                strSQL = "INSERT INTO  trLocationType (LocationCode,LocationDesc) VALUES (" + locationCode + "," + "'" + "Location " + locationCode + "'" + ")";
                m_objDBMgr.ExecuteSQL(strSQL, true);
                isExist = false;
            }

            return isExist;
        }

        public bool IsCustomerUniqueIDExist(string UniqueId)
        {
            bool isExist = false;

            string strSQL = "SELECT * FROM ItemMaster WHERE CustomerUniqueId='" + UniqueId + "'";
            if (m_objDBMgr.GetDataSet(strSQL).Tables[0].Rows.Count > 0)
            {
                isExist = true;
            }
            else
            {
                isExist = false;
            }

            return isExist;
        }


        public int GetCompanyID(string whr)
        {
            int companyId = -1;
            string strQuery = "Select CompanyId " +
                "From  CompanyMaster " +
                "where " +
                whr;

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                companyId = Convert.ToInt32(dsFrom.Tables[0].Rows[0][0]);
            }
            return companyId;
        }

        public int GetDataOwnerID(string whr)
        {
            int DataOwnerID = -1;
            string strQuery = "Select DataOwnerID " +
                "From DataOwner " +
                "where " + whr;

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                DataOwnerID = Convert.ToInt32(dsFrom.Tables[0].Rows[0][0]);
            }
            return DataOwnerID;
        }
        public int GetPackageID(string whr)
        {
            int PackageID = -1;
            string strQuery = "Select PackageID  " +
                "From PackagingType " +
                "where " + whr;

            DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                PackageID = Convert.ToInt32(dsFrom.Tables[0].Rows[0][0]);
            }
            return PackageID;
        }

        public string GetProductNameCount(long ProductID, int PackageID)
        {
       
            string strQuery = " SELECT P.ProductName   FROM SKUMaster S " +
                              " INNER JOIN ProductMaster P ON P.ProductID =S.ProdcutID  " +
                "where S.ProdcutID = " + ProductID + 
                "and S.PackageID = " + PackageID ;
            
               
             DataSet dsFrom = m_objDBMgr.GetDataSet(strQuery);
          
            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return Convert.ToString(dsFrom.Tables[0].Rows[0][0]);
            }

            return null;
        }
        public int GetUserID(string whr)
        {
            int UserID = -1;
            //string strQuery = "Select UserID " +
            //    "From [User] " +
            //    "where " + whr;

            string strquery = "Select [User].UserID from [User] inner join [Role] on [User].RoleID =[Role].RoleID " +
                              " where [User].[Default] = 1  and " + whr;

            DataSet dsFrom = m_objDBMgr.GetDataSet(strquery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                UserID = Convert.ToInt32(dsFrom.Tables[0].Rows[0][0]);
            }
            return UserID;
        }
        public bool UpdateImportAsset(TableInfo[] tableList, Hashtable fieldValueHash, bool useTransaction, out bool recExist)
        {
            string strSQL = string.Empty;
            string strWhrCond = string.Empty;
            recExist = false;
            bool customRecExist = false;
            try
            {
                if (useTransaction)
                    m_objDBMgr.BiginTrans();
                for (int tblCnt = 0; tblCnt < tableList.Length; tblCnt++)
                {
                    string assetID1 = "0";
                    strWhrCond = GetWhereCond(tableList[tblCnt].fieldDetails, fieldValueHash);

                    DataSet dsAsset = m_objDBMgr.GetDataSet("Select AssetID, IsActive, ISDeleted from AssetMaster where " + strWhrCond);
                    if (dsAsset != null && dsAsset.Tables.Count > 0 && dsAsset.Tables[0].Rows.Count > 0)
                    {
                        assetID1 = dsAsset.Tables[0].Rows[0]["AssetID"].ToString();
                        if (assetID1 != "")
                        {
                            bool isActive = Convert.ToBoolean(dsAsset.Tables[0].Rows[0]["IsActive"]);
                            bool isDeleted = Convert.ToBoolean(dsAsset.Tables[0].Rows[0]["ISDeleted"]);
                            if (isActive == false || isDeleted == true)
                                throw new ApplicationException("Cannot update the supplied asset as its in disabled/inactive state.");
                            recExist = true;
                        }
                        else
                            recExist = false;
                    }
                    else
                    {
                        recExist = false;
                    }
                    // recExist = IsRecordExist(strWhrCond, tableList[tblCnt].tableName);

                    if (recExist)
                    {
                        if (!tableList[0].updateData)
                            continue;
                        if (fieldValueHash.ContainsKey("CustomTableInfo"))
                        {
                            customRecExist = IsRecordExist(" ASSETID = " + assetID1, "AssetMasterCustom");
                        }

                        strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);
                        int recUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                        //if (useTransaction)
                        //    m_objDBMgr.CommitTrans();
                        //continue;



                        if (fieldValueHash.ContainsKey("CustomTableInfo"))
                        {
                            // bool recExist1 = IsRecordExist(" ASSETID in (Select AssetID from AssetMaster where " + strWhrCond + " ) ", "AssetMasterCustom");
                            if (customRecExist)
                            {
                                List<string> CustomCols = m_objDBMgr.GetColumns("AssetMasterCustom", true);
                                string strCols = "";
                                foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                {
                                    if (CustomCols.Contains(custom.Key.ToUpper()))
                                    {
                                        strCols += " " + custom.Key + " = '" + custom.Value + "' ,";
                                    }
                                }
                                if (strCols.Trim().EndsWith(","))
                                    strCols = strCols.Trim().Substring(0, strCols.Trim().Length - 1);
                                if (!strCols.Trim().Equals(string.Empty))
                                {
                                    strSQL = "UPDATE AssetMasterCustom SET  " + strCols + " WHERE ASSETID = " + assetID1;

                                    recUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                                }
                            }
                            else
                            {
                                string strCols = "";
                                string strVals = "";
                                List<string> CustomCols = m_objDBMgr.GetColumns("AssetMasterCustom", true);

                                foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                                {
                                    if (CustomCols.Contains(custom.Key.ToUpper()))
                                    {
                                        strCols += ", " + custom.Key;
                                        strVals += ", '" + custom.Value + "'";
                                    }
                                }

                                strSQL = "Insert into AssetMasterCustom ( AssetID  " + strCols + " ) values (" + assetID1 + strVals + " ) ";
                                recUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                            }
                        }


                    }
                    else
                    {

                        strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);

                        assetID1 = m_objDBMgr.ExecuteInsertStatment(strSQL, true);

                        if (fieldValueHash.ContainsKey("CustomTableInfo"))
                        {

                            string strCols = "";
                            string strVals = "";
                            List<string> CustomCols = m_objDBMgr.GetColumns("AssetMasterCustom", true);

                            foreach (KeyValuePair<string, string> custom in (Dictionary<string, string>)fieldValueHash["CustomTableInfo"])
                            {
                                if (CustomCols.Contains(custom.Key.ToUpper()))
                                {
                                    strCols += ", " + custom.Key;
                                    strVals += ", '" + custom.Value + "'";
                                }
                            }

                            strSQL = "Insert into AssetMasterCustom ( AssetID  " + strCols + " ) values (" + assetID1 + strVals + " ) ";
                            int recIns = m_objDBMgr.ExecuteSQL(strSQL, true);
                        }


                    }

                    log.Trace("DBTransact::UpdateImportAsset:SQL => " + strSQL);


                }
                if (useTransaction)
                    m_objDBMgr.CommitTrans();
                return true;
            }
            catch (Exception ex)
            {
                if (useTransaction)
                    m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::UpdateImportAsset=>" + ex.Message, ex);
                throw ex;


            }
        }

        public List<string> GetMandatoryCustomColName(string AssetType)
        {
            string strSQL = "SELECT CUSTOMCOLNAME FROM ASSETTYPECUSTOMCOL  AS ATCC " +
                            " INNER JOIN ASSETTYPEMASTER AS ATM ON ATM.ASSETTYPEMASTERID = ATCC.ASSETTYPEMASTERID " +
                            " WHERE ATM.NAME = '" + AssetType + "' AND ATCC.ISMANDATORY = 1";
            DataTable dt = m_objDBMgr.GetInTransDataSet(strSQL).Tables[0];
            List<string> lstCustCol = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                lstCustCol.Add(dr[0].ToString().ToUpper());
            }
            return lstCustCol;

        }

        public List<string> GetSDCMandatoryCustomColName(string TableName, int datownerid)
        {
            string strSQL = "SELECT CUSTOMCOLNAME FROM USERDEFINEDFIELD  AS UDF " +
                            " INNER JOIN CustomFieldCatagory AS CFC ON CFC.CATEGORYID = UDF.CATEGORYID " +
                            " WHERE CFC.TABLENAME = '" + TableName + "' AND UDF.ISMANDATORY = 1 AND UDF.DataOwnerID='" + datownerid + "'";
            DataTable dt = m_objDBMgr.GetInTransDataSet(strSQL).Tables[0];
            List<string> lstCustCol = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                lstCustCol.Add(dr[0].ToString().ToUpper());
            }
            return lstCustCol;

        }

        public List<string> CheckSDCCustomColName(string TableName, int datownerid)
        {
            string strSQL = "SELECT  UPPER(CUSTOMCOLNAME) FROM USERDEFINEDFIELD  AS UDF " +
                            " INNER JOIN CustomFieldCatagory AS CFC ON CFC.CATEGORYID = UDF.CATEGORYID " +
                            " WHERE CFC.TABLENAME = '" + TableName + "' AND UDF.DataOwnerID='" + datownerid + "'";
            DataTable dt = m_objDBMgr.GetInTransDataSet(strSQL).Tables[0];
            List<string> lstCustCol = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                lstCustCol.Add(dr[0].ToString().ToUpper());
            }
            return lstCustCol;

        }

        #endregion AssetDetailImport

        public void ExecuteStoredProcedures(string SPName)
        {
            m_objDBMgr.CloseConnection();

            m_objDBMgr.ExecuteSP(null, SPName, true);
        }


        public int GetOrderItemIDForOrderImport(string strWhere)
        {
            string strQuery = "Select OrderItemId " +
               "From trOrderItemDetail " +
               "where " +
               strWhere;

            DataSet dsFrom = m_objDBMgr.GetInTransDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsFrom.Tables[0].Rows[0]["OrderItemId"].ToString());
            }
            return 0;
        }




        public bool UpdateOrderImportData(TableInfo[] tableList, Hashtable fieldValueHash, bool useTransaction, bool IsOverrideOrderItemQty)
        {
            string strSQL = string.Empty;
            string strWhrCond = string.Empty;
            bool recExist = false;
            bool isInsertQry = false;
            bool isRecProc = true;
            //bool isRecExistinOrderDetail = false;
            bool isContainarised = false;
            DataTable dtRecords = null;
            isContainarised = false;

            try
            {

                if (useTransaction)
                    m_objDBMgr.BiginTrans();
                for (int tblCnt = 0; tblCnt < tableList.Length; tblCnt++)
                {
                    strWhrCond = string.Empty;
                    recExist = false;
                    strSQL = string.Empty;
                    isInsertQry = false;
                    dtRecords = null;

                    switch (tableList[tblCnt].tableName)
                    {

                        case ORDER_DETAIL_TABLE:
                            strWhrCond = GetWhereCondForOrderDetail(tableList[tblCnt].fieldDetails, fieldValueHash, out isContainarised);
                            break;
                        case ORDER_CONTAINER_ITEMDETAIL_TABLE:
                            break;
                        default:
                            strWhrCond = GetWhereCond(tableList[tblCnt].fieldDetails, fieldValueHash);
                            break;
                    }
                    if (strWhrCond != string.Empty)
                    {
                        //dtRecords = GetRecords(strWhrCond, tableList[tblCnt].tableName);
                        dtRecords = GetInTransRecords(strWhrCond, tableList[tblCnt].tableName);
                        recExist = (dtRecords != null && dtRecords.Rows.Count > 0) ? true : false;
                    }
                    if (recExist)
                    {

                        switch (tableList[tblCnt].tableName)
                        {
                            case ORDER_TABLE:
                                strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);
                                break;


                            case ORDER_DETAIL_TABLE:
                                if (isContainarised)
                                {
                                    //isRecExistinOrderDetail = true;
                                    try
                                    {
                                        fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERDETAIL] =
                                            Convert.ToInt64(dtRecords.Rows[0][ORDER_DETAIL_TABLE_ID]);
                                    }
                                    catch { log.Error("DBTransact::UpdateImportOrderData:Error while geting OrderDetailID.dtRecords Null/Empty."); }
                                }
                                continue;
                            case ORDER_ITEM_DETAIL_TABLE:
                                // If not containerized, they we should only get one 
                                // SKU and its quantity in the order file, so overwrite with new qty

                                #region[FOR SINGLE FILE IMPORT]
                                //This condition is for a single file import
                                if (fieldValueHash["RefOrderItemDetail"].ToString() == null || fieldValueHash["RefOrderItemDetail"].ToString() == string.Empty)
                                {
                                    int RefOrderItemDetail = GetOrderItemIDForOrderImport(strWhrCond);
                                    fieldValueHash["RefOrderItemDetail"] = RefOrderItemDetail;
                                }
                                #endregion[FOR SINGLE FILE IMPORT]
                                if (!isContainarised)
                                {
                                    if (IsOverrideOrderItemQty)
                                    {
                                        log.Trace("DBTransact::UpdateImportOrderData:If IsOverrideOrderItemQty=true the existing qty in trOrderItemDetail will"
                                        + "be updated with new qty for that order number and item no but not added.All the rec will be as processed as processed rec.");
                                        strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);
                                        isRecProc = true;
                                    }
                                    else
                                    {
                                        log.Trace("DBTransact::UpdateImportOrderData:If IsOverrideOrderItemQty=false the existing qty in trOrderItemDetail will"
                                        + "be not updated with new qty for that order number and item no neither will be added.All the rec will be processed as error rec.");
                                        isRecProc = false;
                                    }
                                }
                                else if (!duplicateSKUinContainer(fieldValueHash["OrderDetailId"].ToString(), fieldValueHash["RefOrderItemDetail"].ToString()))
                                {
                                    log.Trace("DBTransact::UpdateImportOrderData:No Duplicate entry in trContinerItemDetails found hence processed as processed rec.");
                                    int prevQty = 0;
                                    try
                                    {

                                        prevQty = Convert.ToInt32(dtRecords.Rows[0][ORDER_ITEMDETAIL_TABLE_QUANTITY]);
                                        fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERITEMDETAIL] = Convert.ToInt64(dtRecords.Rows[0][ORDER_ITEMDETAIL_TABLE_ID]);
                                    }
                                    catch (Exception ex)
                                    {

                                        log.Error("DBTransact::UpdateImportOrderData:No previous qty present.");

                                        string msg = "DBTransact::UpdateImportOrderData:Error-->" + ex.Message;
                                        msg += Environment.NewLine + "DBTransact::UpdateImportOrderData:Stack Trace-->" + ex.StackTrace;

                                        if (ex.InnerException != null)
                                        {
                                            msg += Environment.NewLine + "DBTransact::UpdateImportOrderData:Inner Exception-->" + ex.InnerException.Message;
                                        }
                                        log.Error(msg, ex);
                                    }
                                    if (prevQty > 0)
                                    {

                                        int newQty = Convert.ToInt32(fieldValueHash[ORDER_ITEMDETAIL_TABLE_QUANTITY]);

                                        fieldValueHash[ORDER_ITEMDETAIL_TABLE_QUANTITY] = prevQty + newQty;
                                        strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);
                                        isInsertQry = false;
                                    }
                                    isRecProc = true;
                                }
                                else
                                {
                                    log.Trace("DBTransact::UpdateImportOrderData:Duplicate entry in trContinerItemDetails found hence processed as error rec.");
                                    isRecProc = false;
                                }

                                break;
                            default:
                                continue;
                        }



                    }
                    else //!recExist
                    {

                        if (tableList[tblCnt].tableName == ORDER_CONTAINER_ITEMDETAIL_TABLE)
                        {

                            if (fieldValueHash.ContainsKey(ORDER_DETAIL_TABLE_ID))
                            {
                                if (fieldValueHash[ORDER_DETAIL_TABLE_ID] != null && fieldValueHash[ORDER_DETAIL_TABLE_ID].ToString().Trim() != string.Empty)
                                {
                                    strWhrCond = CONTAINER_ITEM_TABLE_REFORDERDETAIL + " = " + fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERDETAIL] + " and "
                                                + CONTAINER_ITEM_TABLE_REFORDERITEMDETAIL + " = " + fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERITEMDETAIL];
                                    //recExist = IsRecordExist(strWhrCond, tableList[tblCnt].tableName);
                                    recExist = IsRecordExistInTrans(strWhrCond, tableList[tblCnt].tableName);
                                    if (recExist)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                                        isInsertQry = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                            isInsertQry = true;
                        }
                    }

                    log.Trace("DBTransact::UpdateImportOrderData:SQL => " + strSQL);
                    int recUpd = 0;

                    if (strSQL != string.Empty)
                    {
                        recUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                        if (isInsertQry)
                        {
                            if (tableList[tblCnt].tableName == ORDER_DETAIL_TABLE ||
                               tableList[tblCnt].tableName == ORDER_ITEM_DETAIL_TABLE)
                            {
                                try
                                {
                                    long id = 0;
                                    DataSet ds = m_objDBMgr.GetInTransDataSet("Select orderdetailID = SCOPE_IDENTITY()");

                                    if (ds != null && ds.Tables.Count > 0)
                                    {
                                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                                        {
                                            id = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                                            if (tableList[tblCnt].tableName == ORDER_DETAIL_TABLE)
                                                fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERDETAIL] = id;
                                            else
                                                fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERITEMDETAIL] = id;
                                        }
                                    }
                                }
                                catch { log.Error("DBTransact::UpdateImportOrderData:Error while getting SCOPE_IDENTITY"); }

                            }

                        }
                    }


                }
                if (useTransaction)
                    m_objDBMgr.CommitTrans();
                return isRecProc;

            }


            catch (Exception ex)
            {
                if (useTransaction)
                    m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::UpdateImportOrderData=>" + ex.Message);
                log.Error("DBTransact::UpdateImportOrderData:Stack trace=>" + ex.StackTrace);
                log.Error("DBTransact::UpdateImportOrderData:Target Site=>" + ex.TargetSite);
                log.Error("DBTransact::UpdateImportOrderData:Source=>" + ex.Source);
                if (ex.InnerException != null)
                    log.Error("DBTransact::UpdateImportOrderData=>:InnerException:" + ex.InnerException.Message);
                throw ex;
            }

        }


        private bool duplicateSKUinContainer(string orderDetailID, string itemDetailID)
        {

            string strSQL = "select OrderDetailId,RefOrderItemDetail,QtyInContainer  from trcontaineritemdetail where OrderDetailId='" + orderDetailID + "'" + " and RefOrderItemDetail='" + itemDetailID + "'";
            if (m_objDBMgr.GetDataSet(strSQL).Tables[0].Rows.Count > 0)
                return true;
            return false;
        }


        private int UpdateTemporderWithSAPOrder(string SAPorderNo, string tempOrderNo, int tempOrderDetailID, string epc_urn, string shippingTime)
        {

            try
            {

                SqlParameter[] sqlParams = new SqlParameter[6];
                SqlParameter sqlParam = null;


                sqlParam = new SqlParameter("@temporaryOrderNo", SqlDbType.VarChar);
                sqlParam.Value = tempOrderNo;
                sqlParams[0] = sqlParam;

                sqlParam = new SqlParameter("@tempOrderDetailId", SqlDbType.BigInt);
                sqlParam.Value = tempOrderDetailID;
                sqlParams[1] = sqlParam;

                sqlParam = new SqlParameter("@SAPorderno", SqlDbType.VarChar);
                sqlParam.Value = SAPorderNo;
                sqlParams[2] = sqlParam;

                sqlParam = new SqlParameter("@EPC_URN", SqlDbType.VarChar);
                sqlParam.Value = epc_urn;
                sqlParams[3] = sqlParam;

                if (shippingTime.Trim().Equals(string.Empty))
                {
                    sqlParam = new SqlParameter("@ShippingTime", SqlDbType.DateTime);
                    sqlParam.Value = SqlDateTime.Null;
                    sqlParams[4] = sqlParam;

                }
                else
                {
                    sqlParam = new SqlParameter("@ShippingTime", SqlDbType.DateTime);
                    sqlParam.Value = Convert.ToDateTime(shippingTime);
                    sqlParams[4] = sqlParam;
                }
                sqlParam = new SqlParameter("@iErrorCode", SqlDbType.Int);
                sqlParam.Direction = ParameterDirection.Output;
                sqlParam.Value = 0;
                sqlParams[5] = sqlParam;

                int result = m_objDBMgr.ExecuteSP(sqlParams, "sp_trorderdetail_UpdateTemporaryOrders", true);

                if (result != 0)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                log.Error("ImportUtil:UpdateTemporderWithSAPOrder=>" + ex.Message, ex);
            }
            return 0;
        }

        public int GetOrderItemQty(string strWhere)
        {
            string strQuery = "select Quantity from trOrderItemDetail where "
                + strWhere;

            DataSet dsFrom = m_objDBMgr.GetInTransDataSet(strQuery);

            if (dsFrom.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsFrom.Tables[0].Rows[0]["Quantity"].ToString());
            }
            return 0;
        }



        private DataTable GetEPCHistoryForSSCC(string epc_urn)
        {
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[1];
                SqlParameter sqlParam = null;


                sqlParam = new SqlParameter("@EPC_URN", SqlDbType.VarChar);
                sqlParam.Value = epc_urn;
                sqlParams[0] = sqlParam;

                DataTable dtFrom = m_objDBMgr.GetInTransDataTable("sp_EpcHistory_SelectShippedSSCCForImport", sqlParams);
                return dtFrom;

            }
            catch (Exception ex)
            {
                log.Error("ImportUtil:GetEPCHistoryForSSCC=>" + ex.Message, ex);
                return null;
            }

        }


        private int UpdateOrderDetailsIsShipped(long orderdetailID, string shippingTime)
        {

            try
            {

                SqlParameter[] sqlParams = new SqlParameter[3];
                SqlParameter sqlParam = null;


                sqlParam = new SqlParameter("@orderDetailId", SqlDbType.BigInt);
                sqlParam.Value = orderdetailID;
                sqlParams[0] = sqlParam;

                if (shippingTime.Trim().Equals(string.Empty))
                {
                    sqlParam = new SqlParameter("@shippingTime", SqlDbType.DateTime);
                    sqlParam.Value = SqlDateTime.Null;
                    sqlParams[1] = sqlParam;

                }
                else
                {
                    sqlParam = new SqlParameter("@shippingTime", SqlDbType.DateTime);
                    sqlParam.Value = Convert.ToDateTime(shippingTime);
                    sqlParams[1] = sqlParam;
                }
                sqlParam = new SqlParameter("@iErrorCode", SqlDbType.Int);
                sqlParam.Direction = ParameterDirection.Output;
                sqlParam.Value = 0;
                sqlParams[2] = sqlParam;

                int result = m_objDBMgr.ExecuteSP(sqlParams, "sp_trOrderDetail_UpdateOnOrderDetailId", true);

                if (result != 0)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                log.Error("ImportUtil:UpdateOrderDetailsIsShipped=>" + ex.Message, ex);
            }
            return 0;
        }



        public bool UpdateContainerDetlsImportData(TableInfo[] tableList, Hashtable fieldValueHash, bool useTransaction, bool OverrideQtyInContainer)
        {
            string strSQL = string.Empty;
            string strWhrCond = string.Empty;
            bool recExist = false;
            bool isInsertQry = false;
            bool isRecProc = false;
            bool isRecProcOrderDetail = false;
            //bool isRecExistinOrderDetail = false;
            bool isContainarised = false;
            DataTable dtRecords = null;
            try
            {
                if (useTransaction)
                    m_objDBMgr.BiginTrans();
                for (int tblCnt = 0; tblCnt < tableList.Length; tblCnt++)
                {
                    strWhrCond = string.Empty;
                    recExist = false;
                    strSQL = string.Empty;
                    isInsertQry = false;
                    dtRecords = null;
                    isContainarised = false;
                    switch (tableList[tblCnt].tableName)
                    {

                        case ORDER_DETAIL_TABLE:
                            strWhrCond = GetWhereCondForOrderDetail(tableList[tblCnt].fieldDetails, fieldValueHash, out isContainarised);
                            break;
                        case ORDER_CONTAINER_ITEMDETAIL_TABLE:
                            break;
                        default:
                            strWhrCond = GetWhereCond(tableList[tblCnt].fieldDetails, fieldValueHash);
                            break;
                    }
                    if (strWhrCond != string.Empty)
                    {
                        //dtRecords = GetRecords(strWhrCond, tableList[tblCnt].tableName);
                        dtRecords = GetInTransRecords(strWhrCond, tableList[tblCnt].tableName);
                        recExist = (dtRecords != null && dtRecords.Rows.Count > 0) ? true : false;
                    }
                    if (recExist)
                    {

                        switch (tableList[tblCnt].tableName)
                        {
                            case ORDER_DETAIL_TABLE:
                                if (isContainarised)
                                {
                                    //isRecExistinOrderDetail = true;
                                    try
                                    {

                                        if (dtRecords != null && dtRecords.Rows.Count > 0)
                                        {
                                            //Update Order Detail,trPalletMaster if Temp Order Exists  
                                            strWhrCond += "and OrderNo like '%t_%'";
                                            DataRow[] drResult = dtRecords.Select(strWhrCond);
                                            if (drResult.Length > 0)
                                            {

                                                int result = UpdateTemporderWithSAPOrder(fieldValueHash["OrderNo"].ToString(), drResult[0][1].ToString(),
                                                    Convert.ToInt32(drResult[0][0].ToString()), fieldValueHash["EPC_URN"].ToString(), drResult[0][30].ToString());
                                                if (result == 0)
                                                {
                                                    isRecProcOrderDetail = false;
                                                    log.Trace("DBTransact::UpdateContainerDetlsImportData:SP for temporary order sp_trorderdetail_UpdateTemporaryOrders not executed ");

                                                }
                                                else
                                                {
                                                    isRecProcOrderDetail = true;
                                                    log.Trace("DBTransact::UpdateContainerDetlsImportData:SP for temporary order sp_trorderdetail_UpdateTemporaryOrders executed successfully ");
                                                }
                                                fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERDETAIL] =
                                                         Convert.ToInt64(drResult[0][0]);
                                            }
                                            else
                                            {
                                                //Will get Order Detail ID if Order Exists to update trContanierDetails
                                                //If Same UCCNo with different OrderNo is present in AS4 file the rec will be treated as err rec.
                                                //As every OrderNo has unique UCCNo.

                                                strWhrCond = string.Empty;
                                                if (fieldValueHash["UCCNo"].ToString() != string.Empty && fieldValueHash["OrderNo"].ToString() != string.Empty)
                                                {
                                                    strWhrCond = "UCCNo='" + fieldValueHash["UCCNo"].ToString() + "'" + " and OrderNo ='" + fieldValueHash["OrderNo"].ToString() + "'";
                                                    DataRow[] dr = dtRecords.Select(strWhrCond);
                                                    if (dr.Length > 0)
                                                    {
                                                        fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERDETAIL] =
                                                          Convert.ToInt64(dr[0][0]);
                                                        isRecProcOrderDetail = true;
                                                        log.Trace("DBTransact::UpdateContainerDetlsImportData:EPC_URN already exsist.");
                                                    }
                                                    else
                                                    {
                                                        isRecProcOrderDetail = false;
                                                        log.Trace("DBTransact::UpdateContainerDetlsImportData:Order Nos and EPC_URN different.No data available-->" + strWhrCond);
                                                    }

                                                }
                                                else
                                                {
                                                    isRecProcOrderDetail = false;
                                                    log.Trace("DBTransact::UpdateContainerDetlsImportData:OrderNo or EPC_URN not found in hashtable so file treated as err rec");
                                                }

                                            }
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        string msg = "Error:" + ex.Message;
                                        if (ex.InnerException != null)
                                        {
                                            msg += Environment.NewLine + "Inner Exception:" + ex.InnerException.Message;
                                        }
                                        msg += Environment.NewLine + "Stack Trace:" + ex.StackTrace;
                                        log.Error("DBTransact::UpdateContainerDetlsImportData:" + msg);
                                    }
                                }
                                continue;

                            default:
                                continue;
                        }



                    }
                    else //!recExist
                    {
                        #region [Container Details]
                        if (tableList[tblCnt].tableName == ORDER_CONTAINER_ITEMDETAIL_TABLE)
                        {

                            if (fieldValueHash.ContainsKey(ORDER_DETAIL_TABLE_ID))
                            {
                                if (fieldValueHash[ORDER_DETAIL_TABLE_ID] != null && fieldValueHash[ORDER_DETAIL_TABLE_ID].ToString().Trim() != string.Empty)
                                {
                                    strWhrCond = CONTAINER_ITEM_TABLE_REFORDERDETAIL + " = " + fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERDETAIL] + " and "
                                                + CONTAINER_ITEM_TABLE_REFORDERITEMDETAIL + " = " + fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERITEMDETAIL];
                                    //recExist = IsRecordExist(strWhrCond, tableList[tblCnt].tableName);
                                    recExist = IsRecordExistInTrans(strWhrCond, tableList[tblCnt].tableName);



                                    string strWhrCondForQty = "OrderNo" + " = '" + fieldValueHash["OrderNo"] + "'" + " and "
                                              + "OrderItemId" + " = '" + fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERITEMDETAIL] + "'";

                                    int itemDetailQty = GetOrderItemQty(strWhrCondForQty);
                                    if (recExist)
                                    {
                                        log.Trace("DBTransact::UpdateContainerDetlsImportData:If OverrideQtyInContainer=true old qty in container will not be overriden with new qty.");

                                        if (OverrideQtyInContainer)
                                        {
                                            if (fieldValueHash["QtyInContainer"].ToString() != string.Empty)
                                            {
                                                log.Trace("DBTransact::UpdateContainerDetlsImportData:If OverrideQtyInContainer=true.All the rec will be as processed as processed rec.");

                                                int originalQty = GetContItemDetailQtyForOrderNumAndItemId(fieldValueHash["OrderDetailId"].ToString(), fieldValueHash["RefOrderItemDetail"].ToString());

                                                strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);
                                                int recUpdate = m_objDBMgr.ExecuteSQL(strSQL, true);

                                                log.Trace("DBTransact::UpdateContainerDetlsImportData:SQL => " + strSQL);
                                                strSQL = string.Empty;
                                                int totalQtyFromContaineritemDetail = GetTotalQtyForOrderNumAndItemIdFromContItemDetl(fieldValueHash["OrderNo"].ToString(), fieldValueHash["RefOrderItemDetail"].ToString());

                                                if (totalQtyFromContaineritemDetail != 0)
                                                {
                                                    if (totalQtyFromContaineritemDetail > itemDetailQty)
                                                    {
                                                        isRecProc = false;
                                                        fieldValueHash["QtyInContainer"] = originalQty;
                                                        strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);

                                                    }
                                                    else
                                                    {
                                                        isRecProc = true;
                                                    }
                                                    #region [NIU]
                                                    //else
                                                    //{
                                                    //originalQty = originalQty + i;
                                                    //if (originalQty == itemDetailQty)
                                                    //{
                                                    //    fieldValueHash["QtyInContainer"] = originalQty;
                                                    //    strSQL = GetUpdateSQL(tableList[tblCnt], fieldValueHash, strWhrCond);

                                                    //}
                                                    // }
                                                    //isRecProc = true; 
                                                    #endregion [NIU]
                                                }
                                                else
                                                {
                                                    if (Convert.ToInt32(fieldValueHash["QtyInContainer"]) > itemDetailQty)
                                                    {
                                                        log.Trace("DBTransact::UpdateContainerDetlsImportData:Container Item Qty is greater than excpted qty.So error rec.");
                                                        isRecProc = false;
                                                    }
                                                    else
                                                    {
                                                        isRecProc = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                isRecProc = false;
                                                log.Trace("DBTransact::UpdateContainerDetlsImportData:Qty in Container can not be empty.");
                                            }
                                        }
                                        else
                                        {
                                            log.Trace("DBTransact::UpdateContainerDetlsImportData:If OverrideQtyInContainer=false.All the rec will be as processed as error rec.");
                                            isRecProc = false;
                                        }
                                    }
                                    else
                                    {
                                        if (fieldValueHash["QtyInContainer"].ToString() != string.Empty)
                                        {
                                            int i = GetTotalQtyForOrderNumAndItemIdFromContItemDetl(fieldValueHash["OrderNo"].ToString(), fieldValueHash["RefOrderItemDetail"].ToString());

                                            if (i == 0)
                                            {
                                                if (itemDetailQty != Convert.ToInt32(fieldValueHash["QtyInContainer"]))
                                                {
                                                    if (Convert.ToInt32(fieldValueHash["QtyInContainer"]) > itemDetailQty)
                                                    {
                                                        log.Trace("DBTransact::UpdateContainerDetlsImportData:Container Item Qty is greater than excpted qty.So error rec.");
                                                        isRecProc = false;
                                                    }
                                                    else if (Convert.ToInt32(fieldValueHash["QtyInContainer"]) < itemDetailQty)
                                                    {
                                                        log.Trace("DBTransact::UpdateContainerDetlsImportData:Container Item Qty is less than excpted qty.");
                                                        strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                                                        isInsertQry = true;
                                                        isRecProc = true;
                                                    }
                                                }
                                                else if (itemDetailQty == Convert.ToInt32(fieldValueHash["QtyInContainer"]))
                                                {
                                                    log.Trace("DBTransact::UpdateContainerDetlsImportData:Container Item Qty is equal to excpted qty.");
                                                    strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                                                    isInsertQry = true;
                                                    isRecProc = true;
                                                }
                                            }
                                            else
                                            {

                                                int newQty = Convert.ToInt32(fieldValueHash["QtyInContainer"]);
                                                newQty = i + newQty;
                                                if (newQty != itemDetailQty)
                                                {
                                                    if (newQty < itemDetailQty)
                                                    {
                                                        strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                                                        isInsertQry = true;
                                                        isRecProc = true;
                                                    }
                                                    else
                                                    {
                                                        isRecProc = false;
                                                    }
                                                }
                                                else
                                                {

                                                    strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                                                    isInsertQry = true;
                                                    isRecProc = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            isRecProc = false;
                                            log.Trace("DBTransact::UpdateContainerDetlsImportData:Qty in Container can not be empty.");
                                        }
                                    }
                                }
                            }
                        }
                        #endregion [Container Details]
                        else
                        {
                            strSQL = GetInsertSQL(tableList[tblCnt], fieldValueHash);
                            isInsertQry = true;
                            isRecProcOrderDetail = true;
                            log.Trace("DBTransact::UpdateContainerDetlsImportData:New row inserted in trOrderDetails.");
                        }
                    }

                    log.Trace("DBTransact::UpdateContainerDetlsImportData:SQL query for new row => " + strSQL);
                    int recUpd = 0;

                    if (strSQL != string.Empty)
                    {
                        recUpd = m_objDBMgr.ExecuteSQL(strSQL, true);
                        if (isInsertQry)
                        {
                            if (tableList[tblCnt].tableName == ORDER_DETAIL_TABLE)
                            {
                                try
                                {
                                    long id = 0;
                                    DataSet ds = m_objDBMgr.GetInTransDataSet("Select orderdetailID = SCOPE_IDENTITY()");
                                    if (ds != null && ds.Tables.Count > 0)
                                    {
                                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                                        {
                                            id = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                                            if (tableList[tblCnt].tableName == ORDER_DETAIL_TABLE)
                                                fieldValueHash[CONTAINER_ITEM_TABLE_REFORDERDETAIL] = id;
                                        }
                                    }

                                    #region [UpdateShippingStatusInOrderDetails]
                                    if (fieldValueHash["EPC_URN"].ToString() != string.Empty)
                                    {
                                        DataTable dt = GetEPCHistoryForSSCC(fieldValueHash["EPC_URN"].ToString());
                                        if (dt != null && dt.Rows.Count > 0)
                                        {
                                            int res = UpdateOrderDetailsIsShipped(id, dt.Rows[0]["TimeStamp"].ToString());
                                            if (res == 0)
                                            {
                                                log.Trace("DBTransact::UpdateContainerDetlsImportData:Shipping Time and IsShipped not updated for Epc_urn-->" + fieldValueHash["EPC_URN"].ToString());

                                            }
                                            else
                                            {
                                                log.Trace("DBTransact::UpdateContainerDetlsImportData:Shipping Time and IsShipped updated succesfully for Epc_urn-->" + fieldValueHash["EPC_URN"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            log.Trace("DBTransact::UpdateContainerDetlsImportData:No row found in epcHistory for sscc_urn-->" + fieldValueHash["EPC_URN"].ToString());
                                        }
                                    }
                                    #endregion [UpdateShippingStatusInOrderDetails]
                                }
                                catch (Exception ex)
                                {
                                    string msg = "Error while getting orderDetailID:" + ex.Message;
                                    if (ex.InnerException != null)
                                    {
                                        msg += Environment.NewLine + "Inner Exception:" + ex.InnerException.Message;
                                    }
                                    msg += Environment.NewLine + "Stack Trace:" + ex.StackTrace;
                                    msg += Environment.NewLine + "Source:" + ex.Source;
                                    log.Error("DBTransact::UpdateContainerDetlsImportData:" + msg);

                                }

                            }

                        }
                    }


                }
                if (isRecProc == false || isRecProcOrderDetail == false)
                {
                    isRecProc = false;
                    log.Trace("DBTransact::UpdateContainerDetlsImportData:Error in container Item detail so there will be no entry in trOrderDetails also/Error in updateing trOrderDetails Temp order.");
                    if (useTransaction)
                        m_objDBMgr.RollBackTrans();
                }
                else
                {
                    log.Trace("DBTransact::UpdateContainerDetlsImportData:Correct entries in Container.");
                    if (useTransaction)
                        m_objDBMgr.CommitTrans();
                }
                return isRecProc;
            }


            catch (Exception ex)
            {
                if (useTransaction)
                    m_objDBMgr.RollBackTrans();
                log.Error("DBTransact::UpdateContainerDetlsImportData=>" + ex.Message, ex);
                log.Error("DBTransact::UpdateContainerDetlsImportData:Stack trace=>" + ex.StackTrace);
                if (ex.InnerException != null)
                    log.Error("DBTransact::UpdateContainerDetlsImportData=>InnerException:" + ex.InnerException.Message, ex);
                throw ex;


            }

        }


    }
}












