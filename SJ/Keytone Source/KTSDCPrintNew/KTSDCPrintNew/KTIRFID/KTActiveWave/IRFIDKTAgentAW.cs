///********************************************************************************************************
//Copyright (c) 2010 KeyTone Technologies.All Right Reserved

//KeyTone's source code and documentation is copyrighted and contains proprietary information.
//Licensee shall not modify, create any derivative works, make modifications, improvements, 
//distribute or reveal the source code ("Improvements") to anyone other than the software 
//developers of licensee's organization unless the licensee has entered into a written agreement
//("Agreement") to do so with KeyTone Technologies Inc. Licensee hereby assigns to KeyTone all right,
//title and interest in and to such Improvements unless otherwise stated in the Agreement. Licensee 
//may not resell, rent, lease, or distribute the source code alone, it shall only be distributed in 
//compiled component of an application within the licensee'organization. 
//The licensee shall not resell, rent, lease, or distribute the products created from the source code
//in any way that would compete with KeyTone Technologies Inc. KeyTone' copyright notice may not be 
//removed from the source code.

//Licensee may be held legally responsible for any infringement of intellectual property rights that
//is caused or encouraged by licensee's failure to abide by the terms of this Agreement. Licensee may 
//make copies of the source code provided the copyright and trademark notices are reproduced in their 
//entirety on the copy. KeyTone reserves all rights not specifically granted to Licensee. 

//Use of this source code constitutes an agreement not to criticize, in any way, the code-writing style
//of the author, including any statements regarding the extent of documentation and comments present.

//THE SOFTWARE IS PROVIDED "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
//BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER  LIABILITY, 
//WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
//OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. KEYTONE TECHNOLOGIES SHALL NOT BE LIABLE FOR ANY DAMAGES 
//SUFFERED BY LICENSEE AS A RESULT OF USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.
//********************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.Core.KTIRFID
{
    public interface IRFIDKTAgentAW
    {

        #region Methods

        /// <summary>
        /// Returns requested no. of error records.
        /// </summary>
        /// <param name="recCount">no. of error records</param>
        /// <returns></returns>
        string[] QueryErrorRecords(int recCount);

        /// <summary>
        /// Returns requested no. of log records.
        /// </summary>
        /// <param name="recCount">no. of log records</param>
        /// <returns></returns>
        string[] QueryLogRecords(int recCount);

        /// <summary>
        /// CustomColumn's value for DBAGENT type.
        /// </summary>
        /// <param name="CustomColValues">List of values for custom col of ACTIVETAG_DATA(</param>
        void SetCustomColumns(List<string> CustomColValues);
        #endregion

        #region Events
        /// <summary>
        /// Event raised when the state of the element changes.
        /// </summary>
        //event StateChangeEventHandler OnStateChanged;

        /// <summary>
        /// Event raised when the data received by the agent and queued for processing. 
        /// </summary>
        event EventHandler<KTAWAgentEventArgs> KTActiveWaveAgentDataReceived;

        /// <summary>
        /// Event raised when the queued data is processed successfully. 
        /// </summary>
        event EventHandler<KTAWAgentEventArgs> KTAgentActiveWaveDataProcessed;

        #endregion

    }
}


