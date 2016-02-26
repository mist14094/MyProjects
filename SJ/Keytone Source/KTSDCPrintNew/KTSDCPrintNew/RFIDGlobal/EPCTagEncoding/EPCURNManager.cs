#region [USING]
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
//using KTone.DB.SDCDataAccess;
#endregion [USING]

namespace KTone.RFIDGlobal.EPCTagEncoding
{
    public static class EPCURNManager
    {
        private const string SSCC_INITIAL = "urn:epc:tag:sscc-96:";
        
        public static string GetSSCCUrnFromUCCNo(string uccNo, int filter, int companyPrefixLength)
        {
            try
            {
                string extDigit = uccNo.Substring(0, 1);
                string compID = uccNo.Substring(1, companyPrefixLength);
                int serialNoLen = 16 - companyPrefixLength;//companyPrefixLength + serial no length is always 16 digits
                string serNo = uccNo.Substring(companyPrefixLength + 1, serialNoLen);
                return SSCC_INITIAL + filter.ToString() + "." + compID + '.' + extDigit + @serNo;
            }
            catch(Exception ex) 
            {
                throw new Exception("Invalid UCC Number", ex);
            }
        }

        /// <summary>
        /// Gets the Check Digit based on 17 digit UCC Number 
        /// </summary>
        /// <param name="sscc"></param>
        /// <returns></returns>
        public static string GetCheckDigitUCC(string ucc)
        {
            if ((ucc == null) || (ucc.Length != 17))
                throw new ApplicationException(" Unable to calculate the check digit.");
            try
            {
                int[] digits = new int[ucc.Length];
                for (int i = 0; i < digits.Length; i++)
                {
                    digits[i] = int.Parse(ucc.Substring(i, 1));
                }
                int checkValue = 10 - ((((digits[0] + digits[2] + digits[4] + digits[6] + digits[8] + digits[10] + digits[12] + digits[14] + digits[16]) * 3) +
                    (digits[1] + digits[3] + digits[5] + digits[7] + digits[9] + digits[11] + digits[13] + digits[15])) % 10);
                if (checkValue == 10)
                    checkValue = 0;
                return checkValue.ToString();
            }
            catch (Exception)
            {
                throw new ApplicationException(" Unable to calculate the check digit.");
            }
        }

        public static string GetUCCFromSSCCUrn(string SSCCUrn)
        {

            StringBuilder uccNo = new StringBuilder();
            try
            {
                string[] urnArr = SSCCUrn.Split('.');

                if (urnArr == null || urnArr.Length != 3)
                    throw new ApplicationException("Invalid SSCC Urn supplied.");

                uccNo.Append(urnArr[2].Substring(0, 1)); //extdigit
                uccNo.Append(urnArr[1]);//companyprefix
                uccNo.Append(urnArr[2].Substring(1, urnArr[2].Length - 1));//serialNo

                string checkDigit = GetCheckDigitUCC(uccNo.ToString());
                uccNo.Append(checkDigit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return uccNo.ToString();

        }
    }
}
