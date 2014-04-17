using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content
{
    public abstract class ContentReader<T> : IContentReader
    {
        #region Properties
        /// <summary>
        /// Gets the serializer.
        /// </summary>
        protected SerializationManager SerializationManager
        {
            get { return XGL.Components.Require<SerializationManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public abstract T Read(IFormatReader reader);
        #endregion

        #region IContentReader Member
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Id
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
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        object IContentReader.Read(IFormatReader reader)
        {
            return this.Read(reader);
        }
        #endregion
    }
}
