
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
using System.Reflection ;
using KTone.RFIDGlobal.KTLogger;
using NLog;
using System.Collections.Generic;

namespace KTone.RFIDGlobal.ConfigParams
{
	/// <summary>
	/// Abstract class that contains the common functionality of implementation of IConfigParams interface.
	/// </summary>
	public abstract class BaseConfigParams: IConfigParams
	{
		#region [Attributes]

        protected static Logger m_log
            = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();

		#endregion  [Attributes] ENDS		
		
        public BaseConfigParams()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		#region [IConfigParams Virtual Members]

//		public virtual bool IsPresent(string name)
//		{
//			// TODO:  Add ConfigParams.IsPresent implementation
//			return false;
//		}

        public virtual List<string> GetAttributeNames()
        {
            return null;
        }

		public virtual void GetLimits(out string defVal,out string minVal,out string maxVal)
		{
			defVal = null ;
			minVal = null ;
			maxVal = null ;
		}

		public virtual void Lookup(string name, out string paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = null;
		}

		public virtual void Lookup(string name, out bool paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = false;
		}

        public virtual void Lookup(string name, out Byte paramVal)
        {
            // TODO:  Add ConfigParams.Lookup implementation
            paramVal = new Byte();
        }

		public virtual void Lookup(string name, out Int16 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int16 ();
		}

		public virtual void Lookup(string name, out Int32 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int32 ();
		}

		public virtual void Lookup(string name, out Int64 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int64 ();
		}

		public virtual void Lookup(string name, out UInt16 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new UInt16 ();
		}

		public virtual void Lookup(string name, out UInt32 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new UInt32 ();
		}

		public virtual void Lookup(string name, out UInt64 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new UInt64 ();
		}

		public virtual void Lookup(string name, out float paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = 0;
		}

		public virtual void Lookup(string name, out double paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = 0;
		}

		public virtual void Lookup(string name, string defaultVal, out string paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = null;
		}

		public virtual void Lookup(string name, bool defaultVal, out bool paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = false;
		}

        public virtual void Lookup(string name, Byte defaultVal, out Byte paramVal)
        {
            // TODO:  Add ConfigParams.Lookup implementation
            paramVal = new Byte();
        }

		public virtual void Lookup(string name, Int16 defaultVal, out Int16 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int16 ();
		}

		public virtual void Lookup(string name, Int32 defaultVal, out Int32 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int32 ();
		}

		public virtual void Lookup(string name, Int64 defaultVal, out Int64 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new Int64 ();
		}

		public virtual void Lookup(string name, UInt16 defaultVal, out UInt16 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new UInt16 ();
		}

		public virtual void Lookup(string name, UInt32 defaultVal, out UInt32 paramVal) 
		{
			 paramVal = 0 ;
		}

		public virtual void Lookup(string name, UInt64 defaultVal, out UInt64 paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = new UInt64 ();
		}

		public virtual void Lookup(string name, float defaultVal, out float paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = 0;
		}

		public virtual void Lookup(string name, double defaultVal, out double paramVal)
		{
			// TODO:  Add ConfigParams.Lookup implementation
			paramVal = 0;
		}


		public virtual void Save(string name, string paramVal)
		{
			//To Do: Add ConfigParams.Save implementation
		}		

		public virtual void SetDefault(string name)
		{
			//To Do: Add ConfigParams.Save implementation
		}
		#endregion


		#region [Private Methods]

		#endregion [Private Methods] ENDS
	}
}

namespace KTone.RFIDGlobal.ConfigParams.XSD.ConfigParams {


    partial class AttributeConfig
    {
    }
}
