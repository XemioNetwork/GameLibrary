using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Synchronization;

namespace Xemio.GameLibrary.Network.Packages
{
    public abstract class BinaryPackage : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryPackage"/> class.
        /// </summary>
        protected BinaryPackage()
        {
            this.Stream = new MemoryStream();
        }
        #endregion

        #region Fields
        private MemoryStream _stream;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the data.
        /// </summary>
        public byte[] Data
        {
            get { return this.Stream.ToArray(); }
            set
            {
                MemoryStream stream = new MemoryStream(value.Length);
                stream.Write(value, 0, value.Length);

                this.Stream = stream;
            }
        }
        /// <summary>
        /// Gets or sets the stream.
        /// </summary>
        [ExcludeSync]
        public MemoryStream Stream 
        {
            get 
            {
                if (this._stream.CanSeek)
                {
                    this._stream.Seek(0, SeekOrigin.Begin);
                }

                return this._stream;
            }
            set
            {
                this._stream = value;

                this.Writer = new BinaryWriter(this._stream);
                this.Reader = new BinaryReader(this._stream);
            }
        }
        /// <summary>
        /// Gets the writer.
        /// </summary>
        [ExcludeSync]
        public BinaryWriter Writer { get; private set; }
        /// <summary>
        /// Gets the reader.
        /// </summary>
        [ExcludeSync]
        public BinaryReader Reader { get; private set; }
        #endregion
    }
}
