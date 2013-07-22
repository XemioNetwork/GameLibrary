using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content.Texts
{
    public abstract class TextSerializer<T> : ContentSerializer<T>
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
        /// <param name="input">The input.</param>
        protected abstract T Read(string input);
        /// <summary>
        /// Writes the specified value into the string builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="value">The value.</param>
        protected abstract void Write(StringBuilder builder, T value);
        #endregion

        #region Overrides of ContentReader<T>, ContentWriter<T>
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override T Read(BinaryReader reader)
        {
            StreamReader streamReader = new StreamReader(reader.BaseStream, this.Encoding);
            string input = streamReader.ReadToEnd();

            return this.Read(input);
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(BinaryWriter writer, T value)
        {
            StreamWriter streamWriter = new StreamWriter(writer.BaseStream);
            StringBuilder builder = new StringBuilder();

            this.Write(builder, value);

            streamWriter.Write(builder.ToString());
        }
        #endregion
    }
}
