using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content
{
    public abstract class ContentReader<T> : IContentReader
    {
        #region Methods
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public abstract T Read(BinaryReader reader);
        #endregion

        #region IContentReader Member
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type
        {
            get { return typeof(T); }
        }
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        object IContentReader.Read(BinaryReader reader)
        {
            return this.Read(reader);
        }
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public virtual object Read(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                BinaryReader reader = new BinaryReader(stream);
                return this.Read(reader);
            }
        }
        #endregion
    }
}
