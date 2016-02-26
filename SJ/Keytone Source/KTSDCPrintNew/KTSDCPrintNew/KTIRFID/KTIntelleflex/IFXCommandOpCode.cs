/********************************************************************************************************
Copyright (c) 2010 - 2011 KeyTone Technologies.All Right Reserved

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

namespace KTone.Core.KTIRFID
{
    /// <summary>
    /// Enumeration of valid commands 
    /// </summary>
    public enum IFXCommandOpCode
    {
        /// <summary>
        /// Get Tag Info
        /// </summary>
        getInfo,

        /// <summary>
        /// Find All Tags
        /// </summary>
        findAllTags,

        /// <summary>
        /// Find Tags
        /// </summary>
        findTags,

        /// <summary>
        /// Stop Action
        /// </summary>
        stopAction,

        /// <summary>
        /// Read Tag Memory
        /// </summary>
        readTagMemory,

        /// <summary>
        /// Write Tag Memory
        /// </summary>
        writeTagMemory,

        /// <summary>
        /// Set Info
        /// </summary>
        setInfo,

        /// <summary>
        /// Get Memory Locks
        /// </summary>
        getMemoryLocks,

        /// <summary>
        /// Start Inventory
        /// </summary>
        startInventory,

        /// <summary>
        /// Stop Inventory
        /// </summary>
        stopInventory,

        /// <summary>
        /// Set Memory Locks
        /// </summary>
        setMemoryLocks,

        /// <summary>
        /// Get Memory Locks
        /// </summary>
        getMemoryLockBit,

        /// <summary>
        /// Set Memory Locks
        /// </summary>
        setMemoryLockBit,

        /// <summary>
        /// Reset Memory lock 
        /// </summary>
        resetMemoryLockBit,

        /// <summary>
        /// Change Lock password
        /// </summary>
        changeBlockPassword,

        /// <summary>
        /// Discover Readers
        /// </summary>
        discoverReaders,

        /// <summary>
        /// Set GPIO Status
        /// </summary>
        outputGPIO


    }

    /// <summary>
    /// Enumeration of Errors 
    /// </summary>
    public enum IFXReaderErrors
    {
        /// <summary>
        /// No Errors
        /// </summary>
        NO_ERRORS = 0,

        /// <summary>
        /// Invalid Parameter
        /// </summary>
        INVALID_PARAMETER=1,

        /// <summary>
        /// Unknown Error
        /// </summary>
        UNKNOWN_ERROR = 4,
        //NO_TAG_FOUND=4,

        /// <summary>
        /// Memory Locked
        /// </summary>
        MEMORY_LOCKED=5,

        /// <summary>
        /// Memory Overrun
        /// </summary>
        MEMORY_OVERRUN=6,

        /// <summary>
        /// Insufficient Power
        /// </summary>
        INSUFFICIENT_POWER=12,

        /// <summary>
        /// No Reply
        /// </summary>
        NO_REPLY=100,        
        //BAD_PASSWORD = 100,        

        /// <summary>
        /// Format error because of invalid parameter
        /// </summary>
        FORMAT_ERROR_INVALID_PARAMETER = 200,

        /// <summary>
        /// CRC error
        /// </summary>
        CRC_ERROR =201


    }

    /// <summary>
    /// Enumeration of returned status after sending a command to reader and executing it
    /// </summary>
    public enum IFXReaderExecuteStatus
    {
        /// <summary>
        /// Ececuted successfully
        /// </summary>
        Success,

        /// <summary>
        /// Execution error
        /// </summary>
        Error
    }

    /// <summary>
    /// Enumeration of Exceptions
    /// </summary>
    public enum IFXNumExceptions
    {
        /// <summary>
        /// No Errors
        /// </summary>
        No_Error
    }
}
