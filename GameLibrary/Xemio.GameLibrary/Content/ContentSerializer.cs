using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content
{
    public abstract class ContentSerializer<T> : IContentSerializer
    {
        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Id
        {
            get { return typeof (T); }
        }
        /// <summary>
        /// Gets the content.
        /// </summary>
        public ContentManager Content
        {
            get { return XGL.Components.Get<ContentManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public abstract T Read(BinaryReader reader);
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public abstract void Write(BinaryWriter writer, T value);
        #endregion

        #region IContentReader Member
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        object IContentReader.Read(BinaryReader reader)
        {
            return this.Read(reader);
        }
        #endregion

        #region IContentWriter Member
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        void IContentWriter.Write(BinaryWriter writer, object value)
        {
            this.Write(writer, (T)value);
        }
        #endregion
    }
}
