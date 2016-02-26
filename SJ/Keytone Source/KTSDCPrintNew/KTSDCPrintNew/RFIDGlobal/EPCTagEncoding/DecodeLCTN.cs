using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using NLog;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
    public class DecodeLCTN
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public static bool DecodeLCTN96(byte[] encodedLCTN96, out string LCTN, out string serialNo, out byte filterValue, out int partVal,
            bool throwException, out string errorMessage)
        {
            errorMessage = string.Empty;
            LCTN = string.Empty;
            serialNo = string.Empty;
            filterValue = 0;
            partVal = 0;
            
            //Check for the encoded LCTN array length
            if (encodedLCTN96.Length != 12)
            {
                errorMessage = "This is not a valid LCTN96 string.";
                if(throwException)
                    throw new InvalidLCTN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
            }
            
            byte lctnHeader = 0;
            try
            {
                lctnHeader = Convert.ToByte(LCTNHelper.LCTN_HEADER, 2);
            }
            catch
            {
                errorMessage = "Invalid header value for LCTN 96";
                if(throwException)
                    throw new InvalidLCTN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
            }

            //Check for header value validity header value is 00110010=50 in decimal
            if (encodedLCTN96[0] != lctnHeader)
            {
                errorMessage = "Invalid header value for LCTN 96";
                if(throwException)
                    throw new InvalidHeaderValue(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
            }
            
            //Xtract the encoded string from the array
            StringBuilder strbEncodedString = new StringBuilder();

            for (int byteArrayIndex = 0; byteArrayIndex < encodedLCTN96.Length; byteArrayIndex++)
            {
                strbEncodedString.Append(RFUtils.AddReqdZeros(Convert.ToString(encodedLCTN96[byteArrayIndex], 2).Trim(), 8));
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
                    errorMessage = "Invalid LCTN96 partition value.";
                    if(throwException)
                        throw new InvalidLCTN96EncodingException(errorMessage);
                    log.Trace("Decode Error:", errorMessage);
                    return false;
            }

            //xtract the company prefix
            string companyPrefix = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(14, companyPrefixBitLength)), companyPrefixDigitLength);

            //Check for company prefix validity
            if (Convert.ToUInt64(companyPrefix) > Math.Pow(10, companyPrefixDigitLength))
            {
                errorMessage = "Invalid company reference.";
                if(throwException)
                    throw new InvalidLCTN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
            }

            //Xtract the location reference
            string locationReference = RFUtils.AddReqdZeros(RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring((14 + companyPrefixBitLength), locationReferenceBitLength)), locationReferenceDigitLength);

            //Check location reference validity
            if (Convert.ToInt64(locationReference) >= Math.Pow(10, 12 - companyPrefixDigitLength))
            {
                errorMessage = "the input bit string is not a legal LCTN-96 encoding.";
                if(throwException)
                    throw new InvalidLCTN96EncodingException(errorMessage);
                log.Trace("Decode Error:", errorMessage);
                return false;
            }

            //Construct a check digit string
            StringBuilder strbChkDigitString = new StringBuilder();
            strbChkDigitString.Append(companyPrefix);
            strbChkDigitString.Append(locationReference);

            string calculatedCheckDigit = RFUtils.CalculateCheckDigit(strbChkDigitString.ToString(), LCTNHelper.LCTN13_LEN);
            LCTN = String.Concat(strbChkDigitString.ToString(), calculatedCheckDigit);

            //Extract the serial no
            serialNo = RFUtils.ConvertBinaryToDecimal(strbEncodedString.ToString().Substring(55));
            return true;
        }

        public static void BreakLCTN(string strLCTN, int partitionValue, out string companyPrefix, out string locationRef)
        {
            companyPrefix = string.Empty;
            locationRef = string.Empty;
            try
            {

                PartitionTable.FillPartitionTables(EPCFORMAT.LCTN);
                int companyPrefixDigitLen = PartitionTable.companyPrefixDigitLength[partitionValue];
                if (partitionValue == 0)
                {
                    companyPrefix = strLCTN.Substring(0, companyPrefixDigitLen);
                    return;
                }
                int locationRefDigitLen = PartitionTable.locationRefDigitLength[partitionValue];

                companyPrefix = strLCTN.Substring(0, companyPrefixDigitLen);
                locationRef = strLCTN.Substring(companyPrefixDigitLen, locationRefDigitLen);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool DecodeLCTN96(byte[] encodedLCTN96, out string companyPrefix, out string locationRef, out string serialNo, out byte filterValue,
            bool throwException, out string errorMessage)
        {
            errorMessage = string.Empty;
            companyPrefix = string.Empty;
            locationRef = string.Empty;
            serialNo = string.Empty;
            filterValue = 0;
            string LCTN = string.Empty;
            int partVal = 0;

            try
            {
                if (!DecodeLCTN96(encodedLCTN96, out LCTN, out serialNo, out filterValue, out partVal, throwException, out errorMessage))
                    return false;
                BreakLCTN(LCTN, partVal, out companyPrefix, out locationRef);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public static bool DecodeLCTN64(byte[] encodedLCTN96, out string companyPrefix, out string locationRef, out string serialNo, out byte filterValue,
            bool throwException, out string errorMessage)
        {
            errorMessage = string.Empty;
            companyPrefix = string.Empty;
            locationRef = string.Empty;
            serialNo = string.Empty;
            filterValue = 0;
            string LCTN = string.Empty;
            int partVal = 0;
           
            try
            {
                if (!DecodeLCTN96(encodedLCTN96, out LCTN, out serialNo, out filterValue, out partVal,
                    throwException, out errorMessage))
                    return false;
                BreakLCTN(LCTN, partVal, out companyPrefix, out locationRef);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }
    }
}
