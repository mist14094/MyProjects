using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
    public class EncodeLCTN
    {
        public static byte[] EncodeLCTNtoLCTN96(byte filterValue, string actualLCTN, Int32 companyPrefixLength, Int64 serialNo)
        {
            //Check for GNL validity
            if (actualLCTN.Length != 13)
            {
                throw new InvalidLCTN96EncodingException("Invalid LCTN string.");
            }

            //Check for serial no validity
            if (serialNo < 0 || serialNo >= Math.Pow(2, 41))
            {
                throw new InvalidLCTN96EncodingException("Invalid serial no.");
            }

            //Check for filter value validity
            if (filterValue < 0 || filterValue > 7)
            {
                throw new InvalidLCTN96EncodingException("Invalid filter value.");
            }

            int companyPrefixBitsLength = 0, companyPrefixDigitsLength = 0, partitionValue = 0, locationReferenceBitsLength = 0;
            //Lookup the partition value depending on the company prefix length from the partition table
            switch (companyPrefixLength)
            {
                case 12:
                    partitionValue = 0;
                    companyPrefixBitsLength = 40;
                    companyPrefixDigitsLength = 12;
                    locationReferenceBitsLength = 1;
                    break;
                case 11:
                    partitionValue = 1;
                    companyPrefixBitsLength = 37;
                    companyPrefixDigitsLength = 11;
                    locationReferenceBitsLength = 4;
                    break;
                case 10:
                    partitionValue = 2;
                    companyPrefixBitsLength = 34;
                    companyPrefixDigitsLength = 10;
                    locationReferenceBitsLength = 7;
                    break;
                case 9:
                    partitionValue = 3;
                    companyPrefixBitsLength = 30;
                    companyPrefixDigitsLength = 9;
                    locationReferenceBitsLength = 11;
                    break;
                case 8:
                    partitionValue = 4;
                    companyPrefixBitsLength = 27;
                    companyPrefixDigitsLength = 8;
                    locationReferenceBitsLength = 14;
                    break;
                case 7:
                    partitionValue = 5;
                    companyPrefixBitsLength = 24;
                    companyPrefixDigitsLength = 7;
                    locationReferenceBitsLength = 17;
                    break;
                case 6:
                    partitionValue = 6;
                    companyPrefixBitsLength = 20;
                    companyPrefixDigitsLength = 6;
                    locationReferenceBitsLength = 21;
                    break;
                default:
                    throw new InvalidCompanyPrefixLengthException("Invalid company prefix length.");
            }

            //Declare the main encode string
            StringBuilder strbEncodeString = new StringBuilder();

            //Add the header value
            //Header value is 00110010 in binary
            strbEncodeString.Append(LCTNHelper.LCTN_HEADER);

            //filter value
            strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue, 2), 3));

            //Partition value
            strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(partitionValue, 2), 3));

            //Xtrcat the company prefix
            string companyPrefix = RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(actualLCTN.Substring(0, companyPrefixDigitsLength)), 2), companyPrefixBitsLength);

            strbEncodeString.Append(companyPrefix);

            //Construct the location reference
            string locationReference = string.Empty;

            if (companyPrefixDigitsLength == 12)
            {
                locationReference = "0";
            }
            else
                locationReference = RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(actualLCTN.Substring(companyPrefixDigitsLength, 12 - companyPrefixDigitsLength)), 2), locationReferenceBitsLength);

            strbEncodeString.Append(locationReference);

            //Serial No
            strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(serialNo, 2), 41));

            //Declare the byte array
            byte[] encodedLCTN96 = new byte[12];
            //Insert the string values to byte array
            for (int arrIndex = 0, startValue = 0; arrIndex < 12; arrIndex++, startValue += 8)
            {
                encodedLCTN96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(startValue, 8)));
            }
            return encodedLCTN96;
        }

        public static string CreateLCTN13(string companyPrefix, string locationRef)
        {
            string strLCTN = string.Empty;
            int companyPrefixLength = companyPrefix.Length;
            int locationRefLength = locationRef.Length;
            if ((companyPrefixLength + locationRefLength) > 12)
            {
                throw new EPCTagExceptionBase("CreateLCTN13(): LCTN number more than 12 digits. (without the checkdigit)");
            }
            if (companyPrefixLength == LCTNHelper.LCTN13_LEN - 1)
            {
                strLCTN = companyPrefix;
            }
            else
            {
                int locationRefExpectedLength = 12 - (companyPrefixLength);

                if (locationRef.Length < locationRefExpectedLength)
                {
                    locationRef = locationRef.PadLeft(locationRefExpectedLength, '0');
                }
                strLCTN = companyPrefix + locationRef;
            }
            strLCTN += RFUtils.CalculateCheckDigit(strLCTN, LCTNHelper.LCTN13_LEN);
            return strLCTN;
        }

        public static byte[] EncodeLCTNtoLCTN96(byte filterValue, string companyPrefix, string locationRef, Int32 companyPrefixLength, Int64 serialNo)
        {
            //Declare the byte array to return
            byte[] encodedLCTN96 = null;

            string actualLCTN = CreateLCTN13(companyPrefix, locationRef);
            encodedLCTN96 = EncodeLCTNtoLCTN96(filterValue, actualLCTN, companyPrefixLength, serialNo);
            return encodedLCTN96;
        }

        public static byte[] EncodeLCTNtoLCTN96(byte filterValue, string companyPrefix, string locationRef, Int64 serialNo)
        {
            //Declare the byte array to return
            byte[] encodedLCTN96 = null;
            encodedLCTN96 = EncodeLCTNtoLCTN96(filterValue, companyPrefix, locationRef, companyPrefix.Length, serialNo);
            return encodedLCTN96;
        }
    }
}
