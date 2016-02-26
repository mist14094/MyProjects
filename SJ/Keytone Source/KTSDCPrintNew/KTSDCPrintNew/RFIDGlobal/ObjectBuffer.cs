
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
	public delegate void DataLossEventHandler(ObjectBuffer bufObj,DataLossEventArgs e);

	/// <summary>
	/// A structure holding details of connection
	/// a unique connectionID string,
	/// a flag indicating the data to be read is overwritten, 
	/// readIndex indicating position in the object array it last read
	/// </summary>
	public struct ConnectionInfo
	{
		public string connectionID;
		public bool isOverWritten;
		public int readIndex;
		public DataLossEventHandler dataLossHandlerDel;
		

		public ConnectionInfo(string connectionID,bool isOverWritten,int readIndex)
		{
			this.connectionID = connectionID;
			this.isOverWritten  = isOverWritten;
			this.readIndex   = readIndex;
			dataLossHandlerDel = null;

		}

	}
	
	
	public class DataLossEventArgs : EventArgs
	{
		public string dataLossEventId;
		public string connectionID;
		public object objectOverWritten;

		public DataLossEventArgs(string connectionID,object objectOverWritten,string dataLossEventId)
		{
			this.connectionID = connectionID;
			this.objectOverWritten = objectOverWritten;
			this.dataLossEventId = dataLossEventId;

		}

	}

	
	/// <summary>
	/// Summary description for ObjectBuffer.
	/// </summary>
	public class ObjectBuffer
	{

		#region Attributes

		private string bufferID;
		
		private object[] objArray;

		private int capacity = 100;

		private int size ; //size of buffer

		private int writeIndex = 0; //position at which tag is to be added in the array

		private Hashtable CIHash = new Hashtable();//Hashtable of ConnectionID string Vs ConnectionInfo 

		static int connectionCnt = 0;
		static int MAX_CONNECTIONS = 5000;


        private static readonly NLog.Logger log = KTLogger.KTLogManager.GetLogger();
			

		private readonly object clientLock = new object();
		private readonly object arrLock = new object();

		public enum  Status { Success = 0 ,Aborted, Timeout,  OverWrite , Empty, Error } ; 
	
		/// <summary>
		/// This flag indicates the Dequeue operation needs to be aborted.
		/// </summary>
		private bool abortGetObject = false; 

		private event DataLossEventHandler OnDataLoss;

		private static int dataLossEventIndex = 1;

		private object dataLossEventIndexLock = new object();

		#endregion 


		public ObjectBuffer(string bufferID, int capacity)
		{
			this.bufferID = bufferID;
			if(capacity > 0)
				this.capacity = capacity;
			objArray = new object[capacity];
			this.Clear() ; 	
			
			if ( log.IsDebugEnabled ) 
			{
				String str = String.Format("Buffer created size = {0}", this.capacity); 
				log.Debug(str) ;
			}

		}


		#region Properties
		public int Capacity
		{
			get
			{
				return capacity;
			}
		}
		
		/// <summary>
		/// Get the number of elements in Queue 
		/// </summary>
		/// <returns></returns>
		public int Size
		{
			get 
			{
				return size ;
			}
		}
		#endregion 


		#region Public Methods

		/// <summary>
		/// Clear the buffer
		/// </summary>
		public void Clear() 
		{
			writeIndex = 0 ;
			size = 0 ;
			abortGetObject = false ; 
			Array.Clear(objArray, 0, objArray.Length);
		}



		/// <summary>
		/// Generate a unique connectionID .
		/// adds the client with that ID in the Hashtable CIHash
		/// </summary>
		/// <returns> String ConnectionID</returns>
		public string AddClient()
		{
			lock(clientLock)
			{
				if(connectionCnt > MAX_CONNECTIONS)
					connectionCnt = 1;
			
				int connectionId  = connectionCnt++;
				string conIDStr =  "CONNECT" + connectionId.ToString("D5") + connectionCnt;
						
				int readindex = writeIndex;
				if(readindex == capacity)
					readindex = 0;

				CIHash.Add(conIDStr, new ConnectionInfo(conIDStr, false, readindex));
				return conIDStr;
			}

		}


		/// <summary>
		/// If connection exists in the CIHash,Removes it from the same.
		/// </summary>
		/// <param name="conID"> string </param>
		public void RemoveClient(string conID)
		{
			lock(clientLock)
			{
				CheckConnection(conID);
				CIHash.Remove(conID);
			}
		}



		/// <summary>
		/// Checks whether a given conID exists in the CIHash.
		/// </summary>
		/// <param name="conID">string</param>
		public void CheckConnection(string conID)
		{
			if(CIHash.ContainsKey(conID) == false)
			{
				throw new ApplicationException("Client not Added");
			}
			else
			{
				if(CIHash[conID] == null)
					throw new ApplicationException("Client not Added");

			}
		}



		public void Abort() 
		{
			lock (arrLock)  
			{
				abortGetObject = true ; 
				Monitor.PulseAll(arrLock) ; 
				Console.WriteLine ("st = Aborted");
			}

		}

		/// <summary>
		/// Removes all the elements from the objectArray.
		/// </summary>
		public void AbortAndClear()
		{
			lock (arrLock) 
			{
				Clear();
				Monitor.PulseAll(arrLock) ; 
				abortGetObject = true; 
						
			}
		}


		/// <summary>
		/// Checks the capacity of the Object Array.
		/// If it is full starts writing from the first position,returns a flag indicating overflow. 
		/// Now check readIndices of all the connections.
		/// Each connection, having the readIndex same as writeindex(Where the tag is getting added now)
		/// is informed by setting isOverWritten = true.
		/// The object is added and WriteIndex is incremented.
		/// </summary>
		/// <param name="tag"></param>
		/// <returns>Status</returns>
		public Status AddData(object bufferItem)
		{
			abortGetObject = false;
			Status status = Status.Success ; 
			lock(arrLock)
			{
				//Check capacity
				//if(writeIndex == capacity)//the array is full,start from first position again
				if(size == capacity)//the array is full,start from first position again
				{
					log.Warn("objectBuffer::AddObject ::overflow, Data is overwritten.");
					if(writeIndex ==  capacity)
						writeIndex = 0;
					status = Status.OverWrite;
					
				}
				//compare read indices of all the clients ,
				//if any readindex matches with writeindex make isOverWritten = true as
				//the data that client was reading, is now going to be overwritten.
				foreach(DictionaryEntry de in CIHash)
				{
					ConnectionInfo conInfo = (ConnectionInfo)de.Value;
					if(status == Status.OverWrite)
					{
						object  objOverWritten = objArray[writeIndex];
						string dataLossEventId = CreateDataLossEventId();
						DataLossEventArgs  eventArgs = 
							new DataLossEventArgs(conInfo.connectionID, objOverWritten, dataLossEventId);
						OnDataLoss(this, eventArgs);
					}
					if(size != 0)
					{					
						if(writeIndex == conInfo.readIndex)
						{	
							conInfo.isOverWritten = true;
							
						}
					}
				
				}

				objArray[writeIndex++] = bufferItem;
				
				size++;
				if(size > capacity)
				{
					size = capacity ; 

				}
				if(size > 0)
				{					
					Monitor.Pulse(arrLock) ; 
				}
			}
			return status;
			
		}



		/// <summary>
		/// Returns array of given no of object for given connectionID
		/// </summary>
		/// <param name="conID"></param>
		/// <param name="retCount"></param>
		/// <param name="status"></param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		public object[]  GetData(string conID, int retCount, out Status status, int timeout)
		{
			abortGetObject = false;
			status = Status.Success ; 
			
			lock(arrLock)
			{
				CheckConnection(conID);

				ConnectionInfo conInfo = (ConnectionInfo)CIHash[conID];
				
				//cannot read tags more than the capacity
				if(retCount > capacity) 
					retCount = capacity;
				
				ArrayList reqItemList = new ArrayList();
	
				for(int items = 0; items < retCount; items++) //wait till req no of items are added in the buffer
				{					
					object bufferItem = GetBufferItem(out status, conInfo.readIndex, timeout);

					if(status != Status.Success)
						break;

					reqItemList.Add(bufferItem);
					
					conInfo.readIndex++;
					if(conInfo.readIndex == capacity)
						conInfo.readIndex = 0;
				}

				
				object[] reqBufferItems =  (object[])reqItemList.ToArray(typeof(object));

				conInfo.isOverWritten = false;
				CIHash[conID] = conInfo;
				return reqBufferItems;
			}
		}



		/// <summary>
		/// Reads object from the buffer.Waits till timeout
		/// </summary>
		/// <param name="status"></param>
		/// <param name="readIndex"></param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		private object GetBufferItem(out Status status,int readIndex,int timeout)
		{
			abortGetObject = false;
			status = Status.Success ; 
			object bufferItem = new object();
			
			lock(arrLock)
			{
				if(writeIndex != readIndex)
					return objArray[readIndex];

				//write and read indices are same
				//wait till a tag is added in the buffer
								
				if ( Monitor.Wait(arrLock, timeout) == false ) 
				{
					status  = Status.Timeout ; 
					return bufferItem;
				}
				if ( abortGetObject == true ) 
				{
					status  = Status.Aborted ; 
					return bufferItem;
				}
								
				return objArray[readIndex];
			}
		}


		#region DataLoss

		public void RegisterEvent(string conID,DataLossEventHandler del)
		{
			CheckConnection(conID);

			ConnectionInfo conInfo = (ConnectionInfo)CIHash[conID];

			if(del != null)
				conInfo.dataLossHandlerDel = del;

			OnDataLoss += del;

		}

		
		public void UnRegisterEvent(string conID,DataLossEventHandler del)
		{
			CheckConnection(conID);

			ConnectionInfo conInfo = (ConnectionInfo)CIHash[conID];
			
			conInfo.dataLossHandlerDel = null;

			OnDataLoss -= del;

		}

		
		private string CreateDataLossEventId()
		{
			int eventIndex = 0;
			
			lock(dataLossEventIndexLock)
			{
				eventIndex = dataLossEventIndex;
				dataLossEventIndex++;
			}
			
			DateTime curTime = DateTime.Now;
			
			string dataLossEventId = String.Format("DataLossEvent{0:d5}-{1}", 
				eventIndex, curTime.Ticks);
			return dataLossEventId;
		}

		
		#endregion

		#endregion

	}
}
