
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
 
 using System ;

namespace KTone.RFIDGlobal.EPCTagEncoding
{

	public class Constants
	{
		public const string CAGECODEORDODAAC = "CAGECode" ;
		
		public const string HEADER = "Header" ;
		public const string FILTER = "Filter" ;
		public const string PARTITION = "Partition" ;
		public const string COMPANYPREFIX = "CompanyPrefix" ;
		public const string ITEMREFERENCE = "ItemReference" ;
		public const string SERIALNO = "SerialNo" ;
		public const byte	FILTERVALUE = 3 ;
		public const string INDICATORDIGIT = "1" ;
		public const string SGTIN64_HEADER = "10";
		public const string SGTIN96_HEADER = "00110000";
		public const string GRAI64_HEADER = "00001010";
		public const string GRAI96_HEADER = "00110011";
		public const string GIAI64_HEADER = "00001011";
		public const string GIAI96_HEADER = "00110100";
		public const string SGLN64_HEADER = "00001001";
		public const string SGLN96_HEADER = "00110010";
		public const string SSCC64_HEADER = "00001000";//Header value for SSCC64
		public const string SSCC96_HEADER = "00110001";//Header value for SSCC96
		public const string GID96_HEADER  = "00110101";
		public const string APPLICATION_IDENTIFIER = "(00)";
		public const int SSCC18_LEN = 18;
		public const string SERIALREF = "SerialRef" ;
		public const string UNALLOCATED = "UnAllocated" ;
		public const string LOCATIONREF = "LocationRef" ;
		public const string ASSETTYPE = "AssetType" ;
		public const int SGLN13_LEN = 13 ;
		public const string SSCCURI = "sscc" ;
		public const string SGTINURI	 = "sgtin";
		public const string SGLNURI	 = "sgln";
		public const string GIDURI	 = "gid";
		public const string GRAIURI	 = "grai";
		public const string USDODURI	 = "usdod";
		public const string EPCURIPREFIX = "urn:epc:" ;
		public const string URITAG = "tag:" ;
		public const string URIID = "id:";
		public const string URIRAW = "raw:";
		public const string URIPAT = "pat:";
		public const string URIKEYTONE = "keytone:";
		public const int SGTIN14_LEN	 = 14;
		public const string ASSETREF = "AssetRef" ;
		public const string GIAIURI	 = "giai";
		public const int	GRAI_LEN	 = 13 ;//without check digit
		public const string MANUFACTID	=  "ManufaturerId";
		public const string PRODUCTID	=  "ProductId";
		public const string INDICATOR = "Packaging Type";
		public const string TRANSTABLEXMLNAME = "ManagerTranslation.xml";
		public const string TRANSTABLEXSDNAME = "XmlTranslationTableSpec.xsd";
		public const string COMPANYPREFIXINDEX = "CompanyPrefixIndex";
		public const string STRREPRESENT96 = "96";
		public const string STRREPRESENT64 = "64";
		
		public const string SSCCURI96	= "sscc-96:" ;
		public const string SGTINURI96	= "sgtin-96:";
		public const string SGLNURI96	 = "sgln-96:";
		public const string GIDURI96	 = "gid-96:";
		public const string GIAIURI96	 = "giai-96:";
		public const string GRAIURI96	 = "grai-96:";
		public const string USDODURI96	 = "usdod-96:";

		public const string SSCCURI64	= "sscc-64:" ;
		public const string SGTINURI64	= "sgtin-64:";
		public const string SGLNURI64	 = "sgln-64:";
		public const string GIDURI64	 = "sgln-64:";
		public const string GRAIURI64	 = "grai-64:";
		public const string GIAIURI64	 = "giai-64:";
		public const string USDODURI64	 = "usdod-64:";

		public const string USDOD96_HEADER = "00101111";
		public const string USDOD64_HEADER = "11001110";

		public const string SSCCURN96 = "urn:epc:tag:sscc-96:" ;
		public const string SSCCURN64 = "urn:epc:tag:sscc-64:" ;
	}
}