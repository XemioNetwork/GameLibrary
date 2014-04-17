using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Texts
{
    public abstract class TextSerializer<T> : Serializer<T>
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
        /// Reads the specified input.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        protected abstract T Read(Stream stream, string input);
        /// <summary>
        /// Writes the specified value into the string builder.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="builder">The builder.</param>
        /// <param name="value">The value.</param>
        protected abstract void Write(Stream stream, StringBuilder builder, T value);
        #endregion

        #region Overrides of ContentReader<T>, ContentWriter<T>
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override T Read(IFormatReader reader)
        {
            StreamReader streamReader = new StreamReader(reader.Stream, this.Encoding);
            string input = streamReader.ReadToEnd();

            return this.Read(reader.Stream, input);
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, T value)
        {
            StreamWriter streamWriter = new StreamWriter(writer.Stream, this.Encoding);
            StringBuilder builder = new StringBuilder();

            this.Write(writer.Stream, builder, value);

            streamWriter.Write(builder.ToString());
        }
        #endregion
    }
}
