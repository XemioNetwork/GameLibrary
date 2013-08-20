using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
            this._reader = new IO.BinaryReader(stream);
            this.Stream = stream;
        }
        #endregion

        #region Fields
        private readonly IO.BinaryReader _reader;
        #endregion

        #region Implementation of IFormatReader
        /// <summary>
        /// Gets the stream.
        /// </summary>
        public Stream Stream { get; private set; }
        /// <summary>
        /// Begins a new section.
        /// </summary>
        public void BeginSection()
        {
        }
        /// <summary>
        /// Ends the current section.
        /// </summary>
        public void EndSection()
        {
        }
        /// <summary>
        /// Reads an unsigned integer value.
        /// </summary>
        public uint ReadUnsignedInteger()
        {
            return this._reader.ReadUInt32();
        }
        /// <summary>
        /// Reads an unsigned short value.
        /// </summary>
        public ushort ReadUnsignedShort()
        {
            return this._reader.ReadUInt16();
        }
        /// <summary>
        /// Reads an unsigned long value.
        /// </summary>
        public ulong ReadUnsignedLong()
        {
            return this._reader.ReadUInt64();
        }
        /// <summary>
        /// Reads an integer value.
        /// </summary>
        public int ReadInteger()
        {
            return this._reader.ReadInt32();
        }
        /// <summary>
        /// Reads a short value.
        /// </summary>
        public short ReadShort()
        {
            return this._reader.ReadInt16();
        }
        /// <summary>
        /// Reads a long value.
        /// </summary>
        public long ReadLong()
        {
            return this._reader.ReadInt64();
        }
        /// <summary>
        /// Reads a float value.
        /// </summary>
        public float ReadFloat()
        {
            return this._reader.ReadSingle();
        }
        /// <summary>
        /// Reads a double value.
        /// </summary>
        public double ReadDouble()
        {
            return this._reader.ReadDouble();
        }
        /// <summary>
        /// Reads a decimal value.
        /// </summary>
        public decimal ReadDecimal()
        {
            return this._reader.ReadDecimal();
        }
        /// <summary>
        /// Reads a boolean value.
        /// </summary>
        public bool ReadBoolean()
        {
            return this._reader.ReadBoolean();
        }
        /// <summary>
        /// Reads a byte value.
        /// </summary>
        public byte ReadByte()
        {
            return this._reader.ReadByte();
        }
        /// <summary>
        /// Reads a byte array value.
        /// </summary>
        /// <param name="length">The length.</param>
        public byte[] ReadBytes(int length)
        {
            return this._reader.ReadBytes(length);
        }
        /// <summary>
        /// Reads a character value.
        /// </summary>
        public char ReadCharacter()
        {
            return this._reader.ReadChar();
        }
        /// <summary>
        /// Reads a string value.
        /// </summary>
        public string ReadString()
        {
            return this._reader.ReadString();
        }
        /// <summary>
        /// Reads a vector2 value.
        /// </summary>
        public Vector2 ReadVector2()
        {
            return new Vector2(
                this.ReadFloat(),
                this.ReadFloat());
        }
        /// <summary>
        /// Reads a rectangle value.
        /// </summary>
        public Rectangle ReadRectangle()
        {
            return new Rectangle(
                this.ReadFloat(),
                this.ReadFloat(),
                this.ReadFloat(),
                this.ReadFloat());
        }
        #endregion
    }
}
