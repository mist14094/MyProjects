using System;
using System.Collections.Generic;
using System.Text;
using KTone.RFIDGlobal;
using NLog;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
    public class DecodeASET
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public static bool DecodeASET96(byte[] encodedASET96, out string ASET, out string serialNo, out byte filterValue, out int partVal,
            bool throwException, out string errorMessage)
        {
            errorMessage = string.Empty;
            ASET = string.Empty;
            serialNo = string.Empty;
            filterValue = 0;
            partVal = 0;

            //Check for the encoded ASET array length
            if (encodedASET96.Length != 12)
            {
                errorMessage = "This is not a valid ASET96 string.";
                if(throwException)
                    throw new InvalidASET96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
            }

            byte asetHeader = 0;
            try
            {
                asetHeader = Convert.ToByte(LCTNHelper.ASET_HEADER, 2);
            }
            catch
            {
                errorMessage = "Invalid header value for ASET 96";
                if (throwException)
                    throw new InvalidASET96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
            }


            if (encodedASET96[0] != asetHeader)
            {
                errorMessage = "Invalid header value for ASET 96";
                if (throwException)
                    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
            }

            //Xtract the encoded string from the array
            StringBuilder strbEncodedString = new StringBuilder();

            for (int byteArrayIndex = 0; byteArrayIndex < encodedASET96.Length; byteArrayIndex++)
            {
                strbEncodedString.Append(RFUtils.AddReqdZeros (Convert.ToString(encodedASET96[byteArrayIndex], 2).Trim(), 8));
            }

            

            //Xtract the filter values
            filterValue = Convert.ToByte(RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(8, 3)), 3));

            //Xtract the partition value
            byte partitionValue = Convert.ToByte(RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(11, 3)), 3));
            partVal = Convert.ToInt32(partitionValue);
            //Xtract the number of digits and number of bits for the company prefix
            int companyPrefixBitLength = 0;
            int companyPrefixDigitLength = 0;
            int locationReferenceBitLength = 0;
            int locationReferenceDigitLength = 0;

            switch (partitionValue)
            {
                case 0:
                    companyPrefixBitLength = 40;
                    companyPrefixDigitLength = 12;
                    locationReferenceBitLength = 1;
                    locationReferenceDigitLength = 0;
                    break;
                case 1:
                    companyPrefixBitLength = 37;
                    companyPrefixDigitLength = 11;
                    locationReferenceBitLength = 4;
                    locationReferenceDigitLength = 1;
                    break;
                case 2:
                    companyPrefixBitLength = 34;
                    companyPrefixDigitLength = 10;
                    locationReferenceBitLength = 7;
                    locationReferenceDigitLength = 2;
                    break;
                case 3:
                    companyPrefixBitLength = 30;
                    companyPrefixDigitLength = 9;
                    locationReferenceBitLength = 11;
                    locationReferenceDigitLength = 3;
                    break;
                case 4:
                    companyPrefixBitLength = 27;
                    companyPrefixDigitLength = 8;
                    locationReferenceBitLength = 14;
                    locationReferenceDigitLength = 4;
                    break;
                case 5:
                    companyPrefixBitLength = 24;
                    companyPrefixDigitLength = 7;
                    locationReferenceBitLength = 17;
                    locationReferenceDigitLength = 5;
                    break;
                case 6:
                    companyPrefixBitLength = 20;
                    companyPrefixDigitLength = 6;
                    locationReferenceBitLength = 21;
                    locationReferenceDigitLength = 6;
                    break;
                default:
                    errorMessage = "Invalid ASET96 partition value.";
                    if (throwException)
                        throw new InvalidASET96EncodingException(errorMessage);
                    log.Trace("Decode Error:", errorMessage);
                    return false;
            }

            //xtract the company prefix
            string companyPrefix = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(14, companyPrefixBitLength)), companyPrefixDigitLength);

            //Check for company prefix validity
            if (Convert.ToUInt64(companyPrefix) > Math.Pow(10, companyPrefixDigitLength))
            {
                errorMessage = "Invalid company reference.";
                if (throwException)
                    throw new InvalidASET96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
            }

            //Xtract the location reference
            string locationReference = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring((14 + companyPrefixBitLength), locationReferenceBitLength)), locationReferenceDigitLength);

            //Check location reference validity
            if (Convert.ToInt64(locationReference) >= Math.Pow(10, 12 - companyPrefixDigitLength))
            {
                errorMessage = "the input bit string is not a legal ASET-96 encoding.";
                if (throwException)
                    throw new InvalidASET96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
            }

            //Construct a check digit string
            StringBuilder strbChkDigitString = new StringBuilder();
            strbChkDigitString.Append(companyPrefix);
            strbChkDigitString.Append(locationReference);

            string calculatedCheckDigit = RFUtils.CalculateCheckDigit(strbChkDigitString.ToString(), LCTNHelper.ASET13_LEN);
            ASET = String.Concat(strbChkDigitString.ToString(), calculatedCheckDigit);

            //Extract the serial no
            serialNo = RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(55));
            return true;
        }

        public static void BreakASET(string strASET, int partitionValue, out string companyPrefix, out string locationRef)
        {
            companyPrefix = string.Empty;
            locationRef = string.Empty;
            try
            {

                PartitionTable.FillPartitionTables(EPCFORMAT.ASET);
                int companyPrefixDigitLen = PartitionTable.companyPrefixDigitLength[partitionValue];
                if (partitionValue == 0)
                {
                    companyPrefix = strASET.Substring(0, companyPrefixDigitLen);
                    return;
                }
                int locationRefDigitLen = PartitionTable.locationRefDigitLength[partitionValue];

                companyPrefix = strASET.Substring(0, companyPrefixDigitLen);
                locationRef = strASET.Substring(companyPrefixDigitLen, locationRefDigitLen);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool DecodeASET96(byte[] encodedASET96, out string companyPrefix, out string locationRef, out string serialNo, out byte filterValue,
            bool throwException, out string errorMessage)
        {
            errorMessage = string.Empty;
            companyPrefix = string.Empty;
            locationRef = string.Empty;
            serialNo = string.Empty;
            filterValue = 0;
            string ASET = string.Empty;
            int partVal = 0;

            try
            {
                if (!DecodeASET96(encodedASET96, out ASET, out serialNo, out filterValue, out partVal, throwException, out errorMessage))
                    return false;
                BreakASET(ASET, partVal, out companyPrefix, out locationRef);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public static bool DecodeASET64(byte[] encodedASET96, out string companyPrefix, out string locationRef, out string serialNo, out byte filterValue,
            bool throwException, out string errorMessage)
        {
            errorMessage = string.Empty;
            companyPrefix = string.Empty;
            locationRef = string.Empty;
            serialNo = string.Empty;
            filterValue = 0;
            string ASET = string.Empty;
            int partVal = 0;

            try
            {
                if (!DecodeASET96(encodedASET96, out ASET, out serialNo, out filterValue, out partVal, throwException, out errorMessage))
                    return false;
                BreakASET(ASET, partVal, out companyPrefix, out locationRef);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
    }
}
