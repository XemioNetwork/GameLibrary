using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content.Texts
{
    public abstract class TextContentReader<T> : ContentReader<T>
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
        #endregion

        #region Overrides of ContentReader<T>
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override T Read(IFormatReader reader)
        {
            StreamReader streamReader = new StreamReader(reader.Stream, this.Encoding);
            string input = streamReader.ReadToEnd();

            return this.Read(input);
        }
        #endregion
    }
}
