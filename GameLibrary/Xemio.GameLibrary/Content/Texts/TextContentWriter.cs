using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Texts
{
    public abstract class TextContentWriter<T> : ContentWriter<T>
    {
        #region Properties
        /// <summary>
        /// Gets the encoding.
        /// </summary>
        protected virtual Encoding Encoding
        {
            get { return Encoding.Default; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Writes the specified value into the string builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="value">The value.</param>
        protected abstract void Write(StringBuilder builder, T value);
        #endregion

        #region Overrides of ContentWriter<T>
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, T value)
        {
            StreamWriter streamWriter = new StreamWriter(writer.Stream, this.Encoding);
            StringBuilder builder = new StringBuilder();

            this.Write(builder, value);

            streamWriter.Write(builder.ToString());
        }
        #endregion
    }
}
