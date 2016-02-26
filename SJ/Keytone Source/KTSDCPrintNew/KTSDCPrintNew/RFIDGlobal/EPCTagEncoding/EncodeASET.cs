using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.RFIDGlobal.EPCTagEncoding
{
    public class EncodeASET
    {
        internal static byte[] EncodeASETtoASET96(byte filterValue, string actualASET, Int32 companyPrefixLength, Int64 serialNo)
        {
            //Check for GNL validity
            if (actualASET.Length != 13)
            {
                throw new InvalidASET96EncodingException("Invalid ASET string.");
            }

            //Check for serial no validity
            if (serialNo < 0 || serialNo >= Math.Pow(2, 41))
            {
                throw new InvalidASET96EncodingException("Invalid serial no.");
            }

            //Check for filter value validity
            if (filterValue < 0 || filterValue > 7)
            {
                throw new InvalidASET96EncodingException("Invalid filter value.");
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
            strbEncodeString.Append(LCTNHelper.ASET_HEADER);

            //filter value
            strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(filterValue, 2), 3));

            //Partition value
            strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(partitionValue, 2), 3));

            //Xtrcat the company prefix
            string companyPrefix = RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(actualASET.Substring(0, companyPrefixDigitsLength)), 2), companyPrefixBitsLength);

            strbEncodeString.Append(companyPrefix);

            //Construct the location reference
            string locationReference = string.Empty;

            if (companyPrefixDigitsLength == 12)
            {
                locationReference = "0";
            }
            else
                locationReference = RFUtils.AddReqdZeros(Convert.ToString(Convert.ToInt64(actualASET.Substring(companyPrefixDigitsLength, 12 - companyPrefixDigitsLength)), 2), locationReferenceBitsLength);

            strbEncodeString.Append(locationReference);

            //Serial No
            strbEncodeString.Append(RFUtils.AddReqdZeros(Convert.ToString(serialNo, 2), 41));

            //Declare the byte array
            byte[] encodedASET96 = new byte[12];
            //Insert the string values to byte array
            for (int arrIndex = 0, startValue = 0; arrIndex < 12; arrIndex++, startValue += 8)
            {
                encodedASET96[arrIndex] = Convert.ToByte(RFUtils.ConvertBinaryToDecimal(strbEncodeString.ToString().Substring(startValue, 8)));
            }
            return encodedASET96;
        }

        internal static string CreateASET13(string companyPrefix, string locationRef)
        {
            string strASET = string.Empty;
            int companyPrefixLength = companyPrefix.Length;
            int locationRefLength = locationRef.Length;
            if ((companyPrefixLength + locationRefLength) > 12)
            {
                throw new EPCTagExceptionBase("CreateASET13(): ASET number more than 12 digits. (without the checkdigit)");
            }
            if (companyPrefixLength == LCTNHelper.ASET13_LEN - 1)
            {
                strASET = companyPrefix;
            }
            else
            {
                int locationRefExpectedLength = 12 - (companyPrefixLength);

                if (locationRef.Length < locationRefExpectedLength)
                {
                    locationRef = locationRef.PadLeft(locationRefExpectedLength, '0');
                }
                strASET = companyPrefix + locationRef;
            }
            strASET += RFUtils.CalculateCheckDigit(strASET, LCTNHelper.ASET13_LEN);
            return strASET;
        }

        internal static byte[] EncodeASETtoASET96(byte filterValue, string companyPrefix, string locationRef, Int32 companyPrefixLength, Int64 serialNo)
        {
            //Declare the byte array to return
            byte[] encodedASET96 = null;

            string actualASET = CreateASET13(companyPrefix, locationRef);
            encodedASET96 = EncodeASETtoASET96(filterValue, actualASET, companyPrefixLength, serialNo);
            return encodedASET96;
        }

        internal static byte[] EncodeASETtoASET96(byte filterValue, string companyPrefix, string locationRef, Int64 serialNo)
        {
            //Declare the byte array to return
            byte[] encodedASET96 = null;
            encodedASET96 = EncodeASETtoASET96(filterValue, companyPrefix, locationRef, companyPrefix.Length, serialNo);
            return encodedASET96;
        }
    }
}
