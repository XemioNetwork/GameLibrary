using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content
{
    public abstract class Writer<T> : IWriter
    {
        #region Properties
        /// <summary>
        /// Gets the serialization manager.
        /// </summary>
        public SerializationManager SerializationManager
        {
            get { return XGL.Components.Get<SerializationManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The format writer.</param>
        /// <param name="value">The value.</param>
        public abstract void Write(IFormatWriter writer, T value);
        #endregion

        #region IContentWriter Member
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Id
        {
            get { return typeof(T); }
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The format writer</param>
        /// <param name="value">The value.</param>
        void IWriter.Write(IFormatWriter writer, object value)
        {
            this.Write(writer, (T)value);
        }
        #endregion
    }
}
