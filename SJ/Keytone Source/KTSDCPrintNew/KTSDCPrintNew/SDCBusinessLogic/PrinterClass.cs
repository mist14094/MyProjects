using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KTone.DAL.SmartDCDataAccess;
using System.Configuration;
using System.Data.SqlTypes;
using KTone.RFIDGlobal.EPCTagEncoding;
using KTone.DAL.KTDBBaseLib;
using KTone.DAL.KTDAGlobaApplLib;
using KTone.Core.KTIRFID;
using KTone.RFIDGlobal;
using System.Web.Configuration;



namespace KTone.Core.SDCBusinessLogic
{
   public class PrinterClass
    {
       NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

       public bool Print(KTItemDetails ItemDetails, int locationId, int numOfCopies, out string errorMsg, PrintOperation OperationType, int dataOwnerID)
       {
           ItemMaster objItemcls = new ItemMaster();
           GTINSerialNo objGTINSerialNo = new GTINSerialNo();
           byte[] SGTINByte = null;
           byte[] arrByte = null;
           string ByteArrayToHexStringtype = null;
           string TagData = null;
           string DecodedURIString = null;
           long Sr_No;
           bool printitem = false;
           string rftagId = null;
           KTSKUDetails skuDetails = null;
           KTProductDetails prodDetails = null;
           KTCompanyDetails compDetails = null;
           KTLocationDetails locationdetails = null;
           Dictionary<string, string> Dictionarylableobj = new Dictionary<string, string>();
           ItemClass objItemClass = new ItemClass();
           LocationClass objLocationClass=new LocationClass();
           CompProdSKUClass objCompProdSKUClass = new CompProdSKUClass();
           try
           {
               _log.Trace("PrinterClass:Print:Entering");

               //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
               //if (sdcCache == null)
               //{
                   _log.Trace("PrinterClass:Print:sdcCache null");
                   locationdetails = objLocationClass.GetLocationByLocationID(dataOwnerID, locationId);
               //}
               //else
               //{
               //    _log.Trace("PrinterClass:Print:sdcCache not  null");
               //    locationdetails = sdcCache.GetAllLocationByLocationID(dataOwnerID, locationId);
               //}
              
                   System.Threading.Thread.Sleep(200);
                   IKTComponent getcomponent = null;
                   try
                   {
                       getcomponent = SDCWSHelper.GetcomponentInstance(locationdetails, dataOwnerID);
                   }
                   catch(Exception ex)
                   {
                       errorMsg = "Service Not Found.Cannot Print.";
                       return false;
                   }
                  if (getcomponent == null)
                   {
                       errorMsg = "Printer Not Found.Cannot Print.";
                       return false;
                   }
                  
                   if (getcomponent is IKTPrinter)
                   {
                       errorMsg = "";
                   }
                   else
                   {
                       errorMsg = "ComponentInstance Not Matching";
                       return false;
                   }
              
               arrByte = objItemClass.ConvertToByte(ItemDetails);
             //  if (sdcCache == null)
            //   {
                   skuDetails = objCompProdSKUClass.GetSKUForSkuID(ItemDetails.DataOwnerID, ItemDetails.SKU_ID);
                   prodDetails = objCompProdSKUClass.GetProductForProductID(ItemDetails.DataOwnerID,skuDetails.ProductID);
                   compDetails = objCompProdSKUClass.GetCompanyForCompanyID(ItemDetails.DataOwnerID,prodDetails.CompanyID);
             //  }
             //  else
             //  {
                 //  skuDetails = sdcCache.GetSKUForSkuID(ItemDetails.DataOwnerID, ItemDetails.SKU_ID);
                 //  prodDetails = sdcCache.GetProductForProductID(ItemDetails.DataOwnerID, skuDetails.ProductID);
              //     compDetails = sdcCache.GetCompanyForCompanyID(ItemDetails.DataOwnerID, prodDetails.CompanyID);
             //  }
               if (OperationType == PrintOperation.Insert || OperationType == PrintOperation.Update)
               {
                   objGTINSerialNo.SKUID = ItemDetails.SKU_ID;
                   DataTable dtSrNo = objGTINSerialNo.SelectOneSrno();

                   Sr_No = Convert.ToInt64(dtSrNo.Rows[0]["Sr_No"].ToString());

                   SGTINByte = EncodeGTIN.EncodeGTIN14toSGTIN96(0, skuDetails.PackageID.ToString(), compDetails.CompanyPrefix, prodDetails.ProductPrefix, Sr_No);
                   ByteArrayToHexStringtype = RFUtils.ByteArrayToHexString(SGTINByte);
                   DecodedURIString = EPCBytes.Decode(SGTINByte);
                   TagData = RFUtils.ByteArrayToHexString(arrByte);

               }
               else
               {
                   foreach (SDCTagData t in ItemDetails.TagDetails)
                   {
                       rftagId = t.TagID;
                   }

                   DecodedURIString = rftagId;
                   SGTINByte = EPCBytes.GetByteArrFmURN(DecodedURIString);
                   ByteArrayToHexStringtype = RFUtils.ByteArrayToHexString(SGTINByte);
                   ByteArrayToHexStringtype = ByteArrayToHexStringtype.Replace(" ", "");
                   TagData = RFUtils.ByteArrayToHexString(arrByte);
                   TagData = TagData.Replace(" ", "");
               }

               ByteArrayToHexStringtype = ByteArrayToHexStringtype.Replace(" ", "");
               TagData = TagData.Replace(" ", "");

               Dictionarylableobj.Add("ProductName", prodDetails.ProductName);
               Dictionarylableobj.Add("ProductSKU", skuDetails.ProductSKU);
               Dictionarylableobj.Add("CompanyName", compDetails.CompanyName);
               Dictionarylableobj.Add("HEX_URN", ByteArrayToHexStringtype);
               Dictionarylableobj.Add("Serial_No", ItemDetails.CustomerUniqueID);
               Dictionarylableobj.Add("TAG_DATA", TagData);
               Dictionarylableobj.Add("URN", DecodedURIString);

               printitem = ((IKTPrinter)getcomponent).Print(Dictionarylableobj, numOfCopies, out errorMsg);

               if (printitem == true)
               {
                   // Insert Item 
                   if (OperationType == PrintOperation.Insert)
                   {
                       _log.Trace("PrinterClass:Print::PrintOperation:Insert");
                       objItemcls.CustomerUniqueID = ItemDetails.CustomerUniqueID;
                       objItemcls.SKU_ID = ItemDetails.SKU_ID;
                       objItemcls.Status = ItemDetails.Status;
                       objItemcls.CrupUser = ItemDetails.CreatedBy;
                       objItemcls.CategoryID = 3;
                       // objItemcls.LastSeenTime = DateTime.Now;
                       objItemcls.LastSeenLocation = locationId;
                       objItemcls.ItemStatus = ItemDetails.ItemStatus;
                       objItemcls.TagType = 2;
                       objItemcls.IsActive = true;
                       objItemcls.Comments = ItemDetails.Comments;
                       objItemcls.DataOwnerID = ItemDetails.DataOwnerID;
                       objItemcls.RFTagIDURN = DecodedURIString.ToString();


                       StringBuilder sbLine1 = new StringBuilder();
                       StringBuilder sbLine2 = new StringBuilder();
                       string valueL1 = string.Empty;
                       string valueL2 = string.Empty;

                       foreach (KeyValuePair<string, string> pair in ItemDetails.CustomColumnDetails)
                       {
                           valueL1 = Convert.ToString(pair.Key);
                           valueL2 = Convert.ToString(pair.Value);

                           if (!(string.IsNullOrEmpty(valueL1)) && !(string.IsNullOrEmpty(valueL2)))
                           {

                               sbLine1.Append(valueL1 + ",");
                               sbLine2.Append("'" + valueL2.Replace("'", "''") + "'" + ",");

                           }
                           else
                           {
                               valueL1 = string.Empty;
                               valueL2 = string.Empty;

                           }
                       }

                       if (!(string.IsNullOrEmpty(sbLine1.ToString())) && !(string.IsNullOrEmpty(sbLine2.ToString())))
                       {
                           valueL1 = Convert.ToString(sbLine1.Remove(sbLine1.Length - 1, 1));
                           valueL2 = Convert.ToString(sbLine2.Remove(sbLine2.Length - 1, 1));
                       }
                       objItemcls.Insert(valueL1, valueL2);

                       errorMsg = "";
                       return true;
                   }

                   if (OperationType == PrintOperation.Update)
                   {
                       _log.Trace("PrinterClass:Print::PrintOperation:Update");

                       objItemcls.ID = ItemDetails.ID;
                       objItemcls.CrupUser = ItemDetails.CreatedBy;
                       objItemcls.TagType = 2;
                       objItemcls.IsActive = true;
                       objItemcls.Comments = ItemDetails.Comments;
                       objItemcls.DataOwnerID = ItemDetails.DataOwnerID;
                       objItemcls.RFTagIDURN = DecodedURIString.ToString();
                       objItemcls.LastSeenLocation = locationId;
                       //  objItemcls.LastSeenTime = DateTime.Now; 

                       objItemcls.Update();
                       errorMsg = "";
                       return true;
                   }

                   if (OperationType == PrintOperation.Duplicate)
                   {
                       _log.Trace("PrinterClass:Print::PrintOperation:Duplicate");

                       objItemcls.ID = ItemDetails.ID;
                       objItemcls.LastSeenLocation = locationId;
                       // objItemcls.LastSeenTime = DateTime.Now; 
                       objItemcls.UpdateItemDetails();
                       errorMsg = "";
                       return true;
                   }
               }

               else
               {
                   errorMsg = "Item Not Printed Successfully";
                   return false;
               }

           }
           catch (Exception ex)
           {
               _log.Error("Error:PrinterClass:Print:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception (ex.Message);
           }
           finally
           {
               SDCWSHelper.ReleaseRemoteObject();
               _log.Trace("PrinterClass:Print:Leaving");
           }
           return true;
       }

       public bool LocationPrint(KTLocationDetails LocationDetails, int locationId, int numOfCopies, out string errorMsg, LocationPrintOperation OperationType, int dataOwnerID)
       {
           byte[] LCTNByte = null;
           string ByteArrayToHexStringtype = null;
           string DecodedURIString = null;
           Sequence objSequenceNo = new Sequence();
           int Sr_No;
           bool printitem = false;
           byte filterDigit = 0;
           string location_Ref = "00000";
           string CompanyPrefix = WebConfigurationManager.AppSettings["LocationPrefix"].ToString();
           Dictionary<string, string> Dictionarylableobj = new Dictionary<string, string>();
           Location objLoaction = new Location();
           KTLocationDetails tempLocDetails = null;
           LocationClass objLocationClass = new LocationClass();
         
           try
           {
               _log.Trace("PrinterClass:LocationPrint:Entering");

               //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
               //if (sdcCache == null)
               //{
                   _log.Trace("PrinterClass:LocationPrint:sdcCache null");
                   tempLocDetails = objLocationClass.GetLocationByLocationID(dataOwnerID, locationId);
               //}
               //else
               //{
               //    _log.Trace("PrinterClass:LocationPrint:sdcCache not null");
               //     tempLocDetails = sdcCache.GetAllLocationByLocationID(dataOwnerID, locationId);
               //}
               IKTComponent getcomponent = SDCWSHelper.GetcomponentInstance(tempLocDetails, dataOwnerID);

               if (getcomponent == null)
               {
                   errorMsg = "Service Not Found.Cannot Print.";
                   return false;
               }

               if (getcomponent is IKTPrinter)
               {
                   errorMsg = "";
               }
               else
               {
                   errorMsg = "ComponentInstance Not Matching";
                   return false;
               }


               if (OperationType == LocationPrintOperation.Update)
               {
                   objSequenceNo.SequenceName = "Location";
                   DataTable dtSrNo = objSequenceNo.SelectOneSrno();

                   Sr_No = Convert.ToInt32(dtSrNo.Rows[0]["Sequence_ID"].ToString());

                   LCTNByte = EncodeLCTN.EncodeLCTNtoLCTN96(filterDigit, CompanyPrefix, location_Ref, Convert.ToInt64(Sr_No));
                   ByteArrayToHexStringtype = RFUtils.ByteArrayToHexString(LCTNByte);
                   DecodedURIString = EPCBytes.Decode(LCTNByte);

               }
               else
               {

                   DecodedURIString = LocationDetails.RFValue;
                   LCTNByte = EPCBytes.GetByteArrFmURN(DecodedURIString);
                   ByteArrayToHexStringtype = RFUtils.ByteArrayToHexString(LCTNByte);

               }

               ByteArrayToHexStringtype = ByteArrayToHexStringtype.Replace(" ", "");
               Dictionarylableobj.Add("LocationName", LocationDetails.LocationName);
               Dictionarylableobj.Add("HEX_URN", ByteArrayToHexStringtype);
               Dictionarylableobj.Add("Description", LocationDetails.Description);
               Dictionarylableobj.Add("URN", DecodedURIString);

               printitem = ((IKTPrinter)getcomponent).Print(Dictionarylableobj, numOfCopies, out errorMsg);

               if (printitem == true)
               {
                   if (OperationType == LocationPrintOperation.Update)
                   {
                       _log.Trace("LocationPrint::LocationPrint:Update");

                       objLoaction.LocationId = LocationDetails.LocationID;
                       objLoaction.LocationName = LocationDetails.LocationName;
                       objLoaction.Description = LocationDetails.Description;
                       objLoaction.CategoryId = LocationDetails.CategoryID;
                       objLoaction.RFResource = LocationDetails.RFResource;
                       objLoaction.RFValue = DecodedURIString;
                       objLoaction.ParentLocationId = LocationDetails.ParentLocationId;
                       objLoaction.IsActive = LocationDetails.IsActive;
                       objLoaction.DataOwnerId = LocationDetails.DataOwnerID;
                       objLoaction.CreatedDate = LocationDetails.CreatedDate;
                       objLoaction.ModifiedDate = LocationDetails.ModifiedDate;

                       objLoaction.Update();


                       errorMsg = "";
                       return true;
                   }

                   if (OperationType == LocationPrintOperation.Duplicate)
                   {
                       _log.Trace("LocationPrint:Duplicate");

                       errorMsg = "";
                       return true;
                   }
               }
               else
               {
                   errorMsg = "Location Not Printed Successfully";
                   return false;
               }

           }
           catch (Exception ex)
           {
               _log.Error("Error:PrinterClass:LocationPrint:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception (ex.Message);
           }
           finally
           {
               SDCWSHelper.ReleaseRemoteObject();
               _log.Trace("PrinterClass:LocationPrint:Leaving");
           }
           errorMsg = "";
           return true;

       }

       public List<KTItemDetails> GetItemsForBatchPrinting( long SkuID, int NoOfItems, ItemType itemType, int dataOwnerID)
       {
          
           List<KTItemDetails> lstitem = new List<KTItemDetails>();
           List<KTItemDetails> lstitemfinal = new List<KTItemDetails>();
           List<SDCTagData> lst = new List<SDCTagData>();
           ItemClass objItemClass=new ItemClass();
           try
           {
               _log.Trace("PrinterClass:GetItemsForBatchPrinting:Entering");

              
               //IKTSDCCache sdcCache = SDCWSHelper.GetSDCCache();
               //if (sdcCache == null)
               //{
                   return lstitemfinal = objItemClass.GetItemForSkuID(dataOwnerID, SkuID, NoOfItems, itemType);
               //}
               //else
               //{
               //   return lstitemfinal = sdcCache.GetItemForSkuID(dataOwnerID, SkuID, NoOfItems, itemType);
                  
               //}

               //int i = 0;

               //if (itemType == ItemType.UnPrinted)
               //{
               //    foreach (KTItemDetails objItem in lstitem)
               //    {
               //        if (objItem.TagDetails.Count == 0)
               //        {
               //            lstitemfinal.Add(objItem);
               //            i++;
               //        }
               //        if (i == NoOfItems)
               //        {
               //            break;
               //        }
               //    }
               //}

               //if (itemType == ItemType.Printed)
               //{

               //    foreach (KTItemDetails objItem in lstitem)
               //    {
               //        if (objItem.TagDetails.Count > 0)
               //        {
               //            lstitemfinal.Add(objItem);
               //            i++;
               //        }
               //        if (i == NoOfItems)
               //        {
               //            break;
               //        }
               //    }

               //}
               //if (itemType == ItemType.All)
               //{
               //    // return lstitem;
               //    foreach (KTItemDetails objItem in lstitem)
               //    {
               //        i++;
               //        lstitemfinal.Add(objItem);
               //        if (i == NoOfItems)
               //        {
               //            break;
               //        }
               //    }
               //}
           }
           catch (Exception ex)
           {
               _log.Error("Error:PrinterClass:GetItemsForBatchPrinting:: " + ex.Message + Environment.NewLine + ex.StackTrace);
               throw new Exception(ex.Message);

           }
           finally
           {
               SDCWSHelper.ReleaseRemoteObject();
               _log.Trace("PrinterClass:GetItemsForBatchPrinting:Leaving");
           }
           return lstitemfinal;
       }

    }
}
