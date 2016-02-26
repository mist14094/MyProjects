/********************************************************************************************************
Copyright (c) 2010-2011 KeyTone Technologies.All Right Reserved

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
using System.Runtime.Serialization;

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// The exception that is thrown when an error occurs in the execution of any 
    /// command of the reader
    /// </summary>
    [Serializable]
    public class IFXReaderException : ApplicationException
    {
        IFXReaderErrors m_ErrorCode = IFXReaderErrors.NO_ERRORS;
        /// <summary>
        /// Constructor for raising direct exceptions
        /// </summary>
        /// <param name="desc">Description of error</param>
        public IFXReaderException(string desc)
            : base(desc)
        {}

        /// <summary>
        /// Constructor for raising direct exceptions
        /// </summary>
        /// <param name="desc">Description of error</param>
        /// <param name="errorCode">IFXReaderErrors error code returned by the reader</param>
        public IFXReaderException( IFXReaderErrors errorCode,string desc)
            : base(desc)
        {
            m_ErrorCode = errorCode;
        }

        /// <summary>
        /// Constructor for raising direct exceptions 
        /// [With Serializable info]
        /// </summary>
        /// <param name="exceptionInfo"></param>
        /// <param name="exceptionContext"></param>
        public IFXReaderException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            this.m_ErrorCode = (IFXReaderErrors)Enum.Parse(typeof(IFXReaderErrors), exceptionInfo.GetString("ErrorCode"));
        }

        /// <summary>
        /// Sets the SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="exceptionInfo">The SerializationInfo that holds the 
        /// serialized object data about the exception being thrown. </param>
        /// <param name="exceptionContext">The StreamingContext that contains 
        /// contextual information about the source or destination. </param>
        public override void GetObjectData(SerializationInfo exceptionInfo,
            StreamingContext exceptionContext)
        {
            base.GetObjectData(exceptionInfo, exceptionContext);
            exceptionInfo.AddValue("ErrorCode", this.m_ErrorCode.ToString());
        }
    }
    /// <summary>
    /// The exception that is thrown when an error occurs
    /// </summary>
    [Serializable]
    public class IFXException : ApplicationException
    {

        /// <summary>
        /// Constructor for raising direct exceptions
        /// </summary>
        /// <param name="desc">Description of error</param>
        public IFXException(string desc)
            : base(desc)
        {

        }

        /// <summary>
        /// Constructor for raising direct exceptions
        /// [With Serializable info]
        /// </summary>
        /// <param name="exceptionInfo"></param>
        /// <param name="exceptionContext"></param>
        public IFXException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            //this.m_ErrorCode = (IFXReaderErrors)Enum.Parse(typeof(IFXReaderErrors), exceptionInfo.GetString("ErrorCode"));
        }

     
    }

    /// <summary>
    /// The exception that is thrown when there is a timeout while waiting 
    /// for the response from a reader.
    /// </summary>
    [Serializable]
    public class IfxTimeoutException : ApplicationException
    {
        /// <summary>
        /// Constructor for raising direct exceptions
        /// </summary>
        /// <param name="desc">Description of error</param>
        public IfxTimeoutException(string desc)
            : base(desc)
        {
        }

        /// <summary>
        /// Constructor for raising direct exceptions
        /// [With Serializable info]
        /// </summary>
        /// <param name="exceptionInfo"></param>
        /// <param name="exceptionContext"></param>
        public IfxTimeoutException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            //this.m_ErrorCode = (IFXReaderErrors)Enum.Parse(typeof(IFXReaderErrors), exceptionInfo.GetString("ErrorCode"));
        }
    }
    /// <summary>
    /// The exception that is thrown when there is an error while receiving the response 
    /// from a reader.
    /// </summary>
    [Serializable]
    public class InternalErrorException : ApplicationException
    {
        /// <summary>
        /// Constructor for raising direct exceptions
        /// </summary>
        /// <param name="desc">Description of error</param>
        public InternalErrorException(string desc)
            : base(desc)
        {
        }

        /// <summary>
        /// Constructor for raising direct exceptions
        /// [With Serializable info]
        /// </summary>
        /// <param name="exceptionInfo"></param>
        /// <param name="exceptionContext"></param>
        public InternalErrorException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            //this.m_ErrorCode = (IFXReaderErrors)Enum.Parse(typeof(IFXReaderErrors), exceptionInfo.GetString("ErrorCode"));
        }
    }

    /// <summary>
    /// This exception is thrown when
    /// <list >
    /// <item>there is a connection failure</item>
    /// <item>commands cannot be sent to reader</item>
    /// <item>reader is not responding to commands</item>
    /// </list>
    /// </summary>
    [Serializable]
    public class CommunicationException : ApplicationException
    {
        /// <summary>
        /// Constructor for raising direct exceptions
        /// </summary>
        /// <param name="desc"></param>
        public CommunicationException(string desc)
            : base(desc)
        {

        }
        
        /// <summary>
        /// Constructor for raising direct exceptions
        /// [With Serializable info]
        /// </summary>
        /// <param name="exceptionInfo"></param>
        /// <param name="exceptionContext"></param>
        public CommunicationException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            //this.m_ErrorCode = (IFXReaderErrors)Enum.Parse(typeof(IFXReaderErrors), exceptionInfo.GetString("ErrorCode"));
        }
    }

    /// <summary>
    /// This exception is thrown when a command cannot be foramtted in CommandFormatter class
    /// </summary>
    [Serializable]
    public class CommandFormatterException : ApplicationException
    {
        /// <summary>
        /// Constructor for raising direct exceptions
        /// </summary>
        /// <param name="desc"></param>
        public CommandFormatterException(string desc)
            : base(desc)
        {

        }

        /// <summary>
        /// Constructor for raising direct exceptions
        /// [With Serializable info]
        /// </summary>
        /// <param name="exceptionInfo"></param>
        /// <param name="exceptionContext"></param>
        public CommandFormatterException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            //this.m_ErrorCode = (IFXReaderErrors)Enum.Parse(typeof(IFXReaderErrors), exceptionInfo.GetString("ErrorCode"));
        }
    }

    /// <summary>
    /// This exception is thrown when response received from reader cannot be parsed
    /// </summary>
    [Serializable]
    public class ResponserParserException : ApplicationException
    {
        /// <summary>
        /// Constructor for raising direct exceptions
        /// </summary>
        /// <param name="desc"></param>
        public ResponserParserException(string desc)
            : base(desc)
        {

        }

        /// <summary>
        /// Constructor for raising direct exceptions
        /// [With Serializable info]
        /// </summary>
        /// <param name="exceptionInfo"></param>
        /// <param name="exceptionContext"></param>
         public ResponserParserException(SerializationInfo exceptionInfo, StreamingContext exceptionContext)
            : base(exceptionInfo, exceptionContext)
        {
            //this.m_ErrorCode = (IFXReaderErrors)Enum.Parse(typeof(IFXReaderErrors), exceptionInfo.GetString("ErrorCode"));
        }
    }

    /// <summary>
    /// Exception used for all HttpBase errors.
    /// </summary>
    [Serializable]
    public class HttpBaseException : ApplicationException
    {
        /// <summary>
        /// Constructor for raising direct exceptions
        /// </summary>
        /// <param name="desc">Description of error</param>
        public HttpBaseException(string desc) : base(desc) { }

        /// <summary>
        /// Constructor for raising direct exceptions with inner exception
        /// </summary>
        /// <param name="desc">Description of error</param>
        /// <param name="innerException">inner exception</param>
        public HttpBaseException(string desc, Exception innerException) : base(desc, innerException) { }
    }

}
