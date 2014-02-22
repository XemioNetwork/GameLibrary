using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    public class SignaledQueue<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SignaledQueue{T}"/> class.
        /// </summary>
        public SignaledQueue()
        {
            this._signal = new AutoResetEvent(false);
            this._items = new Queue<T>();
        } 
        #endregion

        #region Fields
        private readonly AutoResetEvent _signal;
        private readonly Queue<T> _items; 
        #endregion

        #region Methods
        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Enqueue(T item)
        {
            this._items.Enqueue(item);
            this._signal.Set();
        }
        /// <summary>
        /// Dequeues the next item inside the queue.
        /// </summary>
        public T Dequeue()
        {
            this._signal.WaitOne();
            return this._items.Dequeue();
        }
        #endregion
    }
}
