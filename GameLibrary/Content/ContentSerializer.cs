using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content
{
    public abstract class Serializer<T> : IContentReader, IContentWriter
    {
        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public virtual Type Id
        {
            get { return typeof(T); }
        }
        /// <summary>
        /// Gets a value indicating whether to bypass the current content format.
        /// </summary>
        public virtual bool BypassFormat
        {
            get { return false; }
        }
        /// <summary>
        /// Gets the content.
        /// </summary>
        public SerializationManager SerializationManager
        {
            get { return XGL.Components.Get<SerializationManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public abstract T Read(IFormatReader reader);
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public abstract void Write(IFormatWriter writer, T value);
        #endregion

        #region IContentReader Member
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        object IContentReader.Read(IFormatReader reader)
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
        void IContentWriter.Write(IFormatWriter writer, object value)
        {
            this.Write(writer, (T)value);
        }
        #endregion
    }
}
