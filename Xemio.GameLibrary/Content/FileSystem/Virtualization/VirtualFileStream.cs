using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content.FileSystem.Virtualization
{
    internal sealed class VirtualFileStream : MemoryStream
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualFileStream"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public VirtualFileStream(VirtualFile file)
        {
            this.File = file;

            this.Write(file.Data, 0, file.Data.Length);
            this.Seek(0, SeekOrigin.Begin);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the file.
        /// </summary>
        public VirtualFile File { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Saves all changes to the file.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            //Copy all changes from the stream to our file array.
            this.File.Data = this.ToArray();

            base.Dispose(disposing);
        }
        #endregion
    }
}
