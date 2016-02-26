
/********************************************************************************************************
Copyright (c) 2010 KeyTone Technologies.All Right Reserved

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

namespace KTone.RFIDGlobal.EPCTagEncoding
{

	#region Generic exceptions Applicable for all schemes
	/// <summary>
	/// Class to trap Invalid company prefix exceptions
	/// </summary>
	public class InvalidCompanyPrefixException : EPCTagExceptionBase
	{
		public InvalidCompanyPrefixException(string exceptionMessage) : base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap Invalid item reference exceptions
	/// </summary>
	public class InvalidItemReferenceException : EPCTagExceptionBase
	{
		public InvalidItemReferenceException(string exceptionMessage) : base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap invalid company prefix length exceptions
	/// </summary>
	public class InvalidCompanyPrefixLengthException: EPCTagExceptionBase
	{
		public InvalidCompanyPrefixLengthException(string exceptionMessage) : base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap invalid header value exceptions
	/// </summary>
	public class InvalidHeaderValue : EPCTagExceptionBase
	{
		public InvalidHeaderValue(string exceptionMessage) : base(exceptionMessage){}
	}

	#endregion
	
	#region GTIN specific exceptions
	/// <summary>
	/// Class to trap GTIN string of invalid length
	/// </summary>
	public class GTINStringTooLargeException : EPCTagExceptionBase
	{
		public GTINStringTooLargeException(string exceptionMessage) :  base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap Invalid SGTIN64 bit values
	/// </summary>
	public class InvalidSGTIN64EncodingException: EPCTagExceptionBase
	{
		public InvalidSGTIN64EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap Invalid SGTIN96 bit values
	/// </summary>
	public class InvalidSGTIN96EncodingException: EPCTagExceptionBase
	{
		public InvalidSGTIN96EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}

	/// <summary>
	/// Class to trap Invalid GTIN14 number encountered
	/// </summary>
	public class InvalidGTIN14NumberException: EPCTagExceptionBase
	{
		public InvalidGTIN14NumberException(string exceptionMessage) : base(exceptionMessage){}
	}

	/// <summary>
	/// Invalid company prefix exception
	/// </summary>
	public class InvalidSGTINCompanyPrefixException: EPCTagExceptionBase
	{
		public InvalidSGTINCompanyPrefixException(string exceptionMessage) : base(exceptionMessage){}
	}

	/// <summary>
	/// Invalid Filter Value Exception
	/// </summary>
	public class InvalidSGTINFilterValueException: EPCTagExceptionBase
	{
		public InvalidSGTINFilterValueException(string exceptionMessage) : base(exceptionMessage){}
	}

	/// <summary>
	/// Invalid SGTIN serial number Exception
	/// </summary>
	public class InvalidSGTINSerialNumberException: EPCTagExceptionBase
	{
		public InvalidSGTINSerialNumberException(string exceptionMessage) : base(exceptionMessage){}
	}

	/// <summary>
	/// Invalid SGTIN Partition Value Exception
	/// </summary>
	public class InvalidSGTINPartitionValue : EPCTagExceptionBase
	{
		public InvalidSGTINPartitionValue(string exceptionMessage) : base(exceptionMessage){}
	}

	public class InvalidGTINIndicator: EPCTagExceptionBase
	{
		public InvalidGTINIndicator(string exceptionMessage) : base(exceptionMessage){}
	}
	public class InvalidGTINCompanyPrefix : EPCTagExceptionBase
	{
		public InvalidGTINCompanyPrefix(string exceptionMessage) : base(exceptionMessage){}
	}
	public class InvalidGTINItemReference: EPCTagExceptionBase
	{
		public InvalidGTINItemReference(string exceptionMessage) : base(exceptionMessage){}
	}
	public class InvalidGTINCheckDigit: EPCTagExceptionBase
	{
		public InvalidGTINCheckDigit(string exceptionMessage) : base(exceptionMessage){}
	}
	#endregion

	#region SSC specific exceptions
	/// <summary>
	/// Class to trap SCC values with invalid string length
	/// </summary>
	public class SCCStringTooLargeException : EPCTagExceptionBase
	{
		public SCCStringTooLargeException(string exceptionMessage) :  base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap Invalid SSCC64 bit strings
	/// </summary>
	public class InvalidSSCC64EncodingException: EPCTagExceptionBase
	{
		public InvalidSSCC64EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap Invalid SSCC96 bit strings
	/// </summary>
	public class InvalidSSCC96EncodingException: EPCTagExceptionBase
	{
		public InvalidSSCC96EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}

	public class InvalidSSCCCompanyPrefixException:EPCTagExceptionBase
	{
		public InvalidSSCCCompanyPrefixException(string exceptionMessage) : base(exceptionMessage){}
	}

	public class InvalidSSCCSerialNumberException:EPCTagExceptionBase
	{
		public InvalidSSCCSerialNumberException(string exceptionMessage) : base(exceptionMessage){}
	}

	public class InvalidSSCCExtensionDigitException:EPCTagExceptionBase
	{
		public InvalidSSCCExtensionDigitException(string exceptionMessage) : base(exceptionMessage){}
	}
	#endregion

	#region GLN specific exceptions
	/// <summary>
	/// Class to trap GLN string of invalid length
	/// </summary>
	public class GLNStringTooLargeException : EPCTagExceptionBase
	{
		public GLNStringTooLargeException(string exceptionMessage) :  base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap Invalid SGLN64 bit strings
	/// </summary>
	public class InvalidSGLN64EncodingException: EPCTagExceptionBase
	{
		public InvalidSGLN64EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap Invalid SGLN96 bit strings
	/// </summary>
	public class InvalidSGLN96EncodingException: EPCTagExceptionBase
	{
		public InvalidSGLN96EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}

	#endregion

	#region GRAI specific exceptions
	/// <summary>
	/// Class to trap GRAI strings of invalid length
	/// </summary>
	public class GRAIStringTooLargeException : EPCTagExceptionBase
	{
		public GRAIStringTooLargeException(string exceptionMessage) :  base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap invalid GRAI64 strings
	/// </summary>
	public class InvalidGRAI64EncodingException: EPCTagExceptionBase
	{
		public InvalidGRAI64EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap invalid GRAI96 strings
	/// </summary>
	public class InvalidGRAI96EncodingException: EPCTagExceptionBase
	{
		public InvalidGRAI96EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}

	#endregion

	#region GIAI specific exceptions
	/// <summary>
	/// Class to trap invalid length GIAI strings
	/// </summary>
	public class GIAIStringTooLargeException : EPCTagExceptionBase
	{
		public GIAIStringTooLargeException(string exceptionMessage) :  base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap invalid GIAI 64 bit strings
	/// </summary>
	public class InvalidGIAI64EncodingException: EPCTagExceptionBase
	{
		public InvalidGIAI64EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}
	/// <summary>
	/// Class to trap invalid GIAI 96 bit strings
	/// </summary>
	public class InvalidGIAI96EncodingException: EPCTagExceptionBase
	{
		public InvalidGIAI96EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}

	#endregion

	#region EPC Format Specifications' Exception

	public class InvalidEPCFormatException : EPCTagExceptionBase
	{
		public InvalidEPCFormatException(string exceptionMessage) : base(exceptionMessage){}
	}

	#endregion

	#region DoD specific exceptions

	public class DodStringToolLargeException : EPCTagExceptionBase
	{
		public DodStringToolLargeException(string exceptionMessage) : base(exceptionMessage){}
	}

	public class InvalidDoD96EncodingException : EPCTagExceptionBase
	{
		public InvalidDoD96EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}

	public class InvalidDoD96DecodingException : EPCTagExceptionBase
	{
		public InvalidDoD96DecodingException(string exceptionMessage) : base(exceptionMessage){}
	}

	public class InvalidDoD64EncodingException : EPCTagExceptionBase
	{
		public InvalidDoD64EncodingException(string exceptionMessage) : base(exceptionMessage){}
	}

	public class InvalidDoD64DecodingException : EPCTagExceptionBase
	{
		public InvalidDoD64DecodingException(string exceptionMessage) : base(exceptionMessage){}
	}

	#endregion

    #region LCTN specific exceptions
    /// <summary>
    /// Class to trap LCTN string of invalid length
    /// </summary>
    public class LCTNStringTooLargeException : EPCTagExceptionBase
    {
        public LCTNStringTooLargeException(string exceptionMessage) : base(exceptionMessage) { }
    }
    /// <summary>
    /// Class to trap Invalid LCTN64 bit strings
    /// </summary>
    public class InvalidLCTN64EncodingException : EPCTagExceptionBase
    {
        public InvalidLCTN64EncodingException(string exceptionMessage) : base(exceptionMessage) { }
    }
    /// <summary>
    /// Class to trap Invalid LCTN96 bit strings
    /// </summary>
    public class InvalidLCTN96EncodingException : EPCTagExceptionBase
    {
        public InvalidLCTN96EncodingException(string exceptionMessage) : base(exceptionMessage) { }
    }

    #endregion

    #region ASET specific exceptions
    /// <summary>
    /// Class to trap ASET string of invalid length
    /// </summary>
    public class ASETStringTooLargeException : EPCTagExceptionBase
    {
        public ASETStringTooLargeException(string exceptionMessage) : base(exceptionMessage) { }
    }
    /// <summary>
    /// Class to trap Invalid ASET64 bit strings
    /// </summary>
    public class InvalidASET64EncodingException : EPCTagExceptionBase
    {
        public InvalidASET64EncodingException(string exceptionMessage) : base(exceptionMessage) { }
    }
    /// <summary>
    /// Class to trap Invalid ASET96 bit strings
    /// </summary>
    public class InvalidASET96EncodingException : EPCTagExceptionBase
    {
        public InvalidASET96EncodingException(string exceptionMessage) : base(exceptionMessage) { }
    }

    #endregion
    
	#region Base class of all Exceptions

	public class EPCTagExceptionBase: ApplicationException
	{
		public EPCTagExceptionBase(string exceptionMessage) : base(exceptionMessage){}
	}

	#endregion Base class of all Exceptions ENDS
	
    #region Base class for Translation Table exceptions

	public class CompanyPrefixLookupImplException:EPCTagExceptionBase
	{
	    public CompanyPrefixLookupImplException(string exceptionMessage) : base(exceptionMessage){}
	}

	#endregion Base class for Translation Table exceptions ENDS
}
