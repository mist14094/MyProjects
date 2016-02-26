using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

namespace KTone.RFIDGlobal
{
    /// <summary>
    /// Summary description for Queue.
    /// </summary>
    [Serializable]
    public class GenericObjectQueue<T>
    {
        #region Attributes
        
        private readonly object qLock = new object();

        private Queue<T> queue = new Queue<T>();

        private int capacity = 10;
        public enum Status 
        { 
            Success = 0, 
            Aborted, 
            Timeout, 
            Overflow, 
            Empty, 
            Error 
        } ;

        /// <summary>
        /// This flag indicates the Dequeue operation needs to be aborted.
        /// </summary>
        [NonSerialized]
        private bool abortDequeue = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a Queue object with default capacity = 10.It returns Overflow status 
        /// if actual size exceeds capacity.
        /// </summary>
        public GenericObjectQueue()
        {
            this.capacity = 50;
            //log.Debug("Queue::Queue:capacity : " + capacity);
        }

        /// <summary>
        /// Initializes a Queue object with given capacity.It returns Overflow status 
        /// if actual size exceeds capacity.
        /// </summary>
        /// <param name="capacity">Capacity of queue</param>
        public GenericObjectQueue(int capacity)
        {
            if (capacity <= 0)
                capacity = 50;
            this.capacity = capacity;
            //og.Debug("Queue::Queue:capacity : " + capacity);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or Sets capacity of queue
        /// </summary>
        public int Capacity
        {
            get
            {
                return capacity;
            }
        }

        /// <summary>
        /// Gets size of queue
        /// </summary>
        public int Size
        {
            get
            {
                return queue.Count;
            }
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Enques an object in a queue
        /// </summary>
        /// <param name="obj">Object to be enqued</param>
        /// <returns>status of enqueing</returns>
        public Status Enqueue(T obj)
        {
            Status status = Status.Success;

            lock (qLock)
            {
                abortDequeue = false;
                if (queue.Count == capacity)
                {
                    queue.Dequeue();
                    //log.Warn("Queue::Enqueue:Queue overflow");
                    status = Status.Overflow;
                }

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

            return status;
        }


        /// <summary>
        /// Dequeues object
        /// </summary>
        /// <param name="obj">object to be dequeued</param>
        /// <returns>Status of dequeue</returns>
        public T Dequeue()
        {
            lock (qLock)
            {
                return queue.Dequeue();
            }
        }

        /// <summary>
        /// Dequeues object
        /// </summary>
        /// <param name="obj">object to be dequeued</param>
        /// <returns>Status of dequeue</returns>
        public Status Dequeue(out T obj)
        {
            return this.Dequeue(out obj, Timeout.Infinite);
        }

        /// <summary>
        /// Dequeues object
        /// </summary>
        /// <param name="obj">object to be dequeued</param>
        /// <param name="timeout">Time out</param>
        /// <returns>Status of dequeue</returns>
        public Status Dequeue(out T obj, int timeout)
        {
            Status status = Status.Success;

            obj = default(T);

            lock (qLock)
            {
                abortDequeue = false;
                while (queue.Count == 0)
                {
                    if (Monitor.Wait(qLock, timeout) == false)
                        return Status.Timeout;
                    if (abortDequeue == true)
                        return Status.Aborted;
                }
                obj = queue.Dequeue();
            }

            return status;
        }

        /// <summary>
        /// Abort dequeue process
        /// </summary>
        public void Abort()
        {
            lock (qLock)
            {
                abortDequeue = true;
                Monitor.PulseAll(qLock);
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
                Monitor.PulseAll(qLock);

                queue.Clear();
            }
        }
        public IEnumerator GetEnumerator()
        {
            return queue.GetEnumerator();
        }
        #endregion
    }
}
