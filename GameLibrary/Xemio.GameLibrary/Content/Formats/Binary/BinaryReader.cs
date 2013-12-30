using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Formats.Binary
{
    using IO = System.IO;

    public class BinaryReader : IFormatReader
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryReader"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public BinaryReader(Stream stream)
        {
            this.Stream = stream;
            this._reader = new IO.BinaryReader(this.Stream);
        }
        #endregion

        #region Fields
        private IO.BinaryReader _reader;
        #endregion

        #region Implementation of IFormatReader
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
            return new ActionDisposable(() => { });
        }
        /// <summary>
        /// Reads an unsigned integer value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public uint ReadUnsignedInteger(string tag)
        {
            return this._reader.ReadUInt32();
        }
        /// <summary>
        /// Reads an unsigned short value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public ushort ReadUnsignedShort(string tag)
        {
            return this._reader.ReadUInt16();
        }
        /// <summary>
        /// Reads an unsigned long value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public ulong ReadUnsignedLong(string tag)
        {
            return this._reader.ReadUInt64();
        }
        /// <summary>
        /// Reads an integer value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public int ReadInteger(string tag)
        {
            return this._reader.ReadInt32();
        }
        /// <summary>
        /// Reads a short value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public short ReadShort(string tag)
        {
            return this._reader.ReadInt16();
        }
        /// <summary>
        /// Reads a long value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public long ReadLong(string tag)
        {
            return this._reader.ReadInt64();
        }
        /// <summary>
        /// Reads a float value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public float ReadFloat(string tag)
        {
            return this._reader.ReadSingle();
        }
        /// <summary>
        /// Reads a double value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public double ReadDouble(string tag)
        {
            return this._reader.ReadDouble();
        }
        /// <summary>
        /// Reads a decimal value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public decimal ReadDecimal(string tag)
        {
            return this._reader.ReadDecimal();
        }
        /// <summary>
        /// Reads a boolean value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public bool ReadBoolean(string tag)
        {
            return this._reader.ReadBoolean();
        }
        /// <summary>
        /// Reads a byte value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public byte ReadByte(string tag)
        {
            return this._reader.ReadByte();
        }
        /// <summary>
        /// Reads a byte array value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public byte[] ReadBytes(string tag)
        {
            return this._reader.ReadBytes(this._reader.ReadInt32());
        }
        /// <summary>
        /// Reads a character value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public char ReadCharacter(string tag)
        {
            return this._reader.ReadChar();
        }
        /// <summary>
        /// Reads a string value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public string ReadString(string tag)
        {
            return this._reader.ReadString();
        }
        /// <summary>
        /// Reads a vector2 value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public Vector2 ReadVector2(string tag)
        {
            return new Vector2(
                this.ReadFloat("X"),
                this.ReadFloat("Y"));
        }
        /// <summary>
        /// Reads a rectangle value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public Rectangle ReadRectangle(string tag)
        {
            return new Rectangle(
                this.ReadFloat("X"),
                this.ReadFloat("Y"),
                this.ReadFloat("Width"),
                this.ReadFloat("Height"));
        }
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
        #endregion
    }
}
