using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content
{
    public abstract class ContentWriter<T> : IContentWriter
    {
        #region Properties
        /// <summary>
        /// Gets the content.
        /// </summary>
        public ContentManager Content
        {
            get { return XGL.GetComponent<ContentManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public abstract void Write(BinaryWriter writer, T value);
        #endregion

        #region IContentWriter Member
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type
        {
            get { return typeof(T); }
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value">The value.</param>
        void IContentWriter.Write(BinaryWriter writer, object value)
        {
            this.Write(writer, (T)value);
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="value">The value.</param>
        public virtual void Write(string fileName, object value)
        {
            using (Stream stream = this.Content.FileSystem.Open(fileName))
            {
                BinaryWriter writer = new BinaryWriter(stream);
                this.Write(writer, (T)value);
            }
        }
        #endregion
    }
}
