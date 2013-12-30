using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Formats.Fallback
{
    public class FallbackWriter : IFormatWriter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FallbackWriter" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="exception">The exception.</param>
        public FallbackWriter(Stream stream, Exception exception)
        {
            this.Stream = stream;
            this.Exception = exception;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the exception.
        /// </summary>
        public Exception Exception { get; private set; }
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
        #endregion

        #region Implementation of IFormatWriter
        /// <summary>
        /// Gets the stream.
        /// </summary>
        public Stream Stream { get; private set; }
        /// <summary>
        /// Begins the specified section.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public IDisposable Section(string tag)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified unsigned integer value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteUnsignedInteger(string tag, uint value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified unsigned short value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteUnsignedShort(string tag, ushort value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified unsigned long value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteUnsignedLong(string tag, ulong value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified integer value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteInteger(string tag, int value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified short value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteShort(string tag, short value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified long value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteLong(string tag, long value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified float value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteFloat(string tag, float value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified double value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteDouble(string tag, double value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified decimal value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteDecimal(string tag, decimal value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified boolean value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteBoolean(string tag, bool value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified byte value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteByte(string tag, byte value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified byte array value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteBytes(string tag, byte[] value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified character value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteCharacter(string tag, char value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified string value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteString(string tag, string value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified vector value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteVector2(string tag, Vector2 value)
        {
            throw new FallbackException(this.Exception);
        }
        /// <summary>
        /// Writes the specified rectangle value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteRectangle(string tag, Rectangle value)
        {
            throw new FallbackException(this.Exception);
        }
        #endregion
    }
}
