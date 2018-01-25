using System.Collections.Generic;
using System.Threading;

namespace SmartShelf.Common
{
    public class ThreadSalfBlockingQueue<T>
    {
        private readonly Queue<T> _queue;
        private readonly AutoResetEvent _readLock = new AutoResetEvent(true);
        private readonly object _thisObject = new object();
        private readonly AutoResetEvent _writeLock = new AutoResetEvent(true);
        private bool _readWaitAborted;

        protected ThreadSalfBlockingQueue()
            : this(1000)
        {
        }
        protected ThreadSalfBlockingQueue(int capacity)
        {
            _queue = capacity == 0 ? new Queue<T>() : new Queue<T>(capacity);
        }

        public int Count
        {
            get
            {
                lock (_queue)
                {
                    return _queue.Count;
                }
            }
        }

        public int MaxCount { get; private set; } = int.MaxValue - 1;


        public void Enqueue(T item)
        {
            lock (_writeLock)
            {
                while (_queue.Count >= MaxCount)
                {
                    _writeLock.WaitOne(Timeout.Infinite, false);
                }
                lock (_queue)
                {
                    _queue.Enqueue(item);
                }
                _readWaitAborted = false;
                _readLock.Set();
            }
        }

        /// <summary>
        /// 出队
        /// Dequeue 方法会阻塞线程，如果要终止阻塞，则可以调用 AbortReadWait 方法。
        /// 当 AbortReadWait 方法调用后，WaitAbortException 异常将被抛出。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="WaitAbortException">当 AbortReadWait 方法调用后</exception>
        public T Dequeue()
        {
            lock (_readLock)
            {
                while (_queue.Count <= 0)
                {
                    _readLock.WaitOne(Timeout.Infinite, false);
                    if (_readWaitAborted)
                    {
                        _readWaitAborted = false;
                        throw new WaitAbortException();
                    }
                }
                lock (_queue)
                {
                    var item = _queue.Dequeue();
                    _writeLock.Set();
                    return item;
                }
            }
        }

        /// <summary>
        /// 重新设置队列的最大容量
        /// </summary>
        /// <param name="maxCount">大于1的整数</param>
        public void ResetMaxCount(int maxCount)
        {
            lock (_thisObject)
            {
                if (maxCount > 1 || maxCount < int.MaxValue)
                {
                    MaxCount = maxCount;
                }
            }
        }

        /// <summary>
        /// Resets the read wait.
        /// 调用此方法会导致 Pop 方法引发 WaitAbortException 异常。
        /// </summary>
        public void AbortReadWait()
        {
            _readWaitAborted = true;
            _readLock.Set();
        }
    }
}