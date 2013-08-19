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
        private readonly ConcurrentQueue<QueuedPackage> _packageQueue; 
        private readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageSender"/> class.
        /// </summary>
        public PackageSender()
        {
            this._packageQueue = new ConcurrentQueue<QueuedPackage>();
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
            this._packageQueue.Enqueue(new QueuedPackage(package, receiver));
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

                QueuedPackage queuedPackage;
                while (this._packageQueue.TryDequeue(out queuedPackage))
                {
                    queuedPackage.Receiver.Send(queuedPackage.Package);
                }
            }
        }
        #endregion Private Methods

        private class QueuedPackage
        {
            #region Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="QueuedPackage"/> class.
            /// </summary>
            /// <param name="package">The package.</param>
            /// <param name="receiver">The receiver.</param>
            public QueuedPackage(Package package, IPackageSender receiver)
            {
                this.Package = package;
                this.Receiver = receiver;
            }
            #endregion

            #region Properties
            /// <summary>
            /// Gets the package.
            /// </summary>
            public Package Package { get; private set; }
            /// <summary>
            /// Gets the receiver.
            /// </summary>
            public IPackageSender Receiver { get; private set; }
            #endregion
        }
    }
}
