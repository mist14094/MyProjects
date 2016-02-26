
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
using System.Collections.Generic;

namespace KTone.RFIDGlobal.ConfigParams
{
	/// <summary>
	/// This interface provides method signatures for obtaining paramVals from XML file.
	/// </summary>
	
	public interface IConfigParams
	{
		#region [Interface Methods]
		
//		/// <summary>
//		/// Checks whether a given parameter is present in the XML or not.
//		/// </summary>
//		/// <param name="name">parameter whose value is to be fetched</param>
//		/// <returns>a boolean indicating true/false</returns>
//		bool IsPresent(string name) ;

		void GetLimits(out string defVal,out string minVal,out string maxVal);

        List<string> GetAttributeNames();

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,out string paramVal) ;
		
		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,out bool paramVal) ;

        /// <summary>
        /// Looks up for any value in the config params XML
        /// </summary>
        /// <param name="name">parameter whose value is to be fetched</param>
        /// <param name="paramVal">value of the parameter in relevant data type</param>
        void Lookup(string name, out Byte paramVal);

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,out Int16 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,out Int32 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,out Int64 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,out UInt16 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,out UInt32 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,out UInt64 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,out float paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,out double paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML,if value is not found or 
		/// any exception occurs, a default value entered by the user is set as the current value.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
		/// doesn't return a valid value.</param>
		/// <param name="paramVal"></param>
		void Lookup(string name,string defaultVal,out string paramVal) ;
		
		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
		/// doesn't return a valid value.</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,bool defaultVal,out bool paramVal) ;
		
		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
		/// doesn't return a valid value.</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,Int16 defaultVal,out Int16 paramVal) ;

        /// <summary>
        /// Looks up for any value in the config params XML
        /// </summary>
        /// <param name="name">parameter whose value is to be fetched</param>
        /// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
        /// doesn't return a valid value.</param>
        /// <param name="paramVal">value of the parameter in relevant data type</param>
        void Lookup(string name, Byte defaultVal, out Byte paramVal);

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
		/// doesn't return a valid value.</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,Int32 defaultVal,out Int32 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
		/// doesn't return a valid value.</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,Int64 defaultVal,out Int64 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
		/// doesn't return a valid value.</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,UInt16 defaultVal,out UInt16 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
		/// doesn't return a valid value.</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,UInt32 defaultVal,out UInt32 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
		/// doesn't return a valid value.</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,UInt64 defaultVal,out UInt64 paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
		/// doesn't return a valid value.</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,float defaultVal,out float paramVal) ;

		/// <summary>
		/// Looks up for any value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="defaultVal">The input value to be set to the variable in case XML lookup 
		/// doesn't return a valid value.</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Lookup(string name,double defaultVal,out double paramVal) ;

		/// <summary>
		/// Save new value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be fetched</param>
		/// <param name="paramVal">value of the parameter in relevant data type</param>
		void Save(string name, string paramVal) ;
		/// <summary>
		/// Set default value in the config params XML
		/// </summary>
		/// <param name="name">parameter whose value is to be reset</param>
		void SetDefault(string name);
		#endregion Interface Methods ENDS
	}
}
