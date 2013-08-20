using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Formats.Binary
{
    using IO = System.IO;

    public class BinaryWriter : IFormatWriter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryWriter"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public BinaryWriter(Stream stream)
        {
            this._writer = new IO.BinaryWriter(stream);
            this.Stream = stream;
        }
        #endregion

        #region Fields
        private readonly IO.BinaryWriter _writer;
        #endregion
        
        #region Implementation of IFormatWriter
        /// <summary>
        /// Gets the stream.
        /// </summary>
        public Stream Stream { get; private set; }
        /// <summary>
        /// Begins a new section.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public void BeginSection(string tag)
        {
        }
        /// <summary>
        /// Ends the current section.
        /// </summary>
        public void EndSection()
        {
        }
        /// <summary>
        /// Writes the specified unsigned integer value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteUnsignedInteger(string tag, uint value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified unsigned short value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteUnsignedShort(string tag, ushort value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified unsigned long value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteUnsignedLong(string tag, ulong value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified integer value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteInteger(string tag, int value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified short value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteShort(string tag, short value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified long value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteLong(string tag, long value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified float value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteFloat(string tag, float value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified double value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteDouble(string tag, double value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified decimal value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteDecimal(string tag, decimal value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified boolean value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteBoolean(string tag, bool value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified byte value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteByte(string tag, byte value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified byte array value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteBytes(string tag, byte[] value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified character value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteCharacter(string tag, char value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified string value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteString(string tag, string value)
        {
            this._writer.Write(value);
        }
        /// <summary>
        /// Writes the specified vector value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteVector2(string tag, Vector2 value)
        {
            this._writer.Write(value.X);
            this._writer.Write(value.Y);
        }
        /// <summary>
        /// Writes the specified rectangle value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteRectangle(string tag, Rectangle value)
        {
            this._writer.Write(value.X);
            this._writer.Write(value.Y);
            this._writer.Write(value.Width);
            this._writer.Write(value.Height);
        }
        #endregion
    }
}
