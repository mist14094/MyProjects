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
using System.Text;
using NLog;
using System.Diagnostics;

namespace KTone.RFIDGlobal.KTLogger
{
    public class KTLogManager
    {
        #region [Public]
        /// <summary>
        /// Gets Nlog's Logger Instance with Specifed Compoent ID
        /// Example : For a component,logger Name would be
        /// KTone.Core.KTComponent.KTComponentImpl::[S01READER00001]	
        /// </summary>
        /// <param name="componetID">Id to be asscociated with component</param>
        /// <returns>Nlog.Logger</returns>
        public static Logger GetLogger(string componetID)
        {
            StackFrame frame = new StackFrame(1, false);
            string loggerName = frame.GetMethod().DeclaringType.FullName + "::[" + componetID + "]";
            return LogManager.GetLogger(loggerName);
        }

        /// <summary>
        /// Gets transaction log object for a component
        /// Example : For a component,transaction logger Name would be
        /// KTone.Core.KTComponent.KTComponentImpl::Transaction::[S01READER00001]	
        /// </summary>
        /// <param name="componetID"></param>
        /// <returns></returns>
        public static Logger GetTransactionLogger(string componetID)
        {
            StackFrame frame = new StackFrame(1, false);
            string loggerName = frame.GetMethod().DeclaringType.FullName + "::Transaction::[" + componetID + "]";
            return LogManager.GetLogger(loggerName);
        }
        /// <summary>
        /// Gets Nlog`s Logger Instance to 
        /// </summary>        
        /// <returns></returns>
        public static Logger GetLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }
        /// <summary>
        /// Gets Logger name
        /// </summary>
        /// <param name="loggerName">logger name</param>
        /// <returns>Nlog.Logger</returns>
        public static Logger GetLoggerByName(string loggerName)
        {
            return LogManager.GetLogger(loggerName);
        }
        #endregion [Public]
    }
}
