using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

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
            get { return XGL.Components.Get<ContentManager>(); }
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
        void IContentWriter.Write(IFormatWriter writer, object value)
        {
            this.Write(writer, (T)value);
        }
        #endregion
    }
}
