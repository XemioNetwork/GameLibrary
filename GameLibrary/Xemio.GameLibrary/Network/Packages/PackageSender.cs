using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Protocols;

namespace Xemio.GameLibrary.Network.Packages
{
    internal class PackageSender
    {
        #region Fields
        private readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);
        private readonly ConcurrentQueue<Tuple<IPackageSender, Package>> _packageQueue = new ConcurrentQueue<Tuple<IPackageSender, Package>>();
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageSender"/> class.
        /// </summary>
        public PackageSender()
        {
            Task.Factory.StartNew(this.SendPackageLoop);
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void Send(Package package, IPackageSender receiver)
        {
            this._packageQueue.Enqueue(new Tuple<IPackageSender, Package>(receiver, package));

            this._waitHandle.Set();
        }
        #endregion Methods

        #region Private Methods
        /// <summary>
        /// The loop sending the packages.
        /// </summary>
        private void SendPackageLoop()
        {
            while (true)
            {
                this._waitHandle.WaitOne();

                Tuple<IPackageSender, Package> tuple;
                if (this._packageQueue.TryDequeue(out tuple))
                {
                    tuple.Item1.Send(tuple.Item2);
                }
            }
        }
        #endregion Private Methods
    }
}
