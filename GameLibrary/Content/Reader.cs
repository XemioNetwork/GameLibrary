using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content
{
    public abstract class Reader<T> : IReader
    {
        #region Properties
        /// <summary>
        /// Gets the serializer.
        /// </summary>
        public SerializationManager SerializationManager
        {
            get { return XGL.Components.Get<SerializationManager>(); }
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
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        object IReader.Read(IFormatReader reader)
        {
            return this.Read(reader);
        }
        #endregion
    }
}
