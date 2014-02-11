using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.Streams
{
    public class LinkedMemoryStream : MemoryStream
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedMemoryStream"/> class.
        /// </summary>
        /// <param name="linkedStream">The linked stream.</param>
        public LinkedMemoryStream(Stream linkedStream)
        {
            this.LinkedStream = linkedStream;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the linked stream.
        /// </summary>
        public Stream LinkedStream { get; private set; }
        #endregion

        #region Overrides of MemoryStream
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.IO.MemoryStream"/> class and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            this.LinkedStream.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
