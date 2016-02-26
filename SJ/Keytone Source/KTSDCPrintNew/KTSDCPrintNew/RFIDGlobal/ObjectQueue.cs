
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
using System.Collections;
using System.Threading;



namespace KTone.RFIDGlobal
{
	/// <summary>
	/// Summary description for Queue.
	/// </summary>
	public class ObjectQueue
	{
		#region Attributes
        private static readonly NLog.Logger log = KTLogger.KTLogManager.GetLogger();
			
		private readonly object qLock = new object();
		
		private System.Collections.Queue queue = new System.Collections.Queue();
		
		private int capacity = 10;
		public enum  Status { Success = 0 ,Aborted, Timeout,  Overflow , Empty, Error } ; 
		/// <summary>
		/// This flag indicates the Dequeue operation needs to be aborted.
		/// </summary>
		private bool abortDequeue = false; 
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a Queue object with default capacity = 10.It returns Overflow status 
		/// if actual size exceeds capacity.
		/// </summary>
		public ObjectQueue()
		{
			log.Debug("Queue::Queue:capacity : " + capacity);
		}

		/// <summary>
		/// Initializes a Queue object with given capacity.It returns Overflow status 
		/// if actual size exceeds capacity.
		/// </summary>
		/// <param name="capacity"></param>
		public ObjectQueue(int capacity)
		{
			if(capacity <= 0 )
				capacity = 10;
			this.capacity = capacity;
			log.Debug("Queue::Queue:capacity : " + capacity);
		}
		#endregion
	
		#region Properties
		public int Capacity
		{
			get
			{
				return capacity;
			}
		}

		public int Size
		{
			get
			{
				return queue.Count;
			}
		}

		#endregion

		#region PublicMethods 
		
		public Status Enqueue(object obj)
		{
			Status status = Status.Success ; 

			lock (qLock)
			{
				abortDequeue = false;
				if(queue.Count == capacity)
				{
					log.Warn("Queue::Enqueue:Queue overflow");
					status = Status.Overflow;
				}
				else
				{
					try
					{
						queue.Enqueue(obj);
						if (queue.Count >= 1)
							Monitor.Pulse(qLock);
					}
					catch
					{
						status = Status.Error;
					}
				}
			}

			return status;
		}
    
	
		public Status Dequeue (out object obj) 
		{
			return this.Dequeue(out obj, Timeout.Infinite) ; 
		}


		public Status Dequeue(out object obj,int timeout)
		{
			Status status = Status.Success; 

			obj = null;

			lock (qLock)
			{
				abortDequeue = false;
				while (queue.Count == 0)
				{
					if (Monitor.Wait(qLock,timeout) == false)
						return Status.Timeout; 
					if(abortDequeue == true)
						return Status.Aborted;
				}
				obj = queue.Dequeue();
			}
			
			return status;
		}

		
		public void Abort() 
		{
			lock (qLock) 
			{
				abortDequeue = true ; 
				Monitor.PulseAll(qLock) ; 
			}
		}
		/// <summary>
		/// Removes all the elements from the Queue.
		/// </summary>
		public void AbortAndClear()
		{
			lock (qLock) 
			{
				abortDequeue = true; 
				Monitor.PulseAll(qLock) ; 
				
				queue.Clear();
			}
		}
		#endregion
	}
}
